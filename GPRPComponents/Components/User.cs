//------------------------------------------------------------------------------
// <copyright company="Telligent Systems">
//     Copyright (c) Telligent Systems Corporation.  All rights reserved.
// </copyright> 
//------------------------------------------------------------------------------

// 修改说明：增加若干用户属性 
// 修改人：宝玉
// 修改日期：2005-02-26

using System;
using System.Web;
using System.Collections;
using System.Collections.Specialized;
using System.Text;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

using Microsoft.ScalableHosting.Profile;
using Microsoft.ScalableHosting.Security;

using GPRP.GPRPEnumerations;
namespace GPRP.GPRPComponents {

	// *********************************************************************
	//	User
	//
	///	<summary>
	///	This class contains	the	properties for a User.
	///	</summary>
	///	
	// ********************************************************************/

	[Serializable]
	public class User : ExtendedAttributes
	{
		#region .cnstr

        public User(MembershipUser mu, ProfileBase pb):this(mu,new Profile(pb))
        {

        }

        public User(MembershipUser mu, ProfileData pd):this(mu,new Profile(pd))
        {

        }

        public User(MembershipUser mu, Profile p)
        {
            RefreshMembershipUser(mu);
            RefreshUserProfile(p);
            
        }

		public User()
		{
			RefreshUserProfile(new Profile());
		}

		public void RefreshMembershipUser(MembershipUser mu)
		{
			if(mu == null)
			{
				throw new Exception("A null MembershipUser is not valid to instantiate a new User");
			}
            

			this.memberUser = mu;
			this.username = mu.UserName;
		}

        public void RefreshUserProfile(Profile p)
        {
            if(p == null)
                throw new Exception("A null profile is not valid");

            memberProfile = p;

        }

        public Profile Profile
        {
            get{return memberProfile;}
        }

        public bool HasProfile
        {
            get{return Profile != null;}
        }
			
		#endregion

		#region Private Properties

		// Primary attributes
		//
        static RolesConfiguration roles = CSConfiguration.GetConfig().RolesConfiguration;
		int	userID;
		string username;
		string password;
		string privateEmail;
		MembershipPasswordFormat passwordFormat =	MembershipPasswordFormat.Hashed;
        
		DateTime dateLastActive;
		string lastAction;
		UserAccountStatus accountStatus	= UserAccountStatus.Approved;
		bool isAnonymous = true;
		bool enableEmail = true;
		bool forceLogin	= false;
       

		private MembershipUser memberUser = null;
        private Profile memberProfile = null;

		// Extended	attributes
		//
		NameValueCollection	extendedAttributes = new NameValueCollection();
		int	totalPosts;
		byte[] postRank;
		//int[] groups;
		bool enableAvatar = true; // 默认True
		ModerationLevel	moderationLevel	= ModerationLevel.Moderated;
		bool isAvatarApproved =	true;
		bool enableThreadTracking;
		SortOrder postSortOrder	= SortOrder.Ascending;
		bool enableOnlineStatus;
		bool enableDisplayInMemberList	= true;
		bool enablePrivateMessages;
		bool enableHtmlEmail;
		string salt	= string.Empty;
		string appUserToken	= string.Empty;
        AuditSummary auditCounters = null;
		
		#region 新增内容
		private string nickname = "";
		private string ipCreated;
		private string ipLastActivity;
		private DateTime birthday = DateTime.MinValue;
		private int databaseQuota = 10240;
		private int databaseQuotaUsed = 0;

		#endregion

		#endregion

        #region IsRoles

		protected bool IsInRoles( string[] roleNames ) 
		{
			string [] userRoles = Roles.GetUserRoleNames( this.Username );
			foreach( string userRole in userRoles ) {
				foreach( string roleName in roleNames ) {
					if( roleName == userRole )
						return true;
				}
			}
			return false;
		}

        ///	<summary>
        ///	Specifies if a user	in a System Administator administrator or not.
        ///	</summary>
        public bool	IsAdministrator	
        {
            get	
            { 
                try	
                {
					return IsInRoles( new string[] { roles.SystemAdministrator } );
                }
                catch	{}

                return false;
            }					 
        }

