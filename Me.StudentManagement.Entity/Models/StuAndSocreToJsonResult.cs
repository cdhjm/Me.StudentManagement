using System;
using System.Collections.Generic;
using System.Text;
using NLog.Filters;

namespace Me.StudentManagement.Entity.Models
{
    public class StuAndSocreToJsonResult<T>
    {
        public int code { get; set; }

        public string msg { get; set; }

        public int count { get; set; }

        public IEnumerable<T> data { get; set; }
    }
}
