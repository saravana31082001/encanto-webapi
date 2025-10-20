namespace EncantoWebAPI.Models.Profiles.Requests
{
    public class UserAddressUpdateRequest
    {
        public required string UserId { get; set; }
        public string? AddressId { get; set; }
        public required string StreetAddress { get; set; } // House Number + Street Name
        public string? Landmark { get; set; } //"Near Sivakasi Bus Stand"
        public required string City { get; set; }
        public required string State { get; set; }
        public required string PostalCode { get; set; }
        public required string Country { get; set; }
        public required string AddressType { get; set; } //"Home", "Work"
        public required long UpdatedTimestamp { get; set; }
    }
}
