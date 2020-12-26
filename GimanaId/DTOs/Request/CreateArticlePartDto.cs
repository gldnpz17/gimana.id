using System.Collections.Generic;

namespace GimanaIdApi.DTOs.Request
{
    public class CreateArticlePartDto
    {
        public virtual string Title { get; set; }
        public virtual string Description { get; set; }
        public virtual List<CreateArticleStepDto> Steps { get; set; }
    }
}
