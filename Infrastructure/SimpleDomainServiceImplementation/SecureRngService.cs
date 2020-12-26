using DomainModel.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SimpleDomainServiceImplementation
{
    public class SecureRngService : ISecureRngService
    {
        public byte[] GenerateRandomBytes(int length)
        {
            var cryptoRng = new RNGCryptoServiceProvider();

            var randomBytes = new byte[length];

            cryptoRng.GetBytes(randomBytes);

            return randomBytes;
        }
    }
}
