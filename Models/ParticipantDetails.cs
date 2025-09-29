namespace EncantoWebAPI.Models
{
    public class ParticipantDetails
    {
        public required string ParticipantId { get; set; } // UserId of Guest
        public required string ParticipantName { get; set; }
        public required string EventId { get; set; }
        public required long RegisteredTimestamp { get; set; }
        public required long UpdatedTimestamp { get; set; }
        public required string BackgroundColour {  get; set; }
        public required string ForegroundColour { get; set; }
        public required int Status { get; set; } // e.g., 0 = pending, 1 = confirmed, -1 = cancelled
    }
}