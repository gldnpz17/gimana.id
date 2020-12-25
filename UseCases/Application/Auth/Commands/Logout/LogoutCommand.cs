using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Auth.Commands.Logout
{
    public class LogoutCommand : IRequest
    {
        public string AuthToken { get; set; }
    }
}
