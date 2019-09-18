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
            TempData["weather"] = result.currently.temperature;
            TempData["weatherSum"] = result.currently.summary;

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
      
            decimal tempRating = GetTempRating(result.currently.temperature);
            decimal tempRating2 = GetTempRating(resultOneYear.currently.temperature);
            decimal tempRating3 = GetTempRating(resultFiveYears.currently.temperature);
            decimal windRating = GetWindRating(result.currently.windSpeed);
            decimal windRating2 = GetWindRating(resultOneYear.currently.windSpeed);
            decimal windRating3 = GetWindRating(resultFiveYears.currently.windSpeed);
            decimal sumRating = GetSummaryRating(result.currently.summary);
            decimal sumRating2 = GetSummaryRating(resultOneYear.currently.summary);
            decimal sumRating3 = GetSummaryRating(resultFiveYears.currently.summary);

            ViewBag.rating = GetRatingPercentage(tempRating, tempRating2, tempRating3, windRating, windRating2, windRating3, sumRating, sumRating2, sumRating3);

            return View(result);
        }

        public static decimal GetTempRating(decimal temperature)
        {
            if (temperature >= 80)
            {
                return (5);
            }
            else if(temperature < 80 && temperature >= 60)
            {
                return (4);
            }
            else if(temperature < 60 && temperature >= 50)
            {
                return (3);
            }
            else if(temperature < 50 && temperature >= 32)
            {
                return (2);
            }
            else if(temperature < 32 && temperature > 0)
            {
                return (1);
            }
            else
            {
                return (0);
            }
        }

        public static decimal GetWindRating(decimal windSpeed)
        {
            if(windSpeed >= 50)
            {
                return (0);
            }
            else if(windSpeed < 50 && windSpeed >= 40)
            {
                return (1);
            }
            else if(windSpeed < 40 && windSpeed >= 30)
            {
                return (2);
            }
            else if(windSpeed < 30 && windSpeed >= 20)
            {
                return (3);
            }
            else if(windSpeed <= 20 && windSpeed > 10)
            {
                return (4);
            }
            else
            {
                return (5);
            }
        }

        public static decimal GetSummaryRating(string summary)
        {
            if (summary.ToLower().Contains("clear"))
            {
                return (5);
            }
            else if (summary.ToLower().Contains("cloudy"))
            {
                return (4);
            }
            else if (summary.ToLower().Contains("rain"))
            {
                return (2);
            }
            else if (summary.ToLower().Contains("snow"))
            {
                return (1);
            }
            else 
            {
                return (1);
            }
        }
        public static decimal GetRatingPercentage(decimal temp,decimal temp2, decimal temp3, decimal wind, decimal wind2, decimal wind3, decimal summ, decimal sum2, decimal sum3)
        {
            decimal percentage =((temp + temp2 + temp3 + wind + wind2 + wind3 + summ + sum2 + sum3) / 45) * 100;
            decimal roundedPerc = Math.Round(percentage, 2);
            
            return (roundedPerc);
        }

    }
}