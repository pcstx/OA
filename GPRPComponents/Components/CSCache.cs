//------------------------------------------------------------------------------
// <copyright company="Telligent Systems">
//     Copyright (c) Telligent Systems Corporation.  All rights reserved.
// </copyright> 
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Caching;




namespace GPRP.GPRPComponents
{
	/// <summary>
	/// Summary description for CSCache.
	/// </summary>
	public class CSCache
	{
		private CSCache(){}

        //>> Based on Factor = 5 default value
        public static readonly int DayFactor = 17280;
        public static readonly int HourFactor = 720;
        public static readonly int MinuteFactor = 12;

        private static readonly Cache _cache;

        private static int Factor = 5;

		public static void ReSetFactor(int cacheFactor)
		{
			Factor = cacheFactor;
		}

        /// <summary>
        /// Static initializer should ensure we only have to look up the current cache
        /// instance once.
        /// </summary>
        static CSCache()
        {
            HttpContext context = HttpContext.Current;
            if(context != null)
            {
                _cache = context.Cache;
            }
            else
            {
                _cache = HttpRuntime.Cache;
            }
        }

        /// <summary>
        /// Removes all items from the Cache
        /// </summary>
        public static void Clear()
        {
            IDictionaryEnumerator CacheEnum = _cache.GetEnumerator();
            while(CacheEnum.MoveNext())
                _cache.Remove(CacheEnum.Key.ToString());

        }

        public static void RemoveByPattern(string pattern)
        {
            IDictionaryEnumerator CacheEnum = _cache.GetEnumerator();
            Regex regex = new Regex(pattern,RegexOptions.IgnoreCase|RegexOptions.Singleline|RegexOptions.Compiled);
            while(CacheEnum.MoveNext())
            {
                if(regex.IsMatch(CacheEnum.Key.ToString()))
                    _cache.Remove(CacheEnum.Key.ToString());
            }
        }

        /// <summary>
        /// Removes the specified key from the cache
        /// </summary>
        /// <param name="key"></param>
        public static void Remove(string key)
        {
            _cache.Remove(key);
        }

        /// <summary>
        /// Insert the current "obj" into the cache. 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="obj"></param>
        public static void Insert(string key, object obj)
        {
            Insert(key,obj,null,1);
        }

        public static void Insert(string key, object obj, CacheDependency dep)
        {
            Insert(key,obj,dep,HourFactor * 12);
        }

        public static void Insert(string key, object obj, int seconds)
        {
            Insert(key,obj,null,seconds);
        }

        public static void Insert(string key, object obj, int seconds, CacheItemPriority priority)
        {
            Insert(key,obj,null,seconds,priority);
        }

        public static void Insert(string key, object obj, CacheDependency dep, int seconds)
        {
           Insert(key,obj,dep,seconds,CacheItemPriority.Normal);
        }

        public static void Insert(string key, object obj, CacheDependency dep, int seconds, CacheItemPriority priority)
        {
            if(obj != null)
            {
                _cache.Insert(key,obj,dep,DateTime.Now.AddSeconds(Factor * seconds), TimeSpan.Zero,priority,null);
            }

        }

		public static void MicroInsert (string key, object obj, int secondFactor) 
		{
			if(obj != null) {
				_cache.Insert(key,obj,null,DateTime.Now.AddSeconds(Factor * secondFactor), TimeSpan.Zero);
			}
		}

        /// <summary>
        /// Insert an item into the cache for the Maximum allowed time
        /// </summary>
        /// <param name="key"></param>
        /// <param name="obj"></param>
        public static void Max(string key, object obj)
        {
            Max(key,obj,null);
        }

        public static void Max(string key, object obj, CacheDependency dep)
        {
            if(obj != null)
            {
                _cache.Insert(key,obj,dep,DateTime.MaxValue,TimeSpan.Zero,CacheItemPriority.AboveNormal,null);
            }
        }

        public static object Get(string key)
        {
            return _cache[key];
        }
    
	}
}
