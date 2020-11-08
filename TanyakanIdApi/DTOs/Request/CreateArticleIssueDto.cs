using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TanyakanIdApi.DTOs.Request
{
    public class CreateArticleIssueDto
    {
        public virtual string Title { get; set; }
        public virtual string Content { get; set; }
    }
}
