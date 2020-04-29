using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using Me.StudentManagement.Entity.Enum;
using Me.StudentManagement.Entity.Models;
using Me.StudentManagement.Entity.Models.StudentScore;
using Me.StudentManagement.Logic.Common;
using Me.StudentManagement.Logic.InterfaceAll;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Me.StudentManagement.Common
{
    public class MsSqlContext:DbContext
    {
        //private readonly IConfiguration _configuration;
        public MsSqlContext(DbContextOptions<MsSqlContext> options):base(options)
        {
            
        }


        public DbSet<ClassRoom> ClassRooms { get; set; }
        public DbSet<Teacher_Info> TeacherInfos { get; set; }
        public DbSet<Students_Info> StudentsInfos { get; set; }
        public DbSet<LastclassScore> LastClassScores { get; set; }
        public DbSet<ChineseScore> ChineseScores { get; set; }
        public DbSet<MathScore> MathScores { get; set; }
        public DbSet<EnglishScore> EnglishScores { get; set; }
        public DbSet<PhysicsScore> PhysicsScores { get; set; }
        public DbSet<BiologyScore> BiologyScores { get; set; }
        public DbSet<ChemistryScore> ChemistryScores { get; set; }
        public DbSet<PoliticsScore> PoliticsScores { get; set; }
        public DbSet<HistoryScore> HistoryScores { get; set; }
        public DbSet<GeographyScore> GeographyScores { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            Encryption _encryption = new Encryption();
            modelBuilder.Entity<Students_Info>().HasData(new Students_Info[2]
            {
                new Students_Info(){ Id = "BD22C998A7996360AA636920A42AEE8E", Name = "陈东辉", ClassName = "一班", Grade = Grade.一年级, Gender = "男", Age = 22, Phone = 13501637480, Remark = "小心", DateTime = _encryption.Time()},
                new Students_Info(){Id = "BD22C998A7996360AA636920A42AEE82", Name = "小明", ClassName = "二班", Grade = Grade.二年级, Gender = "女", Age = 32, Phone = 13501980029, Remark = "哈哈", DateTime = _encryption.Time()}
            });
            modelBuilder.Entity<LastclassScore>().HasData(new LastclassScore[2]
            {
                new LastclassScore()
                {
                    ClassInfoId = "1E9D4FF43B8FE797ECC74A5C6ACFB53F", StudentsInfoId = "BD22C998A7996360AA636920A42AEE8E",
                    Biology = 88, Chemistry = 98, Chinese = 76, Geography = 65, English = 54, History = 78, Math = 78,
                    Physics = 65, Politics = 82, DateTime = _encryption.Time(), StudentName = "陈东辉"
                },
                new LastclassScore()
                {
                    ClassInfoId = "F23A91576D4F5F34270BC504DD4A1686", StudentsInfoId = "BD22C998A7996360AA636920A42AEE82",
                    Biology = 84, Chemistry = 58, Chinese = 76, Geography = 95, English = 54, History = 88, Math = 71,
                    Physics = 62, Politics = 32, DateTime = _encryption.Time(), StudentName = "陈东辉"
                },
            });
            modelBuilder.Entity<ClassRoom>().HasData(new List<ClassRoom>()
            {
                new ClassRoom()
                    {Grade = 1, ClassRoomName = "一班", ClassRoomId = "1E9D4FF43B8FE797ECC74A5C6ACFB53F", Remark = "班级"},
                new ClassRoom()
                    {Grade = 1, ClassRoomName = "二班", ClassRoomId = "A7276458F9894C4475F3F3D0F08F5742", Remark = "班级"},
                new ClassRoom()
                    {Grade = 2, ClassRoomName = "二班", ClassRoomId = "F23A91576D4F5F34270BC504DD4A1686", Remark = "班级"},
                new ClassRoom()
                    {Grade = 3, ClassRoomName = "二班", ClassRoomId = "683548FF6097EFE56828397E8FE00E95", Remark = "班级"},
                new ClassRoom()
                    {Grade = 1, ClassRoomName = "六班", ClassRoomId = "A2D36EF35C05FAD14C2475416F381986", Remark = "班级"},
            });

            modelBuilder.Entity<Teacher_Info>().HasData(new Teacher_Info[2]
            {
                new Teacher_Info()
                {
                    Id = "FBFCC389D48AA2DCCBDE9ED0692236F1", Name = "张敏", ClassName = "二班", Grade = 1, Age = 23,
                    Password = "E10ADC3949BA59ABBE56E057F20F883E", Gender = "女", Phone = 13311628894, Remark = "老师", DateTime = _encryption.Time(),
                    Course = Course.Chinese
                },
                new Teacher_Info()
                {
                    Id = "C100FB232C51E600E5EDA3C9E6EF9751", Name = "admin", ClassName = "二班", Grade = 1, Age = 23,
                    Password = "E10ADC3949BA59ABBE56E057F20F883E", Gender = "男", Phone = 13501637480, Remark = "admin", DateTime = _encryption.Time(),
                    Course = Course.Chinese
                }
            });
            base.OnModelCreating(modelBuilder);
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer(_configuration.GetConnectionString("BloggingDatabase"));
        //    //optionsBuilder.UseSqlServer(_dbConnection.ConnectionString);
        //    base.OnConfiguring(optionsBuilder);
        //}

        
    }
}
