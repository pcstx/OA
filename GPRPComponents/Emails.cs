//------------------------------------------------------------------------------
// <copyright company="Telligent Systems">
//     Copyright (c) Telligent Systems Corporation.  All rights reserved.
// </copyright> 
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web.Caching;
using System.Web.Mail;
using System.Xml;
using GPRP.GPRPEnumerations;

namespace GPRP.GPRPComponents {

	/// <summary>
	/// Used to iterate over a of possible email subscribers and optionally send them a message
	/// </summary>
	public delegate MailMessage EnquePostEmails(Post p, User subscriber, FormatList formats);

    // *********************************************************************
    //  Emails
    //
    /// <summary>
    /// This class is responsible for sending out emails to users when certain events occur.  For example,
    /// when a user requests to be emailed their password, a method of this class is called to send the
    /// correct email template populated with the correct data to the correct user.
    /// </summary>
    /// <remarks>There are a number of email templates.  These templates can be viewed/edited via the Email
    /// Administration Web page.  The EmailType enumeration contains a member for each of the potential
    /// email templates.</remarks>
    /// 
    // ********************************************************************/
    public class Emails {

        #region Contact Request

        public static void QueueContactRequest(string to, string from, string subject, string body, string appName, string link)
        {
            FormatList fl = new FormatList();
            fl.Add("AppTitle", appName);
            fl.Add("Body", body);
            fl.Add("link", link);
            fl.Add("useremail", from);
            fl.Add("Subject", subject);

            MailMessage email = GenericEmail("ContactForm",null,null,null,false,false);
            email.To = to;
            email.From = from;
            email.Body = fl.Format(email.Body);
            email.Subject = fl.Format(email.Subject);

            CommonDataProvider dp = CommonDataProvider.Instance();
            dp.EmailEnqueue(email);

            
        }

        #endregion

		#region SectionTracking
		/// <summary>
		/// Based on the postID, this method will retrieve subscribers to the post and section.
		/// It will then create a unique list between them and call the method supplied vai the 
		/// EnquePostEmails delegate
		/// </summary>
		/// <param name="post">Post to send</param>
		/// <param name="epm">Method used to Enque the email</param>
		protected static void SectionTracking(Post post, EnquePostEmails epm, FormatList formats)
		{
			if (post == null)
				return;

			Hashtable threadSubscribers = GetEmailsTrackingThread(post.PostID);
			Hashtable sectionSubscribers = GetEmailsTrackingSectionByPostID(post.PostID);



			foreach(string email in threadSubscribers.Keys)
			{
				User sectionUser = sectionSubscribers[email] as User;
				if(sectionUser != null)
					sectionSubscribers.Remove(email);
			}

			foreach(string email in sectionSubscribers.Keys)
			{
				threadSubscribers.Add(email,sectionSubscribers[email] as User);
			}

			sectionSubscribers.Clear();

			foreach (string email in threadSubscribers.Keys) 
			{
				User subscriber = threadSubscribers[email] as User;
				// Make sure we don't send an email to the user that posted the message
				//			
				if (subscriber.Email != post.User.Email) 
				{
					EnqueuEmail(epm(post,subscriber,formats));
				}
			}

		}
		#endregion

		#region EmailsInQueue
		// *********************************************************************
		//  EmailsInQueue
		//
		/// <summary>
		/// This method returns an ArrayList of all emails in the
		/// database, ready to send.
		/// </summary>
		/// 
		// ********************************************************************/
		public static ArrayList EmailsInQueue (int settingsID) 
		{
		    CommonDataProvider dp = CommonDataProvider.Instance();

			ArrayList emails = dp.EmailDequeue(settingsID);
			
			return emails;
		}
		#endregion

        #region Enque email
        protected static void EnqueuEmail (MailMessage email) {

			// don't enqueue the email if the user has a blank
			// email address.
			//
			if (email != null &&
				email.To != null &&
				email.To.Trim().Length > 0) {

			    CommonDataProvider dp = CommonDataProvider.Instance();

            dp.EmailEnqueue(email);
			}

        }
        #endregion

