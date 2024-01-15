namespace campground_api.Models.Dto
{
    public class SignInDto
    {
        public required string Username { get; set; }
        public required string Password { get; set; }
        public required string Email { get; set; }

        
    }
}
