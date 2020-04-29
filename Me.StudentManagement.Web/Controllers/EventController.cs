using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Me.StudentManagement.Common;
using Me.StudentManagement.Logic.InterfaceAll;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Me.StudentManagement.Web.Controllers
{
    [Authorize]
    public class EventController : Controller
    {
        public IActionResult ChangePasswordView()
        {
            return View("EventPages/ChangePasswordView");
        }
    }

    
}