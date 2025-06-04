using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using dataBase.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Logging;

namespace dataBase.repository
{
    public class eventRepository
    {
        EventsContext eventLibery = new EventsContext();
        public int AddingNewEvent(String Name,DateTime StartDate, DateTime endtDate,int MaxRegistrations,String Location)
        {
            Event ev = new Event();
            try
            {
                ev.Name = Name;
                ev.StartDate = StartDate;
                ev.EndDate = endtDate;
                ev.MaxRegistrations = MaxRegistrations;
                ev.Location = Location;
                eventLibery.Add(ev);
                eventLibery.SaveChanges();
            }
            catch (Exception ex)
            {
                return -1;
            }
            return ev.Id;
        }
        public List<User> returnUsersOfEvent (int id)
        {
            return eventLibery.EventUsers.Include(eventUser => eventUser.UserRefNavigation)
                .Where(eventUser => eventUser.EventRef == id).Select(eventUser=> eventUser.UserRefNavigation)
                .ToList();
        }
        public int AddingNewUserToEvent(String name , DateOnly DateOfBirth, int eventId)
        {
            try
            {
                var eventCheck= eventLibery.Events
                    .Include(e => e.EventUsers)
                    .FirstOrDefault(e => e.Id == eventId);
                if (eventCheck == null)
                {
                    return -2;
                }
                if(eventCheck.EventUsers.Count>=eventCheck.MaxRegistrations)
                {
                    return -3;
                }
                User user = new User();
                user.Name = name;
                user.DateOfBirth = DateOfBirth;
                eventLibery.Users.Add(user);
                eventLibery.SaveChanges();
                EventUser ev = new EventUser(); ;
                ev.EventRef = eventId;
                ev.UserRef = user.Id;
                eventLibery.EventUsers.Add(ev);
                eventLibery.SaveChanges();
                return user.Id;
            }
            catch(Exception ex)
            {
                return -1;
            }
        }
        public  Event getEvent(int id)
        {
            return eventLibery.Events.Where(ev => ev.Id == id).FirstOrDefault();   
        }
        public int UpdatingEvent(String Name, DateTime StartDate, DateTime endtDate, int MaxRegistrations, String Location, int eventid)
        {
            try
            {
                Event eventForUpdate = eventLibery.Events
                .Include(e => e.EventUsers)
                .FirstOrDefault(e => e.Id == eventid);
                if (eventForUpdate == null)
                {
                    return -2;
                }
                eventForUpdate.StartDate = StartDate;
                eventForUpdate.EndDate = endtDate;
                eventForUpdate.Location = Location;
                eventForUpdate.Name = Name;
                eventForUpdate.MaxRegistrations = MaxRegistrations;
                eventLibery.SaveChanges ();
                return 1;
            }
            catch(Exception ex)
            {
                return -1;
            }

        }
        public int deleteEvent(int id)
        {
            try
            {
                Event eventToDelete = eventLibery.Events.Include(e => e.EventUsers)
            .FirstOrDefault(ev => ev.Id == id);


            if (eventToDelete == null)
                    return -2;
                eventLibery.EventUsers.RemoveRange(eventToDelete.EventUsers);
                eventLibery.Events.Remove(eventToDelete);
                eventLibery.SaveChanges();
                return 1; 
            }
            catch (Exception)
            {
                return -1;
            }
        }
        public List<Event> getEventsInSecedual(DateTime StartDate, DateTime endtDate) 
        {
            return eventLibery.Events.Where(ev => ev.StartDate >= StartDate && ev.EndDate <= endtDate).ToList();
        }
        public string getWeatherOfEvent(int eventId)
        {
            String place= eventLibery.Events.Where(ev=> ev.Id== eventId).Select(ev=> ev.Location).FirstOrDefault();
            return place;
        }
    }
    

}
