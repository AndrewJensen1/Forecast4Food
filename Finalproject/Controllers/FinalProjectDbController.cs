using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Finalproject.Controllers
{
    public class FinalProjectDbController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}