        #region SendQueuedEmails
        public static void SendQueuedEmails(int failureInterval, int maxNumberOfTries) {

            CommonDataProvider dp = CommonDataProvider.Instance();
        	CSConfiguration csConfig = CSConfiguration.GetConfig();


			// test to see if this server is disabled for sending email
			//
			if (csConfig.IsEmailDisabled)
				return;

            Hashtable sites = SiteSettingsManager.GetActiveSiteSettings();
            if(sites == null || sites.Count == 0)
                return;

			// Copy the keys to another array to avoid getting a collection modified error
			string[] keys = new string[sites.Count];
			sites.Keys.CopyTo(keys, 0);

            foreach(string key in keys)
            {
                SiteSettings site = sites[key] as SiteSettings;
                if(site == null || !site.EnableEmail)
                    continue;

                ArrayList emails = dp.EmailDequeue(site.SettingsID);
				ArrayList failure = new ArrayList();

                SmtpMail.SmtpServer = site.SmtpServer;
                string username = site.SmtpServerUserName;
                string password = site.SmtpServerPassword;

                int sentCount	= 0;
				int totalSent	= 0;
                short connectionLimit = csConfig.SmtpServerConnectionLimit;
                foreach (EmailTemplate m in emails) 
                {
					try 
                    {
                        //for SMTP Authentication
                        if (site.SmtpServerRequiredLogin) 
                        {
                            m.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpauthenticate", "1" ); //basic authentication
                            m.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendusername", username ); //set your username here
                            m.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendpassword", password ); //set your password here
                        }

                        // For Html emails, this resolves the complete paths for images and links.
                        // Should not effect non-Html emails via RFC-2110.
                        //
                        // EAD 1/14/2005: Commented out until we can access the forumContext from a background thread.
                        //m.Fields.Add("Content-Base", Globals.FullPath(Globals.GetSiteUrls().Home) );
                        //m.Fields.Add("Content-Location", Globals.FullPath(Globals.GetSiteUrls().Home) );
						m.Headers.Add("X-CS-EmailID", m.EmailID.ToString());
                        m.Headers.Add("X-CS-ThreadId", AppDomain.GetCurrentThreadId().ToString());
                        m.Headers.Add("X-CS-Attempts", (m.NumberOfTries + 1).ToString());
                        m.Headers.Add("X-CS-AppDomain", AppDomain.CurrentDomain.FriendlyName);
                        SmtpMail.Send(m);

						// (Ken) Moved the delete command to here
						//       If a number of emails were sent, the command to delete them could timeout and duplicates would be sent on the next run
						dp.EmailDelete(m.EmailID);

                        if(		connectionLimit != -1 &&	++sentCount >= connectionLimit ) 
                        {
                        	Thread.Sleep( new TimeSpan( 0, 0, 0, 15, 0 ) );
                            sentCount = 0;
                        }
						// on error, loop so to continue sending other email.
					} 
                    catch( Exception e ) 
                    {
                    	Debug.WriteLine( e.Message + " : " + ( e.InnerException != null ? e.InnerException.Message : String.Empty ) );
                        CSException fe = new CSException( CSExceptionType.EmailUnableToSend, "SendQueuedEmails Failed To: " + m.To, ( e.InnerException != null ? e.InnerException : e ) );
                        fe.Log(site.SettingsID);

						// Add it to the failure list
						failure.Add(m.EmailID);
                    }

					if( site.EmailThrottle > 0 && ++totalSent >= site.EmailThrottle ) {
						break;
					}
				}

				if(failure.Count > 0)
					dp.EmailFailure(failure, failureInterval, maxNumberOfTries);


            }

        }
        #endregion

        #region User Management Emails

        public static MailMessage User (string emailType, User user, string password) {
            MailMessage email;

			// Do we have an email address?
			//
			if(user.Email == null)
				return null;

            // Do we have a password?
            //
            if (password != null)
                user.Password = password;

            // Get the email template we're going to use
            //
            email = GenericEmail(emailType, user, null, null, true, user.EnableHtmlEmail);
            email.From = GenericEmailFormatter(email.From, user, null);
            email.Subject= GenericEmailFormatter(email.Subject, user, null);
            email.Body = GenericEmailFormatter(email.Body, user, null);

            return email;
        }

