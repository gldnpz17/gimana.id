using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GimanaIdApi.Entities.Entities
{
    public class PasswordResetToken
    {
        public virtual string Token { get; set; }
        public virtual DateTime CreatedAt { get; set; }
    }
}
