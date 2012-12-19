//------------------------------------------------------------------------------
// <copyright company="Telligent Systems">
//     Copyright (c) Telligent Systems Corporation.  All rights reserved.
// </copyright> 
//------------------------------------------------------------------------------

// 修改说明：新增用户属性
// 修改人：宝玉
// 修改日期：2005-02-26

using System;
using System.Text;
using System.Text.RegularExpressions;

using System.Web;
using System.Web.Caching;
using System.Web.Security;
using System.Collections;
using System.Security.Cryptography;

using ms = Microsoft.ScalableHosting.Security;
using GPRP.GPRPEnumerations;
namespace GPRP.GPRPComponents {


    // *********************************************************************
    //  Users
    //
    /// <summary>
    /// This class encapsulates all data operations for managing forum users.
    /// </summary>
    // ***********************************************************************/
    public class Users {


        # region GetUser methods
        // *********************************************************************
        //  GetUser
        //
        /// <summary>
        /// Return a User class for the specified user
        /// </summary>
        /// <returns>
        /// Instance of User with details about a given forum user.</returns>
        // ***********************************************************************/
        public static User GetUser()
        {
            return GetUser(0, GetLoggedOnUsername(), true, true);
        }

        public static User GetUser(bool isOnline)
        {
            return GetUser(0, GetLoggedOnUsername(), isOnline, true);
        }

        public static User GetUser(int userID, bool isOnline)
        {
            return GetUser(userID, null, isOnline, true);
        }

        public static User GetUser(int userID, bool isOnline, bool isCacheable)
        {
            return GetUser(userID, null, isOnline, isCacheable);
        }

        public static User GetUser(int userID, string username, bool isOnline, bool isCacheable)
        {
            return GetUser(userID, username, null, isOnline, isCacheable, true);
        }

        public static User GetUser(int userID, string username, string nickname, bool isOnline, bool isCacheable, bool autoCreateUser)
        {
            User user = null;

            // If the request is not authenticated return
            // a new user instance
            //
            if ((userID == 0) && (username == "Anonymous"))
            {
                user = Users.GetAnonymousUser();
                return user;
            }

            CSContext csContext = CSContext.Current;
            int maxOnlineAgeInSeconds = 30;
            int maxOfflineAgeInMinutes = 180;
            Hashtable userLookupTable;
            CachedUser cachedUser;
            const string cacheKey = "UserLookupTable";
            string userKey;

            // Attempt to get the user lookup table
            //
            userLookupTable = CSCache.Get(cacheKey) as Hashtable;
            if (userLookupTable == null)
            {
                userLookupTable = new Hashtable();
                CSCache.Insert(cacheKey, userLookupTable, 3 * CSCache.MinuteFactor);
            }

            // Create the user key for Cache/Context lookups
            //
            if (userID > 0)
                userKey = "User-" + userID;
            else if (username != null)
                userKey = "User-" + username;
            else
                userKey = "User-" + nickname;

            // If we're compiled with debug code we never cache
            //
#if DEBUG_NOCACHE
                isCacheable = false;
#endif

            // Attempt to return the user from ContextItems to save
            // us a trip to the database.
            //
            user = csContext.Items[userKey] as User;
            if (user != null)
                return user;

            //            if (csContext.Context.Items[userKey] != null)
            //                return (User) csContext.Context.Items[userKey];

            // Attempt to return the user from Cache to save
            // us a trip to the database.
            //
            if (userLookupTable[userKey] != null)
            {

                cachedUser = (CachedUser)userLookupTable[userKey];

                if (isOnline)
                {

                    if (cachedUser.Created > DateTime.Now.AddSeconds(-maxOnlineAgeInSeconds))
                        return cachedUser.User;
                    else
                        userLookupTable[userKey] = null;

                }
                else
                {

                    if (cachedUser.Created > DateTime.Now.AddMinutes(-maxOfflineAgeInMinutes))
                        return cachedUser.User;
                    else
                        userLookupTable[userKey] = null;

                }

            }

            // User was not in the Context.Items collection and not in the Cache
            // we need to go to the database and fetch the user
            //
            user = GetUserFromDataProvider(userID, username, nickname, isOnline, false, autoCreateUser);

            // A bit of error checking to ensure we have a new user.
            //
            // When GetUserFromDataProvider() failed to lookup an existing UserID from the
            // cookie stored, an error was thrown and this caused the very annoying
            // and very well known "Forms" bug showing up on any page.
            // This resolves that.  I caused GetUserFromDatProvider() to return null
            // if that happens now and we check for it below.
            //
            // TODO this should not be to log out the user, it should be in the page logic
            if (user == null)
            {
                /*
                if( HttpContext.Current					!= null &&
                    HttpContext.Current.User			!= null &&
                    HttpContext.Current.User.Identity	!= null &&
                    HttpContext.Current.User.Identity.GetType() == typeof(System.Web.Security.FormsIdentity)) 
                {
                    Roles.SignOut();
                    FormsAuthentication.SignOut();
                }
                */
                return new User();
            }


            // If we can't cache, add the user to Context items so we don't have
            // to retrieve the user again for the same request even if the user is
            // not cacheable
            //
            csContext.Items[userKey] = user;

            // If not cacheable, return
            //
            if (!isCacheable)
                return user;

            // Create a CachedUser struct
            //
            cachedUser.User = user;
            cachedUser.Created = DateTime.Now;

            // Add user to lookup table
            //
            userLookupTable[userKey] = cachedUser;

            return user;

        }

