//------------------------------------------------------------------------------
// <copyright company="Telligent Systems">
//     Copyright (c) Telligent Systems Corporation.  All rights reserved.
// </copyright> 
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.ComponentModel;
using System.Web.Caching;
using System.Xml;
using GPRP.GPRPEnumerations;

namespace GPRP.GPRPComponents
{

	#region Delegates
	//Do we want one single delegate or a custom one for each type
	//public delegate void CSEventHandler(object sender, CSEventArgs e);
	public delegate void CSUserEventHandler(User user, CSEventArgs e);
	public delegate void CSPostEventHandler(Post post, CSEventArgs e);
	public delegate void CSSectionEventHandler(Section section, CSEventArgs e);
	public delegate void CSGroupEventHandler(Group group, CSEventArgs e);
	public delegate void CSExceptionHandler(CSException csEx, CSEventArgs e);
	#endregion

	/// <summary>
	/// Summary description for CSApplication.
	/// </summary>
	public class CSApplication
	{
		#region private members
		private EventHandlerList Events = new EventHandlerList();
		private static readonly object sync = new object();
		private Hashtable modules = new Hashtable();
		#endregion

		#region Event Keys (static)
		private static object EventAuthorizePost = new object();
		private static object EventPrePostUpdate = new object();
		private static object EventPreProcessPost = new object();
		private static object EventPostPostUpdate = new object();
		private static object EventRatePost = new object();
		//private static object EventPreRenderPost = new object();

		private static object EventPreUserUpdate = new object();
		private static object EventPostUserUpdate = new object();
		private static object EventUserRemove = new object();
		private static object EventUserKnown = new object();
		private static object EventUserValidated = new object();

		private static object EventPreSectionUpdate = new object();
		private static object EventPostSectionUpdate = new object();

		private static object EventPreSectionGroupUpdate = new object();
		private static object EventPostSectionGroupUpdate = new object();

		private static object EventUnhandledException = new object();
		#endregion

		#region cnstr
		private CSApplication()
		{
		}

		internal static CSApplication Instance()
		{
			const string key = "CSApplication";
			CSApplication app = CSCache.Get(key) as CSApplication;
			if(app == null)
			{
				lock(sync)
				{
					app = CSCache.Get(key) as CSApplication;
					if(app == null)
					{
						CSConfiguration config = CSContext.Current.Config;

						XmlNode node = config.GetConfigSection("CommunityServer/CSModules");
						app = new CSApplication();
						if(node != null)
						{
							foreach(XmlNode n in node.ChildNodes)
							{
								if(n.NodeType != XmlNodeType.Comment)
								{
									switch(n.Name)
									{
										case "clear":
											app.modules.Clear();
											break;
										case "remove":
											app.modules.Remove(n.Attributes["name"].Value);
											break;
										case "add":
										
											string name = n.Attributes["name"].Value;
											string itype = n.Attributes["type"].Value;

											Type type = Type.GetType(itype);

											if(type == null)
												throw new Exception(itype + " does not exist");

											ICSModule mod = Activator.CreateInstance(type) as ICSModule;

											if(mod == null)
												throw new Exception(itype + " does not implement ICSModule or is not configured correctly");

											mod.Init(app, n);
											app.modules.Add(name,mod);


											break;

									}
								}
							}
						}
						CacheDependency dep = new CacheDependency(null, new string[]{CSConfiguration.CacheKey});
						CSCache.Max(key, app,dep);
					}

					
				}
			}
			return app;
		}
		#endregion

		#region Post Events

		#region Execute Events

		internal void ExecuteAuthorizePost()
		{
			ExecuteUserEvent(EventAuthorizePost,CSContext.Current.User);
		}

		internal void ExecutePrePostEvents(Post post, ObjectState state, ApplicationType appType)
		{
			ExecutePostEvent(EventPreProcessPost,post,state,appType);
		}

		internal void ExecutePrePostUpdateEvents(Post post, ObjectState state, ApplicationType appType)
		{
			ExecutePostEvent(EventPrePostUpdate,post,state,appType);
		}

		internal void ExecutePostPostUpdateEvents(Post post, ObjectState state, ApplicationType appType)
		{
			ExecutePostEvent(EventPostPostUpdate,post,state,appType);
		}

