using DomainModel.Services;
using DomainModel.Services.EmailSender;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace SimpleDomainServiceImplementation
{
    public class SmtpEmailSender : IEmailSender
    {
        private string _emailAddress;
        private string _emailPassword;

        public SmtpEmailSender(
            string emailAddress,
            string emailPassword
            )
        {
            _emailAddress = emailAddress;
            _emailPassword = emailPassword;
        }

        public void SendEmail(Email email)
        {
            var smtpClient = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(_emailAddress, _emailPassword)
            };

            using (var message =
                new MailMessage(_emailAddress, email.Recipient)
                {
                    Subject = email.Subject,
                    Body = email.Body,
                    IsBodyHtml = (email.EmailBodyType == EmailBodyType.HTML)
                })
            {
                smtpClient.Send(message);
            }
        }
    }
}
