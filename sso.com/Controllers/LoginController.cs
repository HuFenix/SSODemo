using sso.com.Common;
using sso.com.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Utils;

namespace sso.com.Controllers
{
    public class LoginController : Controller
    {
        // 登录
        public ActionResult Index(string redirect_url, string client_id = null)
        {
            //验证redirect_url是否可靠 TODO
            //验证client_id是否可信  TODO
            ViewBag.redirect_url = redirect_url;
            return View();
        }


        [HttpPost]
        public ActionResult Post(string name, string pwd, string redirect_url, string client_id = null)
        {
            //TODO 模拟账号
            var userList = RoleHelper.AccountInfo();

            //限制暴力登录-设置IP登录频率
            //验证登录名密码是否正确 TODO
            string token = "";
            var ret = new ReturnBaseModel();            

            if (userList.Exists(x => x.UserName == name))
            {
                if (pwd == "123")
                {
                    //1写cache
                    token = name + "_" + Guid.NewGuid().ToString().Substring(4, 12) + DateTime.Now.Millisecond;
                    token = Common.Common.EncryptMD5(token);
                    //将用户登录信息保存在cache中，有效时间一分钟
                    Utils.CacheHelper.Insert(token, name, 1);
                    //生成token--应该以更复杂的形式生成

                    //2写cookie
                    HttpCookie cookie = new HttpCookie("currentUser");
                    cookie.HttpOnly = true;
                    cookie.Expires = DateTime.Now.AddYears(100);//永不过期
                    cookie.Value = token;
                    Response.Cookies.Add(cookie);
                    ret.ReturnCode = "1"; ret.ReturnMsg = redirect_url;                   
                }
                else
                {
                    ret.ReturnCode = "-1"; ret.ReturnMsg = "密码有误";
                }

            }
            else
            {
                ret.ReturnCode = "-1"; ret.ReturnMsg = "账号未注册";
            }     
            


            //return Redirect(acom+"&others="+substation+"&main="+redirect_url);

            return Json(ret);

        }


        //验证是否登录--每个需要登录的页面都要调用
        [HttpPost]
        public string validateLogin(string token)
        {
            //验证IP是否可靠

            var v = Utils.CacheHelper.Get(token);
            if (v == null)
            {
                return "error";
            }
            //更新缓存过期时间
            Utils.CacheHelper.Remove(token);
            Utils.CacheHelper.Insert(token, v, 1);
            //TODO:将用户相关的信息返回
            return v.ToString();
        }

        public ActionResult LoginOut(string redirect_url, string token)
        {
            Utils.CacheHelper.Remove(token);
            return Redirect(redirect_url);
        }

    }
}