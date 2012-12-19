//------------------------------------------------------------------------------
// <copyright company="Telligent Systems">
//     Copyright (c) Telligent Systems Corporation.  All rights reserved.
// </copyright> 
//------------------------------------------------------------------------------

using System;
using System.Web.Caching;

using System.Xml;
using System.Collections;
using System.Collections.Specialized;
using GPRP.GPRPEnumerations;



namespace GPRP.GPRPComponents 
{

    /// <summary>
    /// Public class to load all language XML files into cache for quick access.
    /// </summary>
    public class ResourceManager {
        //读取资源的类型,是字符串还是错误信息反馈
        enum ResourceManagerType {
            String,
            ErrorMessage
        }

        public static NameValueCollection GetSupportedLanguages () {
            CSContext csContext = CSContext.Current;
            
            string cacheKey = "Forums-SupportedLanguages";

            NameValueCollection supportedLanguages = CSCache.Get(cacheKey) as NameValueCollection;
            if (supportedLanguages == null) {
                string filePath = csContext.Context.Server.MapPath("~" + CSConfiguration.GetConfig().FilesPath + "/Languages/languages.xml");
                CacheDependency dp = new CacheDependency(filePath);
                supportedLanguages = new NameValueCollection();

                XmlDocument d = new XmlDocument();
                d.Load( filePath );

                foreach (XmlNode n in d.SelectSingleNode("root").ChildNodes) {
                    if (n.NodeType != XmlNodeType.Comment) {
                        supportedLanguages.Add(n.Attributes["name"].Value, n.Attributes["key"].Value);
                    }
                }

                CSCache.Max(cacheKey, supportedLanguages, dp);
            }

            return supportedLanguages;
        }

		public static string GetString(string name) 
		{
            return GetString(name, CSContext.Current.Language, false);
		}
        //CSContext.Current.User.Profile.Language,
		/// <summary>
		/// 可选是否进行Uncode转义
		/// </summary>
		/// <param name="name"></param>
		/// <param name="unicodeEncode"></param>
		/// <returns></returns>
		public static string GetString(string name, bool unicodeEncode)
		{
			return GetString(name, CSContext.Current.User.Profile.Language, unicodeEncode);
		}

		public static string GetString(string name, string language)
		{
			return GetString(name, language, false);
		}

		/// <summary>
		/// 可选是否进行Uncode转义
		/// </summary>
		/// <param name="name"></param>
		/// <param name="language"></param>
		/// <param name="unicodeEncode"></param>
		/// <returns></returns>
        public static string GetString(string name, string language, bool unicodeEncode) 
		{

            Hashtable resources = GetResource (ResourceManagerType.String,language);

            string text = resources[name] as string;

            if (text == null) 
            {
                #if DEBUG
                    return string.Format("<strong><FONT color=#ff0000 size=5>Missing Resource: {0}</FONT></strong>",name);
                #else
                    throw new CSException(CSExceptionType.ResourceNotFound, "Value not found in Resources.xml for: " + name);
                #endif
            }
			if (unicodeEncode)
				text = StringTransforms.UnicodeEncode(text);
            return text;
        }

        public static Message GetMessage (CSExceptionType exceptionType) {

            Hashtable resources = GetResource (ResourceManagerType.ErrorMessage);

            if (resources[(int) exceptionType] == null) {
                // LN 6/9/04: Changed to throw a forums exception 
                throw new CSException(CSExceptionType.ResourceNotFound, "Value not found in Messages.xml for: " + exceptionType);
            }

            return (Message) resources[(int) exceptionType];
        }

		private static Hashtable GetResource (ResourceManagerType resourceType)
		{
			return GetResource(resourceType, CSContext.Current.User.Profile.Language);
		}

        private static Hashtable GetResource (ResourceManagerType resourceType, string userLanguage) {
            CSContext csContext = CSContext.Current;
            
            string defaultLanguage = CSConfiguration.GetConfig().DefaultLanguage;
            string cacheKey = resourceType.ToString() + defaultLanguage + userLanguage;

            // Ensure the user has a language set
            //
            if (userLanguage == "")
                userLanguage = defaultLanguage;

            // Attempt to get the resources from the Cache
            //
            Hashtable resources = CSCache.Get(cacheKey) as Hashtable;

            if (resources == null) 
            {
                resources = new Hashtable();

                // First load the English resouce, changed from loading the default language
				// since the userLanguage is set to the defaultLanguage if the userLanguage
				// is unassigned. We load the english language always just to ensure we have
				// a resource loaded just incase the userLanguage doesn't have a translated
				// string for this English resource.
                //
                resources = LoadResource(resourceType, resources, "en-US", cacheKey);

                // If the user language is different load it
                //
                if ("en-US" != userLanguage)
                    resources= LoadResource(resourceType, resources, userLanguage, cacheKey);

            }

            return resources;
        }

		private static Hashtable LoadResource (ResourceManagerType resourceType, Hashtable target, string language, string cacheKey) {
		    CSContext csContext = CSContext.Current;
			string filePath = csContext.PhysicalPath("Languages\\" + language + "\\{0}");// csContext.MapPath("~" + CSConfiguration.GetConfig().FilesPath + "/Languages/" + language + "/{0}");

			switch (resourceType) {
				case ResourceManagerType.ErrorMessage:
					filePath = string.Format(filePath, "Messages.xml");
					break;

				default:
					filePath = string.Format(filePath, "Resources.xml");
					break;
			}

			CacheDependency dp = new CacheDependency(filePath);

			XmlDocument d = new XmlDocument();
			try {
				d.Load( filePath );
			} catch {
				return target;
			}

			foreach (XmlNode n in d.SelectSingleNode("root").ChildNodes) {
				if (n.NodeType != XmlNodeType.Comment) {
					switch (resourceType) {
						case ResourceManagerType.ErrorMessage:
					        Message m = new Message(n);
							target[m.MessageID] = m;
							break;

						case ResourceManagerType.String:
							if (target[n.Attributes["name"].Value] == null)
								target.Add(n.Attributes["name"].Value, n.InnerText);
							else
								target[n.Attributes["name"].Value] = n.InnerText;

							break;
					}
				}
			}

			// Create a new cache dependency and set it to never expire
			// unless the underlying file changes
			//
			// 7/26/2004 Terry Denham
			// We should only keep the default language cached forever, not every language.
			//DateTime cacheUntil;
			if( language == CSConfiguration.GetConfig().DefaultLanguage ) {
                CSCache.Max(cacheKey,target,dp);
			}
			else {
                CSCache.Insert(cacheKey,target,dp,CSCache.HourFactor);
			}

            return target;

        }

    }
}
