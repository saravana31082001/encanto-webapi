using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace EncantoWebAPI.Models.Profiles
{
    public class Address
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public required string AddressId { get; set; }
        public required string StreetAddress { get; set; } // House Number + Street Name
        public string? Landmark { get; set; } //"Near Sivakasi Bus Stand"
        public required string City { get; set; }
        public required string State { get; set; }
        public int PostalCode { get; set; }
        public required string Country { get; set; }
        public required string AddressType { get; set; } //"Home", "Work"
        public required long CreatedTimestamp { get; set; }
        public required long UpdatedTimestamp { get; set; }
    }
}
