//------------------------------------------------------------------------------
// <copyright company="Telligent Systems">
//     Copyright (c) Telligent Systems Corporation.  All rights reserved.
// </copyright> 
//------------------------------------------------------------------------------

using System;
using System.Collections.Specialized;
using System.Configuration;
using System.IO;
using System.Threading;
using System.Web;
using System.Collections;

using GPRP.GPRPEnumerations;
namespace GPRP.GPRPComponents
{

	/// <summary>
	/// The CSContext represents common properties and settings used through out of a Request. All data stored
	/// in the context will be cleared at the end of the request
	/// 
	/// ThreadSafety has not been tested.
	/// 
	/// This object should be safe to use outside of a web request, but querystring and other values should be prepopulated
	/// </summary>
	public class CSContext 
	{

		#region Private Containers

		//Generally expect 10 or less items
		private HybridDictionary _items = new HybridDictionary();
		private NameValueCollection _queryString = null;
		private string _siteUrl = null;
		private Uri _currentUri;

		string rolesCacheKey = null;

		string authenticationType = "forms";
        
		bool _isUrlReWritten = false;
		string _rawUrl;

		HttpContext _httpContext = null;
		DateTime requestStartTime = DateTime.Now;
		
		#endregion

		#region Initialize  and cnstr.'s

		/// <summary>
		/// Create/Instatiate items that will vary based on where this object 
		/// is first created
		/// 
		/// We could wire up Path, encoding, etc as necessary
		/// </summary>
		private void Initialize(NameValueCollection qs, Uri uri, string rawUrl, string siteUrl)
		{
			_queryString = qs;
			_siteUrl = siteUrl;
			_currentUri = uri;
			_rawUrl = rawUrl;
			
		}

		/// <summary>
		/// cntr called when no HttpContext is available
		/// </summary>
		private CSContext(Uri uri, string siteUrl)
		{
			Initialize(new NameValueCollection(), uri, uri.ToString(), siteUrl);
		}

		/// <summary>
		/// cnst caled when HttpContext is avaiable
		/// </summary>
		/// <param name="context"></param>
		private CSContext(HttpContext context)
		{
			this._httpContext = context;
			Initialize(new NameValueCollection(context.Request.QueryString), context.Request.Url, context.Request.RawUrl, GetSiteUrl());
		}
        private CSContext(HttpContext context, bool includeQS)
        {
            this._httpContext = context;

           if(includeQS)
            {
                    Initialize(new NameValueCollection(context.Request.QueryString), context.Request.Url, context.Request.RawUrl, GetSiteUrl());
            }
        }

		#endregion

		#region Create


		/// <summary>
		/// Creates a Context instance based on HttpContext. Generally, this
		/// method should be called via Begin_Request in an HttpModule
		/// </summary>
		public static CSContext Create(HttpContext context)
		{
			return Create(context,false);
		}

		/// <summary>
		/// Creates a Context instance based on HttpContext. Generally, this
		/// method should be called via Begin_Request in an HttpModule
		/// </summary>
		public static CSContext Create(HttpContext context, bool isReWritten)
		{
			
			CSContext csContext = new CSContext(context);
			csContext.IsUrlReWritten = isReWritten;
			SaveContextToStore(GetSlot(),csContext);

			return csContext;
		}

		/// <summary>
		/// Creates a Context instance based. If the HttpContext is available it will be used.
		/// Generally this method should be used when working with CS outside of a valid Web Request
		/// </summary>
		public static CSContext Create(Uri uri, string appName)
		{
			CSContext csContext = new CSContext(uri,appName);
			SaveContextToStore(GetSlot(),csContext);
			return csContext;
		}
		#endregion

		#region Core Properties
		/// <summary>
		/// Simulates Context.Items and provides a per request/instance storage bag
		/// </summary>
		public IDictionary Items
		{
			get{ return _items;}
		}

		/// <summary>
		/// Provides direct access to the .Items property
		/// </summary>
		public object this[string key]
		{
			get
			{
				return this.Items[key];
			}
			set
			{
				this.Items[key] = value;
			}
		}

