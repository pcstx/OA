//------------------------------------------------------------------------------
// <copyright company="Telligent Systems">
//     Copyright (c) Telligent Systems Corporation.  All rights reserved.
// </copyright> 
//------------------------------------------------------------------------------

using System;
using System.Web;
using System.Collections;
using GPRP.GPRPEnumerations;

namespace GPRP.GPRPComponents {

	public class Smilies {

		private static readonly String cacheKey = "CommunityServer-Emoticons";

		public static ArrayList GetSmilies() {
			return GetSmilies( true );
		}

		public static ArrayList GetSmilies( bool pullFromCache ) {
			ArrayList smilies = null;

            // If we're compiled with debug code we never cache
            //
            #if DEBUG_NOCACHE
                pullFromCache = false;
            #endif

            // Attempt to fetch from cache
			if( pullFromCache )
				smilies = CSCache.Get(cacheKey) as ArrayList;

            if (smilies == null) {

                CommonDataProvider dp = CommonDataProvider.Instance();

                smilies = dp.GetSmilies();

                CSCache.Insert(cacheKey,smilies,CSCache.HourFactor);
            } 

            return smilies;

		}

		public static Smiley GetSmiley( int smileyId ) {
			    
            ArrayList smilies = Smilies.GetSmilies(true);

            foreach (Smiley smiley in smilies) 
            {
                if (smiley.SmileyId == smileyId)
                    return smiley;
            }

            return null;
		}

		public static int CreateSmiley( Smiley smiley ) {
			CSCache.Remove( cacheKey );
			return CreateUpdateDeleteSmiley( smiley, DataProviderAction.Create);
		}
		public static int DeleteSmiley( Smiley smiley ) {
			CSCache.Remove( cacheKey );
			return CreateUpdateDeleteSmiley( smiley, DataProviderAction.Delete);
		}
		public static int UpdateSmiley( Smiley smiley ) {
			CSCache.Remove( cacheKey );
			return CreateUpdateDeleteSmiley( smiley, DataProviderAction.Update);
		}

		private static int CreateUpdateDeleteSmiley( Smiley smiley, DataProviderAction action ) {
		    CommonDataProvider dp = CommonDataProvider.Instance();

			return dp.CreateUpdateDeleteSmiley( smiley, action );
		}
	}
}