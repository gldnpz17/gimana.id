using DomainModel.Entities;
using DomainModel.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PostgresDatabase;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Auth.Commands.VerifyAuthToken
{
    public class VerifyAuthTokenHandler : IRequestHandler<VerifyAuthTokenCommand, AuthToken>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IDateTimeService _dateTimeService;

        public VerifyAuthTokenHandler(AppDbContext appDbContext, IDateTimeService dateTimeService)
        {
            _appDbContext = appDbContext;
            _dateTimeService = dateTimeService;
        }

        public async Task<AuthToken> Handle(VerifyAuthTokenCommand request, CancellationToken cancellationToken)
        {
            var token = await _appDbContext.AuthTokens.FirstOrDefaultAsync(i => i.Token == request.Token);

            if (token == null)
            {
                throw new ApplicationException("Token not found.");
            }

            if (_dateTimeService.GetCurrentDateTime() > token.ExpireDate)
            {
                _appDbContext.AuthTokens.Remove(token);

                await _appDbContext.SaveChangesAsync();
                throw new ApplicationException("Token Expired.");
            }

            if (token.IPAddress != request.IpAddress)
            {
                throw new ApplicationException("IP Address doesn't match the one on the token.");
            }

            if (token.UserAgent != request.UserAgent)
            {
                throw new ApplicationException("User Agent doesn't match the one on the token.");
            }

            token.Extend(_dateTimeService);

            await _appDbContext.SaveChangesAsync();

            return token;
        }
    }
}
