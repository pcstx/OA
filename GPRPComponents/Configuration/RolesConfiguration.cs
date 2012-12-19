

using System;
using System.Xml.Serialization;

namespace GPRP.GPRPComponents
{
	/// <summary>
	/// Summary description for RoleConfiguration.
	/// </summary>
	[Serializable]
    public class RolesConfiguration
	{
		public RolesConfiguration()
		{
			
		}
        private string _registeredUsers = "Registered Users";
        private string _systemAdmin = "SystemAdministrator";
        private string _moderator = "Moderator";
        private string _editor = "Editor";
        private string _forumsAdministrator = "ForumsAdministrator";
        private string _blogAdministrator = "BlogAdministrator";
        private string _galleryAdministrator = "GalleryAdministrator";

        private string _everyone = "Everyone";

        [XmlAttribute("everyone")]
        public string Everyone
        {
            get{return _everyone;}
            set{_everyone = value;}
        }

        [XmlAttribute("registeredUsers")]
        public string RegisteredUsers
        {
            get
            {  return this._registeredUsers; }
            set
            {  this._registeredUsers = value; }
        }
        
       
        /// <summary>
        /// Property SystemAdmin (string)
        /// </summary>
        [XmlAttribute("systemAdministrator")]
        public string SystemAdministrator
        {
            get
            {  return this._systemAdmin; }
            set
            {  this._systemAdmin = value; }
        }

        
        
        /// <summary>
        /// Property Moderator (string)
        /// </summary>
       [XmlAttribute("moderator")]
        public string Moderator
        {
            get
            {  return this._moderator; }
            set
            {  this._moderator = value; }
        }

        
        
        /// <summary>
        /// Property Editor (string)
        /// </summary>
        [XmlAttribute("editor")]
        public string Editor
        {
            get
            {  return this._editor; }
            set
            {  this._editor = value; }
        }

        
        
        /// <summary>
        /// Property ForumsAdministrator (string)
        /// </summary>
        [XmlAttribute("forumsAdministrator")]
        public string ForumsAdministrator 
        {
            get
            {  return this._forumsAdministrator; }
            set
            {  this._forumsAdministrator = value; }
        }

        
        
        /// <summary>
        /// Property BlogAdministrator (string)
        /// </summary>
        [XmlAttribute("blogAdministrator")]
        public string BlogAdministrator
        {
            get
            {  return this._blogAdministrator; }
            set
            {  this._blogAdministrator = value; }
        }

        
        /// <summary>
        /// Property GalleryAdministrator (string)
        /// </summary>
       [XmlAttribute("galleryAdministrator")]
        public string GalleryAdministrator
        {
            get
            {  return this._galleryAdministrator; }
            set
            {  this._galleryAdministrator = value; }
        }
 
        public string RoleList()
        {
            return string.Format("{0}|{1}|{2}|{3}|{4}|{5}|{6}|{7}",Everyone,RegisteredUsers,SystemAdministrator,Moderator,Editor,ForumsAdministrator,GalleryAdministrator,BlogAdministrator);
        }

	}
}
