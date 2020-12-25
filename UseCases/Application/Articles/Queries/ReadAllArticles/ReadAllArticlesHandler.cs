using DomainModel.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PostgresDatabase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Articles.Queries.ReadAllArticles
{
    public class ReadAllArticlesHandler : IRequestHandler<ReadAllArticlesQuery, IList<Article>>
    {
        private readonly AppDbContext _appDbContext;

        public ReadAllArticlesHandler(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<IList<Article>> Handle(ReadAllArticlesQuery request, CancellationToken cancellationToken)
        {
            var articles = await _appDbContext.Articles.ToListAsync();

            return articles;
        }
    }
}
