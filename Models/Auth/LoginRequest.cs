namespace EncantoWebAPI.Models.Auth
{
    public class LoginRequest
    {
        public required string Email { get; set; }  // email as username
        public required string PasswordHash { get; set; }
    }
}
