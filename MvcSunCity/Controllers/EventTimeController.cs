using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MvcSunCity;
using MvcSunCity.Data;

namespace MvcSunCity.Controllers
{
    public static class SunService
    {
        private static readonly HttpClient client = new HttpClient();
        
        public static async System.Threading.Tasks.Task<string> GetDataFromServer(string lattitude, string longitude, string date)
        {
            StringBuilder sb = new StringBuilder("https://api.sunrise-sunset.org/json?")
            .Append("lat=" + lattitude)
            .Append("&lng=" + longitude)
            .Append("&date=" + date);

            UriBuilder builder = new UriBuilder(sb.ToString());
            client.DefaultRequestHeaders.Accept.Clear();
            var result = client.GetStringAsync(builder.Uri);
            var msg = await result;
            return msg ;
        }
    }
    
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