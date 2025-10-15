namespace EncantoWebAPI.Models.Profiles.Requests
{
    public class UserNameUpdateRequest
    {
        public required string UserId { get; set; }
        public required string Name { get; set; }
        public required long UpdatedTimestamp { get; set; }
    }
}
