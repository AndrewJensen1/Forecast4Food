using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Finalproject.Models;
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

        public async Task<IActionResult> Map(string location)
        {
            var client = GetHttpClient();
            if (!string.IsNullOrWhiteSpace(location))
            {
                location.Trim().Replace(" ", "+");
            }
            var response = await client.GetAsync($"maps/api/geocode/json?address={location}&key={_googleApiKey}");
            var name = await response.Content.ReadAsAsync<Location>();
            TempData["lat"] = name.results[0].geometry.location.lat;
            TempData["lng"] = name.results[0].geometry.location.lng;
            return RedirectToAction("Something", "DarkSky");
           // return RedirectToAction("GetWeather", "DarkSky", new {latitude = name.results[0].geometry.location.lat, longitude = name.results[0].geometry.location.lng });
        }


        
        
    }
}