using dataBase.Models;
using dataBase.repository;
using eventswebApi.DTO;
using eventswebApi.service;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System.ComponentModel;
using System.Net;

namespace eventswebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class eventController : ControllerBase
    {
        private readonly eventService _eventService;
        private readonly IMemoryCache _memoryCache;
        public eventController(eventService eventService, IMemoryCache memoryCache)
        {
            _eventService = eventService;
            _memoryCache = memoryCache;
        }
      

        [HttpPost]
       public ActionResult<String> AddingNewEvent([FromBody] eventDTO ev)
        {
            int id = _eventService.AddingNewEvent(ev.Name, ev.StartDate, ev.EndDate, ev.MaxRegistrations, ev.Location);
            if (id!=-1)
            {
                return Ok("the adding is succesfull and its id:"+id);
            }
            return Ok("the adding is not succesfull");
        }

        [HttpGet]
        [Route ("{id}/registration")]
        public ActionResult<List<User>> returnUsersOfEvent(int id)
        {
           return Ok(_eventService.returnUsersOfEvent(id));
        }


        [HttpPost]
        [Route("{id}/registration")]

        public ActionResult<String> AddingNewUserToEvent([FromBody] newUserDTO user, int id)
        {
            int userId = _eventService.AddingNewUserToEvent(user.Name,user.DateOfBirth, id);
            if(userId == -1)
            {
                return Ok("EROR EROR user didnt added");
            }
            else
            {
                if (userId == -2)
                    return Ok("EROR EROR Event wasnt found");
                else
                    if (userId == -3)
                    return Ok("The event with the id of " + id + " is already in max capasity");
                else
                    return Ok("the user is added his id is " + userId);
            }
        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult<Event> getEvent(int id)
        {
            return Ok(_eventService.getEvent(id));
        }


        [HttpPut]
        [Route("{id}")]
        public ActionResult<String> UpdatingEvent( [FromBody] eventDTO ev , int id)
        {
            int happand = _eventService.UpdatingEvent(ev.Name, ev.StartDate, ev.EndDate, ev.MaxRegistrations, ev.Location, id);
            if (happand == -2)
                return Ok("didnt found your Event");
            else
                if (happand == -1)
                return Ok("EROR EROR somthing happand could not update Event");
            else
                return Ok("the event " + id + " was succesfuly update");
        }
        [HttpDelete]
        [Route("{id}")]
        public ActionResult<String> deleteEvent( int id)
        {
            int happand = _eventService.deleteEvent( id);
            if (happand == -2)
                return Ok("didnt found your Event");
            else
                if (happand == -1)
                return Ok("EROR EROR somthing happand could not delete Event");
            else
                return Ok("the event " + id + " was succesfuly deleted");
        }

        [HttpGet("/schedule")]
        public ActionResult<List<Event>> getSchedule(DateTime StartDate, DateTime endDate)
        {
            return Ok(_eventService.getEventsInSecedual(StartDate, endDate));
        }
        [HttpGet]
        [Route ("{id}/weather")]
        public ActionResult<String> getWeather (int id)
        {
            String place = _eventService.getWeatherOfEvent(id);
            string apiKey = "7a9e79568f9143a4b0162449250406";
            if (place != null)
            {
                string url = $"http://api.weatherapi.com/v1/forecast.json?key={apiKey}&q={place}";
                string json = (new WebClient()).DownloadString(url);
                _memoryCache.Set("weather", json);
                return Ok(json);
            }
            else
                return Ok("place not found");
           
        }

    }
}
