using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
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

        /// <summary>
        /// 创建Cookie，当前服务器无租户
        /// </summary>
        /// <param name="token"></param>
        /// <param name="redirect_url"></param>
        /// <param name="Tenant_id"></param>
        /// <param name="Tenant_name"></param>
        /// <returns></returns>
        public ActionResult CreateCookieWithoutTenant(string token, string redirect_url, string Tenant_id, string Tenant_name)
        {
            if (token != null && redirect_url != null)
            {
                HttpCookie cookie = new HttpCookie("currentUser");
                cookie.HttpOnly = true;
                cookie.Expires = DateTime.Now.AddYears(100);
                cookie.Value = token;
                Response.Cookies.Add(cookie);
                Utils.CacheHelper.Insert(Tenant_id, new Tenants { Tenant_id = Tenant_id, Name = Tenant_name, CreatDate = DateTime.Now }, 300);
                return Redirect(redirect_url);
            }
            else
            {
                return Json("error,Your Token or Url is missing!");
            }
        }

        /// <summary>
        /// 创建COOKIE ，当前服务器有租户
        /// </summary>
        /// <param name="token"></param>
        /// <param name="redirect_url"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 通过用户获取租户信息，该服务器验证时使用
        /// </summary>
        /// <param name="tId">用户名</param>
        /// <returns></returns>
        public TenantsVM GetTenantInfo(string tId)
        {
            var v = Utils.CacheHelper.Get(tId);
            var res = JsonConvert.SerializeObject(v);
            if (res == "" || res == "null")
            {
                return null;
            }
            else
            {
                var data = JsonConvert.DeserializeObject<TenantsVM>(res);
                var retData = new TenantsVM()
                {
                    Name = data.Name,
                    Tenant_id = data.Tenant_id,
                    CreatDate = data.CreatDate
                };
                return retData;

            }

        }


        /// <summary>
        /// 通过用户获取租户信息Post (ToJson) ,SSO登录时租户遍历时调用
        /// </summary>
        /// <param name="tId">用户名</param>
        /// <returns></returns>
        [HttpPost]
        public string GetTenantInfoToJson(string tId)
        {
            var v = Utils.CacheHelper.Get(tId);
            return JsonConvert.SerializeObject(v);
            

        }


        /// <summary>
        /// 获取服务器压力信息
        /// </summary>
        /// <returns></returns>
        public string ServerLoadInfo()
        {
            var sys = new SystemInfoHelper();
            var model = new SysInfo();
            model.CpuCount = sys.ProcessorCount;
            model.CpuLoad = Math.Round((double)sys.CpuLoad, 2);
            model.MemoryAvailable = sys.MemoryAvailable;
            model.PhysicalMemory = sys.PhysicalMemory;
            return  JsonConvert.SerializeObject(model);
        }

        /// <summary>
        /// 清除租户cache
        /// </summary>
        /// <param name="tId"></param>
        /// <returns></returns>
        public string CleanTenant(string tId)
        {
            var res = Utils.CacheHelper.Remove(tId);
            if(res != null)
            {
                return "success";
            }
            else
            {
                return "error";
            }            
        }


        /// <summary>
        /// 遍历服务器 返回有当前租户cache的服务器地址
        /// </summary>
        /// <param name="tId">租户Id</param>
        /// <returns></returns>
        public string GetCurrentTenantInfoAddr(string tId)
        {
            var resUrl = "";
            IDictionary dict = ConfigurationManager.GetSection("ApplicationServers") as IDictionary;
            if (dict != null)
            {
                foreach (DictionaryEntry e in dict)
                {
                    var res = HttpHelper.OpenReadWithHttps(e.Value + "/Base/GetTenantInfoToJson", "tId=" + tId);
                    if (res != "null")
                    {
                        resUrl = e.Value.ToString();
                    }

                }
            }
            return resUrl;
        }

        public class Tenants
        {
            public string Tenant_id { get; set; }
            public string Name { get; set; }
            public DateTime CreatDate { get; set; }
        }
    }
}
