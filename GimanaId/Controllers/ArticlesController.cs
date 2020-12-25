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
using GimanaIdApi.Entities.Entities;
using GimanaIdApi.Entities.ValueObjects;
using GimanaIdApi.Infrastructure.DataAccess;
using GimanaId.Infrastructure.DateTimeService;
using GimanaId.DTOs.Response;

namespace GimanaIdApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticlesController : ControllerBase
    {
        public ArticlesController()
        {
        }

        /// <summary>
        /// Read all articles.
        /// </summary>
        /// <returns></returns>
        [HttpGet("")]
        public async Task<ActionResult<List<SimpleArticleDto>>> ReadAllArticles()
        {

        }

        /// <summary>
        /// Read article by id.
        /// </summary>
        /// <param name="articleId"></param>
        /// <returns></returns>
        [HttpGet("{articleId}")]
        public async Task<ActionResult<DetailedArticleDto>> ReadArticlebyId([FromRoute]string articleId) 
        {

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

        }

        /// <summary>
        /// Read all article issues.
        /// </summary>
        /// <param name="articleId"></param>
        /// <returns></returns>
        [HttpGet("{articleId}/issues")]
        public async Task<ActionResult<List<ArticleIssueDto>>> ReadAllArticleIssues([FromRoute]string articleId)
        {

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

        }

        /// <summary>
        /// Read all ratings.
        /// </summary>
        /// <param name="articleId"></param>
        /// <returns></returns>
        [HttpGet("{articleId}/ratings")]
        public async Task<ActionResult<List<ArticleRatingDto>>> ReadAllRatings([FromRoute]string articleId)
        {

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
            var article = await _appDbContext.Articles.FirstOrDefaultAsync(i => i.Id == Guid.Parse(articleId));

            var rating = article.Ratings.FirstOrDefault(i => i.Id == Guid.Parse(ratingId));

            article.Ratings.Remove(rating);

            await _appDbContext.SaveChangesAsync();

            return Ok();
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
            var article = await _appDbContext.Articles.FirstOrDefaultAsync(i => i.Id == Guid.Parse(articleId));

            var histories = article.Histories;

            var output = _mapper.Map<List<ArticleHistoryDto>>(histories);

            return Ok(output);
        }

        private async Task<User> GetCurrentUser()
        {
            return await _appDbContext.Users.FirstOrDefaultAsync(i => i.Id == Guid.Parse(User.FindFirst("UserId").Value));
        }
    }
}