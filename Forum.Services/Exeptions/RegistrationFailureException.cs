namespace Forum.Services.Exeptions
{
    public class RegistrationFailureException : Exception
    {
        public RegistrationFailureException(string message) : base(message)
        {
        }
    }
}
