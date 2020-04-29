using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Me.StudentManagement.Entity.Enum
{
    public enum Course
    {
        [Display(Name = "语文")]
        Chinese = 1,

        [Display(Name = "数学")]
        Math,

        [Display(Name = "英语")]
        English,

        [Display(Name = "物理")]
        Physics,

        [Display(Name = "化学")]
        Chemistry,

        [Display(Name = "生物")]
        Biology,

        [Display(Name = "政治")]
        Politics,

        [Display(Name = "历史")]
        History,

        [Display(Name = "地理")]
        Geography
    }

}
