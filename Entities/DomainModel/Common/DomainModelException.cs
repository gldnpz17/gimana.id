using System;

namespace DomainModel.Common
{
    public class DomainModelException : Exception
    {
        public DomainModelException(string message) : base(message)
        {

        }
    }
}