        ///	<summary>
        ///	Specifies if a user	in an administrator	or not.
        ///	</summary>
        public bool	IsBlogAdministrator	
        {
            get	
            { 
                try	
                {
					return IsInRoles( new string[] { roles.SystemAdministrator, roles.BlogAdministrator } );
                }
                catch	{}

                return false;
            }					 
        }

        ///	<summary>
        ///	Specifies if a user	in an administrator	or not.
        ///	</summary>
        public bool	IsGalleryAdministrator	
        {
            get	
            { 
                try	
                {
					return IsInRoles( new string[] { roles.SystemAdministrator, roles.GalleryAdministrator } );
                }
                catch	{}

                return false;
            }					 
        }

        ///	<summary>
        ///	Specifies if a user	in an administrator	or not.
        ///	</summary>
        public bool	IsForumAdministrator	
        {
            get	
            { 
                try	
                {
					return IsInRoles( new string[] { roles.SystemAdministrator, roles.ForumsAdministrator } );
                }
                catch	{}

                return false;
            }					 
        }

        ///	<summary>
        ///	Specifies if a user	in an administrator	or not.
        ///	</summary>
        public bool	IsModerator	
        {
            get	
            { 
                try	
                {
					return IsInRoles( new string[] { roles.SystemAdministrator, roles.Moderator } );
                } 
                catch	{}

                return false;
            }
        }



        /// <summary>
        /// Lookup to determine if this user belongs to the editor role.
        /// </summary>
        public bool IsEditor 
        {
            get 
            {
                try 
                {
					return IsInRoles( new string[] {roles.SystemAdministrator, roles.Editor } );
                }
                catch {}

                return false;
            }
        }

//        public static bool IsInRole(string rolename) 
//        {
//            return HttpContext.Current.User.IsInRole(rolename);
//        }

        #endregion

		#region Public Properties

		public MembershipUser Member
		{
			get{return memberUser;}
		}

		public UserCookie GetUserCookie() 
		{
		    return new UserCookie( this	);
		}

		public string LastAction 
		{
			get	
			{
				return lastAction;
			}
			set	
			{
				lastAction = value;
			}
		}

		public string Username 
		{
			get { return this.username; }
			set 
			{
                if(this.Member != null)
                {
                    #if DEBUG
                    throw new Exception("WSHA Provider can not update usernames");
                    #endif
                }
                else
                {
                    this.username = value;
                }

			}
		}

		public string DisplayName {
			get { 
				string cn = this.Profile.CommonName;

				if (cn == string.Empty)
					return this.username; 

				return cn;
			}
		}

		public string Password 
		{
			get { return this.password; }
			set 
			{
				//We sometimes use this a temporarty container. Need a cleaner way of allowing this
//				if(this.Member != null)
//				{
//					throw new Exception("SHS can not be changed directly");
//				}
				this.password = value;

			}
		}

		#region 新增内容

		/// <summary>
		/// 昵称
		/// </summary>
		public string Nickname
		{
			get 
			{
				if (nickname == "")
					nickname = username;
				return nickname;
			}
			set 
			{
				nickname = value;
			}
		}

		/// <summary>
		/// 注册IP
		/// </summary>
		public string IPCreated
		{
			get 
			{
				return ipCreated;
			}
			set 
			{
				ipCreated = value;
			}
		}

		/// <summary>
		/// 最后活动IP
		/// </summary>
		public string IPLastActivity
		{
			get 
			{
				return ipLastActivity;
			}
			set 
			{
				ipLastActivity = value;
			}
		}

		/// <summary>
		/// 生日
		/// </summary>
		public DateTime Birthday
		{
			get 
			{
				return birthday;
			}
			set 
			{
				birthday = value;
			}
		}

		/// <summary>
		/// 磁盘配额
		/// </summary>
		/// <remarks>默认10mb</remarks>
		public int DatabaseQuota
		{
			get 
			{
				return databaseQuota;
			}
			set 
			{
				databaseQuota = value;
			}
		}

