namespace TanyakanIdApi.Infrastructure.AlphanumericTokenGenerator
{
    public interface IAlphanumericTokenGenerator
    {
        string GenerateAlphanumericToken(int length);
    }
}