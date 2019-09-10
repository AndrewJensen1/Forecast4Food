using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Finalproject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace Finalproject.Controllers
{
    public class DarkSkyController : Controller
    {
        private readonly FinalProjectDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly string _darkSkyKey;

        public DarkSkyController(FinalProjectDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
            _darkSkyKey = _configuration.GetSection("AppConfiguration")["DarkSkyAPIKey"];
        }

        public static HttpClient GetHttpClient()
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("https://api.darksky.net/forecast/");
            return client;
        }

        public async Task<IActionResult> WeatherView()
        {
            var client = GetHttpClient();

            var latitude = TempData["lat"].ToString();
            var longitude = TempData["lng"].ToString();
            var response = await client.GetAsync($"{_darkSkyKey}/{latitude},{longitude}"); 
            var result = await response.Content.ReadAsAsync<DarkSky>();
            //var something = JsonConvert.DeserializeObject<DarkSky>(result.ToString());

            return View(result);
        }
    }
}