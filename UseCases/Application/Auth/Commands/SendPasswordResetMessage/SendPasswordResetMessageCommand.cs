using MediatR;

namespace Application.Auth.Commands.SendPasswordResetMessage
{
    public class SendPasswordResetMessageCommand : IRequest
    {
        public string EmailAddress { get; set; }
    }
}
