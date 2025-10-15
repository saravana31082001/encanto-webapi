using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace EncantoWebAPI.Models.Events
{
    public class EventFeedback
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)] //Depticts primary key in DB
        public ObjectId Id { get; set; }  // MongoDB’s internal primary key
        public required string EventId { get; set; }
        public required bool EnableRating { get; set; }
        public required bool EnableComments { get; set; }
        public required int TotalRatings { get; set; } // Total number of ratings received
        public List<Ratings>? ParticipantRatings {  get; set; }
        public float? AverageRating { get; set; } // Average ratings
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
