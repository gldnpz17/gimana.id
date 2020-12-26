using DomainModel.Entities;
using DomainModel.Services;
using MediatR;
using PostgresDatabase;
using System;
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

            //number each part and step
            for (int partCount = 0; partCount < article.Parts.Count; partCount++)
            {
                var part = article.Parts[partCount];

                part.PartNumber = partCount + 1;

                for (int stepCount = 0; stepCount < part.Steps.Count; stepCount++)
                {
                    part.Steps[stepCount].StepNumber = stepCount + 1;
                }
            }

            _appDbContext.Articles.Add(article);

            article.Users.Add(request.User);

            await _appDbContext.SaveChangesAsync();

            return article;
        }
    }
}
