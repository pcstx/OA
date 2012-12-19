//------------------------------------------------------------------------------
// <copyright company="Telligent Systems">
//     Copyright (c) Telligent Systems Corporation.  All rights reserved.
// </copyright> 
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Web.Caching;
using GPRP.GPRPEnumerations;


namespace GPRP.GPRPComponents
{

	public class Censors {

		public static ArrayList GetCensors() {
			
			string key = CacheKey;
			ArrayList censors = CSCache.Get(key) as ArrayList;
			if(censors == null)
			{
				CommonDataProvider dp = CommonDataProvider.Instance();
				censors = dp.GetCensors();
				CSCache.Insert(key,censors,15*CSCache.MinuteFactor,CacheItemPriority.Low);
			}

			return censors;
		}

		private static string CacheKey
		{
			get { return "Censors:" + CSContext.Current.SiteSettings.SettingsID; }
		}

		public static Censor GetCensor( string censorId ) {
			return null;
		}

		public static int CreateCensor( Censor censor ) {
			CSCache.Remove(CacheKey);
			return CreateUpdateDeleteCensor( censor, DataProviderAction.Create);
		}
		public static int DeleteCensor( Censor censor ) {
			CSCache.Remove(CacheKey);
			return CreateUpdateDeleteCensor( censor, DataProviderAction.Delete);
		}
		public static int UpdateCensor( Censor censor ) {
			CSCache.Remove(CacheKey);
			return CreateUpdateDeleteCensor( censor, DataProviderAction.Update);
		}

		private static int CreateUpdateDeleteCensor( Censor censor, DataProviderAction action ) {
		    CSCache.Remove(CacheKey);
			CommonDataProvider dp = CommonDataProvider.Instance();
			return dp.CreateUpdateDeleteCensor( censor, action );
		}
	}
}