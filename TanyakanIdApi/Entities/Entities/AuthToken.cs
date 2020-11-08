﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TanyakanIdApi.Entities.Entities
{
    public class AuthToken
    {
        public virtual User User { get; set; }
        public virtual string Token { get; set; }
        public virtual DateTime CreatedAt { get; set; }
    }
}