		/// <summary>
		/// 已使用的磁盘空间
		/// </summary>
		public int DatabaseQuotaUsed
		{
			get 
			{
				return databaseQuotaUsed;
			}
			set 
			{
				databaseQuotaUsed = value;
			}
		}

		#endregion

		public string PasswordQuestion 
		{
		    get
		    {
				if(this.Member != null)
					return Member.PasswordQuestion;
				else
					return null; 
		    }
		}

		string passwordAnswer = null;
		public string PasswordAnswer {
			get { return passwordAnswer; }
			set { passwordAnswer = value; }
		}

		///	<summary>
		///	Unique identifier for the user.
		///	</summary>
		public int UserID 
		{
			get	{ return userID; }
			set	{ userID = value; }			
		}

		///	<summary>
		///	Determins if the user's	online status can be displayed.
		///	</summary>
		public bool	EnableOnlineStatus 
		{
			get	{ return enableOnlineStatus; }
			set	{ enableOnlineStatus = value; }
		}

		///	<summary>
		///	Determines if the user is displayed	in the member list.
		///	</summary>
		public bool	EnableDisplayInMemberList 
		{
			get	{ return enableDisplayInMemberList;	}
			set	{ enableDisplayInMemberList	= value; }
		}
		
		///	<summary>
		///	Can	the	user send/recieve private messages.
		///	</summary>
		public bool	EnablePrivateMessages 
		{
			get	{ return enablePrivateMessages;	}
			set	{ enablePrivateMessages	= value; }
		}

		///	<summary>
		///	Does the user want to recieve Html Email.
		///	</summary>
		public bool	EnableHtmlEmail	
		{
			get	{ return enableHtmlEmail; }
			set	{ enableHtmlEmail =	value; }
		}

		///	<summary>
		///	Does the user want to recieve Email.
		///	</summary>
		public bool	EnableEmail	
		{
			get	{ return enableEmail; }
			set	{ enableEmail =	value; }
		}
		
		///	<summary>
		///	Used to	determine the user's post rank.
		///	</summary>
		public byte[] PostRank 
		{
			get	{ return postRank; }
			set	{ postRank = value;	}
		}


		public MembershipPasswordFormat PasswordFormat 
		{
			get	{ return passwordFormat; }
			set	{ passwordFormat = value; }
		}

		public string AppUserToken 
		{
			get	{ return appUserToken; }
			set	{ appUserToken = value;	}			
		}

		///	<summary>
		///	Controls views in posts
		///	</summary>
		public SortOrder PostSortOrder 
		{
			get	{ return postSortOrder;	}
			set	{ postSortOrder	= value; }
		}
		

		///	<summary>
		///	Controls whether or	not	a user's avatar	is shown
		///	</summary>
		public bool	EnableAvatar 
		{
			get	{ return enableAvatar; }
			set	{ enableAvatar = value;	}
		}

		///	<summary>
		///	Path to	the	user's avatar
		///	</summary>
		public string AvatarUrl	
		{
			get	
			{
				return GetExtendedAttribute("avatarUrl");
			}
			set	
			{
				SetExtendedAttribute("avatarUrl", value);
			}
		}

		///	<summary>
		///	Returns	the	user's real	email address.	It is this email address that the user is sent
		///	email notifications.
		///	</summary>
		public String Email	
		{
			get	
			{
				if(this.Member != null)
				{
					return this.Member.Email;
				}
				else
				{
					return privateEmail; 
				}
			}
			set	
			{ 
				if(this.Member != null)
				{
					Member.Email = value;
				}
				else
				{
					privateEmail = value;	
				}
			}
		}

		///	<summary>
		///	Icon for the user
		///	</summary>
		public bool	HasAvatar 
		{
			get	
			{ 
				if (this.AvatarUrl.Length >	0)
					return true;
				return false;
			}
		}

		///	<summary>
		///	ICQ	address
		///	</summary>
		public String Theme	
		{
			get	
			{ 
				string skin	= GetExtendedAttribute("Theme"); 

				if (skin ==	string.Empty)
					skin = "default";

				return skin;
			}
			set	{ SetExtendedAttribute("Theme",	value);	}
		}

