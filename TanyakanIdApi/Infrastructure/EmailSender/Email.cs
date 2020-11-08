using System;
using System.Collections.Generic;
using System.Text;

namespace TanyakanIdApi.Infrastructure.EmailSender
{
    public class Email
    {
        public string Recipient { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public EmailBodyType EmailBodyType { get; set; }
    }
}
