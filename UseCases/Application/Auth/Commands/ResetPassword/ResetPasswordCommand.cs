using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Auth.Commands.ResetPassword
{
    public class ResetPasswordCommand : IRequest
    {
        public string PasswordResetToken { get; set; }
        public string NewPassword { get; set; }
    }
}
