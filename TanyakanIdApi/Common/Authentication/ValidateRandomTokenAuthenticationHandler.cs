﻿using Microsoft.AspNetCore.Authentication;
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
using TanyakanIdApi.Infrastructure.DataAccess;

namespace TanyakanIdApi.Common.Authentication
{
    public class ValidateRandomTokenAuthenticationHandler : AuthenticationHandler<RandomTokenAuthenticationSchemeOptions>
    {
        private readonly AppDbContext _appDbContext;

        public ValidateRandomTokenAuthenticationHandler(
            IOptionsMonitor<RandomTokenAuthenticationSchemeOptions> options, 
            ILoggerFactory logger, 
            UrlEncoder encoder, 
            ISystemClock clock,
            AppDbContext appDbContext) : base(options, logger, encoder, clock)
        {
            _appDbContext = appDbContext;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            var tokenString = Request.Headers["Auth-Token"].FirstOrDefault();

            if (tokenString == null)
            {
                return AuthenticateResult.Fail("No auth token found in header.");
            }

            var token = await _appDbContext.AuthTokens.FirstOrDefaultAsync(i => i.Token == tokenString);

            if (token == null)
            {
                return AuthenticateResult.Fail("Invalid auth token supplied.");
            }
            else
            {
                var user = token.User;

                var claims = 
                    new List<Claim>()
                    {
                        new Claim("UserId", user.Id.ToString()),
                        new Claim("IsAdmin", user.Privileges.Where(i => i.PrivilegeName == "Admin").Any().ToString()),
                        new Claim("IsModerator", user.Privileges.Where(i => i.PrivilegeName == "Moderator").Any().ToString()),
                        new Claim("EmailVerified", user.Email.IsVerified.ToString())
                    };

                if (user.BanLiftedDate <= DateTime.Now)
                {
                    claims.Add(new Claim("IsBanned", false.ToString()));
                }
                else
                {
                    claims.Add(new Claim("IsBanned", true.ToString()));
                }

                var claimsIdentity = new ClaimsIdentity(claims);

                var ticket = new AuthenticationTicket(new ClaimsPrincipal(claimsIdentity), Scheme.Name);

                return AuthenticateResult.Success(ticket);
            }
        }
    }
}