        public static void UserPasswordForgotten (User user, string password) {
            if (user == null)
                return;

            EnqueuEmail(  User(EmailType.ForgottenPassword, user, password) );
        }

        public static void UserPasswordChanged (User user, string password) {
            if (user == null)
                return;

            EnqueuEmail(  User(EmailType.ChangedPassword, user, password) );
        }

        public static void UserCreate (User user, string password) {
            if (user == null)
                return;

            EnqueuEmail( User(EmailType.NewUserAccountCreated, user, password) );
        }

        public static void UserAccountPending (User user) {
            if (user == null)
                return;

            EnqueuEmail( User(EmailType.NewUserAccountPending, user, null) );
        }

        public static void UserAccountRejected (User user, User moderatedBy) {
            if (user == null)
                return;

            EnqueuEmail( User(EmailType.NewUserAccountRejected, user, null) );
        }

        public static void UserAccountApproved (User user) {
            if (user == null)
                return;

            EnqueuEmail( User(EmailType.NewUserAccountApproved, user, null) );
        }

        #endregion

		#region Private Message Notification
		public static void PrivateMessageNotifications(Post post, ArrayList users) {

			foreach (User user in users) {

				if (post.User.UserID != user.UserID)
					EnqueuEmail( CreatePost(post, EmailType.PrivateMessageNotification, user, null, null, false));
			}


		}
		#endregion

		#region User to User emails
		public static void UsersInRole(Guid roleID, Post post) {
			// Get Role
			Role role = Roles.GetRole(roleID);
			
			UserSet countSet;
			UserSet emailSet;

			// special case for Forums-Everyone (roleID == 0)
			if (roleID == Guid.Empty) {
				// find total users
				countSet = Users.GetUsers(0,1,true,false);

				// get all users
				emailSet = Users.GetUsers(0,countSet.TotalRecords,true,false);

			} else {
				// find total users in role
				countSet = Roles.UsersInRole(0,1,SortUsersBy.Username,SortOrder.Ascending,roleID);

				// get all users in role
				emailSet = Roles.UsersInRole(0,countSet.TotalRecords,SortUsersBy.Username,SortOrder.Ascending,roleID);	
			}

			foreach (User user in emailSet.Users) {
				MailMessage email;

				email = GenericEmail(EmailType.RoleEmail, user, null, null, true, user.EnableHtmlEmail);
				email.From = GenericEmailFormatter(email.From, user, post);
				email.Subject = GenericEmailFormatter(email.Subject, user, post);
				email.Body = GenericEmailFormatter(email.Body, user, post, user.EnableHtmlEmail, false).Replace("[RoleName]",role.Name);

				Emails.EnqueuEmail(email);
			}

		}
		public static void UserToUser(User fromUser, User toUser, Post post) {
		
			MailMessage email;

			email = GenericEmail(EmailType.SendEmail, toUser, null, null, true, toUser.EnableHtmlEmail);
			email.From = GenericEmailFormatter(email.From, fromUser, post);
			email.Subject = GenericEmailFormatter(email.Subject, toUser, post);
			email.Body = GenericEmailFormatter(email.Body, toUser, post);

			Emails.EnqueuEmail(email);

		}
		#endregion

        #region Email formatter

        // *********************************************************************
        //  FormatEmail
        //
        /// <summary>
        /// This method formats a given string doing search/replace for markup
        /// </summary>
        /// <param name="messageToFormat">Message to apply formatting to</param>
        /// <param name="user">User the message is being sent to</param>
        /// <param name="timezoneOffset">User's timezone offset</param>
        /// <param name="dbTimezoneOffset">Database's timezone offset</param>
        /// <param name="postID">ID of the post the message is about</param>
        /// <param name="html">If false HTML will stripped out of messages</param>
        /// 
        // ********************************************************************/


		protected static string GenericEmailFormatter (string stringToFormat, User user, Post post) {
			return GenericEmailFormatter (stringToFormat, user, post, false, false);
		}

