namespace EncantoWebAPI.Models
{
    public class EventFeedback
    {
        public required string EventId { get; set; }
        public required bool EnableRating { get; set; }
        public required bool EnableComments { get; set; }
        public int TotalRaings { get; set; } // Total number of ratings received
        public float? Rating { get; set; } // Average ratings 1
        public List<Comments>? Comments { get; set; }
    }

    public class Comments
    {
        public required string UserId { get; set; }
        public required string GuestName { get; set; }
        public required string BackgroundColour { get; set; }
        public required string ForegroundColour { get; set; }
        public required string Comment { get; set; }
        public required long CreatedTimestamp { get; set; }
    }
}
