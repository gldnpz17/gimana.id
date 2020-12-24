﻿using DomainModel.Common;
using DomainModel.Services;
using DomainModel.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.Entities
{
    public class PasswordCredential
    {
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

        public void Reset(string newPassword, string token, IDateTimeService dateTimeService,
            IPasswordHashingService passwordHashingService, ISecureRngService secureRngService)
        {
            if (token == PasswordResetToken.Token)
            {
                if (dateTimeService.GetCurrentDateTime() < PasswordResetToken.CreatedAt.AddHours(6))
                {
                    PasswordSalt = Convert.ToBase64String(secureRngService.GenerateRandomBytes());

                    HashedPassword = passwordHashingService.HashPassword(newPassword, PasswordSalt);
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
    }
}