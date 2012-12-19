//------------------------------------------------------------------------------
// <copyright company="Telligent Systems">
//     Copyright (c) Telligent Systems Corporation.  All rights reserved.
// </copyright> 
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Specialized;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Caching;
using System.Xml;
using GPRP.GPRPEnumerations;

namespace GPRP.GPRPComponents {

	public class SiteUrls 
	{

		public static readonly string SiteUrlsProviderName = "SiteUrlsProvider";

        #region Initializer

        /// <summary>
        /// Returns a intance of SiteUrls. We first check the cache and if not found
        /// we parse the SiteUrls.config file and build it.
        /// </summary>
        /// <returns></returns>
		public static SiteUrls Instance()
		{
			const string cacheKey = "SiteUrls";

			SiteUrls siteUrls = CSCache.Get(cacheKey) as SiteUrls;
            
			if(siteUrls == null)
			{
				try
				{
					HttpContext Context = HttpContext.Current;
					CSConfiguration config = CSConfiguration.GetConfig();
                    
					Provider siteUrlProvider = config.Providers["SiteUrlsDataProvider"] as Provider;

					if(siteUrlProvider == null)
					{
						if(Context != null)
						{
							Context.Response.Write("<h1>SiteUrl Provider was NOT found!</h1>");
							Context.Response.Write("A provider named SiteUrlsDataProvider with the type or derived type of CommunityServer.Components.SiteUrlsData, CommunityServer.Components must be added to the provider list");
							Context.Response.End();
						}
						else
						{
							throw new CSException(CSExceptionType.SiteUrlDataProvider,"Could not load SiteUrlsProvider");
						}
					}

					string siteUrlsXmlFile = config.FilesPath +  siteUrlProvider.Attributes["path"];
					string file = CSContext.Current.PhysicalPath(siteUrlProvider.Attributes["path"]); // CSContext.Current.MapPath(Globals.ApplicationPath + siteUrlsXmlFile);
					Type type = Type.GetType(siteUrlProvider.Type);
					SiteUrlsData data = Activator.CreateInstance(type,new object[]{file}) as SiteUrlsData;

					// Create the instance for the provider, or fall back on the builtin one
					//
					Provider urlProvider = (Provider) CSConfiguration.GetConfig().Providers[SiteUrlsProviderName];
					if(urlProvider != null)
						siteUrls = ApplicationUrls.CreateInstance(urlProvider, data) as SiteUrls;
					else
						siteUrls = new SiteUrls(data);

					CacheDependency dep = new CacheDependency(file);
					CSCache.Max(cacheKey, siteUrls, dep);
				}
				catch(Exception ex)
				{
					

					if (ex.InnerException != null && ex.InnerException is CSException) 
					{
						throw ex.InnerException;
					}

					string message = "<p>" +  ex.Message + "</p>";

					Exception inner = ex.InnerException;

					while(inner != null)
					{
						message = string.Format("{0}<p>{1}</p>", message, inner.Message);
						inner = inner.InnerException;

					}

					CSException csex =  new CSException(CSExceptionType.SiteUrlDataProvider,message,ex);
					CatastrophicMessage.Write(HttpContext.Current,csex,"[SiteUrlData]","SiteUrlData.htm");
				}
			}
 
			return siteUrls;
		}

		#endregion

		#region Force Refresh

		public static void ForceRefresh()
		{
			const string cacheKey = "SiteUrls";
			CSCache.Remove(cacheKey);
		}

		#endregion

		#region Member variables & constructor

		//CSContext forumContext = CSContext.Current;
		bool enableWaitPage = false;
        private SiteUrlsData urlData = null;

		// Constructor loads all the site urls from the SiteUrls.config file
		// or returns from the cache
		//
        // Updated 8/17/2004 by ScottW >> Moved Initializate to Static instance method
        // This should enable us to cache on instance of SiteUrls and not 3 unique collections
        public SiteUrls(SiteUrlsData data) 
        {
            urlData = data;
        }

        public virtual string RawPath(string rawpath)
        {
            return urlData.FormatUrl(rawpath);
        }

		#endregion

