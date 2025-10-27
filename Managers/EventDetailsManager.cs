﻿using EncantoWebAPI.Accessors;
using EncantoWebAPI.Models;
using EncantoWebAPI.Models.Events;
using EncantoWebAPI.Models.Events.Requests;
using Microsoft.Extensions.Hosting;
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

            string organizerDesignationString = string.Empty;

            if (!string.IsNullOrWhiteSpace(hostDetails.OccupationId))
            {
                var hostOccupationDetails = await userDetailsAccessor.GetOccupationDetails(hostDetails.OccupationId);
                
                var designationParts = new[] { hostOccupationDetails?.Designation, hostOccupationDetails?.OrganizationName }
                .Where(s => !string.IsNullOrWhiteSpace(s));

                organizerDesignationString = string.Join(", ", designationParts);
            }

            var organizerDetails = new OrganizerDetails
            {
                OrganizerId = hostDetails.UserId,
                OrganizerName = hostDetails.Name,
                BackgroundColour = hostDetails.BackgroundColour,
                ForegroundColour = hostDetails.ForegroundColour,
                OrganizerDesignation = organizerDesignationString
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
                OrganizerDetails = organizerDetails,
                MeetingLink = newEventRequest.MeetingLink,
                StartTimestamp = newEventRequest.StartTimestamp,
                EndTimestamp = newEventRequest.EndTimestamp,
                CreatedTimestamp = newEventRequest.CreatedTimestamp,
                UpdatedTimestamp = newEventRequest.CreatedTimestamp,
                IsPrivate = newEventRequest.IsPrivate,
                Participants = [],
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
                int totalParticipantsCount = eventDetails.TotalRegisteredParticipants;
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
                    totalParticipantsCount += 1;
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

                await eventDetailsAccessor.ApplyForUpcomingEvent(participantDetails, eventDetails.EventId, totalParticipantsCount);
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

        public async Task<List<EventDetails>> GetMyRegisteredEvents(string guestId)
        {
            var eventDetailsAccessor = new EventDetailsAccessor();
            var events = await eventDetailsAccessor.GetMyRegisteredEvents(guestId);
            return events;
        }

        public async Task<List<EventDetails>> GetMyPastAttendedEvents(string guestId)
        {
            var eventDetailsAccessor = new EventDetailsAccessor();
            var events = await eventDetailsAccessor.GetMyPastAttendedEvents(guestId);
            return events;
        }

        public async Task UpdateEventPendingRequest(string eventId, string participantId, bool isParticipantAccepted)
        {
            var eventDetailsAccessor = new EventDetailsAccessor();
            await eventDetailsAccessor.UpdateEventPendingRequest(eventId, participantId, isParticipantAccepted);
        }

        public async Task<List<PrivateEventRequestPreview>> GetAllPendingRequests(string hostId)
        {
            var currentTimestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();

            var pendingRequests = new List<PrivateEventRequestPreview>();
            var eventDetailsAccessor = new EventDetailsAccessor();
            var events = await eventDetailsAccessor.GetMyUpcomingHostedEvents(hostId);

            var upcomingEvents = events.Where(e => e.StartTimestamp > currentTimestamp);

            foreach (var Event in upcomingEvents)
            {
                if (Event.Is_accepting_participants == true && Event.Participants != null)
                {
                    var pendingParticipantsInThisEvent = Event.Participants
                        .Where(p => p.RegistrationStatus == 0);

                    foreach (var participant in pendingParticipantsInThisEvent)
                    {
                        PrivateEventRequestPreview pendingRequest = new()
                        {
                            EventId = Event.EventId,
                            EventName = Event.Title,
                            StartTimestamp = Event.StartTimestamp,
                            EndTimestamp = Event.EndTimestamp,
                            ParticipantDetails = participant,
                            ParticipantRequestTimestamp = participant.UpdatedTimestamp
                        };

                        pendingRequests.Add(pendingRequest);
                    }
                }
            }

            var sortedPendingRequests = pendingRequests
                .OrderByDescending(r => r.ParticipantRequestTimestamp)
                .ToList();

            return sortedPendingRequests;
        }

        public async Task UpdateEventActiveStatus(string eventId, int eventStatus)
        {
            var eventDetailsAccessor = new EventDetailsAccessor();
            var eventDetails = await eventDetailsAccessor.GetEventDetailsFromEventId(eventId);

            if (eventDetails.Active == eventStatus)
            {
                throw new InvalidOperationException($"Event's Active Status is already {eventStatus}");
            }

            await eventDetailsAccessor.UpdateEventActiveStatus(eventId, eventStatus);
        }
    }
}
