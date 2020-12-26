using DomainModel.Entities;
using MediatR;
using System;

namespace Application.Users.Queries.ReadUserById
{
    public class ReadUserByIdQuery : IRequest<User>
    {
        public Guid Id { get; set; }
    }
}