		///	<summary>
		///	Total posts	by this	user
		///	</summary>
		public int TotalPosts 
		{
			get	{ return totalPosts; }
			set	{ totalPosts = value; }
		}
		
        /// <summary>
        /// Dummy post counter used to display user activity by predefined user levels.
        /// This could be artificially increased by admin to reach certain user level 
        /// reserved for admin/moderators.
        /// </summary>
		public Int32 DummyTotalPosts {
			get { 
				string returnValue = GetExtendedAttribute( "dummyTotalPosts" );

                // Do we have a valid value?
                //
                try {
                    return Int32.Parse( returnValue );
                } catch { 
                    // If not, starting now we will have one
                    //
                    SetExtendedAttribute( "dummyTotalPosts", TotalPosts.ToString() );
                    return (Int32) TotalPosts;
                }
			}
			set {
				SetExtendedAttribute( "dummyTotalPosts", value.ToString() );
			}
		}

		///	<summary>
		///	The	date/time the user's account was created.
		///	</summary>
		public DateTime	DateCreated	
		{
			get	{ return Member.CreationDate; }
			//set	{ dateCreated =	value; }
		}

		///	<summary>
		///	The	date/time the user last	logged in.
		///	</summary>
		public DateTime	LastLogin 
		{
			get	{ return Member.LastLoginDate;	}
			//set	{ dateLastLogin	= value; }
		}

		///	<summary>
		///	The	date/time the user last	logged in.
		///	</summary>
		public DateTime	LastActivity 
		{
			get	{ return dateLastActive; }
			set	{ dateLastActive = value; }
		}

		///	<summary>
		///	Specifies whether a	user is	Approved or	not.  Non-approved users cannot	log	into the system
		///	and, therefore,	cannot post	messages.
		///	</summary>
		public bool	IsBanned 
		{
			get	
			{ 
				if (accountStatus == UserAccountStatus.Banned)
					return true;
				else
					return false;
			}
		}

		///	<summary>
		///	Specifies the date until the user account is banned.
		///	It makes sense only when UserAccountStatus is set on 2.
		///	</summary>
		public DateTime	BannedUntil 
		{
			get	
			{ 
				try 
				{
					return DateTime.Parse(GetExtendedAttribute("BannedUntil"));
				} 
				catch 
				{
					return DateTime.Now;
				}
			}
			set	{ SetExtendedAttribute("BannedUntil", value.ToString()); }
		}

		///	<summary>
		///	Specifies whether a	user is	Approved or	not.  Non-approved users cannot	log	into the system
		///	and, therefore,	cannot post	messages.
		///	</summary>
		public bool	ForceLogin 
		{
			get	{ return forceLogin; }
			set	{ forceLogin = value; }
		}

		public UserAccountStatus AccountStatus 
		{
			get	
			{
				return accountStatus;
			}
			set	
			{
				accountStatus =	value;
			}
		}

		///	<summary>
		///	Specifies whether a	user's profiles	is Approved	or not.
		///	</summary>
		public bool	IsAvatarApproved 
		{
			get	{ return isAvatarApproved; }
			set	{ isAvatarApproved = value;	}
		}
		
		///	<summary>
		///	Returns	if a user is trusted or	not.  A	trusted	user is	one	whose messages do not require
		///	any	sort of	moderation approval.
		///	</summary>
		public ModerationLevel ModerationLevel 
		{
			get	{ return moderationLevel; }
			set	{ moderationLevel =	value; }
		}


		///	<summary>
		///	Specifies if the user wants	to automatically turn on email tracking	for	threads	that 
		///	he/she posts to.
		///	</summary>
		public bool	EnableThreadTracking 
		{
			get	{ return enableThreadTracking; }
			set	{ enableThreadTracking = value;	}
		}

		public bool	IsAnonymous	
		{
			get	
			{
				return isAnonymous;
			}
			set	
			{
				isAnonymous	= value;
			}
		}

		#endregion