		protected static string GenericEmailFormatter (string stringToFormat, User user, Post post, bool html) {
			return GenericEmailFormatter (stringToFormat, user, post, html, false);
		}

        protected static string GenericEmailFormatter (string stringToFormat, User user, Post post, bool html, bool truncateMessage ) {

//            string timeSend = string.Format(ResourceManager.GetString("Utility_CurrentTime_formatGMT"), DateTime.Now.ToString(ResourceManager.GetString("Utility_CurrentTime_dateFormat")));
			DateTime time = DateTime.Now;

            // set the timesent and sitename
            stringToFormat = Regex.Replace(stringToFormat, "\\[timesent\\]", time.ToString(ResourceManager.GetString("Utility_CurrentTime_dateFormat")) + " " + string.Format( ResourceManager.GetString("Utility_CurrentTime_formatGMT"), CSContext.Current.SiteSettings.TimezoneOffset.ToString() ), RegexOptions.IgnoreCase | RegexOptions.Compiled);
            stringToFormat = Regex.Replace(stringToFormat, "\\[moderatorl\\]", Globals.GetSiteUrls().Moderate, RegexOptions.IgnoreCase | RegexOptions.Compiled);
            stringToFormat = Regex.Replace(stringToFormat, "\\[sitename\\]", CSContext.Current.SiteSettings.SiteName.Trim(), RegexOptions.IgnoreCase | RegexOptions.Compiled);
            stringToFormat = Regex.Replace(stringToFormat, "\\[websiteurl\\]", Globals.GetSiteUrls().Home, RegexOptions.IgnoreCase | RegexOptions.Compiled);
            stringToFormat = Regex.Replace(stringToFormat, "\\[adminemail\\]", (CSContext.Current.SiteSettings.AdminEmailAddress.Trim() != "" ) ? CSContext.Current.SiteSettings.AdminEmailAddress.Trim() : "notset@localhost.com", RegexOptions.IgnoreCase | RegexOptions.Compiled);

			if(CSContext.Current.Context != null)
			{
				stringToFormat = Regex.Replace(stringToFormat, "\\[loginurl\\]", Globals.HostPath(CSContext.Current.Context.Request.Url) + Globals.GetSiteUrls().LoginReturnHome, RegexOptions.IgnoreCase | RegexOptions.Compiled);
				stringToFormat = Regex.Replace(stringToFormat, "\\[passwordchange\\]", Globals.HostPath(CSContext.Current.Context.Request.Url) + Globals.GetSiteUrls().UserChangePassword, RegexOptions.IgnoreCase | RegexOptions.Compiled);			
			}
			else
			{
				stringToFormat = Regex.Replace(stringToFormat, "\\[loginurl\\]", Globals.HostPath(new Uri(CSContext.Current.RawUrl)) + Globals.GetSiteUrls().LoginReturnHome, RegexOptions.IgnoreCase | RegexOptions.Compiled);
				stringToFormat = Regex.Replace(stringToFormat, "\\[passwordchange\\]", Globals.HostPath(new Uri(CSContext.Current.RawUrl)) + Globals.GetSiteUrls().UserChangePassword, RegexOptions.IgnoreCase | RegexOptions.Compiled);			
			}
			
			// return a generic email address if it isn't set.
			//
			string adminEmailAddress = (CSContext.Current.SiteSettings.AdminEmailAddress.Trim() != "" ) ? CSContext.Current.SiteSettings.AdminEmailAddress.Trim() : "notset@localhost.com";
			string siteName = CSContext.Current.SiteSettings.SiteName.Trim();
            stringToFormat = Regex.Replace(stringToFormat, "\\[adminemailfrom\\]", string.Format( ResourceManager.GetString("AutomatedEmail").Trim(), siteName, adminEmailAddress), RegexOptions.IgnoreCase | RegexOptions.Compiled);

            // Specific to a user
            //
            if (user != null) {
                stringToFormat = Regex.Replace(stringToFormat, "\\[username\\]", user.Username.Trim(), RegexOptions.IgnoreCase | RegexOptions.Compiled);
                stringToFormat = Regex.Replace(stringToFormat, "\\[email\\]", user.Email.Trim(), RegexOptions.IgnoreCase | RegexOptions.Compiled);
                stringToFormat = Regex.Replace(stringToFormat, "\\[publicemail\\]", user.Profile.PublicEmail.Trim(), RegexOptions.IgnoreCase | RegexOptions.Compiled);
                stringToFormat = Regex.Replace(stringToFormat, "\\[datecreated\\]", user.GetTimezone(user.DateCreated).ToString(user.Profile.DateFormat), RegexOptions.IgnoreCase | RegexOptions.Compiled);
                stringToFormat = Regex.Replace(stringToFormat, "\\[lastlogin\\]", user.GetTimezone(user.LastLogin).ToString(user.Profile.DateFormat), RegexOptions.IgnoreCase | RegexOptions.Compiled);
                stringToFormat = Regex.Replace(stringToFormat, "\\[profileurl\\]", Globals.GetSiteUrls().UserEditProfile, RegexOptions.IgnoreCase | RegexOptions.Compiled);

				if (user.Password != null)
					stringToFormat = Regex.Replace(stringToFormat, "\\[password\\]", user.Password.Trim(), RegexOptions.IgnoreCase | RegexOptions.Compiled);

            }

			// make urls clickable, don't do it if we have a post, 
			// because we're going to do it again before adding the post contents
			if (html && post == null) stringToFormat = Regex.Replace(stringToFormat,@"(http|ftp|https):\/\/[\w]+(.[\w]+)([\w\-\.,@?^=%&:/~\+#]*[\w\-\@?^=%&/~\+#])?","<a href=\"$0\">$0</a>",RegexOptions.IgnoreCase | RegexOptions.Compiled);

            if (post != null) {
                stringToFormat = Regex.Replace(stringToFormat, "\\[postedby\\]", post.Username.Trim(), RegexOptions.IgnoreCase | RegexOptions.Compiled);
                stringToFormat = Regex.Replace(stringToFormat, "\\[subject\\]", post.Subject.Trim(), RegexOptions.IgnoreCase | RegexOptions.Compiled);
                stringToFormat = Regex.Replace(stringToFormat, "\\[postdate\\]", post.User.GetTimezone(post.PostDate).ToString(), RegexOptions.IgnoreCase | RegexOptions.Compiled);
                stringToFormat = Regex.Replace(stringToFormat, "\\[replyurl\\]", Globals.GetSiteUrls().Post(post.PostID), RegexOptions.IgnoreCase | RegexOptions.Compiled);
                stringToFormat = Regex.Replace(stringToFormat, "\\[moderatePostUrl\\]", Globals.GetSiteUrls().Post(post.PostID), RegexOptions.IgnoreCase | RegexOptions.Compiled);

				if(CSContext.Current.Context != null)
					stringToFormat = Regex.Replace(stringToFormat, "\\[posturl\\]", Globals.HostPath(CSContext.Current.Context.Request.Url) + Globals.GetSiteUrls().Post(post.PostID), RegexOptions.IgnoreCase | RegexOptions.Compiled);
				else
					stringToFormat = Regex.Replace(stringToFormat, "\\[posturl\\]", Globals.HostPath(new Uri(CSContext.Current.RawUrl)) + Globals.GetSiteUrls().Post(post.PostID), RegexOptions.IgnoreCase | RegexOptions.Compiled);

				if(post.Section != null)
					stringToFormat = Regex.Replace(stringToFormat, "\\[forumname\\]", post.Section.Name.Trim(), RegexOptions.IgnoreCase | RegexOptions.Compiled);

                //stringToFormat = Regex.Replace(stringToFormat, "\\[forumUrl\\]", Globals.HostPath(CSContext.Current.Context.Request.Url) + ForumUrls.Instance().Forum(post.SectionID), RegexOptions.IgnoreCase | RegexOptions.Compiled);
				
				// make urls clickable before adding post HTML
				if (html) stringToFormat = Regex.Replace(stringToFormat,@"(http|ftp|https):\/\/[\w]+(.[\w]+)([\w\-\.,@?^=%&:\$/~\+#]*[\w\-\@?^=%&/~\+#])?","<a href=\"$0\">$0</a>",RegexOptions.IgnoreCase | RegexOptions.Compiled);
				
				// strip html from post if necessary
				string postbody = post.FormattedBody;				

				// if the user doesn't want HTML and the post is HTML, then strip it
				if (!html && post.PostType == PostType.HTML) 
					postbody = Emails.FormatHtmlAsPlainText(postbody);
				
					// if the user wants HTML and the post is PlainText, then add HTML to it
				else if (html &&  post.PostType == PostType.BBCode) 
					postbody = Emails.FormatPlainTextAsHtml(postbody);

				// Finally, trim this post so the user doesn't get a huge email
				//
				postbody.Trim();

				if (truncateMessage) {
					// if we throw an error, the post was too short to cut anyhow
					// 
					try {
						postbody = Formatter.CheckStringLength(postbody, 300);						
					}
					catch {}
				}

				stringToFormat = Regex.Replace(stringToFormat, "\\[postbody\\]", postbody, RegexOptions.IgnoreCase | RegexOptions.Compiled);
            }

            return stringToFormat;

        }


