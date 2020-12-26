using DomainModel.Entities;
using MediatR;

namespace Application.Auth.Commands.VerifyAuthToken
{
    public class VerifyAuthTokenCommand : IRequest<AuthToken>
    {
        public string Token { get; set; }
        public string IpAddress { get; set; }
        public string UserAgent { get; set; }
    }
}