		#region	Timezone
		// *********************************************************************
		//	GetTimezone
		//
		///	<summary>
		///	Adjusts	a date/time	for	a user's particular	timezone offset.
		///	</summary>
		///	<param name="dtAdjust">The time	to adjust.</param>
		///	<param name="user">The user	viewing	the	time.</param>
		///	<returns>A datetime	adjusted for the user's	timezone offset.</returns>
		///	
		// ********************************************************************/
		public DateTime	GetTimezone(DateTime date) 
		{
			
			if (IsAnonymous)
				return date;

			return date.AddHours(Profile.Timezone -	CSContext.Current.SiteSettings.TimezoneOffset);
		}

		public DateTime	GetTimezone	() 
		{
			return GetTimezone(DateTime.Now);
		}
		#endregion

		#region IsOnline
		        public bool IsOnline {
		            get {
							//validate this method before we use it.
							//return Member.IsOnline;
		                ArrayList users = Users.GetUsersOnline( CSContext.Current.SiteSettings.UserOnlineTimeWindow );
		                if( users != null ) {
		                    foreach( User tmpUser in users ) {
		                        if( tmpUser.UserID == this.UserID ) {
		                            return true;
		                        }
		                    }
		                }
		                return false;
		            }
		        }        
		        #endregion

		#region IsRegistered
		public bool IsRegistered 
		{
			get 
			{
				if (this.UserID > 0 &&
					this.Username != null && this.Username.Length > 0 &&
					this.Email != null && this.Email.Length > 0)
					return true;

				return false;
			}
		}        
		#endregion

		#region Change Password for logged on user
                
                 public bool ResetPassword(string answer)
                 {
                     CommonDataProvider cdp = CommonDataProvider.Instance();
                     if(cdp.ValidateUserPasswordAnswer(this.Member.ProviderUserKey, answer))
                     {

                         try
                         {
                             string password = Member.ResetPassword(answer);
                             
                             Audit.SaveUserAuditEvent( ModerateUserSetting.PasswordReset, this, CSContext.Current.User.UserID );
                             
                             Emails.UserPasswordForgotten (this, password);
                             return true;
                         }
                         /*
                         catch(MembershipPasswordException ex) {
                             throw new CSException( CSExceptionType.UnknownError, ex.Message );                             
                         }*/
                         catch (Exception ex)  {
                             throw new CSException( CSExceptionType.UnknownError, ex.Message );
                         }		
                     }


                     return false;
                 }

		        // *********************************************************************
		        //	ChangePassword
		        //
		        ///	<summary>
		        ///	Changes	the	password for the currently logged on user.
		        ///	</summary>
		        ///	<param name="password">User's current password.</param>
		        ///	<param name="newPassword">User's new password.</param>
		        ///	<returns>Indicates whether or not the password change succeeded</returns>
		        // ***********************************************************************/
		        public bool	ChangePassword (string password, string	newPassword) 
		        {
		            // Check to ensure the passwords match and get the salt
		            //
		            // If this instance of the user object can be validated or
		            // the logged in user is an administrator then allow the password 
		            // change to go through. The user this, is populated from the UserID
		            // specified in the changepassword url.
		            if( (Users.ValidUser(this) == LoginUserStatus.Success) || (CSContext.Current.User.IsAdministrator)) {// || (CSContext.Current.User.IsModerator) ) {
		
		                // NOTE: If	new	property named Salt	will be	added to user object,
		                // then	the	salt might be reused, because it could be loaded in	
		                // Users.ValidUser() method. Also user's PasswordFormat	might be used 
		                // instead of current site's PasswordFormat	value.	
		
		                // Generate	new	salt and do	the	encryption
		                //
		                //string newSalt = Users.CreateSalt();
		                //CommonDataProvider dp =	CommonDataProvider.Instance();
		                //dp.UserChangePassword(userID, this.PasswordFormat,	Users.Encrypt(this.PasswordFormat,	newPassword, newSalt), newSalt);				  

						// Reset and then change to new password value
						if ((CSContext.Current.User.IsAdministrator) && (password == ""))
							password = Member.ResetPassword();

                        try {
                            // NOTE: We will now pass this along to SHS
                            if (this.Member.ChangePassword(password,newPassword)) {
                                // Email the user their password
                                Emails.UserPasswordChanged (this, newPassword);
                            
                                Audit.SaveUserAuditEvent( ModerateUserSetting.PasswordChanged, this, CSContext.Current.User.UserID );
							
                                return true;
                            }
                        }
                        /*
                        catch (MembershipPasswordException ex) { 
                            throw new CSException( CSExceptionType.UnknownError, ex.Message );
                        }*/
                        catch (Exception ex)  {
                            throw new CSException( CSExceptionType.UnknownError, ex.Message );
                        }		
		            } 

		            return false;
		        }
	 #endregion

