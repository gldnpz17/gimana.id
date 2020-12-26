namespace GimanaIdApi.DTOs.Response
{
    public class UserEmailDto
    {
        public virtual string EmailAddress { get; set; }
        public virtual bool IsVerified { get; set; }
    }
}
