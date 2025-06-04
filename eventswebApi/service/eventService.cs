using dataBase.Models;
using dataBase.repository;
namespace eventswebApi.service
{
    public class eventService
    {
        private readonly eventRepository _eventRepository;
        public eventService(eventRepository purchaseRepository)
        {
            _eventRepository = purchaseRepository;  
        }

            public int AddingNewEvent(String Name, DateTime StartDate, DateTime endtDate, int MaxRegistrations, String Location)
            { 
             return _eventRepository.AddingNewEvent(Name, StartDate, endtDate, MaxRegistrations, Location);
            }
        public List<User> returnUsersOfEvent(int id)
        {
            return _eventRepository.returnUsersOfEvent(id);
        }
        public int AddingNewUserToEvent(String name, DateOnly DateOfBirth, int eventId)
        {
            return (_eventRepository.AddingNewUserToEvent(name, DateOfBirth, eventId));
        }
        public Event getEvent(int id)
        {
            return _eventRepository.getEvent(id);
        }
        public int UpdatingEvent(String Name, DateTime StartDate, DateTime endtDate, int MaxRegistrations, String Location, int eventid)
        {
            return _eventRepository.UpdatingEvent(Name,StartDate, endtDate,MaxRegistrations, Location,eventid);
        }
        public int deleteEvent(int id)
        {
            return _eventRepository.deleteEvent(id);
        }
        public List<Event> getEventsInSecedual(DateTime StartDate, DateTime endtDate)
        {
            return _eventRepository.getEventsInSecedual(StartDate, endtDate);
        }
        public string getWeatherOfEvent(int eventId)
        {
            return _eventRepository.getWeatherOfEvent(eventId);
        }

    }
}
