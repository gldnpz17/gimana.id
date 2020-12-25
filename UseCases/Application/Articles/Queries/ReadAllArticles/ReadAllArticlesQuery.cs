using DomainModel.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Articles.Queries.ReadAllArticles
{
    public class ReadAllArticlesQuery : IRequest<IList<Article>>
    {
        
    }
}
