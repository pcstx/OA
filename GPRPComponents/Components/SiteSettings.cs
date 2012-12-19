//------------------------------------------------------------------------------
// <copyright company="Telligent Systems">
//     Copyright (c) Telligent Systems Corporation.  All rights reserved.
// </copyright> 
//------------------------------------------------------------------------------

// 修改说明：新增默认属性
// 修改人：宝玉
// 修改日期：2005-02-26

using System;
using System.Collections;
using System.Web;
using System.Xml;
using System.Xml.Serialization;
using Microsoft.ScalableHosting.Security;
using GPRP.GPRPEnumerations;

namespace GPRP.GPRPComponents {

    [Serializable]
    public class SiteSettings {
        CSContext csContext = CSContext.Current;

        Hashtable settings = new Hashtable();
        private int settingsID = 1;
        private string defaultTimeFormat = "HH:mm tt"; // 修改为24小时制
        private SearchMode defaultSearchMode = SearchMode.Simple;
        private bool defaultEnableEmail = false;
        private bool defaultEnableAutoPostDelete = false;
        private bool defaultEnableAutoUserDelete = false;
        private bool defaultEnableAnonymousUserTracking = true;
        private bool defaultEnableAnonymousUserPosting = false;
        private string defaultDateFormat = "yyyy-MM-dd"; //修改为年-月-日形式
        private string defaultSiteDescription = "Knowledge Management and Collaboration Platform";
        private string defaultTheme = "default";
        private string defaultSiteName = "Community Server";
        private string defaultSmtpServer = "localhost";
        private int defaultTimezoneOffset = 8; // 修改为北京时区
        private int defaultAnonymousCookieExpiration = 20;
        private string defaultAnonymousCookieName = "CSAnonymous";
		private string defaultCookieDomain = "localhost";
		private int defaultAnonymousUserOnlineTimeWindow = 30;
        private int defaultAvatarHeight = 96; // 修改为96，MSN头像标准
        private int defaultAvatarWidth = 96;
        private int defaultRoleCookieExpiration = 20;
		private string defaultRoleCookieName = "CSRoles";
        private bool defaultEnableAvatars = true;
        private bool defaultEnableDebugMode = false;
        private bool defaultEnableEmoticons = false;
        private bool defaultEnableRemoteAvatars = true; //修改为允许
        private bool defaultEnableRoleCookie = true;
        private bool defaultEnableVersionCheck = true;
		private string defaultWindowsDomain = "*";
		private string defaultEmailDomain = "@TempURI.org";
        private ThreadDateFilterMode threadDateFilter = ThreadDateFilterMode.TwoMonths;
		private bool defaultSmtpServerRequiredLogin = false;
		private string defaultSmtpServerUserName ="";
		private string defaultSmtpServerPassword = "";
		private bool defaultEnableAttachments = true;
		private string defaultAllowedAttachmentTypes = "zip;cab;jpg;gif;png;mpg;mpeg;avi;wmv;wma;mp3;ra;rar;rm;sql;txt";
		private Int32 defaultMaxAttachmentSize = 1024; // in Kilobytes
		private bool defaultEnablePostReporting = true;
		private bool defaultEnableAnonymousReporting = true;
		private int defaultReportingForum = 3;	// default admin forum for reporting posts.
		private bool disabledForums = false;
        private bool enableDefaultRole = true;
        private Guid siteKey;
        private UserActivityDisplayMode postingActivityDisplay = UserActivityDisplayMode.PostCount;
		private string _homePageContent = null;

		// 新增 用户默认磁盘空间
		private int defaultUserDatabaseQuota = 10240;
		// 允许直接显示在浏览器的附件类型
		private string defaultContentTypesDisplayInBrowser = "image/*;video/*;application/x-shockwave-flash";

		public string HomePageContent
		{
			get
			{
				if(Globals.IsNullorEmpty(_homePageContent))
					return ResourceManager.GetString("default_homepage_content");
				else
					return _homePageContent;
			}
			set { _homePageContent = value;}
		}
        
        #region EnableDefaultRole
        /// <summary>
        /// Should users be given a default role as they are added to the system
        /// </summary>
        public bool EnableDefaultRole
        {
            get
            {  return this.enableDefaultRole; }
            set
            {  this.enableDefaultRole = value; }
        }
        #endregion
        

		#region Forums Disabled
        public bool ForumsDisabled 
        {
            get { return disabledForums; 
//                string key = "ForumsDisabled";
//
//                if (settings[key] != null)
//                    return (bool) settings[key];
//                else
//                    return false;

            }
            set {
				disabledForums = value;
//                settings["ForumsDisabled"] = value;
            }
        }

		public bool BlogsDisabled {
			get {
				string key = "BlogsDisabled";

				if (settings[key] != null)
					return (bool) settings[key];
				else
					return false;

			}
			set {
				settings["BlogsDisabled"] = value;
			}
		}
		public bool GalleriesDisabled {
			get {
				string key = "GalleriesDisabled";

				if (settings[key] != null)
					return (bool) settings[key];
				else
					return false;

			}
			set {
				settings["GalleriesDisabled"] = value;
			}
		}

