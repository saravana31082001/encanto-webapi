namespace EncantoWebAPI.Models.Events
{
    public class PrivateEventRequestPreview
    {
        public required string EventId { get; set; }
        public required string EventName { get; set; }
        public required long StartTimestamp { get; set; }
        public required long EndTimestamp { get; set; }
        public required ParticipantDetails ParticipantDetails { get; set; }
        public required long ParticipantRequestTimestamp { get; set; } //time of sending the requests
    }
}
