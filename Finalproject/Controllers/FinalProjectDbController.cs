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
            location.Replace(" ", "+");
            var client = GetHttpClient();
            var response = await client.GetAsync($"maps/api/geocode/json?address=1600+Amphitheatre+Parkway,+Mountain+View,+CA&key={_googleApiKey}");
            var name = await response.Content.ReadAsAsync<Location>();
            return View(name);
        }
    }
}