        public static User GetUserWithWriteableProfile()
        {
            return GetUserFromDataProvider(0,GetLoggedOnUsername(), null,false,true,false);
        }

        public static User GetUserWithWriteableProfile(int userID, string username, bool isOnline)
        {
            return GetUserFromDataProvider(userID,username, null,isOnline,true,false);
        }

        public static User GetUserByNickname(string nickname)
        {
            return GetUser(0, null, nickname, false, true, false);
        }

        private static User GetUserFromDataProvider(int userID, string username, string nickname, bool isOnline, bool isEditable, bool autoCreateUser)
        {

            // Get an instance of the CommonDataProvider
            //
            CommonDataProvider dp = CommonDataProvider.Instance();

            string lastAction = SiteUrls.LocationKey();

            // Attempt to get the user from the dataprovider layer
            //
            try
            {
                // 2005-02-27: 新增，记录最后活动IP, 新增根据昵称获取用户资料方法
                string ipAddress = Globals.IPAddress;
                return dp.GetUser(userID, username, nickname, isOnline, isEditable, lastAction, ipAddress);
            }
            catch (CSException)
            {
                CSContext csContext = CSContext.Current;

                if (autoCreateUser && csContext.IsWebRequest)
                {
                    if (Regex.IsMatch(csContext.Context.User.Identity.AuthenticationType, "^(Negotiate|Digest|Basic|NTLM)$"))
                    {

                        bool bSendEmail = false;

                        User newUser = new User();

                        // What type of account activation are we using?
                        //
                        if (csContext.SiteSettings.AccountActivation == AccountActivation.Email)
                        {
                            bSendEmail = true;
                            newUser.AccountStatus = UserAccountStatus.Approved;
                        }

                        if (csContext.SiteSettings.AccountActivation == AccountActivation.AdminApproval)
                        {
                            newUser.AccountStatus = UserAccountStatus.ApprovalPending;
                        }

                        //ToDo: Fix this
                        //newUser.Username = GetLoggedOnUsername();
                        newUser.Email = username.Substring(username.IndexOf("\\") + 1) + CSContext.Current.SiteSettings.EmailDomain;
                        //newUser.Password = "DomainGenerated";
                        CreateUserStatus myStatus = Create(newUser, bSendEmail);
                        if (myStatus == CreateUserStatus.Created)
                            return newUser;
                        else
                            return null;
                    }
                }
                // if we made it to here, we can't figure out the user
                //
                return null;
            }
        }

        // ***********************************************************************
        // GetCurrentUsername
        /// <summary>
        ///  Returns the user name without the domain, replaces forumContext.Username
        /// </summary>
        /// <returns>stripped username</returns>
        /// <remarks>
        /// Author:		PLePage
        /// Date:		07/09/03
        /// This strips the users domain from their name.  If the configuration 
        /// specifies a domain, it will remove it, * will remove all domains and 
        /// nothing will not remove any domain appendages.
        /// 
        /// History:
        /// 7/16/2004	Terry Denham	moddified the method to use the StripDomainName as
        ///		this needs to be handled before we actually connect to the database so that
        ///		the admin can configure the web.config file, hit the site and get logged in
        ///		and associated with the correct role. If we make it pull from the database
        /// </remarks>
        // ***********************************************************************/
        public static string GetLoggedOnUsername() {
            CSContext csContext = CSContext.Current;

//            string strDomain = CSContext.Current.SiteSettings.WindowsDomain;
            if (csContext.Context.User == null || csContext.Context.User.Identity.Name == string.Empty)
                return "Anonymous";

            string username = csContext.UserName;

			if( username.IndexOf("\\") > 0 &&
				CSContext.Current.SiteSettings.StripDomainName ) {

				username = username.Substring( username.LastIndexOf("\\") + 1 );
			}

			return username;

//            if (strDomain.Equals("*"))
//                return username.Substring(username.IndexOf("\\") + 1);
//            else {
//                if (username.ToUpper().StartsWith(strDomain + "\\"))
//                    return username.Substring(username.IndexOf("\\") + 1);
//                else
//                    return username;
//            }
        }

        #endregion

        #region WhoIsOnline
        // *********************************************************************
        //  WhoIsOnline
        //
        /// <summary>
        /// Returns a user collection of all the user's online. Lookup is only
        /// performed every 30 seconds.
        /// </summary>
        /// <param name="pastMinutes">How many minutes in time we should go back to return users.</param>
        /// <returns>A collection of user.</returns>
        /// 
        // ********************************************************************/
        public static ArrayList GetGuestsOnline (int pastMinutes) {
            return (ArrayList) GetMembersGuestsOnline (pastMinutes)["Guests"];
        }

