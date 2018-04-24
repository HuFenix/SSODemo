using DataEntity;
using DataEntity.EntityModel;
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
     
        private string systemNo = "a";//系统识别代码
        private TenantsVM _tenantModel =new TenantsVM();

        public ActionResult Index()
        {
           
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
            var res = new List<Products>();
            if (tenantModel != null)
            {
                ViewBag.TenantId = tenantModel.Tenant_id;
                ViewBag.Name = tenantModel.Name; var _dbContext = new SSoTestEntities(tenantModel.Tenant_id);
                res = _dbContext.Products.ToList();
            }

            ViewBag.v = v;
            return View(res);

        }

      

    }


}