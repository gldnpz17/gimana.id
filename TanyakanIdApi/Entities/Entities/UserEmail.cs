using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GimanaIdApi.Entities.Entities
{
    public class UserEmail
    {
        public virtual Guid UserId { get; set; }
        public virtual User User { get; set; }
        public virtual string EmailAddress { get; set; }
        public virtual bool IsVerified { get; set; }
        public virtual EmailVerificationToken VerificationToken { get; set; }
    }
}