        public bool GuestBookDisabled 
        {
            get 
            {
                string key = "GuestBookDisabled";

                if (settings[key] != null)
                    return (bool) settings[key];
                else
                    return true;

            }
            set 
            {
                settings["GuestBookDisabled"] = value;
            }
        }
        #endregion

        #region General Settings
        public int SiteSettingsCacheWindowInMinutes {
            get {
                string key = "SiteSettingsCacheWindowInMinutes";

                if (settings[key] != null)
                    return (int) settings[key];
                else
                    return 15;
            }
            set {
                settings["SiteSettingsCacheWindowInMinutes"] = value;
            }
        }

		public bool EnableAds 
		{
			get 
			{
				string key = "EnableAds";

				if( settings[key] != null )
					return (bool) settings[key];
				else
					return false;
			}
			set 
			{
				settings["EnableAds"] = value;
			}
		}
        
		public bool EnableCollapsingPanels {
			get{
				string key = "enableCollapsingPanels";

				if( settings[key] != null )
					return (bool) settings[key];
				else
					return true;
			}
			set {
				settings["enableCollapsingPanels"] = value; 
			}
		}

		public string SearchMetaDescription {
			get {
				string key = "searchMetaDescription";

				if( settings[key] != null )
					return (string) settings[key];
				else
					return String.Empty;
			}
			set {
				settings["searchMetaDescription"] = value;
			}
		}

		public string SearchMetaKeywords {
			get {
				string key = "searchMetaKeywords";

				if( settings[key] != null )
					return (string) settings[key];
				else
					return String.Empty;
			}
			set {
				settings["searchMetaKeywords"] = value;
			}
		}
		
		public string Copyright {
			get {
				string key = "copyright";

				if( settings[key] != null )
					return (string) settings[key];
				else
					return String.Empty;
			}
			set {
				settings["copyright"] = value;
			}
		}


        #endregion

        #region Contact Details
        public string AdminEmailAddress 
		{
            get {
                string key = "AdminEmailAddress";

                if (settings[key] != null)
                    return (string) settings[key];
                else
                    return string.Empty;
            }
            set {
                settings["AdminEmailAddress"] = value;
            }
        }

        public string CompanyName {
            get {
                string key = "CompanyName";

                if (settings[key] != null)
                    return (string) settings[key];
                else
                    return string.Empty;

            }
            set {
                settings["CompanyName"] = value;
            }
        }

        public string CompanyContactUs {
            get {
                string key = "CompanyContactUs";

                if (settings[key] != null)
                    return (string) settings[key];
                else
                    return string.Empty;

            }
            set {
                settings["CompanyContactUs"] = value;
            }
        }

        public string CompanyFaxNumber {
            get {
                string key = "CompanyFaxNumber";

                if (settings[key] != null)
                    return (string) settings[key];
                else
                    return string.Empty;

            }
            set {
                settings["CompanyFaxNumber"] = value;
            }
        }

        public string CompanyAddress {
            get {
                string key = "CompanyAddress";

                if (settings[key] != null)
                    return (string) settings[key];
                else
                    return string.Empty;

            }
            set {
                settings["CompanyAddress"] = value;
            }
        }
        #endregion

        #region RSS
        public bool EnableRSS {
            get {
                string key = "EnableRSS";

                if (settings[key] != null)
                    return (bool) settings[key];
                else
                    return true;
            }
            set {
                settings["EnableRSS"] = value;
            }
        }

        public int RssDefaultThreadsPerFeed {
            get {
                string key = "RssDefaultThreadsPerFeed";

                if (settings[key] != null)
                    return (int) settings[key];
                else
                    return 25;
            }
            set {
                settings["RssDefaultThreadsPerFeed"] = value;
            }
        }

        public int RssMaxThreadsPerFeed {
            get {
                string key = "RssMaxThreadsPerFeed";

                if (settings[key] != null)
                    return (int) settings[key];
                else
                    return 50;
            }
            set {
                settings["RssMaxThreadsPerFeed"] = value;
            }
        }

        public int RssCacheWindowInMinutes {
            get {
                string key = "RssCacheWindow";

                if (settings[key] != null)
                    return (int) settings[key];
                else
                    return 15;
            }
            set {
                settings["RssCacheWindow"] = value;
            }
        }
        #endregion

        #region Menus and Sections
        public bool EnablePublicMemberList {
            get {
                string key = "EnablePublicMemberList";

                if (settings[key] != null)
                    return (bool) settings[key];
                else
                    return true;
            }
            set {
                settings["EnablePublicMemberList"] = value;
            }
        }

        public bool EnableBirthdays {
            get {
                string key = "EnableBirthdays";

                if (settings[key] != null)
                    return (bool) settings[key];
                else
                    return false;
            }
            set {
                settings["EnableBirthdays"] = value;
            }
        }

