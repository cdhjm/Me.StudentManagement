using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Me.StudentManagement.Common;
using Me.StudentManagement.Entity.Models;
using Me.StudentManagement.Entity.Models.StudentScore;
using Me.StudentManagement.Logic.Common;
using Me.StudentManagement.Logic.InterfaceAll;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace Me.StudentManagement.Logic.Controller
{
    public class CreateToDB:ICreateInterface
    {
        private MsSqlContext _MsSqlContext { get; set; }
        private  Encryption _encryption { get; set; }
        private ILoggerHelper _loggerHelper { get; set; }

        public CreateToDB(MsSqlContext msSqlContext, Encryption encryption, ILoggerHelper loggerHelper)
        {
            _MsSqlContext = msSqlContext;
            _encryption = encryption;
            _loggerHelper = loggerHelper;
        }

        public int AddClassToDB<T>(T entity) where T:class
        {
            //int random = new Random().Next(1000, 10000);
            //string time = _encryption.Time().ToString("s");
            //string newTime = Regex.Replace(time, @"[^0-9]+", "");
            //classRoom.ClassRoomId = string.Format("0{0}{1}", newTime, random);
            object obj = entity;
            int result = -1;
            if (typeof(T) == typeof(ClassRoom))
            {
                ClassRoom classRoom = (ClassRoom) obj;
                classRoom.ClassRoomId = _encryption.SignMD5(string.Format("{0}{1}", classRoom.Grade, classRoom.ClassRoomName)).ToUpper();
                classRoom.Remark = classRoom.Remark != null ? classRoom.Remark : "班级信息";
                obj = classRoom;
                entity = (T)obj;
            }
            else if (typeof(T) == typeof(Students_Info))
            {
                Students_Info studentsInfo = (Students_Info)obj;
                studentsInfo.Id = _encryption.SignMD5(string.Format("{0}{1}", studentsInfo.Name, studentsInfo.Phone)).ToUpper();
                studentsInfo.Remark = studentsInfo.Remark != null ? studentsInfo.Remark : "学生信息";
                studentsInfo.DateTime = _encryption.Time();
                obj = studentsInfo;
                entity = (T)obj;
            }

            else if (typeof(T) == typeof(Teacher_Info))
            {
                Teacher_Info teacherInfo = (Teacher_Info)obj;
                teacherInfo.Id = _encryption.SignMD5(string.Format("{0}{1}", teacherInfo.Name, teacherInfo.Phone)).ToUpper();
                teacherInfo.Remark = teacherInfo.Remark != null ? teacherInfo.Remark : "教师信息";
                teacherInfo.Password = _encryption.SignMD5(teacherInfo.Password).ToUpper();
                teacherInfo.DateTime = _encryption.Time();
                obj = teacherInfo;
                entity = (T)obj;
            }
            _loggerHelper.WriteInfoLog(entity);
            try
            {
                _MsSqlContext.Set<T>().Add(entity);
                if (typeof(T) == typeof(Students_Info))
                {
                    Students_Info stu = (Students_Info)obj;
                    _MsSqlContext.Set<LastclassScore>().Add(new LastclassScore()
                    {
                        StudentsInfoId = stu.Id,
                        ClassInfoId = _encryption.SignMD5(string.Format("{0}{1}", stu.Grade, stu.ClassName)).ToUpper(),
                        StudentName = stu.Name
                    });
                }
                result = _MsSqlContext.SaveChanges();
            }
            catch (Exception e)
            {
                _loggerHelper.WriteInfoLog(e.StackTrace);
                
            }
            _loggerHelper.WriteInfoLog(result);
            return result;
        }

        public void AddUsuallyScore<T>(LastclassScore lastclassScore, int courseScore) where T:BaseCourse, new()
        {
            _MsSqlContext.Set<T>().Add(new T()
            {
                Id = lastclassScore.id,
                Score = courseScore,
                Name = lastclassScore.StudentName,
                StudentInfoId = lastclassScore.StudentsInfoId,
                ClassInfoId = lastclassScore.ClassInfoId,
                DateTime = _encryption.Time()
            });
        }

        public int AddUsuallyScoreToDB(LastclassScore lastclassScore)
        {
            var result = -1;
            if (lastclassScore.Chinese != 0)
            {
                AddUsuallyScore<ChineseScore>(lastclassScore, lastclassScore.Chinese);
            }

            if (lastclassScore.Math != 0)
            {
                AddUsuallyScore<MathScore>(lastclassScore, lastclassScore.Math);
            }

            if (lastclassScore.English != 0)
            {
                AddUsuallyScore<EnglishScore>(lastclassScore, lastclassScore.English);
            }

            if (lastclassScore.Politics != 0)
            {
                AddUsuallyScore<PoliticsScore>(lastclassScore, lastclassScore.Politics);
            }

            if (lastclassScore.History != 0)
            {
                AddUsuallyScore<HistoryScore>(lastclassScore, lastclassScore.History);
            }

            if (lastclassScore.Geography != 0)
            {
                AddUsuallyScore<GeographyScore>(lastclassScore, lastclassScore.Geography);
            }

            if (lastclassScore.Physics != 0)
            {
                AddUsuallyScore<PhysicsScore>(lastclassScore, lastclassScore.Physics);
            }

            if (lastclassScore.Chemistry != 0)
            {
                AddUsuallyScore<ChemistryScore>(lastclassScore, lastclassScore.Chemistry);
            }

            if (lastclassScore.Biology != 0)
            {
                AddUsuallyScore<BiologyScore>(lastclassScore, lastclassScore.Biology);
            }

            try
            {
                result = _MsSqlContext.SaveChanges();
            }
            catch (Exception e)
            {
                _loggerHelper.WriteInfoLog(e);
                throw;
            }

            return result;
        }
    }
}
