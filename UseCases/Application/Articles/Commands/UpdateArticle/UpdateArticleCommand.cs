using DomainModel.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Articles.Commands.UpdateArticle
{
    public class UpdateArticleCommand : IRequest
    {
        public User User { get; set; }
        public Article Article { get; set; }
    }
}
