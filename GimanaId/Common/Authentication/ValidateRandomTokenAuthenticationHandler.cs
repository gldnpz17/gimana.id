using Microsoft.AspNetCore.Authentication;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using System.Net;
using MediatR;
using Application.Auth.Queries.ReadUserByAuthToken;
using Application.Auth.Commands.VerifyAuthToken;
using DomainModel.Entities;

namespace GimanaIdApi.Common.Authentication
{
    public class ValidateRandomTokenAuthenticationHandler : AuthenticationHandler<RandomTokenAuthenticationSchemeOptions>
    {
        private readonly IMediator _mediator;

        public ValidateRandomTokenAuthenticationHandler(
            IOptionsMonitor<RandomTokenAuthenticationSchemeOptions> options, 
            ILoggerFactory logger, 
            UrlEncoder encoder, 
            ISystemClock clock,
            IMediator mediator) : base(options, logger, encoder, clock)
        {
            _mediator = mediator;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            var tokenString = Request.Cookies["session-token"];

            if (tokenString == null)
            {
                return AuthenticateResult.Fail("No session token found in request cookies.");
            }

            AuthToken token;

            try 
            {
                token = await _mediator.Send(new VerifyAuthTokenCommand()
                {
                    Token = tokenString
                });
            }
            catch (ApplicationException ex)
            {
                Response.Cookies.Delete("session-token");

                return AuthenticateResult.Fail(ex.Message);
            }

            //verify user agent
            var userAgent = Request.Headers["User-Agent"].FirstOrDefault();
            if (token.UserAgent != userAgent) 
            {
                return AuthenticateResult.Fail("User agent in token doesn't match the user agent used.");
            }

            //verify ip address
            var ipAddress = Request.HttpContext.Connection.RemoteIpAddress;
            if (token.IPAddress != ipAddress.ToString()) 
            {
                return AuthenticateResult.Fail("IP address registered in token doesn't match the ip address the request is sent from.");
            }

            var user = token.User;

            var claims = 
                new List<Claim>()
                {
                    new Claim("AuthToken", tokenString),
                    new Claim("UserId", user.Id.ToString()),
                    new Claim("EmailVerified", user.Email.IsVerified.ToString())
                };

            var claimsIdentity = new ClaimsIdentity(claims);

            var ticket = new AuthenticationTicket(new ClaimsPrincipal(claimsIdentity), Scheme.Name);

            return AuthenticateResult.Success(ticket);
        }
    }
}
