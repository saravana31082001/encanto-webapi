namespace EncantoWebAPI.Models.Auth
{
    public class SignupRequest
    {
        public required string Name { get; set; }
        public required string Email { get; set; }  // email as username
        public required string PasswordHash { get; set; }
        public required string ProfileType { get; set; } //"Guest" or "Host"
        public long CreatedTimestamp { get; set; }
        public long UpdatedTimestamp { get; set; }
    }
}
