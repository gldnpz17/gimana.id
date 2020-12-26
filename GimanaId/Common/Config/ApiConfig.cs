using System;

namespace GimanaIdApi.Common.Config
{
    public class ApiConfig
    {
        public int PasswordResetTokenLength { get; set; }
        public int EmailVerificationTokenLength { get; set; }
        public int AuthTokenLength { get; set; }
        public string ApiBaseAddress { get; set; }
        public TimeSpan EmailVerificationTokenLifetime { get; set; }
        public TimeSpan PasswordResetTokenLifetime { get; set; }
    }
}