        public bool EnableCurrentTime {
            get {
                string key = "EnableCurrentTime";

                if (settings[key] != null)
                    return (bool) settings[key];
                else
                    return true;
            }
            set {
                settings["EnableCurrentTime"] = value;
            }
        }

        public bool EnableSiteStatistics {
            get {
                string key = "EnableSiteStatistics";

                if (settings[key] != null)
                    return (bool) settings[key];
                else
                    return true;
            }
            set {
                settings["EnableSiteStatistics"] = value;
            }
        }

        public bool EnableForumDescription {
            get {
                string key = "EnableForumDescription";

                if (settings[key] != null)
                    return (bool) settings[key];
                else
                    return true;
            }
            set {
                settings["EnableForumDescription"] = value;
            }
        }

        public bool EnableWhoIsOnline {
            get {
                string key = "EnableWhoIsOnline";

                if (settings[key] != null)
                    return (bool) settings[key];
                else
                    return true;
            }
            set {
                settings["EnableWhoIsOnline"] = value;
            }
        }
        #endregion

        #region User Registration
        public bool EnableUsernameChange {
            get {


				// Item 446, JLA:
				// WSHA doesn't allow user name changes. As such, I'm updating
				// site settings to always return false so that any other controls
				// will just assume no user name changes.
				return false;
//                string key = "EnableUsernameChange";
//
//                if (settings[key] != null)
//                    return (bool) settings[key];
//                else
//                    return false;
            }
            set {
                //settings["EnableUsernameChange"] = value;
            }
        }

        public MembershipPasswordFormat PasswordFormat {
            get {
                string key = "PasswordFormat";

                if (settings[key] != null)
                    return (MembershipPasswordFormat) settings[key];
                else
                    return MembershipPasswordFormat.Hashed;
            }
            set {
                settings["PasswordFormat"] = value;
            }
        }

        public AccountActivation AccountActivation {
            get {
                string key = "AccountActivation";

                if (settings[key] != null)
                    return (AccountActivation) settings[key];
                else
                    return AccountActivation.Automatic;
            }
            set {
                settings["AccountActivation"] = value;
            }
        }

        public PasswordRecovery PasswordRecovery {
            get {
                string key = "PasswordRecovery";

                if (settings[key] != null)
                    return (PasswordRecovery) settings[key];
                else
                    return PasswordRecovery.Reset;
            }
            set {
                settings["PasswordRecovery"] = value;
            }
        }

        public bool AllowUserSignatures {
            get {
                string key = "AllowUserSignatures";

                if (settings[key] != null)
                    return (bool) settings[key];
                else
                    return true;
            }
            set {
                settings["AllowUserSignatures"] = value;
            }
        }

        public bool AllowNewUserRegistration {
            get {
                string key = "AllowNewUserRegistration";

                if (settings[key] != null)
                    return (bool) settings[key];
                else
                    return true;
            }
            set {
                settings["AllowNewUserRegistration"] = value;
            }
        }

        public ModerationLevel NewUserModerationLevel {
            get {
                string key = "NewUserModerationLevel";

                if (settings[key] != null)
                    return (ModerationLevel) settings[key];
                else
                    return ModerationLevel.Moderated;
            }
            set {
                settings["NewUserModerationLevel"] = value;
            }
        }

        public bool AllowLogin {
            get {
                string key = "AllowLogin";

                if (settings[key] != null)
                    return (bool) settings[key];
                else
                    return true;
            }
            set {
                settings["AllowLogin"] = value;
            }
        }

        public bool EnableUserSignatures {
            get {
                string key = "EnableUserSignatures";

                if (settings[key] != null)
                    return (bool) settings[key];
                else
                    return true;
            }
            set {
                settings["EnableUserSignatures"] = value;
            }
        }

        public int UserSignatureMaxLength {
            get {
                string key = "UserSignatureMaxLength";

                if (settings[key] != null)
                    return (int) settings[key];
                else
                    return 256;
            }
            set {
                settings["UserSignatureMaxLength"] = value;
            }
        }

		public int UsernameMaxLength {
			get {
				string key = "UsernameMaxLength";

				if (settings[key] != null)
					return (int) settings[key];
				else
					return 64;
			}
			set {
				if ((value > 256) || (value < UsernameMinLength))
					settings["UsernameMaxLength"] = 64;
				else
					settings["UsernameMaxLength"] = value;
			}
		}

		
		public int UsernameMinLength {
			get {
				string key = "UsernameMinLength";

				if (settings[key] != null)
					return (int) settings[key];
				else
					return 3;
			}
			set {
				if ((value < 3) || (value > UsernameMaxLength))
					settings["UsernameMinLength"] = 3;
				else
					settings["UsernameMinLength"] = value;
			}
		}

		public string UsernameRegex {
			get {
				string key = "UsernameRegex";

				if (settings[key] != null)
					return (string) settings[key];
				else
					return "[a-zA-Z]+[^\\<\\>]*";
			}
			set {
					settings["UsernameRegex"] = value;
			}
		}

