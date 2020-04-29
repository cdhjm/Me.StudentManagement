using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Me.StudentManagement.Logic.InterfaceAll;
using NLog;

namespace Me.StudentManagement.Common
{
    public class LoggerHelper:ILoggerHelper
    {
        private readonly ILogger _logger;

        public LoggerHelper()
        {
            _logger = LogManager.GetCurrentClassLogger();
        }

        public void WriteInfoLog(object info)
        {
            //string path = string.Format("{0}\\info", AppDomain.CurrentDomain.BaseDirectory);
            //string fileName = string.Format("{0}{1}.txt", path, busy);
            
            _logger.Info(info);
        }

        public void WriteErrorLog(object error)
        {
            _logger.Error(error);
        }

        public void WriteTraceLog(object trace)
        {
            _logger.Trace(trace);
        }
    }
}
