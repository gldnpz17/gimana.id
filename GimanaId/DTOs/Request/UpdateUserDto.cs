using GimanaIdApi.DTOs.Request;
using GimanaIdApi.DTOs.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GimanaId.DTOs.Request
{
    public class UpdateUserDto
    {
        public virtual CreateImageDto ProfilePicture { get; set; }
    }
}
