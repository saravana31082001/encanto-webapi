namespace EncantoWebAPI.Models.Notifications
{
    public class EventUpdateMessage
    {
        public required string Action { get; set; }
        public required EventDetails Event {  get; set; }
    }
}
