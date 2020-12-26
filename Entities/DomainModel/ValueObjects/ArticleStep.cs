namespace DomainModel.ValueObjects
{
    public class ArticleStep
    {
        public virtual int StepNumber { get; set; }
        public virtual string Title { get; set; }
        public virtual string Description { get; set; }
        public virtual Image Image { get; set; }
    }
}