		/// <summary>
		/// Quick reference to our internal cache api
		/// </summary>
		public CSCache Cache;

		/// <summary>
		/// Allows access to QueryString values
		/// </summary>
		public NameValueCollection QueryString
		{
			get{return _queryString;}
		}

		/// <summary>
		/// Quick check to see if we have a valid web reqeust
		/// </summary>
		public bool IsWebRequest
		{
			get{ return this.Context != null;}
		}

		public bool IsAuthenticated
		{
			get { return !User.IsAnonymous;}	
		}

		public string AuthenticationType
		{
			get { return authenticationType; }
			set { authenticationType = value.ToLower(); }
		}

		public HttpContext Context 
		{ 
			get 
			{ 
				return _httpContext;
			} 
		}

		public string SiteUrl
		{
			get { return _siteUrl; }
		}

		#endregion

		#region Helpers
		// *********************************************************************
		//  GetGuidFromQueryString
		//
		/// <summary>
		/// Retrieves a value from the query string and returns it as an int.
		/// </summary>
		// ***********************************************************************/
		public Guid GetGuidFromQueryString(string key) 
		{
			Guid returnValue = Guid.Empty;
			string queryStringValue;

			// Attempt to get the value from the query string
			//
			queryStringValue = QueryString[key];

			// If we didn't find anything, just return
			//
			if (queryStringValue == null)
				return returnValue;

			// Found a value, attempt to conver to integer
			//
			try 
			{

				// Special case if we find a # in the value
				//
				if (queryStringValue.IndexOf("#") > 0)
					queryStringValue = queryStringValue.Substring(0, queryStringValue.IndexOf("#"));

				returnValue = new Guid(queryStringValue);
			} 
			catch {}

			return returnValue;

		}

		// *********************************************************************
		//  GetIntFromQueryString
		//
		/// <summary>
		/// Retrieves a value from the query string and returns it as an int.
		/// </summary>
		// ***********************************************************************/
		public int GetIntFromQueryString(string key, int defaultValue) 
		{
			string queryStringValue;


			// Attempt to get the value from the query string
			//
			queryStringValue = this.QueryString[key];

			// If we didn't find anything, just return
			//
			if (queryStringValue == null)
				return defaultValue;

			// Found a value, attempt to conver to integer
			//
			try 
			{

				// Special case if we find a # in the value
				//
				if (queryStringValue.IndexOf("#") > 0)
					queryStringValue = queryStringValue.Substring(0, queryStringValue.IndexOf("#"));

				defaultValue = Convert.ToInt32(queryStringValue);
			} 
			catch {}

			return defaultValue;

		}

