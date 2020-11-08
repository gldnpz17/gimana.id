using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TanyakanIdApi.DTOs.Request
{
    public class CreateArticleDto
    {
        public virtual string Title { get; set; }
        public virtual string Description { get; set; }
        public virtual CreateImageDto HeroImage { get; set; }
        public virtual List<CreateArticlePartDto> Parts { get; set; }
    }
}
