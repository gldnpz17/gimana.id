using DomainModel.Services;
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
        public virtual DateTime ExpireDate { get; set; }
        public virtual string UserAgent { get; set; }
        public virtual string IPAddress { get; set; }
        public virtual bool IsRemembered { get; set; }

        public virtual void Extend(IDateTimeService _dateTimeService)
        {
            if (IsRemembered)
            {
                ExpireDate = _dateTimeService.GetCurrentDateTime().AddDays(30);
            }
        }
    }
}
