using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GimanaIdApi.Common.Config;
using GimanaIdApi.DTOs.Request;
using GimanaIdApi.DTOs.Response;
using MediatR;
using Application.Auth.Commands.Login;
using Application.Auth.Commands.Logout;
using Application.Auth.Commands.SignUp;

namespace GimanaIdApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public AuthController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }
        
        /// <summary>
        /// Log in.
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost("login")]
        public async Task<ActionResult<AuthTokenDto>> Login([FromBody]LoginDto dto)
        {
            var token = await _mediator.Send(new LoginCommand()
            {
                Username = dto.Username,
                Password = dto.Password,
                IpAddress = Request.HttpContext.Connection.RemoteIpAddress.ToString(),
                UserAgent = Request.Headers["User-Agent"],
                IsRemembered = dto.RememberMe
            });

            var cookieOptions = new CookieOptions()
            {
                //Secure = true,
                SameSite = SameSiteMode.Strict,
                HttpOnly = true
            };

            if (dto.RememberMe)
            {
                cookieOptions.Expires = DateTime.Now.AddYears(10);
            }

            Response.Cookies.Append("session-token", token.Token, cookieOptions);

            var output = _mapper.Map<AuthTokenDto>(token);

            return output;
        }

        /// <summary>
        /// Log out.
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost("logout")]
        public async Task<ActionResult> Logout()
        {
            await _mediator.Send(new LogoutCommand()
            {
                AuthToken = User.FindFirst("AuthToken").Value
            });

            return Ok();
        }

        /// <summary>
        /// Sign up.
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost("sign-up")]
        public async Task<ActionResult> SignUp([FromBody]SignUpDto dto)
        {
            await _mediator.Send(new SignUpCommand()
            {
                Username = dto.Username,
                Password = dto.Password,
                EmailAddress = dto.Email
            });

            return Ok();
        }

        /// <summary>
        /// Send password reset message.
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost("send-password-reset-message")]
        public async Task<ActionResult> SendPasswordResetMessage([FromBody]SendPasswordResetMessageDto dto)
        {
            return StatusCode(501); //not implemented
        }

        /// <summary>
        /// Reset password.
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost("reset-password")]
        public async Task<ActionResult> ResetPassword([FromBody]ResetPasswordDto dto)
        {
            return StatusCode(501); //not implemented
        }
    }
}