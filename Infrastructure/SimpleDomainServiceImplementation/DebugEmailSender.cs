using DomainModel.Services;
using DomainModel.Services.EmailSender;
using System.Diagnostics;

namespace SimpleDomainServiceImplementation
{
    public class DebugEmailSender : IEmailSender
    {
        public void SendEmail(Email email)
        {
            Debug.WriteLine($"Email sent! Recipient:{email.Recipient}; Subject:{email.Subject}; BodyType:{email.EmailBodyType}; Body:{email.Body}");
        }
    }
}
