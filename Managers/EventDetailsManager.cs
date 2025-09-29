using EncantoWebAPI.Accessors;
using EncantoWebAPI.Models;
using System.Text.RegularExpressions;

namespace EncantoWebAPI.Managers
{
    public class EventDetailsManager
    {
        public async Task<List<EventDetails>> GetAllUpcomingEvents()
        {
            var eventDetailsAccessor = new EventDetailsAccessor();
            var events = await eventDetailsAccessor.GetAllUpcomingEvents();
            return events;
        }

        public async Task CreateNewEvent(CreateEventRequest newEventRequest)
        {
            var eventId = GenerateEventId(newEventRequest);

            var userDetailsAccessor = new UserDetailsAccessor();
            var hostDetails = await userDetailsAccessor.GetProfileDetails(newEventRequest.OrganizerId);

            if (hostDetails == null)
            {
                throw new InvalidOperationException("Unable to retreive Organizer Details");
            }

            var organizerDetails = new OrganizerDetails
            {
                EventId = eventId,
                OrganizerId = hostDetails.UserId,
                OrganizerName = hostDetails.Name,
                BackgroundColour = hostDetails.BackgroundColour,
                ForegroundColour = hostDetails.ForegroundColour

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
            await eventDetailsAccessor.CreateNewEvent(newEvent, eventFeedback);
        }

        public static string GenerateEventId(CreateEventRequest newEventRequest)
        {
            string eventType = newEventRequest.IsPrivate ? "Priv" : "Pub";
            string shortEventName = Regex.Replace(newEventRequest.Title, "[^a-zA-Z0-9]", ""); //remove whitespaces
            shortEventName = shortEventName.Length <= 7 ? shortEventName : shortEventName.Substring(0, 7);
            
            return $"Event_{eventType}_{newEventRequest.CreatedTimestamp}_{shortEventName}";
        }
    }
}
