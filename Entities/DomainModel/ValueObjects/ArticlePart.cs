using System.Collections.Generic;

namespace DomainModel.ValueObjects
{
    public class ArticlePart
    {
        public virtual int PartNumber { get; set; }
        public virtual string Title { get; set; }
        public virtual string Description { get; set; }
        public virtual IList<ArticleStep> Steps { get; set; } = new List<ArticleStep>();
    }
}
