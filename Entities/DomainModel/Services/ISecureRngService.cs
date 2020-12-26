namespace DomainModel.Services
{
    public interface ISecureRngService
    {
        byte[] GenerateRandomBytes(int length);
    }
}
