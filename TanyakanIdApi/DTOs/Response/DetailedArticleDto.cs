using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TanyakanIdApi.DTOs.Response
{
    public class DetailedArticleDto
    {
        public virtual Guid Id { get; set; }
        public virtual string Title { get; set; }
        public virtual string Description { get; set; }
        public virtual ImageDto HeroImage { get; set; }
        public virtual List<ArticlePartDto> Parts { get; set; }
        public virtual List<SimpleUserDto> Contributors { get; set; }
    }
}
