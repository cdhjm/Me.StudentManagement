using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Me.StudentManagement.Logic.Common
{
    public class Encryption
    {
        public string SignMD5(string str)
        {
            MD5 md5 = MD5.Create();
            byte[] byteOld = Encoding.UTF8.GetBytes(str);
            byte[] byteNew = md5.ComputeHash(byteOld);
            StringBuilder sb = new StringBuilder();
            foreach (var b in byteNew)
            {
                sb.Append(b.ToString("x2"));
            }

            return sb.ToString();
        }

        public DateTime Time()
        {
            var dt = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
            DateTime result = Convert.ToDateTime(dt);
            return result;
        }
    }
}
