using Application.Common.Config;
using DomainModel.Entities;
using DomainModel.Services;
using MediatR;
using PostgresDatabase;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Auth.Commands.SignUp
{
    public class SignUpHandler : IRequestHandler<SignUpCommand>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IPasswordHashingService _passwordHashingService;
        private readonly ISecureRngService _secureRngService;
        private readonly ApplicationConfig _applicationConfig;
        private readonly IEmailSender _emailSender;
        private readonly IAlphanumericTokenGenerator _alphanumericTokenGenerator;
        private readonly IDateTimeService _dateTimeService;

        public SignUpHandler(AppDbContext appDbContext, IPasswordHashingService passwordHashingService,
            ISecureRngService secureRngService, ApplicationConfig applicationConfig, IEmailSender emailSender,
            IAlphanumericTokenGenerator alphanumericTokenGenerator, IDateTimeService dateTimeService)
        {
            _appDbContext = appDbContext;
            _passwordHashingService = passwordHashingService;
            _secureRngService = secureRngService;
            _applicationConfig = applicationConfig;
            _emailSender = emailSender;
            _alphanumericTokenGenerator = alphanumericTokenGenerator;
            _dateTimeService = dateTimeService;
        }

        public async Task<Unit> Handle(SignUpCommand request, CancellationToken cancellationToken)
        {
            var newUser = new User()
            {
                Id = Guid.NewGuid(),
                Username = request.Username,
                Email = new Email() 
                { 
                    EmailAddress = request.EmailAddress, 
                    IsVerified = false
                },
                PasswordCredential = new PasswordCredential(request.Password, _passwordHashingService, _secureRngService)
            };

            await _appDbContext.Users.AddAsync(newUser);

            await _appDbContext.SaveChangesAsync();

            newUser.Email.SendEmailVerificationMessage(_applicationConfig.ApiBaseAddress, _emailSender, 
                _alphanumericTokenGenerator, _dateTimeService);

            await _appDbContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
