using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Finalproject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

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
        // LOOK AT ME I AM SUPER COOL MAKING CHAnges yay me!!!!
    }
}