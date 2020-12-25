using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Config
{
    public class ApplicationConfig
    {
        public string ApiBaseAddress { get; set; }
        public TypeOfEnvironment TypeOfEnvironment { get; set; }
    }
}
