using EncantoWebAPI.Accessors;
using EncantoWebAPI.Models;

namespace EncantoWebAPI.Managers
{
    public class EventDetailsManager
    {
        public async Task CreateNewEvent(CreateEventRequest newEventRequest)
        {
            var eventId = GenerateEventId(newEventRequest);

            var organizerDetails = new OrganizerDetails
            {
                EventId = eventId,
                OrganizerId = newEventRequest.OrganizerId,
                OrganizerName = newEventRequest.OrganizerName,
                BackgroundColour = newEventRequest.OrganizerBackground,
                ForegroundColour = newEventRequest.OrganizerForeground

            };

            var eventFeedback = new EventFeedback
            {
                EventId = eventId,
                EnableRating = newEventRequest.EnableRating,
                EnableComments = newEventRequest.EnableComments,
                TotalRatings = 0,
            };


            var newEvent = new EventDetails
            {
                EventId = eventId,
                Title = newEventRequest.Title,
                Description = newEventRequest.Description,
                SendLinkNotificationAt = newEventRequest.SendLinkNotificationAt,
                EventFeedback = eventFeedback,
                OrganizerDetails = organizerDetails,
                StartTimestamp = newEventRequest.StartTimestamp,
                EndTimestamp = newEventRequest.EndTimestamp,
                CreatedTimestamp = newEventRequest.CreatedTimestamp,
                UpdatedTimestamp = newEventRequest.CreatedTimestamp,
                IsPrivate = newEventRequest.IsPrivate,
                Is_accepting_participants = true,
                TotalRegisteredParticipants = 0,
                Active = 1,
            };

            var eventDetailsAccessor = new EventDetailsAccessor();
            await eventDetailsAccessor.CreateNewEvent(newEvent);

        }

        public async Task<List<EventDetails>> GetAllUpcomingEvents()
        {
            var eventDetailsAccessor = new EventDetailsAccessor();
            var events = await eventDetailsAccessor.GetAllUpcomingEvents();
            return events;
        }

        public static string GenerateEventId(CreateEventRequest newEventRequest)
        {
            string eventType = newEventRequest.IsPrivate ? "Priv" : "Pub";
            string shortEventName = newEventRequest.Title.Length <= 7 ? newEventRequest.Title : newEventRequest.Title.Substring(0, 7);
            return $"Event_{eventType}_{newEventRequest.CreatedTimestamp}_{shortEventName}";
        }
    }
}
