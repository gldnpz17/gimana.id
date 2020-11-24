using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GimanaIdApi.Infrastructure.AlphanumericTokenGenerator
{
    public class AlphanumericTokenGenerator : IAlphanumericTokenGenerator
    {
        private readonly string CharacterSet = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

        public string GenerateAlphanumericToken(int length)
        {
            Random random = new Random();

            return new string(Enumerable.Repeat(CharacterSet, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
