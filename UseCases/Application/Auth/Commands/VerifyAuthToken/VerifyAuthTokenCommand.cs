using DomainModel.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Auth.Commands.VerifyAuthToken
{
    public class VerifyAuthTokenCommand : IRequest<AuthToken>
    {
        public string Token { get; set; }
        public string IpAddress { get; set; }
        public string UserAgent { get; set; }
    }
}
