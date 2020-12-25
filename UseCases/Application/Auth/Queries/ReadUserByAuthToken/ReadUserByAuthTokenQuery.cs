using DomainModel.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Auth.Queries.ReadUserByAuthToken
{
    public class ReadUserByAuthTokenQuery : IRequest<User>
    {
        public string AuthToken { get; set; }
    }
}
