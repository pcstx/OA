//------------------------------------------------------------------------------
// <copyright company="Telligent Systems">
//     Copyright (c) Telligent Systems Corporation.  All rights reserved.
// </copyright> 
//------------------------------------------------------------------------------

using System;
using System.Collections;
using GPRP.GPRPEnumerations;


namespace GPRP.GPRPComponents {

	public class Ranks {

		public static ArrayList GetRanks()
		{
			return GetRanks(true);
		}

		public static ArrayList GetRanks(bool useCache) {
			string cacheKey = "RanksKey";
			ArrayList ranks = null;

			if(useCache)
				ranks = CSCache.Get(cacheKey) as ArrayList;

			if (ranks != null) 
				return ranks;

			CommonDataProvider dp = CommonDataProvider.Instance();
            ranks = dp.GetRanks();
			CSCache.Max(cacheKey, ranks);
			return ranks;
		}

		public static Rank GetRank( int rankId ) {
			return null;
		}

		public static int CreateRank( Rank rank ) {
			return CreateUpdateDeleteRank( rank, DataProviderAction.Create);
		}
		public static int DeleteRank( Rank rank ) {
			return CreateUpdateDeleteRank( rank, DataProviderAction.Delete);
		}
		public static int UpdateRank( Rank rank ) {
			return CreateUpdateDeleteRank( rank, DataProviderAction.Update);
		}

		private static int CreateUpdateDeleteRank( Rank rank, DataProviderAction action ) {

			CSCache.Remove("RanksKey");

		    CommonDataProvider dp = CommonDataProvider.Instance();

			return dp.CreateUpdateDeleteRank( rank, action );
		}
	}
}