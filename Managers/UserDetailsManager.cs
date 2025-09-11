using EncantoWebAPI.Accessors;
using EncantoWebAPI.Models;
using System.Text.Json;

namespace EncantoWebAPI.Managers
{
    public class UserDetailsManager
    {

        public async Task<UserProfile> GetProfileDetailsFromUserId(string UserId)
        {
            var userDetailsAccessor = new UserDetailsAccessor();
            var profileDetails = await userDetailsAccessor.GetProfileDetails(UserId);

            if (profileDetails == null)
            {
                throw new InvalidOperationException($"User profile with ID '{UserId}' not found.");
            }

            return profileDetails;
        }

        public async Task<string> GetUserIdFromSessionDetails(string sessionKey)
        {
            var userDetailsAccessor = new UserDetailsAccessor();
            var sessionDetails = await userDetailsAccessor.GetSessionDetails(sessionKey);
            if (sessionDetails == null)
            {
                throw new InvalidOperationException($"Session not found.");
            }
            return sessionDetails.UserId;
        }

    }
}
