using DomainModel.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PostgresDatabase;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Articles.Queries.ReadArticleById
{
    public class ReadArticleByIdHandler : IRequestHandler<ReadArticleByIdQuery, Article>
    {
        private readonly AppDbContext _appDbContext;

        public ReadArticleByIdHandler(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<Article> Handle(ReadArticleByIdQuery request, CancellationToken cancellationToken)
        {
            var article = await _appDbContext.Articles.FirstOrDefaultAsync(i => i.Id == request.Id);

            if (article == null)
            {
                throw new ApplicationException("Article not found.");
            }

            return article;
        }
    }
}
