using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Utils;
using Utils.CommonModel;

namespace a.com.Controllers
{
    public class HomeController : BaseController
    {
        //需要登录的页面-TODO
        public ActionResult Index()
        {
            //A            
            var systemNo = "a";//系统识别代码
            ViewBag.ser = serURL;
            ViewBag.sso = ssoURL;    
            var requestCookies = Request.Cookies["currentUser"];
            if (requestCookies != null)
            {
                ViewBag.token = requestCookies.Value;
            }


            //验证权限
            v = RoleHelper.CheckRole(systemNo, ViewBag.token);

            //获取租户信息           
            var resModel = TenantHelper.GetTenantInfo(v, ssoURL);
            if (resModel != null)
            {
                ViewBag.TenantId = resModel.Tenant_id;
                ViewBag.Name = resModel.Name;
            }


            ViewBag.v = v;
            return View();
        }
        //public ActionResult ProductList()
        //{
        //    var res = _dbContext.Products.ToList();
        //    return View();
        //} 

    }


}