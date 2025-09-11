using EncantoWebAPI.Models;
using MongoDB.Driver;

namespace EncantoWebAPI.Accessors
{
    public class AuthenticationAccessor
    {
        private readonly MongoDBAccessor _db;
        public AuthenticationAccessor()
        {
            _db = new MongoDBAccessor();
        }

        #region Login/Logout

        public async Task<string> LoginExistingUser(LoginRequest loginRequest)
        {
            var filter = Builders<LoginCredential>.Filter.Eq(x => x.Username, loginRequest.Email) &
                         Builders<LoginCredential>.Filter.Eq(x => x.PasswordHash, loginRequest.PasswordHash);

            var credential = await _db.LoginCredentials.Find(filter).FirstOrDefaultAsync();
            return credential.UserId;
        }

        public async Task StoreSessionKey(SessionDetails sessionDetails)
        {
            await _db.SessionDetails.InsertOneAsync(sessionDetails);
        }

        public async Task DeleteSessionKey(string sessionKey)
        {
            var filter = Builders<SessionDetails>.Filter.Eq(s => s.SessionKey, sessionKey);
            await _db.SessionDetails.DeleteOneAsync(filter);
        }

        #endregion

        #region Signup

        public async Task CreateNewUser(UserProfile newUser)
        {
            await _db.Users.InsertOneAsync(newUser);
        }

        public async Task CreateNewLoginCredential(LoginCredential newLoginCredential)
        {
            await _db.LoginCredentials.InsertOneAsync(newLoginCredential);
        }

        public async Task<List<string>> GetAllEmailsIdsAsync()
        {
            var projection = Builders<UserProfile>.Projection.Include(u => u.Email);

            var emailsIds = await _db.Users
                .Find(FilterDefinition<UserProfile>.Empty) // no filter = fetch all
                .Project(u => u.Email)                    // project only UserId
                .ToListAsync();

            return emailsIds;
        }

        #endregion
    }
}
