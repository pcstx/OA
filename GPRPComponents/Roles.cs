//------------------------------------------------------------------------------
// <copyright company="Telligent Systems">
//     Copyright (c) Telligent Systems Corporation.  All rights reserved.
// </copyright> 
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Security.Principal;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Caching;
using System.Web.Security;
using GPRP.GPRPEnumerations;
using ms = Microsoft.ScalableHosting.Security;

namespace GPRP.GPRPComponents {

    // *********************************************************************
    //  Roles
    //
    /// <summary>
    /// The user roles class is used to manage user to role mappings.
    /// </summary>
    // ***********************************************************************/
    public class Roles {

        private static readonly string defaultRoles = null;
        private static readonly RolesConfiguration rolesConfig = null;

        static Roles()
        {
            rolesConfig = CSConfiguration.GetConfig().RolesConfiguration;
            defaultRoles = rolesConfig.RoleList();
        }

		#region Role Types

        public static string Everyone
        {
            get{return rolesConfig.Everyone;}
		}

        public static string RegisteredUsers
        {
            get {  return rolesConfig.RegisteredUsers; }
        }

		/// <summary>
        /// Property SystemAdmin (string)
        /// </summary>
        public static string SystemAdministrator
        {
            get  {  return rolesConfig.SystemAdministrator; }
        }

		/// <summary>
        /// Property Moderator (string)
        /// </summary>
        public static string Moderator
        {
            get  {  return rolesConfig.Moderator; }
        }

		/// <summary>
        /// Property Editor (string)
        /// </summary>
        public static string Editor
        {
            get {  return rolesConfig.Editor; }
        }

		/// <summary>
        /// Property ForumsAdministrator (string)
        /// </summary>
        public static string ForumsAdministrator 
        {
            get  {  return rolesConfig.ForumsAdministrator; }
        }

		/// <summary>
        /// Property BlogAdministrator (string)
        /// </summary>
        public static string BlogAdministrator
        {
            get  {  return rolesConfig.BlogAdministrator; }
        }

		/// <summary>
        /// Property GalleryAdministrator (string)
        /// </summary>
        public static string GalleryAdministrator
        {
            get  {  return rolesConfig.GalleryAdministrator; }
        }

		#endregion

        // *********************************************************************
        //  IsBuiltInRole
        //
        /// <summary>
        /// Returns a list of default system roles. These roles can not be edited, although there
        /// names can be configured in the web.config RoleConfiguration section
        /// </summary>
        // ***********************************************************************/
        public static bool IsBuiltInRole(string roleName)
        {
            return Regex.IsMatch(roleName,defaultRoles,RegexOptions.IgnoreCase);
        }

        // *********************************************************************
        //  GetUserRoles
        //
        /// <summary>
        /// Connects to the user role's datasource, retrieves all the roles a given
        /// user belongs to, and add them to the curret IPrincipal. The roles are retrieved
        /// from the datasource or from an encrypted cookie.
        /// </summary>
        // ***********************************************************************/
		public void GetUserRoles() 
		{
			CSContext csContext = CSContext.Current;
        	HttpContext context = csContext.Context;
			string[] roles = null;

			// Is the request authenticated?
			//
			if (!context.Request.IsAuthenticated)
				return;

			// Get the roles this user is in
			//
			if ((context.Request.Cookies[CSContext.Current.SiteSettings.RoleCookieName] == null) || (context.Request.Cookies[CSContext.Current.SiteSettings.RoleCookieName].Value == "")) 
			{
				// Get roles from UserRoles table, and add to cookie
				//
				roles = Roles.GetUserRoleNames(CSContext.Current.User.Username);
				CreateRolesCookie(roles);
			} 
			else 
			{
				// Get roles from roles cookie
				//
				FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(context.Request.Cookies[CSContext.Current.SiteSettings.RoleCookieName].Value);

				// Ensure the user logged in and the user the cookie was issued to are the same
				//
				if (ticket.Name != csContext.UserName) 
				{
					// Get roles from UserRoles table, and add to cookie
					//
					roles = Roles.GetUserRoleNames(CSContext.Current.User.Username);
					CreateRolesCookie(roles);
				} 
				else 
				{
					// Convert the string representation of the role data into a string array
					//
					roles = ticket.UserData.Split(';');
				}
			}

			//Not very clean here.
			//TODO: Where should this be set?
			csContext.RolesCacheKey = string.Join(";", roles);

			// Add our own custom principal to the request containing the roles in the auth ticket
			//
			context.User = new GenericPrincipal(context.User.Identity, roles);
		}

