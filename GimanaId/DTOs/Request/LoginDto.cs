namespace GimanaIdApi.DTOs.Request
{
    public class LoginDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; } // On `true`, keep auth token on client browser for a predetermined amount of time
    }
}