		public string PasswordRegex {
			get {
				string key = "PasswordRegex";

				if (settings[key] != null)
					return (string) settings[key];
				else
					return "(.*)";
			}
			set {
				settings["PasswordRegex"] = value;
			}
		}

        public bool AllowGender {
            get {
                string key = "AllowGender";

                if (settings[key] != null)
                    return (bool) settings[key];
                else
                    return true;
            }
            set {
                settings["AllowGender"] = value;
            }
        }

//		public bool AllowAutoRegistration
//		{
//			get 
//			{
//				string key = "AllowAutoRegistration";
//
//				if( settings[key] != null )
//					return (bool) settings[key];
//				else
//					return true;
//			}
//			set 
//			{
//				settings["AllowAutoRegistration"] = value;
//			}
//		}

		public bool StripDomainName 
		{
			get {
				string key = "StripDomainName";

				if( settings[key] != null )
					return (bool) settings[key];
				else
					return true;
			}
			set {
				settings["StripDomainName"] = value;
			}
		}
		
		/// <summary>
		/// 默认磁盘限额
		/// </summary>
		public int UserDatabaseQuota
		{
			get 
			{
				string key = "UserDatabaseQuota";
				if (settings[key] != null)
				{
					return (int) settings[key];
				}
				return defaultUserDatabaseQuota;
			}
			set 
			{
				settings["UserDatabaseQuota"] = value;
			}
		}

        #endregion

        #region MemberList
        public int MaxTopPostersToDisplay 
		{
            get {
                string key = "MaxTopPostersToDisplay";

                if (settings[key] != null)
                    return (int) settings[key];
                else
                    return 10;
            }
            set {
                if ((value < 0) || (value > 100))
                    value = 10;

                settings["MaxTopPostersToDisplay"] = value;
            }
        }

        public int MembersPerPage {
            get {
                string key = "MembersPerPage";

                if (settings[key] != null)
                    return (int) settings[key];
                else
                    return 20;
            }
            set {
                if ((value < 0) || (value > 100))
                    value = 20;

                settings["MembersPerPage"] = value;
            }
        }

        public bool EnablePublicAdvancedMemberSearch {
            get {
                string key = "EnablePublicAdvancedMemberSearch";

                if (settings[key] != null)
                    return (bool) settings[key];
                else
                    return true;
            }
            set {
                settings["EnablePublicAdvancedMemberSearch"] = value;
            }
        }


        #endregion

		#region Post Flooding
		public bool EnableFloodIntervalChecking {
			get{
				string key = "EnableFloodIntervalChecking";

				if( settings[key] != null )
					return (bool) settings[key];
				else
					return false;
			}
			set {
				settings["EnableFloodIntervalChecking"] = value; 
			}
		}

		public int MinimumTimeBetweenPosts {
			get {
				string key = "MinimumTimeBetweenPosts";

				if( settings[key] != null )
					return (int) settings[key];
				else
					return 15;
			}
			set {
				settings["MinimumTimeBetweenPosts"] = value; 
			}
		}
		#endregion

        #region User Posting
        public bool EnableDuplicatePosts {
            get {
                string key = "EnableDuplicatePosts";

                if (settings[key] != null)
                    return (bool) settings[key];
                else
                    return false;
            }
            set {
                settings["EnableDuplicatePosts"] = value;
            }
        }

        public int DuplicatePostIntervalInMinutes {
            get {
                string key = "DuplicatePostIntervalInMinutes";

                if (settings[key] != null)
                    return (int) settings[key];
                else
                    return 15;
            }
            set {
                settings["DuplicatePostIntervalInMinutes"] = value;
            }
        }

        public int AutoUnmoderateAferNApprovedPosts {
            get {
                string key = "AutoUnmoderateAferNApprovedPosts";

                if (settings[key] != null)
                    return (int) settings[key];
                else
                    return 50;
            }
            set {
                settings["AutoUnmoderateAferNApprovedPosts"] = value;
            }
        }

        public int PostInterval {
            get {
                string key = "PostInterval";

                if (settings[key] != null)
                    return (int) settings[key];
                else
                    return 15;
            }
            set {
                settings["PostInterval"] = value;
            }
        }

        public bool DisplayEditNotesInPost {
            get {
                string key = "DisplayEditNotesInPost";

                if (settings[key] != null)
                    return (bool) settings[key];
                else
                    return false;
            }
            set {
                settings["DisplayEditNotesInPost"] = value;
            }
        }

		public bool RequireEditNotes {
			get {
				string key = "requireEditNotes";

				if( settings[key] != null )
					return (bool) settings[key];
				else
					return false;
			}
			set {
				settings["requireEditNotes"] = value;
			}
		}

        public int PostEditBodyAgeInMinutes {
            get {
                string key = "PostEditBodyAgeInMinutes";

                if (settings[key] != null)
                    return (int) settings[key];
                else
                    return 0;
            }
            set {
                settings["PostEditBodyAgeInMinutes"] = value;
            }
        }

