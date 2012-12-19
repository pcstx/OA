//------------------------------------------------------------------------------
// <copyright company="Telligent Systems">
//     Copyright (c) Telligent Systems Corporation.  All rights reserved.
// </copyright> 
//------------------------------------------------------------------------------

// 修改说明：新增获取IP方法
// 修改人：宝玉
// 修改日期：2005-02-26

using System;
using System.Collections;
using System.Collections.Specialized;
using System.Configuration;
using System.Web;
using System.Globalization;
using GPRP.GPRPEnumerations;

namespace GPRP.GPRPComponents {

    public class Globals {

        // the HTML newline character
        public const String HtmlNewLine = "<br />";
        public const String _appSettingsPrefix = "AspNetForumsSettings.";

		public static void ValidateSecureConnection(HttpContext context)
		{
			//Validate we have a secure connection as early as possible
			if(CSConfiguration.GetConfig().RequireSLL && !context.Request.IsSecureConnection)
			{
				Uri url = context.Request.Url;
				context.Response.Redirect("https://" + url.ToString().Substring(7));
				context.Response.End();
			}
		}

		/// <summary>
		/// Will return false if no Url querystring value can be found
		/// </summary>
		/// <returns></returns>
		public static bool RedirectSiteUrl()
		{

			CSContext cntx = CSContext.Current;
			if(cntx.Url != null)
			{
				RedirectSiteUrl( cntx.Url, cntx.Args );
			}

			return false;
		}
		public static void RedirectSiteUrl(string path) { RedirectSiteUrl( path, null ); }

		public static void RedirectSiteUrl(string path, string args)
		{
			CSContext cntx = CSContext.Current;

			if(cntx.IsWebRequest)
			{
				try
				{
					if(args == null)
						cntx.Context.Response.Redirect(Globals.GetSiteUrls().RawPath(path));
					else
						cntx.Context.Response.Redirect(string.Format(Globals.GetSiteUrls().RawPath(path), args.Split(',')), true);
				}
				catch { }
			}
			throw new CSException(CSExceptionType.RedirectFailure);
		}

        #region FormatSignature
        public static string FormatSignature (string userSignature) 
        {
            if ( userSignature != null && userSignature.Length > 0 )
                return "<hr size=\"1\" align=\"left\" width=\"25%\">" + userSignature.Replace("\r\n", "<br>");
			else
				return String.Empty;
        }
        #endregion

        #region Trace
        public static void Trace(string category, string message) 
        {

            if (!CSContext.Current.SiteSettings.EnableDebugMode)
                return;

            HttpContext context = HttpContext.Current;
            Stack debugStack;

            // Do we have the debug stack?
            //
            if (context.Items["DebugTrace"] == null)
                debugStack = new Stack();
            else
                debugStack = (Stack) context.Items["DebugTrace"];

            // Add a new item
            //
            debugStack.Push(new DebugTrace(category, message));

            // Add item to general ASP.NET trace
            //
            context.Trace.Write(category, message);

        }

        public class DebugTrace {
            DateTime time;
            string category;
            string message;

            public DebugTrace(string category, string message) {
                this.category = category;
                this.message = message;
                time = DateTime.Now;
            }

            public DateTime Time { get { return time; } }
            public string Category { get { return category; } }
            public string Message { get { return message; } }

        }

        #endregion

        #region Encode/Decode
        /// <summary>
        /// Converts a prepared subject line back into a raw text subject line.
        /// </summary>
        /// <param name="textToFormat">The prepared subject line.</param>
        /// <returns>A raw text subject line.</returns>
        /// <remarks>This function is only needed when editing an existing message or when replying to
        /// a message - it turns the HTML escaped characters back into their pre-escaped status.</remarks>
        public static string HtmlDecode(String textToFormat) 
        {   
            if(IsNullorEmpty(textToFormat))
                return textToFormat;
            
            //ScottW: Removed Context dependency
            return System.Web.HttpUtility.HtmlDecode(textToFormat);
            // strip the HTML - i.e., turn < into &lt;, > into &gt;
            //return HttpContext.Current.Server.HtmlDecode(FormattedMessageSubject);
        } 

