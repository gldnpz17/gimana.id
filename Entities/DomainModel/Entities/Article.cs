using System;
using System.Collections.Generic;
using DomainModel.ValueObjects;

namespace DomainModel.Entities
{
    public class Article
    {
        public virtual Guid Id { get; set; }
        public virtual string Title { get; set; }
        public virtual string Description { get; set; }
        public virtual Image HeroImage { get; set; }
        public virtual DateTime DateCreated { get; set; }
        public virtual IList<ArticlePart> Parts { get; set; } = new List<ArticlePart>();
        public virtual IList<User> Users { get; set; } = new List<User>();
    }
}
