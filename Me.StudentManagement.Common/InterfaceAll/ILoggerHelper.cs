using System;
using System.Collections.Generic;
using System.Text;

namespace Me.StudentManagement.Logic.InterfaceAll
{
    public interface ILoggerHelper
    {
        void WriteInfoLog(object info);
        void WriteErrorLog(object error);
        void WriteTraceLog(object trace);
    }
}
