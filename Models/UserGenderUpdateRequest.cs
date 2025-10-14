using Microsoft.AspNetCore.SignalR;

namespace EncantoWebAPI.Models
{
    public class UserGenderUpdateRequest
    {
        public required string UserId { get; set; }
        public string? Gender { get; set; }
        public required long UpdatedTimestamp { get; set; }
    }
}
