using EncantoWebAPI.Hubs;
using EncantoWebAPI.Models;
using EncantoWebAPI.Models.Notifications;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace EncantoWebAPI.Controllers
{
    [Route("")]
    [ApiController]
    public class EventDetailsController : ControllerBase
    {
        private readonly IHubContext<NotificationHub> _hubContext;

        public EventDetailsController(IHubContext<NotificationHub> hubContext)
        {
            _hubContext = hubContext;
        }

        [HttpPost("events/new")]
        public async Task<ActionResult> CreateNewEvent(CreateEventRequest newEventRequest)
        {
            var eventDetailsManager = new Managers.EventDetailsManager();
            try
            {
                var createdEvent = await eventDetailsManager.CreateNewEvent(newEventRequest);

                var broadcastMessage = new EventUpdateMessage
                {
                    Action = "create",
                    Event = createdEvent
                };

                await _hubContext.Clients.All.SendAsync("EventChanged", broadcastMessage);

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