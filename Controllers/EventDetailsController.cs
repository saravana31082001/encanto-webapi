using EncantoWebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EncantoWebAPI.Controllers
{
    [Route("")]
    [ApiController]
    public class EventDetailsController : ControllerBase
    {
        [HttpPost("events/new")]
        public async Task<ActionResult> CreateNewEvent(CreateEventRequest newEventRequest)
        {
            var eventDetailsManager = new Managers.EventDetailsManager();
            try
            {
                await eventDetailsManager.CreateNewEvent(newEventRequest);
                return Ok("Event created successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("events/browse-upcoming")]
        public async Task<ActionResult<List<EventDetails>>> GetAllUpcomingEvents()
        {
            var eventDetailsManager = new Managers.EventDetailsManager();
            try
            {
                var events = await eventDetailsManager.GetAllUpcomingEvents();
                return Ok(events);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

//== host ==
//createNewEvent()
//getMyUpcomingEvents()
//getMyPastEvents()


//== guest ==
//GetAllUpcomingEvents()
//getEventDetails(eventId)
//getMyRegisteredEvents()
//applyForEvent()
//viewMyParticipatedEvents()
//submitEventFeedback()