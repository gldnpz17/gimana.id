using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GimanaIdApi.DTOs.Response
{
    public class ArticlePartDto
    {
        public virtual int PartNumber { get; set; }
        public virtual string Title { get; set; }
        public virtual string Description { get; set; }
        public virtual List<ArticleStepDto> Steps { get; set; }
    }
}
