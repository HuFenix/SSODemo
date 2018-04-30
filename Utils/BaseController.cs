using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Utils.CommonModel;

namespace Utils
{
    /// <summary>
    /// 控制器基类
    /// </summary>
    public class BaseController : Controller
    {
        //当前服务器地址
        protected string serURL = System.Configuration.ConfigurationManager.AppSettings["ServerURL"];
        //单点登录服务器地址
        protected string ssoURL = System.Configuration.ConfigurationManager.AppSettings["SSOAddress"];
        //登录时token
        protected string _token = "";
        //登录验证返回值(验证通过为用户名，验证失败为error，权限验证失败为roleError)
        protected string v = "";


        public ActionResult CreateCookie(string token, string redirect_url)
        {
            if (token != null && redirect_url != null)
            {
                HttpCookie cookie = new HttpCookie("currentUser");
                cookie.HttpOnly = true;
                cookie.Expires = DateTime.Now.AddYears(100);
                cookie.Value = token;
                Response.Cookies.Add(cookie);
                return Redirect(redirect_url);
            }
            else
            {
                return Json("error,Your Token or Url is missing!");
            }
        }

        protected List<string> GetSerInfo()
        {
            List<string> info = new List<string>()
            {
                 "请求开始时间：" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                 "服务器名称：" + Server.MachineName,//服务器名称  
                 "服务器IP地址：" + Request.ServerVariables["LOCAL_ADDR"],//服务器IP地址  
                 "HTTP访问端口：" + Request.ServerVariables["SERVER_PORT"],//HTTP访问端口"
                 ".NET解释引擎版本：" + ".NET CLR" + Environment.Version.Major + "." + Environment.Version.Minor + "." + Environment.Version.Build + "." + Environment.Version.Revision,//.NET解释引擎版本  
                 "服务器操作系统版本：" + Environment.OSVersion.ToString(),//服务器操作系统版本  
                 "服务器IIS版本：" + Request.ServerVariables["SERVER_SOFTWARE"],//服务器IIS版本  
                 "服务器域名：" + Request.ServerVariables["SERVER_NAME"],//服务器域名  
                 "虚拟目录的绝对路径：" + Request.ServerVariables["APPL_RHYSICAL_PATH"],//虚拟目录的绝对路径  
                 "执行文件的绝对路径：" + Request.ServerVariables["PATH_TRANSLATED"],//执行文件的绝对路径  
                 "虚拟目录Session总数：" + Session.Contents.Count.ToString(),//虚拟目录Session总数  
                 "域名主机：" + Request.ServerVariables["HTTP_HOST"],//域名主机  
                 "服务器区域语言：" + Request.ServerVariables["HTTP_ACCEPT_LANGUAGE"],//服务器区域语言  
                 "用户信息：" + Request.ServerVariables["HTTP_USER_AGENT"],
                 "CPU个数：" + Environment.GetEnvironmentVariable("NUMBER_OF_PROCESSORS"),//CPU个数  
                 "CPU类型：" + Environment.GetEnvironmentVariable("PROCESSOR_IDENTIFIER"),//CPU类型  
                 "请求来源地址：" + Request.Headers["X-Real-IP"]
        };
            return info;

        }

    }
}
