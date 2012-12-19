//------------------------------------------------------------------------------
// <copyright company="Telligent Systems">
//     Copyright (c) Telligent Systems Corporation.  All rights reserved.
// </copyright> 
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Specialized;
using System.Data;
using System.Diagnostics;
using System.Web.Mail;
using System.Xml;

using Microsoft.ScalableHosting.Profile;
using Microsoft.ScalableHosting.Security;
using GPRP.GPRPEnumerations;

namespace GPRP.GPRPComponents {

    /// <summary>
    /// The DataProvider class contains a single method, Instance(), which returns an instance of the
    /// user-specified data provider class.
    /// </summary>
    /// <remarks>  The data provider class must inherit the CommonDataProvider
    /// interface.</remarks>
	public abstract class CommonDataProvider {

        public static readonly string CommonDataProviderName = "CommonDataProvider";

		#region Instance

        private static CommonDataProvider _defaultInstance = null;

        static CommonDataProvider()
        {
            CreateDefaultCommonProvider();
        }

		/// <summary>
		/// Returns an instance of the user-specified data provider class.
		/// </summary>
		/// <returns>An instance of the user-specified data provider class.  This class must inherit the
		/// CommonDataProvider interface.</returns>
		public static CommonDataProvider Instance() {
            return _defaultInstance;
		}

        public static CommonDataProvider Instance (Provider dataProvider) 
        {
            CommonDataProvider fdp = CSCache.Get(dataProvider.Name) as CommonDataProvider;
            if(fdp == null)
            {
                fdp = DataProviders.Invoke(dataProvider) as CommonDataProvider;
                CSCache.Max(dataProvider.Name,fdp);
            }
            return fdp;
        }

        /// <summary>
        /// Creates the Default CommonDataProvider
        /// </summary>
        private static void CreateDefaultCommonProvider()
        {
            // Get the names of the providers
            //
            CSConfiguration config = CSConfiguration.GetConfig();

            // Read the configuration specific information
            // for this provider
            //
            Provider sqlForumsProvider = (Provider) config.Providers[CommonDataProviderName];

            // Read the connection string for this provider
            //
            
            _defaultInstance = DataProviders.CreateInstance(sqlForumsProvider) as CommonDataProvider;
        }
        
        #endregion

        #region Site Settings
		public abstract void CalculateSiteStatistics(int settingsID, int updateWindow);
        public abstract SiteStatistics LoadSiteStatistics(int settingsID);
        public abstract SiteSettings LoadSiteSettings(string application);
        public abstract ArrayList LoadAllSiteSettings ();
        public abstract void SaveSiteSettings(SiteSettings siteSettings);
		public abstract Hashtable GetSiteUrls();

        public static SiteSettings PopulateSiteSettingsFromIDataReader(IDataReader dr) 
        {
			SiteSettings settings = null;

			try {
				settings =  Serializer.ConvertToObject(dr["SettingsXML"] as string,typeof(SiteSettings)) as SiteSettings;

                //When new sites/communities are created, it is very likely they will not have any XML history.
                //Serializer.ConvertToObject will return null if no XML is present.
                if(settings == null) 
                    settings = new SiteSettings();
			} catch (Exception exception) {

				if (exception.InnerException is XmlException) {

					Debug.WriteLine("Critical Error: SiteSettings serialized XML is invalid");
					CSException csException = new CSException(CSExceptionType.SiteSettingsInvalidXML);

					throw csException;
				
				} else {
					throw exception;
				}

			}

			// read addition data
			settings.SettingsID = (int) dr["SettingsID"];
			settings.SiteDomain = dr["SiteUrl"] as string;
			settings.ForumsDisabled = (bool)dr["Disabled"];
			settings.SiteKey = (Guid)dr["SettingsKey"];
			settings.ApplicationName = dr["ApplicationName"] as string;

			return settings;
        }

        #endregion

        #region Post

		public abstract bool IsThreadTracked(int threadID, int userID);
        public abstract void AddPostAttachment(Post post, PostAttachment attachment);
		public abstract void DeletePostAttachment(int postID);
        public abstract PostAttachment GetPostAttachment (int postID);
        public abstract void ReverseThreadTracking(int userID, int PostID);
		public abstract EmailSubscriptionType GetSectionSubscriptionType(int SectionID, int UserID);
		public abstract void SetSectionSubscriptionType(int SectionID, int UserID, EmailSubscriptionType subType);
		public abstract  void RemoveThreadTracking(int SectionID, int UserID);
        
        #endregion

        #region Ratings
        public abstract void ThreadRate (int threadID, int userID, int rating);
        public abstract ArrayList ThreadRatings (int threadID);
        #endregion

        #region Referrals

        public abstract void SaveReferralList(ArrayList referrals);
        public abstract ReferralSet GetReferrals(Referral r, int pageSize, int pageIndex);

        #endregion

        public abstract void SaveViewList(Hashtable views);
        
        #region Messages
        public abstract ArrayList GetMessages(int messageID);
        public abstract void CreateUpdateDeleteMessage(Message message, DataProviderAction action);
        #endregion

        #region Emails
        public abstract Hashtable GetEmailsTrackingThread(int postID);
		public abstract Hashtable GetEmailsTrackingSectionByPostID(int postID);
        public abstract void EmailEnqueue (MailMessage email);
        public abstract void EmailDelete (Guid emailID);
		public abstract void EmailFailure (ArrayList list, int failureInterval, int maxNumberOfTries);
        public abstract ArrayList EmailDequeue (int settingsID);

