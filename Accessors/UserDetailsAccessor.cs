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
        public async Task UpdateProfileName(UserNameUPRequest userNameUPRequest)
        {
            var collection = _db.GetCollection<UserProfile>("UserProfiles");

            var filter = Builders<UserProfile>.Filter.Eq(u => u.UserId, userNameUPRequest.UserId);
            var update = Builders<UserProfile>.Update
                .Set(u => u.Name, userNameUPRequest.Name)
                .Set(u => u.UpdatedTimestamp, userNameUPRequest.UpdatedTimestamp);

            var result = await collection.UpdateOneAsync(filter, update);

            if (result.ModifiedCount == 0)
                throw new Exception("User not found or name not updated.");
        }

        // ✅ Update Phone Number
        public async Task UpdateProfilePhn(UserPhnUpdateRequest userPhnUpdateRequest)
        {
            var collection = _db.GetCollection<UserProfile>("UserProfiles");

            var filter = Builders<UserProfile>.Filter.Eq(u => u.UserId, userPhnUpdateRequest.UserId);
            var update = Builders<UserProfile>.Update
                .Set(u => u.PhoneNumber, userPhnUpdateRequest.PhoneNumber)
                .Set(u => u.UpdatedTimestamp, userPhnUpdateRequest.UpdatedTimestamp);

            var result = await collection.UpdateOneAsync(filter, update);

            if (result.ModifiedCount == 0)
                throw new Exception("User not found or phone number not updated.");
        }

        // ✅ Update Gender
        public async Task UpdateProfileGender(UserGenderUpdateRequest userGenderUpdateRequest)
        {
            var collection = _db.GetCollection<UserProfile>("UserProfiles");

            var filter = Builders<UserProfile>.Filter.Eq(u => u.UserId, userGenderUpdateRequest.UserId);
            var update = Builders<UserProfile>.Update
                .Set(u => u.Gender, userGenderUpdateRequest.Gender)
                .Set(u => u.UpdatedTimestamp, userGenderUpdateRequest.UpdatedTimestamp);

            var result = await collection.UpdateOneAsync(filter, update);

            if (result.ModifiedCount == 0)
                throw new Exception("User not found or gender not updated.");
        }


    }
}
