using System;
using System.Collections.Generic;
using System.Text;

namespace GimanaIdApi.Infrastructure.EmailSender
{
    public interface IEmailSender
    {
        void SendEmail(Email email);
    }
}