        // *********************************************************************
        //
        //  PopulateEmailFromIDataReader
        //
        /// <summary>
        /// This private method accepts a datareader and attempts to create and
        /// populate a EmailTempalte class instance which is returned to the caller.
        /// </summary>
        //
        // ********************************************************************/
        public static EmailTemplate PopulateEmailFromIDataReader(IDataReader reader) {
            EmailTemplate email = new EmailTemplate();

            email.EmailID       = (Guid) reader["EmailID"];
            email.Priority      = (MailPriority) (int) reader["emailPriority"];
			email.BodyFormat    = (MailFormat) (int) reader["emailBodyFormat"];
            email.Subject       = (string) reader["emailSubject"];
            email.To            = (string) reader["emailTo"];
            email.From          = (string) reader["emailFrom"];
            email.Body          = (string) reader["emailBody"];
			email.NumberOfTries = (int) reader["numberOfTries"];

            if (reader["emailCc"] != DBNull.Value)
                email.Cc = (string) reader["emailCc"];

            if (reader["emailBcc"] != DBNull.Value)
                email.Bcc = (string) reader["emailBcc"];


            return email;
        }
        #endregion

        #region Sections
        public abstract int CreateUpdateDeleteSection(Section section, DataProviderAction action);
		public abstract void ChangeSectionSortOrder(int forumID, bool moveUp); 
		public abstract HybridDictionary GetSectionsRead(int userID);
        #endregion

        #region Permissions
        public abstract void CreateUpdateDeletePermission(PermissionBase p, DataProviderAction action);


        public static void PopulatePermissionFromIDataReader( PermissionBase p, IDataReader reader) {

			p.Name          = (string) reader["RoleName"];
			p.RoleID        = (Guid) reader["RoleID"];
			p.SectionID       = (int) reader["SectionID"];
			p.Implied		= (bool)reader["Implied"];
			p.AllowMask		= (Permission) (long)reader["AllowMask"];
			p.DenyMask		= (Permission) (long)reader["DenyMask"];

		}


        #endregion

        #region Groups
        public abstract int CreateUpdateDeleteGroup(Group group, DataProviderAction action);
        public abstract void ChangeGroupSortOrder(int forumGroupID, bool moveUp);
       
        public abstract Hashtable GetGroups(ApplicationType appType, bool requireModeration);

        public static Group PopulateForumGroupFromIDataReader(IDataReader dr) {

            Group forumGroup               = new Group();
            forumGroup.GroupID             = (int) dr["GroupID"];
            forumGroup.Name                = (string) dr["Name"];
            forumGroup.NewsgroupName       = (string) dr["NewsgroupName"];
            forumGroup.SortOrder           = Convert.ToInt32(dr["SortOrder"]);
            forumGroup.ApplicationType     = (ApplicationType) (Int16) dr["ApplicationType"];

            return forumGroup;
        }
        #endregion

        #region User
		public abstract User GetAnonymousUser( int settingsID );
        public abstract Avatar GetUserAvatar (int userID);

		// 增加IP纪录和根据昵称取用户资料的方法
        public abstract User GetUser(int userID, string username, string nickname, bool isOnline, bool isEditable, string lastAction, string ipAddress);
        public abstract User CreateUpdateDeleteUser(User user, DataProviderAction action, out CreateUserStatus status);
        public abstract int UpdateAnonymousUsers(Hashtable anonymousUserList, int settingsID);
        public abstract int GetUserIDByEmail(string emailAddress);
		public abstract int GetUserIDByAppUserToken(string appUserToken);
		public abstract Hashtable WhoIsOnline(int pastMinutes);
        public abstract void UserChangePasswordAnswer(int userID, string newQuestion, string newAnswer);
        public abstract void ToggleOptions(string username, bool hideReadThreads, ViewOptions viewOptions);
        public abstract UserSet GetUsers(int pageIndex, int pageSize, SortUsersBy sortBy, SortOrder sortOrder, string usernameFilter, bool includeEmailInFilter, UserAccountStatus accountStatus, bool returnRecordCount, bool includeHiddenUsers, bool returnModerationCounters);
		public abstract void ToggleUserForceLogin(User user);
        public abstract bool UpgradePassword(string username, string password);
        public abstract bool ValidateUserPasswordAnswer(object UserId, string answer);
        #endregion

        #region Search
        //public abstract SearchResultSet GetSearchResults(int pageIndex, int pageSize, int userID, string[] forumsToSearch, string[] usersToSearch, string[] andTerms, string[] orTerms);
        public abstract Hashtable GetSearchIgnoreWords(int settingsID);
        public abstract void CreateDeleteSearchIgnoreWords (ArrayList words, DataProviderAction action);
        public abstract void InsertIntoSearchBarrel (Hashtable words, Post post, int settingsID);
        public abstract PostSet SearchReindexPosts (int setsize, int settingsID);
        #endregion
	
