//------------------------------------------------------------------------------
// <copyright company="Telligent Systems">
//     Copyright (c) Telligent Systems Corporation.  All rights reserved.
// </copyright> 
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Web;
using GPRP.GPRPEnumerations;

namespace GPRP.GPRPComponents
{
    public delegate void SiteSettingsListIterator(int settingsID);

	/// <summary>
	/// Summary description for SiteSettingsManager.
	/// </summary>
	public class SiteSettingsManager
	{
        private static readonly ArrayList _settingsList = new ArrayList();

        //Can not be instantiated
		private SiteSettingsManager()
		{
			
		}

        #region Loop

        /// <summary>
        /// Provides safe access to a list of current SettingsIDs
        /// </summary>
        /// <returns></returns>
        public static int[] GetSettingIDs()
        {
            lock(_settingsList.SyncRoot)
            {
                return (int[])_settingsList.ToArray(typeof(int));
            }
        }

        /// <summary>
        /// Enables the interation of all current SiteSettings
        /// </summary>
        /// <param name="iter">Meth which takes a single parameter SettingsID</param>
        public static void IterateSiteSettings(SiteSettingsListIterator iter)
        {
//            Hashtable ht = SiteSettingsManager.GetActiveSiteSettings();
//            if(ht == null || ht.Count == 0)
//                return;
//
//            
//			string[] keys = new string[ht.Count];
//			ht.Keys.CopyTo(keys, 0);

            int[] settingIDs = SiteSettingsManager.GetSettingIDs();
			for(int i = 0; i < settingIDs.Length; i++)
            {

				try
				{
					iter(settingIDs[i]);
                    
				}
				catch(Exception ex)
				{
					CSException csException = new CSException(CSExceptionType.UnknownError,string.Format("Iterator Failed. Type {0}. Method {1}. Reason {2}",iter.Method.DeclaringType, iter.Method.Name,ex.Message));
					csException.Log(settingIDs[i]);
					throw;
				}
            }
        }

        #endregion

        #region GetSiteSettings
        /// <summary>
        /// Look up for a given SiteSettings by APplicationName. If not found in the cache, it will be
        /// retrieved from the data provider and cached.
        /// </summary>
        static public SiteSettings GetSiteSettings() 
        {
            return GetSiteSettings(CSContext.Current.SiteUrl, HttpContext.Current);
        }

        /// <summary>
        /// Look up for a given SiteSettings by APplicationName. If not found in the cache, it will be
        /// retrieved from the data provider and cached.
        /// </summary>
        static public SiteSettings GetSiteSettings(string applicationName) 
        {
            return GetSiteSettings(applicationName, HttpContext.Current);
        }

    
        /// <summary>
        /// Look up for a given SiteSettings by APplicationName. If not found in the cache, it will be
        /// retrieved from the data provider and cached.
        /// </summary>
        static public SiteSettings GetSiteSettings (HttpContext context) 
        {
            return GetSiteSettings(CSContext.Current.SiteUrl, context);
        }

        /// <summary>
        /// Look up for a given SiteSettings by APplicationName. If not found in the cache, it will be
        /// retrieved from the data provider and cached.
        /// </summary>
        static public SiteSettings GetSiteSettings (string applicationName, HttpContext context) 
        {
                        
            applicationName = applicationName.ToLower();

            Hashtable ht = GetActiveSiteSettings();
            
            SiteSettings settings = ht[applicationName] as SiteSettings;

            //Add a test for site.com/
            if(settings == null)
                settings = ht[applicationName + "/"] as SiteSettings;

            if(settings == null)
            {
                CommonDataProvider dp = CommonDataProvider.Instance();
                settings = dp.LoadSiteSettings(applicationName);

				if(settings != null)
				{
                    lock(ht.SyncRoot)
                    {
                        if(!ht.Contains(applicationName))
                            ht.Add(applicationName,settings);

                        lock(_settingsList.SyncRoot)
                        {
                            if(!_settingsList.Contains(settings.SettingsID))
                                _settingsList.Add(settings.SettingsID);
                        }

                    }
				}
				else
					throw new CSException(CSExceptionType.UnRegisteredSite,applicationName);
            }
            return settings;
            
        }

        

        /// <summary>
        /// Returns a collection of SiteSettings which exist in the current Application Domain.
        /// </summary>
        /// <returns></returns>
        public static Hashtable GetActiveSiteSettings()
        {
            //Note: We cache/store by applicationName. More than one application name
            //may share the same sitesettings object. Unless we start running thousands of
            //sites in the same appDomain, this secondary look up is probably worth it. 

            const string siteSettingsCacheKey = "SiteSettings";
            Hashtable ht = CSCache.Get(siteSettingsCacheKey) as Hashtable;
            if(ht == null)
            {
                ht = new Hashtable();
                CSCache.Insert(siteSettingsCacheKey, ht, null, CSCache.MinuteFactor * 15);
            }
            return ht;
        }

        static public void Save(SiteSettings settings) 
        {
            CommonDataProvider dp = CommonDataProvider.Instance();
            dp.SaveSiteSettings(settings);
        }

        /// <summary>
        /// Returns all of the site settings in the datastore
        /// </summary>
        /// <returns></returns>
        public static ArrayList AllSiteSettings () 
        {
        
            // Create Instance of the CommonDataProvider
            CommonDataProvider dp = CommonDataProvider.Instance();
        
            return dp.LoadAllSiteSettings();
        
        }


        #endregion

	}
}
