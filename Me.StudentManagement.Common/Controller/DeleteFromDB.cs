using System;
using System.Collections.Generic;
using System.Text;
using Me.StudentManagement.Common;
using Me.StudentManagement.Logic.Common;
using Me.StudentManagement.Logic.InterfaceAll;
using Microsoft.EntityFrameworkCore;

namespace Me.StudentManagement.Logic.Controller
{
    public class DeleteFromDB:IDeleteFormDB
    {
        private MsSqlContext _MsSqlContext { get; set; }
        private Encryption _encryption { get; set; }
        private ILoggerHelper _loggerHelper { get; set; }

        public DeleteFromDB(MsSqlContext msSqlContext, Encryption encryption, ILoggerHelper loggerHelper)
        {
            _MsSqlContext = msSqlContext;
            _encryption = encryption;
            _loggerHelper = loggerHelper;
        }

        public int DeleteUser(DeleteUser deleteUser)
        {
            _loggerHelper.WriteInfoLog(deleteUser.Identity + " " + deleteUser.UserId);
            var result = -1;
            if (deleteUser.Identity == "student")
            {
                StringBuilder sql = new StringBuilder();
                sql.AppendFormat("delete from StudentsInfos where Id = '{0}';", deleteUser.UserId);
                sql.AppendFormat("delete from LastClassScores where StudentsInfoId = '{0}';", deleteUser.UserId);

                result = _MsSqlContext.Database.ExecuteSqlRaw(sql.ToString());
            }

            if (deleteUser.Identity == "teacher")
            {
                string sql = string.Format("delete from TeacherInfos where Id = '{0}'", deleteUser.UserId);
                result = _MsSqlContext.Database.ExecuteSqlRaw(sql);
            }

            return result;
        }

    }

    public class DeleteUser
    {
        public string Identity { get; set; }

        public string UserId { get; set; }
    }
}
