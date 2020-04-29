using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Web.Http.Controllers;
using Me.StudentManagement.Common;
using Me.StudentManagement.Entity.Models;
using Me.StudentManagement.Logic.InterfaceAll;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Primitives;

namespace Me.StudentManagement.Logic.Common.ControllerAttribute
{
    public class UserLoginAttribute:ActionFilterAttribute
    {
        private string Name { get; set; }

        private string PassWord { get; set; }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var value =context.HttpContext.Request.Form;
            List<string> list = new List<string>();
            foreach (var t in value)
            {
                list.Add(t.Value[0]);
            }

            Name = list[0];
            PassWord = list[1];

            //context.HttpContext.Request.Cookies

            var result = VerifyPassword(Name, PassWord);
            if(result == false)
            {
                context.Result = new RedirectResult("/account/login");
            }

            

            base.OnActionExecuting(context);
        }

        public bool VerifyPassword(string name, string password)
        {
            Teacher_Info result = null;

            using (MsSqlContext msSqlContext = new MsSqlContext(new UseSqlServerOptionBuilder().Option))
            {
                result = msSqlContext.TeacherInfos.Where(teach => teach.Name == name && teach.Password == password)
                    .FirstOrDefault();
            }

            if (result == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
