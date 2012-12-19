using System;
using System.Diagnostics;
using System.Threading;
using System.Web;
using System.Xml;
using System.Text.RegularExpressions;
using System.IO;

using GPRP.GPRPComponents;
using GPRP.GPRPEnumerations;

namespace GPRP.GPRPBussiness
{
	/// <summary>
	/// HttpModule类

	/// </summary>
	public class HttpModule : System.Web.IHttpModule
	{
		//public readonly static Mutex m=new Mutex();

		/// <summary>
		/// 实现接口的Init方法
		/// </summary>
		/// <param name="context"></param>
		public void Init(HttpApplication context)
		{
			//OnlineUsers.ResetOnlineList();
			context.BeginRequest += new EventHandler(ReUrl_BeginRequest);
            //context.AuthenticateRequest += new EventHandler(Application_AuthenticateRequest);
            //context.Error += new EventHandler(this.Application_OnError);
            //context.AuthorizeRequest += new EventHandler(this.Application_AuthorizeRequest);
         
		}

       

		public void Application_OnError(Object sender , EventArgs e)
		{
            //HttpApplication application = (HttpApplication)sender; 
            //HttpContext context = application.Context; 
            ////if (context.Server.GetLastError().GetBaseException() is MyException)
            //{
            //    //MyException ex = (MyException) context.Server.GetLastError().GetBaseException();
            //    context.Response.Write("<html><body style=\"font-size:14px;\">");
            //    context.Response.Write("Error:<br />");
            //    context.Response.Write("<textarea name=\"errormessage\" style=\"width:80%; height:200px; word-break:break-all\">");
            //    context.Response.Write(System.Web.HttpUtility.HtmlEncode(context.Server.GetLastError().ToString()));
            //    context.Response.Write("</textarea>");
            //    context.Response.Write("</body></html>");
            //    context.Response.End();
            //}
            HttpApplication application = (HttpApplication)sender;
            HttpContext context = application.Context;

            CSException csException = context.Server.GetLastError() as CSException;

            if (csException == null)
                csException = context.Server.GetLastError().GetBaseException() as CSException;

            try
            {
                if (csException != null)
                {
                    switch (csException.ExceptionType)
                    {
                        case CSExceptionType.UserInvalidCredentials:
                        case CSExceptionType.AccessDenied:
                        case CSExceptionType.AdministrationAccessDenied:
                        case CSExceptionType.ModerateAccessDenied:
                        case CSExceptionType.PostDeleteAccessDenied:
                        case CSExceptionType.PostProblem:
                        case CSExceptionType.UserAccountBanned:
                        case CSExceptionType.ResourceNotFound:
                        case CSExceptionType.UserUnknownLoginError:
                        case CSExceptionType.SectionNotFound:
                            csException.Log();
                            break;
                    }
                }
                else
                {
                    Exception ex = context.Server.GetLastError();
                    if (ex.InnerException != null)
                        ex = ex.InnerException;

                    csException = new CSException(CSExceptionType.UnknownError, ex.Message, context.Server.GetLastError());

                    System.Data.SqlClient.SqlException sqlEx = ex as System.Data.SqlClient.SqlException;
                    if (sqlEx == null || sqlEx.Number != -2) //don't log time outs
                        csException.Log();
                }
            }
            catch { } //not much to do here, but we want to prevent infinite looping with our error handles

            CSEvents.CSException(csException);
		}
        #region Application AuthenticateRequest

        private void Application_AuthenticateRequest(Object source, EventArgs e)
        {
            HttpContext context = HttpContext.Current;
            Provider p = null;
            ExtensionModule module = null;

            // If the installer is making the request terminate early
            if (CSConfiguration.GetConfig().AppLocation.CurrentApplicationType == ApplicationType.Installer)
            {
                return;
            }

            // Only continue if we have a valid context
            //
            if ((context == null) || (context.User == null))
                return;

            try
            {
                // Logic to handle various authentication types
                //
                switch (context.User.Identity.GetType().Name.ToLower())
                {

                    // Microsoft passport
                    case "passportidentity":
                        p = (Provider)CSConfiguration.GetConfig().Extensions["PassportAuthentication"];
                        module = ExtensionModule.Instance(p);
                        if (module != null)
                            module.ProcessRequest();
                        else
                            goto default;
                        break;

                    // Windows
                    case "windowsidentity":
                        p = (Provider)CSConfiguration.GetConfig().Extensions["WindowsAuthentication"];
                        module = ExtensionModule.Instance(p);
                        if (module != null)
                            module.ProcessRequest();
                        else
                            goto default;
                        break;

                    // Forms
                    case "formsidentity":
                        p = (Provider)CSConfiguration.GetConfig().Extensions["FormsAuthentication"];
                        module = ExtensionModule.Instance(p);
                        if (module != null)
                            module.ProcessRequest();
                        else
                            goto default;
                        break;

                    // Custom
                    case "customidentity":
                        p = (Provider)CSConfiguration.GetConfig().Extensions["CustomAuthentication"];
                        module = ExtensionModule.Instance(p);
                        if (module != null)
                            module.ProcessRequest();
                        else
                            goto default;
                        break;

                    default:
                        CSContext.Current.UserName = context.User.Identity.Name;
                        break;

                }

            }
            catch (Exception ex)
            {
                CSException forumEx = new CSException(CSExceptionType.UnknownError, "Error in AuthenticateRequest", ex);
                forumEx.Log();

                throw forumEx;
            }

            //			// Get the roles the user belongs to
            //			//
            //			Roles roles = new Roles();
            //			roles.GetUserRoles();
        }
        #endregion

