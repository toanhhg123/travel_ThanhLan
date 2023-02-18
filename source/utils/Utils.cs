using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace source.utils
{
    public class Utils
    {
        public static String HmacSHA512(string key, String inputData)
        {
            var hash = new StringBuilder();
            byte[] keyBytes = Encoding.UTF8.GetBytes(key);
            byte[] inputBytes = Encoding.UTF8.GetBytes(inputData);
            using (var hmac = new HMACSHA512(keyBytes))
            {
                byte[] hashValue = hmac.ComputeHash(inputBytes);
                foreach (var theByte in hashValue)
                {
                    hash.Append(theByte.ToString("x2"));
                }
            }

            return hash.ToString();
        }
        public static string GetIpAddress(IHttpContextAccessor httpContextAccessor)
        {
            string ipAddress = "";
            // try
            // {
            //     ipAddress = httpContextAccessor.HttpContext?.Request.Headers["HTTP_X_FORWARDED_FOR"] ?? "";

            //     if (string.IsNullOrEmpty(ipAddress) || (ipAddress.ToLower() == "unknown") || ipAddress.Length > 45)
            //         ipAddress = httpContextAccessor.HttpContext?.Request.Headers["REMOTE_ADDR"] ?? "";
            // }
            // catch (Exception ex)
            // {
            //     ipAddress = "Invalid IP:" + ex.Message;
            // }

            return ipAddress;
        }


    }
}