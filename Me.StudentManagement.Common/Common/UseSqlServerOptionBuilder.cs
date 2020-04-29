using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using Me.StudentManagement.Common;
using Microsoft.EntityFrameworkCore;

namespace Me.StudentManagement.Logic.Common
{
    public class UseSqlServerOptionBuilder
    {

        private string connection = "Data Source=.;Database=test;User Id=sa;Password=CDH@jm123;Trusted_Connection=False;Encrypt=False;";

        public DbContextOptions<MsSqlContext> Option { get; }

        public UseSqlServerOptionBuilder()
        {
            DbContextOptionsBuilder<MsSqlContext> optionsBuilder = new DbContextOptionsBuilder<MsSqlContext>();
            optionsBuilder.UseSqlServer(connection);
            Option = optionsBuilder.Options;
        }
    }
}
