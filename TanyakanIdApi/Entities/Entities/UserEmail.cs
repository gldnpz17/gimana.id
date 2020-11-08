﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TanyakanIdApi.Entities.Entities
{
    public class UserEmail
    {
        public virtual string EmailAddress { get; set; }
        public virtual bool IsVerified { get; set; }
        public virtual EmailVerificationToken VerificationToken { get; set; }
    }
}
