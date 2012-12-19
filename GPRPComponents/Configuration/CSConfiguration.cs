//------------------------------------------------------------------------------
// <copyright company="Telligent Systems">
//     Copyright (c) Telligent Systems Corporation.  All rights reserved.
// </copyright> 
//------------------------------------------------------------------------------

// 修改说明：新增附件保存方式、附件保存路径


using System;
using System.Collections;
using System.Collections.Specialized;
using System.IO;
using System.Web;
using System.Web.Caching;
using System.Xml;
using System.Configuration;
using System.Xml.Serialization;
using GPRP.GPRPEnumerations;

namespace GPRP.GPRPComponents
{

    // *********************************************************************
    //  ForumConfiguration
    //
    /// <summary>
    /// Class used to represent the configuration data for the ASP.NET Forums
    /// </summary>
    /// 
    // ***********************************************************************/
    public class CSConfiguration {

		public static readonly string CacheKey = "CSConfiguration";

		#region private members
        Hashtable providers = new Hashtable();
		Hashtable extensions = new Hashtable();
        string defaultLanguage;
        string filesPath;
		bool disableBackgroundThreads = false;
		bool disableIndexing = false;
		bool disableEmail = false;
		short smtpServerConnectionLimit = -1;
		bool enableLatestVersionCheck = true;
        private AppLocation app = null;
        private string textEditorType = null;
        private string applicationOverride = null;
        private int threadCount = 2;
        private SystemType systemType = SystemType.Self;
        bool backwardsCompatiblePasswords = false;
		bool requireSSL = false;
		private string[] _defaultRoles = new string[]{"Everyone", "Registered Users"};
		private string applicationKeyOverride = null;
		private WWWStatus _wwwStatus = WWWStatus.Remove;

        private RolesConfiguration roleConfiguration= null;
		private int cacheFactor = 5;
		private XmlDocument XmlDoc = null;

		#region 新增内容 by 宝玉
		private string attachmentsPath = "~/Attachments/";
		private string avatarsPath = "~/Avatars/Uploads/";
		// 新增 附件，头像保存方式
		private FileSaveMode attachmentSaveMode = FileSaveMode.Disk; // 默认保存在磁盘中
		private FileSaveMode avatarSaveMode = FileSaveMode.Disk; // 默认保存在磁盘中
		#endregion

		#endregion

		#region cnstr
		public CSConfiguration(XmlDocument doc)
		{
			XmlDoc = doc;
			LoadValuesFromConfigurationXml();
		}
		#endregion

		#region GetXML

		/// <summary>
		/// Enables reading of the configuration file's XML without reloading the file
		/// </summary>
		/// <param name="nodePath"></param>
		/// <returns></returns>
		public XmlNode GetConfigSection(string nodePath)
		{
			return XmlDoc.SelectSingleNode(nodePath);
		}

		#endregion

		#region GetConfig
        public static CSConfiguration GetConfig() 
		{
			CSConfiguration config = CSCache.Get(CacheKey) as CSConfiguration;
			if(config == null)
			{
				string path;
				if(HttpContext.Current != null)
					path = HttpContext.Current.Server.MapPath("~/communityserver.config");
				else
					path = Directory.GetCurrentDirectory() + Path.DirectorySeparatorChar + "communityserver.config";

				XmlDocument doc = new XmlDocument();
				doc.Load(path);
				config = new CSConfiguration(doc);
				CSCache.Max(CacheKey,config,new CacheDependency(path));

				CSCache.ReSetFactor(config.CacheFactor);
			}
			return config;
            
        }
		#endregion

