namespace GimanaIdApi.Common.Authentication
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
