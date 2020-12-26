using DomainModel.Entities;
using MediatR;

namespace Application.Articles.Commands.CreateArticle
{
    public class CreateArticleCommand : IRequest<Article>
    {
        public User User { get; set; }
        public Article Article { get; set; }
    }
}