        public int PostEditTitleAgeInMinutes {
            get {
                string key = "PostEditTitleAgeInMinutes";

                if (settings[key] != null)
                    return (int) settings[key];
                else
                    return 5;
            }
            set {
                settings["PostEditTitleAgeInMinutes"] = value;
            }
        }

        public int PostDeleteAgeInMinutes {
            get {
                string key = "PostDeleteAgeInMinutes";

                if (settings[key] != null)
                    return (int) settings[key];
                else
                    return 5;
            }
            set {
                settings["PostDeleteAgeInMinutes"] = value;
            }
        }
        #endregion

        #region IP Address Tracking
        public bool EnableTrackPostsByIP {
            get {
                string key = "EnableTrackPostsByIP";

                if (settings[key] != null)
                    return (bool) settings[key];
                else
                    return true;
            }
            set {
                settings["EnableTrackPostsByIP"] = value;
            }
        }

        public bool DisplayPostIP {
            get {
                string key = "DisplayPostIP";

                if (settings[key] != null)
                    return (bool) settings[key];
                else
                    return false;
            }
            set {
                settings["DisplayPostIP"] = value;
            }
        }
        
        public bool DisplayPostIPAdminsModeratorsOnly {
            get {
                string key = "DisplayPostIPAdminsModeratorsOnly";

                if (settings[key] != null)
                    return (bool) settings[key];
                else
                    return true;
            }
            set {
                settings["DisplayPostIPAdminsModeratorsOnly"] = value;
            }
        }
        #endregion

        #region Properties
        [XmlIgnore]
        public Hashtable Settings 
        {
            get {
                return settings;
            }
            set {
                settings = value;
            }
        }

        [XmlIgnore]
        public int SettingsID {
            get {
                return settingsID;
            }
            set {
                settingsID = value;
            }
        }

        string siteDomain;
        [XmlIgnore]
        public string SiteDomain {
            get {
                return siteDomain;
            }
            set {
                siteDomain = value;
            }
        }

        private string applicationName;
        /// <summary>
        /// Property ApplicationName (string)
        /// </summary>
        [XmlIgnore]
        public string ApplicationName
        {
            get {  return this.applicationName; }
            set {  this.applicationName = value; }
        }

        [XmlIgnore]
        public Guid SiteKey
        {
            get{return siteKey;}
            set{siteKey = value;}
        }


        public bool EnableVersionCheck {
            get {
                string key = "EnableVersionCheck";

                if (settings[key] != null)
                    return (bool) settings[key];
                else
                    return defaultEnableVersionCheck;

            }
            set {
                settings["EnableVersionCheck"] = value;
            }
        }

        public bool EnableEmail {
            get {
                string key = "defaultEnableEmail";

                if (settings[key] != null)
                    return (bool) settings[key];
                else
                    return defaultEnableEmail;

            }
            set {
                settings["defaultEnableEmail"] = value;
            }
        }

		public int EmailThrottle {
			get {
				string key = "emailThrottle";

				if( settings[key] != null )
					return (int) settings[key];
				else
					return -1;
			}
			set {
				settings["emailThrottle"] = value;
			}
		}

        public bool EnableAntiSpamProtection {
          get {
            string key = "EnableAntiSpamProtection";

            if (settings[key] != null)
              return (bool) settings[key];
            else
              return false;
          }
          set {
            settings["EnableAntiSpamProtection"] = value;
          }
        }

        public ThreadDateFilterMode DefaultThreadDateFilter 
        {
            get {
                string key = "threadDateFilter";

                if (settings[key] != null)
                    return (ThreadDateFilterMode) settings[key];
                else
                    return threadDateFilter;
            }
            set {
                settings["threadDateFilter"] = value;
            }
        }


        public string DefaultTheme {
            get {
                string key = "DefaultTheme";

                if (settings[key] != null)
                    return (string) settings[key];
                else
                    return defaultTheme;

            }
            set {
                settings["DefaultTheme"] = value;
            }
        }

		public bool EnableUserThemeSelection {
			get {
				string key = "EnableUserThemeSelection";

				if (settings[key] != null)
					return (bool) settings[key];
				else
					return true;

			}
			set {
				settings["EnableUserThemeSelection"] = value;
			}
		}

        public string DomainName {
            get {
                return (string) settings["DomainName"];
            }
            set {
                settings["DomainName"] = value;
            }
        }

        public string SiteName {
            get {
                string key = "SiteName";

                if (settings[key] != null)
                    return (string) settings[key];
                else
                    return defaultSiteName;
            }
            set {
                settings["SiteName"] = value;
            }
        }

        public string SiteDescription {
            get {
                string key = "SiteDescription";

                if (settings[key] != null)
                    return (string) settings[key];
                else
                    return defaultSiteDescription;
            }
            set {
                settings["SiteDescription"] = value;
            }
        }

        public string DateFormat {
            get {
                string key = "DateFormat";

                if (settings[key] != null)
                    return (string) settings[key];
                else
                    return defaultDateFormat;
            }
            set {
                settings["DateFormat"] = value;
            }
        }

