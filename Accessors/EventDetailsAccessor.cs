using EncantoWebAPI.Models.Events;
using EncantoWebAPI.Models.Profiles;
using EncantoWebAPI.Models.Profiles.Requests;
using MongoDB.Driver;

namespace EncantoWebAPI.Accessors
{
    public class EventDetailsAccessor
    {
        private readonly MongoDBAccessor _db;

        public EventDetailsAccessor()
        {
            _db = new MongoDBAccessor();
        }

        public async Task CreateNewEvent(EventDetails newEvent, EventFeedback newEventFeedback)
        {
            await _db.Events.InsertOneAsync(newEvent);
            await _db.EventFeedbacks.InsertOneAsync(newEventFeedback);
        }

        public async Task<List<EventDetails>> GetAllUpcomingEvents()
        {
            var currentTimestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();

            var filter = Builders<EventDetails>.Filter.And(
                Builders<EventDetails>.Filter.Eq(e => e.Active, 1),
                Builders<EventDetails>.Filter.Gt(e => e.StartTimestamp, currentTimestamp));

            var activeEvents = await _db.Events
                .Find(filter)
                .ToListAsync();

            return activeEvents;
        }

        public async Task<EventDetails> GetEventDetailsFromEventId(string eventId)
        {
            var filter = Builders<EventDetails>.Filter.Eq(u => u.EventId, eventId);
            var eventDetails = await _db.Events.Find(filter).FirstOrDefaultAsync();
            return eventDetails;
        }

        public async Task<EventFeedback> GetFeedbacksByEventId(string eventId)
        {
            var filter = Builders<EventFeedback>.Filter.Eq(e => e.EventId, eventId);
            return await _db.EventFeedbacks.Find(filter).FirstOrDefaultAsync();
        }

        public async Task ApplyForUpcomingEvent(ParticipantDetails participantDetails, string eventId)
        {
            var filter = Builders<EventDetails>.Filter.Eq(u => u.EventId, eventId);
            var update = Builders<EventDetails>.Update
                .Push(u => u.Participants, participantDetails) // append to list
                .Set(u => u.UpdatedTimestamp, participantDetails.UpdatedTimestamp); // update timestamp

            var result = await _db.Events.UpdateOneAsync(filter, update);

            if (result.ModifiedCount == 0)
            {
                throw new Exception("Event not found or Participant Details not updated.");
            }
        }

        public async Task<List<EventDetails>> GetMyUpcomingHostedEvents(string hostId)
        {
            var filter = Builders<EventDetails>.Filter.And(
                Builders<EventDetails>.Filter.Eq(e => e.Active, 1),
                Builders<EventDetails>.Filter.Eq(e => e.OrganizerDetails.OrganizerId, hostId));

            var activeEvents = await _db.Events
                .Find(filter)
                .ToListAsync();

            return activeEvents;
        }

        public async Task<List<EventDetails>> GetMyPastHostedEvents(string hostId)
        {
            var filter = Builders<EventDetails>.Filter.And(
                Builders<EventDetails>.Filter.Eq(e => e.Active, 0),
                Builders<EventDetails>.Filter.Eq(e => e.OrganizerDetails.OrganizerId, hostId));

            var activeEvents = await _db.Events
                .Find(filter)
                .ToListAsync();

            return activeEvents;
        }

        public async Task<List<EventDetails>> GetMyCancelledHostedEvents(string hostId)
        {
            var filter = Builders<EventDetails>.Filter.And(
                Builders<EventDetails>.Filter.Eq(e => e.Active, -1),
                Builders<EventDetails>.Filter.Eq(e => e.OrganizerDetails.OrganizerId, hostId));

            var activeEvents = await _db.Events
                .Find(filter)
                .ToListAsync();

            return activeEvents;
        }
    }
}
