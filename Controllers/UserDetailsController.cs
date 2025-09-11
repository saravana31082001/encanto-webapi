using EncantoWebAPI.Accessors;
using EncantoWebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EncantoWebAPI.Controllers
{
    [Route("")]
    [ApiController]
    public class UserDetailsController : ControllerBase
    {

        [HttpGet("profileinfo")]
        public async Task<ActionResult<UserProfile>> GetProfileDetails()
        {
            var userDetailsManager = new Managers.UserDetailsManager();

            // Retrieve session key from context (middleware)
            var sessionKey = HttpContext.Items["SessionKey"] as string;

            try
            {
                if (sessionKey != null)
                {
                    var userIdFromSession = await userDetailsManager.GetUserIdFromSessionDetails(sessionKey);
                    var profileDetails = await userDetailsManager.GetProfileDetailsFromUserId(userIdFromSession);
                    return Ok(profileDetails); //returns profileDetails
                }
                else
                {
                    return BadRequest("Session key not found.");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
