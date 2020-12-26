using System;

namespace DomainModel.ValueObjects
{
    public class PasswordResetToken
    {
        public virtual string Token { get; set; }
        public virtual DateTime CreatedAt { get; set; }
    }
}