        #region UsersInRole
        public static UserSet UsersInRole (int pageIndex, int pageSize, SortUsersBy sortBy, SortOrder sortOrder, Guid roleID) {
            return UsersInRole(pageIndex, pageSize, sortBy, sortOrder, roleID, true, UserAccountStatus.Approved, true);
        }

        public static UserSet UsersInRole (int pageIndex, int pageSize, SortUsersBy sortBy, SortOrder sortOrder, Guid roleID, bool cacheable, UserAccountStatus accountStatus, bool returnRecordCount) {

            UserSet u = null;

            // build a unique cache key
        	StringBuilder s = new StringBuilder();
            s.Append("UsersInRole-");
            s.Append(pageIndex.ToString());
            s.Append(pageSize.ToString());
            s.Append(sortBy.ToString());
            s.Append(sortOrder.ToString());
            s.Append(roleID.ToString());
            s.Append(accountStatus.ToString());
            s.Append(returnRecordCount.ToString());

            string cacheKey =  s.ToString();

            // Get the data from the data provider if not in the cache
            //
            u = CSCache.Get(cacheKey) as UserSet;
            if (u == null || !cacheable) {
                CommonDataProvider dp = CommonDataProvider.Instance();
                u = dp.UsersInRole(pageIndex, pageSize, sortBy, sortOrder, roleID, accountStatus, returnRecordCount);

                if (cacheable)
                    CSCache.Insert(cacheKey,u,12 * CSCache.HourFactor);
            }
            return u;
        }

        #endregion

        //*********************************************************************
        //
        // CreateRolesCookie
        //
        /// <summary>
        /// Used to create the cookie that store the roles for the current
        /// user.
        /// </summary>
        //
        //*********************************************************************
		private void CreateRolesCookie(string[] roles) 
		{
			CSContext csContext = CSContext.Current;
			HttpContext context = csContext.Context;

			// Is the roles cookie enabled?
			//
			if (!CSContext.Current.SiteSettings.EnableRoleCookie)
				return;

			// Create a string to persist the roles
			String roleStr = string.Join(";",roles);

			// Create a cookie authentication ticket.
			FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(
				1,                              // version
				csContext.UserName,             // user name
				DateTime.Now,                   // issue time
				DateTime.Now.AddHours(1),       // expires every hour
				false,                          // don't persist cookie
				roleStr                         // roles
				);

			// Encrypt the ticket
			String cookieStr = FormsAuthentication.Encrypt(ticket);

			// Send the cookie to the client
			context.Response.Cookies[CSContext.Current.SiteSettings.RoleCookieName].Value = cookieStr;
			context.Response.Cookies[CSContext.Current.SiteSettings.RoleCookieName].Path = "/";
			context.Response.Cookies[CSContext.Current.SiteSettings.RoleCookieName].Expires = DateTime.Now.AddMinutes(CSContext.Current.SiteSettings.RoleCookieExpiration);
		}

		#region Role Methods

		// *********************************************************************
		//  AddRole
		//
		/// <summary>
		/// Creates a new security role
		/// </summary>
		// ***********************************************************************/
		public static void AddRole(string roleName)
		{
			if(!Microsoft.ScalableHosting.Security.Roles.Provider.RoleExists(roleName))
				Microsoft.ScalableHosting.Security.Roles.Provider.CreateRole(roleName);
		}

