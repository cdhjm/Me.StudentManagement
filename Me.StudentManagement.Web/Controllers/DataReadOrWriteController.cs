using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Me.StudentManagement.Entity.Models;
using Me.StudentManagement.Logic.InterfaceAll;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Me.StudentManagement.Entity.Enum;
using Me.StudentManagement.Entity.Models.StudentScore;
using Me.StudentManagement.Logic.Controller;
using Microsoft.AspNetCore.Authorization;


namespace Me.StudentManagement.Web.Controllers
{
    [Authorize]
    public class DataReadOrWriteController : Controller
    {
        private readonly ICreateInterface _create;
        private readonly ILoggerHelper _loggerHelper;
        private readonly IQueryDBData _queryDbData;
        private readonly IUpdateToDB _updateToDb;
        private readonly IDeleteFormDB _deleteFormDb;

        public DataReadOrWriteController(
            ICreateInterface create, 
            ILoggerHelper loggerHelper, 
            IQueryDBData queryDbData, 
            IUpdateToDB updateToDb, 
            IDeleteFormDB deleteFormDb)
        {
            _create = create;
            _loggerHelper = loggerHelper;
            _queryDbData = queryDbData;
            _updateToDb = updateToDb;
            _deleteFormDb = deleteFormDb;
        }

        /// <summary>
        /// 获取所有学生数据
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public string TestResult(int page, int limit)
        {
            var data = _queryDbData.GetStudentJsonResult(page,limit, out int count);
            StuAndSocreToJsonResult<StudentFullClassScore> result = new StuAndSocreToJsonResult<StudentFullClassScore>();
            result.code = 0;
            result.count = count;
            result.msg = "";
            result.data = data;
            var json = JsonConvert.SerializeObject(result);
            return json;
        }

        /// <summary>
        /// 获取学生查询数据
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public string SutudentSearch(int page, int limit, string searchName, int grade, string className)
        {
                //if (!string.IsNullOrEmpty(searchName) && !string.IsNullOrEmpty(grade.ToString()) && !string.IsNullOrEmpty(className))
                //{
                    _loggerHelper.WriteInfoLog(searchName + grade+ className);
                    var data = _queryDbData.GetStudentJsonResult(page, limit, searchName, grade, className);
                    StuAndSocreToJsonResult<StudentFullClassScore> result = new StuAndSocreToJsonResult<StudentFullClassScore>();
                    result.count = data.Count();
                    result.code = 0;
                    result.msg = "";
                    result.data = data;
                    var json = JsonConvert.SerializeObject(result);
                    return json;
           
        }


        [HttpPost]
        public IActionResult CreateClass(ClassRoom classRoom)
        {
            _loggerHelper.WriteInfoLog(classRoom.ClassRoomName);
            object result = _create.AddClassToDB(classRoom);
            return Content(result.ToString());
        }

        [HttpPost]
        public IActionResult CreateTeacher(Teacher_Info teacherInfo)
        {
            _loggerHelper.WriteInfoLog(teacherInfo.Name);
            object result = _create.AddClassToDB(teacherInfo);
            return Content(result.ToString());
        }

        [HttpPost]
        public IActionResult CreateStudent(Students_Info studentsInfo)
        {
            _loggerHelper.WriteInfoLog(studentsInfo.Name);
            object result = _create.AddClassToDB(studentsInfo);
            return Content(result.ToString());
        }

        /// <summary>
        /// 根据年级返回班级
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IEnumerable<string> AjaxActionResult(int id)
        {
            return _queryDbData.GetClassRooms(id).ToList();
        }

        /// <summary>
        /// 更新期末成绩
        /// </summary>
        /// <param name="studentInfoId"></param>
        /// <param name="classinfoId"></param>
        /// <param name="newScore"></param>
        /// <param name="course"></param>
        public void UpdateScore(string studentInfoId, string classinfoId, int newScore, string course)
        {
            _updateToDb.UpdateScore(studentInfoId, classinfoId, newScore, course);
        }

        /// <summary>
        /// 添加单科成绩
        /// </summary>
        /// <param name="lastclassScore"></param>
        /// <returns></returns>
        public int AddUsuallyScore(LastclassScore lastclassScore)
        {
            return _create.AddUsuallyScoreToDB(lastclassScore);
        }

        /// <summary>
        /// 查询单科成绩
        /// </summary>
        /// <returns></returns>
        public string QueryCourseScore(int page, int limit, string StudentsInfoId, Course course)
        {
            var data = _queryDbData.QueryCourseScoreData(page, limit, StudentsInfoId, course);
            StuAndSocreToJsonResult<QueryCourseScore> result = new StuAndSocreToJsonResult<QueryCourseScore>();
            result.count = data.Count();
            result.code = 0;
            result.msg = "";
            result.data = data;
            var json = JsonConvert.SerializeObject(result);
            _loggerHelper.WriteInfoLog(json);
            return json;
        }

        /// <summary>
        /// 教师查询
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="searchName"></param>
        /// <param name="grade"></param>
        /// <param name="className"></param>
        /// <returns></returns>
        public string TeacherSearch(int page, int limit, string searchName, int grade, string className, int state)
        {
            //if (!string.IsNullOrEmpty(searchName) && !string.IsNullOrEmpty(grade.ToString()) && !string.IsNullOrEmpty(className))
            //{
            _loggerHelper.WriteInfoLog(searchName + grade + className);
            var data = _queryDbData.GetTeacherJsonResult(page, limit, searchName, grade, className,state, out int count);
            StuAndSocreToJsonResult<TeacherReault> result = new StuAndSocreToJsonResult<TeacherReault>();
            result.count = count;
            result.code = 0;
            result.msg = "";
            result.data = data;
            var json = JsonConvert.SerializeObject(result);
            return json;

        }

        /// <summary>
        /// 查询班级
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="searchName"></param>
        /// <param name="grade"></param>
        /// <param name="className"></param>
        /// <returns></returns>
        public string QueryClassRoom(int page, int limit, int grade, int state)
        {
            var data = _queryDbData.QureyClassRooms(page, limit, grade, state, out int count);
            StuAndSocreToJsonResult<ClassRoom> result = new StuAndSocreToJsonResult<ClassRoom>();
            result.count = count;
            result.code = 0;
            result.msg = "";
            result.data = data;
            var json = JsonConvert.SerializeObject(result);
            return json;
        }

        public int ChangeTeacherPassword(ChangePasswd changePasswd)
        {
            var result = _updateToDb.UpdateTeacherPassword(changePasswd);
            return result;
        }

        public int DeleteUser(DeleteUser deleteUser)
        {
            var result = _deleteFormDb.DeleteUser(deleteUser);
            return result;
        }


        // POST: DataReadOrWrite/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            return View("Error");
        }

        
    }

    
}