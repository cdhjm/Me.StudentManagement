using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata;
using System.Text;
using Me.StudentManagement.Entity.Enum;

namespace Me.StudentManagement.Entity.Models
{
    public class ClassRoom
    {
        [Column(TypeName = "varchar(50)")]
        public string ClassRoomId { get; set; }

        public string ClassRoomName { get; set; }

        public int Grade { get; set; }

        #nullable enable
        public string? Remark { get; set; }

    }
}
