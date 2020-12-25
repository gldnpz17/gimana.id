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

namespace Application.Users.Queries.ReadUserById
{
    public class ReadUserByIdHandler : IRequestHandler<ReadUserByIdQuery, User>
    {
        private readonly AppDbContext _appDbContext;

        public ReadUserByIdHandler(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<User> Handle(ReadUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _appDbContext.Users.FirstOrDefaultAsync(i => i.Id == request.Id);

            if (user == null)
            {
                throw new ApplicationException("User not found.");
            }

            return user;
        }
    }
}
