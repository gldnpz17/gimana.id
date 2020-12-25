using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Auth.Commands.SendPasswordResetMessage
{
    public class SendPasswordResetMessageCommand : IRequest
    {
        public string EmailAddress { get; set; }
    }
}
