namespace EncantoWebAPI.Models
{
    public class CreateEventRequest
    {
        public required string Title { get; set; }
        public required string Description { get; set; }
        public required string OrganizerId { get; set; } // UserId of Host
        public required string OrganizerName { get; set; }
        public required string OrganizerBackground { get; set; }
        public required string OrganizerForeground { get; set; }
        public required int SendLinkNotificationAt { get; set; }
        public required bool EnableRating { get; set; }
        public required bool EnableComments { get; set; }        
        public required long StartTimestamp { get; set; }
        public required long EndTimestamp { get; set; }
        public required long CreatedTimestamp { get; set; }
        public required bool IsPrivate { get; set; }
    }
}
