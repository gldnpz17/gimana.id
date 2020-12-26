using System;

namespace DomainModel.ValueObjects
{
    public class EmailVerificationToken
    {
        public virtual string Token { get; set; }
        public virtual DateTime CreatedAt { get; set; }
    }
}
