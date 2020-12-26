using DomainModel.Services;
using System;
using System.Linq;

namespace SimpleDomainServiceImplementation
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