        public static ArrayList GetUsersOnline (int pastMinutes) {
            return (ArrayList) GetMembersGuestsOnline (pastMinutes)["Members"];
        }

        private static Hashtable GetMembersGuestsOnline(int pastMinutes) {
            
            string cacheKey = "WhoIsOnline-" + pastMinutes.ToString();

            // Read from the cache if available
            Hashtable users = CSCache.Get(cacheKey) as Hashtable;
            if (users == null) {

                // Create Instance of the CommonDataProvider
                //
                CommonDataProvider dp = CommonDataProvider.Instance();

                // Get the users
                users = dp.WhoIsOnline(pastMinutes);

                // Add to the Cache
                CSCache.Insert(cacheKey, users, CSCache.MinuteFactor);

            }

            return users;

        }
        #endregion

        #region FindUserByEmail / Username
        // *********************************************************************
        //  FindUserByEmail
        //
        /// <summary>
        /// Returns a user given an email address.
        /// </summary>
        /// <param name="emailAddress">Email address to look up username by.</param>
        /// <returns>Username</returns>
        /// 
        // ********************************************************************/
        public static User FindUserByEmail(string emailAddress) {
            
            // Create Instance of the CommonDataProvider
            CommonDataProvider dp = CommonDataProvider.Instance();

            return GetUser(dp.GetUserIDByEmail(emailAddress), false);
        }

        public static User FindUserByUsername (string username) {
            return GetUser(0, username, false, true);
        }
        #endregion

        #region GetUsers
        // *********************************************************************
        //  GetUsers
        //
        /// <summary>
        /// Returns all the users currently in the system.
        /// </summary>
        /// <param name="pageIndex">Page position in which to return user's for, e.g. position of result set</param>
        /// <param name="pageSize">Size of a given page, e.g. size of result set.</param>
        /// <param name="sortBy">How the returned user's are to be sorted.</param>
        /// <param name="sortOrder">Direction in which to sort</param>
        /// <returns>A collection of user.</returns>
        /// 
        // ********************************************************************/
        public static UserSet GetUsers (int pageIndex, int pageSize, bool returnRecordCount, bool includeHiddenUsers ) {
            return GetUsers(pageIndex, pageSize, SortUsersBy.Username, SortOrder.Ascending, null, false, true, UserAccountStatus.Approved, returnRecordCount, includeHiddenUsers );
        }

        public static UserSet GetUsers(int pageIndex, int pageSize, SortUsersBy sortBy, SortOrder sortOrder, string usernameFilter, bool includeEmailInFilter, bool cacheable, UserAccountStatus accountStatus, bool returnRecordCount, bool includeHiddenUsers) {
            HttpContext context = HttpContext.Current;
            UserSet users;
            // Return moderation counters also
            //
            bool returnModerationCounters = CSContext.Current.SiteSettings.EnableUserModerationCounters;

            // If we're compiled with debug code we never cache
            //
#if DEBUG_NOCACHE
            cacheable = false;
#endif

            if (cacheable) {

                // Build a cache key
                //
                string usersKey = pageIndex.ToString() + pageSize.ToString() + sortBy + sortOrder + usernameFilter + includeEmailInFilter + accountStatus + returnModerationCounters;

                // Serve from the cache when possible
                //
                users = CSCache.Get(usersKey) as UserSet;

                if (users == null) {

                    users = GetUsersFromDataProvider (pageIndex, pageSize, sortBy, sortOrder, usernameFilter, includeEmailInFilter, accountStatus, returnRecordCount, includeHiddenUsers, returnModerationCounters);

                    // Insert the user collection into the cache for 30 seconds
                    CSCache.Insert(usersKey, users, CSCache.MinuteFactor / 2);

                }

            } else {

                users = GetUsersFromDataProvider(pageIndex, pageSize, sortBy, sortOrder, usernameFilter, includeEmailInFilter, accountStatus, returnRecordCount, includeHiddenUsers, returnModerationCounters);

            }

            return users;
        }

        private static UserSet GetUsersFromDataProvider (int pageIndex, int pageSize, SortUsersBy sortBy, SortOrder sortOrder, string usernameFilter, bool includeEmailInFilter, UserAccountStatus accountStatus, bool returnRecordCount, bool includeHiddenUsers, bool returnModerationCounters) {
            // Create Instance of the CommonDataProvider
            CommonDataProvider dp = CommonDataProvider.Instance();

            return dp.GetUsers(pageIndex, pageSize, sortBy, sortOrder, usernameFilter, includeEmailInFilter, accountStatus, returnRecordCount, includeHiddenUsers, returnModerationCounters );
        }
        #endregion