        // *********************************************************************
        //  LoadValuesFromConfigurationXml
        //
        /// <summary>
        /// Loads the forums configuration values.
        /// </summary>
        /// <param name="node">XmlNode of the configuration section to parse.</param>
        /// 
        // ***********************************************************************/
		internal void LoadValuesFromConfigurationXml() 
		{
            
			XmlNode node = GetConfigSection("CommunityServer/Core");
			
			XmlAttributeCollection attributeCollection = node.Attributes;
            
          
			// Get the default language
			//

			XmlAttribute att = attributeCollection["defaultLanguage"];
			if(att != null)
				defaultLanguage =att.Value;
			else
				defaultLanguage = "en-US";

			att = attributeCollection["filesPath"];
			if(att != null)
				filesPath = att.Value;
			else
				filesPath = "/";

			att = attributeCollection["applicationKeyOverride"];
			if(att != null)
				applicationKeyOverride = att.Value;

			att = attributeCollection["disableThreading"];
			if(att != null)
				disableBackgroundThreads = bool.Parse(att.Value);

			att = attributeCollection["disableIndexing"];
			if(att != null)
				disableIndexing = bool.Parse(att.Value);


			att = attributeCollection["cacheFactor"];
			if(att != null)
				cacheFactor = Int32.Parse(att.Value);
			else
				cacheFactor = 5;

			att = attributeCollection["disableEmail"];
			if(att != null)
				disableEmail = bool.Parse(att.Value);

			
			att = attributeCollection["smtpServerConnectionLimit"];
			if(att != null)
				smtpServerConnectionLimit = short.Parse(att.Value);
			else
				smtpServerConnectionLimit = -1;

			att = attributeCollection["enableLatestVersionCheck"];
			if(att != null)
				enableLatestVersionCheck = bool.Parse(att.Value);

			att = attributeCollection["backwardsCompatiblePasswords"];
			if(att != null)
				backwardsCompatiblePasswords = bool.Parse(att.Value);

			att = attributeCollection["textEditorType"];
			if(att != null)
				textEditorType = att.Value;
			else
				textEditorType =  "CommunityServer.Controls.DefaultTextEditor,CommunityServer.Controls";
            

			att = attributeCollection["applicationOverride"];
			if(att != null)
				applicationOverride = att.Value;
			else
				applicationOverride = null;

			att = attributeCollection["systemType"];
			if(att != null)
				systemType =  (SystemType)Enum.Parse(typeof(SystemType),attributeCollection["systemType"].Value);
			else
				systemType = SystemType.Self;

			att = attributeCollection["wwwStatus"];
			if(att != null)
				_wwwStatus = (WWWStatus)Enum.Parse(typeof(WWWStatus),attributeCollection["wwwStatus"].Value);

			// 读取默认附件保存方式
			//
			att = attributeCollection["AttachmentSaveMode"];
			if(att != null)
			{
				try
				{
					attachmentSaveMode = (FileSaveMode)Enum.Parse(typeof(FileSaveMode), attributeCollection["AttachmentSaveMode"].Value);
				}
				catch{}
			}

			// 读取默认附件保存方式
			//
			att = attributeCollection["AvatarSaveMode"];
			if(att != null)
			{
				try
				{
					avatarSaveMode = (FileSaveMode)Enum.Parse(typeof(FileSaveMode), attributeCollection["AvatarSaveMode"].Value);
				}
				catch{}
			}
			
			// 读取附件保存路径
			//
			att = attributeCollection["AttachmentsPath"];
			if(att != null)
				attachmentsPath = att.Value;
			
			// 读取头像保存路径
			//
			att = attributeCollection["AvatarsPath"];
			if(att != null)
				avatarsPath = att.Value;


			att = attributeCollection["requireSSL"];
			if(att != null)
				requireSSL = bool.Parse(att.Value);


			XmlAttribute roles = attributeCollection["defaultRoles"];
			if(roles != null)
			{
				_defaultRoles = roles.Value.Split(';');
			}

			// Read child nodes
			//
			foreach (XmlNode child in node.ChildNodes) 
			{

				if (child.Name == "providers")
					GetProviders(child, providers);

				if(child.Name == "appLocation")
					GetAppLocation(child);

				if (child.Name == "extensionModules")
					GetProviders(child, extensions);

				if(child.Name == "roleConfiguration")
					GetRoleConfiguration(child);

			}

			//if we do not have an application, create the default one
			if(app == null)
			{
				app = AppLocation.Default();
			}

			if(roleConfiguration == null)
			{
				roleConfiguration = new RolesConfiguration();
			}

            


		}

