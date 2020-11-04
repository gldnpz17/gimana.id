using AnemicDomainModel.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnemicDomainModel.Entities
{
    public class Article
    {
        public virtual Guid Id { get; set; }
        public virtual string Title { get; set; }
        public virtual string Description { get; set; }
        public virtual Image HeroImage { get; set; }
        public virtual IList<ArticlePart> Parts { get; set; } = new List<ArticlePart>();
        public virtual IList<User> Contributors { get; set; } = new List<User>();
        public virtual IList<ArticleMemento> History { get; set; } = new List<ArticleMemento>();
    }
}
