﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TanyakanIdApi.Entities.ValueObjects;

namespace TanyakanIdApi.Entities.Entities
{
    public class Article
    {
        public virtual Guid Id { get; set; }
        public virtual string Title { get; set; }
        public virtual string Description { get; set; }
        public virtual Image HeroImage { get; set; }
        public virtual IList<ArticlePart> Parts { get; set; } = new List<ArticlePart>();
        public virtual IList<User> Contributors { get; set; } = new List<User>();
        public virtual IList<ArticleHistory> History { get; set; } = new List<ArticleHistory>();
        public virtual IList<ArticleIssue> Issues { get; set; } = new List<ArticleIssue>();
        public virtual IList<ArticleRating> Ratings { get; set; } = new List<ArticleRating>();
    }
}