        public string TimeFormat {
            get {
                string key = "TimeFormat";
                
                if (settings[key] != null)
                    return (string) settings[key];
                else
                    return defaultTimeFormat;
            }
            set {
                settings["TimeFormat"] = value;
            }
        }

        public string SmtpServer {
            get {
                string key = "SmtpServer";

                if (settings[key] != null)
                    return (string) settings[key];
                else
                    return defaultSmtpServer;
            }
            set {
                settings["SmtpServer"] = value;
            }
        }


        public int TimezoneOffset {
            get {
                string key = "TimezoneOffset";

                if (settings[key] != null)
                    return (int) settings[key];
                else
                    return defaultTimezoneOffset;
            }
            set {
                settings["TimezoneOffset"] = value;
            }
        }

        public SearchMode SearchMode {
            get {
                string key = "SearchMode";

                if (settings[key] != null)
                    return (SearchMode) settings[key];
                else
                    return defaultSearchMode;
            }
            set {
                settings["SearchMode"] = value;
            }
        }



        public int ThreadsPerPage {
            get {
                string key = "ThreadsPerPage";

                if (settings[key] != null)
                    return (int) settings[key];
                else
                    return 20;
            }
            set {
                settings["ThreadsPerPage"] = value;
            }
        }

		public int SearchPostsPerPage {
			get {
				string key = "SearchPostsPerPage";

				if (settings[key] != null)
					return (int) settings[key];
				else
					return 10;
			}
			set {
				settings["SearchPostsPerPage"] = value;
			}
		}

        public int PostsPerPage {
            get {
                string key = "PostsPerPage";

                if (settings[key] != null)
                    return (int) settings[key];
                else
                    return 15;
            }
            set {
                settings["PostsPerPage"] = value;
            }
        }

        public int PopularPostLevelPosts {
            get {
                string key = "PopularPostLevelPosts";

                if (settings[key] != null)
                    return (int) settings[key];
                else
                    return 15;
            }
            set {
                settings["PopularPostLevelPosts"] = value;
            }
        }

        public int PopularPostLevelViews {
            get {
                string key = "PopularPostLevelViews";

                if (settings[key] != null)
                    return (int) settings[key];
                else
                    return 200;
            }
            set {
                settings["PopularPostLevelViews"] = value;
            }
        }

        public int PopularPostLevelDays {
            get {
                string key = "PopularPostLevelDays";

                if (settings[key] != null)
                    return (int) settings[key];
                else
                    return 3;
            }
            set {
                settings["PopularPostLevelDays"] = value;
            }
        }

        public bool EnableAutoPostDelete {
            get {
                string key = "EnableAutoPostDelete";

                if (settings[key] != null)
                    return (bool) settings[key];
                else
                    return defaultEnableAutoPostDelete;
            }
            set {
                settings["EnableAutoPostDelete"] = value;
            }
        }

        public bool EnableAutoUserDelete {
            get {
                string key = "EnableAutoUserDelete";

                if (settings[key] != null)
                    return (bool) settings[key];
                else
                    return defaultEnableAutoUserDelete;
            }
            set {
                settings["EnableAutoUserDelete"] = value;
            }
        }
 
        public bool EnableAnonymousUserTracking {
            get {
                string key = "EnableAnonymousUserTracking";

                if (settings[key] != null)
                    return (bool) settings[key];
                else
                    return defaultEnableAnonymousUserTracking;
            }
            set {
                settings["EnableAnonymousUserTracking"] = value;
            }
        }


        public bool EnableAnonymousUserPosting {
            get {
                string key = "EnableAnonymousUserPosting";

                if (settings[key] != null)
                    return (bool) settings[key];
                else
                    return defaultEnableAnonymousUserPosting;
            }
            set {
                settings["EnableAnonymousUserPosting"] = value;
            }
        }

        public int AvatarHeight {
            get {
                string key = "AvatarHeight";

                if (settings[key] != null)
                    return (int) settings[key];
                else
                    return defaultAvatarHeight;
            }
            set {
                settings["AvatarHeight"] = value;
            }
        }

        public int AvatarWidth {
            get {
                string key = "AvatarWidth";

                if (settings[key] != null)
                    return (int) settings[key];
                else
                    return defaultAvatarWidth;
            }
            set {
                settings["AvatarWidth"] = value;
            }
        }

        public string RoleCookieName {
            get {
                string key = "RoleCookieName";

                if (settings[key] != null)
                    return (string) settings[key];
                else
                    return defaultRoleCookieName;
            }
            set {
                settings["RoleCookieName"] = value;
            }
        }

        public int RoleCookieExpiration {
            get {
                string key = "RoleCookieExpiration";

                if (settings[key] != null)
                    return (int) settings[key];
                else
                    return defaultRoleCookieExpiration;
            }
            set {
                settings["RoleCookieExpiration"] = value;
            }
        }

        public string AnonymousCookieName {
            get {
                string key = "AnonymousCookieName";

                if (settings[key] != null)
                    return (string) settings[key];
                else
                    return defaultAnonymousCookieName;
            }
            set {
                settings["AnonymousCookieName"] = value;
            }
        }


