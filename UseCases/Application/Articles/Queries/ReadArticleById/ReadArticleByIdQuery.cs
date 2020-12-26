using DomainModel.Entities;
using MediatR;
using System;

namespace Application.Articles.Queries.ReadArticleById
{
    public class ReadArticleByIdQuery : IRequest<Article>
    {
        public Guid Id { get; set; }
    }
}
