namespace Forum.Models.Identity
{
    public class RegistrationRequestDto
    {
        public string Email { get; set; }
        public string Token { get; set; }
        public string Password { get; set; }
    }
}
