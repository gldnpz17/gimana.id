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

namespace Application.Auth.Commands.ResetPassword
{
    public class ResetPasswordHandler : IRequestHandler<ResetPasswordCommand>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IDateTimeService _dateTimeService;
        private readonly IPasswordHashingService _passwordHashingService;
        private readonly ISecureRngService _secureRngService;

        public ResetPasswordHandler(AppDbContext appDbContext, IDateTimeService dateTimeService,
            IPasswordHashingService passwordHashingService, ISecureRngService secureRngService)
        {
            _appDbContext = appDbContext;
            _dateTimeService = dateTimeService;
            _passwordHashingService = passwordHashingService;
            _secureRngService = secureRngService;
        }

        public async Task<Unit> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            var credential = await _appDbContext.PasswordCredentials.FirstOrDefaultAsync(i => i.PasswordResetToken.Token == request.PasswordResetToken);

            if (credential == null)
            {
                throw new ApplicationException("Invalid token.");
            }

            credential.Reset(request.NewPassword, _dateTimeService, _passwordHashingService, _secureRngService);

            await _appDbContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
