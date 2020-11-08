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
using TanyakanIdApi.Common.Authentication;
using TanyakanIdApi.Common.Config;
using TanyakanIdApi.DTOs.Request;
using TanyakanIdApi.DTOs.Response;
using TanyakanIdApi.Entities.Entities;
using TanyakanIdApi.Infrastructure.AlphanumericTokenGenerator;
using TanyakanIdApi.Infrastructure.DataAccess;
using TanyakanIdApi.Infrastructure.EmailSender;
using TanyakanIdApi.Infrastructure.PasswordHasher;
using TanyakanIdApi.Infrastructure.SecurePasswordSaltGenerator;

namespace TanyakanIdApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;
        private readonly IEmailSender _emailSender;
        private readonly ISecurePasswordSaltGenerator _securePasswordSaltGenerator;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IAlphanumericTokenGenerator _alphanumericTokenGenerator;
        private readonly ApiConfig _config;

        public UsersController(
            AppDbContext appDbContext,
            IMapper mapper,
            IEmailSender emailSender,
            ISecurePasswordSaltGenerator securePasswordSaltGenerator,
            IPasswordHasher passwordHasher,
            IAlphanumericTokenGenerator alphanumericTokenGenerator,
            ApiConfig config)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
            _emailSender = emailSender;
            _securePasswordSaltGenerator = securePasswordSaltGenerator;
            _passwordHasher = passwordHasher;
            _alphanumericTokenGenerator = alphanumericTokenGenerator;
            _config = config;
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
            var user = await _appDbContext.Users.FirstOrDefaultAsync(i => i.Id == Guid.Parse(userId));

            var output = _mapper.Map<DetailedUserDto>(user);

            return Ok(output);
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
            var user = await _appDbContext.Users.FirstOrDefaultAsync(i => i.Id == Guid.Parse(userId));

            var newToken = _alphanumericTokenGenerator.GenerateAlphanumericToken(_config.EmailVerificationTokenLength);

            user.Email.VerificationToken =
                new EmailVerificationToken()
                {
                    Token = newToken,
                    CreatedAt = DateTime.Now
                };

            await _appDbContext.SaveChangesAsync();

            _emailSender.SendEmail(
                new Email()
                {
                    Recipient = user.Email.EmailAddress,
                    Body = $"<a href=\"{_config.ApiBaseAddress}/api/Users/{User.FindFirst("UserId").Value}/verify-email?token={newToken}\">click here to verify</a>",
                    EmailBodyType = EmailBodyType.HTML,
                    Subject = "Email Verification"
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
            var user = await _appDbContext.Users.FirstOrDefaultAsync(i => i.Id == Guid.Parse(userId));

            var tokenMatches = user.Email.VerificationToken.Token == token;
            var tokenHasNotExpired = DateTime.Now - user.Email.VerificationToken.CreatedAt <= _config.EmailVerificationTokenLifetime;

            if (tokenMatches && tokenHasNotExpired)
            {
                user.Email.IsVerified = true;

                await _appDbContext.SaveChangesAsync();

                return Ok();
            }
            else
            {
                throw new Exception("Invalid token.");
            }
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
            var user = await _appDbContext.Users.FirstOrDefaultAsync(i => i.Id == Guid.Parse(userId));

            user.BanLiftedDate = DateTime.Now + dto.BanDuration;

            await _appDbContext.SaveChangesAsync();

            return Ok();
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
            var user = await _appDbContext.Users.FirstOrDefaultAsync(i => i.Id == Guid.Parse(userId));

            user.Privileges.Add(new UserPrivilege() { PrivilegeName = dto.Privilege });

            await _appDbContext.SaveChangesAsync();

            return Ok();
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
            var user = await _appDbContext.Users.FirstOrDefaultAsync(i => i.Id == Guid.Parse(userId));

            var privilege = user.Privileges.FirstOrDefault(i => i.PrivilegeName == dto.Privilege);

            user.Privileges.Remove(privilege);

            await _appDbContext.SaveChangesAsync();

            return Ok();
        }

        /// <summary>
        /// Delete user.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpDelete("{userId}")]
        public async Task<ActionResult> DeleteUser([FromRoute]string userId) 
        {
            var user = await _appDbContext.Users.FirstOrDefaultAsync(i => i.Id == Guid.Parse(userId));

            _appDbContext.Users.Remove(user);

            await _appDbContext.SaveChangesAsync();

            return Ok();
        }

        /// <summary>
        /// Get user id.
        /// </summary>
        /// <returns></returns>
        [Authorize(Policy = AuthorizationPolicyConstants.AuthenticatedUsersOnlyPolicy)]
        [HttpGet("get-user-id")]
        public async Task<ActionResult<UserIdDto>> GetUserId()
        {
            var output = _mapper.Map<UserIdDto>(await GetCurrentUser());

            return output;
        }

        private async Task<User> GetCurrentUser()
        {
            return await _appDbContext.Users.FirstOrDefaultAsync(i => i.Id == Guid.Parse(User.FindFirst("UserId").Value));
        }
    }
}