		// *********************************************************************
		//  DeleteRole
		//
		/// <summary>
		/// Deletes a security role and any associated forum and user connections
		/// </summary>
		// ***********************************************************************/
		public static void DeleteRole(Role role) 
		{
			CommonDataProvider dp = CommonDataProvider.Instance();
			dp.CreateUpdateDeleteRole(role, Microsoft.ScalableHosting.Security.Roles.ApplicationName, DataProviderAction.Delete);
		}

		// *********************************************************************
		//  UpdateRole
		//
		/// <summary>
		/// Updates the description for a given role.
		/// </summary>
		// ***********************************************************************/
		public static void UpdateRole(Role role) 
		{
			// Cannot update a role with no name
			if (role.Name == null || role.Name.Length == 0) 
				return;
            
			CommonDataProvider dp = CommonDataProvider.Instance();
			dp.CreateUpdateDeleteRole(role,Microsoft.ScalableHosting.Security.Roles.ApplicationName,DataProviderAction.Update);
		}

		// *********************************************************************
		//  GetRole
		//
		/// <summary>
		/// Gets a role from a roleID
		/// </summary>
		// ***********************************************************************/
		public static Role GetRole(Guid roleID)
		{
			return GetRole(roleID, true);
		}

		// *********************************************************************
		//  GetRole
		//
		/// <summary>
		/// Gets a role from a roleID
		/// </summary>
		// ***********************************************************************/
		public static Role GetRole(Guid roleID, bool cacheable)
		{
			CSContext csContext = CSContext.Current;
			Hashtable roles = GetRoles(csContext, 0, cacheable, false);

			// Return the role
			Role role = roles[roleID] as Role;

			if(role != null)
				return role;
			
			// If we are still here, then the role doesn't exist
			throw new CSException(CSExceptionType.ResourceNotFound, "Role not found: " + roleID.ToString());
		}

		// *********************************************************************
		//  GetRole
		//
		/// <summary>
		/// Gets a role from a roleID
		/// </summary>
		// ***********************************************************************/
		public static Role GetRole(string roleName)
		{
			return GetRole(roleName, true);
		}

		// *********************************************************************
		//  GetRole
		//
		/// <summary>
		/// Gets a role from a roleID
		/// </summary>
		// ***********************************************************************/
		public static Role GetRole(string roleName, bool cacheable)
		{
			CSContext csContext = CSContext.Current;
			Hashtable roles = GetRoles(csContext, 0, cacheable, false);

			// Return the role
			foreach(Role role in roles.Values)
				if(role.Name == roleName)
					return role;

			// If we are still here, then the role doesn't exist
			throw new CSException(CSExceptionType.ResourceNotFound, "Role not found: " + roleName);
		}

		public static ArrayList GetRoles()
		{
			return GetRoles(0, true, false);
		}

		public static ArrayList GetRoles(bool cacheable)
		{
			return GetRoles(0, cacheable, false);
		}

		public static ArrayList GetRoles(int userID, bool cacheable)
		{
			return GetRoles(userID, cacheable, false);
		}

		public static ArrayList GetRoles(int userID, bool cacheable, bool flush)
		{
			CSContext csContext = CSContext.Current;
			ArrayList roles = new ArrayList();
			Hashtable rolesTable;

			// Get the Albums as a hashtable
			rolesTable = GetRoles(csContext, userID, cacheable, flush);

			foreach(Role role in rolesTable.Values)
				roles.Add(role);

			roles.Sort();

			return roles;
		}

		public static Hashtable GetRoles(CSContext csContext, int userID, bool cacheable, bool flush)
		{
			Hashtable roles;
			string cacheKey = string.Format("Roles:User:{0}:Site:{1}", userID, CSContext.Current.SiteSettings.SettingsID);

#if DEBUG_NOCACHE
            cacheable = false;
#endif

			if(flush)
			{
				CSCache.Remove(cacheKey);
				csContext.Items.Remove(cacheKey);
			}

			// See if this has already been requested
			roles = csContext.Items[cacheKey] as Hashtable;
			if(roles != null)
				return roles;

			// Ensure it's not in the cache
			if(!cacheable)
				CSCache.Remove(cacheKey);

			// Have we already fetched for this request?
			roles = CSCache.Get(cacheKey) as Hashtable;

			// Get the roles if it is not in the cache
			if(roles == null)
			{
				roles = CommonDataProvider.Instance().GetRoles(userID);

				// Cache if we can
				if (cacheable) {
					CSCache.Insert(cacheKey, roles, 30 * CSCache.MinuteFactor, CacheItemPriority.Normal);
				}
			} 

			// Insert into request cache
			csContext.Items.Add(cacheKey, roles);

			return roles;
		}

