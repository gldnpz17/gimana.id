using DomainModel.Entities;
using MediatR;

namespace Application.Auth.Queries.ReadUserByAuthToken
{
    public class ReadUserByAuthTokenQuery : IRequest<User>
    {
        public string AuthToken { get; set; }
    }
}
