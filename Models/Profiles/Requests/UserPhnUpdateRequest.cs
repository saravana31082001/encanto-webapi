namespace EncantoWebAPI.Models.Profiles.Requests
{
    public class UserPhnUpdateRequest
    {
        public required string UserId { get; set; }
        public required long PhoneNumber { get; set; }
        public required long UpdatedTimestamp {  get; set; }
    }
}
