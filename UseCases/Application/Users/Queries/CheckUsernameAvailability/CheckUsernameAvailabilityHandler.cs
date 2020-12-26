using MediatR;
using Microsoft.EntityFrameworkCore;
using PostgresDatabase;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Users.Queries.CheckUsernameAvailability
{
    public class CheckUsernameAvailabilityHandler : IRequestHandler<CheckUsernameAvailabilityQuery, bool>
    {
        private readonly AppDbContext _appDbContext;

        public CheckUsernameAvailabilityHandler(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<bool> Handle(CheckUsernameAvailabilityQuery request, CancellationToken cancellationToken)
        {
            var user = await _appDbContext.Users.FirstOrDefaultAsync(i => i.Username == request.Username);

            if (user == null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
