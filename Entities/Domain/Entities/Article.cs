using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Article
    {
        public virtual Guid Id { get; set; }
        public virtual string Title { get; set; }

    }
}
