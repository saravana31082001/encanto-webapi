namespace EncantoWebAPI.Models
{
    public class EventDetails
    {
        public required string EventId { get; set; }
        public required string Title { get; set; }
        public required string Description { get; set; }
        public required OrganizerDetails OrganizerDetails { get; set; }
        public required long StartTimestamp { get; set; }
        public required long EndTimestamp { get; set; }
        public required long CreatedTimestamp { get; set; }
        public required long UpdatedTimestamp { get; set; }
        public required bool IsPrivate { get; set; }
        public required List<ParticipantDetails> Participants { get; set; }
        public required bool Is_accepting_participants { get; set; }
        public int TotalRegisteredParticipants { get; set; }
        public required int Active { get; set; } //1 -> upcoming, 0 -> past, -1 -> cancelled
    }
}
