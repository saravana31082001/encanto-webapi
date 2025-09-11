using EncantoWebAPI.Models;
using MongoDB.Driver;
using System.Text.Json;

namespace EncantoWebAPI.Accessors
{
    public class UserDetailsAccessor
    {
        private readonly MongoDBAccessor _db;
        public UserDetailsAccessor()
        {
            _db = new MongoDBAccessor();
        }

        public async Task<UserProfile?> GetProfileDetails(string userId)
        {
            var filter = Builders<UserProfile>.Filter.Eq(u => u.UserId, userId);
            var user = await _db.Users.Find(filter).FirstOrDefaultAsync();
            return user;
        }

        public async Task<SessionDetails?> GetSessionDetails(string sessionKey)
        {
            var filter = Builders<SessionDetails>.Filter.Eq(s => s.SessionKey, sessionKey);
            var session = await _db.SessionDetails.Find(filter).FirstOrDefaultAsync();
            return session;
        }

    }
}
