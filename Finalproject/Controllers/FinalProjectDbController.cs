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
            if(location != null && dayy.Year != 0001)
            {

            TempData["yearPast"] = dayy.AddYears(-1).ToString("yyyy-MM-ddThh:mm:ss");
            TempData["yearsPast"] = dayy.AddYears(-5).ToString("yyyy-MM-ddThh:mm:ss");
            var client = GetHttpClient();
            if (!string.IsNullOrWhiteSpace(location))
            {
                location.Trim().Replace(" ", "+");
            }
            TempData["day"] = dayy.ToString("yyyy-MM-ddThh:mm:ss");
            TempData["dayy"] = dayy.ToString("yyyy-MM-dd");
            var response = await client.GetAsync($"maps/api/geocode/json?address={location}&key={_googleApiKey}");
            var name = await response.Content.ReadAsAsync<Location>();

            TempData["lat"] = name.results[0].geometry.location.lat;
            TempData["lng"] = name.results[0].geometry.location.lng;

            return RedirectToAction("WeatherView", "DarkSky");
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        [Authorize]
        public IActionResult AddFavorite(Datum datum)
        {
            AspNetUsers thisUser = _context.AspNetUsers.Where(u => u.UserName == User.Identity.Name).First();
            UserPlanner userPlanner = new UserPlanner();

            if (ModelState.IsValid)
            {
                userPlanner.UserId = thisUser.Id;
                userPlanner.Restaurants = datum.restaurant_name;
                userPlanner.Dates = TempData["dayy"].ToString();     
                userPlanner.Notes = null;                         
                userPlanner.Weather = TempData["weather"].ToString();    
                userPlanner.Events = TempData["weatherSum"].ToString();             

                _context.UserPlanner.Add(userPlanner);
                _context.SaveChanges();
                return RedirectToAction("UserPlanner", userPlanner);
            }

            return RedirectToAction("RestaurantSearch", "XYZ");
        }
        [Authorize]
        public IActionResult UserPlanner()
        {
            var thisUserName = User.Identity.Name;
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

        [HttpGet]
        public IActionResult EditNote(int Id)
        {
            UserPlanner found = _context.UserPlanner.Find(Id);
            if (found != null)
            {
                return View(found);
            }
            else
            {
                return RedirectToAction("UserPlanner");
            }
        }
        [HttpPost]
        public IActionResult EditNote(int Id, string note)
        {
            UserPlanner found = _context.UserPlanner.Find(Id);
            if (ModelState.IsValid && note != null)
            {
                found.Notes = note;

                _context.Entry(found).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                _context.UserPlanner.Update(found);
                _context.SaveChanges();
                return RedirectToAction("UserPlanner");
            }
            else
            {
                return RedirectToAction("UserPlanner");
            }
        }


        public IActionResult AddNote(UserPlanner users)
        {
            return View(users);
        }

        [HttpPost]
        public IActionResult AddNote(UserPlanner planner, string note)
        { 
            planner.Notes = note;

            _context.UserPlanner.Add(planner);
            _context.SaveChanges();

            return RedirectToAction("UserPlanner");
        }

        [HttpGet]
        public IActionResult UpdateNote(int Id)
        {
            UserPlanner found = _context.UserPlanner.Find(Id);
            if (found != null)
            {
                return View(found);
            }
            else
            {
                return RedirectToAction("UserPlanner");
            }
        }

        [HttpPost]
        public IActionResult UpdateNote(UserPlanner updatedNote)
        {
            UserPlanner found = _context.UserPlanner.Find(updatedNote.UserId);
            if (ModelState.IsValid && found != null)
            {
                found.UserId = updatedNote.UserId;
                found.Notes = updatedNote.Notes;

                _context.Entry(found).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                _context.Update(found);
                _context.SaveChanges();

                return RedirectToAction("UserPlanner");
            }
            return View("UpdateNote", found);
        }

    }
}