		public string MapPath(string path)
		{
			if(_httpContext != null)
				return _httpContext.Server.MapPath(path);
			else
				return Directory.GetCurrentDirectory() + path.Replace("/", @"\").Replace("~", "");
		}

		public string PhysicalPath(string path)
		{
			return RootPath() + path;
		}

		private string _rootPath = null;

		protected string RootPath()
		{
			if(_rootPath == null)
			{
				_rootPath = AppDomain.CurrentDomain.BaseDirectory;
				string dirSep = Path.DirectorySeparatorChar.ToString();

				_rootPath = _rootPath.Replace("/", dirSep);

				string filePath = Config.FilesPath;
				
				if(filePath != null)
				{
					filePath = filePath.Replace("/", dirSep);

					if(filePath.Length > 1 &&  filePath.StartsWith(dirSep) && _rootPath.EndsWith(dirSep))
					{
						_rootPath = _rootPath + filePath.Substring(1);
					}
					else
					{
						_rootPath = _rootPath + filePath;
					}
				}
			}
				return _rootPath;
		}

		private string GetSiteUrl() 
		{
			string appOverride = this.Config.ApplicationOverride;
			if(appOverride != null)
				return appOverride;

			//NOTE: Watch this change. Should be safe, but not tested.
			//Virtualization means urls must be very precise.
			string hostName = _httpContext.Request.Url.Host.Replace("www.",string.Empty);
			string applicationPath = _httpContext.Request.ApplicationPath;

			if(applicationPath.EndsWith("/"))
				applicationPath = applicationPath.Remove(applicationPath.Length-1,1);

			return  hostName + applicationPath;
				
		}
		#endregion

		#region CS Data
		private User _currentUser = null;
		private SiteSettings _currentSettings = null;
		private CSConfiguration _config = null;
		private SiteStatistics _stats = null;
		private Section _section = null;
		private Group _group = null;
		private Post _post = null;

		public User User
		{
			get
			{
				if(_currentUser == null)
					_currentUser= Users.GetUser(true);

				return _currentUser;
			}
			set { _currentUser = value;}
		}

		public SiteSettings SiteSettings
		{
			get
			{
				if(_currentSettings == null)
					_currentSettings = SiteSettingsManager.GetSiteSettings(this.SiteUrl);

				return _currentSettings;
			}
			set { _currentSettings = value;}
		}

		public CSConfiguration Config
		{
			get
			{
				if(_config == null)
					_config = CSConfiguration.GetConfig();

				return _config;
			}		
		}

		public SiteStatistics Statistics 
		{
			get
			{	
				if(_stats == null)
					_stats = SiteStatistics.LoadSiteStatistics(this.SiteSettings.SettingsID,true,3);

				return _stats;

			}
		}

		public Post Post
		{
			get{ return _post;}
			set {_post = value;}
		}

		public Section Section
		{
			get{ return _section;}
			set {_section = value;}
		}

		public Group Group
		{
			get{ return _group;}
			set {_group = value;}
		}

		#endregion

		#region Status Properties
		public DateTime RequestStartTime { get { return requestStartTime; } }
		public string RolesCacheKey { get { return rolesCacheKey; } set { rolesCacheKey = value; } }
		public bool IsUrlReWritten { get { return _isUrlReWritten; } set { _isUrlReWritten = value; } }
		public string RawUrl { get { return _rawUrl; } set { _rawUrl = value; } }
		public ApplicationType ApplicationType { get {return Config.AppLocation.CurrentApplicationType;}}
		public Uri CurrentUri {get {return _currentUri;} set {_currentUri = value;}}
		private string _hostPath = null;
		public string HostPath
		{
			get
			{
				if(_hostPath == null)
				{
					string portInfo = CurrentUri.Port == 80 ? string.Empty : ":" + CurrentUri.Port.ToString();
					_hostPath = string.Format("{0}://{1}{2}",CurrentUri.Scheme,CurrentUri.Host, portInfo);
				}
				return _hostPath;
			}
		}
		#endregion

		#region Common QueryString Properties

		#region Private Members

		int forumID =       -2;
		int categoryID =	-2;
		int messageID =     -2;
		int forumGroupID =  -2;
		int postID =        -2;
		int threadID =      -2;
		int userID =        -2;
		string userName =   "";
		int pageIndex =     -2;
		int blogGroupID =   -2;
		Guid roleID =        Guid.Empty;
		string queryText =  null;
		string returnUrl =  null;
		string appKey = null;
		string url = null;
		string args = null;
        string language = null;
		#endregion

		public int MessageID 
		{
			get
			{
				if(messageID == -2)
					messageID = this.GetIntFromQueryString("MessageID", -1);

				return messageID;
			} 
			set {messageID = value;}
		}

		public int ForumID 
		{
			get
			{
				if(forumID == -2)
					forumID = this.GetIntFromQueryString("ForumID", -1);

				return forumID;
			} 
			set {forumID = value;}
		}

		public int ForumGroupID 
		{
			get
			{
				if(forumGroupID == -2)
					forumGroupID = this.GetIntFromQueryString("ForumGroupID", -1);

				return forumGroupID;
			} 
			set {forumGroupID = value;}
		}


		public int CategoryID 
		{
			get
			{
				if(categoryID == -2)
					categoryID = this.GetIntFromQueryString("CategoryID", -1);

				return categoryID;
			} 
			set {categoryID = value;}
		}

		public int BlogGroupID 
		{
			get
			{
				if(blogGroupID == -2)
					blogGroupID = this.GetIntFromQueryString("BlogGroupID", -1);

				return blogGroupID;
			} 
			set {blogGroupID = value;}
		}


		public int PostID 
		{
			get
			{
				if(postID == -2)
					postID = this.GetIntFromQueryString("PostID", -1);

				return postID;
			} 
			set {postID = value;}
		}

		public int ThreadID 
		{
			get
			{
				if(threadID == -2)
					threadID = this.GetIntFromQueryString("ThreadID", -1);

				return threadID;
			} 
			set {threadID = value;}
		}

		public int UserID 
		{
			get
			{
				if(userID == -2)
					userID = this.GetIntFromQueryString("UserID", -1);

				return userID;
			} 
			set {userID = value;}
		}

		public string UserName 
		{
			get
			{
				if(userName == null)
				{
					userName = QueryString["UserName"];
				}

				return userName;
			}
			set
			{
				userName = value;
			}
		}

		public Guid RoleID 
		{
			get
			{
				if(roleID == Guid.Empty)
					roleID = GetGuidFromQueryString("RoleID");

				return roleID;
			} 
			set {roleID = value;}
		}



		public string QueryText
		{
			get
			{
				if(queryText == null)
					queryText = QueryString["q"];

				return queryText;
			}
			set {queryText = value;}
		}

		public string ReturnUrl
		{
			get
			{
				if(returnUrl == null)
					returnUrl = QueryString["returnUrl"];

				return returnUrl;
			}
			set {returnUrl = value;}
		}

		public string Url
		{
			get
			{
				if(url == null)
					url = QueryString["url"];

				return url;
			}
			set {url = value;}
		}

		public string Args
		{
			get
			{
				if(args == null)
					args = QueryString["args"];

				return args;
			}
			set {args = value;}
		}

		public int PageIndex 
		{
			get
			{
				if(pageIndex == -2)
				{
					pageIndex = this.GetIntFromQueryString("PageIndex", -1);
					pageIndex = pageIndex - 1;
				}
				return pageIndex;
			} 
			set {pageIndex = value;}
		}


		public string ApplicationKey
		{
			get
			{
				if(appKey == null)
				{
					appKey= Config.ApplicationKeyOverride;
					if(Globals.IsNullorEmpty(appKey))
						appKey = Globals.UrlEncode(QueryString["App"]);
				}
				return appKey;
			}
			set {appKey = value;}
		}

        public string Language
        {
            get {
                if (language == null)
                    language = Config.DefaultLanguage;
                return language; 
            }
            set { language = value; }
        }
		#endregion

		#region State
        private static readonly string dataKey = "CSContextStore";
		public static CSContext Current 
		{
            
            get{
        
                HttpContext httpContext=HttpContext.Current;
                CSContext context = null;
                if (httpContext != null)
                { context = httpContext.Items[dataKey] as CSContext; }
                else
                {
                    context = Thread.GetData(GetSlot()) as CSContext;
                }
                if (context == null)
                {
                    if (httpContext == null)
                        throw new Exception("No CSContext exists in the Current Application. AutoCreate fails since HttpContext.Current is not accessible");

                    context = new CSContext(httpContext, true);
                    SaveContextToStore(context);
                }
                return context;
                }
        }

		private static LocalDataStoreSlot GetSlot()
		{
            return Thread.GetNamedDataSlot(dataKey);
		}

		private static void SaveContextToStore(LocalDataStoreSlot storeSlot, CSContext context)
		{
			Thread.SetData(storeSlot, context);
		}
         /**//*CsContextS数据存入指定名称数据槽中*/
        private static void SaveContextToStore(CSContext context)
        {
            if(context.IsWebRequest)
            {
                context.Context.Items[dataKey] = context;
            }
            else
            {
                Thread.SetData(GetSlot(), context);    
            }
            
        }
        /**//*释放线程指定数据槽中数据*/

        public static void Unload()
        {
            Thread.FreeNamedDataSlot(dataKey);
        }

		#endregion
	}
}
