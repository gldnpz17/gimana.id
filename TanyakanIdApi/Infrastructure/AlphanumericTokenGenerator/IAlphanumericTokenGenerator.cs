namespace GimanaIdApi.Infrastructure.AlphanumericTokenGenerator
{
    public interface IAlphanumericTokenGenerator
    {
        string GenerateAlphanumericToken(int length);
    }
}