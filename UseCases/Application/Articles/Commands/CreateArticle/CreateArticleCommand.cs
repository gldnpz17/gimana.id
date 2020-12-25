using DomainModel.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Articles.Commands.CreateArticle
{
    public class CreateArticleCommand : IRequest
    {
        public Article Article { get; set; }
    }
}