		// *********************************************************************
		//  FormatHtmlMessageAsPlainText
		//
		/// <summary>
		/// This method removes HTML from a string
		/// </summary>
		/// <param name="messageToFormat">Message to apply formatting to</param>
		/// 
		// ********************************************************************/

		protected static string FormatHtmlAsPlainText (string stringToFormat) {
			if (stringToFormat == null || stringToFormat == string.Empty) return "";
			
			// get rid of extra line breaks
			stringToFormat = Regex.Replace(stringToFormat,"\n"," ",RegexOptions.IgnoreCase | RegexOptions.Compiled);
			
			// add linebreaks from HTML for <br>, <p>, <li>, and <blockquote> tags
			stringToFormat = Regex.Replace(stringToFormat,@"</?(br|p|li|blockquote)(\s/)?>","\n",RegexOptions.IgnoreCase | RegexOptions.Compiled);
			
			// strip all remaining HTML
			stringToFormat = Regex.Replace(stringToFormat,@"</?(\w+)(\s*\w*\s*=\s*(""[^""]*""|'[^']'|[^>]*))*|/?>","",RegexOptions.IgnoreCase | RegexOptions.Compiled);

			// replace special characters
			stringToFormat = stringToFormat.Replace("&nbsp;", " ");
			stringToFormat = stringToFormat.Replace("&lt;", "<");
			stringToFormat = stringToFormat.Replace("&gt;", ">");
			stringToFormat = stringToFormat.Replace("&amp;", "&");
			stringToFormat = stringToFormat.Replace("&quot;", "\"");

			return stringToFormat;

		}

