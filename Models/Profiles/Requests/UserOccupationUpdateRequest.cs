namespace EncantoWebAPI.Models.Profiles.Requests
{
    public class UserOccupationUpdateRequest
    {
        public required string UserId { get; set; }
        public string? OccupationId { get; set; }
        public string? Designation { get; set; } //"Software Engineer", "Doctor", "Tutor"
        public string? IndustryDomain { get; set; } //"IT", "Healthcare", "Finance", "Education"
        public string? OrganizationName { get; set; } //"Zoho", "Appollo Hospitals", "Aakash Coaching Center"
        public string? EmploymentType { get; set; } //"Full-Time", "Part-Time", "Self-Employed", "Freelancer"
        public string? AddressId { get; set; }
        public UserAddressUpdateRequest? JobLocation { get; set; }
        public string? WorkEmail { get; set; }
        public long? WorkPhoneNumber { get; set; }
        public required long UpdatedTimestamp { get; set; }
    }
}