		#region Who is online
		// Who is online
		//
		public virtual string WhoIsOnline 
		{
			get { return urlData.FormatUrl("whoIsOnline"); }
		}
		#endregion

		#region Moderation
		// Moderation
		//
		public virtual string Moderate 
		{
			get { return urlData.FormatUrl("moderate"); }
		}

		public virtual string ModerationHome 
		{
			get { return urlData.FormatUrl("moderate_home"); }
		}

		public virtual string ModerateViewPost (int postID) 
		{
			return urlData.FormatUrl("moderate_ViewPost", postID.ToString() );
		}

		public virtual string ModeratePostDelete (int postID, string returnUrl) 
		{
			return urlData.FormatUrl("moderate_Post_Delete", postID.ToString(), returnUrl );
		}

		public virtual string ModeratePostEdit (int postID, string returnUrl) 
		{
			return urlData.FormatUrl("moderate_Post_Edit", postID.ToString(), returnUrl );
		}

		public virtual string ModerateViewUserProfile (int userID) 
		{
			return urlData.FormatUrl("moderate_ViewUserProfile", userID.ToString() );
		}

		public virtual string ModeratePostMove (int postID, string returnUrl) 
		{
			return urlData.FormatUrl("moderate_Post_Move", postID.ToString(), returnUrl );
		}

		public virtual string ModerateThreadSplit (int postID, string returnUrl) 
		{
			return urlData.FormatUrl("moderate_Thread_Split", postID.ToString(), returnUrl );
		}

		public virtual string ModerateThreadJoin (int postID, string returnUrl) 
		{
			return urlData.FormatUrl("moderate_Thread_Join", postID.ToString(), returnUrl );
		}

        public virtual string ModerationHistory (int postID) {
            return ModerationHistory( postID, null );
        }

		public virtual string ModerationHistory (int postID, string returnUrl) {
			string outString = ""; 

            if (Globals.IsNullorEmpty( returnUrl )) { 
                outString = urlData.FormatUrl("moderation_History", postID.ToString(), "");
                outString = outString.Replace( "&ReturnUrl=", "" );
            } 
            else { 
                outString = urlData.FormatUrl("moderation_History", postID.ToString(), returnUrl);
            }

            return outString;
		}

		public virtual string ModerateForum (int forumID) 
		{
			return urlData.FormatUrl("moderate_Forum", forumID.ToString()) ;
		}

        public virtual string UserModerationHistory (int userID) {
            return UserModerationHistory( userID, -1 ) ;
        }

        public virtual string UserModerationHistory (int userID, int action) {
            string outString = urlData.FormatUrl( "admin_User_ModerationHistory", userID.ToString(), action.ToString() );
            
            if (action <= 0) {                
                outString = outString.Replace( "&Action=" + action.ToString(), "" );
            } 

            return outString;
        }
		#endregion

		#region Admin
		// Admin
		//
		public virtual string Admin 
		{
			get { return urlData.FormatUrl("admin"); }
		}

		public virtual string AdminConfiguration 
		{
			get{ return urlData.FormatUrl("admin_Configuration"); }
		}
		public virtual string AdminManageForums 
		{
			get{ return urlData.FormatUrl("admin_Forums");}
		}
		public virtual string AdminUserEdit ( int userID ) 
		{
			return urlData.FormatUrl("admin_User_Edit", userID.ToString()) ;
		}

		public virtual string AdminForumGroup( int forumID ) 
		{
			return urlData.FormatUrl("admin_ForumGroup", forumID.ToString() );
		}

		public virtual string AdminForumEdit ( int forumID ) 
		{
			return urlData.FormatUrl("admin_Forum_Edit", forumID.ToString() );
		}

		public virtual string AdminForumPermissions 
		{
			get{ return urlData.FormatUrl("admin_Forum_Permissions"); }
		}
		
