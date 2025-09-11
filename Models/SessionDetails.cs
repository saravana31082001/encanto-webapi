using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace EncantoWebAPI.Models
{
    public class SessionDetails
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)] //Depticts primary key in DB
        public ObjectId Id { get; set; }  // MongoDB’s internal primary key
        public required string SessionKey { get; set; }
        public required string UserId { get; set; }
        public long ExpirationTimestamp { get; set; } // 7 days validity
    }
}
