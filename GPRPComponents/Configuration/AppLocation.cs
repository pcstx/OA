//------------------------------------------------------------------------------
// <copyright company="Telligent Systems">
//     Copyright (c) Telligent Systems Corporation.  All rights reserved.
// </copyright> 
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Xml;
using System.Text.RegularExpressions;
using System.Web;
using GPRP.GPRPEnumerations;

namespace GPRP.GPRPComponents
{
	/// <summary>
	/// Summary description for AppLocation.
	/// </summary>
	public class AppLocation
	{
        public AppLocation() {}

        #region private members

        private Regex regex = null;
        private string pattern = null;
        private string defaultName = null;
        private Hashtable ht = new Hashtable();

        const string HttpContextAppLocation = "AppLocation";

        #endregion

        #region Internal Methods

        internal void Add(CSApplicationInternal app)
        {
            if(!ht.Contains(app.Name))
            {
                ht.Add(app.Name,app);
            }
            else
            {
                throw new Exception(string.Format("The CSApplication.Name ({0}) was not unique",app.Name));
            }
        }

        internal CSApplicationInternal LookUp(string url)
        {
            //Is we have a global pattern, use it to pre-validate
            if(Pattern != null)
            {
                if(!regex.IsMatch(url))
                    return null;
            }
            
            //Loop through all of our CSApplications and look for a match
            foreach(string key in ht.Keys)
            {
                CSApplicationInternal csa = ht[key] as CSApplicationInternal;
                if(csa.IsMatch(url))
                    return csa;
            }

            
            //Do we want to include the Default value in IsTests

            //Was a default name specified
            if(DefaultName != null)
            {
                return ht[DefaultName] as CSApplicationInternal;
            }

            return null;
        }

        internal CSApplicationInternal CurrentCSApplication()
        {

            HttpContext context = HttpContext.Current;
            CSApplicationInternal app = context.Items[HttpContextAppLocation] as CSApplicationInternal;
            if(app == null)
            {
                app = LookUp(context.Request.Path);
                if(app != null)
                {
                    context.Items.Add(HttpContextAppLocation,app);
                }
            }

            return app;
        }

        #endregion

        #region Current

        public string CurrentName
        {
            get
            {
                CSApplicationInternal app = CurrentCSApplication();

                if(app != null)
                {
                    return app.Name;
                }
                else
                {
                    return null;
                }
            }
        }

        public ApplicationType CurrentApplicationType
        {
            get
            {
                CSApplicationInternal app = CurrentCSApplication();

                if(app != null)
                {
                    return app.ApplicationType;
                }
                else
                {
                    return ApplicationType.Unknown;
                }
            }
        }

        #endregion

        #region Public members 

        public string Pattern
        {
            get{return pattern;}
            set
            {
                pattern = value;
                if(pattern != null)
                {
                    regex = new Regex(pattern,RegexOptions.Compiled|RegexOptions.IgnoreCase);
                }
            }
        }

        public string DefaultName
        {
            get{return defaultName;}
            set{defaultName = value;}
        }

        #endregion

        #region Is Checks

        public bool IsName(string name)
        {
            return string.Compare(name,CurrentName,true) == 0;
        }

        public bool IsApplicationType(ApplicationType applicationType)
        {
            return CurrentApplicationType == applicationType;
        }


        //ToDo: Should this include "default". Currently, if DefaultName is set,
        //this will always be true.

        /// <summary>
        /// Does AppLocation know which application we are in?
        /// </summary>
        public bool IsKnownApplication
        {
            get
            {
                return CurrentApplicationType != ApplicationType.Unknown;
            }
        }

        #endregion

        #region Static Create Methods
        /// <summary>
        /// All configuration settings should "default" if possible.
        /// </summary>
        /// <returns></returns>
        public static AppLocation Default()
        {
            AppLocation app = new AppLocation();
            app.Add(new CSApplicationInternal("blogs/admin", "BlogAdmin", ApplicationType.Weblog));
            app.Add(new CSApplicationInternal("forums/admin", "ForumAdmin", ApplicationType.Forum));
            app.Add(new CSApplicationInternal("galleries/admin", "GalleryAdmin", ApplicationType.Gallery));

            app.Add(new CSApplicationInternal("blogs", "BlogPublic", ApplicationType.Weblog));
            app.Add(new CSApplicationInternal("forums", "ForumPublic", ApplicationType.Forum));
            app.Add(new CSApplicationInternal("galleries", "GalleryPublic", ApplicationType.Gallery));

            return app;

        }

        /// <summary>
        /// Creates an instance of AppLocation  for the supplied XmlNode. 
        /// This node will generally be supplied during the ForumsConfiguration
        /// instantiation
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public static AppLocation Create(XmlNode node)
        {
            AppLocation app = new AppLocation();

            //Look for AppLocation property values
            XmlAttributeCollection xac = node.Attributes;
            if(xac != null)
            {
                foreach(XmlAttribute att in xac)
                {
                    if(att.Name == "pattern")
                    {
                        app.Pattern = Globals.ApplicationPath + att.Value;
                    }
                    else if(att.Name == "defaultName")
                    {
                        app.DefaultName = att.Value;
                    }

                }
            
                //loop through the child nodes. For the moment, we will
                //only support add, but it might be worth while allowing applicatoins
                //to stack these settings
                foreach(XmlNode child in node.ChildNodes)
                {
                    if(child.Name == "add")
                    {
                        XmlAttributeCollection csAtt = child.Attributes;
                        if(csAtt != null)
                        {
                            string pattern = Globals.ApplicationPath + csAtt["pattern"].Value;
                            string name = csAtt["name"].Value;
                            ApplicationType appType = (ApplicationType) Enum.Parse(typeof(ApplicationType),csAtt["type"].Value,true);
                            app.Add(new CSApplicationInternal(pattern, name, appType));
                        }
                    }
                }

            }

            return app;
            
        }

        #endregion

	}

    #region CSApplication

    internal class CSApplicationInternal
    {
        private string _pattern = null;
        private string _name = null;
        private ApplicationType _appType = ApplicationType.Forum;
        private Regex _regex = null;

        /// <summary>
        /// Must supply all three values to create a new instance of CSApplication. Name must be unique
        /// </summary>
        /// <param name="pattern"></param>
        /// <param name="name">Must be unique</param>
        /// <param name="value"></param>
        internal CSApplicationInternal(string pattern, string name, ApplicationType appType)
        {
            _pattern = pattern.ToLower();
            _name = name.ToLower();
            _appType = appType;
            _regex = new Regex(pattern,RegexOptions.Compiled | RegexOptions.IgnoreCase);
        }

        public string Name
        {
            get{return _name;}
        }

        public ApplicationType ApplicationType
        {
            get{return _appType;}
        }

        public bool IsMatch(string url)
        {
            return _regex.IsMatch(url);
        }
    }

    #endregion
}
