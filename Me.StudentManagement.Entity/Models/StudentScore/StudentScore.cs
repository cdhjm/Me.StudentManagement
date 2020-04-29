using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Me.StudentManagement.Entity.Models.StudentScore
{
    public class BaseCourse
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long Id { get; set; }

        public string Name { get; set; }

        public string StudentInfoId { get; set; }

        public string ClassInfoId { get; set; }

        public int Score { get; set; }

        public DateTime DateTime { get; set; }
    }

    public class ChineseScore : BaseCourse
    {
    }

    public class EnglishScore : BaseCourse
    {
    }

    public class MathScore : BaseCourse
    {
        
    }

    public class PhysicsScore : BaseCourse
    {
    }

    public class ChemistryScore : BaseCourse
    {
    }

    public class BiologyScore : BaseCourse
    {
    }

    public class PoliticsScore : BaseCourse
    {
    }

    public class HistoryScore : BaseCourse
    {
    }

    public class GeographyScore : BaseCourse
    {
    }
}
