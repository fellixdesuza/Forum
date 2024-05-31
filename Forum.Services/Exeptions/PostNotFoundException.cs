namespace Forum.Services.Exeptions
{
    public class PostNotFoundException : Exception
    {
        public PostNotFoundException() : base("Post Not Found.") 
        { 
        }
    }
}
