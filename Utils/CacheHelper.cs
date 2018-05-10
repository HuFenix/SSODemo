using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Caching;

namespace Utils
{
    /// <summary>
    ///  Cache帮助类
    /// </summary>
    public class CacheHelper
    {
        /// <summary>
        /// 创建缓存项
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void Insert(string key, object value)
        {

            HttpContext.Current.Cache.Insert(key, value);
        }
        /// <summary>
        /// 创建缓存项的文件依赖
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="fileName">文件绝对路径</param>
        public static void Insert(string key, object value,string fileName)
        {
            CacheDependency dep = new CacheDependency(fileName);
            HttpContext.Current.Cache.Insert(key, value,dep);
        }
        /// <summary>
        /// 创建缓存项过期时间（分钟）
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="expires"></param>
        //public static void Insert(string key, object value, int expires)
        //{
        //    HttpContext.Current.Cache.Insert(key, value, null, Cache.NoAbsoluteExpiration, new TimeSpan(0, expires, 0));
           
        //}
        public static void Insert(string cacheKey, object content, int timeOut = 3600)
        {
            try
            {
                if (content == null)
                {
                    return;
                }
                var objCache = HttpRuntime.Cache;
                //设置绝对过期时间
                //绝对时间过期。DateTime.Now.AddSeconds(10)表示缓存在3600秒后过期，TimeSpan.Zero表示不使用平滑过期策略。
                objCache.Insert(cacheKey, content, null, DateTime.Now.AddSeconds(timeOut), TimeSpan.Zero, CacheItemPriority.High, null);
                //相对过期
                //DateTime.MaxValue表示不使用绝对时间过期策略，TimeSpan.FromSeconds(10)表示缓存连续10秒没有访问就过期。
                //objCache.Insert(cacheKey, objObject, null, DateTime.MaxValue, timeout, CacheItemPriority.NotRemovable, null); 
            }
            catch (Exception)
            {

                throw;
            }
        }
        public static object Get(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                return null;
            }
            return HttpContext.Current.Cache.Get(key);
        }

        public static T Get<T>(string key)
        {
            object obj = Get(key);
            return obj == null ? default(T) : (T)obj;
        }
        public static object Remove(string key)
        {
          return  HttpContext.Current.Cache.Remove(key);
        }
    }
}
