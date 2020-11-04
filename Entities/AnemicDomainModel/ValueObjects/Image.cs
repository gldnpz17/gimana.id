using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnemicDomainModel.ValueObjects
{
    public class Image
    {
        public string FileFormat { get; set; }
        public byte[] Data { get; set; }
    }
}