        #region Track Anonymous Users
        // *********************************************************************
        //  TrackAnonymousUsers
        //
        /// <summary>
        /// Used to keep track of the number of anonymous users on the system
        /// </summary>
        /// <returns>A collection of user.</returns>
        /// 
        // ********************************************************************/
        public static void TrackAnonymousUsers(HttpContext context) {

            // Ignore RSS requests
            //
            if (context.Request.Url.ToString().IndexOf("rss.aspx") > 0)
                return;


			// Ignore style sheet requests
			//
			if (context.Request.Url.ToString().IndexOf("style") > 0)
				return;

            SiteSettings siteSettings = SiteSettingsManager.GetSiteSettings(context);

            
            // Is anonymous user tracking enabled?
            //
            if (!siteSettings.EnableAnonymousUserTracking)
                return;

            // Have we already done this work for this request?
            //
            if (context.Items["CheckedAnonymousCookie"] != null)
                return;
            else
                context.Items["CheckedAnonymousCookie"] = "true";


            string cookieName = siteSettings.AnonymousCookieName;
             HttpCookie cookie =null;

            // Is the user anonymous?
            //
            if (context.Request.IsAuthenticated) 
            {
                cookie = new HttpCookie(cookieName);
                context.Response.Cookies[cookieName].Expires = new System.DateTime(1999, 10, 12);
                context.Response.Cookies.Add(cookie);
                return;
            }

            
            string userID = Guid.NewGuid().ToString();
                      
           
            Hashtable anonymousUsers = GetAnonymousUserList(siteSettings.SettingsID);
            string lastAction = context.Request.Url.PathAndQuery;
            User user = null;


            // Check if the Tracking cookie exists
            //
            cookie = context.Request.Cookies[cookieName];

            // Track anonymous user
            //
            if ((null == cookie) || (cookie.Value == null)) {   // Only do the work if we don't have the cookie

                // Set the UsedID value of the cookie
                //
                cookie = new HttpCookie(cookieName);
                cookie.Value = userID;
                cookie.Expires = DateTime.Now.AddMinutes(siteSettings.AnonymousCookieExpiration);
                context.Response.Cookies.Add(cookie);

                // Create a user
                //
                user = new User();
                user.LastAction = SiteUrls.LocationKey();
                user.LastActivity = DateTime.Now;
                user.SettingsID = siteSettings.SettingsID;
				// 2005-02-27: 新增内容，记录匿名用户IP
				user.IPCreated = Globals.IPAddress;
				user.IPLastActivity = Globals.IPAddress;

                // Add the anonymous user
                //
                if (!anonymousUsers.Contains(userID) )
                    anonymousUsers[userID] = user;
        
            } else {

                if (cookie.Value != null)
                    userID = cookie.Value.ToString();
                else
                    userID = null;

                // Update the anonymous list
                //
                if ((userID == null) || (userID == string.Empty)) {

                    context.Response.Cookies[cookieName].Expires = new System.DateTime(1999, 10, 12);

                } else {

                    // Find the cookie in the anonymous list
                    //
                    if (anonymousUsers[userID] == null) {
                        anonymousUsers[userID] = new User();
                    }

                    user = (User) anonymousUsers[userID];

                    user.LastAction = SiteUrls.LocationKey();
                    user.LastActivity = DateTime.Now;
					// 2005-02-27: 新增内容，记录匿名用户IP
					user.IPCreated = Globals.IPAddress;
					user.IPLastActivity = Globals.IPAddress;

                    // Reset the expiration on the cookie
                    //
                    cookie = new HttpCookie(cookieName);
                    cookie.Value = userID;
                    cookie.Expires = DateTime.Now.AddMinutes(siteSettings.AnonymousCookieExpiration);
                    context.Response.Cookies.Add(cookie);

                }

            }

        }

        public static void UpdateAnonymousUsers (int settingsID) {
            string key = string.Format("SettingsID:{0}AnonymousUserList",settingsID);

            CommonDataProvider dp = CommonDataProvider.Instance();

            Hashtable anonymousUsers = GetAnonymousUserList(settingsID);

            SiteStatistics.LoadSiteStatistics(settingsID, true, 3).CurrentAnonymousUsers = dp.UpdateAnonymousUsers( anonymousUsers, settingsID );

            CSCache.Remove(key);
        }

        private static Hashtable GetAnonymousUserList (int settingsID) {
            string key = string.Format("SettingsID:{0}AnonymousUserList",settingsID);
            
            Hashtable anonymousUserList = CSCache.Get(key) as Hashtable;

            // Do we have the hashtable?
            //
            if (anonymousUserList == null) {
             

                // Get the hashtable
                //
                anonymousUserList = new Hashtable();
                CSCache.Insert(key,anonymousUserList, 10 * CSCache.MinuteFactor);

            }

            return anonymousUserList;

        }

        #endregion

        #region CreateAnonymousUser
        
