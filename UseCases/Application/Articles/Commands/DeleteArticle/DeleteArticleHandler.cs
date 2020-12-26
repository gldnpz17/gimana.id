using MediatR;
using Microsoft.EntityFrameworkCore;
using PostgresDatabase;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Articles.Commands.DeleteArticle
{
    public class DeleteArticleHandler : IRequestHandler<DeleteArticleCommand>
    {
        private readonly AppDbContext _appDbContext;

        public DeleteArticleHandler(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<Unit> Handle(DeleteArticleCommand request, CancellationToken cancellationToken)
        {
            var article = await _appDbContext.Articles.FirstOrDefaultAsync(i => i.Id == request.Id);

            if (article == null)
            {
                throw new ApplicationException("Article not found.");
            }

            _appDbContext.Remove(article);

            await _appDbContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
