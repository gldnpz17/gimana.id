using System;

namespace GimanaIdApi.DTOs.Response
{
    public class ArticleRatingDto
    {
        public virtual Guid Id { get; set; }
        public virtual SimpleUserDto Submitter { get; set; }
        public virtual string Title { get; set; }
        public virtual string Content { get; set; }
        public virtual int Rating { get; set; }
    }
}
