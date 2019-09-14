using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Finalproject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Finalproject.Controllers
{ 
    
    public class FinalProjectDbController : Controller
    {
        private readonly FinalProjectDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly string _googleApiKey;

        public FinalProjectDbController(FinalProjectDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
            _googleApiKey = _configuration.GetSection("AppConfiguration")["GoogleAPIKey"];
        }
        
        public IActionResult Index()
        {
            return View();
        }
        
        public static HttpClient GetHttpClient()
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("https://maps.google.com");
            return client;
        }

        public async Task<IActionResult> Map(string location, DateTime dayy)
        {
            TempData["yearPast"] = dayy.AddYears(-1).ToString("yyyy-MM-ddThh:mm:ss");
            TempData["yearsPast"] = dayy.AddYears(-5).ToString("yyyy-MM-ddThh:mm:ss");
            var client = GetHttpClient();
            if (!string.IsNullOrWhiteSpace(location))
            {
                location.Trim().Replace(" ", "+");
            }
            TempData["day"] = dayy.ToString("yyyy-MM-ddThh:mm:ss");
            var response = await client.GetAsync($"maps/api/geocode/json?address={location}&key={_googleApiKey}");
            var name = await response.Content.ReadAsAsync<Location>();

            TempData["lat"] = name.results[0].geometry.location.lat;
            TempData["lng"] = name.results[0].geometry.location.lng;

            return RedirectToAction("WeatherView", "DarkSky");
        }
        [Authorize]
        public IActionResult AddFavorite(Datum datum, DSDatum1 weather)
        {
            AspNetUsers thisUser = _context.AspNetUsers.Where(u => u.UserName == User.Identity.Name).First();
            UserPlanner userPlanner = new UserPlanner();

            if (ModelState.IsValid)
            {
                userPlanner.UserId = thisUser.Id;
                userPlanner.Restaurants = datum.restaurant_name;
                userPlanner.Dates = null;                              //<<<<<<<<
                userPlanner.Notes = null;                             //<<<<<<<<<This is what gets added to userPlanner 
                userPlanner.Weather = weather.summary;               //<<<<<<<<<<when user saves favorite. Need to 
                userPlanner.Events = null;                          //<<<<<<<<<<<fill in fields, can add more parameters if needed

                _context.UserPlanner.Add(userPlanner);
                _context.SaveChanges();
                return RedirectToAction("UserPlanner");
            }

            return RedirectToAction("RestaurantSearch", "XYZ");
        }
        [Authorize]
        public IActionResult UserPlanner()
        {
            AspNetUsers thisUser = _context.AspNetUsers.Where(u => u.UserName == User.Identity.Name).First();
            List<UserPlanner> userPlanner = _context.UserPlanner.Where(u => u.UserId == thisUser.Id).ToList();
            return View(userPlanner);
        }
        [Authorize]
        public IActionResult RemovePlanner(UserPlanner userPlanner)
        {
            if (userPlanner != null)
            {
                _context.UserPlanner.Remove(userPlanner);
                _context.SaveChanges();
                return RedirectToAction("UserPlanner");
            }
            return RedirectToAction("UserPlanner");
        }

    }
}