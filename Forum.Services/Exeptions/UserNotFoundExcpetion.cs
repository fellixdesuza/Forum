namespace Forum.Services.Exeptions
{
    public class UserNotFoundException : Exception
    {
        public UserNotFoundException() : base("User not found")
        {
        }
    }
}
