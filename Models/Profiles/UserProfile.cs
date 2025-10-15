using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace EncantoWebAPI.Models.Profiles
{
    public class UserProfile
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)] //Depticts primary key in DB
        public ObjectId Id { get; set; }  // MongoDB’s internal primary key
        public required string UserId { get; set; } //API's primary key
        public required string Name { get; set; }
        public required string Email { get; set; }
        public long? PhoneNumber { get; set; }
        public string? DateOfBirth { get; set; } // 'yyyy-mm-dd' format
        public string? AddressId { get; set; }
        public Address? Address { get; set; }
        public required string ProfileType { get; set; } //"guest" or "host"
        public string? Gender { get; set; }
        public string? OccupationId { get; set; }
        public OccupationDetails? OccupationDetails { get; set; }
        public required string BackgroundColour { get; set; }
        public required string ForegroundColour { get; set; }
        public long CreatedTimestamp { get; set; }
        public long UpdatedTimestamp { get; set; }
        public required bool Is_email_verified { get; set; }
        public required int Active { get; set; } //1 -> active account, 0 -> deleted account
    }
}
