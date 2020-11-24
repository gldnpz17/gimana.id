using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GimanaIdApi.Entities.Common
{
    public abstract class ValueObject
    {
        protected abstract IEnumerable<object> GetAtomicValues();

        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != GetType())
            {
                return false;
            }

            var thisValues = GetAtomicValues().GetEnumerator();
            var otherValues = ((ValueObject)obj).GetAtomicValues().GetEnumerator();

            //compare every value
            while (thisValues.MoveNext() && otherValues.MoveNext())
            {
                if (thisValues.Current == null && thisValues.Current == null)
                {
                    return true;
                }

                if (thisValues.Current != otherValues.Current)
                {
                    return false;
                }
            }

            //check if both are already out of values to compare
            if (thisValues.MoveNext() == false && otherValues.MoveNext() == false)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
