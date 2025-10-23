using EncantoWebAPI.Accessors;
using EncantoWebAPI.Models;
using EncantoWebAPI.Models.Events;
using EncantoWebAPI.Models.Events.Requests;
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

        public async Task<EventDetails> CreateNewEvent(CreateEventRequest newEventRequest)
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

            return newEvent;
        }

        public static string GenerateEventId(CreateEventRequest newEventRequest)
        {
            string eventType = newEventRequest.IsPrivate ? "Priv" : "Pub";
            string shortEventName = Regex.Replace(newEventRequest.Title, "[^a-zA-Z0-9]", ""); //remove whitespaces
            shortEventName = shortEventName.Length <= 7 ? shortEventName : shortEventName.Substring(0, 7);
            
            return $"Event_{eventType}_{newEventRequest.CreatedTimestamp}_{shortEventName}";
        }

        public async Task ApplyForUpcomingEvent(EventApplicationRequest eventApplicationRequest)
        {
            var userDetailsManager = new Managers.UserDetailsManager();
            var eventDetailsAccessor = new EventDetailsAccessor();

            try
            {
                var profileDetails = await userDetailsManager.GetProfileDetailsForEventCreationFromUserId(eventApplicationRequest.UserId);
                var eventDetails = await eventDetailsAccessor.GetEventDetailsFromEventId(eventApplicationRequest.EventId);
                bool isParticipantAlreadyRegistered = CheckIfUserAlreadyRegisteredForTheEvent(profileDetails.UserId, eventDetails);
                
                bool isEventPrivate = eventDetails.IsPrivate;
                int registrationStatus;

                if (isParticipantAlreadyRegistered)
                {
                    throw new InvalidOperationException("Participant Already Registered for the Event.");
                }

                if (isEventPrivate) //private event
                {
                    registrationStatus = 0;
                }
                else //public event
                {
                    registrationStatus = 1;
                }

                ParticipantDetails participantDetails = new() 
                {
                    ParticipantId = profileDetails.UserId,
                    ParticipantName = profileDetails.Name,
                    BackgroundColour = profileDetails.BackgroundColour,
                    ForegroundColour = profileDetails.ForegroundColour,
                    RegistrationStatus = registrationStatus,
                    RegisteredTimestamp = eventApplicationRequest.UpdatedTimestamp,
                    UpdatedTimestamp = eventApplicationRequest.UpdatedTimestamp
                };

                await eventDetailsAccessor.ApplyForUpcomingEvent(participantDetails, eventDetails.EventId);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }

        public bool CheckIfUserAlreadyRegisteredForTheEvent(string userId, EventDetails eventDetails)
        {
            var participantsList = eventDetails.Participants;

            if (participantsList == null || participantsList.Count == 0)
            {
                return false;
            }

            foreach (var participant in participantsList)
            {
                if (participant.ParticipantId == userId)
                {
                    return true;
                }
            }

            return false;
        }

        public async Task<List<EventDetails>> GetMyUpcomingHostedEvents(string hostId)
        {
            var eventDetailsAccessor = new EventDetailsAccessor();
            var events = await eventDetailsAccessor.GetMyUpcomingHostedEvents(hostId);
            return events;
        }

        public async Task<List<EventDetails>> GetMyPastHostedEvents(string hostId)
        {
            var eventDetailsAccessor = new EventDetailsAccessor();
            var events = await eventDetailsAccessor.GetMyPastHostedEvents(hostId);
            return events;
        }
    }
}
