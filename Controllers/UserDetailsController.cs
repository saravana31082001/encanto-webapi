using EncantoWebAPI.Accessors;
using EncantoWebAPI.Models.Profiles;
using EncantoWebAPI.Models.Profiles.Requests;
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

        [HttpPut("update-user-name")]
        public async Task<ActionResult> UpdateProfileName([FromBody] UserNameUpdateRequest userNameUpdateRequest)
        {
            var userDetailsManager = new Managers.UserDetailsManager();
            try
            {
                if (userNameUpdateRequest != null)
                {
                    await userDetailsManager.UpdateProfileName(userNameUpdateRequest);
                    return Ok(userDetailsManager);
                }
                else
                {
                    return BadRequest("Invaild User name Request");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }

        [HttpPut("update-user-phone")]
        public async Task<ActionResult> UpdateProfilePhn([FromBody] UserPhnUpdateRequest userPhnUpdateRequest)
        {
            var userDetailsManager = new Managers.UserDetailsManager();
            try
            {
                if (userPhnUpdateRequest != null)
                {
                    await userDetailsManager.UpdateProfilePhn(userPhnUpdateRequest);
                    return Ok();
                }
                else
                {
                    return BadRequest("Invaild User Phone number Request");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }

        [HttpPut("update-user-gender")]
        public async Task<ActionResult> UpdateProfileGender([FromBody] UserGenderUpdateRequest userGenderUpdateRequest)
        {
            var userDetailsManager = new Managers.UserDetailsManager();
            try
            {
                if (userGenderUpdateRequest != null)
                {
                    await userDetailsManager.UpdateProfileGender(userGenderUpdateRequest);
                    return Ok();
                }
                else
                {
                    return BadRequest("Invaild User gender Request");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }

    }
}