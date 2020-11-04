using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnemicDomainModel.Entities
{
    public class UserEmail
    {
        public virtual string EmailAddress { get; set; }
        public virtual bool IsVerified { get; set; }
    }
}
