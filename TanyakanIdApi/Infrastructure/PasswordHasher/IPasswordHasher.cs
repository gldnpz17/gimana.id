namespace TanyakanIdApi.Infrastructure.PasswordHasher
{
    public interface IPasswordHasher
    {
        string HashPassword(string password, string salt);
    }
}