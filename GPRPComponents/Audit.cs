//------------------------------------------------------------------------------
// <copyright company="Telligent Systems">
//     Copyright (c) Telligent Systems Corporation.  All rights reserved.
// </copyright> 
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Web;

using GPRP.GPRPEnumerations;
namespace GPRP.GPRPComponents {

	/// <summary>
	/// Summary description for Audit.
	/// </summary>
	public class Audit {
        
        #region Audit Summary

        #region GetUserAuditCounters
        public static int[] GetUserAuditCounters (AuditSummary summary) {
            if (summary == null)
                return null;

            int[] counters = new int[4];
            int maxForumCounterValue = 0;
            int maxMessageCounterValue = 0;
            
            counters[0] = 0; // Message Action counter
            counters[1] = -1; // Message Action selected counter with the most entries
            counters[2] = 0; // Forum Action counter
            counters[3] = -1; // Forum Action selected counter with the most entries

            IDictionaryEnumerator iterator = summary.Collection.GetEnumerator();

            while (iterator.MoveNext()) {

                switch (((ModeratorActions) iterator.Key)) {                        
                    case ModeratorActions.ApprovePost:
                    case ModeratorActions.EditPost:
                    case ModeratorActions.MovePost:
                    case ModeratorActions.DeletePost:
                    case ModeratorActions.LockPost:                            
                    case ModeratorActions.UnlockPost:                            
                    case ModeratorActions.MergePost:                            
                    case ModeratorActions.SplitPost:
                        if (iterator.Value != null) {
                            counters[0] += (int) iterator.Value;

                            if (maxMessageCounterValue < (int) iterator.Value) {
                                maxMessageCounterValue = (int) iterator.Value;
                                counters[1] = (int) iterator.Key;
                            }
                        }
                        break;

                    case ModeratorActions.EditUser:
                    case ModeratorActions.BanUser:
                    case ModeratorActions.UnbanUser:
                    case ModeratorActions.ModerateUser:
                    case ModeratorActions.UnmoderateUser:
                    case ModeratorActions.ResetPassword:
                    case ModeratorActions.ChangePassword:                            
                        if (iterator.Value != null) {
                            counters[2] += (int) iterator.Value;
                            
                            if (maxForumCounterValue < (int) iterator.Value) {
                                maxForumCounterValue = (int) iterator.Value;
                                counters[3] = (int) iterator.Key;
                            }
                        }
                        break;
                }
            }
            
            return counters;
        }
        #endregion

        #region GetPostAuditCounter
        public static int GetPostAuditCounter (int postID) { 
            AuditSummary actions = GetPostAuditSummary( postID );
            if (actions != null)
                return actions.Total;

            return 0;
        }
        #endregion

        #region GetUserMessageActionsCounter
        /// <summary>
        /// This method counts the actions related to user messages: 
        /// Posts Approved, Posts Edited, Posts Moved, Posts Deleted.
        /// Also returns the moderation action that has maximum of audit records.
        /// </summary>
        public static int GetUserMessageActionsCounter (int userID, ref int selectedCounter) {
            int counter = 0;
            AuditSummary actions = GetUserAuditSummary( userID );
            int maxCounterValue = 0;
            selectedCounter = -1;

            if (actions != null) { 
                IDictionaryEnumerator iterator = actions.Collection.GetEnumerator();

                while (iterator.MoveNext()) {

                    switch (((ModeratorActions) iterator.Key)) {                        
                        case ModeratorActions.ApprovePost:
                        case ModeratorActions.EditPost:
                        case ModeratorActions.MovePost:
                        case ModeratorActions.DeletePost:
                        case ModeratorActions.LockPost:                            
                        case ModeratorActions.UnlockPost:                            
                        case ModeratorActions.MergePost:                            
                        case ModeratorActions.SplitPost:
                            if (iterator.Value != null) {
                                counter += (int) iterator.Value;

                                if (maxCounterValue < (int) iterator.Value) {
                                    maxCounterValue = (int) iterator.Value;
                                    selectedCounter = (int) iterator.Key;
                                }
                            }
                            break;
                    }
                }
            }

            return counter;
        }
        #endregion

        #region GetUserForumActionsCounter
        /// <summary>
        /// This method counts the actions against the user - Edit, Ban/Unban, 
        /// Moderate/Unmoderate, Reset Password, Change Password.
        /// Also returns the moderation action that has maximum of audit records.
        /// </summary>
        public static int GetUserForumActionsCounter (int userID, ref int selectedCounter) {
            int counter = 0;
            AuditSummary actions = GetUserAuditSummary( userID );
            int maxCounterValue = 0;
            selectedCounter = -1;

            if (actions != null) { 
                IDictionaryEnumerator iterator = actions.Collection.GetEnumerator();

                while (iterator.MoveNext()) {

                    switch (((ModeratorActions) iterator.Key)) {
                        case ModeratorActions.EditUser:
                        case ModeratorActions.BanUser:
                        case ModeratorActions.UnbanUser:
                        case ModeratorActions.ModerateUser:
                        case ModeratorActions.UnmoderateUser:
                        case ModeratorActions.ResetPassword:
                        case ModeratorActions.ChangePassword:                            
                            if (iterator.Value != null) {
                                counter += (int) iterator.Value;
                                
                                if (maxCounterValue < (int) iterator.Value) {
                                    maxCounterValue = (int) iterator.Value;
                                    selectedCounter = (int) iterator.Key;
                                }
                            }
                            break;
                    }
                }
            }

            return counter;
        }
        #endregion

