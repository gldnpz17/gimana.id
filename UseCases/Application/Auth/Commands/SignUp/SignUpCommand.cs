using MediatR;

namespace Application.Auth.Commands.SignUp
{
    public class SignUpCommand : IRequest
    {
        public string Username { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }
    }
}
