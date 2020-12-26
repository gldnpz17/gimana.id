using DomainModel.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.ValueObjects
{
    public class UserPrivilege : ValueObject
    {
        public string PrivilegeName { get; set; }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return PrivilegeName;
        }
    }
}