		public static User GetAnonymousUser () {
			return GetAnonymousUser(true);
		}
        public static User GetAnonymousUser (bool fromCache) 
        {
            const string cacheKeyFormat = "Site:{0}.AnonymousUser";
            string cacheKey = String.Format(cacheKeyFormat, CSContext.Current.SiteSettings.SettingsID );

            User u = null;

            if(fromCache)
                u = CSCache.Get(cacheKey) as User;

			if( u == null) {
				u = CommonDataProvider.Instance().GetAnonymousUser( CSContext.Current.SiteSettings.SettingsID );

				if( u != null && u.Username != null && u.UserID > 0 && fromCache ) {
					CSCache.Insert( cacheKey, u, 120 );
				}
			}

            return u;
			
        }

        public static User GetAnonymousUser (string username) {
            User user = new User();

            // Do we have a username or email address?
            //
            if ( username == null )
                username = ResourceManager.GetString("DefaultAnonymousUsername");

			//Find away to set this...or default it better
            user.Username = username;
            user.UserID = 0;
            user.IsAnonymous = true;
			// 2005-02-27: 新增内容，记录匿名用户IP
			user.IPCreated = Globals.IPAddress;
			user.IPLastActivity = Globals.IPAddress;


            return user;
        }

        #endregion CreateAnonymousUser

        #region Update User

        // *********************************************************************
        //  Update
        //
        /// <summary>
        /// Updates a user's personal information.
        /// </summary>
        /// <param name="user">The user to update.  The Username indicates what user to update.</param>
        /// <param name="NewPassword">If the user is changing their password, the user's new password.
        /// Otherwise, this should be the user's existing password.</param>
        /// <returns>This method returns a boolean: it returns True if
        /// the update succeeds, false otherwise.  (The update might fail if the user enters an
        /// incorrect password.)</returns>
        /// <remarks>For the user to update their information, they must supply their password.  Therefore,
        /// the Password property of the user object passed in should be set to the user's existing password.
        /// The NewPassword parameter should contain the user's new password (if they are changing it) or
        /// existing password if they are not.  From this method, only the user's personal information can
        /// be updated (the user's password, forum view settings, email address, etc.); to update the user's
        /// system-level settings (whether or not they are banned, their trusted status, etc.), use the
        /// UpdateUserInfoFromAdminPage method.  <seealso cref="UpdateUserInfoFromAdminPage"/></remarks>
        /// 
        // ********************************************************************/
        public static bool UpdateUser (User user) {

			CSEvents.BeforeUser(user,ObjectState.Update);

			//Make sure we update all references to User
			CSContext context = CSContext.Current;
			context.User = user;

            // Create Instance of the CommonDataProvider
            CommonDataProvider dp = CommonDataProvider.Instance();
            bool updatePasswordSucceded = false;
            CreateUserStatus status;
			string cacheKey = "UserLookupTable";
			string userKey = "User-";

            user.Email = Globals.HtmlEncode(user.Email);
            
            if(user.HasProfile && !user.Profile.IsReadOnly)
            {
                // we need to strip the <script> tags from input forms
                user.Profile.Signature = HtmlScrubber.Clean(user.Profile.Signature, true, true); //Transforms.StripHtmlXmlTags(user.Profile.Signature);
                user.Profile.AolIM = Globals.HtmlEncode(user.Profile.AolIM);
                user.Profile.PublicEmail = Globals.HtmlEncode(user.Profile.PublicEmail);
                user.Profile.IcqIM = Globals.HtmlEncode(user.Profile.IcqIM);
                user.Profile.Interests = Globals.HtmlEncode(user.Profile.Interests);
                user.Profile.Location = Globals.HtmlEncode(user.Profile.Location);
                user.Profile.MsnIM = Globals.HtmlEncode(user.Profile.MsnIM);
                user.Profile.Occupation = Globals.HtmlEncode(user.Profile.Occupation);
                user.Profile.WebAddress = Globals.HtmlEncode(user.Profile.WebAddress);
                user.Profile.WebLog = Globals.HtmlEncode(user.Profile.WebLog);
				user.Profile.YahooIM = Globals.HtmlEncode(user.Profile.YahooIM);
				user.Profile.QQIM = Globals.HtmlEncode(user.Profile.QQIM);
                
                //Save the user's profile data
                user.Profile.Save();

            }
			//Note: SHS does not support updating username
			//user.Username = Transforms.StripHtmlXmlTags(user.Username);
            user.AvatarUrl = Globals.HtmlEncode(user.AvatarUrl);
			user.Nickname = Globals.HtmlEncode(user.Nickname);
            
            // Call the underlying update
            dp.CreateUpdateDeleteUser(user, DataProviderAction.Update, out status);
            updatePasswordSucceded = true;

            CSEvents.AfterUser(user,ObjectState.Update);


            // Remove from the cache if it exists
			Hashtable userLookupTable = CSCache.Get(cacheKey) as Hashtable;
			if (userLookupTable != null)
			{
				userLookupTable.Remove(userKey + user.UserID);
				userLookupTable.Remove(userKey + user.Username);
				userLookupTable.Remove(userKey + user.Nickname);
			}

			
            return updatePasswordSucceded;
        }
        #endregion


