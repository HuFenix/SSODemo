﻿using System;  
using System.Collections.Generic;  
using System.Text;  
using System.Net;  
using System.IO;  
using System.Text.RegularExpressions;  
using System.IO.Compression;  
using System.Security.Cryptography.X509Certificates;  
using System.Net.Security;
using System.Net.Http;

namespace Utils
{
    /// <summary>  
    /// Http连接操作帮助类  
    /// </summary>  
    public class HttpHelper
    {
        public static string OpenReadWithHttps(string URL, string strPostdata)
        {
            try
            {
                Encoding encoding = Encoding.Default;
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URL);
                request.Method = "post";
                request.Accept = "text/html, application/xhtml+xml, */*";
                request.ContentType = "application/x-www-form-urlencoded";
                byte[] buffer = encoding.GetBytes(strPostdata);
                request.ContentLength = buffer.Length;
                request.GetRequestStream().Write(buffer, 0, buffer.Length);
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                using (StreamReader reader = new StreamReader(response.GetResponseStream(), System.Text.Encoding.UTF8))
                {
                    return reader.ReadToEnd();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }


        
    }
}