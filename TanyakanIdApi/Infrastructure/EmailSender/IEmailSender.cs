using System;
using System.Collections.Generic;
using System.Text;

namespace TanyakanIdApi.Infrastructure.EmailSender
{
    public interface IEmailSender
    {
        void SendEmail(Email email);
    }
}
