using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Me.StudentManagement.Entity.Models;
using Me.StudentManagement.Logic.Common.ControllerAttribute;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Me.StudentManagement.Web.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        [HttpGet]
        public IActionResult Login()
        {
            return View("LoginView");
        }

        [UserLogin]
        [HttpPost]
        public async Task<IActionResult> Login(SysUser user)
        {
            Claim[] claims = new Claim[3]
            {
                new Claim(ClaimTypes.Name, "Student"),
                new Claim("fullName", user.Name),
                new Claim(ClaimTypes.Country,"China")
            };
            ClaimsIdentity identity = new ClaimsIdentity(claims,CookieAuthenticationDefaults.AuthenticationScheme);
            AuthenticationProperties properties = new AuthenticationProperties()
            {
                AllowRefresh = true,
                ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10)
            };
            ClaimsPrincipal principal = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, properties);

            return LocalRedirect("/home/index");
            
        } 
    }
}