		public static string[] GetRoleNames()
		{
			string cacheKey = "RoleNames:Site:" + CSContext.Current.SiteSettings.SettingsID;
			string[] roles = CSCache.Get(cacheKey) as string[];

			if(roles == null)
			{
				roles = Microsoft.ScalableHosting.Security.Roles.Provider.GetAllRoles();
				CSCache.Insert(cacheKey, roles, 10 * CSCache.MinuteFactor);
			}

			return roles;
		}

		#endregion


		#region User/Role Methods

        // *********************************************************************
        //  AddUserToRole
        //
        /// <summary>
        /// Adds a specified user to a role
        /// </summary>
        // ***********************************************************************/
        public static void AddUserToRole(string userName, string roleName)
        {
        	Microsoft.ScalableHosting.Security.Roles.AddUserToRole(userName,roleName);
        }

        // *********************************************************************
        //  RemoveUserFromRole
        //
        /// <summary>
        /// Removes the specified user from a role
        /// </summary>
        // ***********************************************************************/
        public static void RemoveUserFromRole(string userName, string roleName) 
        {
        	Microsoft.ScalableHosting.Security.Roles.RemoveUserFromRole(userName,roleName);
        }

        public static string[] GetUserRoleNames(string username)
        {
            return GetUserRoleNames(username, true);
        }

        public static string[] GetUserRoleNames(string username, bool cacheable)
        {
            string[] roles  = null;
            string key = "UserRoleNames:" + username;

            if(cacheable)
                roles = CSCache.Get(key) as string[];
			else
				CSCache.Remove(key);

            if(roles == null)
            {

				// there is a situation where the cookie may be using an old username, which can cause this call to fail, at this point
				// we're too deep in the call tree to do anything else, so we just need to log the exception and force the
				// user to signout since we're having problems pulling the user's roles from the database
				try{
					roles = Microsoft.ScalableHosting.Security.Roles.GetRolesForUser(username);

					if(cacheable) 
						CSCache.Insert(key, roles, 10 * CSCache.MinuteFactor);
				}
				catch( Exception e ) {
					CSException cse = new CSException( CSExceptionType.RoleNotFound, String.Format("Error while trying to find a role for the user '{0}'. Possible cause is a invalid client cookie or a user rename.", username ), e );
					cse.Log();

					FormsAuthentication.SignOut();
					HttpContext.Current.Response.Redirect( SiteUrls.Instance().Home );
				}
            }

            return roles;
        }

		#endregion


        // *********************************************************************
        //  SignOut
        //
        /// <summary>
        /// Cleans up cookies used for role management when the user signs out.
        /// </summary>
        // ***********************************************************************/
        public static void SignOut() {
            HttpContext Context = HttpContext.Current;

            // Invalidate roles token
            Context.Response.Cookies[CSContext.Current.SiteSettings.RoleCookieName].Value = null;
            Context.Response.Cookies[CSContext.Current.SiteSettings.RoleCookieName].Expires = new DateTime(1999, 10, 12);
            Context.Response.Cookies[CSContext.Current.SiteSettings.RoleCookieName].Path = "/";
        }

		protected static void ClearUserRoleCache( int userID ) {
			// force a refresh of the users role cache
			string key = "UserRoles-" + userID.ToString();
			if( CSContext.Current.Context.Cache[key] != null ) {
			    CSContext.Current.Context.Cache.Remove( key );								
			}
		}
    }
}