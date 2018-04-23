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
    }
}
