//------------------------------------------------------------------------------
// <copyright company="Telligent Systems">
//     Copyright (c) Telligent Systems Corporation.  All rights reserved.
// </copyright> 
//------------------------------------------------------------------------------

using System;

namespace GPRP.GPRPComponents
{
	/// <summary>
	/// Summary description for Referral.
	/// </summary>
	public class Referral
	{
		public Referral()
		{
		}

        private int _referralID;
        /// <summary>
        /// Property ReferralID (int)
        /// </summary>
        public int ReferralID
        {
            get {  return this._referralID; }
            set {  this._referralID = value; }
        }

        private int _settingsID;
        /// <summary>
        /// Property settingsID (int)
        /// </summary>
        public int SettingsID
        {
            get {  return this._settingsID; }
            set {  this._settingsID = value; }
        }

        private int _sectionID = -1;
        /// <summary>
        /// Property SectionID (int)
        /// </summary>
        public int SectionID
        {
            get {  return this._sectionID; }
            set {  this._sectionID = value; }
        }

        private int _postID = -1;
        /// <summary>
        /// Property PostID (int)
        /// </summary>
        public int PostID
        {
            get {  return this._postID; }
            set {  this._postID = value; }
        }

        private string _url;
        /// <summary>
        /// Property Url (string)
        /// </summary>
        public string Url
        {
            get {  return this._url; }
            set {  this._url = value.ToLower(); }
        }

        public int UrlID
        {
            get { return Url.GetHashCode();}
        }

        private string _title;
        /// <summary>
        /// Property Title (string)
        /// </summary>
        public string Title
        {
            get {  return this._title; }
            set {  this._title = value; }
        }

        private int _hits;
        /// <summary>
        /// Property Hits (int)
        /// </summary>
        public int Hits
        {
            get {  return this._hits; }
            set {  this._hits = value; }
        }

        private DateTime _lastDate;
        /// <summary>
        /// Property LastDate (DateTime)
        /// </summary>
        public DateTime LastDate
        {
            get {  return this._lastDate; }
            set {  this._lastDate = value; }
        }
	}
}
