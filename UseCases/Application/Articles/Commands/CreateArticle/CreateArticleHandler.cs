using DomainModel.Entities;
using DomainModel.Services;
using MediatR;
using PostgresDatabase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Articles.Commands.CreateArticle
{
    public class CreateArticleHandler : IRequestHandler<CreateArticleCommand, Article>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IDateTimeService _dateTimeService;

        public CreateArticleHandler(AppDbContext appDbContext, IDateTimeService dateTimeService)
        {
            _appDbContext = appDbContext;
            _dateTimeService = dateTimeService;
        }

        public async Task<Article> Handle(CreateArticleCommand request, CancellationToken cancellationToken)
        {
            var article = request.Article;

            article.Id = Guid.NewGuid();
            article.DateCreated = _dateTimeService.GetCurrentDateTime();

            article.Users.Add(request.User);
            request.User.Articles.Add(article);

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

            await _appDbContext.Articles.AddAsync(article);

            await _appDbContext.SaveChangesAsync();

            return article;
        }
    }
}
