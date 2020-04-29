using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Me.StudentManagement.Entity.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Me.StudentManagement.Common;
 using   Me.StudentManagement.Logic.Controller;
using Me.StudentManagement.Logic.InterfaceAll;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;

namespace Me.StudentManagement.Web.Controllers
{
    [Authorize]
    public class CreateController : Controller
    {
        private readonly ICreateInterface _create;
        private readonly ILoggerHelper _loggerHelper;
        private readonly IQueryDBData _queryDbData;

        public CreateController(ICreateInterface create, ILoggerHelper loggerHelper, IQueryDBData queryDbData)
        {
            _create = create;
            _loggerHelper = loggerHelper;
            _queryDbData = queryDbData;
        }
        
        [HttpGet]
        public IActionResult CreateStudent()
        {
            return View("PartialPage/CreateStudent");
        }

        
        [HttpGet]
        public IActionResult CreateTeacher()
        {
            return View("PartialPage/CreateTeacher");
        }

        [HttpGet]
        public IActionResult CreateClass()
        {
            return View("PartialPage/CreateClass");
        }


        public IActionResult StudentInfo()
        {
            return View("Pages/StudentListView");
        }

        public IActionResult StudentUsuallyScore()
        {
            return View("Pages/StudentUsuallyScoreView");
        }

        public IActionResult TeacherInfo()
        {
            return View("Pages/TeacherListView");
        }

        public IActionResult ClassInfo()
        {
            return View("Pages/ClassRoomListView");
        }
    }

}