        public virtual string AdminDefaultForumPermissions 
        {
            get{ return urlData.FormatUrl("admin_Forum_Default_Permissions"); }
        }

		
		public virtual string AdminForumModeratorsEdit( int forumId, int groupId ) 
		{
			return urlData.FormatUrl("admin_Forum_Moderators_Edit", forumId, groupId );
		}
        
		
		public virtual string AdminForumPermissionEdit( int forumId, Guid roleId ) 
		{
            if(forumId == -1)
                return urlData.FormatUrl("admin_Forum_Default_Permissions_Edit",roleId);
            else
			return urlData.FormatUrl("admin_Forum_Permissions_Edit", forumId, roleId );
		}

		public virtual string AdminDefaultPermissions {
			get{ return urlData.FormatUrl("admin_Default_Permissions"); }
		}

		public virtual string AdminDefaultPermissionsEdit {
			get{ return urlData.FormatUrl("admin_Default_Permissions_Edit"); }
		}

		public virtual string AdminHome 
		{
			get { return urlData.FormatUrl("admin_Home"); }
		}

		public virtual string AdminManageUsers 
		{
			get { return urlData.FormatUrl("admin_User_List"); }
		}

		public virtual string AdminManageUsersFilter(UserAccountStatus statusFilter)
		{
			return urlData.FormatUrl("admin_User_List_Filter", (int)statusFilter);
		}

		public virtual string AdminManageRoles 
		{
			get { return urlData.FormatUrl("admin_Roles"); }
		}

		public virtual string AdminUserRoles (int userID) 
		{
			return urlData.FormatUrl("admin_User_UserRolesAdmin", userID.ToString()) ;
		}

		public virtual string AdminUserPasswordChange (int userID) 
		{
			return urlData.FormatUrl("admin_User_ChangePassword", userID.ToString()) ;
		}

		public virtual string AdminUserPasswordAnswerChange (int userID) 
		{
			return urlData.FormatUrl("admin_User_ChangePasswordAnswer", userID.ToString()) ;
		}

		public virtual string AdminUserConfiguration 
		{
			get { return urlData.FormatUrl("admin_User_Configuration"); }
		}

		public virtual string AdminManageRanks 
		{
			get{ return urlData.FormatUrl("admin_Ranks"); }
		}

		public virtual string AdminManageNames 
		{
			get{ return urlData.FormatUrl("admin_Names"); }
		}

		public virtual string AdminManageCensorships 
		{
			get{ return urlData.FormatUrl("admin_Censorships");}
		}

		public virtual string AdminManageSmilies 
		{
			get{ return urlData.FormatUrl("admin_Smilies"); }
		}

		public virtual string AdminMassEmail 
		{
			get{ return urlData.FormatUrl("admin_MassEmail"); }
		}

		public virtual string AdminRoleEdit( Guid roleID ) 
		{
			return urlData.FormatUrl("admin_Role_Edit", roleID.ToString() );
		}

		public virtual string AdminReportsBuiltIn 
		{
			get { return urlData.FormatUrl("admin_Reports_BuiltIn"); }
		}
		#endregion

		#region KnowledgeBase
		public virtual string KbArticle (int articleID) 
        {
			return KbArticle(articleID, CSContext.Current.SiteSettings.EnableSearchFriendlyURLs);
		}

		public virtual string KbArticle (int articleID, bool searchFriendly) {
			return urlData.FormatUrl("kbArticle", articleID.ToString());
		}

		#endregion

		#region Posts

		// Post related properties
		//
		/// <summary>
		/// If possible use Post (int postID, int forumID)
		/// </summary>
		public virtual string Post (int postID) 
		{
			return Post(postID, CSContext.Current.SiteSettings.EnableSearchFriendlyURLs);
		}

		public virtual string Post (int postID, bool searchFriendly) 
		{
			if (searchFriendly)
				return urlData.FormatUrl("searchFriendlyPost", postID.ToString());
			else
				return urlData.FormatUrl("post", postID.ToString());
		}

		public virtual string PrintPost (int postID) 
		{
			return urlData.FormatUrl("post_Print", postID.ToString());
		}

		public virtual string PostRating (int postID) 
		{
			return urlData.FormatUrl("post_Rating", postID.ToString());
		}

		public virtual string PostCloseWindow (string url) {
			return urlData.FormatUrl("post_CloseWindow",  url);
		}
		
		public virtual string PostInPage (int postID, int postInPageID) 
		{
			return urlData.FormatUrl("post_InPage", postID.ToString(), postInPageID.ToString());
		}