        // *********************************************************************
        //  GetRoleConfiguration
        //
        /// <summary>
        /// Internal class used to populate default role changes.
        /// </summary>
        /// <param name="node">XmlNode of the configurations.</param>
        /// 
        // ***********************************************************************/
        internal void GetRoleConfiguration(XmlNode node)
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof (RolesConfiguration));
                roleConfiguration = serializer.Deserialize(new XmlNodeReader(node)) as RolesConfiguration;
            }
            catch
            {
                //Should we let exception go?
                roleConfiguration = new RolesConfiguration();
            }
        }

        // *********************************************************************
        //  GetProviders
        //
        /// <summary>
        /// Internal class used to populate the available providers.
        /// </summary>
        /// <param name="node">XmlNode of the providers to add/remove/clear.</param>
        /// 
        // ***********************************************************************/
        internal void GetProviders(XmlNode node, Hashtable table) {

            foreach (XmlNode provider in node.ChildNodes) {

                switch (provider.Name) {
                    case "add" :
                        table.Add(provider.Attributes["name"].Value, new Provider(provider.Attributes) );
                        break;

                    case "remove" :
                        table.Remove(provider.Attributes["name"].Value);
                        break;

                    case "clear" :
                        table.Clear();
                        break;

                }

            }

        }

        internal void GetAppLocation(XmlNode node)
        {
            app = AppLocation.Create(node);
        }

        // Properties
        //
        public string DefaultLanguage { get { return defaultLanguage; } }
        public string FilesPath { get { return filesPath; } }
        public Hashtable Providers { get { return providers; } } 
		public Hashtable Extensions { get { return extensions; } } 
		public bool IsBackgroundThreadingDisabled { get { return disableBackgroundThreads; } }
		public bool IsIndexingDisabled { get { return disableIndexing; } }
        public RolesConfiguration RolesConfiguration{get { return roleConfiguration;} }
        public string ApplicationOverride {get {return applicationOverride;} }
        public int QueuedThreads { get { return threadCount; }}
		public int CacheFactor { get  {  return cacheFactor;  } }
		public bool RequireSLL { get {return requireSSL;}}
		public WWWStatus WWWStatus { get { return _wwwStatus;}}
		public FileSaveMode AttachmentSaveMode { get { return attachmentSaveMode;}}
		public FileSaveMode AvatarSaveMode { get { return avatarSaveMode;}}
		public string AttachmentsPath { get { return attachmentsPath; } }
		public string AvatarsPath { get { return avatarsPath; } }
		public string ApplicationKeyOverride
		{
			get{ return applicationKeyOverride;}
		}

        public string TextEditorType
        {
            get{return textEditorType;}
        }

        public AppLocation AppLocation
        {
            get{return app;}
        }

		public bool IsEmailDisabled { 
			get { 
				return disableEmail;
			}
		}

		public short SmtpServerConnectionLimit {
			get {
				return smtpServerConnectionLimit; 
			}
		}

		public bool EnableLatestVersionCheck {
			get {
				return enableLatestVersionCheck;
			}
		}

        public bool BackwardsCompatiblePasswords 
        {
            get 
            {
                return backwardsCompatiblePasswords;
            }
        }

		public string[] DefaultRoles
		{
			get{return _defaultRoles;}
		}

        

        public SystemType SystemType {get{ return systemType;}}
	}

	public class Provider 
	{
        string name;
		ExtensionType extensionType;
        string providerType;
        NameValueCollection providerAttributes = new NameValueCollection();

		public Provider (XmlAttributeCollection attributes) 
		{

            // Set the name of the provider
            //
            name = attributes["name"].Value;

			// Set the extension type
			//
			try
			{
				extensionType = (ExtensionType)Enum.Parse(typeof(ExtensionType), attributes["extensionType"].Value, true);
			}
			catch
			{
				// Occassionally get an exception on parsing the extensiontype, so set it to Unknown
				extensionType = ExtensionType.Unknown;
			}

            // Set the type of the provider
            //
            providerType = attributes["type"].Value;

            // Store all the attributes in the attributes bucket
            //
			foreach (XmlAttribute attribute in attributes) 
			{
                if ( (attribute.Name != "name") && (attribute.Name != "type") )
                    providerAttributes.Add(attribute.Name, attribute.Value);
            }
        }

        public string Name {
            get {
                return name;
            }
        }

		public ExtensionType ExtensionType
		{
			get { return extensionType; }
		}

        public string Type {
            get {
                return providerType;
            }
        }

        public NameValueCollection Attributes {
            get {
                return providerAttributes;
            }
        }

    }
}
