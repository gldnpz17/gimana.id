namespace GimanaIdApi.DTOs.Response
{
    public class ArticleStepDto
    {
        public virtual int StepNumber { get; set; }
        public virtual string Title { get; set; }
        public virtual string Description { get; set; }
        public virtual ImageDto Image { get; set; }
    }
}
