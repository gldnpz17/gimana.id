using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GimanaIdApi.DTOs.Request;
using GimanaIdApi.DTOs.Response;
using GimanaIdApi.Entities.Entities;
using GimanaIdApi.Entities.ValueObjects;

namespace GimanaIdApi.Common.Mapper
{
    public class MapperConfig
    {
        public MapperConfiguration GetConfiguration()
        {
            return new MapperConfiguration(
                (config) =>
                {
                    config.CreateMap<ArticleHistory, ArticleHistoryDto>();
                    config.CreateMap<ArticleIssue, ArticleIssueDto>();
                    config.CreateMap<ArticlePart, ArticlePartDto>();
                    config.CreateMap<ArticleRating, ArticleRatingDto>();
                    config.CreateMap<ArticleStep, ArticleStepDto>();
                    config.CreateMap<AuthToken, AuthTokenDto>();
                    config.CreateMap<Article, DetailedArticleDto>()
                    .ForMember(
                        dto => dto.Contributors, 
                        exp => exp.MapFrom(ori => ori.Users));
                    config.CreateMap<User, DetailedUserDto>()
                    .ForMember(
                        dto => dto.ContributedArticles,
                        exp => exp.MapFrom(ori => ori.Articles));
                    config.CreateMap<Image, ImageDto>();
                    config.CreateMap<Article, SimpleArticleDto>();
                    config.CreateMap<User, SimpleUserDto>();
                    config.CreateMap<UserEmail, UserEmailDto>();
                    config.CreateMap<User, UserIdDto>();
                    config.CreateMap<UserPrivilege, UserPrivilegeDto>();

                    config.CreateMap<CreateImageDto, Image>();

                    config.CreateMap<CreateArticlePartDto, ArticlePart>();
                    config.CreateMap<CreateArticleStepDto, ArticleIssue>();
                    config.CreateMap<CreateArticlePartDto, ArticlePart>();
                    config.CreateMap<CreateArticleStepDto, ArticleStep>();
                });
        }
    }
}