		// *********************************************************************
		//  FormatPlainTextAsHtml
		//
		/// <summary>
		/// This method formats a plain text message as HTML
		/// </summary>
		/// <param name="stringToFormat">Message to apply formatting to</param>
		/// 
		// ********************************************************************/

		protected static string FormatPlainTextAsHtml (string stringToFormat) {
			if (stringToFormat == null || stringToFormat == string.Empty) return "";
			
			// line breaks
			stringToFormat = Regex.Replace(stringToFormat,"\n","<br />",RegexOptions.IgnoreCase | RegexOptions.Compiled);

			// make urls clickable
			stringToFormat = Regex.Replace(stringToFormat,@"(http|ftp|https):\/\/[\w]+(.[\w]+)([\w\-\.,@?^=%&:/~\+#\$]*[\w\-\@?^=%&/~\+#])?","<a href=\"$0\">$0</a>",RegexOptions.IgnoreCase | RegexOptions.Compiled);

			return stringToFormat;
		}
		#endregion

        #region Post Emails

        protected static MailMessage CreatePost (Post post, string emailType) {
            return CreatePost(post, emailType, null, null, null, true);
        }

		protected static MailMessage CreatePost (Post post, string emailType, User userTo, string[] cc, string[] bcc, bool sendToUser) {
			return CreatePost(post, emailType, userTo, cc, bcc, true, false);
		}

