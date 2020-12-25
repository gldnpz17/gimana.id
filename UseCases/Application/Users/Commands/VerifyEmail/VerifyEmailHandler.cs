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

namespace Application.Users.Commands.VerifyEmail
{
    public class VerifyEmailHandler : IRequestHandler<VerifyEmailCommand>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IDateTimeService _dateTimeService;

        public VerifyEmailHandler(AppDbContext appDbContext, IDateTimeService dateTimeService)
        {
            _appDbContext = appDbContext;
            _dateTimeService = dateTimeService;
        }

        public async Task<Unit> Handle(VerifyEmailCommand request, CancellationToken cancellationToken)
        {
            var email = await _appDbContext.Emails.FirstOrDefaultAsync(i => i.VerificationToken.Token == request.VerificationToken);

            if (email == null)
            {
                throw new ApplicationException("Invalid token.");
            }

            email.Verify(request.VerificationToken, _dateTimeService);

            await _appDbContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
