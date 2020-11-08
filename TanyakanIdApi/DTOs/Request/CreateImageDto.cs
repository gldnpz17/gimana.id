using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TanyakanIdApi.DTOs.Request
{
    public class CreateImageDto
    {
        public string FileFormat { get; set; }
        public byte[] Data { get; set; }
    }
}
