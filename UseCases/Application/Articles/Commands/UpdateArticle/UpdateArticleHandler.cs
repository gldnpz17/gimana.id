using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PostgresDatabase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Articles.Commands.UpdateArticle
{
    public class UpdateArticleHandler : IRequestHandler<UpdateArticleCommand>
    {
        private readonly AppDbContext _appDbContext;

        public UpdateArticleHandler(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<Unit> Handle(UpdateArticleCommand request, CancellationToken cancellationToken)
        {
            var article = await _appDbContext.Articles.FirstOrDefaultAsync(i => i.Id == request.Article.Id);

            if (article == null)
            {
                throw new ApplicationException("Article not found.");
            }

            article.Title = request.Article.Title;
            article.Description = request.Article.Description;
            article.HeroImage = request.Article.HeroImage;
            article.Parts = request.Article.Parts;
            
            if (!article.Users.Contains(request.User))
            {
                article.Users.Add(request.User);
            }

            //number each part and step
            for (int partCount = 0; partCount < article.Parts.Count; partCount++)
            {
                var part = article.Parts[partCount];

                part.PartNumber = partCount;

                for (int stepCount = 0; stepCount < part.Steps.Count; stepCount++)
                {
                    part.Steps[stepCount].StepNumber = stepCount;
                }
            }

            await _appDbContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
