using System;
using System.Collections.Generic;
using System.Text;
using Me.StudentManagement.Entity.Enum;
using Me.StudentManagement.Logic.Controller;

namespace Me.StudentManagement.Logic.InterfaceAll
{
    public interface IUpdateToDB
    {
        void UpdateScore(string stuId, string classId, int newScore, string course);
        int UpdateTeacherPassword(ChangePasswd changePasswd);
    }
}
