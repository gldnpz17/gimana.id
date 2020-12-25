using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GimanaIdApi.Common.Authentication;
using GimanaIdApi.Common.Config;
using GimanaIdApi.DTOs.Request;
using GimanaIdApi.DTOs.Response;
using GimanaId.DTOs.Response;
using GimanaId.DTOs.Request;

namespace GimanaIdApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        public UsersController()
        {

        }

        /// <summary>
        /// Read user by id.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [Authorize(Policy = AuthorizationPolicyConstants.AuthenticatedUsersOnlyPolicy)]
        [HttpGet("{userId}")]
        public async Task<ActionResult<DetailedUserDto>> ReadUserById([FromRoute]string userId) 
        {

        }

        /// <summary>
        /// Send email verification message.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [Authorize(Policy = AuthorizationPolicyConstants.AuthenticatedUsersOnlyPolicy)]
        [HttpPost("{userId}/send-email-verification-message")]
        public async Task<ActionResult> SendEmailVerificationMessage([FromRoute]string userId)
        {

        }

        /// <summary>
        /// Verify email.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpGet("{userId}/verify-email")]
        public async Task<ActionResult> VerifyEmail([FromRoute]string userId, [FromQuery]string token)
        {

        }

        /// <summary>
        /// Ban user.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        [Authorize(Policy = AuthorizationPolicyConstants.IsNotBannedPolicy)]
        [Authorize(Policy = AuthorizationPolicyConstants.ModeratorOnlyPolicy)]
        [HttpPost("{userId}/ban")]
        public async Task<ActionResult> BanUser([FromRoute]string userId, [FromBody]BanDto dto)
        {

        }

        /// <summary>
        /// Grant privilege.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        [Authorize(Policy = AuthorizationPolicyConstants.AdminOnlyPolicy)]
        [HttpPost("{userId}/privileges")]
        public async Task<ActionResult> GrantPrivilege([FromRoute]string userId, [FromBody]GrantPrivilegeDto dto)
        {

        }

        /// <summary>
        /// Revoke privilege.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        [Authorize(Policy = AuthorizationPolicyConstants.AdminOnlyPolicy)]
        [HttpDelete("{userId}/privileges")]
        public async Task<ActionResult> RevokePrivilege([FromRoute]string userId, [FromBody]RevokePrivilegeDto dto)
        {

        }

        /// <summary>
        /// Delete user.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpDelete("{userId}")]
        public async Task<ActionResult> DeleteUser([FromRoute]string userId) 
        {

        }

        /// <summary>
        /// Get user id.
        /// </summary>
        /// <returns></returns>
        [Authorize(Policy = AuthorizationPolicyConstants.AuthenticatedUsersOnlyPolicy)]
        [HttpGet("get-user-id")]
        public async Task<ActionResult<UserIdDto>> GetUserId()
        {

        }
        
        /// <summary>
        /// Check whether or not a given username is available.
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpGet("check-username-availability/{username}")]
        public async Task<ActionResult<UsernameAvailabilityDto>> CheckUsernameAvailability([FromRoute]string username)
        {

        }

        /// <summary>
        /// Update user profile
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPut("{userId}")]
        public async Task<ActionResult> UpdateUser([FromRoute]string userId, [FromBody]UpdateUserDto dto)
        {

        }

        private async Task<User> GetCurrentUser()
        {

        }
    }
}