        #region Roles
		public abstract Hashtable GetRoles(int userID);
        public abstract UserSet UsersInRole(int pageIndex, int pageSize, SortUsersBy sortBy, SortOrder sortOrder, Guid roleID, UserAccountStatus accountStatus, bool returnRecordCount);
		public abstract Role GetRole(Guid roleID, string RoleName);
//		public abstract void AddUserToRole(int userID, Guid roleID);s
//		public abstract void RemoveUserFromRole(int userID, Guid roleID);
		public abstract void AddForumToRole(int forumID, Guid roleID);
		public abstract void RemoveForumFromRole(int forumID, Guid roleID);
		public abstract Guid CreateUpdateDeleteRole(Role role, string applicationName, DataProviderAction action);
        #endregion

        #region Vote
        public abstract void Vote(int postID, int userID, string selection);
        public abstract PollSummary GetPoll(PollSummary summary);
        #endregion

        #region Exceptions and Tracing
        public abstract void LogException (CSException exception, int SettingsID);

		public void LogException(CSException exception)
		{
			LogException(exception,CSContext.Current.SiteSettings.SettingsID);
		}

        public abstract ArrayList GetExceptions (int exceptionType, int minFrequency);
        public abstract void DeleteExceptions( int settingsID, ArrayList deleteList );

        public static CSException PopulateForumExceptionFromIDataReader (IDataReader reader) {

            CSException exception = new CSException( (CSExceptionType) (int) reader["Category"], (string) reader["ExceptionMessage"]);

            exception.LoggedStackTrace  = (string) reader["Exception"];
            exception.IPAddress         = (string) reader["IPAddress"];
            exception.UserAgent         = (string) reader["UserAgent"];
            exception.HttpReferrer      = (string) reader["HttpReferrer"];
            exception.HttpVerb          = (string) reader["HttpVerb"];
            exception.HttpPathAndQuery  = (string) reader["PathAndQuery"];
            exception.DateCreated       = (DateTime) reader["DateCreated"];
            exception.DateLastOccurred  = (DateTime) reader["DateLastOccurred"];
            exception.Frequency         = (int) reader["Frequency"];
            exception.ExceptionID       = (int) reader["ExceptionID"];

            return exception;
        }
        #endregion

		#region Audit
        public abstract AuditSet GetAuditHistoryForPost (int postID, int pageIndex, int pageSize, bool returnRecordCount);
        public abstract AuditSet GetAuditHistoryForUser (int userID, ModeratorActions action, int pageIndex, int pageSize, bool returnRecordCount); 
        public abstract AuditSummary GetAuditSummary (int postID, int userID);
        public abstract void SaveUserAuditEvent (ModerateUserSetting auditFlag, User user, int moderatorID);

		public static AuditItem PopulateAuditItemFromIDataReader (IDataReader reader) {

			AuditItem auditItem = new AuditItem();

            auditItem.Action = (ModeratorActions) Convert.ToInt32( reader["ModerationAction"] );
            auditItem.DateModerated = Convert.ToDateTime( reader["ModeratedOn"] );
            
            try {
                auditItem.PostID = Convert.ToInt32( reader["PostID"] );
            } catch { }

            try {
                auditItem.SectionID = Convert.ToInt32( reader["SectionID"] );
            } catch { }

            try {
                auditItem.UserID = Convert.ToInt32( reader["UserID"] );
            } catch { }
            
            try {
                auditItem.UserName = Convert.ToString( reader["UserName"] );
            } catch { }

            try {
                auditItem.ModeratorID = Convert.ToInt32( reader["ModeratorID"] );
            } catch { }
            
            try {
                auditItem.ModeratorName = Convert.ToString( reader["ModeratorName"] );
            } catch { }

            try {
                auditItem.Notes = Convert.ToString( reader["Notes"] );
            } catch { }

			return auditItem;
		}

        public static AuditSummary PopulateAuditSummaryFromIDataReader (IDataReader reader) {
            AuditSummary summary = new AuditSummary();

            try {
                summary[ModeratorActions.ApprovePost] = (reader["ApprovePost"] != DBNull.Value) ? (int) reader["ApprovePost"] : 0;
                summary[ModeratorActions.BanUser] = (reader["BanUser"] != DBNull.Value) ? (int) reader["BanUser"] : 0;
                summary[ModeratorActions.ChangePassword] = (reader["ChangePassword"] != DBNull.Value) ? (int) reader["ChangePassword"] : 0;
                summary[ModeratorActions.DeletePost] = (reader["DeletePost"] != DBNull.Value) ? (int) reader["DeletePost"] : 0;
                summary[ModeratorActions.EditPost] = (reader["EditPost"] != DBNull.Value) ? (int) reader["EditPost"] : 0;
                summary[ModeratorActions.EditUser] = (reader["EditUser"] != DBNull.Value) ? (int) reader["EditUser"] : 0;
                summary[ModeratorActions.LockPost] = (reader["LockPost"] != DBNull.Value) ? (int) reader["LockPost"] : 0;
                summary[ModeratorActions.MergePost] = (reader["MergePost"] != DBNull.Value) ? (int) reader["MergePost"] : 0;
                summary[ModeratorActions.ModerateUser] = (reader["ModerateUser"] != DBNull.Value) ? (int) reader["ModerateUser"] : 0;
                summary[ModeratorActions.MovePost] = (reader["MovePost"] != DBNull.Value) ? (int) reader["MovePost"] : 0;
                summary[ModeratorActions.PostIsAnnouncement] = (reader["PostIsAnnouncement"] != DBNull.Value) ? (int) reader["PostIsAnnouncement"] : 0;
                summary[ModeratorActions.PostIsNotAnnoucement] = (reader["PostIsNotAnnoucement"] != DBNull.Value) ? (int) reader["PostIsNotAnnoucement"] : 0;
                summary[ModeratorActions.ResetPassword] = (reader["ResetPassword"] != DBNull.Value) ? (int) reader["ResetPassword"] : 0;
                summary[ModeratorActions.SplitPost] = (reader["SplitPost"] != DBNull.Value) ? (int) reader["SplitPost"] : 0;
                summary[ModeratorActions.UnApprovePost] = (reader["UnApprovePost"] != DBNull.Value) ? (int) reader["UnApprovePost"] : 0;
                summary[ModeratorActions.UnbanUser] = (reader["UnbanUser"] != DBNull.Value) ? (int) reader["UnbanUser"] : 0;
                summary[ModeratorActions.UnlockPost] = (reader["UnlockPost"] != DBNull.Value) ? (int) reader["UnlockPost"] : 0;
                summary[ModeratorActions.UnmoderateUser] = (reader["UnmoderateUser"] != DBNull.Value) ? (int) reader["UnmoderateUser"] : 0;
            } 
            catch { 
                // this should happen if one of the query column is missing
                //
                summary = null;
            }

            return summary;
        }
		#endregion

