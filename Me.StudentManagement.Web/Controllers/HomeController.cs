using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Me.StudentManagement.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Extensions.Primitives;
using Microsoft.AspNetCore.Http;

namespace Me.StudentManagement.Web.Controllers
{
   [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        
        public IActionResult Index()
        {
            if (HttpContext.Request.Headers.Contains(
                new KeyValuePair<string, StringValues>("x-requested-with", "XMLHttpRequest")))
            {
                HttpContext.Response.Headers.Add("REDIRECT", "REDIRECT");
                HttpContext.Response.Headers.Add("CONTENTPATH", HttpContext.Request.GetEncodedUrl());

            }
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}