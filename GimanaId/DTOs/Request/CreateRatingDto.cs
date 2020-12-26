namespace GimanaIdApi.DTOs.Request
{
    public class CreateRatingDto
    {
        public virtual string Title { get; set; }
        public virtual string Content { get; set; }
        public virtual int Rating { get; set; }
    }
}
