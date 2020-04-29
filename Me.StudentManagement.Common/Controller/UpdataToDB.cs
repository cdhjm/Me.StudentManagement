using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using Me.StudentManagement.Common;
using Me.StudentManagement.Entity.Enum;
using Me.StudentManagement.Entity.Models;
using Me.StudentManagement.Logic.Common;
using Me.StudentManagement.Logic.InterfaceAll;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Me.StudentManagement.Logic.Controller
{
    public class UpdataToDB : IUpdateToDB
    {
        private readonly MsSqlContext _msSqlContext;
        private readonly ILoggerHelper _loggerHelper;
        public UpdataToDB(MsSqlContext msSqlContext, ILoggerHelper loggerHelper)
        {
            _msSqlContext = msSqlContext;
            _loggerHelper = loggerHelper;
        }

        public void UpdateScore(string studentInfoId,string classInfoId, int newScore, string course)
        {
            SqlParameter[] sql = new []
            {
                new SqlParameter("course", course),
                new SqlParameter("stuInfo", studentInfoId), 
                new SqlParameter("classInfo", classInfoId), 
                new SqlParameter("newScore", newScore){Direction = ParameterDirection.Output}, 
            };
            _loggerHelper.WriteInfoLog(studentInfoId+","+classInfoId+","+newScore+","+course);
            //没搞明白为什么ExecuteSqlInterpolated可以执行却不能变更,暂时先用ExecuteSqlRaw
            var update = _msSqlContext.Database
                .ExecuteSqlRaw($"update LastClassScores set {course} = {newScore} where ClassInfoId = '{classInfoId}' and StudentsInfoId = '{studentInfoId}'");
            _msSqlContext.SaveChanges();
            _loggerHelper.WriteInfoLog(update);
        }

        public int UpdateTeacherPassword(ChangePasswd changePasswd)
        {
            var result = -1;
            if (changePasswd.OldPassword == "1")
            {
                string sql = string.Format("update TeacherInfos set Password = '{0}' where Id = '{1}';",
                    changePasswd.NewPassword, changePasswd.TeacherId);
                result = _msSqlContext.Database.ExecuteSqlRaw(sql);
                return result;
            }
            else
            {
                return result;
            }
        }
    }


    public class ChangePasswd
    {
        public string Name { get; set; }

        public string TeacherId { get; set; }

        private string _isMatch { get; set; }
        public string OldPassword
        {
            get => _isMatch;

            set
            {
                if (VerifyPassword(value)) _isMatch="1";
                else
                {
                    _isMatch = "0";
                }
            }
        }

        public string NewPassword { get; set; }

        public bool VerifyPassword(string password)
        {
            var result = string.Empty;
            UseSqlServerOptionBuilder useSqlServerOption = new UseSqlServerOptionBuilder();
            using (MsSqlContext msSqlContext = new MsSqlContext(useSqlServerOption.Option))
            {
               result = msSqlContext.TeacherInfos
                    .Where(te => te.Id == TeacherId)
                    .Select(t => t.Password).FirstOrDefault();
            }
            
            if (password == result)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