        #region GetUserAuditSummary
        public static AuditSummary GetUserAuditSummary (int userID) {
            return GetUserAuditSummary( userID, true, false );
        }

        /// <summary>
        /// Returns overall moderation audit summary for provided userID.
        /// This has a counter for each moderator action.
        /// </summary>
        public static AuditSummary GetUserAuditSummary (int userID, bool cacheable, bool flush) {
            string cacheKey = string.Format( "UserAuditSummary-{0}-{1}", userID, CSContext.Current.SiteSettings.SettingsID ); 
            HttpContext context = HttpContext.Current;
            AuditSummary summary;

            if (flush) {
                CSCache.Remove(cacheKey);
                context.Items[cacheKey] = null;
            }

            // Get the summary from context
            //
            summary = context.Items[cacheKey] as AuditSummary;
            if(summary != null)
                return summary;

            // Get the summary from cache
            //
            summary = CSCache.Get(cacheKey) as AuditSummary;
            if (summary == null) {
                summary = CommonDataProvider.Instance().GetAuditSummary( -1, userID );

                if (cacheable)
                    CSCache.Insert(cacheKey, summary, 10 * CSCache.MinuteFactor );
            } 

            context.Items[cacheKey] = summary;

            return summary;
        }
        #endregion
        
        #region GetPostAuditSummary
        public static AuditSummary GetPostAuditSummary (int postID) {
            return GetPostAuditSummary( postID, true, false );
        }

        /// <summary>
        /// Returns overall moderation audit summary for provided postID
        /// </summary>
        public static AuditSummary GetPostAuditSummary (int postID, bool cacheable, bool flush) {
            string cacheKey = string.Format( "PostAuditSummary-{0}-{1}", postID, CSContext.Current.SiteSettings.SettingsID ); 
            HttpContext context = HttpContext.Current;
            AuditSummary summary;

            if (flush) {
                CSCache.Remove(cacheKey);
                context.Items[cacheKey] = null;
            }

            // Get the summary from context
            //
            summary = context.Items[cacheKey] as AuditSummary;
            if(summary != null)
                return summary;

            // Get the summary from cache
            //
            summary = CSCache.Get(cacheKey) as AuditSummary;
            if (summary == null) {
                summary = CommonDataProvider.Instance().GetAuditSummary( postID, -1 );

                if (cacheable)
                    CSCache.Insert(cacheKey, summary, 10 * CSCache.MinuteFactor );
            } 

            context.Items[cacheKey] = summary;

            return summary;
        }
        #endregion
        
        #endregion
        
        #region Post Audit

        #region GetAuditHistoryForPost
        public static AuditSet GetAuditHistoryForPost (int postID, int pageIndex, int pageSize) { 
            return GetAuditHistoryForPost( postID, pageIndex, pageSize, true, true, false );
        }

        public static AuditSet GetAuditHistoryForPost (int postID, int pageIndex, int pageSize, bool returnRecordCount) { 
            return GetAuditHistoryForPost( postID, pageIndex, pageSize, returnRecordCount, true, false );
        }

        /// <summary>
        /// Returns moderation audit entries for provided post ID.
        /// Records are returned in a controled amount through records pagination.
        /// </summary>
        /// <param name="postID">A valid post ID as an integer.</param>
        /// <param name="pageIndex">Number of the current page.</param>
        /// <param name="pageSize">Number of records contained by the current page.</param>
        /// <param name="returnRecordCount">Returns also total number of entries if true.</param>
        /// <param name="cacheable">Store in cache also if true.</param>
        /// <param name="flush">Reset previous cached version if true.</param>
        /// <returns>Collection of audit entries incapsulated into ModerationAuditSet.</returns>
        public static AuditSet GetAuditHistoryForPost (int postID, int pageIndex, int pageSize, bool returnRecordCount, bool cacheable, bool flush) { 
            string cacheKey = string.Format( "AuditHistoryForPost-{0}-{1}-{2}-{3}", postID, CSContext.Current.SiteSettings.SettingsID, pageIndex, pageSize ); 
            HttpContext context = HttpContext.Current;
            AuditSet collection;

            if (flush) {
                CSCache.Remove(cacheKey);
                context.Items[cacheKey] = null;
            }

            // Get the summary from context
            //
            collection = context.Items[cacheKey] as AuditSet;
            if(collection != null)
                return collection;

            // Get the summary from cache
            //
            collection = CSCache.Get(cacheKey) as AuditSet;
            if (collection == null) {
                collection = CommonDataProvider.Instance().GetAuditHistoryForPost( postID, pageIndex, pageSize, returnRecordCount );

// Does not need to be cached. Revisit API in 1.2 - SW
//                if (cacheable)
//                    CSCache.Insert(cacheKey, collection, 10 * CSCache.MinuteFactor );
            } 

            context.Items[cacheKey] = collection;

            return collection;
        }
        #endregion

