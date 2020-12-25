using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.Entities
{
    public class AuthToken
    {
        public User User { get; set; }
        public virtual string Token { get; set; }
        public virtual DateTime CreatedAt { get; set; }
        public virtual string UserAgent { get; set; }
        public virtual string IPAddress { get; set; }

        public virtual void Extend()
        {
            CreatedAt.AddDays(7);
        }
    }
}