        #region Censorship
        public abstract ArrayList GetCensors();
		public abstract int CreateUpdateDeleteCensor( Censor censor, DataProviderAction action );
		public static Censor PopulateCenshorFromIDataReader( IDataReader dr ) {
			Censor censorship = new Censor();

			censorship.Replacement	= Convert.ToString(dr["Replacement"]);
			censorship.Word			= Convert.ToString(dr["Word"]);

			return censorship;
		}
        #endregion

        #region Disallowed Names
        public abstract ArrayList GetDisallowedNames();
        public abstract int CreateUpdateDeleteDisallowedName(string name, string replacement, DataProviderAction action);
        #endregion
        
        #region Resources
        public abstract void CreateUpdateDeleteImage (int userID, Avatar avatar, DataProviderAction action);
        #endregion

        #region Categories
            
            public abstract Hashtable GetCategories(int forumID);
            public abstract void DeleteCategory(int categoryID, int forumID);
            public abstract bool UpdateCategory(PostCategory category);
            public abstract int CreateCategory(PostCategory category);

        #endregion

		#region Blog Links

		public abstract Int32 CreateLink( Link link );
		public abstract Boolean UpdateLink( Link link );
		public abstract void DeleteLink( Int32 linkID );
		public abstract Hashtable GetLinks( Int32 linkCategoryID );
		public abstract void ChangeLinkSortOrder( Int32 linkID, Boolean moveUp );

		public abstract Int32 CreateLinkCategory( LinkCategory linkCategory );
		public abstract Boolean UpdateLinkCategory( LinkCategory linkCategory );
		public abstract void DeleteLinkCategory( Int32 linkCategoryID, Int32 ForumID );
		public abstract Hashtable GetLinkCategories( Int32 forumID );
		public abstract void ChangeLinkCategorySortOrder( Int32 linkCategoryID, Boolean moveUp );

		#endregion

		#region Blog Feedback

		public abstract ArrayList GetFeedback( Int32 forumID );
		public abstract ArrayList GetFeedback( Int32 forumID, Int32 pageIndex, Int32 pageSize, out Int32 totalRecords );

		#endregion

        #region RANKS FUNCTIONS
		/************************ RANKS FUNCTIONS *****************************/
		public abstract ArrayList GetRanks();
		public abstract int CreateUpdateDeleteRank( Rank rank, DataProviderAction action );
		public static Rank PopulateRankFromIDataReader( IDataReader dr ) {
			Rank rank = new Rank();
			
			rank.PostingCountMaximum	= Convert.ToInt32(dr["PostingCountMax"]);
			rank.PostingCountMinimum	= Convert.ToInt32(dr["PostingCountMin"]);
			rank.RankIconUrl			= Convert.ToString(dr["RankIconUrl"]);
			rank.RankId					= Convert.ToInt32(dr["RankId"]);
			rank.RankName				= Convert.ToString(dr["RankName"]);

			return rank;
		}
       

		/**********************************************************************/

         #endregion

        #region REPORTS FUNCTIONS
		/************************ REPORTS FUNCTIONS ***************************/
		public abstract ArrayList GetReports(int userID, bool ignorePermissions);
		public abstract int CreateUpdateDeleteReport( Report report, DataProviderAction action );
		public static Report PopulateReportFromIDataReader( IDataReader dr ) {
			Report report = new Report();

			report.IsActive			= Convert.ToBoolean(dr["Active"]);
			report.ReportCommand	= Convert.ToString(dr["ReportCommand"]);
			report.ReportId			= Convert.ToInt32(dr["ReportId"]);
			report.ReportName		= Convert.ToString(dr["ReportName"]);
			report.ReportScript		= Convert.ToString(dr["ReportScript"]);

			return report;
		}
		
		/**********************************************************************/

        #endregion

        #region SERVICES FUNCTIONS

