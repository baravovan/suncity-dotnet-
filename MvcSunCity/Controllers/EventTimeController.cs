using System;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MvcSunCity.Data;
using MvcSunCity.Services;

namespace MvcSunCity.Controllers
{
    [Authorize]
    public class EventTimeController : Controller
    {
        private readonly MvcSunCityContext _context;

        public EventTimeController(MvcSunCityContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Search([Bind("CityName,Date,Action")] EventTime eventTime)
        {
            if (String.IsNullOrEmpty(eventTime.Date)) eventTime.Date = "today";

            string response = GetDataByDateAndCityNames(eventTime.Date, eventTime.CityName).Result;
            Console.WriteLine("Response: {0}", response);
            CityInfo cityInfo = JsonSerializer.Deserialize<CityInfoResponse>(response).results;
            Console.WriteLine("CityInfo sunset= {0}, sunrise= {1}", cityInfo.sunset, cityInfo.sunrise);
            switch (eventTime.Action) {
                case "sunset":  
                    eventTime.Sunset = cityInfo.sunset;
                    break;
                case "sunrise": 
                    eventTime.Sunrise = cityInfo.sunrise;
                    break;
                default: 
                    eventTime.Sunset = cityInfo.sunset;
                    eventTime.Sunrise = cityInfo.sunrise;
                    break;
                }
            return View(eventTime);
        }

        public async Task<string> GetDataByDateAndCityNames(string date, string cityName)
        {
            var city = await _context.City
                .FirstOrDefaultAsync(m => m.Name == cityName);
            return SunService.GetDataFromServer(city.Lattitude.ToString(), city.Longitude.ToString(), date).Result.ToString();
        }
        public RedirectToActionResult Redirect()
        {
            return RedirectToAction(actionName: "Index", controllerName: "Cities");
        }
    }
}