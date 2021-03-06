﻿using DomainModel.Common;
using DomainModel.Services;
using DomainModel.ValueObjects;
using System;

namespace DomainModel.Entities
{
    public class PasswordCredential
    {
        public PasswordCredential() 
        {

        }
        public PasswordCredential(string password, IPasswordHashingService passwordHashingService, ISecureRngService secureRngService)
        {
            PasswordSalt = Convert.ToBase64String(secureRngService.GenerateRandomBytes(64));

            HashedPassword = passwordHashingService.HashPassword(password, PasswordSalt);
        }

        public virtual Guid UserId { get; set; }
        public virtual User User { get; set; }
        public virtual string HashedPassword { get; set; }
        public virtual string PasswordSalt { get; set; }
        public virtual PasswordResetToken PasswordResetToken { get; set; }

        public bool Verify(string password, IPasswordHashingService passwordHashingService)
        {
            if (passwordHashingService.HashPassword(password, PasswordSalt) == HashedPassword)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void Reset(string newPassword, IDateTimeService dateTimeService, IPasswordHashingService passwordHashingService, 
            ISecureRngService secureRngService)
        {
            if (dateTimeService.GetCurrentDateTime() < PasswordResetToken.CreatedAt.AddHours(6))
            {
                PasswordSalt = Convert.ToBase64String(secureRngService.GenerateRandomBytes(64));

                HashedPassword = passwordHashingService.HashPassword(newPassword, PasswordSalt);
            }
            else
            {
                throw new DomainModelException("Token expired.");
            }
        }
    }
}
