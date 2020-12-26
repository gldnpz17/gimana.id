using DomainModel.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PostgresDatabase;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Auth.Commands.SendPasswordResetMessage
{
    public class SendPasswordResetMessageHandler : IRequestHandler<SendPasswordResetMessageCommand>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IEmailSender _emailSender;
        private readonly IAlphanumericTokenGenerator _alphanumericTokenGenerator;
        private readonly IDateTimeService _dateTimeService;

        public SendPasswordResetMessageHandler(AppDbContext appDbContext, IEmailSender emailSender, 
            IAlphanumericTokenGenerator alphanumericTokenGenerator, IDateTimeService dateTimeService)
        {
            _appDbContext = appDbContext;
            _emailSender = emailSender;
            _alphanumericTokenGenerator = alphanumericTokenGenerator;
            _dateTimeService = dateTimeService;
        }

        public async Task<Unit> Handle(SendPasswordResetMessageCommand request, CancellationToken cancellationToken)
        {
            var email = await _appDbContext.Emails.FirstOrDefaultAsync(i => i.EmailAddress == request.EmailAddress);

            if (email == null)
            {
                throw new ApplicationException("Email not found.");
            }

            email.SendPasswordResetMessage(_emailSender, _alphanumericTokenGenerator, _dateTimeService);

            await _appDbContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