        #region Application AuthorizeRequest
        private void Application_AuthorizeRequest(Object source, EventArgs e)
        {


            if (CSConfiguration.GetConfig().AppLocation.CurrentApplicationType == ApplicationType.Installer)
            {
                //CSContext.Create(context);
                return;
            }


            HttpApplication application = (HttpApplication)source;
            HttpContext context = application.Context;

            CSContext csContext = CSContext.Current;
          
      
            //CSEvents.UserKnown(csContext.User);

            //ValidateApplicationStatus(csContext);

        
            //// Do we need to force the user to login?
            ////

            //if (context.Request.IsAuthenticated)
            //{
            //    string username = context.User.Identity.Name;
            //    if (username != null)
            //    {
            //        string[] roles = Roles.GetUserRoleNames(username);
            //        if (roles != null && roles.Length > 0)
            //        {
            //            csContext.RolesCacheKey = string.Join(",", roles);
            //        }
            //    }
            //}
        }
        private void ValidateApplicationStatus(CSContext cntx)
        {
            if (!cntx.User.IsAdministrator)
            {
                string disablePath = null;
                switch (cntx.Config.AppLocation.CurrentApplicationType)
                {
                    case ApplicationType.Forum:
                        if (cntx.SiteSettings.ForumsDisabled)
                            disablePath = "ForumsDisabled.htm";
                        break;
                    case ApplicationType.Weblog:
                        if (cntx.SiteSettings.BlogsDisabled)
                            disablePath = "BlogsDisabled.htm";
                        break;
                    case ApplicationType.Gallery:
                        if (cntx.SiteSettings.GalleriesDisabled)
                            disablePath = "GalleriesDisabled.htm";
                        break;
                    case ApplicationType.GuestBook:
                        if (cntx.SiteSettings.GuestBookDisabled)
                            disablePath = "GuestBookDisabled.htm";
                        break;
                }

                if (disablePath != null)
                {

                    string errorpath = cntx.Context.Server.MapPath(string.Format("~/Languages/{0}/errors/{1}", cntx.Config.DefaultLanguage, disablePath));
                    using (StreamReader reader = new StreamReader(errorpath))
                    {
                        string html = reader.ReadToEnd();
                        reader.Close();

                        cntx.Context.Response.Write(html);
                        cntx.Context.Response.End();
                    }
                }
            }
        }