		public virtual string PostAttachment (int postID) 
		{
			return urlData.FormatUrl("post_Attachment", postID.ToString());
		}

		public virtual string PostPaged (int postID, int page) 
		{
			return urlData.FormatUrl("post_Paged", page.ToString(), postID.ToString(), postID.ToString());
		}

		public virtual string PostPagedFormat (int postID) 
		{
			return urlData.FormatUrl("post_Paged", "{0}", postID.ToString(), postID.ToString());
		}

		public virtual string PostCreate (int forumID) 
		{
			return urlData.FormatUrl("post_Create", forumID.ToString());
		}

		public virtual string PostReply (int postID) 
		{
			return urlData.FormatUrl("post_Reply",  postID.ToString());
		}

		public virtual string PostReply (int postID, bool isQuote) 
		{
			return urlData.FormatUrl("post_Quote", postID.ToString(), isQuote.ToString());
		}

		public virtual string PostEdit (int postID, string returnUrl) 
		{
			return urlData.FormatUrl("post_Edit", postID.ToString(), returnUrl);
		}

		public virtual string PostDelete (int postID, string returnUrl) 
		{
			return urlData.FormatUrl("post_Delete", postID.ToString(), returnUrl);
		}

		public virtual string PostsUnanswered 
		{
			get { return urlData.FormatUrl("post_Unanswered"); }
		}

		public virtual string PostsActive 
		{
			get { return urlData.FormatUrl("post_Active"); }
		}

        public virtual string PostsNotRead {
            get { return urlData.FormatUrl("post_NotRead"); }
        }
		#endregion

		#region Help
		public virtual string HelpThreadIcons 
		{
			get { return urlData.FormatUrl("help_ThreadIcons", Globals.Language); }
		}
		#endregion

		#region User
		// User related properties
		//
		public virtual string UserBanned
		{
			get { return urlData.FormatUrl("user_Banned"); }
		}

		public virtual string UserProfile (string username) {
			return urlData.FormatUrl("user_ByName", username);
		}

		public virtual string UserProfile (int userID) 
		{
			return UserProfile (userID, CSContext.Current.SiteSettings.EnableSearchFriendlyURLs);
		}

		public virtual string UserProfile (int userID, bool searchFriendly) 
		{
			if (searchFriendly)
				return urlData.FormatUrl("searchFriendlyUser", userID.ToString());
			else
				return urlData.FormatUrl("user", userID.ToString());
		}

		public virtual string UserEditProfile 
		{
			get 
			{ 
				string currentPath = CSContext.Current.Context.Request.Url.PathAndQuery;
				string returnUrl = null;

				if (ExtractQueryParams(currentPath)["ReturnUrl"] != null)
					returnUrl = ExtractQueryParams(currentPath)["ReturnUrl"];

				if ((returnUrl == null) || (returnUrl == string.Empty))
					return urlData.FormatUrl("user_EditProfile", currentPath);
				else
					return urlData.FormatUrl("user_EditProfile", returnUrl);
			}
                
		}

		public virtual string CleanUserEditProfile
		{
			get { return urlData.FormatUrl("user_EditProfile_Clean");}
		}

		public virtual string UserMyForums 
		{
			get { return urlData.FormatUrl("user_MyForums"); }
		}

		public virtual string UserChangePassword 
		{
			get { return urlData.FormatUrl("user_ChangePassword"); }
		}

		public virtual string UserChangePasswordAnswer 
		{
			get { return urlData.FormatUrl("user_ChangePasswordAnswer"); }
		}

		public virtual string UserForgotPassword 
		{
			get { return urlData.FormatUrl("user_ForgotPassword"); }
		}
        
		public virtual string UserRegister 
		{
			get 
			{ 
				string currentPath = CSContext.Current.Context.Request.Url.PathAndQuery;
				string returnUrl = null;

				if (ExtractQueryParams(currentPath)["ReturnUrl"] != null)
					returnUrl = ExtractQueryParams(currentPath)["ReturnUrl"];

				if ((returnUrl == null) || (returnUrl == string.Empty))
					return urlData.FormatUrl("user_Register", currentPath);
				else
					return urlData.FormatUrl("user_Register", returnUrl);

			}
		}

