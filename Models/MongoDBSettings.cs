namespace EncantoWebAPI.Models
{
    public class MongoDBSettings
    {
        public required string ConnectionURI { get; set; }
        public required string DatabaseName { get; set; }
        public required string UsersCollectionName { get; set; }
        public required string AddressCollectionName { get; set; }
        public required string OccupationDetailsCollectionName { get; set; }
        public required string LoginCredentialsCollectionName { get; set; }
        public required string SessionsCollectionName { get; set; }
        public required string EventsCollectionName { get; set; }

    }
}

//public required string EventsCollectionName { get; set; }