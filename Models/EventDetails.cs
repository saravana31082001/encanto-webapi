using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace EncantoWebAPI.Models
{
    public class EventDetails
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)] //Depticts primary key in DB
        public ObjectId Id { get; set; }  // MongoDB’s internal primary key
        public required string EventId { get; set; }
        public required string Title { get; set; }
        public required string Description { get; set; }
        public List<string>? Tags { get; set; }
        public string? MeetingLink { get; set; }
        public required int SendLinkNotificationAt { get; set; } //1-> send meet link at a physical push of a button , 2->send meeting link when Is_accepting_participants is turned off, 3-> send when user Applies/isApproved for the event
        public EventFeedback? EventFeedback { get; set; }
        public required OrganizerDetails OrganizerDetails { get; set; }
        public required long StartTimestamp { get; set; }
        public required long EndTimestamp { get; set; }
        public required long CreatedTimestamp { get; set; }
        public required long UpdatedTimestamp { get; set; }
        public required bool IsPrivate { get; set; }
        public List<ParticipantDetails>? Participants { get; set; }
        public required bool Is_accepting_participants { get; set; }
        public required int TotalRegisteredParticipants { get; set; }
        public required int Active { get; set; } //1 -> upcoming, 0 -> past, -1 -> cancelled
    }
}
