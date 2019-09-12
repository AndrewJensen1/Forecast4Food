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
    public class XYZController : Controller
    {
        private readonly FinalProjectDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly string _xyzKey;
        
        public XYZController(FinalProjectDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
            _xyzKey = _configuration.GetSection("AppConfiguration")["XYZMenusAPIKey"];
        }

        public static HttpClient GetHttpClient()
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("https://us-restaurant-menus.p.rapidapi.com");
            return client;
        }

        [HttpGet]
        public async Task<IActionResult> RestaurantSearch()
        {
            var client = GetHttpClient();
            client.DefaultRequestHeaders.Add("X-RapidAPI-Key", $"{_xyzKey}");

            var lat = TempData["lat"].ToString();
            TempData.Keep("lat");
            var lon = TempData["lng"].ToString();
            TempData.Keep("lng");


            var response = await client.GetAsync($"/restaurants/search?lat={lat}&lon={lon}&distance=1");
            var result = await response.Content.ReadAsAsync<XYZMenu>();

            return View(result);
        }

        [HttpPost]
        public async Task<IActionResult> RestaurantSearch(int num)
        {
            var client = GetHttpClient();
            client.DefaultRequestHeaders.Add("X-RapidAPI-Key", $"{_xyzKey}");

            var lat = TempData["lat"].ToString();
            TempData.Keep("lat");
            var lon = TempData["lng"].ToString();
            TempData.Keep("lng");

            var response = await client.GetAsync($"/restaurants/search?lat={lat}&lon={lon}&distance={num}");
            var result = await response.Content.ReadAsAsync<XYZMenu>();

           
            return View(result);
        }

    }
}