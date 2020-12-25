using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Users.Commands.VerifyEmail
{
    public class VerifyEmailCommand : IRequest
    {
        public string VerificationToken { get; set; }
    }
}
