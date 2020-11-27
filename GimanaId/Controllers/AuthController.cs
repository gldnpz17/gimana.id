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
using GimanaIdApi.Entities.Entities;
using GimanaIdApi.Infrastructure.AlphanumericTokenGenerator;
using GimanaIdApi.Infrastructure.DataAccess;
using GimanaIdApi.Infrastructure.EmailSender;
using GimanaIdApi.Infrastructure.PasswordHasher;
using GimanaIdApi.Infrastructure.SecurePasswordSaltGenerator;

namespace GimanaIdApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;
        private readonly IPasswordHasher _passwordHasher;
        private readonly ISecurePasswordSaltGenerator _securePasswordSaltGenerator;
        private readonly IAlphanumericTokenGenerator _alphanumericTokenGenerator;
        private readonly IEmailSender _emailSender;
        private readonly ApiConfig _config;
        private readonly IMapper _mapper;
        private readonly UsersController _usersController;

        public AuthController(
            AppDbContext appDbContext,
            IPasswordHasher passwordHasher,
            ISecurePasswordSaltGenerator securePasswordSaltGenerator,
            IAlphanumericTokenGenerator alphanumericTokenGenerator,
            IEmailSender emailSender,
            ApiConfig config,
            IMapper mapper,
            UsersController usersController)
        {
            _appDbContext = appDbContext;
            _passwordHasher = passwordHasher;
            _securePasswordSaltGenerator = securePasswordSaltGenerator;
            _alphanumericTokenGenerator = alphanumericTokenGenerator;
            _emailSender = emailSender;
            _config = config;
            _mapper = mapper;
            _usersController = usersController;
        }
        
        /// <summary>
        /// Log in.
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost("login")]
        public async Task<ActionResult<AuthTokenDto>> Login([FromBody]LoginDto dto)
        {
            var user = await _appDbContext.Users.FirstOrDefaultAsync(i => i.Username == dto.Username);

            var passwordCredential = user.PasswordCredential;

            var passwordHash = _passwordHasher.HashPassword(dto.Password, passwordCredential.PasswordSalt);

            if (passwordHash == passwordCredential.HashedPassword)
            {
                var newToken = new AuthToken()
                {
                    User = user,
                    Token = _alphanumericTokenGenerator.GenerateAlphanumericToken(_config.PasswordResetTokenLength),
                    CreatedAt = DateTime.Now
                };

                user.AuthTokens.Add(newToken);
                await _appDbContext.SaveChangesAsync();

                var output = _mapper.Map<AuthTokenDto>(newToken);

                return Ok(output);
            }
            else
            {
                throw new Exception("Invalid login credentials");
            }
        }

        /// <summary>
        /// Log out.
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost("logout")]
        public async Task<ActionResult> Logout([FromBody]LogoutDto dto)
        {
            var authToken = await _appDbContext.AuthTokens.FirstOrDefaultAsync(i => i.Token == dto.Token);

            _appDbContext.AuthTokens.Remove(authToken);

            await _appDbContext.SaveChangesAsync();

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
            var passwordSalt = _securePasswordSaltGenerator.GenerateSecureRandomString();

            var newPassworCredential = new PasswordCredential()
            {
                HashedPassword = _passwordHasher.HashPassword(dto.Password, passwordSalt),
                PasswordSalt = passwordSalt
            };

            var newUser = new User()
            {
                Username = dto.Username,
                PasswordCredential = newPassworCredential,
                Email = new UserEmail()
                {
                    EmailAddress = dto.Email,
                    IsVerified = false
                },
                BanLiftedDate = DateTime.MinValue
            };

            await _appDbContext.Users.AddAsync(newUser);

            await _appDbContext.SaveChangesAsync();

            await _usersController.SendEmailVerificationMessage(newUser.Id.ToString());

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
            var user = await _appDbContext.Users.FirstOrDefaultAsync(i => i.Username == dto.Username);
            
            var newToken = _alphanumericTokenGenerator.GenerateAlphanumericToken(_config.PasswordResetTokenLength);

            var newResetToken = new PasswordResetToken()
            {
                Token = newToken,
                CreatedAt = DateTime.Now
            };

            user.PasswordCredential.PasswordResetToken = newResetToken;

            await _appDbContext.SaveChangesAsync();

            _emailSender.SendEmail(
                new Email()
                {
                    Recipient = user.Email.EmailAddress,
                    Subject = "Password Reset",
                    Body = $"your token: {newToken}",
                    EmailBodyType = EmailBodyType.Plaintext
                });

            return Ok();
        }

        /// <summary>
        /// Reset password.
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost("reset-password")]
        public async Task<ActionResult> ResetPassword([FromBody]ResetPasswordDto dto)
        {
            var user = await _appDbContext.Users.FirstOrDefaultAsync(i => i.Username == dto.Username);

            if (dto.Token == user.PasswordCredential.PasswordResetToken.Token)
            {
                var newSalt = _securePasswordSaltGenerator.GenerateSecureRandomString();

                user.PasswordCredential = new PasswordCredential()
                {
                    HashedPassword = _passwordHasher.HashPassword(dto.NewPassword, newSalt),
                    PasswordSalt = newSalt
                };

                await _appDbContext.SaveChangesAsync();

                return Ok();
            }
            else
            {
                throw new Exception("Invalid Token.");
            }
        }
    }
}