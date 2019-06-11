using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CourseworkHelsi.Helpers
{
    class PasswdEncoding
    {
        public static string GetPswdMD5(string spwd)
        {
            string result = string.Empty;
            var inputData = UnicodeEncoding.Unicode.GetBytes(spwd);
            result = UnicodeEncoding.Unicode.GetString(new MD5CryptoServiceProvider().ComputeHash(inputData));
            return result;
        }
    }
}