        protected static MailMessage CreatePost (Post post, string emailType, User userTo, string[] cc, string[] bcc, bool sendToUser, bool html) {
            MailMessage email;

            // Get the email template we're going to use
            //
            email = GenericEmail(emailType, userTo, cc, bcc, sendToUser, html);

            if (userTo != null)
                email.To = userTo.Email;

            email.From = GenericEmailFormatter(email.From, null, post);
            email.Body = GenericEmailFormatter(email.Body, null, post, html);
            email.Subject = GenericEmailFormatter(email.Subject, null, post);

            return email;
        }

        // *********************************************************************
        //  PostApproved
        //
        /// <summary>
        /// This method sends an email to the user whose post has just been approved.
        /// </summary>
        /// <param name="PostID">Specifies the ID of the Post that was just approved.</param>
        /// 
        // ********************************************************************/
        public static void PostApproved (Post post) {
            if (post == null)
                return;

            // Make sure we can send the email
            //
            if (!CanSend(post.User))
                return;

            EnqueuEmail(CreatePost(post, EmailType.MessageApproved, post.User, null, null, true));
        }

        // *********************************************************************
        //  PostMoved
        //
        /// <summary>
        /// This method sends an email to the user whose approved post has just been moved.
        /// </summary>
        /// <param name="postID">The post to move</param>
        // ********************************************************************/        
        public static void PostMoved (Post post) {
            if (post == null)
                return;

            // Make sure we can send the email
            //
            if (!CanSend(post.User))
                return;

            EnqueuEmail(CreatePost(post, EmailType.MessageMoved, post.User, null, null, true));
        }

        // *********************************************************************
        //  PostMovedAndApproved
        //
        /// <summary>
        /// This method sends an email to the user whose post has just been moved AND approved.
        /// </summary>
        /// <param name="postID">Specifies the ID of the Post that was just approved.</param>
        /// 
        // ********************************************************************/
        public static void PostMovedAndApproved (Post post) {
            if (post == null)
                return;

            // Make sure we can send the email
            //
            if (!CanSend(post.User))
                return;

            EnqueuEmail(CreatePost(post, EmailType.MessageMovedAndApproved, post.User, null, null, true));
        }

        
        public static void ThreadJoined (Post parent, Post child) {
        }

        // *********************************************************************
        //  PostRemoved
        //
        /// <summary>
        /// Email sent when a post is removed.
        /// </summary>
        // ********************************************************************/
        public static void PostRemoved(Post post, User moderatedBy, string reason) {
            if (post == null)
                return;

            // Make sure we can send the email
            //
            if (!CanSend(post.User))
                return;

            MailMessage email = CreatePost(post, EmailType.MessageDeleted, post.User, null, null, true);

            email.Body = email.Body.Replace("[DeleteReasons]", reason);
            email.Body = email.Body.Replace("[DeletedByID]", moderatedBy.UserID.ToString());
            email.Body = email.Body.Replace("[Moderator]", moderatedBy.Username);

            EnqueuEmail(email);
        }

        #endregion

		#region GetEmailsTracking

		protected static Hashtable GetEmailsTrackingSectionByPostID(int postID) {
			// Create Instance of the CommonDataProvider
		    CommonDataProvider dp = CommonDataProvider.Instance();
			
			return dp.GetEmailsTrackingSectionByPostID(postID);
		}

        // *********************************************************************
        //  GetEmailsTrackingThread
        //
        /// <summary>
        /// Retrieves a list of email addresses from the users who are tracking a particular thread.
        /// </summary>
        /// <param name="postID">The PostID of the new message.  We really aren't interested in this
        /// Post, specifically, but the thread it belongs to.</param>
        /// <returns>A ArrayList with the email addresses of those who want to receive
        /// notification when a message in this thread is replied to.</returns>
        /// 
        // ********************************************************************/
        protected static Hashtable GetEmailsTrackingThread(int postID) {
            // Create Instance of the CommonDataProvider
            CommonDataProvider dp = CommonDataProvider.Instance();

            return dp.GetEmailsTrackingThread(postID);
        }		
		#endregion

