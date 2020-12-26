using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GimanaIdApi.Common.Authentication;
using GimanaIdApi.DTOs.Request;
using GimanaIdApi.DTOs.Response;
using GimanaId.DTOs.Response;
using MediatR;
using Application.Articles.Queries.ReadAllArticles;
using Application.Articles.Queries.ReadArticleById;
using Application.Articles.Commands.CreateArticle;
using DomainModel.Entities;
using Application.Articles.Commands.UpdateArticle;
using Application.Users.Queries.ReadUserById;
using Application.Articles.Commands.DeleteArticle;

namespace GimanaIdApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticlesController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public ArticlesController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        /// <summary>
        /// Read all articles.
        /// </summary>
        /// <returns></returns>
        [HttpGet("")]
        public async Task<ActionResult<List<SimpleArticleDto>>> ReadAllArticles()
        {
            var result = await _mediator.Send(new ReadAllArticlesQuery());

            var output = new List<SimpleArticleDto>();

            result.ToList().ForEach(i =>
            {
                output.Add(_mapper.Map<SimpleArticleDto>(i));
            });

            return output;
        }

        /// <summary>
        /// Read article by id.
        /// </summary>
        /// <param name="articleId"></param>
        /// <returns></returns>
        [HttpGet("{articleId}")]
        public async Task<ActionResult<DetailedArticleDto>> ReadArticlebyId([FromRoute]string articleId) 
        {
            var result = await _mediator.Send(new ReadArticleByIdQuery() 
            { 
                Id = Guid.Parse(articleId) 
            });

            var output = _mapper.Map<DetailedArticleDto>(result);

            return output;
        }

        /// <summary>
        /// Create article.
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [Authorize(Policy = AuthorizationPolicyConstants.IsNotBannedPolicy)]
        [Authorize(Policy = AuthorizationPolicyConstants.EmailVerifiedPolicy)]
        [HttpPost("")]
        public async Task<ActionResult<CreateArticleResponseDto>> CreateArticle([FromBody]CreateArticleDto dto)
        {
            var result = await _mediator.Send(new CreateArticleCommand() 
            { 
                User = await GetCurrentUser(),
                Article = _mapper.Map<Article>(dto)
            });

            return new CreateArticleResponseDto() { Id = result.Id };
        }

        /// <summary>
        /// Update article.
        /// </summary>
        /// <param name="articleId"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        [Authorize(Policy = AuthorizationPolicyConstants.IsNotBannedPolicy)]
        [Authorize(Policy = AuthorizationPolicyConstants.EmailVerifiedPolicy)]
        [HttpPut("{articleId}")]
        public async Task<ActionResult> UpdateArticle([FromRoute]string articleId, [FromBody]CreateArticleDto dto)
        {
            var newArticle = _mapper.Map<Article>(dto);
            newArticle.Id = Guid.Parse(articleId);

            await _mediator.Send(new UpdateArticleCommand()
            {
                User = await GetCurrentUser(),
                Article = newArticle
            });

            return Ok();
        }

        /// <summary>
        /// Delete article.
        /// </summary>
        /// <param name="articleId"></param>
        /// <returns></returns>
        [Authorize(Policy = AuthorizationPolicyConstants.IsNotBannedPolicy)]
        [Authorize(Policy = AuthorizationPolicyConstants.ModeratorOnlyPolicy)]
        [HttpDelete("{articleId}")]
        public async Task<ActionResult> DeleteArticle([FromRoute]string articleId)
        {
            await _mediator.Send(new DeleteArticleCommand() 
            { 
                Id = Guid.Parse(articleId) 
            });

            return Ok();
        }
        
        /// <summary>
        /// Undo changes to an article.
        /// </summary>
        /// <param name="articleId"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        [Authorize(Policy = AuthorizationPolicyConstants.IsNotBannedPolicy)]
        [Authorize(Policy = AuthorizationPolicyConstants.ModeratorOnlyPolicy)]
        [HttpPost("{articleId}")]
        public async Task<ActionResult> RevertArticle([FromRoute]string articleId, [FromBody]RevertArticleDto dto)
        {
            return StatusCode(501); //not implemented
        }

        /// <summary>
        /// Read all article issues.
        /// </summary>
        /// <param name="articleId"></param>
        /// <returns></returns>
        [HttpGet("{articleId}/issues")]
        public async Task<ActionResult<List<ArticleIssueDto>>> ReadAllArticleIssues([FromRoute]string articleId)
        {
            return StatusCode(501); //not implemented
        }

        /// <summary>
        /// Create issue.
        /// </summary>
        /// <param name="articleId"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        [Authorize(Policy = AuthorizationPolicyConstants.IsNotBannedPolicy)]
        [Authorize(Policy = AuthorizationPolicyConstants.ModeratorOnlyPolicy)]
        [HttpPost("{articleId}/issues")]
        public async Task<ActionResult> CreateIssue([FromRoute]string articleId, [FromBody]CreateArticleIssueDto dto)
        {
            return StatusCode(501); //not implemented
        }

        /// <summary>
        /// Mark issue as resolved.
        /// </summary>
        /// <param name="articleId"></param>
        /// <param name="issueId"></param>
        /// <returns></returns>
        [Authorize(Policy = AuthorizationPolicyConstants.IsNotBannedPolicy)]
        [Authorize(Policy = AuthorizationPolicyConstants.EmailVerifiedPolicy)]
        [HttpPost("{articleId}/issues/{issueId}/mark-as-resolved")]
        public async Task<ActionResult> MarkIssueAsResolved([FromRoute]string articleId, [FromRoute]string issueId)
        {
            return StatusCode(501); //not implemented
        }

        /// <summary>
        /// Delete issue.
        /// </summary>
        /// <param name="articleId"></param>
        /// <param name="issueId"></param>
        /// <returns></returns>
        [Authorize(Policy = AuthorizationPolicyConstants.IsNotBannedPolicy)]
        [Authorize(Policy = AuthorizationPolicyConstants.ModeratorOnlyPolicy)]
        [HttpDelete("{articleId}/issues/{issueId}")]
        public async Task<ActionResult> DeleteIssue([FromRoute]string articleId, [FromRoute]string issueId)
        {
            return StatusCode(501); //not implemented
        }

        /// <summary>
        /// Read all ratings.
        /// </summary>
        /// <param name="articleId"></param>
        /// <returns></returns>
        [HttpGet("{articleId}/ratings")]
        public async Task<ActionResult<List<ArticleRatingDto>>> ReadAllRatings([FromRoute]string articleId)
        {
            return StatusCode(501); //not implemented
        }

        /// <summary>
        /// Create rating.
        /// </summary>
        /// <param name="articleId"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        [Authorize(Policy = AuthorizationPolicyConstants.IsNotBannedPolicy)]
        [Authorize(Policy = AuthorizationPolicyConstants.EmailVerifiedPolicy)]
        [HttpPost("{articleId}/ratings")]
        public async Task<ActionResult> CreateRating([FromRoute]string articleId, [FromBody]CreateRatingDto dto)
        {
            return StatusCode(501); //not implemented
        }
        
        /// <summary>
        /// Delete rating.
        /// </summary>
        /// <param name="articleId"></param>
        /// <param name="ratingId"></param>
        /// <returns></returns>
        [Authorize(Policy = AuthorizationPolicyConstants.IsNotBannedPolicy)]
        [Authorize(Policy = AuthorizationPolicyConstants.ModeratorOnlyPolicy)]
        [HttpDelete("{articleId}/ratings/{ratingId}")]
        public async Task<ActionResult> DeleteRating([FromRoute]string articleId, [FromRoute]string ratingId)
        {
            return StatusCode(501); //not implemented
        }

        /// <summary>
        /// Get an article's version history.
        /// </summary>
        /// <param name="articleId"></param>
        /// <returns></returns>
        [Authorize(Policy = AuthorizationPolicyConstants.IsNotBannedPolicy)]
        [Authorize(Policy = AuthorizationPolicyConstants.EmailVerifiedPolicy)]
        [HttpGet("{articleId}/previous-versions")]
        public async Task<ActionResult<List<ArticleHistoryDto>>> ReadAllPreviousVersions([FromRoute]string articleId)
        {
            return StatusCode(501); //not implemented
        }

        private async Task<User> GetCurrentUser()
        {
            var result = await _mediator.Send(new ReadUserByIdQuery()
            {
                Id = Guid.Parse(User.FindFirst("UserId").Value)
            });

            return result;
        }
    }
}