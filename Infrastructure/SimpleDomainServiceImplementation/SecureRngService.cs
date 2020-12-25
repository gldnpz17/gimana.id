using DomainModel.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleDomainServiceImplementation
{
    public class SecureRngService : ISecureRngService
    {
        public byte[] GenerateRandomBytes()
        {
            throw new NotImplementedException();
        }
    }
}
