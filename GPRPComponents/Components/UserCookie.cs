//------------------------------------------------------------------------------
// <copyright company="Telligent Systems">
//     Copyright (c) Telligent Systems Corporation.  All rights reserved.
// </copyright> 
//------------------------------------------------------------------------------

using System;
using System.Web;
using GPRP.GPRPEnumerations;

namespace GPRP.GPRPComponents {
    /// <summary>
    /// Summary description for UserCookie.
    /// </summary>
    public class UserCookie {

        #region Member variables and constructor
        protected HttpCookie userCookie;
        string cookieName;//= "CommunityServer-UserCookie";
        CSContext csContext = CSContext.Current;
        HttpContext context = null;
        User user;
        string cookieKey;

        public UserCookie( User user ) {
            this.user = user;

            //Allows different users on the same machine to hide unique sets of forums
            cookieName = "CommunityServer-UserCookie" + user.UserID.ToString();
            context = csContext.Context;
            userCookie = context.Request.Cookies[cookieName];
            
            //use a global key
            cookieKey = string.Format("hfg-{0}",CSContext.Current.ApplicationType);
            

            // Did we find a cookie?
            //
            if (userCookie == null) {
                userCookie = new HttpCookie(cookieName);
            } else {
                userCookie = context.Request.Cookies[cookieName];
            }



        }
        #endregion

        #region Private helper functions
        protected void WriteCookie() {
            userCookie.Expires = DateTime.Now.AddYears(1);
            context.Response.Cookies.Add(userCookie);
        }
        #endregion

        #region Last Visit
        public DateTime LastVisit {
            get {
                if (userCookie["lvd"] == null) {
                    userCookie["lvd"] = DateTime.Parse("1/1/1999").ToString();
                } else if (DateTime.Parse(userCookie["sd"]) < DateTime.Now.AddMinutes(20)) {
                    userCookie["lvd"] = userCookie["sd"];
                }

                userCookie["sd"] = DateTime.Now.ToString();

                // Write the updated cookie
                WriteCookie();

                return DateTime.Parse(userCookie["lvd"]);
            }
        }
        #endregion

        #region Forum Filter/View Settings
        public void SaveForumOptions (int forumID, string settings) {
            userCookie["fo" + forumID] = settings;

            // Write the updated cookie
            WriteCookie();
        }

        public ForumUserOptions GetForumOptions (int forumID) {
            return new ForumUserOptions(userCookie["fo" + forumID]);
        }
        #endregion

        #region Hidden forum groups
        public string[] HiddenGroups {

            get {
                // Do we have values?
                //
                
                if (userCookie.Values[cookieKey] != null)
                    return userCookie.Values[cookieKey].Split(',');

                return new string[0];
            }
        }

        public void RemoveHiddenForumGroup(int forumGroupID) {

            string hfg = "," + userCookie.Values[cookieKey] + ",";

            // Add some details to the string to do a simple replace
            //
            hfg = hfg.Replace("," + forumGroupID + ",", ",");

            // Trim the string
            //
            hfg = hfg.TrimStart(',').TrimEnd(',');

            userCookie.Values[cookieKey] = hfg;

            WriteCookie();
        }

        public void AddHiddenForumGroup(int forumGroupID) {
            string[] s = HiddenGroups;
            string hfg = string.Empty;

            // Do we already have this forum group
            //
            for (int i = 0; i < s.Length; i++)
                if (s[i] == forumGroupID.ToString())
                    return;

            string[] s1 = new String[s.Length + 1];
            s.CopyTo(s1, 0);
            s1[s1.Length - 1] = forumGroupID.ToString();
                
            for (int i=0; i<s1.Length; i++)
                hfg = hfg + s1[i] + ",";

            // Trim off the extra comma
            //
            hfg = hfg.TrimStart(',').TrimEnd(',');

            userCookie.Values[cookieKey] = hfg;

            WriteCookie();
        }
        #endregion

    }

    public class ForumUserOptions {
        public SortThreadsBy SortBy = SortThreadsBy.LastPost;
        public SortOrder SortOrder = SortOrder.Descending;
        public ThreadUsersFilter UserFilter = ThreadUsersFilter.All;
        public ThreadDateFilterMode DateFilter = ThreadDateFilterMode.All;
        public bool HideReadPosts = false;
        public bool HasSettings = false;

        public ForumUserOptions(string settings) {

            if ((settings == null) || (settings == string.Empty))
                return;

            HasSettings = true;

            try {
                // Serialized format is: [SortBy]:[SortOrder]:[DateFilter]:[HideReadPosts]:[UserFilter]
                string[] s = settings.Split(':');

                SortBy = (SortThreadsBy) int.Parse(s[0]);
                SortOrder = (SortOrder) int.Parse(s[1]);
                DateFilter = (ThreadDateFilterMode) int.Parse(s[2]);

                if (s[3] == "T")
                    HideReadPosts = true;
                else
                    HideReadPosts = false;

                UserFilter = (ThreadUsersFilter) int.Parse(s[4]);
            } catch {}

        }

    }

}
