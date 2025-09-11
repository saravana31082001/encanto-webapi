using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace EncantoWebAPI.Models
{
    public class OccupationDetails
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public required string OccupationId { get; set; }
        public required string Designation { get; set; } //"Software Engineer", "Doctor", "Tutor"
        public required string IndustryDomain { get; set; } //"IT", "Healthcare", "Finance", "Education"
        public required string OrganizationName { get; set; } //"Zoho", "Appollo Hospitals", "Aakash Coaching Center"
        public string? EmploymentType { get; set; } //"Full-Time", "Part-Time", "Self-Employed", "Freelancer"
        public string? AddressId { get; set; }
        public Address? JobLocation { get; set; }
        public required string WorkEmail { get; set; }
        public long? WorkPhoneNumber { get; set; }
        public required long CreatedTimestamp { get; set; }
        public required long UpdatedTimestamp { get; set; }
    }
}
