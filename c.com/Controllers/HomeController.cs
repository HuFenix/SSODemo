using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Utils;

namespace c.com.Controllers
{
    public class HomeController : Controller
    {
        //需要登录的页面-TODO
        public ActionResult Index()
        {
            //C
            var v = "";//页面返回状态
            var systemNo = "c";//系统识别代码

            var requestCookies = Request.Cookies["currentUser"];
            HttpCookie cookie = new HttpCookie("currentUser");
            cookie.HttpOnly = true;
            cookie.Expires = DateTime.Now.AddYears(100);
            if (requestCookies != null)
            {
                ViewBag.token = requestCookies.Value;
            }
            cookie.Value = ViewBag.token;
            Response.Cookies.Add(cookie);

            //验证权限
            v = RoleHelper.CheckRole(systemNo, ViewBag.token);
           
            ViewBag.v = v;
            return View();
        }



    }
}