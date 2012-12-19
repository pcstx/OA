//------------------------------------------------------------------------------
// <copyright company="Telligent Systems">
//     Copyright (c) Telligent Systems Corporation.  All rights reserved.
// </copyright> 
//------------------------------------------------------------------------------

using System;
using System.Text;
using System.Web;
using System.Web.Caching;
using System.Web.Security;
using System.Collections;
using System.Text.RegularExpressions;

using GPRP.GPRPEnumerations;

namespace GPRP.GPRPComponents
{
	/// <summary>
	/// Summary description for DisallowedNames.
	/// </summary>
	public class DisallowedNames
	{
    #region Check Name
    // ********************************************************************/
    //  NameIsDisallowed
    //
    /// <summary>
    /// Check out if provided name was previously disallowed.
    /// <param name="name">Name to check for</param>
    /// <returns>True if the name has been disallowed, otherwise False.</returns>
    /// </summary>
    /// 
    // ********************************************************************/
    public static bool NameIsDisallowed(string name)
    {
      string regExWildcard = ResourceManager.GetString("ManageNames_RegExWildcardReplacement");
      if(regExWildcard == null)
        regExWildcard = "[a-zA-Z0-9]*";

      // If no names are cached then every name is valid
      ArrayList bannedNames = GetNames();
      if(bannedNames == null)
        return false;

      bool found = false;
      RegexOptions regExOpt = RegexOptions.IgnoreCase;

      IEnumerator tmpEnum = bannedNames.GetEnumerator();
      while( tmpEnum.MoveNext() ) {

        string thePattern = @"^" + tmpEnum.Current.ToString().Replace("*", regExWildcard) + @"$";
        Regex regEx = new Regex(thePattern, regExOpt);

        // Check for match
        if( regEx.IsMatch(name) == true ) {
          found = true;
          break;
        }
      }

      return found;
    }
    #endregion

    #region Get Names
    // ********************************************************************/
    //  GetNames
    //
    /// <summary>
    /// Get the list of current disallowed names.
    /// </summary>
    /// 
    // ********************************************************************/
    public static ArrayList GetNames()
    {

      // Serve from the cache when possible
      //
      ArrayList namesCollection = CSCache.Get(cacheKey) as ArrayList;
      if(namesCollection == null) {

          CommonDataProvider dp = CommonDataProvider.Instance();
        namesCollection = dp.GetDisallowedNames();
        if(namesCollection != null) {

            // Insert the user collection into the cache for 120 seconds
            CSCache.Insert(cacheKey, namesCollection,2 * CSCache.MinuteFactor);
        }
      }
        
      return namesCollection;
    }      
    #endregion

    #region Manage Names: Create/Update/Delete
    // ********************************************************************/
    //  CreateName
    //
    /// <summary>
    /// Add new disallowed name to data store.
    /// <param name="newName">The name that will be added</param>
    /// <returns>No of new rows</returns>
    /// </summary>
    /// 
    // ********************************************************************/
    public static int CreateName(string newName) {
		CSCache.Remove( cacheKey );
      return CreateUpdateDeleteName(newName, null, DataProviderAction.Create);
    }      

    // ********************************************************************/
    //  UpdateName
    //
    /// <summary>
    /// Update selected disallowed name with a new name in the data store.
    /// <param name="newName">The changing name</param>
    /// <param name="oldName">The changed name</param>
    /// <returns>No of updated rows</returns>
    /// </summary>
    /// 
    // ********************************************************************/
    public static int UpdateName(string newName, string oldName) {
		CSCache.Remove( cacheKey );
		return CreateUpdateDeleteName(oldName, newName, DataProviderAction.Update);
    }      

    // ********************************************************************/
    //  DeleteName
    //
    /// <summary>
    /// Delete an existing disallowe name from the data store.
    /// <param name="name">The name that will be deleted</param>
    /// <returns>No of deleted rows</returns>
    /// </summary>
    /// 
    // ********************************************************************/
    public static int DeleteName(string name) {
		CSCache.Remove( cacheKey );
		return CreateUpdateDeleteName(name, null, DataProviderAction.Delete);
    }

    private static int CreateUpdateDeleteName(string name, string replacement, DataProviderAction action ) {
		CSCache.Remove( cacheKey );
		
        CommonDataProvider dp = CommonDataProvider.Instance();

      return dp.CreateUpdateDeleteDisallowedName(name, replacement, action);
    }
    #endregion

	private static String cacheKey = "CommunityServer-DisallowedNames";
	}
}
