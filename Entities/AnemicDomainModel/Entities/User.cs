using AnemicDomainModel.Enums;
using AnemicDomainModel.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnemicDomainModel.Entities
{
    public class User
    {
        public virtual Guid Id { get; set; }
        public virtual string Username { get; set; }
        public virtual IList<UserPrivilegeTypes> Privileges { get; set; } = new List<UserPrivilegeTypes>();
        public virtual Image ProfilePicture { get; set; }
        public virtual UserEmail Email { get; set; }
        public virtual bool IsBanned { get; set; }
        public virtual DateTime BanLiftedDate { get; set; }
    }
}
