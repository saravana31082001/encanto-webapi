using EncantoWebAPI.Hubs;
using EncantoWebAPI.Managers;
using EncantoWebAPI.Models.Events;
using EncantoWebAPI.Models.Events.Requests;
using EncantoWebAPI.Models.Notifications;
using EncantoWebAPI.Models.Profiles.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;

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
                if (newEventRequest != null)
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
                else
                {
                    return BadRequest("Invaild Event Creation Request");
                }
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


        [HttpGet("events/hosted-upcoming")]
        public async Task<ActionResult<List<EventDetails>>> GetMyUpcomingHostedEvents(string hostId)
        {
            var eventDetailsManager = new Managers.EventDetailsManager();
            try
            {
                var events = await eventDetailsManager.GetMyUpcomingHostedEvents(hostId);
                return Ok(events);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("events/hosted-past")]
        public async Task<ActionResult<List<EventDetails>>> GetMyPastHostedEvents(string hostId)
        {
            var eventDetailsManager = new Managers.EventDetailsManager();
            try
            {
                var events = await eventDetailsManager.GetMyPastHostedEvents(hostId);
                return Ok(events);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("events/apply")]
        public async Task<ActionResult> ApplyForUpcomingEvent([FromBody] EventApplicationRequest eventApplicationRequest)
        {
            var eventDetailsManager = new Managers.EventDetailsManager();
            try
            {
                if (eventApplicationRequest != null)
                {
                    await eventDetailsManager.ApplyForUpcomingEvent(eventApplicationRequest);
                    return Ok();
                }
                else
                {
                    return BadRequest("Invaild Event Application Request");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}
