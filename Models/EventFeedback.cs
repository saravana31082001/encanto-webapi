namespace EncantoWebAPI.Models
{
    public class EventFeedback
    {
        public required string EventId { get; set; }
        public required bool EnableRating { get; set; }
        public required bool EnableComments { get; set; }
        public int TotalRatings { get; set; } // Total number of ratings received
        public List<Ratings>? ParticipantRatings {  get; set; }
        public required float AverageRating { get; set; } // Average ratings
        public List<Comments>? Comments { get; set; }
    }

    public class Ratings
    {
        public required string UserId { get; set; }
        public required int ParticipantRating { get; set; }
        public required long CreatedTimestamp { get; set; }
        public required long UpdatedTimestamp { get; set; }
    }


    public class Comments
    {
        public required string UserId { get; set; }
        public required string GuestName { get; set; }
        public required string BackgroundColour { get; set; }
        public required string ForegroundColour { get; set; }
        public required string Comment { get; set; }
        public required long CreatedTimestamp { get; set; }
        public required long UpdatedTimestamp { get; set; }
    }
}
