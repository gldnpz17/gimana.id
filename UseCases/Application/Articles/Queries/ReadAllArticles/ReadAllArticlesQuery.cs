using DomainModel.Entities;
using MediatR;
using System.Collections.Generic;

namespace Application.Articles.Queries.ReadAllArticles
{
    public class ReadAllArticlesQuery : IRequest<IList<Article>>
    {
        
    }
}
