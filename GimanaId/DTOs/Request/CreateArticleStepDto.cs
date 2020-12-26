namespace GimanaIdApi.DTOs.Request
{
    public class CreateArticleStepDto
    {
        public virtual string Title { get; set; }
        public virtual string Description { get; set; }
        public virtual CreateImageDto Image { get; set; }
    }
}
