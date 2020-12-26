using MediatR;
using System;

namespace Application.Users.Commands.SendEmailVerificationMessage
{
    public class SendEmailVerificationMessageCommand : IRequest
    {
        public Guid UserId { get; set; }
    }
}
