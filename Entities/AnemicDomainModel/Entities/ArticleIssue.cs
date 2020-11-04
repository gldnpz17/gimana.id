using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnemicDomainModel.Entities
{
    public class ArticleIssue
    {
        public virtual Guid Id { get; set; }
        public virtual Article Article { get; set; }
        public virtual User Submitter { get; set; }
        public virtual string Title { get; set; }
        public virtual string Content { get; set; }
        public virtual bool Resolved { get; set; }
    }
}
