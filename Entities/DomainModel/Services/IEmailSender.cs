using DomainModel.Services.EmailSender;

namespace DomainModel.Services
{
    public interface IEmailSender
    {
        void SendEmail(Email email);
    }
}