		public string CookieDomain    
		{ 
			get 
			{ 
				string key = "CookieDomain"; 

				if (settings[key] != null) 
					return (string) settings[key]; 
				else 
					return defaultCookieDomain; 
			} 
			set 
			{ 
				settings["CookieDomain"] = value; 
			} 
		} 

        public int AnonymousCookieExpiration 
		{
            get {
                string key = "AnonymousCookieExpiration";

                if (settings[key] != null)
                    return (int) settings[key];
                else
                    return defaultAnonymousCookieExpiration;
            }
            set {
                settings["AnonymousCookieExpiration"] = value;
            }
        }

        public int AnonymousUserOnlineTimeWindow {
            get {
                string key = "AnonymousUserOnlineTimeWindow";

                if (settings[key] != null)
                    return (int) settings[key];
                else
                    return defaultAnonymousUserOnlineTimeWindow;
            }
            set {
                settings["AnonymousUserOnlineTimeWindow"] = value;
            }
        }

		public int UserOnlineTimeWindow {
			get {
				string key = "UserOnlineTimeWindow";

				if (settings[key] != null)
					return (int) settings[key];
				else
					return 15;
			}
			set {
				settings["UserOnlineTimeWindow"] = value;
			}
		}

        public bool EnableEmoticons {
            get {
                string key = "EnableEmoticons";

                if (settings[key] != null)
                    return (bool) settings[key];
                else
                    return defaultEnableEmoticons;
            }
            set {
                settings["EnableEmoticons"] = value;
            }
        }

        public bool EnableAvatars {
            get {
                string key = "EnableAvatars";

                if (settings[key] != null)
                    return (bool) settings[key];
                else
                    return defaultEnableAvatars;
            }
            set {
                settings["EnableAvatars"] = value;
            }
        }

        public bool EnableRemoteAvatars {
            get {
                string key = "EnableRemoteAvatars";

                if (settings[key] != null)
                    return (bool) settings[key];
                else
                    return defaultEnableRemoteAvatars;
            }
            set {
                settings["EnableRemoteAvatars"] = value;
            }
        }

        public bool EnableRoleCookie {
            get {
                string key = "EnableRoleCookie";

                if (settings[key] != null)
                    return (bool) settings[key];
                else
                    return defaultEnableRoleCookie;
            }
            set {
                settings["EnableRoleCookie"] = value;
            }
        }

        public bool EnableDebugMode {
            get {
                string key = "EnableDebugMode";

                if (settings[key] != null)
                    return (bool) settings[key];
                else
                    return defaultEnableDebugMode;
            }
            set {
                settings["EnableDebugMode"] = value;
            }
        }

		public string EmailDomain {
			get {
				string key = "EmailDomain";

				if (settings[key] != null)
					return (string) settings[key];
				else
					return defaultEmailDomain;
			}
			set {
				settings["EmailDomain"] = value;
			}
		}

		public string WindowsDomain	{
			get {
				string key = "WindowsDomain";

				if (settings[key] != null)
					return (string) settings[key];
				else
					return defaultWindowsDomain;
			}
			set {
				settings["WindowsDomain"] = value;
			}
		}
		
		public string SmtpServerUserName {
			get {
				string key = "SmtpServerUserName";
				if (settings[key] != null)
					return (string) settings[key];
				else
					return defaultSmtpServerUserName;
			}
			set {
				settings["SmtpServerUserName"] = value;
			}
		}

		public string SmtpServerPassword {
			get {
				string key = "SmtpServerPassword";
				if (settings[key] != null)
					return (string) settings[key];
				else
					return defaultSmtpServerPassword;
			}
			set {
				settings["SmtpServerPassword"] = value;
			}
		}

		public bool SmtpServerRequiredLogin {
			get {
				string key = "SmtpServerRequiredLogin";
				if (settings[key] != null)
					return (bool) settings[key];
				else
					return defaultSmtpServerRequiredLogin;
			}
			set {
				settings["SmtpServerRequiredLogin"] = value;
			}
		} 

		public bool EnableAttachments {
			get {
				string key = "EnableAttachments";

				if (settings[key] != null)
					return (bool) settings[key];
				else
					return defaultEnableAttachments;
			}
			set {
				settings["EnableAttachments"] = value;
			}
		} 		
		
		public int MaxAttachmentSize{
			get {
				string key = "MaxAttachmentSize";

				if (settings[key] != null)
					return (Int32) settings[key];
				else
					return defaultMaxAttachmentSize;
			}
			set {
				settings["MaxAttachmentSize"] = value;
			}
		}
		
		
		/// <summary>
		/// 在浏览器中直接打开，不弹出下载对话框的文件类型
		/// </summary>
		public string ContentTypesDisplayInBrowser
		{
			get 
			{
				string key = "ContentTypesDisplayInBrowser";

				if (settings[key] != null)
					return (string) settings[key];
				else
					return defaultContentTypesDisplayInBrowser;
			}
			set 
			{
				settings["ContentTypesDisplayInBrowser"] = value;
			}
		}	

