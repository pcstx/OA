//------------------------------------------------------------------------------
// <copyright company="Telligent Systems">
//     Copyright (c) Telligent Systems Corporation.  All rights reserved.
// </copyright> 
//------------------------------------------------------------------------------

using System.Collections;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Xml;

namespace GPRP.GPRPComponents
{
	/// <summary>
	/// Summary description for SiteUrlData.
	/// </summary>
	public class SiteUrlsData
	{
		public SiteUrlsData(string siteUrlsXmlFile)
		{
			_paths = new NameValueCollection();
			_reversePaths = new NameValueCollection();
			_locations = new NameValueCollection();
			_reWrittenUrls = new ArrayList();
			_tabUrls = new ArrayList();

            Initialize(siteUrlsXmlFile);
		}

		protected XmlDocument CreateDoc(string siteUrlsXmlFile)
		{
			XmlDocument doc = new XmlDocument();
			doc.Load( siteUrlsXmlFile );
			return doc;
		}


        protected void Initialize(string siteUrlsXmlFile)
        {
            
           
            //pattern used to build Urls
            //const string urlPattern = "{0}{1}{2}";

            string globalPath = Globals.ApplicationPath;
            if(globalPath != null)
                globalPath = globalPath.Trim();

            // Load SiteUrls from the SiteUrls.xml file
            //
            XmlDocument doc = CreateDoc(siteUrlsXmlFile);

            //Locations define a common directory/path structure. Instead of appending
            //forums to each forum link, the location attribute is set to forums
            //and while the links are processed below, /forums/ is appened to the link.
            //This should allow directories to be easily changed.
            XmlNode basePaths = doc.SelectSingleNode("SiteUrls/locations");
            ArrayList al = new ArrayList();
            foreach(XmlNode n in basePaths.ChildNodes)
            {
                if(n.NodeType != XmlNodeType.Comment)
                {
                    XmlAttribute name = n.Attributes["name"];
                    XmlAttribute path = n.Attributes["path"];
                    XmlAttribute exclude = n.Attributes["exclude"];

                    if(name != null && path != null)
                    {
                        _locations.Add(name.Value,globalPath + path.Value);
						if(exclude != null)
						{
							string filter = globalPath + path.Value;
							if(filter != null && filter.Length > 1)
							al.Add(filter);
						}
                    }
                }
            }

            _locationFilter =  string.Join("|", (string[])al.ToArray(typeof(string)));

			#region SiteUrls
            XmlNode urls = doc.SelectSingleNode("SiteUrls/urls");

            foreach (XmlNode n in urls.ChildNodes) 
            {

                if (n.NodeType != XmlNodeType.Comment) 
                {
                    string name = n.Attributes["name"].Value;
                    string path = n.Attributes["path"].Value.Replace("^", "&");

                    string location = null;
                    XmlAttribute l = n.Attributes["location"];
                    if(l != null)
                        location = l.Value;

                    _paths.Add(name, _locations[location] + path);

                    //TODO: Determine if we need to store the full path
                    _reversePaths.Add(path, name);

                    XmlAttribute vanity = n.Attributes["vanity"];
                    XmlAttribute pattern = n.Attributes["pattern"];

                    //Store full paths like regular urls. 
                    if(vanity != null && pattern != null)
                    {
                        string p = _locations[location] + pattern.Value;
                        string v = _locations[location] + vanity.Value.Replace("^", "&");
                        _reWrittenUrls.Add(new ReWrittenUrl(name,p,v));
                    }
                    
                }

            }
			#endregion

			XmlNode nav = doc.SelectSingleNode("SiteUrls/navigation");
			if(nav != null)
			{
				foreach(XmlNode node in nav.ChildNodes)
				{
					if(node.NodeType != XmlNodeType.Comment)
					{
						XmlAttribute name = node.Attributes["name"];

						XmlAttribute resourceUrl = node.Attributes["resourceUrl"];
						XmlAttribute resourceName = node.Attributes["resourceName"];

						XmlAttribute navigateUrl = node.Attributes["navigateUrl"];
						XmlAttribute text = node.Attributes["text"];

						XmlAttribute roles = node.Attributes["roles"];

						// Skip over certain tabs if the application is disabled
						if((name.Value == "blog") && CSContext.Current.SiteSettings.BlogsDisabled)
							continue;
						if((name.Value == "forums") && CSContext.Current.SiteSettings.ForumsDisabled)
							continue;
						if((name.Value == "gallery") && CSContext.Current.SiteSettings.GalleriesDisabled)
							continue;

						// Set roles to "Everyone" if the attribute is null
						string rolesValue;
						if(roles == null)
							rolesValue = "Everyone";
						else
							rolesValue = roles.Value;

						// Set the navigateUrl to a specified one if no resource entries are given
						string urlValue;
						if(resourceUrl == null)
							urlValue = navigateUrl.Value;
						else
							urlValue = FormatUrl(resourceUrl.Value);

						// Create the CSLink and add it
						CSLink link = new CSLink(name.Value, (resourceName == null) ? null : resourceName.Value, (text == null) ? null : text.Value, urlValue, rolesValue);
						_tabUrls.Add(link);
					}
				}
			}
        }

        public string FormatUrl(string name)
        {
            return FormatUrl(name,null);
        }

        public virtual string FormatUrl(string name, params object[] parameters)
        {
        	

            if(parameters == null)
                return this.Paths[name];

            else
                return string.Format(Paths[name],parameters);
        }

        #region Public Properties
        private NameValueCollection _paths = null;
        private NameValueCollection _reversePaths = null;
        private ArrayList _reWrittenUrls = null;
		private ArrayList _tabUrls = null;
        private NameValueCollection _locations;
        private string _locationFilter;

        public string LocationFilter
        {
            get
            {
                return _locationFilter;
            }
        }

        /// <summary>
        /// Property Paths (NameValueCollection)
        /// </summary>
        public NameValueCollection Paths
        {
            get {  return this._paths; }
        }

        public NameValueCollection Locations
        {
            get {  return this._locations; }
        }

        /// <summary>
        /// Property Paths (NameValueCollection)
        /// </summary>
        public NameValueCollection ReversePaths
        {
            get {  return this._reversePaths; }
        }

        public ArrayList ReWrittenUrls
        {
            get { return _reWrittenUrls;}   
        }


		public ArrayList TabUrls
		{
			get
			{
				return _tabUrls;
			}
		}

        #endregion

        private static string ResolveUrl (string path) 
        {

            if (Globals.ApplicationPath.Length > 0)
                return Globals.ApplicationPath + path;
            else
                return path;
        }

	}
}
