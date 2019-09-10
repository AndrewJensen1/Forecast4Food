using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Finalproject.Controllers
{
    public class GoogleController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}