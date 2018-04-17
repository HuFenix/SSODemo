using Newtonsoft.Json;
using sso.com.Common;
using sso.com.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using Utils;

namespace sso.com.Controllers
{
    public class LoginController : Controller
    {
        private SSoTestEntities1 _dbContext;
        public LoginController()
        {
            _dbContext = new SSoTestEntities1();
        }
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

                    //将用户登录信息保存在cache中，有效时间30分钟，用于验证登录
                    Utils.CacheHelper.Insert(token, name, 30);



                    
                    #region 租户识别
                    //根据登录判别所属租户TODO


                    var tenantId = userList.Where(x => x.UserName == name).Select(x => x.TenantId).FirstOrDefault() ;
                    //通过数据库查询tennat 信息
                    var tenantData = _dbContext.TenantsInfo.Where(x => x.Tenant_id == tenantId).FirstOrDefault();
                    if (tenantData != null)
                    {
                        Utils.CacheHelper.Insert(name, new Tenants { Id = tenantData.Id, Tenant_id = tenantData.Tenant_id, Name =  tenantData.Name, CreatDate = DateTime.Now },300);
                        ret.ReturnCode = "1"; ret.ReturnMsg = redirect_url + "?token=" + token;
                    }
                    else
                    {
                        ret.ReturnCode = "-1"; ret.ReturnMsg = "租户信息获取失败";
                    }
                   
                    #endregion 

                    
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

        /// <summary>
        /// 登出
        /// </summary>
        /// <param name="redirect_url">返回页面</param>
        /// <param name="token">识别码</param>
        /// <param name="name">商户名</param>
        /// <returns></returns>
        public ActionResult LoginOut(string redirect_url, string token,string name)
        {
            Utils.CacheHelper.Remove(token);
            Utils.CacheHelper.Remove(name);
            return Redirect(redirect_url);
        }

        /// <summary>
        /// 通过用户获取租户信息
        /// </summary>
        /// <param name="name">用户名</param>
        /// <returns></returns>
        [HttpPost]
        public string GetTenantInfo(string name)
        {
            var v = Utils.CacheHelper.Get(name);
            var data = JsonConvert.SerializeObject(v);            
            return data;
        }

    }
}