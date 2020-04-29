using System;
using System.Collections.Generic;
using System.Text;
using Me.StudentManagement.Entity.Enum;

namespace Me.StudentManagement.Entity.Models
{ 
    public class StudentFullClassScore
    {
        public long id { get; set; }

        public string ClassInfoId { get; set; }

        public string StudentInfoId { get; set; }

        public string Name { get; set; }

        public int Age { get; set; }

        public long Phone { get; set; }

        public string Gender { get; set; }

        public string ClassName { get; set; }

        public int? Chinese { get; set; } = 0;

        public int? Math { get; set; } = 0;

        public int? English { get; set; } = 0;

        public int? Physics { get; set; } = 0;

        public int? Chemistry { get; set; } = 0;

        public int? Biology { get; set; } = 0;

        public int? Politics { get; set; } = 0;

        public int? History { get; set; } = 0;

        public int? Geography { get; set; } = 0;

        public int? TotalScore { get; set; }

        public string Grade { get; set; }

        public string Remark { get; set; }
    }
}
