using DomainModel.Common;
using System.Collections.Generic;

namespace DomainModel.ValueObjects
{
    public class UserPrivilege : ValueObject
    {
        public virtual string PrivilegeName { get; set; }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return PrivilegeName;
        }
    }
}
