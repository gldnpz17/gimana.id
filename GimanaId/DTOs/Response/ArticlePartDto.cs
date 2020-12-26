using System.Collections.Generic;

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
