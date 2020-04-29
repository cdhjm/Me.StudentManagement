using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Me.StudentManagement.Entity.Models.StudentScore
{
    public class LastclassScore
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long id { get; set; }

        
        public string ClassInfoId { get; set; }

        [Key]
        [Column(TypeName = "varchar(50)")]
        public string StudentsInfoId { get; set; }

        public string StudentName { get; set; }

        public int Chinese { get; set; } = 0;

        public int Math { get; set; } = 0;

        public int English { get; set; } = 0;

        public int Physics { get; set; } = 0;

        public int Chemistry { get; set; } = 0;

        public int Biology { get; set; } = 0;

        public int Politics { get; set; } = 0;

        public int History { get; set; } = 0;

        public int Geography { get; set; } = 0;

        public DateTime DateTime { get; set; }

        [Timestamp]
        public byte[] TimeStamp { get; set; }
        
    }
}