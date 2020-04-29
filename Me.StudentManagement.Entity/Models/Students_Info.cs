using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Me.StudentManagement.Entity.Enum;
using Me.StudentManagement.Entity.Models.StudentScore;

namespace Me.StudentManagement.Entity.Models
{
    public class Students_Info
    {
        [Column(TypeName = "varchar(50)")]
        public string Id { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int sort { get; set; }

        public string Identity { get; set; } = "student";

        public string Name { get; set; }

        public int Age { get; set; }

        public long Phone { get; set; }

        public string Gender { get; set; }

        public string ClassName { get; set; }

        public Grade Grade { get; set; }
        
        public DateTime DateTime { get; set; }
        
        public string Remark { get; set; }

        public List<LastclassScore> Score { get; set; }
    }
}