using DomainModel.Services;
using System.Security.Cryptography;

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
