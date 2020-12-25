using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModel.Common;
using DomainModel.Services;
using DomainModel.ValueObjects;

namespace DomainModel.Entities
{
    public class User
    {
        public virtual Guid Id { get; set; }
        public virtual string Username { get; set; }
        public virtual IList<string> Privileges { get; set; } = new List<string>();
        public virtual Image ProfilePicture { get; set; }
        public virtual Email Email { get; set; }
        public virtual PasswordCredential PasswordCredential { get; set; }
        public virtual IList<AuthToken> AuthTokens { get; set; } = new List<AuthToken>();
        public virtual IList<Article> Articles { get; set; } = new List<Article>();

        public AuthToken Login(string password, string IpAddress, string userAgent, IDateTimeService dateTimeService,
            IPasswordHashingService passwordHashingService, IAlphanumericTokenGenerator alphanumericTokenGenerator)
        {
            if (PasswordCredential.Verify(password, passwordHashingService) == true)
            {
                //generate new auth token
                var token = new AuthToken()
                {
                    Token = alphanumericTokenGenerator.GenerateAlphanumericToken(64),
                    CreatedAt = dateTimeService.GetCurrentDateTime(),
                    IPAddress = IpAddress,
                    UserAgent = userAgent
                };

                AuthTokens.Add(token);

                return token;
            }
            else
            {
                throw new DomainModelException("Incorrect username or password.");
            }
        }
    }
}
