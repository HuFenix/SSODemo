using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace sso.com.Common
{
    public class Common
    {
        /// <summary>
        /// MD5生成
        /// </summary>
        /// <param name="sSource"></param>
        /// <returns></returns>
        public static string EncryptMD5(string sSource)
        {
            try
            {
                byte[] inputByteArray = Encoding.UTF8.GetBytes(sSource);
                MD5CryptoServiceProvider md5csp = new MD5CryptoServiceProvider();
                byte[] resultData = md5csp.ComputeHash(inputByteArray);

                // Create a new Stringbuilder to collect the bytes and create a string.
                StringBuilder sBuilder = new StringBuilder();
                // Loop through each byte of the hashed data and format each one as a hexadecimal string.
                for (int i = 0; i < resultData.Length; i++)
                    sBuilder.Append(resultData[i].ToString("x2"));
                // Return the hexadecimal string.
                return sBuilder.ToString();
            }
            catch
            {
                return null;
            }
        }
    }
}