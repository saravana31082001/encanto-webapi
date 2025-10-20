namespace EncantoWebAPI.Models.Profiles.Requests
{
    public class UserBirthdayUpdateRequest
    {
        public required string UserId { get; set; }
        public required string DateOfBirth { get; set; } // "dd-mm-yyyy" format
        public required long UpdatedTimestamp { get; set; }
    }
}