        #endregion
		/// <summary>
		/// 实现接口的Dispose方法
		/// </summary>
		public void Dispose()
		{
		}

		
		/// <summary>
		/// 重写Url
		/// </summary>
		/// <param name="sender">事件的源</param>
		/// <param name="e">包含事件数据的 EventArgs</param>
		private void ReUrl_BeginRequest(object sender, EventArgs e)
		{
            BaseConfigInfo baseconfig = BaseConfigProvider.Instance();
            if (baseconfig == null)
            {
                return;
            }
            GeneralConfigInfo config = GeneralConfigs.GetConfig();
            HttpContext context = ((HttpApplication)sender).Context;
            string forumPath = baseconfig.Forumpath.ToLower();

            string requestPath = context.Request.Path.ToLower();


            //CSConfiguration csconfig = CSConfiguration.GetConfig();

            // // If the installer is making the request terminate early
            //if (csconfig.AppLocation.CurrentApplicationType == ApplicationType.Installer)
            // {
            //     //CSContext.Create(context);
            //     return;
            // }


            //CSContext.Create(context);

            if (requestPath.StartsWith(forumPath))
            {
                if (requestPath.Substring(forumPath.Length).IndexOf("/") == -1)
                {
                    //// 当前样式id
                    //string strTemplateid = config.Templateid.ToString();
                    //if (Utils.InArray(Utils.GetCookie(Utils.GetTemplateCookieName()), Templates.GetValidTemplateIDList()))
                    //{
                    //    strTemplateid = Utils.GetCookie(Utils.GetTemplateCookieName());
                    //}

                    if (requestPath.EndsWith("/index.aspx"))
                    {
                        context.RewritePath(forumPath + "aspx/index.aspx");
                        return;
                    }

                    context.RewritePath(forumPath + "aspx/" + requestPath.Substring(context.Request.Path.LastIndexOf("/")), string.Empty, context.Request.QueryString.ToString());
                }


                else if (requestPath.StartsWith(forumPath + "tools/"))
                {
                    //当使用伪aspx, 如:showforum-1.aspx等.
                    if (config.Aspxrewrite == 1)
                    {
                        string path = requestPath.Substring(forumPath.Length + 5);
                        foreach (SiteUrls.URLRewrite url in SiteUrls.GetSiteUrls().Urls)
                        {
                            if (Regex.IsMatch(path, url.Pattern, Utils.GetRegexCompiledOptions() | RegexOptions.IgnoreCase))
                            {
                                string newUrl = Regex.Replace(path, url.Pattern, Utils.UrlDecode(url.QueryString), Utils.GetRegexCompiledOptions() | RegexOptions.IgnoreCase);

                                context.RewritePath(forumPath + "tools" + url.Page, string.Empty, newUrl);
                                return;
                            }
                        }
                    }
                    return;
                }

            }
              
		
		}

   
	}


    
	//////////////////////////////////////////////////////////////////////
	/// <summary>
	/// 站点伪Url信息类

	/// </summary>
	public class SiteUrls
	{
		#region 内部属性和方法
		private static object lockHelper = new object();
		private static volatile SiteUrls instance = null;

		string SiteUrlsFile = HttpContext.Current.Server.MapPath(BaseConfigs.GetForumPath + "config/urls.config");
		private System.Collections.ArrayList _Urls;
		public System.Collections.ArrayList Urls
		{
			get
			{
				return _Urls;
			}
			set
			{
				_Urls = value;
			}
		}

		private System.Collections.Specialized.NameValueCollection _Paths;
		public System.Collections.Specialized.NameValueCollection Paths
		{
			get
			{
				return _Paths;
			}
			set
			{
				_Paths = value;
			}
		}
  
		private SiteUrls()
		{
			Urls = new System.Collections.ArrayList();
			Paths = new System.Collections.Specialized.NameValueCollection();

			XmlDocument xml = new XmlDocument();

			xml.Load(SiteUrlsFile);

			XmlNode root = xml.SelectSingleNode("urls");
			foreach(XmlNode n in root.ChildNodes)
			{
				if (n.NodeType != XmlNodeType.Comment && n.Name.ToLower() == "rewrite")
				{
					XmlAttribute name = n.Attributes["name"];
					XmlAttribute path = n.Attributes["path"];
					XmlAttribute page = n.Attributes["page"];
					XmlAttribute querystring = n.Attributes["querystring"];
					XmlAttribute pattern = n.Attributes["pattern"];

					if (name != null && path != null && page != null && querystring != null && pattern != null)
					{
						Paths.Add(name.Value, path.Value);
						Urls.Add(new URLRewrite(name.Value, pattern.Value, page.Value.Replace("^", "&"), querystring.Value.Replace("^", "&")));
					}
				}
			}
		}
		#endregion

		public static SiteUrls GetSiteUrls()
		{
			if (instance == null)
			{
				lock (lockHelper)
				{
					if (instance == null)
					{
						instance = new SiteUrls();
					}
				}
			}
			return instance;

		}

		public static void SetInstance(SiteUrls anInstance)
		{
			if (anInstance != null)
				instance = anInstance;
		}

		public static void SetInstance()
		{
			SetInstance(new SiteUrls());
		}


		/// <summary>
		/// 重写伪地址
		/// </summary>
		public class URLRewrite
		{
			#region 成员变量
			private string _Name;
			public string Name
			{
				get
				{
					return _Name;
				}
				set
				{
					_Name = value;
				}
			}

			private string _Pattern;
			public string Pattern
			{
				get
				{
					return _Pattern;
				}
				set
				{
					_Pattern = value;
				}
			}

			private string _Page;
			public string Page
			{
				get
				{
					return _Page;
				}
				set
				{
					_Page = value;
				}
			}

			private string _QueryString;
			public string QueryString
			{
				get
				{
					return _QueryString;
				}
				set
				{
					_QueryString = value;
				}
			}
			#endregion 

			#region 构造函数

			public URLRewrite(string name, string pattern, string page, string querystring)
			{
				_Name = name;
				_Pattern = pattern;
				_Page = page;
				_QueryString = querystring;
			}
			#endregion
		}

	}

}
