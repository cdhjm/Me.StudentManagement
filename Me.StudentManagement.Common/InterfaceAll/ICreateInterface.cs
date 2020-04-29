using System;
using System.Collections.Generic;
using System.Text;
using Me.StudentManagement.Entity.Models;
using Me.StudentManagement.Entity.Models.StudentScore;

namespace Me.StudentManagement.Logic.InterfaceAll
{
    public interface ICreateInterface
    {
        //int AddClassToDB<T>(T classRoom);
        int AddClassToDB<T>(T obj) where T : class;
        int AddUsuallyScoreToDB(LastclassScore lastclassScore);
    }
}
