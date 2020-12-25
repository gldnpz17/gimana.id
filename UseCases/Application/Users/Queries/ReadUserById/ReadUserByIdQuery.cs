using DomainModel.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Users.Queries.ReadUserById
{
    public class ReadUserByIdQuery : IRequest<User>
    {
        public Guid Id { get; set; }
    }
}
