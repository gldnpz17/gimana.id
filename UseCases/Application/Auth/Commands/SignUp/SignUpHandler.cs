using DomainModel.Entities;
using DomainModel.Services;
using MediatR;
using PostgresDatabase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Auth.Commands.SignUp
{
    public class SignUpHandler : IRequestHandler<SignUpCommand>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IPasswordHashingService _passwordHashingService;
        private readonly ISecureRngService _secureRngService;

        public SignUpHandler(AppDbContext appDbContext, IPasswordHashingService passwordHashingService,
            ISecureRngService secureRngService)
        {
            _appDbContext = appDbContext;
            _passwordHashingService = passwordHashingService;
            _secureRngService = secureRngService;
        }

        public async Task<Unit> Handle(SignUpCommand request, CancellationToken cancellationToken)
        {
            var newUser = new User()
            {
                Id = Guid.NewGuid(),
                Username = request.Username,
                PasswordCredential = new PasswordCredential(request.Password, _passwordHashingService, _secureRngService)
            };

            await _appDbContext.Users.AddAsync(newUser);

            await _appDbContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
