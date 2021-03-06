﻿using DomainModel.Common;
using DomainModel.Services;
using DomainModel.ValueObjects;
using System;

namespace DomainModel.Entities
{
    public class Email
    {
        public virtual Guid UserId { get; set; }
        public virtual User User { get; set; }
        public virtual string EmailAddress { get; set; }
        public virtual bool IsVerified { get; set; }
        public virtual EmailVerificationToken VerificationToken { get; set; }

        public void Verify(string token, IDateTimeService dateTimeService)
        {
            if (token == VerificationToken.Token)
            {
                if (dateTimeService.GetCurrentDateTime() < VerificationToken.CreatedAt.AddHours(6))
                {
                    IsVerified = true;
                }
                else
                {
                    throw new DomainModelException("Token expired.");
                }
            }
            else
            {
                throw new DomainModelException("Invalid token.");
            }
        }

        public void SendPasswordResetMessage(IEmailSender emailSender, IAlphanumericTokenGenerator alphanumericTokenGenerator,
            IDateTimeService dateTimeService)
        {
            var resetToken = new PasswordResetToken()
            {
                Token = alphanumericTokenGenerator.GenerateAlphanumericToken(64),
                CreatedAt = dateTimeService.GetCurrentDateTime()
            };

            var email = new Services.EmailSender.Email()
            {
                Recipient = EmailAddress,
                Subject = "Email Verification",
                EmailBodyType = Services.EmailSender.EmailBodyType.Plaintext,
                Body = $"your reset token : {resetToken.Token}"
            };

            User.PasswordCredential.PasswordResetToken = resetToken;
        }

        public void SendEmailVerificationMessage(string apiBaseAddress, IEmailSender emailSender, 
            IAlphanumericTokenGenerator alphanumericTokenGenerator, IDateTimeService dateTimeService)
        {
            var verificationToken = new EmailVerificationToken()
            {
                Token = alphanumericTokenGenerator.GenerateAlphanumericToken(64),
                CreatedAt = dateTimeService.GetCurrentDateTime()
            };

            var email = new Services.EmailSender.Email()
            {
                Recipient = EmailAddress,
                Subject = "Email Verification",
                EmailBodyType = Services.EmailSender.EmailBodyType.HTML,
                Body = $"<a href=\"{apiBaseAddress}/Users/{User.Id}/verify-email?token={verificationToken.Token}\">click here to verify</a>"
            };

            emailSender.SendEmail(email);

            VerificationToken = verificationToken;
        }
    }
}