		/************************* SERVICES FUNCTIONS *************************/
		public abstract ArrayList GetServices(int userID, bool ignorePermissions);
		public abstract int CreateUpdateDeleteService( Service service, DataProviderAction action );
		public static Service PopulateServiceFromIDataReader( IDataReader dr ) {
			Service service = new Service();

			service.ServiceAssemblyPath		= Convert.ToString(dr["ServiceAssemblyPath"]);
			service.ServiceCode				= (Service.ServiceCodeType)Convert.ToInt32(dr["ServiceCode"]);
			service.ServiceFullClassName	= Convert.ToString(dr["ServiceFullClassName"]);
			service.ServiceId				= Convert.ToInt32(dr["ServiceId"]);
			service.ServiceName				= Convert.ToString(dr["ServiceName"]);
			service.ServiceWorkingDirectory	= Convert.ToString(dr["ServiceWorkingDirectory"]);

			return service;
		}

		/**********************************************************************/

        #endregion

        #region SMILIES FUNCTIONS

		/************************ SMILIES FUNCTIONS ***************************/
		public abstract ArrayList GetSmilies();
		public abstract int CreateUpdateDeleteSmiley( Smiley smiley, DataProviderAction action );
		public static Smiley PopulateSmileyFromIDataReader( IDataReader dr ) {
			Smiley smiley = new Smiley( Convert.ToInt32(dr["SmileyID"])
										, Convert.ToString( dr["SmileyCode"])
										, Convert.ToString( dr["SmileyUrl"])
										, Convert.ToString( dr["SmileyText"])
										, Convert.ToBoolean( dr["BracketSafe"]) );

//			smiley.SmileyCode	= Convert.ToString(dr["SmileyCode"]);
//			smiley.SmileyId		= Convert.ToInt32(dr["SmileyId"]);
//			smiley.SmileyText	= Convert.ToString(dr["SmileyText"]);
//			smiley.SmileyUrl	= Convert.ToString(dr["SmileyUrl"]);

			return smiley;
		}
		
		/**********************************************************************/

        #endregion

        #region STYLE FUNCTIONS
		/********************** STYLE FUNCTIONS *******************************/
		public abstract ArrayList GetStyles(int userID, bool ignorePermissions);
		public abstract int CreateUpdateDeleteStyle( Style style, DataProviderAction action );
		public static Style PopulateStyleFromIDataReader( IDataReader dr ) {
			Style style = new Style();

			style.StyleId				= Convert.ToInt32(dr["StyleId"]);
			style.StyleName				= Convert.ToString(dr["StyleName"]);
			style.StyleSheetTemplate	= Convert.ToString(dr["StyleSheetTemplate"]);
			style.BodyBackgroundColor	= Convert.ToString(dr["BodyBackgroundColor"]);
			style.BodyTextColor			= Convert.ToString(dr["BodyTextColor"]);
			style.LinkVisited			= Convert.ToString(dr["LinkVisited"]);
			style.LinkHover				= Convert.ToString(dr["LinkHover"]);
			style.LinkActive			= Convert.ToString(dr["LinkActive"]);
			style.RowColorPrimary		= Convert.ToString(dr["RowColorPrimary"]);
			style.RowColorSecondary		= Convert.ToString(dr["RowColorSecondary"]);
			style.RowColorTertiary		= Convert.ToString(dr["RowColorTertiary"]);
			style.RowClassPrimary		= Convert.ToString(dr["RowClassPrimary"]);
			style.RowClassSecondary		= Convert.ToString(dr["RowClassSecondary"]);
			style.RowClassTertiary		= Convert.ToString(dr["RowClassTertiary"]);
			style.HeaderColorPrimary	= Convert.ToString(dr["HeaderColorPrimary"]);
			style.HeaderColorSecondary	= Convert.ToString(dr["HeaderColorSecondary"]);
			style.HeaderColorTertiary	= Convert.ToString(dr["HeaderColorTertiary"]);
			style.HeaderStylePrimary	= Convert.ToString(dr["HeaderStylePrimary"]);
			style.HeaderStyleSecondary	= Convert.ToString(dr["HeaderStyleSecondary"]);
			style.HeaderStyleTertiary	= Convert.ToString(dr["HeaderStyleTertiary"]);
			style.CellColorPrimary		= Convert.ToString(dr["CellColorPrimary"]);
			style.CellColorSecondary	= Convert.ToString(dr["CellColorSecondary"]);
			style.CellColorTertiary		= Convert.ToString(dr["CellColorTertiary"]);
			style.CellClassPrimary		= Convert.ToString(dr["CellClassPrimary"]);
			style.CellClassSecondary	= Convert.ToString(dr["CellClassSecondary"]);
			style.CellClassTertiary		= Convert.ToString(dr["CellClassTertiary"]);
			style.FontFacePrimary		= Convert.ToString(dr["FontFacePrimary"]);
			style.FontFaceSecondary		= Convert.ToString(dr["FontFaceSecondary"]);
			style.FontFaceTertiary		= Convert.ToString(dr["FontFaceTertiary"]);
			style.FontSizePrimary		= Convert.ToInt16(dr["FontSizePrimary"]);
			style.FontSizeSecondary		= Convert.ToInt16(dr["FontSizeSecondary"]);
			style.FontSizeTertiary		= Convert.ToInt16(dr["FontSizeTertiary"]);
			style.FontColorPrimary		= Convert.ToString(dr["FontColorPrimary"]);
			style.FontColorSecondary	= Convert.ToString(dr["FontColorSecondary"]);
			style.FontColorTertiary		= Convert.ToString(dr["FontColorTertiary"]);
			style.SpanClassPrimary		= Convert.ToString(dr["SpanClassPrimary"]);
			style.SpanClassSecondary	= Convert.ToString(dr["SpanClassSecondary"]);
			style.SpanClassTertiary		= Convert.ToString(dr["SpanClassTertiary"]);

			return style;
		}
		