		public virtual string UserList 
		{
			get { return urlData.FormatUrl("user_List"); }
		}

		public virtual string UserRoles (string roleGuid) 
		{
			return urlData.FormatUrl("user_Roles", roleGuid );
		}
		#endregion

		#region Private Messages
		// Private Messages
		//
		public virtual string PrivateMessage (int userID) 
		{
			return urlData.FormatUrl("privateMessage", userID.ToString());
		}

		public virtual string UserPrivateMessages 
		{
			get { return urlData.FormatUrl("privateMessages"); }
		}

		#endregion

		#region Email
		// Email
		//
		public virtual string EmailToUser (int userID) 
		{
			return urlData.FormatUrl("email_ToUser", userID.ToString());
		}
		#endregion

        #region Emoticon
        // Resolves the absolute path to emoticon images
        //
        public virtual string Emoticon 
        {
            get { return urlData.FormatUrl("emoticons"); }
        }
        #endregion

		#region RSS
		// RSS
		//
		public virtual string RssForum (int forumID, ThreadViewMode mode) 
		{
			return urlData.FormatUrl("rss", forumID.ToString(), ((int) mode).ToString());
		}

        public virtual string AggView(int postid)
        {
            return urlData.FormatUrl("aggview",postid);
        }
		#endregion

		#region Search
		public virtual string Search 
		{
			get { return urlData.FormatUrl("search"); }
		}

		public virtual string SearchAdvanced 
		{
			get { return urlData.FormatUrl("search_Advanced"); }
		}

		public virtual string SearchForText(string textToSearchFor) 
		{
			return SearchForText(textToSearchFor, "", "");
		}

		public virtual string SearchForText(string textToSearchFor, string forumsToSearch, string usersToSearch) 
		{
			if (enableWaitPage)
				return urlData.FormatUrl("wait", (urlData.FormatUrl("search_ForText", textToSearchFor, forumsToSearch, usersToSearch)));
			else
				return urlData.FormatUrl("search_ForText", textToSearchFor, forumsToSearch, usersToSearch);
		}

		public virtual string GallerySearchForText(string textToSearchFor, string forumsToSearch)
		{
			if (enableWaitPage)
				return urlData.FormatUrl("wait", (urlData.FormatUrl("gallery_Search", textToSearchFor, forumsToSearch)));
			else
				return urlData.FormatUrl("gallery_Search", textToSearchFor, forumsToSearch);
		}


		public virtual string SearchByUser (int userID) 
		{
			string encodedUserID =GPRPComponents.Search.ForumsToSearchEncode(userID.ToString());

			return SearchForText("", "", encodedUserID);
		}

		public virtual string SearchForUser (string username) 
		{
			return urlData.FormatUrl("search_ForUser", username);
		}

		public virtual string SearchForUserAdmin (string username) 
		{
			return urlData.FormatUrl("search_ForUserAdmin", username);
		}
		#endregion

		#region FAQ
		public virtual string FAQ 
		{
			get { return urlData.FormatUrl("faq", CSContext.Current.User.Profile.Language); }
		}
		#endregion

        #region TOU
		public virtual string TermsOfUse 
		{
			get 
			{ 
				string termsOfUse = "";
				try 
				{
					termsOfUse = urlData.FormatUrl("termsOfUse");
				}
				catch 
				{
					termsOfUse = "";
				}

				return termsOfUse;
			}
		}

        #endregion

		#region Login, Logout, Messages
		public virtual string LoginReturnHome 
		{
			get 
			{
				return urlData.FormatUrl("login", Globals.ApplicationPath);
			}
		}

		public virtual string Login 
		{
			get 
			{ 
				string currentPath = CSContext.Current.Context.Request.Url.PathAndQuery;
				string returnUrl = null;

				//if (ExtractQueryParams(currentPath)["ReturnUrl"] != null)
				
					returnUrl = ExtractQueryParams(currentPath)["ReturnUrl"];

				if (Globals.IsNullorEmpty(returnUrl))
					return urlData.FormatUrl("login", CSContext.Current.RawUrl);
				else
					return urlData.FormatUrl("login", returnUrl);
			}
		}

