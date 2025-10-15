using Microsoft.AspNetCore.SignalR;

namespace EncantoWebAPI.Models.Profiles.Requests
{
    public class UserGenderUpdateRequest
    {
        public required string UserId { get; set; }
        public required string Gender { get; set; }
        public required long UpdatedTimestamp { get; set; }
    }
}