		/**********************************************************************/
	
        #endregion

        #region Post  FUNCTIONS



        public static Rating PopulateRatingFromIDataReader(IDataReader dr) {
            Rating rating = new Rating();

            rating.User = PopulateUserFromIDataReader(dr,PopulateMembershipUserFromIDataReader(dr,null),false);
            rating.Value = (int) dr["Rating"];

            return rating;
        }


		public static void PopulateIndexPostFromIDataReader(IDataReader dr, Post post) {

			// Populate Post
			//
			post.PostID             = Convert.ToInt32(dr["PostID"]);
			post.ParentID           = Convert.ToInt32(dr["ParentID"]);
			post.FormattedBody      = Convert.ToString(dr["FormattedBody"]).Trim();
			post.Body               = Convert.ToString(dr["Body"]).Trim();
			post.SectionID            = Convert.ToInt32(dr["SectionID"]);
			post.PostDate           = Convert.ToDateTime(dr["PostDate"]);
			post.PostLevel          = Convert.ToInt32(dr["PostLevel"]);
			post.SortOrder          = Convert.ToInt32(dr["SortOrder"]);
			post.Subject            = Convert.ToString(dr["Subject"]).Trim();
			post.ThreadDate         = Convert.ToDateTime(dr["ThreadDate"]);
			post.ThreadID           = Convert.ToInt32(dr["ThreadID"]);
			post.Replies            = Convert.ToInt32(dr["Replies"]);
			post.Username           = Convert.ToString(dr["Username"]).Trim();
			post.IsApproved         = Convert.ToBoolean(dr["IsApproved"]);
			post.IsLocked           = Convert.ToBoolean(dr["IsLocked"]);
			post.Views              = Convert.ToInt32(dr["TotalViews"]);
			post.HasRead            = Convert.ToBoolean(dr["HasRead"]);
			post.UserHostAddress    = (string) dr["IPAddress"];
			post.PostType           = (PostType) dr["PostType"];
			post.EmoticonID			= (int) dr["EmoticonID"];
            post.PostConfiguration  =   (int) dr["PostConfiguration"];
        }
 
        public static void PopulatePostFromIDataReader(IDataReader dr, Post post) {

            // Populate Post
            //
            post.PostID             = Convert.ToInt32(dr["PostID"]);
            post.ParentID           = Convert.ToInt32(dr["ParentID"]);
            post.FormattedBody      = Convert.ToString(dr["FormattedBody"]).Trim();
            post.Body               = Convert.ToString(dr["Body"]).Trim();
            post.SectionID            = Convert.ToInt32(dr["SectionID"]);
            post.PostDate           = Convert.ToDateTime(dr["PostDate"]);
            post.PostLevel          = Convert.ToInt32(dr["PostLevel"]);
            post.SortOrder          = Convert.ToInt32(dr["SortOrder"]);
            post.Subject            = Convert.ToString(dr["Subject"]).Trim();
            post.ThreadDate         = Convert.ToDateTime(dr["ThreadDate"]);
            post.ThreadID           = Convert.ToInt32(dr["ThreadID"]);
            post.Replies            = Convert.ToInt32(dr["Replies"]);
			
			// 修改为显示昵称
            post.Username           = Convert.ToString(dr["Nickname"]).Trim(); 
			if (post.Username == "")
				post.Username		= Convert.ToString(dr["Username"]).Trim();

            post.IsApproved         = Convert.ToBoolean(dr["IsApproved"]);
            post.AttachmentFilename = dr["AttachmentFilename"] as string;
            post.IsLocked           = Convert.ToBoolean(dr["IsLocked"]);
            post.Views              = Convert.ToInt32(dr["TotalViews"]);
            post.HasRead            = Convert.ToBoolean(dr["HasRead"]);
            post.UserHostAddress    = (string) dr["IPAddress"];
            post.PostType           = (PostType) dr["PostType"];
			post.EmoticonID			= (int) dr["EmoticonID"];
            post.PostConfiguration  = (int) dr["PostConfiguration"];
			
            //post.DeserializeExtendedAttributes(dr["StringNameValues"] as byte[]);
            SerializerData data = PopulateSerializerDataIDataReader(dr, SerializationType.Post);
            post.SetSerializerData(data);

            try {
                post.IsTracked          = (bool) dr["UserIsTrackingThread"];
            } catch {}

            try {
                post.EditNotes = dr["EditNotes"] as string;
            } catch {}

            try {
                post.ThreadIDNext = (int) dr["NextThreadID"];
                post.ThreadIDPrev = (int) dr["PrevThreadID"];
            } catch {}

            // Populate User
            //
            post.User = cs_PopulateUserFromIDataReader(dr);           
        }

        #endregion
            