		public virtual string Logout 
		{
			get { return urlData.FormatUrl("logout"); }
		}

		public virtual string Message (CSExceptionType exceptionType) 
		{
			return urlData.FormatUrl("message", ((int) exceptionType).ToString());
		}

		public virtual string Message (CSExceptionType exceptionType, string returnUrl) 
		{
			return urlData.FormatUrl("message_return", ((int) exceptionType).ToString(), returnUrl);
		}
		#endregion

		#region Voting
		public virtual string PollCreate (int sectionID) {
			return urlData.FormatUrl("poll",sectionID.ToString());
		}

		#endregion

        public virtual string GuestBook(string username)
        {
            Globals.ValidateApplicationKey(username,out username);
            username = Globals.UrlEncode(username);

            return urlData.FormatUrl("book_User", username);
        }

        public virtual string Redirect(string url)
        {
            return urlData.FormatUrl("redirect",Globals.UrlEncode(url));
        }

        public virtual string Home 
        { 
            get { return urlData.FormatUrl("home"); }
        }

		#region Forum / ForumGroup

		public virtual string ForumsHome 
		{ 
			get { return urlData.FormatUrl("forumshome"); }
		}





		public virtual string ForumGroup (int forumGroupID) 
		{
			return ForumGroup (forumGroupID, CSContext.Current.SiteSettings.EnableSearchFriendlyURLs);
		}

		public virtual string ForumGroup (int forumGroupID, bool searchFriendly) 
		{
			if (searchFriendly)
				return urlData.FormatUrl("searchFriendlyForumGroup", forumGroupID.ToString());
			else
				return urlData.FormatUrl("forumGroup", forumGroupID.ToString());
		}
		#endregion

		#region Private static helper methods

		public static string RemoveParameters (string url) 
		{

			if (url == null)
				return string.Empty;

			int paramStart = url.IndexOf("?");

			if (paramStart > 0)
				return url.Substring(0, paramStart);

			return url;
		}

		protected static NameValueCollection ExtractQueryParams(string url) 
		{
			int startIndex = url.IndexOf("?");
			NameValueCollection values = new NameValueCollection();

			if (startIndex <= 0)
				return values;

			string[] nameValues = url.Substring(startIndex + 1).Split('&');

			foreach (string s in nameValues) 
			{
				string[] pair = s.Split('=');

				string name = pair[0];
				string value = string.Empty;

				if (pair.Length > 1)
					value = pair[1];

				values.Add(name, value);
			}

			return values;
		}

		public static string FormatUrlWithParameters(string url, string parameters) 
		{
			if (url == null)
				return string.Empty;

			if (parameters.Length > 0)
				url = url + "?" + parameters;

			return url;

		}

		#endregion

		#region Public helper methods

        private static Regex ReWriteFilter = null;

        public static bool RewriteUrl(string path, string queryString, out string newPath)
        {
            
            //ReWriterFilter enables us to mark sections of the SiteUrls.config as non rewritable. This is particularly helpful
            //when moving the weblogs and gallery to the root of the site. Otherwise, there is a very likely chance /user /admin /utility
            // /search will be mistaken treated as a blog or gallery.
            if(ReWriteFilter == null)
                ReWriteFilter = new Regex(SiteUrls.Instance().LocationFilter, RegexOptions.IgnoreCase|RegexOptions.Compiled);

            //If we do not match a filter, continue one
            if(!ReWriteFilter.IsMatch(path))
            {
                //now, we walk through all of our ReWritable Urls and see if any match
            	ArrayList urls = SiteUrls.Instance().ReWrittenUrls;
                if(urls.Count > 0)
                {
                    foreach(ReWrittenUrl url in urls)
                    {
                        if(url.IsMatch(path))
                        {
                            newPath = url.Convert(path,queryString);
                            return true;
                        }
                    
                    }
                }
            }

            //Nothing found
            newPath = null;
            return false;
        }

