//------------------------------------------------------------------------------
// <copyright company="Telligent Systems">
//     Copyright (c) Telligent Systems Corporation.  All rights reserved.
// </copyright> 
//------------------------------------------------------------------------------

using System;

namespace GPRP.GPRPEnumerations {

	/// <summary>
	/// The EmailTypeEnum enumeration determines what type of email template is used to send an email.
	/// The available options are: ForgottenPassword, ChangedPassword, NewMessagePostedToThread,
	/// NewUserAccountCreated, MessageApproved, MessageMovedAndApproved, MessageMovedAndNotApproved,
	/// MessageDeleted, ModeratorEmailNotification and MessageMovedAlreadyApproved.
	/// </summary>
	public class EmailType {
		/// <summary>
		/// Sends a user their username and password to the email address on file.
		/// </summary>
		public static readonly string ForgottenPassword = "ForgottenPassword";

		/// <summary>
		/// Sends an email to the user when he changes his password.
		/// </summary>
		public static readonly string ChangedPassword = "ChangedPassword";

		/// <summary>
		/// Sends a mass emailing when a new post is added to a thread.  Those who receive the email are those
		/// who have email thread tracking turned on for the particular thread that the new post was added to.
		/// </summary>
		public static readonly string NewMessagePostedToThread = "NewMessagePostedToThread";

		/// <summary>
		/// When a user creates a new account, this email template sends their UrlShowPost information (username/password).
		/// </summary>
		public static readonly string NewUserAccountCreated = "NewUserAccountCreated";

		/// <summary>
		/// When a user's post that was awaiting moderation is approved, they are sent this email.
		/// </summary>
		public static readonly string MessageApproved = "MessageApproved";

		/// <summary>
		/// If a user's post is moved from one forum to another, this email indicates this fact.
		/// </summary>
		public static readonly string MessageMovedAndApproved = "MessageMovedAndApproved";

		/// <summary>
		/// If a user's post is deleted, this email explains why their post was deleted.
		/// </summary>
		public static readonly string MessageDeleted = "MessageDeleted";

		/// <summary>
		/// When a new post needs to be approved, those moderators of the posted-to forum who have email
		/// notification turned on are sent this email to instruct them that there is a post waiting moderation.
		/// </summary>
		public static readonly string ModeratorEmailNotification = "ModeratorEmailNotification";

		/// <summary>
		/// If a user's approved post is moved from one forum to another, this email indicates this fact.
		/// </summary>
		public static readonly string MessageMoved = "MessageMoved";
		
		/// <summary>
		/// Email from user to user.
		/// </summary>
		public static readonly string SendEmail = "SendEmail";

		/// <summary>
		/// When a user creates a new account and it requires admin approval.
		/// </summary>
		public static readonly string NewUserAccountPending = "NewUserAccountPending";

		/// <summary>
		/// When admin approve a new user account request and notify the user to use it.
		/// </summary>
		public static readonly string NewUserAccountApproved = "NewUserAccountApproved";

		/// <summary>
		/// When admin rejected a new user account request and notify the user.
		/// </summary>
		public static readonly string NewUserAccountRejected = "NewUserAccountRejected";

		/// <summary>
		/// Sent from admin to all users in a role
		/// </summary>
		public static readonly string RoleEmail = "RoleEmail";


		/// <summary>
		/// Email to send a notification of a picture to a friend
		/// </summary>
		public static readonly string SendToFriend = "SendToFriend";

		/// <summary>
		/// Email to send a notification of a picture to a friend
		/// </summary>
		public static readonly string PrivateMessageNotification = "PrivateMessageNotification";

	}
	/***************************************************/
}
