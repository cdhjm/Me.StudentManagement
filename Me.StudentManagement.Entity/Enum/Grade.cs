using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Me.StudentManagement.Entity.Enum
{
    public enum Grade
    {
        [Display(Name = "一年级")]
        一年级 = 1,
        二年级,
        三年级,
        四年级,
        五年级,
        六年级,
        初一,
        初二,
        初三,
        高一,
        高二,
        高三
    }
}
