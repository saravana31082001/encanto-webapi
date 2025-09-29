using EncantoWebAPI.Models;
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

        public async Task CreateNewEvent(EventDetails newEvent)
        {
            await _db.Events.InsertOneAsync(newEvent);
        }

        public async Task<List<EventDetails>> GetAllUpcomingEvents()
        {
            var filter = Builders<EventDetails>.Filter.Eq(e => e.Active, 1);

            var activeEvents = await _db.Events
                .Find(filter)
                .ToListAsync();

            return activeEvents;
        }


        public async Task<EventFeedback> GetFeedbacksByEventId(string eventId)
        {
            var filter = Builders<EventFeedback>.Filter.Eq(e => e.EventId, eventId);
            return await _db.EventFeedbacks.Find(filter).FirstOrDefaultAsync();
        }
    }
}