        #endregion

        #region User Audit

        #region GetAuditOnUser
        public static AuditSet GetAuditHistoryForUser (int userID, ModeratorActions action, int pageIndex, int pageSize) {
            return GetAuditHistoryForUser( userID, action, pageIndex, pageSize, true, true, false );
        }

        public static AuditSet GetAuditHistoryForUser (int userID, ModeratorActions action, int pageIndex, int pageSize, bool returnRecordCount) {
            return GetAuditHistoryForUser( userID, action, pageIndex, pageSize, returnRecordCount, true, false );
        }

        /// <summary>
        /// Returns moderation audit entries for provided user ID and moderator action.
        /// Records are returned in a controled amount through records pagination.
        /// </summary>
        /// <param name="userID">A valid user ID as an integer.</param>
        /// <param name="action">Filter audit trail by moderation action.</param>
        /// <param name="pageIndex">Number of the current page.</param>
        /// <param name="pageSize">Number of records contained by the current page.</param>
        /// <param name="returnRecordCount">Returns also total number of entries if true.</param>
        /// <param name="cacheable">Store in cache also if true.</param>
        /// <param name="flush">Reset previous cached version if true.</param>
        /// <returns>Collection of audit entries incapsulated into ModerationAuditSet.</returns>
        public static AuditSet GetAuditHistoryForUser (int userID, ModeratorActions action, int pageIndex, int pageSize, bool returnRecordCount, bool cacheable, bool flush) { 
            string cacheKey = string.Format( "AuditHistoryForUser-{0}-{1}-{2}-{3}-{4}", userID, action.ToString(), CSContext.Current.SiteSettings.SettingsID, pageIndex, pageSize ); 
            HttpContext context = HttpContext.Current;
            AuditSet collection;

            if (flush) {
                CSCache.Remove(cacheKey);
                context.Items[cacheKey] = null;
            }

            // Get the summary from context
            //
            collection = context.Items[cacheKey] as AuditSet;
            if(collection != null)
                return collection;

            // Get the summary from cache
            //
            collection = CSCache.Get(cacheKey) as AuditSet;
            if (collection == null) {
                collection = CommonDataProvider.Instance().GetAuditHistoryForUser( userID, action, pageIndex, pageSize, returnRecordCount );

// Does not need to be cached. Revisit API in 1.2 - SW
//                if (cacheable)
//                    CSCache.Insert(cacheKey, collection, 10 * CSCache.MinuteFactor );
            } 

            context.Items[cacheKey] = collection;

            return collection;
        }
        #endregion       

        #endregion

        #region Save User Audit Event
        public static void SaveUserAuditEvent (ModerateUserSetting auditFlag, User user, int moderatorID) { 
            if (user == null)
                return;

            // Create a new audit entry
            //
            CommonDataProvider dp = CommonDataProvider.Instance();
            dp.SaveUserAuditEvent( auditFlag, user, moderatorID );
        }
        #endregion

        #region ModeratorAction to UserAction
        public static UserModerationAction ModeratorActionToUserAction (ModeratorActions action) {
            UserModerationAction uAction = UserModerationAction.ForumAction;

            switch (action) { 
                case ModeratorActions.ApprovePost:
                case ModeratorActions.EditPost:
                case ModeratorActions.MovePost:
                case ModeratorActions.DeletePost:
                case ModeratorActions.LockPost:
                case ModeratorActions.UnlockPost:
                case ModeratorActions.MergePost:
                case ModeratorActions.SplitPost:
                case ModeratorActions.PostIsAnnouncement:
                case ModeratorActions.PostIsNotAnnoucement:
                case ModeratorActions.UnApprovePost:
                    uAction = UserModerationAction.MessageAction;
                break;

                case ModeratorActions.EditUser:
                case ModeratorActions.UnmoderateUser:
                case ModeratorActions.ModerateUser:
                case ModeratorActions.BanUser:
                case ModeratorActions.UnbanUser:
                case ModeratorActions.ResetPassword:
                case ModeratorActions.ChangePassword:
                    uAction = UserModerationAction.ForumAction;
                    break;
            }

            return uAction;
        }
        #endregion
    }
}
