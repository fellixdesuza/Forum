namespace Forum.Services.Exeptions
{
    public class CommentNotFoundException : Exception
    {
        public CommentNotFoundException() : base("Comment Not Found.")
        {
        }
    }
}