        /// <summary>
        /// Converts a prepared subject line back into a raw text subject line.
        /// </summary>
        /// <param name="textToFormat">The prepared subject line.</param>
        /// <returns>A raw text subject line.</returns>
        /// <remarks>This function is only needed when editing an existing message or when replying to
        /// a message - it turns the HTML escaped characters back into their pre-escaped status.</remarks>
        public static string HtmlEncode(String textToFormat) {       
            // strip the HTML - i.e., turn < into &lt;, > into &gt;

            if(IsNullorEmpty(textToFormat))
                return textToFormat;

            //ScottW: Removed Context dependency
            return System.Web.HttpUtility.HtmlEncode(textToFormat);
            //return HttpContext.Current.Server.HtmlEncode(FormattedMessageSubject);
        } 
        
        public static string UrlEncode(string urlToEncode)
        {
            if(IsNullorEmpty(urlToEncode))
                return urlToEncode;

            return System.Web.HttpUtility.UrlEncode(urlToEncode);
        }

		public static string UrlDecode(string urlToDecode)
		{
			if(IsNullorEmpty(urlToDecode))
				return urlToDecode;

			return System.Web.HttpUtility.UrlEncode(urlToDecode);
		}


        #endregion

        #region DefaultForumView
        /************ PROPERTY SET/GET STATEMENTS **************/
        /// <summary>
        /// Returns the default view to use for viewing the forum posts, as specified in the AspNetForumsSettings
        /// section of Web.config.
        /// </summary>
        static public int DefaultForumView 
        {
            get {
                const int _defaultForumView = 2;
                const String _settingName = "defaultForumView";

                String _str = CSCache.Get("webForums." + _settingName) as String;
                int iValue = _defaultForumView;
                if (_str == null) {
                    // we need to get the string and place it in the cache
                    String prefix = "";
                    NameValueCollection context = (NameValueCollection)ConfigurationSettings.GetConfig("AspNetForumsSettings");
                    if (context == null) {
                        // get the appSettings context
                        prefix = Globals._appSettingsPrefix;;
                        context = (NameValueCollection)ConfigurationSettings.GetConfig("appSettings");
                    }

                    _str = context[prefix + _settingName];

                    // determine what forum view to show
                    if (_str == null)
                        // choose the default
                        iValue = _defaultForumView;
                    else
                        switch(_str.ToUpper()) {
                            case "THREADED":
                                iValue = 2;
                                break;

                            case "MIXED":
                                iValue = 1;
                                break;

                            case "FLAT":
                                iValue = 0;
                                break;

                            default:
                                iValue = _defaultForumView;
                                break;
                        }
                    
                    _str = iValue.ToString();
                    
                    CSCache.Insert("webForums." + _settingName, _str);
                }

                return Convert.ToInt32(_str);
            }
        }
        #endregion

        #region Skin/App Paths
        static public String GetSkinPath() 
        {

            // TODO -- Need to get the full path if the application path is not available
            try {
                if (CSConfiguration.GetConfig().FilesPath == "/")
                    return ApplicationPath + "/Themes/" + CSContext.Current.User.Theme;
                else
                    return ApplicationPath + CSConfiguration.GetConfig().FilesPath + "/Themes/" + CSContext.Current.User.Theme;
            } catch {
                return "";
            }
        }

        static public string ApplicationPath {

            get {
                string applicationPath = "/";
				
				if(HttpContext.Current != null)
					applicationPath = HttpContext.Current.Request.ApplicationPath;

                // Are we in an application?
                //
                if (applicationPath == "/") {
                    return string.Empty;
                } else {
                    return applicationPath;
                }
            }

        }

        /// <summary>
        /// Name of the skin to be applied
        /// </summary>
        static public String Skin {
            get {
                return "default";
            }
        }

        static public SiteUrls GetSiteUrls() 
        {
            return SiteUrls.Instance();
        }

        #endregion

        #region Language
        static public string Language 
        {
            get {
				return CSConfiguration.GetConfig().DefaultLanguage;
            }
        }
        #endregion

      
		/// <summary>
		/// 获取IP地址
		/// </summary>
		static public string IPAddress
		{
			get 
			{
				string ipAddress = "000.000.000.000";
				try
				{
					// 有可能是后台调用
					HttpContext context = HttpContext.Current;				
					ipAddress = GetUserIpAddress(context);
				}
				catch{}
				return ipAddress;
			}
		}

		/// <summary>
		/// 透过代理获取真实IP
		/// </summary>
		/// <param name="context"></param>
		/// <returns></returns>
		public static string GetUserIpAddress(HttpContext context)
		{
			string result = String.Empty;
			if (context == null) 
				return result;

			// 透过代理取真实IP
			result = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
			if (null == result || result == String.Empty)
				result = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
			
			if (null == result || result == String.Empty)
				result = HttpContext.Current.Request.UserHostAddress;

			return result;
		}


