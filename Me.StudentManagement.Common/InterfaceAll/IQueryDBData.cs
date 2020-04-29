using System;
using System.Collections.Generic;
using System.Text;
using Me.StudentManagement.Entity.Enum;
using Me.StudentManagement.Entity.Models;
using Me.StudentManagement.Logic.Controller;

namespace Me.StudentManagement.Logic.InterfaceAll
{
    public interface IQueryDBData
    {
        IEnumerable<string> GetClassRooms(int id);
        IEnumerable<StudentFullClassScore> GetStudentJsonResult(int page, int size, out int count);

        IEnumerable<StudentFullClassScore> GetStudentJsonResult(int page, int limit, string name, int grade,
            string className);

        IEnumerable<QueryCourseScore> QueryCourseScoreData(int page, int limit, string StudentsInfoId, Course course);

        IEnumerable<TeacherReault> GetTeacherJsonResult(int page, int limit, string name, int grade, string className,int state,
            out int count);

        IEnumerable<ClassRoom> QureyClassRooms(int page, int limit, int grade, int state, out int count);
    }
}
