namespace EncantoWebAPI.Models.Events
{
    public class ParticipantDetails
    {
        public required string ParticipantId { get; set; } // UserId of Guest
        public required string ParticipantName { get; set; }
        public required string BackgroundColour {  get; set; }
        public required string ForegroundColour { get; set; }
        public required int RegistrationStatus { get; set; } // e.g., 0 = pending, 1 = confirmed, -1 = cancelled
        public required long RegisteredTimestamp { get; set; }
        public required long UpdatedTimestamp { get; set; }
    }
}