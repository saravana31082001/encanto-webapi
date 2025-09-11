using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace EncantoWebAPI.Models
{
    public class LoginCredential
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)] //Depticts primary key in DB
        public ObjectId Id { get; set; }  // MongoDB’s internal primary key
        public required string UserId { get; set; } //API's primary key
        public required string Username { get; set; }  // email as username
        public required string PasswordHash { get; set; }
        public long CreatedTimestamp { get; set; }
        public long UpdatedTimestamp { get; set; }

    }
}