        #region Protected helper methods
        protected static bool CanSend (User user) {

            if ((user == null) || (!user.EnableEmail))
                return false;

            return true;
        }

        protected static MailMessage GenericEmail (string emailType, User user, string[] cc, string[] bcc) {
            return GenericEmail (emailType, user, cc, bcc, true, false);
        }

		protected static MailMessage GenericEmail (string emailType, User user, string[] cc, string[] bcc, bool sendToUser) {
			return GenericEmail (emailType, user, cc, bcc, sendToUser, false);
		}

        protected static MailMessage GenericEmail (string emailType, User user, string[] cc, string[] bcc, bool sendToUser, bool html) {
			MailMessage email = new MailMessage();
			
			//try {
				Hashtable emailTemplates = null;
				
				// first try to load the templates in the user language
				if(user != null && !Globals.IsNullorEmpty(user.Profile.Language)) {
					emailTemplates = LoadEmailTemplates( user.Profile.Language );
				}
			
				// if the user language templates are not found, then load the system defaults
				if( emailTemplates == null ||
					emailTemplates.ContainsKey( emailType ) == false ) {
	
					emailTemplates = LoadEmailTemplates( CSConfiguration.GetConfig().DefaultLanguage );

					// if they still are not found, then load the en-US templates
					if( emailTemplates == null ||
						emailTemplates.ContainsKey( emailType ) == false ) {

						emailTemplates = LoadEmailTemplates( "en-US" );
					}
				}


				EmailTemplate template =  emailTemplates[emailType] as EmailTemplate;

				email.Subject = template.Subject;
				email.Priority = template.Priority;
				email.From = template.From;
				email.Body = template.Body;
			
				if (html) {
					email.BodyFormat = MailFormat.Html;
					email.Body = "<html><body>" + Emails.FormatPlainTextAsHtml(email.Body).Trim() + "</body></html>";	
				}

				// Set to:
				//
				if (sendToUser)
					email.To = user.Email;
				else
					email.To = "";

				if (cc != null)
					email.Cc = Transforms.ToDelimitedString(cc, ",");

				if (bcc != null)
					email.Bcc = Transforms.ToDelimitedString(bcc, ",");
			//}
			//catch( Exception e ) {
			    //CSException ex = new CSException( CSExceptionType.EmailUnableToSend, "Error when trying to send GenericEmail", e );
				//ex.Log();
			//}

			return email;
        }

        protected static Hashtable LoadEmailTemplates (string language) {
            Hashtable emailTemplates;
        	FileInfo f;
            string cacheKey = "emailTemplates-" + language;
            CSContext csContext = CSContext.Current;

            emailTemplates = CSCache.Get(cacheKey) as Hashtable;
            if (emailTemplates == null) {
                emailTemplates = new Hashtable();

                try {

                    f = new FileInfo(csContext.PhysicalPath("Languages\\" + language + "\\emails\\emails.xml"));//   csContext.MapPath("~" + CSConfiguration.GetConfig().FilesPath + "\\Languages\\" + language + "\\emails\\emails.xml" ));
                } catch {
                    
                    throw new CSException(CSExceptionType.EmailTemplateNotFound, "No email templates found for language: " + language);
                }


                // Read in the file
                //
                FileStream reader = f.OpenRead();
            	XmlDocument d = new XmlDocument();
                d.Load(reader);
                reader.Close();

                // Loop through all contained emails
                //
                foreach (XmlNode node in d.GetElementsByTagName("email")) {

                    // Create a new email template
                    //
                    EmailTemplate t = new EmailTemplate(node);

                    // Add to the lookup table
                    //
                    emailTemplates.Add(t.EmailType, t);

                }

				// Terry Denham 7/26/2004
				// changing default caching duration to 2 hours, intead of forever
                // ScottW 1/24/2005 adding file dependency
                System.Web.Caching.CacheDependency dep = new CacheDependency(f.FullName);
                CSCache.Insert(cacheKey,emailTemplates,dep,2 * CSCache.HourFactor);
                
                
            }

            return emailTemplates;
        
        }
        #endregion

    }
}
