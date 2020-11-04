using Domain.Enums;
using Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class User
    {
        public virtual Guid Id { get; set; }
        public virtual string Username { get; set; }
        public virtual IList<UserPrivilegeTypes> Privileges { get; private set; } = new List<UserPrivilegeTypes>();
        public virtual Image ProfilePicture { get; set; }
        public virtual
    }
}