        // *********************************************************************
        //  ToggleOptions
        //
        /// <summary>
        /// Toggle various user options
        /// </summary>
        /// <param name="username">Name of user we're updating</param>
        /// <param name="hideReadThreads">Hide threads that the user has already read</param>
        /// 
        // ********************************************************************/
        public static void ToggleOptions(string username, bool hideReadThreads) {
            ToggleOptions(username, hideReadThreads, ViewOptions.NotSet);			
        }

        // *********************************************************************
        //  ToggleOptions
        //
        /// <summary>
        /// Toggle various user options
        /// </summary>
        /// <param name="username">Name of user we're updating</param>
        /// <param name="hideReadThreads">Hide threads that the user has already read</param>
        /// <param name="viewOptions">How the user views posts</param>
        /// 
        // ********************************************************************/
        public static void ToggleOptions(string username, bool hideReadThreads, ViewOptions viewOptions) {
            // Create Instance of the CommonDataProvider
            CommonDataProvider dp = CommonDataProvider.Instance();

            dp.ToggleOptions(username, hideReadThreads, viewOptions);			
        }


        #region Valid User
        // *********************************************************************
        //  ValidUser
        //
        /// <summary>
        /// Determines if the user is a valid user.
        /// </summary>
        /// <param name="user">The user to check.  Note that the Username and Password properties of the
        /// User object must be set.</param>
        /// <returns>A boolean: true if the user's Username/password are valid; false if they are not,
        /// or if the user has been banned.</returns>
        /// 
        // ********************************************************************/
        public static LoginUserStatus ValidUser(User user)
        {
            return ValidUser(user, true);
        }
        
        
        protected static LoginUserStatus ValidUser(User user, bool retry) 
        {

			if(!ms.Membership.ValidateUser(user.Username,user.Password))
			{
                //if the upgrade was a success, force the user login via the Membership API. 
                if(retry && CSConfiguration.GetConfig().BackwardsCompatiblePasswords && CommonDataProvider.Instance().UpgradePassword(user.Username,user.Password))
                    return ValidUser(user, false);
                
				return LoginUserStatus.InvalidCredentials;
			}
			

            // Create Instance of the CommonDataProvider
            //
            //CommonDataProvider dp = CommonDataProvider.Instance();		
            
            // Lookup account by provided username
            //
			User userLookup = Users.FindUserByUsername( user.Username );
			if (userLookup == null)
				return LoginUserStatus.InvalidCredentials;

			if(!userLookup.Member.IsApproved)
				return LoginUserStatus.AccountPending;
            
            // Check Account Status
            // (This could be done before to validate credentials because
            // system doesn't allow duplicate usernames)
            //
            if (userLookup.IsBanned &&  DateTime.Now <= userLookup.BannedUntil) {
                // It's a banned account
                return LoginUserStatus.AccountBanned;
            }
            // LN 5/21/04: Update user to DB if ban expired
            else if (userLookup.IsBanned &&  DateTime.Now > userLookup.BannedUntil) {
                // Update to back to datastore
                userLookup.AccountStatus = UserAccountStatus.Approved;
                userLookup.BannedUntil = DateTime.Now;

                Users.UpdateUser(userLookup);
            }
            if (userLookup.AccountStatus == UserAccountStatus.ApprovalPending) {
                // It's a pending account
                return LoginUserStatus.AccountPending;
            }
            if (userLookup.AccountStatus == UserAccountStatus.Disapproved) {
                // It's a disapproved account
                return LoginUserStatus.AccountDisapproved;
            }

			//Set current user
			CSContext.Current.User = userLookup;

			//allow others to take action here
			CSEvents.UserValidated(userLookup);

				return LoginUserStatus.Success;

			//MOVED TO SHS

////			if (HttpContext.Current.User.Identity.AuthenticationType == "" ) 
//			{
//
//				// We have a valid account so far.
//				//
//				// Get Salt & Passwd format
//				user.Salt = userLookup.Salt;     
// 				user.PasswordFormat = userLookup.PasswordFormat; // Lucian: I think it must be reused. Usefull when there are a wide range of passwd formats.
//				// Set the Password
//				//should be able to safely drop this since it will happen in SHS
//				//user.Password = Users.Encrypt(user.PasswordFormat, user.Password, user.Salt );
//			}
//
//            return (LoginUserStatus) dp.ValidateUser(user);
        }
			
