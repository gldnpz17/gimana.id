using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GimanaId.DTOs.Response
{
    public class UsernameAvailabilityDto
    {
        public string Username { get; set; }
        public bool IsAvailable { get; set; }
    }
}
