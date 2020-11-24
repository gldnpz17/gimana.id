using GimanaIdApi.Infrastructure.EmailSender;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GimanaId.Infrastructure.Mocks.MockEmailSender
{
    public class MockEmailSender : IEmailSender
    {
        public void SendEmail(Email email)
        {
            Debug.WriteLine($"email sent! subject={email.Recipient}; Subject={email.Subject}; BodyType={email.EmailBodyType}; Body={email.Body}");
        }
    }
}
