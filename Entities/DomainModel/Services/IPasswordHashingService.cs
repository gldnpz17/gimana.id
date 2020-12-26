namespace DomainModel.Services
{
    public interface IPasswordHashingService
    {
        string HashPassword(string password, string salt);
    }
}
