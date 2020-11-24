namespace GimanaIdApi.Infrastructure.PasswordHasher
{
    public interface IPasswordHasher
    {
        string HashPassword(string password, string salt);
    }
}