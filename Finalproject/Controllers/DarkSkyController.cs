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

            var response = await client.GetAsync($"{_darkSkyKey}/{TempData["lat"]},{TempData["lng"]},{TempData["day"]}");
            var result = await response.Content.ReadAsAsync<DarkSky>();

            var responseOneYear = await client.GetAsync($"{_darkSkyKey}/{TempData["lat"]},{TempData["lng"]},{ TempData["yearPast"]}");
            var resultOneYear = await responseOneYear.Content.ReadAsAsync<DarkSky>();

            var responseFiveYears = await client.GetAsync($"{_darkSkyKey}/{TempData["lat"]},{TempData["lng"]},{TempData["yearsPast"]}");
            var resultFiveYears = await responseFiveYears.Content.ReadAsAsync<DarkSky>();
            TempData.Keep("day");
            TempData.Keep("yearPast");
            TempData.Keep("yearsPast");
            TempData.Keep("lat");
            TempData.Keep("lng");

            ViewBag.oneYearTemp = resultOneYear.currently.temperature;
            ViewBag.oneYearHumid = resultOneYear.currently.humidity;
            ViewBag.oneYearWind = resultOneYear.currently.windSpeed;
            ViewBag.oneYearPrecip = resultOneYear.currently.precipProbability;
            ViewBag.oneYearSumm = resultOneYear.currently.summary;

            ViewBag.fiveYearTemp = resultFiveYears.currently.temperature;
            ViewBag.fiveYearHumid = resultFiveYears.currently.humidity;
            ViewBag.fiveYearWind = resultFiveYears.currently.windSpeed;
            ViewBag.fiveYearPrecip = resultFiveYears.currently.precipProbability;
            ViewBag.fiveYearSumm = resultFiveYears.currently.summary;




            return View(result);
        }

    }
}