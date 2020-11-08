using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TanyakanIdApi.Common.Authentication
{
    public static class AuthorizationPolicyConstants
    {
        public const string AdminOnlyPolicy = "AdminOnly";
        public const string ModeratorOnlyPolicy = "ModeratorOnly";
        public const string AuthenticatedUsersOnlyPolicy = "AuthenticatedUsersOnly";
        public const string EmailVerifiedPolicy = "EmailVerified";
        public const string IsNotBannedPolicy = "IsNotBanned";
    }
}
