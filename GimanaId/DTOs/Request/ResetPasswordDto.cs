using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GimanaIdApi.DTOs.Request
{
    public class ResetPasswordDto
    {
        public string Username { get; set; }
        public string Token { get; set; }
        public string NewPassword { get; set; }
    }
}
