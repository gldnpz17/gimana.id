using DomainModel.Entities;
using MediatR;

namespace Application.Articles.Commands.UpdateArticle
{
    public class UpdateArticleCommand : IRequest
    {
        public User User { get; set; }
        public Article Article { get; set; }
    }
}
