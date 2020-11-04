using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class AuthToken
    {
        public virtual User Owner { get; set; }
        public virtual string Token { get; set; }
    }
}
