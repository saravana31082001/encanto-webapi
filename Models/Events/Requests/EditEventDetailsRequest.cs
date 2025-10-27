namespace EncantoWebAPI.Models.Events.Requests
{
    public class EditEventDetailsRequest
    {
        public required string EventId { get; set; }
        public required string Title { get; set; }
        public required string Description { get; set; }
        public required string MeetingLink { get; set; }
        public required long StartTimestamp { get; set; }
        public required long EndTimestamp { get; set; }
        public required bool Is_accepting_participants { get; set; }
        public required long UpdatedTimestamp { get; set; }
    }
}
