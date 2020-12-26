using System;

namespace GimanaIdApi.DTOs.Response
{
    public class SimpleArticleDto
    {
        public virtual Guid Id { get; set; }
        public virtual string Title { get; set; }
        public virtual string Description { get; set; }
        public virtual DateTime DateCreated { get; set; }
        public virtual ImageDto HeroImage { get; set; }
    }
}
