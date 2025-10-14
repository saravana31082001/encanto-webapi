namespace EncantoWebAPI.Models
{
    public class UserNameUPRequest
    { 
        public required string UserId { get; set; }
        public required string Name { get; set; }
        public required long UpdatedTimestamp { get; set; }
    }
}
