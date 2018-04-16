using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using Utils;

namespace TenantDemo.Controllers
{
    public class TenantInfoController : Controller
    {
        private SSoTestEntities _dbcontext;
        public TenantInfoController()
        {
            _dbcontext = new SSoTestEntities();
        }

        public ActionResult Index()
        {
            var urlData = new List<string>() { "weba.sys.atjubo.com", "webb.sys.atjubo.com", "webc.sys.atjubo.com" };
            var tenantName = urlData.OrderBy(_ => Guid.NewGuid()).First();
            tenantName = tenantName.Split(new char[] { '.' })[0];
            var tenantId = EncryptHelper.Md5(tenantName);
            //TODO 通过数据库查询tennat 信息
            Utils.CacheHelper.Insert(tenantId, new Tenants { Id = 1, Tenant_id = tenantId, Name = "A租户", CreatDate = DateTime.Now });
            var data = _dbcontext.Tenants.Where(x => x.Tenant_id == tenantId).FirstOrDefault();
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}