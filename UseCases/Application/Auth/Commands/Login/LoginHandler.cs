using DomainModel.Entities;
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

namespace Application.Auth.Commands.Login
{
    public class LoginHandler : IRequestHandler<LoginCommand, AuthToken>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IPasswordHashingService _passwordHashingService;
        private readonly IDateTimeService _dateTimeService;
        private readonly IAlphanumericTokenGenerator _alphanumericTokenGenerator;

        public LoginHandler(AppDbContext appDbContext, IPasswordHashingService passwordHashingService,
            IDateTimeService dateTimeService, IAlphanumericTokenGenerator alphanumericTokenGenerator)
        {
            _appDbContext = appDbContext;
            _passwordHashingService = passwordHashingService;
            _dateTimeService = dateTimeService;
            _alphanumericTokenGenerator = alphanumericTokenGenerator;
        }

        public async Task<AuthToken> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _appDbContext.Users.FirstOrDefaultAsync(i => i.Username == request.Username);

            if (user == null)
            {
                throw new ApplicationException("Incorrect username or password");
            }

            var token = user.Login(request.Password, request.IpAddress, request.UserAgent, _dateTimeService,
                _passwordHashingService, _alphanumericTokenGenerator);

            await _appDbContext.SaveChangesAsync();

            return token;
        }
    }
}
