using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Me.StudentManagement.Common;
using Me.StudentManagement.Entity.Enum;
using Me.StudentManagement.Entity.Models;
using Me.StudentManagement.Entity.Models.StudentScore;
using Me.StudentManagement.Logic.InterfaceAll;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace Me.StudentManagement.Logic.Controller
{
    public class QueryDBData : IQueryDBData
    {
        private readonly MsSqlContext _msSqlContext;
        private readonly ILoggerHelper _loggerHelper;
        public QueryDBData(MsSqlContext msSqlContext, ILoggerHelper loggerHelper)
        {
            _msSqlContext = msSqlContext;
            _loggerHelper = loggerHelper;
        }

        public IEnumerable<string> GetClassRooms(int id)
        {
            //var classRoom = _msSqlContext.ClassRooms
            //    .Where(b=>b.Grade==id)
            //    .OrderBy(classRoom => classRoom.ClassRoomName)
            //    .Select(b => b.ClassRoomName).ToList();
            var className = from b in _msSqlContext.ClassRooms
                    .Where(b => b.Grade == id)
                    .OrderBy(b => b.ClassRoomName)
                            select b.ClassRoomName;
            return className;
        }
        /// <summary>
        /// 返回学生所有数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IEnumerable<StudentFullClassScore> GetStudentJsonResult(int page, int limit, out int count)
        {
            //var students = _msSqlContext.StudentsInfos
            //    .Join(_msSqlContext.LastClassScores, stu => stu.Id, score => score.StudentsInfo, (stu, score) =>
            //        new StudentFullClassScore()
            //        {
            //            Id = stu.Id, Name = stu.Name, Age = stu.Age, Gender = stu.Gender, Phone = stu.Phone,
            //            Grade = stu.Grade, ClassName = stu.ClassName, Remark = stu.Remark, Geography = score.Geography,
            //            Chinese = score.Chinese, Physics = score.Physics, Biology = score.Biology,
            //            English = score.English,
            //            Math = score.Math, History = score.History, Politics = score.Politics,
            //            Chemistry = score.Chemistry
            //        }).SelectMany(combination=>combination.Id.DefaultIfEmpty(), (score, c) => new{}).ToList();
            var students = (from stu in _msSqlContext.StudentsInfos
                            join score in _msSqlContext.LastClassScores
                                on stu.Id equals score.StudentsInfoId into t
                            from score in t.DefaultIfEmpty()
                            select new StudentFullClassScore()
                            {
                                StudentInfoId = stu.Id,
                                Name = stu.Name,
                                Grade = stu.Grade.ToString(),
                                ClassName = stu.ClassName,
                                Remark = stu.Remark,
                                id = score.id,
                                ClassInfoId = score.ClassInfoId,
                                Geography = score.Geography,
                                Chinese = score.Chinese,
                                Physics = score.Physics,
                                Biology = score.Biology,
                                English = score.English,
                                Math = score.Math,
                                History = score.History,
                                Politics = score.Politics,
                                Chemistry = score.Chemistry,
                                TotalScore = (score.Chinese + score.Math + score.English + score.Geography + score.Biology +
                                              score.Chemistry + score.History + score.Politics + score.Physics)
                            }).OrderBy(key => key.id).Skip(limit * (page - 1)).Take(limit).ToList();
            count = _msSqlContext.LastClassScores.Count();
            _loggerHelper.WriteInfoLog(students);

            return students;
        }


        public IEnumerable<StudentFullClassScore> GetStudentJsonResult(int page, int limit, string name, int grade, string className)
        {
            var data = (from stu in _msSqlContext.StudentsInfos
                        where stu.Name == name && stu.Grade == (Grade)grade && stu.ClassName == className
                        join score in _msSqlContext.LastClassScores on stu.Id equals score.StudentsInfoId
                            into t
                        from score in t.DefaultIfEmpty()
                        select new StudentFullClassScore()
                        {
                            StudentInfoId = stu.Id,
                            Name = stu.Name,
                            Grade = stu.Grade.ToString(),
                            ClassName = stu.ClassName,
                            Remark = stu.Remark,
                            id = score.id,
                            ClassInfoId = score.ClassInfoId,
                            Geography = score.Geography,
                            Chinese = score.Chinese,
                            Physics = score.Physics,
                            Biology = score.Biology,
                            English = score.English,
                            Math = score.Math,
                            History = score.History,
                            Politics = score.Politics,
                            Chemistry = score.Chemistry,
                            TotalScore = (score.Chinese + score.Math + score.English + score.Geography + score.Biology +
                                          score.Chemistry + score.History + score.Politics + score.Physics)
                        }).OrderBy(key => key.id).Skip(limit * (page - 1)).Take(limit);
            //if (!string.IsNullOrEmpty(name))
            //{
            //    data = data.Where(stu => stu.Name == name);
            //}

            //if (string.IsNullOrEmpty(grade.ToString()))
            //{
            //    data = data.Where(stu => stu.Grade == grade.ToString());
            //}

            //if (string.IsNullOrEmpty(className))
            //{
            //    data = data.Where(stu => stu.ClassName == className);
            //}
            return data.ToList();
        }

        /// <summary>
        /// 返回单科成绩数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="StudentsInfoId"></param>
        /// <returns></returns>
        public IEnumerable<QueryCourseScore> QueryCourseScores<T>(int page, int limit, string StudentsInfoId) where T : BaseCourse
        {
            //Dictionary<int, IList<QueryCourseScore> > dic = new Dictionary<int, IList<QueryCourseScore>>();

            //var count = _msSqlContext.Set<T>().Where(scores => scores.StudentInfoId == StudentsInfoId).Count();

            var datas = _msSqlContext.Set<T>()
                .Where(scores => scores.StudentInfoId == StudentsInfoId)
                .Skip(limit * (page - 1))
                .Take(limit)
                .Select(data => new QueryCourseScore()
                {
                    Id = data.Id,
                    Name = data.Name,
                    DateTime = data.DateTime,
                    Score = data.Score
                })
                .ToList();
            return datas;
        }

        public IEnumerable<QueryCourseScore> QueryCourseScoreData(int page, int limit, string StudentsInfoId, Course course)
        {
            IEnumerable<QueryCourseScore> datas = null;
            //Dictionary<Course, Type> dic = new Dictionary<Course, Type>();
            //Type[] types = new[]
            //{
            //    typeof(ChineseScore), typeof(MathScore), typeof(EnglishScore), typeof(PoliticsScore),
            //    typeof(HistoryScore),
            //    typeof(GeographyScore), typeof(PhysicsScore), typeof(ChemistryScore), typeof(BiologyScore)
            //};
            //Course[] courses = new[]
            //{
            //    Course.Chinese, Course.Math, Course.English, Course.Politics, Course.History,
            //    Course.Geography, Course.Physics, Course.Chemistry, Course.Biology
            //};
            //for (int i = 0; i < types.Length; i++)
            //{
            //    dic.Add(courses[i], types[i]);
            //}

            //Type value = null;
            //var d = dic.Where(t => t.Key == course);

            //foreach (var key in d)
            //{
            //    value = key.Value;
            //}

            //datas = QueryCourseScores<value>(value, page, limit, StudentsInfoId);

            if (course == Course.Chinese)
            {
                datas = QueryCourseScores<ChineseScore>(page, limit, StudentsInfoId);
            }

            if (course == Course.Math)
            {
                datas = QueryCourseScores<MathScore>(page, limit, StudentsInfoId);
            }

            if (course == Course.English)
            {
                datas = QueryCourseScores<EnglishScore>(page, limit, StudentsInfoId);
            }

            if (course == Course.Politics)
            {
                datas = QueryCourseScores<PoliticsScore>(page, limit, StudentsInfoId);
            }

            if (course == Course.History)
            {
                datas = QueryCourseScores<HistoryScore>(page, limit, StudentsInfoId);
            }

            if (course == Course.Geography)
            {
                datas = QueryCourseScores<GeographyScore>(page, limit, StudentsInfoId);
            }

            if (course == Course.Physics)
            {
                datas = QueryCourseScores<PhysicsScore>(page, limit, StudentsInfoId);
            }

            if (course == Course.Chemistry)
            {
                datas = QueryCourseScores<ChemistryScore>(page, limit, StudentsInfoId);
            }

            if (course == Course.Biology)
            {
                datas = QueryCourseScores<BiologyScore>(page, limit, StudentsInfoId);
            }

            return datas;
        }

        /// <summary>
        /// 返回教师数据
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="name"></param>
        /// <param name="grade"></param>
        /// <param name="className"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public IEnumerable<TeacherReault> GetTeacherJsonResult(int page, int limit, string name, int grade, string className,int state, out int count)
        {
            _loggerHelper.WriteInfoLog(state);
            var teachers = _msSqlContext.TeacherInfos
                .Select(tea => new TeacherReault()
                {
                    Id = tea.Id,
                    Name = tea.Name,
                    ClassName = tea.ClassName,
                    Grade = tea.Grade,
                    Course = tea.Course,
                    Phone = tea.Phone,
                    Remark = tea.Remark
                }).OrderBy(key => key.Id).Skip(limit * (page - 1)).Take(limit);


            count = _msSqlContext.TeacherInfos.Count();


            if (state == 2)
            {
                teachers = teachers.Where(tea => tea.Name == name);
            }

            var result = teachers.ToList();
            return result;
        }


        public IEnumerable<ClassRoom> QureyClassRooms(int page, int limit, int grade, int state, out int count)
        {
            var rooms = (from classRoom in _msSqlContext.ClassRooms
                select new ClassRoom()
                {
                    ClassRoomName = classRoom.ClassRoomName, Grade = classRoom.Grade, Remark = classRoom.Remark,
                    ClassRoomId = classRoom.ClassRoomId

                }).Skip(limit *(page -1)).Take(limit);

            count = _msSqlContext.ClassRooms.Count();

            if (state == 1)
            {
                if (grade > 0)
                {
                    rooms = rooms.Where(c => c.Grade == grade);
                }
            }

            return rooms.ToList();
        }
    }

    public class TeacherReault
    {
        public int sort { get; set; }

        public string Id { get; set; }

        public string Name { get; set; }

        public int Age { get; set; }

        public long Phone { get; set; }

        public string Gender { get; set; }

        public Course Course { get; set; }

        public string ClassName { get; set; }

        public int Grade { get; set; }

        public DateTime DateTime { get; set; }

        public string Remark { get; set; }
    }

    public class QueryCourseScore
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public int Score { get; set; }

        public DateTime DateTime { get; set; }
    }
}
