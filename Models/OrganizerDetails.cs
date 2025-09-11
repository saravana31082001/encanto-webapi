namespace EncantoWebAPI.Models
{
    public class OrganizerDetails
    {
        public required string OrganizerId { get; set; } // UserId of Host
        public required string OrganizerName { get; set; }
        public required string EventId { get; set; }
        public required string Background { get; set; }
        public required string Foreground { get; set; }
    }
}
