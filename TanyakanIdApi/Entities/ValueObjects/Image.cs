using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TanyakanIdApi.Entities.Common;

namespace TanyakanIdApi.Entities.ValueObjects
{
    public class Image : ValueObject
    {
        public string FileFormat { get; set; }
        public string Base64EncodedData { get; set; }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return FileFormat;
            yield return Base64EncodedData;
        }
    }
}