        /// <summary>
        /// Builds and returns an instance of the Forum class based on the current row of an
        /// aptly populated IDataReader object.
        /// </summary>
        /// <param name="dr">The IDataReader object that contains, at minimum, the following
        /// columns: ForumID, DateCreated, Description, Name, Moderated, and DaysToView.</param>
        /// <returns>An instance of the Forum class that represents the current row of the passed 
        /// in SqlDataReader, dr.</returns>
        public static void PopulateSectionFromIDataReader(IDataReader dr, Section s) 
        {           
            s.SectionID =                 (int) dr["SectionID"];
            s.ParentID =                (int) dr["ParentID"];
			s.SettingsID =                  (int) dr["SettingsID"];
            s.GroupID =            (int) dr["GroupID"];
            s.DateCreated =             (DateTime) dr["DateCreated"];
            s.Description =             (string) dr["Description"];
            s.Name =                    (string) dr["Name"];
            s.NewsgroupName =           (string) dr["NewsgroupName"];
            s.IsModerated =             Convert.ToBoolean(dr["IsModerated"]);
            s.DefaultThreadDateFilter = (ThreadDateFilterMode) dr["DaysToView"];
            s.IsActive =                Convert.ToBoolean(dr["IsActive"]);
            s.IsSearchable =		    Convert.ToBoolean(dr["IsSearchable"]);
            s.SortOrder =               (int) dr["SortOrder"];
            s.DisplayMask =             (byte[]) dr["DisplayMask"];
            s.TotalPosts =              (int) dr["TotalPosts"];
            s.TotalThreads =            (int) dr["TotalThreads"];
            s.GroupID =            (int) dr["GroupID"];
            s.MostRecentPostAuthor =     dr["MostRecentPostAuthor"] as string;
            s.MostRecentPostSubject =    dr["MostRecentPostSubject"] as string;
            s.MostRecentPostAuthorID =  (int) dr["MostRecentPostAuthorID"];
            s.MostRecentPostID =        (int) dr["MostRecentPostId"];
            s.MostRecentThreadID =      (int) dr["MostRecentThreadId"];
            s.MostRecentThreadReplies = (int) dr["MostRecentThreadReplies"];
            s.MostRecentPostDate =      (DateTime) dr["MostRecentPostDate"];
            s.EnableAutoDelete =        Convert.ToBoolean(dr["EnableAutoDelete"]);
            s.EnablePostStatistics =    Convert.ToBoolean(dr["EnablePostStatistics"]);
            s.AutoDeleteThreshold =     (int) dr["AutoDeleteThreshold"];
            s.EnableAnonymousPosting =  Convert.ToBoolean(dr["EnableAnonymousPosting"]);
			s.ForumType =				(ForumType) dr["ForumType"];
			s.NavigateUrl =				(string) dr["Url"];

			s.ApplicationKey = dr["ApplicationKey"] as string;
			//s.DeserializeExtendedAttributes(dr["StringNameValues"] as byte[]);

            s.SetSerializerData(PopulateSerializerDataIDataReader(dr, SerializationType.Section));

			s.ApplicationType = (ApplicationType) (Int16)dr["ApplicationType"];

            try {
                s.PostsToModerate =         (int) dr["PostsToModerate"];
            } catch {}

            //return forum;
        }


		public static MembershipUser PopulateMembershipUserFromIDataReader(IDataReader dr, string userName)
		{
			string _name = userName == null ? dr["UserName"] as string : userName;

			string email = dr["Email"] as string;
			string passwordQuestion = dr["PasswordQuestion"] as string;
			string comment = dr["Comment"] as string;

			bool isApproved = Convert.ToBoolean(dr["IsApproved"]);
            bool isLockedOut = Convert.ToBoolean(dr["IsLockedOut"]);
			DateTime dtCreate = Convert.ToDateTime(dr["CreateDate"]);
			DateTime dtLastLogin = Convert.ToDateTime(dr["LastLoginDate"]);
			DateTime dtLastActivity = Convert.ToDateTime(dr["LastActivityDate"]);
			DateTime dtLastPassChange = Convert.ToDateTime(dr["LastPasswordChangedDate"]);
            DateTime dtLastLockoutDate = Convert.ToDateTime(dr["LastLockoutDate"]);
			Guid userId = (Guid)dr["UserId"];

			return new MembershipUser(Membership.Provider,_name,userId,email,passwordQuestion,comment,isApproved,isLockedOut,dtCreate,dtLastLogin,dtLastActivity,dtLastPassChange,dtLastLockoutDate);

		}

		//we can rename this once we make all of our changes.
		public static User cs_PopulateUserFromIDataReader(IDataReader dr)
		{
            MembershipUser mu = PopulateMembershipUserFromIDataReader(dr,null);
            return PopulateUserFromIDataReader(dr,mu,false);
		}

        public static User cs_PopulateUserFromIDataReader(IDataReader dr, bool isEditable)
		{
            MembershipUser mu = PopulateMembershipUserFromIDataReader(dr,null);
            return PopulateUserFromIDataReader(dr,mu,isEditable);
        }

