using AnemicDomainModel.Entities;
using AnemicDomainModel.ValueObjects;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Articles.Commands.CreateArticle
{
    public class CreateArticleCommand : IRequest
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public Image HeroImage { get; set; }
        public List<ArticlePart> Parts { get; set; }
    }

    internal class CreateArticleHandler : IRequestHandler<CreateArticleCommand>
    {


        public Task<Unit> Handle(CreateArticleCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
