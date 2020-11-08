using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TanyakanIdApi.DTOs.Response
{
    public class SimpleUserDto
    {
        public virtual Guid Id { get; set; }
        public virtual string Username { get; set; }
        public virtual ImageDto ProfilePicture { get; set; }
    }
}
