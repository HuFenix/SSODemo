using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Utils
{
    public class BaseController : Controller
    {
        protected string serURL = System.Configuration.ConfigurationManager.AppSettings["ServerURL"];
        protected string ssoURL = System.Configuration.ConfigurationManager.AppSettings["SSOAddress"];
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
