﻿using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}