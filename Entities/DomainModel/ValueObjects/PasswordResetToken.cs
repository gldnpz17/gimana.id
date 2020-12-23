using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.ValueObjects
{
    public class PasswordResetToken
    {
        public virtual string Token { get; set; }
        public virtual DateTime CreatedAt { get; set; }
    }
}