		public struct ForumLocation 
		{
			public string Description;
			public string UrlName;
			public int ForumID;
			public int ForumGroupID;
			public int PostID;
			public int UserID;
		}

		public static ForumLocation GetForumLocation (string encodedLocation) 
		{

			// Decode the location
			ForumLocation location = new ForumLocation();
			string[] s = encodedLocation.Split(':');
			try 
			{
				location.UrlName        = s[0];
				location.ForumGroupID   = int.Parse(s[1]);
				location.ForumID        = int.Parse(s[2]);
				location.PostID         = int.Parse(s[3]);
				location.UserID         = int.Parse(s[4]);
			} 
			catch {}

			return location;

		}

        #region LocationKey
		// Takes a URL used in the forums and performs a reverse
		// lookup to return a friendly name of the currently viewed
		// resource
		//
		public static string LocationKey () 
		{
		    CSContext csContext = CSContext.Current;
			string url = csContext.RawUrl;
            
			if (Globals.ApplicationPath.Length > 0)
				url = url.Replace(Globals.ApplicationPath, "").ToLower();

			NameValueCollection reversePaths = Globals.GetSiteUrls().ReversePaths;
			int forumGroupIDqs = -1;
			int forumIDqs = -1;
			int postIDqs = -1;
			int userIDqs = -1;
			int modeIDqs = -1;

			// Modify the url so we can perform a reverse lookup
			//
			try 
			{
				for (int i = 0; i < csContext.Context.Request.QueryString.Count; i++) 
				{
					string key = csContext.Context.Request.QueryString.Keys[i].ToLower();

					switch (key) 
					{
						case "forumid":
							forumIDqs = int.Parse(csContext.Context.Request.QueryString[key]);
							break;
						case "postid":
							postIDqs = int.Parse(csContext.Context.Request.QueryString[key]);
							break;
						case "userid":
							userIDqs = int.Parse(csContext.Context.Request.QueryString[key]);
							break;
						case "forumgroupid":
							forumGroupIDqs = int.Parse(csContext.Context.Request.QueryString[key]);
							break;
						case "mode":
							modeIDqs = int.Parse(csContext.Context.Request.QueryString[key]);
							break;
					}
					url = url.Replace(csContext.Context.Request.QueryString[key], "{"+i+"}");
				}

			} 
			catch 
			{
				return "";
			}

			string retval = reversePaths[url];

			if ((retval == null) || (retval == string.Empty))
				retval = "/";

			return retval + ":" + forumGroupIDqs + ":" + forumIDqs + ":" + postIDqs + ":" + userIDqs + ":" + modeIDqs;


		}
		#endregion

        #endregion

		#region Public properties
		public bool EnableWaitPage 
		{
			get 
			{
				return true;
			}
			set 
			{
				enableWaitPage = value;
			}

		}

		public ArrayList TabUrls
		{
			get
			{
				return urlData.TabUrls;
			}
		}

        public virtual string LocationFilter
        {
            get
            {
                return urlData.LocationFilter;
            }
        }

        public SiteUrlsData UrlData
        {
            get{return urlData;}
        }

		public NameValueCollection ReversePaths 
		{
			get 
			{
				return urlData.ReversePaths;
			}
		}

        public ArrayList ReWrittenUrls
        {
            get
            {
                return urlData.ReWrittenUrls;
            }
        }

        public NameValueCollection Locations
        {
            get
            {
                return urlData.Locations;
            }
        }

		#endregion

    }

    #region ReWrittenUrl
    public class ReWrittenUrl
    {
        private string _name; 
        private string _path;
        private Regex _regex=null;
        
        public ReWrittenUrl(string name, string pattern, string path)
        {
            _name = name;
            _path = path;
            _regex = new Regex(pattern,RegexOptions.IgnoreCase | RegexOptions.Compiled);
        }

        public bool IsMatch(string url)
        {
            return _regex.IsMatch(url);
        }

        public virtual string Convert(string url, string qs)
        {
            if(qs != null && qs.StartsWith("?"))
            {
                qs = qs.Replace("?","&");
            }
            return string.Format("{0}{1}", _regex.Replace(url, _path),qs);
        }
    }
    #endregion
}
