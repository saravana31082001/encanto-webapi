namespace EncantoWebAPI.Models
{
    public class UserPhnUpdateRequest
    {
        public required string UserId { get; set; }
        public long? PhoneNumber { get; set; }
        public required long UpdatedTimestamp {  get; set; }
    }
}
