namespace EncantoWebAPI.Models.Events.Requests
{
    public class EventApplicationRequest
    {
        public required string UserId { get; set; } //user id of participant
        public required string EventId { get; set; }
        public required long UpdatedTimestamp { get; set; }
    }
}
