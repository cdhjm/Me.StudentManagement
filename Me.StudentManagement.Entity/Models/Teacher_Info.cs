using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Me.StudentManagement.Entity.Enum;


namespace Me.StudentManagement.Entity.Models
{
    public class Teacher_Info
    {
        

        [Column(TypeName = "varchar(50)")]
        public string Id { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int sort { get; set; }


        public string Identity { get; set; } = "teacher";

        public string Name { get; set; }

        public int Age { get; set; }

        public long Phone { get; set; }

        public string Gender { get; set; }

        public Course Course { get; set; }

        public string ClassName { get; set; }

        public int Grade { get; set; }

        public DateTime DateTime { get; set; }

        public string Remark { get; set; }

        public string Password { get; set; }
    }
}
