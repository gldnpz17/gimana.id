using Application.Common.Config;
using DomainModel.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PostgresDatabase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Users.Commands.SendEmailVerificationMessage
{
    public class SendEmailVerificationMessageHandler : IRequestHandler<SendEmailVerificationMessageCommand>
    {
        private readonly AppDbContext _appDbContext;
        private readonly ApplicationConfig _applicationConfig;
        private readonly IEmailSender _emailSender;
        private readonly IAlphanumericTokenGenerator _alphanumericTokenGenerator;
        private readonly IDateTimeService _dateTimeService;

        public SendEmailVerificationMessageHandler(AppDbContext appDbContext, ApplicationConfig applicationConfig,
            IEmailSender emailSender, IAlphanumericTokenGenerator alphanumericTokenGenerator, IDateTimeService dateTimeService)
        {
            _appDbContext = appDbContext;
            _applicationConfig = applicationConfig;
            _emailSender = emailSender;
            _alphanumericTokenGenerator = alphanumericTokenGenerator;
            _dateTimeService = dateTimeService;
        }

        public async Task<Unit> Handle(SendEmailVerificationMessageCommand request, CancellationToken cancellationToken)
        {
            var user = await _appDbContext.Users.FirstOrDefaultAsync(i => i.Id == request.UserId);

            if (user == null)
            {
                throw new ApplicationException("User not found.");
            }

            user.Email.SendEmailVerificationMessage(_applicationConfig.ApiBaseAddress, _emailSender,
                _alphanumericTokenGenerator, _dateTimeService);

            await _appDbContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
