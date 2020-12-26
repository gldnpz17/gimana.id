using MediatR;

namespace Application.Auth.Commands.ResetPassword
{
    public class ResetPasswordCommand : IRequest
    {
        public string PasswordResetToken { get; set; }
        public string NewPassword { get; set; }
    }
}
