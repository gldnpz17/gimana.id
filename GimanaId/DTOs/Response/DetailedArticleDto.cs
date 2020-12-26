using System;
using System.Collections.Generic;

namespace GimanaIdApi.DTOs.Response
{
    public class DetailedArticleDto
    {
        public virtual Guid Id { get; set; }
        public virtual string Title { get; set; }
        public virtual string Description { get; set; }
        public virtual DateTime DateCreated { get; set; }
        public virtual ImageDto HeroImage { get; set; }
        public virtual List<ArticlePartDto> Parts { get; set; }
        public virtual List<SimpleUserDto> Contributors { get; set; }
    }
}
