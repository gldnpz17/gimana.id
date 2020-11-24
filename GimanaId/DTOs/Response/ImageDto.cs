using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GimanaIdApi.DTOs.Response
{
    public class ImageDto
    {
        public string FileFormat { get; set; }
        public string Base64EncodedData { get; set; }
    }
}
