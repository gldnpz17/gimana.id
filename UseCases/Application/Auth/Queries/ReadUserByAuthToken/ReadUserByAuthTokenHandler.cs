using DomainModel.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PostgresDatabase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Auth.Queries.ReadUserByAuthToken
{
    public class ReadUserByAuthTokenHandler : IRequestHandler<ReadUserByAuthTokenQuery, User>
    {
        private readonly AppDbContext _appDbContext;

        public ReadUserByAuthTokenHandler(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<User> Handle(ReadUserByAuthTokenQuery request, CancellationToken cancellationToken)
        {
            var token = await _appDbContext.AuthTokens.FirstOrDefaultAsync(i => i.Token == request.AuthToken);

            return token.User;
        }
    }
}
