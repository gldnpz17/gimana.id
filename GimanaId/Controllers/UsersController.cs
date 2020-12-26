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
using MediatR;
using Application.Users.Queries.ReadUserById;
using Application.Users.Commands.SendEmailVerificationMessage;
using Application.Users.Commands.VerifyEmail;
using DomainModel.Entities;
using Application.Users.Queries.CheckUsernameAvailability;

namespace GimanaIdApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public UsersController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        /// <summary>
        /// Read user by id.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet("{userId}")]
        public async Task<ActionResult<DetailedUserDto>> ReadUserById([FromRoute]string userId) 
        {
            var result = await _mediator.Send(new ReadUserByIdQuery() { Id = Guid.Parse(userId) });

            var output = _mapper.Map<DetailedUserDto>(result);

            return output;
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
            await _mediator.Send(new SendEmailVerificationMessageCommand()
            {
                UserId = Guid.Parse(userId)
            });

            return Ok();
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
            await _mediator.Send(new VerifyEmailCommand()
            {
                VerificationToken = token
            });

            return Ok();
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
            return StatusCode(501); //not yet implemented
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
            return StatusCode(501); //not yet implemented
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
            return StatusCode(501); //not yet implemented
        }

        /// <summary>
        /// Delete user.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpDelete("{userId}")]
        public async Task<ActionResult> DeleteUser([FromRoute]string userId) 
        {
            return StatusCode(501); //not yet implemented
        }

        /// <summary>
        /// Get user id.
        /// </summary>
        /// <returns></returns>
        [Authorize(Policy = AuthorizationPolicyConstants.AuthenticatedUsersOnlyPolicy)]
        [HttpGet("get-user-id")]
        public async Task<ActionResult<UserIdDto>> GetUserId()
        {
            var result = await GetCurrentUser();

            return new UserIdDto()
            {
                Id = result.Id
            };
        }
        
        /// <summary>
        /// Check whether or not a given username is available.
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpGet("check-username-availability/{username}")]
        public async Task<ActionResult<UsernameAvailabilityDto>> CheckUsernameAvailability([FromRoute]string username)
        {
            var result = await _mediator.Send(new CheckUsernameAvailabilityQuery()
            {
                Username = username
            });

            return new UsernameAvailabilityDto()
            {
                Username = username,
                IsAvailable = result
            };
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
            return StatusCode(501); //not yet implemented
        }

        private async Task<User> GetCurrentUser()
        {
            var result = await _mediator.Send(new ReadUserByIdQuery()
            {
                Id = Guid.Parse(User.FindFirst("UserId").Value)
            });

            return result;
        }
    }
}