		// Method is not used, commenting out
		// 12/29/04 krobertson
		/*public static bool AuthenticateUser( User userToLogin ) {
			LoginUserStatus loginStatus = Users.ValidUser(userToLogin);

			if( loginStatus == LoginUserStatus.Success ) 
			{

				// Are we allowing login?
				// TODO -- this could be better optimized
				if (!CSContext.Current.SiteSettings.AllowLogin) 
				{
					// Check the user is in the administrator role
					if (!userToLogin.IsAdministrator)
						throw new CSException(CSExceptionType.UserLoginDisabled);
				}

				return true;
			} 
			else 
			{
				if(loginStatus == LoginUserStatus.InvalidCredentials) 
				{ // Invalid Credentials
					throw new CSException(CSExceptionType.UserInvalidCredentials, userToLogin.Username);
				} 
				else if(loginStatus == LoginUserStatus.AccountPending) 
				{ // Account not approved yet
					throw new CSException(CSExceptionType.UserAccountPending);
				}
				else if(loginStatus == LoginUserStatus.AccountBanned) 
				{ // Account banned
					throw new CSException(CSExceptionType.UserAccountBanned, userToLogin.Username);
				}
				else if(loginStatus == LoginUserStatus.AccountDisapproved) 
				{ // Account disapproved
					throw new CSException(CSExceptionType.UserAccountDisapproved, userToLogin.Username);
				}
				else if(loginStatus == LoginUserStatus.UnknownError) 
				{ // Unknown error because of miss-syncronization of internal data
					throw new CSException(CSExceptionType.UserUnknownLoginError);
				}

				return false;
			}
		}*/

        #endregion

        #region CreateUser
        // *********************************************************************
        //  CreateNewUser
        //
        /// <summary>
        /// Creates a new user.
        /// </summary>
        /// <param name="user">A User object containing information about the user to create.  Only the
        /// Username and Email properties are used here.</param>
        /// <returns></returns>
        /// <remarks>This method chooses a random password for the user and emails the user his new Username/password.
        /// From that point on, the user can configure their settings.</remarks>
        /// 
        // ********************************************************************/
		public static CreateUserStatus Create(User user, bool sendEmail) {
			return Create(user, sendEmail, false);
		}

		public static CreateUserStatus Create(User user, bool sendEmail, bool ignoreDisallowNames) {
            
			CSEvents.BeforeUser(user, ObjectState.Create);

			CSContext csContext = CSContext.Current;
			SiteSettings settings = csContext.SiteSettings;

            AccountActivation activation = settings.AccountActivation;
            CreateUserStatus status;
            
            string password = user.Password;
			// 2005-02-26: 新增注册IP、磁盘限额，格式化昵称
			user.Nickname = Globals.HtmlEncode(user.Nickname);
			User nicknameUser = Users.GetUserByNickname(user.Nickname);
			if (nicknameUser != null && nicknameUser.UserID > 0)
				return CreateUserStatus.DuplicateNickname; 

			user.Email = Globals.HtmlEncode(user.Email);
			user.DatabaseQuota = settings.UserDatabaseQuota;
			user.IPCreated = Globals.IPAddress;


            // Lucian: deprecated since it is not handled in CreateUser control
            // and regEx validation on control's form.
            // Make sure the username begins with an alpha character
            //if (!Regex.IsMatch(user.Username, "^[A-Za-z].*"))
            //    return CreateUserStatus.InvalidFirstCharacter;

            // Check if username is disallowed
            if ((!ignoreDisallowNames) && ( DisallowedNames.NameIsDisallowed(user.Username) == true ))
                return CreateUserStatus.DisallowedUsername;

			// Strip the domain name from the username, if one is present
			if( user.Username.IndexOf("\\") > 0 && settings.StripDomainName ) 
				user.Username = user.Username.Substring( user.Username.LastIndexOf("\\") + 1 );

			// Set the user's default moderation level
			user.ModerationLevel = CSContext.Current.SiteSettings.NewUserModerationLevel;

            // Create Instance of the CommonDataProvider
            CommonDataProvider dp = CommonDataProvider.Instance();

			try {
				User createdUser = dp.CreateUpdateDeleteUser(user, DataProviderAction.Create, out status);
                
                
                if(createdUser != null && status == CreateUserStatus.Created)
                {
					csContext.User = createdUser;

					CSConfiguration config = csContext.Config;

                    if(settings.EnableDefaultRole && config.DefaultRoles.Length > 0)
                    {
						foreach(string role in config.DefaultRoles)
						{
							Roles.AddUserToRole(createdUser.Username, role);
						}
                    }

					// 注册时默认时区和默认语言
					createdUser.Profile.Timezone = settings.TimezoneOffset;
					createdUser.Profile.Language = Globals.Language;
					createdUser.Profile.Save();


					CSEvents.AfterUser(createdUser,ObjectState.Create);
                }

                
			}
			catch (CSException e) {
				return e.CreateUserStatus;
			}

			// process the emails now
			//
            if(sendEmail == true) 
			{

                User currentUser = CSContext.Current.User;
                if (currentUser.IsForumAdministrator || currentUser.IsBlogAdministrator || currentUser.IsGalleryAdministrator) 
                {
                    activation = AccountActivation.Automatic;
                }
				
				// TDD HACK 7/19/2004
				// we are about to send email to the user notifying them that their account was created, problem is
				// when we create the user above we can't set the DateCreated property as this is set through the proc
				// but the email needs to know the DateCreated property. So for now, we'll just set the date to the current
				// datetime of the server. We don't care about the user local time at this point because the user hasn't
				// logged in to set their user profile.
				
				//user.DateCreated = DateTime.Now;
				//user.LastLogin = DateTime.Now;

				// based on the account type, we send different emails
				//
				switch (activation) {
					case AccountActivation.AdminApproval:
						Emails.UserAccountPending (user);
						break;
					
					case AccountActivation.Email:
						Emails.UserCreate(user, password);
						break;
					
					case AccountActivation.Automatic:
						Emails.UserCreate(user, password);
						break;
				}
            }
            
            return CreateUserStatus.Created;
        }
        #endregion

