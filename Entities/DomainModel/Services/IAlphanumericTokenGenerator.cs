namespace DomainModel.Services
{
    public interface IAlphanumericTokenGenerator
    {
        string GenerateAlphanumericToken(int length);
    }
}