		public static User PopulateUserFromIDataReader(IDataReader dr, MembershipUser member, bool isEditable) {
            Profile profile = null;
            if (isEditable) {
                
            	ProfileBase pb = ProfileBase.Create(member.UserName,true);
                profile = new Profile(pb);
            }
            else {
                ProfileData pd = PopulateProfileDataFromIReader(dr);
                profile = new Profile(pd);
            }

			// Read in the result set
			User user = new User(member,profile);
			
			user.UserID                         = (int) dr["cs_UserID"];
			
			//Need to remove/remap this value
			user.LastActivity                   = member.LastActivityDate;
			user.AccountStatus                  = (UserAccountStatus) int.Parse( dr["cs_UserAccountStatus"].ToString() );
			user.IsAnonymous                    = Convert.ToBoolean(dr["IsAnonymous"]);
			user.LastAction                     = dr["cs_LastAction"] as string;
			user.AppUserToken                   = dr["cs_AppUserToken"] as string;
			user.ForceLogin                     = Convert.ToBoolean(dr["cs_ForceLogin"]);
			// 2005-02-27: 对用户新增属性的转化
			user.Nickname						= (string) dr["Nickname"];
			user.IPCreated 						= (string) dr["IPCreated"];
			user.IPLastActivity 				= (string) dr["IPLastActivity"];
			if (dr["Birthday"] != DBNull.Value)
				user.Birthday 					= (DateTime) dr["Birthday"];
			user.DatabaseQuota  				= (int) dr["DatabaseQuota"];
			user.DatabaseQuotaUsed  			= (int) dr["DatabaseQuotaUsed"];
            
            SerializerData data = CommonDataProvider.PopulateSerializerDataIDataReader(dr, SerializationType.User);
            user.SetSerializerData(data);

			user.IsAvatarApproved               = Convert.ToBoolean(dr["IsAvatarApproved"]);
			user.ModerationLevel                = (ModerationLevel) int.Parse( dr["ModerationLevel"].ToString());
			user.EnableThreadTracking           = Convert.ToBoolean(dr["EnableThreadTracking"]);
			user.TotalPosts                     = (int) dr["TotalPosts"];
			user.EnableAvatar                   = Convert.ToBoolean(dr["EnableAvatar"]);
			user.PostSortOrder                  = (SortOrder) dr["PostSortOrder"];
			user.PostRank                       = (byte[]) dr["PostRank"];
			user.EnableDisplayInMemberList      = Convert.ToBoolean(dr["EnableDisplayInMemberList"]);
			user.EnableOnlineStatus             = Convert.ToBoolean(dr["EnableOnlineStatus"]);
			user.EnablePrivateMessages          = Convert.ToBoolean(dr["EnablePrivateMessages"]);
			user.EnableHtmlEmail				= Convert.ToBoolean(dr["EnableHtmlEmail"]);
            
            // Moderation counters
            //
            user.AuditCounters                  = CommonDataProvider.PopulateAuditSummaryFromIDataReader( dr );

			return user;
		}

        public static ProfileData PopulateProfileDataFromIReader(IDataReader reader)
        {
            ProfileData pd = new ProfileData();
            pd.PropertyNames = reader["ProfileNames"] as string;
            pd.PropertyValues = reader["PropertyValuesString"] as string;
            pd.PropertyValuesBinary = reader["PropertyValuesBinary"] as byte[];
            return pd;
        }

        public static Avatar PopulateAvatarFromIReader (IDataReader reader) {
            Avatar avatar = new Avatar();

            avatar.ImageID          = (int) reader["ImageID"];
            avatar.UserID           = (int) reader["UserID"];
            avatar.Content          = (byte[]) reader["Content"];
            avatar.ContentType      = (string) reader["ContentType"];
            avatar.Length           = (int) reader["Length"];
			// 新增Filename
			avatar.FileName			= (string) reader["FileName"];
            avatar.DateCreated      = (DateTime) reader["DateLastUpdated"];

            return avatar;
        }

        public static PostAttachment PopulatePostAttachmentFromIReader (IDataReader reader) {
			PostAttachment attachment = new PostAttachment();

			attachment.AttachmentID       = (Guid) reader["AttachmentID"];
			attachment.PostID       = (int) reader["PostID"];
			attachment.Content      = (byte[]) reader["Content"];
			attachment.ContentType  = (string) reader["ContentType"];
			attachment.Length       = (int) reader["ContentSize"];
			attachment.FileName     = (string) reader["FileName"];
			attachment.RealFileName = (string) reader["RealFileName"];
			attachment.UserID       = (int) reader["UserID"];
			attachment.ForumID      = (int) reader["SectionID"];
			attachment.DownloadCount = (int) reader["DownloadCount"];
			attachment.DateCreated  = (DateTime) reader["Created"];

			return attachment;
        }

		public static Role PopulateRoleFromIDataReader (IDataReader reader) {
			Role role = new Role();

			role.RoleID       = (Guid) reader["RoleID"];
			role.Name         = (string) reader["Name"];
			role.Description  = reader["Description"] as string;

			return role;
		}
        
        public static SerializerData PopulateSerializerDataIDataReader(IDataReader dr, SerializationType type)
        {
            SerializerData data = new SerializerData();

			switch(type)
			{
				case SerializationType.Post:
					//data.Bytes = dr["PostStringNameValues"] as byte[];
					data.Keys = dr["PostPropertyNames"] as string;
					data.Values = dr["PostPropertyValues"] as string;
					break;
				case SerializationType.Section:
					//data.Bytes = dr["SectionStringNameValues"] as byte[];
					data.Keys = dr["SectionPropertyNames"] as string;
					data.Values = dr["SectionPropertyValues"] as string;
					break;
				case SerializationType.User:
					//data.Bytes = dr["UserStringNameValues"] as byte[];
					data.Keys = dr["UserPropertyNames"] as string;
					data.Values = dr["UserPropertyValues"] as string;
					break;
				default:
					//data.Bytes = dr["StringNameValues"] as byte[];
					data.Keys = dr["PropertyNames"] as string;
					data.Values = dr["PropertyValues"] as string;
					break;
			}

            return data;
        }

    }

}
