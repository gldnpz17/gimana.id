using MediatR;
using Microsoft.EntityFrameworkCore;
using PostgresDatabase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Auth.Commands.Logout
{
    public class LogoutHandler : IRequestHandler<LogoutCommand>
    {
        private readonly AppDbContext _appDbContext;

        public LogoutHandler(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<Unit> Handle(LogoutCommand request, CancellationToken cancellationToken)
        {
            var token = await _appDbContext.AuthTokens.FirstOrDefaultAsync(i => i.Token == request.AuthToken);

            if (token == null)
            {
                throw new ApplicationException("Invalid token.");
            }

            _appDbContext.Remove(token);

            await _appDbContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
