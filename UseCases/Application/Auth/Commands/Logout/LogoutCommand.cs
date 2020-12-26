using MediatR;

namespace Application.Auth.Commands.Logout
{
    public class LogoutCommand : IRequest
    {
        public string AuthToken { get; set; }
    }
}
