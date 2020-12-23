using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.Entities
{
    public class ArticleRating
    {
        public virtual Guid Id { get; set; }
        public virtual Article Article { get; set; }
        public virtual User Submitter { get; set; }
        public virtual string Title { get; set; }
        public virtual string Content { get; set; }
        public virtual int Rating { get; set; }
    }
}
