using MediatR;

namespace Application.Users.Queries.CheckUsernameAvailability
{
    public class CheckUsernameAvailabilityQuery : IRequest<bool>
    {
        public string Username { get; set; }
    }
}
