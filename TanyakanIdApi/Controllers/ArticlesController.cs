using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TanyakanIdApi.Common.Authentication;
using TanyakanIdApi.DTOs.Request;
using TanyakanIdApi.DTOs.Response;
using TanyakanIdApi.Entities.Entities;
using TanyakanIdApi.Entities.ValueObjects;
using TanyakanIdApi.Infrastructure.DataAccess;

namespace TanyakanIdApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticlesController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public ArticlesController(
            AppDbContext appDbContext,
            IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        /// <summary>
        /// Read all articles.
        /// </summary>
        /// <returns></returns>
        [HttpGet("")]
        public async Task<ActionResult<List<SimpleArticleDto>>> ReadAllArticles()
        {
            var result = await _appDbContext.Articles.ToListAsync();
            var output = new List<SimpleArticleDto>();

            result.ForEach(i => output.Add(_mapper.Map<SimpleArticleDto>(i)));

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
            var result = await _appDbContext.Articles.FirstAsync(i => i.Id == Guid.Parse(articleId));

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
        public async Task<ActionResult> CreateArticle([FromBody]CreateArticleDto dto)
        {
            var newArticle = new Article()
            {
                Title = dto.Title,
                Description = dto.Description,
                HeroImage = _mapper.Map<Image>(dto.HeroImage)
            };

            for(int partCount = 0; partCount < dto.Parts.Count; partCount++)
            {
                var newPart = new ArticlePart()
                {
                    PartNumber = partCount + 1,
                    Title = dto.Parts[partCount].Title,
                    Description = dto.Parts[partCount].Description
                };

                for(int stepCount = 0; stepCount < dto.Parts[partCount].Steps.Count; stepCount++)
                {
                    var newStep = new ArticleStep()
                    {
                        StepNumber = stepCount + 1,
                        Title = dto.Parts[partCount].Steps[stepCount].Title,
                        Description = dto.Parts[partCount].Steps[stepCount].Description
                    };

                    newPart.Steps.Add(newStep);
                }

                newArticle.Parts.Add(newPart);
            }

            var currentUser = await GetCurrentUser();

            newArticle.Users.Add(currentUser);

            await _appDbContext.Articles.AddAsync(newArticle);

            await _appDbContext.SaveChangesAsync();

            return Ok();
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
            var article = await _appDbContext.Articles.FirstOrDefaultAsync(i => i.Id == Guid.Parse(articleId));

            var currentUser = await GetCurrentUser();

            if (!article.Users.Contains(currentUser))
            {
                article.Users.Add(currentUser);
            }

            var newHistory =
                new ArticleHistory()
                {
                    Version = article.Histories.Count + 1,
                    TimeStamp = DateTime.Now,
                    Title = article.Title,
                    Description = article.Description,
                    HeroImage = article.HeroImage,
                    Parts = article.Parts,
                    Issues = article.Issues,
                    Ratings = article.Ratings,
                    Contributors = article.Users
                };
            article.Histories.Add(newHistory);

            article.Title = dto.Title;
            article.Description = dto.Description;
            article.HeroImage = _mapper.Map<Image>(dto.HeroImage);
            article.Parts = _mapper.Map<List<ArticlePart>>(dto.Parts);

            await _appDbContext.SaveChangesAsync();

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
            var article = await _appDbContext.Articles.FirstOrDefaultAsync(i => i.Id == Guid.Parse(articleId));

            _appDbContext.Articles.Remove(article);

            await _appDbContext.SaveChangesAsync();

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
            var article = await _appDbContext.Articles.FirstOrDefaultAsync(i => i.Id == Guid.Parse(articleId));

            var history = article.Histories.FirstOrDefault(i => i.Version == dto.Version);

            article.Title = history.Title;
            article.Description = history.Description;
            article.HeroImage = history.HeroImage;
            article.Users = history.Contributors;
            article.Parts = history.Parts;
            article.Issues = history.Issues;
            article.Ratings = history.Ratings;

            //remove histories
            var historiesToDelete = new List<ArticleHistory>();
            foreach (var i in article.Histories)
            {
                if (i.Version >= dto.Version)
                {
                    historiesToDelete.Add(i);
                }
            }
            historiesToDelete.ForEach(i => article.Histories.Remove(i));

            await _appDbContext.SaveChangesAsync();

            return Ok();
        }

        /// <summary>
        /// Read all article issues.
        /// </summary>
        /// <param name="articleId"></param>
        /// <returns></returns>
        [HttpGet("{articleId}/issues")]
        public async Task<ActionResult<List<ArticleIssueDto>>> ReadAllArticleIssues([FromRoute]string articleId)
        {
            var article = await _appDbContext.Articles.FirstOrDefaultAsync(i => i.Id == Guid.Parse(articleId));

            return _mapper.Map<List<ArticleIssueDto>>(article.Issues);
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
            var article = await _appDbContext.Articles.FirstOrDefaultAsync(i => i.Id == Guid.Parse(articleId));

            var newIssue = new ArticleIssue()
            {
                Article = article,
                Title = dto.Title,
                Content = dto.Content,
                Resolved = false,
                Submitter = await GetCurrentUser()
            };

            article.Issues.Add(newIssue);

            await _appDbContext.SaveChangesAsync();

            return Ok();
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
            var article = await _appDbContext.Articles.FirstOrDefaultAsync(i => i.Id == Guid.Parse(articleId));

            var issue = article.Issues.FirstOrDefault(i => i.Id == Guid.Parse(issueId));

            issue.Resolved = true;

            await _appDbContext.SaveChangesAsync();

            return Ok();
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
            var article = await _appDbContext.Articles.FirstOrDefaultAsync(i => i.Id == Guid.Parse(articleId));

            var issue = article.Issues.FirstOrDefault(i => i.Id == Guid.Parse(issueId));

            article.Issues.Remove(issue);

            await _appDbContext.SaveChangesAsync();

            return Ok();
        }

        /// <summary>
        /// Read all ratings.
        /// </summary>
        /// <param name="articleId"></param>
        /// <returns></returns>
        [HttpGet("{articleId}/ratings")]
        public async Task<ActionResult<List<ArticleRatingDto>>> ReadAllRatings([FromRoute]string articleId)
        {
            var article = await _appDbContext.Articles.FirstOrDefaultAsync(i => i.Id == Guid.Parse(articleId));

            var ratings = article.Ratings;

            return Ok(_mapper.Map<List<ArticleRatingDto>>(ratings));
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
            var article = await _appDbContext.Articles.FirstOrDefaultAsync(i => i.Id == Guid.Parse(articleId));

            var newRating = new ArticleRating()
            {
                Article = article,
                Title = dto.Title,
                Content = dto.Content,
                Rating = dto.Rating,
                Submitter = await GetCurrentUser()
            };

            article.Ratings.Add(newRating);

            await _appDbContext.SaveChangesAsync();

            return Ok();
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