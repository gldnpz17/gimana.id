using DomainModel.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.Entities
{
    public class ArticlePart
    {
        public virtual int PartNumber { get; set; }
        public virtual string Title { get; set; }
        public virtual string Description { get; set; }
        public virtual IList<ArticleStep> Steps { get; set; } = new List<ArticleStep>();
    }
}