		#region ToggleForceLogin
		public static void ToggleForceLogin(User user)
		{
			// Update the setting in the database
			CommonDataProvider dp = CommonDataProvider.Instance();
			dp.ToggleUserForceLogin(user);

			// Remove the cached user entry so it is force to reget it and not have ForceLogin true
			string cacheKey = "UserLookupTable";
			string userKey = "User-";
			Hashtable userLookupTable = CSCache.Get(cacheKey) as Hashtable;
			if (userLookupTable != null)
			{
				userLookupTable.Remove(userKey + user.UserID);
				userLookupTable.Remove(userKey + user.Username);
				userLookupTable.Remove(userKey + user.Nickname);
			}
		}
		#endregion

		#region CheckUserLastPostDate

		public static bool CheckUserLastPostDate(User user)
		{
			string cacheKey = "UserLastPostTable";
			string userKey = "User:" + user.UserID;
			Hashtable userLastPostTable = CSCache.Get(cacheKey) as Hashtable;

			// If they're anonymous, tack their IP onto the userKey
			if(user.IsAnonymous)
				userKey += ":IP:" + CSContext.Current.Context.Request.UserHostAddress;

			// If the table is null, no posters to care about, so return true
			if(userLastPostTable == null)
				return true;

			// Iterate through the table... remove all bad entries
			// This may seem a little slower, but it helps to keep the table small
			// Some users may only post once a day, but if the forums stay active all that time,
			// their postdate will still be in memory... regularly walking it and removing unnecessary
			// entries keeps it short
			lock(userLastPostTable.SyncRoot)
			{
				string[] keys = new string[userLastPostTable.Count];
				userLastPostTable.Keys.CopyTo(keys, 0);

				foreach(string key in keys)
					if(((DateTime)userLastPostTable[key]).AddSeconds(CSContext.Current.SiteSettings.PostInterval) <= DateTime.Now)
						userLastPostTable.Remove(key);
			}

			// If they aren't in there, then they aren't still being blocked, return true;
			if(!userLastPostTable.Contains(userKey))
				return true;

			// An entry for them is still in there, so they still can't post
			return false;
		}

		public static void UpdateUserLastPostDate(User user)
		{
			string cacheKey = "UserLastPostTable";
			string userKey = "User:" + user.UserID;
			Hashtable userLastPostTable = CSCache.Get(cacheKey) as Hashtable;

			// If they're anonymous, tack their IP onto the userKey
			if(user.IsAnonymous)
				userKey += ":IP:" + CSContext.Current.Context.Request.UserHostAddress;

			// Create the table if it doesn't exist
			if(userLastPostTable == null)
			{
				userLastPostTable = new Hashtable();
				CSCache.Max(cacheKey, userLastPostTable);
			}

			// If they have an entry already in there, remove it
			if(userLastPostTable.Contains(userKey))
				userLastPostTable.Remove(userKey);

			// Add the new time
			userLastPostTable.Add(userKey, DateTime.Now);
		}

		#endregion
        
        /// <summary>
        /// Validates if provided password is compliant with Microsoft Membership 
        /// requirements: minimum required password length and minimum required non
        /// alphanumeric chars no.
        /// </summary>
        /// <param name="newPassword">Password to be verified.</param>
        /// <param name="errorMessage">Error message to return.</param>
        /// <returns>True if compliant, otherwise False.</returns>
        public static bool PasswordIsMembershipCompliant (string newPassword, out string errorMessage) {
            int minRequiredPasswordLength = ms.Membership.MinRequiredPasswordLength;
            int minRequiredNonAlphanumericCharacters = ms.Membership.MinRequiredNonAlphanumericCharacters;
            errorMessage = "";

            if (newPassword.Length < minRequiredPasswordLength) {
                errorMessage = string.Format( ResourceManager.GetString("ChangePassword_InvalidLength"), ms.Membership.MinRequiredPasswordLength.ToString() );;
                return false;
            }

            int nonAlphaNumChars = 0;
            for (int i = 0; i < newPassword.Length; i++) {
                if (!char.IsLetterOrDigit(newPassword, i)) {
                    nonAlphaNumChars++;
                }
            }
            if (nonAlphaNumChars < minRequiredNonAlphanumericCharacters) {
                errorMessage = string.Format( ResourceManager.GetString("ChangePassword_InvalidContent"), ms.Membership.MinRequiredNonAlphanumericCharacters.ToString() );
                return false;
            }

            return true;
        }

    }


	struct CachedUser 
	{
		public DateTime Created;
		public User User;
	}
}
