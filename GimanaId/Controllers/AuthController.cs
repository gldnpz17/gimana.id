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

namespace GimanaIdApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public AuthController()
        {

        }
        
        /// <summary>
        /// Log in.
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost("login")]
        public async Task<ActionResult<AuthTokenDto>> Login([FromBody]LoginDto dto)
        {

        }

        /// <summary>
        /// Log out.
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost("logout")]
        public async Task<ActionResult> Logout()
        {

        }

        /// <summary>
        /// Sign up.
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost("sign-up")]
        public async Task<ActionResult> SignUp([FromBody]SignUpDto dto)
        {

        }

        /// <summary>
        /// Send password reset message.
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost("send-password-reset-message")]
        public async Task<ActionResult> SendPasswordResetMessage([FromBody]SendPasswordResetMessageDto dto)
        {

        }

        /// <summary>
        /// Reset password.
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost("reset-password")]
        public async Task<ActionResult> ResetPassword([FromBody]ResetPasswordDto dto)
        {

        }
    }
}