		public string AllowedAttachmentTypes
		{
			get {
				string key = "AllowedAttachmentTypes";

				if (settings[key] != null)
					return (string) settings[key];
				else
					return defaultAllowedAttachmentTypes;
			}
			set {
				settings["AllowedAttachmentTypes"] = value;
			}
		}

		public bool EnablePostReporting {
			get {
				string key = "EnablePostReporting";

				if (settings[key] != null)
					return (bool) settings[key];
				else
					return defaultEnablePostReporting;
			}
			set {
				settings["EnablePostReporting"] = value;
			}
		} 		

		public bool EnableAnonymousReporting {
			get {
				string key = "EnableAnonymousReporting";

				if (settings[key] != null)
					return (bool) settings[key];
				else
					return defaultEnableAnonymousReporting;
			}
			set {
				settings["EnableAnonymousReporting"] = value;
			}
		} 		
		
		public int ReportingForum{
			get {
				string key = "ReportingForum";

				if (settings[key] != null)
					return (int) settings[key];
				else
					return defaultReportingForum;
			}
			set {
				settings["ReportingForum"] = value;
			}
		}

		public bool EnablePostPreviewPopup {
			get{
				string key = "EnablePostPreviewPopup";

				if( settings[key] != null )
					return (bool) settings[key];
				else
					return false;
			}
			set {
				settings["EnablePostPreviewPopup"] = value;
			}
		}

		public bool EnableInlinedImages {
			get{ 
				string key = "EnableInlinedImages";
				
				if( settings[key] != null )
					return (bool) settings[key];
				else
					return true;
			}
			set{ settings["EnableInlinedImages"] = value; }
		}

		public short InlinedImageWidth {
			get{
				string key = "InlinedImageWidth";

				if( settings[key] != null )
					return (short) settings[key];
				else
					return 550;
			}
			set{ settings["InlinedImageWidth"] = value;	}
		}

		public short InlinedImageHeight {
			get{
				string key = "InlinedImageHeight";

				if( settings[key] != null )
					return (short) settings[key];
				else
					return -1;
			}
			set{ settings["InlinedImageHeight"] = value; }
		}

		public string SupportedInlinedImageTypes {
			get{
				string key = "SupportedInlinedImageTypes";

				if( settings[key] != null )
					return (string) settings[key];
				else
					return "jpg;gif;bmp;pcx;png;pic";
			}
			set{ settings["SupportedInlinedImageTypes"] = value; }
		}

		public bool EnableCensorship {
			get{
				string key = "EnableCensorship";

				if( settings[key] != null )
					return (bool) settings[key];
				else
					return true;
			}
			set { settings["EnableCensorship"] = value;	}
		}
		#endregion

        #region Forum URLs
        public bool EnableSearchFriendlyURLs {
            get {
                string key = "EnableSearchFriendlyURLs";

                if (settings[key] != null)
                    return (bool) settings[key];
                else
                    return false;
            }
            set {
                settings["EnableSearchFriendlyURLs"] = value;
            }
        }
        #endregion
        
        #region Enable user ranking
        public UserActivityDisplayMode PostingActivityDisplay {
            get {
                string key = "postingActivityDisplay";

                if (settings[key] != null)
                    return (UserActivityDisplayMode) settings[key];
                else
                    return postingActivityDisplay;

            }
            set {
                settings["postingActivityDisplay"] = value;
            }
        }

		public bool DisplayUserRankAsPicture {
			get{
				string key = "DisplayUserRankAsPicture";

				if( settings[key] != null )
					return (bool) settings[key];
				else
					return false;
			}
			set { settings["DisplayUserRankAsPicture"] = value;	}
		}
        #endregion

        #region Enable users to post as anonymous
		public bool EnableUserPostingAsAnonymous {
			get{
				string key = "EnableUserPostingAsAnonymous";

				if( settings[key] != null )
					return (bool) settings[key];
				else
					return false;
			}
			set { settings["EnableUserPostingAsAnonymous"] = value;	}
		}
        #endregion

        #region Enable banned users to login
		public bool EnableBannedUsersToLogin {
			get{
				string key = "EnableBannedUsersToLogin";

				if( settings[key] != null )
					return (bool) settings[key];
				else
					return false;
			}
			set { settings["EnableBannedUsersToLogin"] = value;	}
		}
        #endregion

        #region Enable User Moderation Counters
		public bool EnableUserModerationCounters {
			get{
				string key = "EnableUserModerationCounters";

				if( settings[key] != null )
					return (bool) settings[key];
				else
					return false;
			}
			set { settings["EnableUserModerationCounters"] = value;	}
		}
        #endregion

        #region Enable thread status
		public bool EnableThreadStatus {
			get{
				string key = "EnableThreadStatus";

				if( settings[key] != null )
					return (bool) settings[key];
				else
					return true;
			}
			set { settings["EnableThreadStatus"] = value; }
		}
        #endregion
    }

}
