//------------------------------------------------------------------------------
// <copyright company="Telligent Systems">
//     Copyright (c) Telligent Systems Corporation.  All rights reserved.
// </copyright> 
//------------------------------------------------------------------------------
using GPRP.GPRPEnumerations;
namespace GPRP.GPRPComponents
{
	public class CSEvents
	{
		private CSEvents(){}

		#region Users
		/// <summary>
		/// Raises all UserValidated events. These events are raised after a users credentials have been validated
		/// </summary>
		/// <param name="user"></param>
		public static void UserValidated(User user)
		{
			CSApplication.Instance().ExecuteUserValidated(user);
		}

		/// <summary>
		/// Raises all UserKnown events. These events are raised the first time CS "recognizes" the current user. 
		/// </summary>
		/// <param name="user"></param>
		public static void UserKnown(User user)
		{
			CSApplication.Instance().ExecuteUserKnown(user);
		}

		/// <summary>
		/// Raises all BeforeUser events. These events are raised before any committements are
		/// made to the User (create or update)
		/// </summary>
		public static void BeforeUser(User user, ObjectState state)
		{
			CSApplication.Instance().ExecutePreUserUpdate(user,state);
		}

		/// <summary>
		/// Raises all AfterUser events. These events are raised after changes (created or update) are 
		/// made to the datastore
		/// </summary>
		public static void AfterUser(User user, ObjectState state)
		{
			CSApplication.Instance().ExecutePostUserUpdate(user,state);
		}

		/// <summary>
		/// Raises all UserRemoved events. These events are raised before the user is removed from the data store
		/// </summary>
		/// <param name="user"></param>
		public static void UserRemoved(User user)
		{
			CSApplication.Instance().ExecuteUserRemove(user);
		}


		#endregion

		#region Posts

		/// <summary>
		/// Raises all AuthorizePost events. These events are raised before a user can access a page (control) 
		/// which includes allows new content to be posted.
		/// </summary>
		public static void AuthorizePost()
		{
			CSApplication.Instance().ExecuteAuthorizePost();
		}

		/// <summary>
		/// Raises all PrePost events. These events are raised before a post reaches BeforePost
		/// </summary>
		public static void PrePost(Post post, ObjectState state, ApplicationType appType)
		{
			CSApplication.Instance().ExecutePrePostEvents(post,state,appType);
		}

		/// <summary>
		/// Raises all BeforePost events. These events are raised before a post is commited to the datastore
		/// </summary>
		public static void BeforePost(Post post, ObjectState state, ApplicationType appType)
		{
			CSApplication.Instance().ExecutePrePostUpdateEvents(post,state,appType);
		}

		/// <summary>
		/// Raises all AfterPost events. These events are raised after a post is committed to the datastore
		/// </summary>
		public static void AfterPost(Post post, ObjectState state, ApplicationType appType)
		{
			CSApplication.Instance().ExecutePostPostUpdateEvents(post,state,appType);
		}

		/// <summary>
		/// Raises all RatePost events. These events are raised after a post is rated.
		/// </summary>
		public static void RatePost(Post post, ApplicationType appType)
		{
			CSApplication.Instance().ExecuteRatePostEvents(post,appType);
		}

//		/// <summary>
//		/// Raises all PreRenderPost events. These events should be raised before the post is rendered
//		/// </summary>
//		public static void PreRenderPost(Post post, ApplicationType appType)
//		{
//			CSApplication.Instance().ExecutePrePostRender(post,appType);
//		}

		#endregion

		#region Sections

		/// <summary>
		/// Raises all BeforeSection events. These events are raised before a section is commited to the datastore
		/// </summary>
		public static void BeforeSection(Section section, ObjectState state, ApplicationType appType)
		{
			CSApplication.Instance().ExecutePreSectionUpdate(section,state,appType);
		}

		/// <summary>
		/// Raises all AfterSection events. These events are raised after a section is commited to the datastore
		/// </summary>
		public static void AfterSection(Section section, ObjectState state, ApplicationType appType)
		{
			CSApplication.Instance().ExecutePostSectionUpdate(section,state,appType);
		}

		#endregion

		#region Groups

		/// <summary>
		/// Raises all BeforeGroup events. These events are raised before a group change is commited to the datastore
		/// </summary>
		public static void BeforeGroup(Group group, ObjectState state, ApplicationType appType)
		{
			CSApplication.Instance().ExecutePreSectionGroupUpdate(group,state,appType);
		}

		/// <summary>
		/// Raises all AfterGroup events. These events are raised after a group change is commited to the datastore
		/// </summary>
		public static void AfterGroup(Group group, ObjectState state, ApplicationType appType)
		{
			CSApplication.Instance().ExecutePostSectionGroupUpdate(group,state,appType);
		}

		#endregion

		#region Exceptions

		public static void CSException(CSException csEx)
		{
			CSApplication.Instance().ExecuteCSExcetion(csEx);
		}

		#endregion

	}
}