using System;
using System.Collections.Generic;
using DomainModel.Common;

namespace DomainModel.ValueObjects
{
    public class Image : ValueObject
    {
        public Guid Id { get; set; }
        public virtual string FileFormat { get; set; }
        public virtual string Base64EncodedData { get; set; }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return FileFormat;
            yield return Base64EncodedData;
        }
    }
}
