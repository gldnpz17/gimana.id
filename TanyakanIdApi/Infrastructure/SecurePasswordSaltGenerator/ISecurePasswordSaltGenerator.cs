namespace TanyakanIdApi.Infrastructure.SecurePasswordSaltGenerator
{
    public interface ISecurePasswordSaltGenerator
    {
        string GenerateSecureRandomString();
    }
}