        #region CreateTemporaryPassword Replaced by WSHA
        /// <summary>
        /// Creates a temporary password of a specified length.
        /// </summary>
        /// <param name="length">The maximum length of the temporary password to create.</param>
        /// <returns>A temporary password less than or equal to the length specified.</returns>
//        public static String CreateTemporaryPassword(int length) 
//        {
//
//            string strTempPassword = Guid.NewGuid().ToString("N");
//
//            for(int i = 0; i < (length / 32); i++) {
//                strTempPassword += Guid.NewGuid().ToString("N");
//            }
//
//            return strTempPassword.Substring(0, length);
//        }
        #endregion

        public static int SafeInt(string text, int defaultValue)
        {
            if(!IsNullorEmpty(text))
            {
                try
                {
                    return Int32.Parse(text);
                }
                catch(Exception){}
                
            }

            return defaultValue;
        }

        public static bool IsNullorEmpty(string text)
        {
            return text == null || text.Trim() == string.Empty;
        }

        public static string HostPath(Uri uri)
        {
            string portInfo = uri.Port == 80 ? string.Empty : ":" + uri.Port.ToString();
            return string.Format("{0}://{1}{2}",uri.Scheme,uri.Host, portInfo);
        }

        public static string FullPath(string local)
        {
            return FullPath(HostPath(HttpContext.Current.Request.Url),local);
        }

        public static string FullPath(string hostPath, string local)
        {
            return hostPath + local;
        }

		public static bool ValidateApplicationKey(string appKey, out string formattedKey)
		{
            
			formattedKey = appKey.Trim().Replace(" ", "_").ToLower();

			//Should we remove these items? Or just encode them. We can change this logic 
			string[] parts = formattedKey.Split('!', '@', '#', '$', '%', '^', '&', '*', '(', ')', '[', ']', '{', '}', '<', '>', ',', '?', '\\', '/', '\'','+','=','~','`','|');
			formattedKey =  string.Join("", parts);
			formattedKey = Globals.UrlEncode(formattedKey);

            return formattedKey == appKey;
		}

		/// <summary>
		/// 获取属性值
		/// </summary>
		/// <param name="headerValue"></param>
		/// <param name="attrName"></param>
		/// <returns></returns>
		public static string GetAttributeFromHeader(string headerValue, string attrName)
		{
			int attrLen;
			if (headerValue == null)
			{
				return null;
			}
			int len = headerValue.Length;
			int attrNameLen = attrName.Length;
			int index = 1;
			while (index < len)
			{
				index = CultureInfo.InvariantCulture.CompareInfo.IndexOf(headerValue, attrName, index, CompareOptions.IgnoreCase);
				if ((index < 0) || ((index + attrNameLen) >= len))
				{
					break;
				}
				char start = headerValue[index - 1];
				char end = headerValue[index + attrNameLen];
				if ((((start == ';') || (start == ',')) || char.IsWhiteSpace(start)) && ((end == '=') || char.IsWhiteSpace(end)))
				{
					break;
				}
				index += attrNameLen;
			}
			if ((index < 0) || (index >= len))
			{
				return null;
			}
			index += attrNameLen;
			while ((index < len) && char.IsWhiteSpace(headerValue[index]))
			{
				index++;
			}
			if ((index >= len) || (headerValue[index] != '='))
			{
				return null;
			}
			index++;
			while ((index < len) && char.IsWhiteSpace(headerValue[index]))
			{
				index++;
			}
			if (index >= len)
			{
				return null;
			}
			if ((index < len) && (headerValue[index] == '"'))
			{
				if (index == (len - 1))
				{
					return null;
				}
				attrLen = headerValue.IndexOf('"', (int) (index + 1));
				if ((attrLen < 0) || (attrLen == (index + 1)))
				{
					return null;
				}
				return headerValue.Substring(index + 1, (attrLen - index) - 1).Trim();
			}
			attrLen = index;
			while (attrLen < len)
			{
				if ((headerValue[attrLen] == ' ') || (headerValue[attrLen] == ',') || (headerValue[attrLen] == ';'))
				{
					break;
				}
				attrLen++;
			}
			if (attrLen == index)
			{
				return null;
			}
			return headerValue.Substring(index, attrLen - index).Trim();
		}

    }
}
