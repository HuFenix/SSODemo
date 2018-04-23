using DataEntity;
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

namespace c.com.Controllers
{
    public class HomeController : BaseController
    {
        //需要登录的页面-TODO
        public ActionResult Index()
        {
            //C
            var systemNo = "c";//系统识别代码
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
            var tenantModel = TenantHelper.GetTenantInfo(v, ssoURL);

            if (tenantModel != null)
            {
                ViewBag.TenantId = tenantModel.Tenant_id;
                ViewBag.Name = tenantModel.Name;
            }
            if (tenantModel != null)
            {
                var _dbContext = new SSoTestEntities(tenantModel.Tenant_id);
                var res = _dbContext.Products.ToList();
                ViewBag.ProCount = res.Count();
            }


            ViewBag.v = v;
            return View();
        }



    }
}