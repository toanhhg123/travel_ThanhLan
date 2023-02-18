using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace source.utils
{
    public static class MD5Password
    {
        public static string HashPass(string password)
        {
            var bytes = new UTF8Encoding().GetBytes(password);
            var hashBytes = System.Security.Cryptography.MD5.Create().ComputeHash(bytes);
            return Convert.ToBase64String(hashBytes);
        }

        public static bool ComparePassword(string password, string passHash) => passHash.Equals(HashPass(password));
    }
}