		internal void ExecuteRatePostEvents(Post post, ApplicationType appType)
		{
			ExecutePostEvent(EventRatePost,post,ObjectState.None,appType);
		}

//		internal void ExecutePrePostRender(Post post, ApplicationType appType)
//		{
//			ExecutePostEvent(EventPreRenderPost,post,ObjectState.None,appType);
//		}

		protected void ExecutePostEvent(object EventKey, Post post,ObjectState state, ApplicationType appType)
		{
			CSPostEventHandler handler = Events[EventKey] as CSPostEventHandler;
			if (handler != null)
			{
				handler(post, new CSEventArgs(state,appType));
			}
		}

		#endregion

		#region Events
		/// <summary>
		/// Event raised before a user accesses a page which can be used to create content
		/// </summary>
		public event CSUserEventHandler AuthorizePost
		{
			add{Events.AddHandler(EventAuthorizePost, value);}
			remove{Events.RemoveHandler(EventAuthorizePost, value);}
		}

		/// <summary>
		/// Event raised before any post processing takes place
		/// </summary>
		public event CSPostEventHandler PreProcessPost
		{
			add{Events.AddHandler(EventPreProcessPost, value);}
			remove{Events.RemoveHandler(EventPreProcessPost, value);}
		}

		/// <summary>
		/// Fires after PreProcessPost but before the post change is commited to the datastore
		/// </summary>
		public event CSPostEventHandler PrePostUpdate
		{
			add{Events.AddHandler(EventPrePostUpdate, value);}
			remove{Events.RemoveHandler(EventPrePostUpdate, value);}
		}

		/// <summary>
		/// Fires after a post change is commited to the datastore
		/// </summary>
		public event CSPostEventHandler PostPostUpdate
		{
			add{Events.AddHandler(EventPostPostUpdate, value);}
			remove{Events.RemoveHandler(EventPostPostUpdate, value);}
		}

		/// <summary>
		/// Fires after a Post or Thread is rated
		/// </summary>
		public event CSPostEventHandler RatePost
		{
			add{Events.AddHandler(EventRatePost, value);}
			remove{Events.RemoveHandler(EventRatePost, value);}
		}

//		/// <summary>
//		/// Event raised before an individual post is rendered
//		/// </summary>
//		public event CSPostEventHandler PreRenderPost
//		{
//			add{Events.AddHandler(EventPreRenderPost, value);}
//			remove{Events.RemoveHandler(EventPreRenderPost, value);}
//		}

		#endregion

		#endregion

		#region User Events

		#region Execute Events
		
		internal void ExecuteUserValidated(User user)
		{
			ExecuteUserEvent(EventUserValidated,user);
		}

		internal void ExecuteUserKnown(User user)
		{
			ExecuteUserEvent(EventUserKnown,user);
		}

		internal void ExecutePreUserUpdate(User user, ObjectState state)
		{
			ExecuteUserEvent(EventPreUserUpdate,user,state,ApplicationType.Unknown);
		}

		internal void ExecutePostUserUpdate(User user, ObjectState state)
		{
			ExecuteUserEvent(EventPostUserUpdate,user,state,ApplicationType.Unknown);
		}

		internal void ExecuteUserRemove(User user)
		{
			ExecuteUserEvent(EventUserRemove,user,ObjectState.Delete,ApplicationType.Unknown);
		}

		protected void ExecuteUserEvent(object EventKey, User user)
		{
			ExecuteUserEvent(EventKey,user,ObjectState.None,ApplicationType.Unknown);
		}
		protected void ExecuteUserEvent(object EventKey, User user,ObjectState state, ApplicationType appType)
		{
			CSUserEventHandler handler = Events[EventKey] as CSUserEventHandler;
			if (handler != null)
			{
				handler(user, new CSEventArgs(state,appType));
			}
		}

		#endregion

		#region Events

		/// <summary>
		/// Fires after a user's credentials have been validated.
		/// </summary>
		public event CSUserEventHandler UserValidated
		{
			add{Events.AddHandler(EventUserValidated, value);}
			remove{Events.RemoveHandler(EventUserValidated, value);}
		}

		/// <summary>
		/// Fires once the current user has been identified. This user may still be anonymous.
		/// </summary>
		public event CSUserEventHandler UserKnown
		{
			add{Events.AddHandler(EventUserKnown, value);}
			remove{Events.RemoveHandler(EventUserKnown, value);}
		}

