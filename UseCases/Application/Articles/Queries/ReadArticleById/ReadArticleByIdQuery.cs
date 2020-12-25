using DomainModel.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Articles.Queries.ReadArticleById
{
    public class ReadArticleByIdQuery : IRequest<Article>
    {
        public Guid Id { get; set; }
    }
}
