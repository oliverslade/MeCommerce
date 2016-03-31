using System;
using System.Web;

namespace MeCommerce.Chaching
{
    public static class CacheManager
    {
        public static void Add<X>(X obj, string key)
        {
            HttpContext.Current.Cache.Insert(key, obj, null, DateTime.Now.AddDays(7), System.Web.Caching.Cache.NoSlidingExpiration);
        }

        public static void Delete(string key)
        {
            if (Exists(key))
            {
                HttpContext.Current.Cache.Remove(key);
            }
        }

        public static bool Exists(string key)
        {
            try
            {
                return HttpContext.Current.Cache[key] != null;
            }
            catch
            {
                return false;
            }
        }

        public static X Get<X>(string key) where X : class
        {
            try
            {
                return (X)HttpContext.Current.Cache[key];
            }
            catch
            {
                return null;
            }
        }
    }
}