using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Users.Commands.SendEmailVerificationMessage
{
    public class SendEmailVerificationMessageCommand : IRequest
    {
        public Guid UserId { get; set; }
    }
}
