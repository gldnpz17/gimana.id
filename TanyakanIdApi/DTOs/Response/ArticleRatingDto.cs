using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TanyakanIdApi.DTOs.Response
{
    public class ArticleRatingDto
    {
        public virtual Guid Id { get; set; }
        public virtual SimpleUserDto Submitter { get; set; }
        public virtual string Title { get; set; }
        public virtual string Content { get; set; }
        public virtual int Rating { get; set; }
    }
}
