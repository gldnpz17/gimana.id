using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TanyakanIdApi.DTOs.Response
{
    public class ArticleHistoryDto
    {
        public virtual int Version { get; set; }
        public virtual DateTime TimeStamp { get; set; }
        public virtual string Title { get; set; }
        public virtual string Description { get; set; }
        public virtual ImageDto HeroImage { get; set; }
        public virtual IList<ArticlePartDto> Parts { get; set; }
        public virtual IList<SimpleUserDto> Contributors { get; set; }
        public virtual IList<ArticleIssueDto> Issues { get; set; } = new List<ArticleIssueDto>();
        public virtual IList<ArticleRatingDto> Ratings { get; set; } = new List<ArticleRatingDto>();
    }
}
