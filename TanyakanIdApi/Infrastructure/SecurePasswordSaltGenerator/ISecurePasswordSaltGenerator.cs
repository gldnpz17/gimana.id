namespace GimanaIdApi.Infrastructure.SecurePasswordSaltGenerator
{
    public interface ISecurePasswordSaltGenerator
    {
        string GenerateSecureRandomString();
    }
}