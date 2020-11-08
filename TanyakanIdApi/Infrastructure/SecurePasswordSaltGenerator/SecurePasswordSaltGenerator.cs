using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace TanyakanIdApi.Infrastructure.SecurePasswordSaltGenerator
{
    public class SecurePasswordSaltGenerator : ISecurePasswordSaltGenerator
    {
        public string GenerateSecureRandomString()
        {
            var cryptoRng = new RNGCryptoServiceProvider();

            var salt = new byte[64];

            cryptoRng.GetBytes(salt);

            return Convert.ToBase64String(salt);
        }
    }
}
