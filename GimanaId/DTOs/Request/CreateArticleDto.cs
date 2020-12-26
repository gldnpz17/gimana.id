using System.Collections.Generic;

namespace GimanaIdApi.DTOs.Request
{
    public class CreateArticleDto
    {
        public virtual string Title { get; set; }
        public virtual string Description { get; set; }
        public virtual CreateImageDto HeroImage { get; set; }
        public virtual List<CreateArticlePartDto> Parts { get; set; }
    }
}
