using GimanaIdApi.Entities.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GimanaIdApi.DTOs.Request
{
    public class CreateArticleStepDto
    {
        public virtual string Title { get; set; }
        public virtual string Description { get; set; }
        public virtual CreateImageDto Image { get; set; }
    }
}