		/// <summary>
		/// Fires before a User is saved/updated to the datastore
		/// </summary>
		public event CSUserEventHandler PreUserUpdate
		{
			add{Events.AddHandler(EventPreUserUpdate, value);}
			remove{Events.RemoveHandler(EventPreUserUpdate, value);}
		}

		/// <summary>
		/// Fires after a User is saved/updated to the datastore
		/// </summary>
		public event CSUserEventHandler PostUserUpdate
		{
			add{Events.AddHandler(EventPostUserUpdate, value);}
			remove{Events.RemoveHandler(EventPostUserUpdate, value);}
		}

		/// <summary>
		/// Fires before a User is removed from the datastore.
		/// </summary>
		public event CSUserEventHandler UserRemove
		{
			add{Events.AddHandler(EventUserRemove, value);}
			remove{Events.RemoveHandler(EventUserRemove, value);}
		}

		#endregion

		#endregion

		#region Section Events

		internal void ExecutePreSectionUpdate(Section section, ObjectState state, ApplicationType appType)
		{
			CSSectionEventHandler handler = Events[EventPreSectionUpdate] as CSSectionEventHandler;
			if (handler != null)
			{
				handler(section, new CSEventArgs(state,appType));
			}
		}

		internal void ExecutePostSectionUpdate(Section section, ObjectState state, ApplicationType appType)
		{
			CSSectionEventHandler handler = Events[EventPostSectionUpdate] as CSSectionEventHandler;
			if (handler != null)
			{
				handler(section, new CSEventArgs(state,appType));
			}
		}


		/// <summary>
		/// Event raised before a section change is committed to the datastore (create/update)
		/// </summary>
		public event CSSectionEventHandler PreSectionUpdate
		{
			add{Events.AddHandler(EventPreSectionUpdate, value);}
			remove{Events.RemoveHandler(EventPreSectionUpdate, value);}
		}


		/// <summary>
		/// Event raised after a section chage is committed to the data store
		/// </summary>
		public event CSSectionEventHandler PostSectionUpdate
		{
			add{Events.AddHandler(EventPostSectionUpdate, value);}
			remove{Events.RemoveHandler(EventPostSectionUpdate, value);}
		}

		#endregion

		#region Group Events

		internal void ExecutePreSectionGroupUpdate(Group group, ObjectState state, ApplicationType appType)
		{
			CSGroupEventHandler handler = Events[EventPreSectionGroupUpdate] as CSGroupEventHandler;
			if (handler != null)
			{
				handler(group, new CSEventArgs(state,appType));
			}
		}

		internal void ExecutePostSectionGroupUpdate(Group group, ObjectState state, ApplicationType appType)
		{
			CSGroupEventHandler handler = Events[EventPostSectionGroupUpdate] as CSGroupEventHandler;
			if (handler != null)
			{
				handler(group, new CSEventArgs(state,appType));
			}
		}

		/// <summary>
		/// Event raised before a group chage is committed to the datastore (create/update)
		/// </summary>
		public event CSGroupEventHandler PreSectionGroupUpdate
		{
			add{Events.AddHandler(EventPreSectionGroupUpdate, value);}
			remove{Events.RemoveHandler(EventPreSectionGroupUpdate, value);}
		}

		/// <summary>
		/// Event raised after a group chage is committed to the data store
		/// </summary>
		public event CSGroupEventHandler PostSectionGroupUpdate
		{
			add{Events.AddHandler(EventPostSectionGroupUpdate, value);}
			remove{Events.RemoveHandler(EventPostSectionGroupUpdate, value);}
		}

		#endregion

		#region Exceptions
		/// <summary>
		/// Event raised before a group chage is committed to the datastore (create/update)
		/// </summary>
		public event CSExceptionHandler CSException
		{
			add{Events.AddHandler(EventUnhandledException, value);}
			remove{Events.RemoveHandler(EventUnhandledException, value);}
		}

		internal void ExecuteCSExcetion(CSException csEx)
		{
			CSExceptionHandler handler = Events[EventUnhandledException] as CSExceptionHandler;
			if (handler != null)
			{
				handler(csEx,new CSEventArgs());
			}
		}

		#endregion

	}
}
