using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GimanaIdApi.Entities.Entities
{
    public class ArticleStep
    {
        public virtual int StepNumber { get; set; }
        public virtual string Title { get; set; }
        public virtual string Description { get; set; }
    }
}