        #region Change Secret Answer for a logged on user
		        // *********************************************************************
		        //	ChangePasswordAnswer
		        //
		        ///	<summary>
		        ///	Changes	the	password/secret answer for the currently logged on user.
		        ///	</summary>
		        ///	<param name="answer">User's current password answer.</param>
		        ///	<param name="newQuestion">User's new password question.</param>
		        ///	<param name="newAnswer">User's new password answer.</param>
		        ///	<returns>Indicates whether or not the password answer change succeeded</returns>
		        // ***********************************************************************/
		        public bool	ChangePasswordAnswer(string answer, string newQuestion, string	newAnswer) 
		        {
					//Note: SHS does not support admin changing the question/answer. 
					//The user must supply the password, newQuestion, and newAnser. 

		            CommonDataProvider dp =	CommonDataProvider.Instance();
		            dp.UserChangePasswordAnswer(userID, newQuestion, newAnswer);				  
		
		            return true;
		        }
		        #endregion

		#region	ForgotPassword
		// *********************************************************************
		//	ForgotPassword
		//
		///	<summary>
		///	Mails the user their password when they	forgot it.
		///	</summary>
		///	
		// ********************************************************************/
		public bool	ResetPassword() 
		{

			//Note: WSHA Update
			//WSHA does not allow you to change a password without knowing the original password. 
			//So we will always have to generate a new one.

			try
			{
				string password  = Member.ResetPassword();

                Audit.SaveUserAuditEvent( ModerateUserSetting.PasswordReset, this, -1 );
                
                Emails.UserPasswordForgotten( this,	password);
				return true;
			}
			catch
			{
				//do we want to do this? Will we know if password updates fail?
				return false;
			}

		}
		#endregion

		public bool EnableCollapsingPanels {
			get { 
				string returnValue = GetExtendedAttribute("enableCollapsingPanels");
				
				if( returnValue == null || returnValue == string.Empty )
					return true;
				else
					return Boolean.Parse(returnValue);
			}
			set {
				SetExtendedAttribute("enableCollapsingPanels", ((bool)value).ToString() );
			}
		}

        private int _settingsID;
        /// <summary>
        /// Property SettingsID (int)
        /// </summary>
        public int SettingsID
        {
            get {  return this._settingsID; }
            set {  this._settingsID = value; }
        }

		private string roleKey = null;
		public string RoleKey
		{
			get
			{
				if(roleKey == null)
				{
					if(!IsAnonymous)
					{
						string[] roles = null;
						HttpContext context = HttpContext.Current;
						if(context != null)
						{
							RolePrincipal rp = context.User as RolePrincipal;
							if(rp != null)
								roles = rp.GetRoles();
						}

						if(roles == null)
							roles = Roles.GetUserRoleNames(this.Username,true);

						if(roles != null)
							roleKey = string.Join(",",roles);
						else
							roleKey = this.Username;
					}
				}
				return roleKey;
			}
		}
        
        #region Ban Reason
        public UserBanReason BanReason {
			get { 
				string returnValue = GetExtendedAttribute("UserBanReason");
				
				if( returnValue == null || returnValue == string.Empty )
					return UserBanReason.Other;
				else
					return (UserBanReason) Enum.Parse( typeof( UserBanReason ), returnValue );
			}
			set {
				SetExtendedAttribute( "UserBanReason", value.ToString() );
			}

        }
        #endregion

        #region Audit Counters
        /// <summary>
        /// Used in Members List. Otherwise it should be null.
        /// </summary>
        public AuditSummary AuditCounters { 
            get { 
                return auditCounters;
            }            
            set { 
                auditCounters = value;
            }
        }
        #endregion
	}
}
