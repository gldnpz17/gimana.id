using DomainModel.Entities;
using MediatR;

namespace Application.Auth.Commands.Login
{
    public class LoginCommand : IRequest<AuthToken>
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string IpAddress { get; set; }
        public string UserAgent { get; set; }
        public bool IsRemembered { get; set; }
    }
}
