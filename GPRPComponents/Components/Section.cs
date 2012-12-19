//------------------------------------------------------------------------------
// <copyright company="Telligent Systems">
//     Copyright (c) Telligent Systems Corporation.  All rights reserved.
// </copyright> 
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Specialized;
using System.Text.RegularExpressions;
using GPRP.GPRPEnumerations;
namespace GPRP.GPRPComponents {
    /// <summary>
    /// A Section is the base container for a group of posts. 
    /// </summary>
    [Serializable]
	public abstract class Section : ExtendedAttributes, IComparable {

        #region Private Members
        // Member Variables
        int sectionID = 0;				// Unique section identifier
        int parentId = 0;
		int settingsID = 0;
                                               // moved to constructor try/catch for import/export
		ThreadDateFilterMode threadDateFilter; //= CSContext.Current.SiteSettings.DefaultThreadDateFilter;
        int totalPosts = -1;			// Total posts in the forum
        int totalThreads = -1;			// Total threads in the forum
        int groupId = -1;          // Identifier for the forum group this forum belongs to
        int sortOrder = 0;              // Used to control sorting of forums
        String name = "";				// Name of the forum
        String description = "";		// Description of the forum
        bool isModerated = false;	    // Is the forum isModerated?
        bool isActive = true;          // Is the forum isActive?
        bool isPrivate = false;         // Is the forum private?
        DateTime mostRecentPostDate = DateTime.MinValue.AddMonths(1);	        // The date of the most recent post to the forum
        int mostRecentPostId = 0;       // the most recent post id
        int mostRecentThreadId = 0;     // Post ID of the most recent thread
        String mostRecentUser = "";		// The author of the most recent post to the forum
        string mostRecentPostSubject =""; // most recent post subject
        DateTime dateCreated;			// The date/time the forum was created
        byte[] displayMask;
		Hashtable permissions = new Hashtable();
//        ForumPermission permission = new ForumPermission();
        static SectionSortBy sortBy = SectionSortBy.SortOrder;
        ArrayList sections = null;
        bool enablePostStatistics = true;
        bool enableAutoDelete = false;
        bool enableAnonymousPosts = false;
        bool isSearchable = true;
        int autoDeleteThreshold = 90;
        int mostRecentAuthorID;
        int postsToModerate = 0;
        int mostRecentThreadReplies = 0;

		private string appKey;
		private ApplicationType appType = ApplicationType.Unknown;
        #endregion

        public Section() 
        {
			try {
				threadDateFilter = CSContext.Current.SiteSettings.DefaultThreadDateFilter;
			} catch {
				threadDateFilter = ThreadDateFilterMode.TwoMonths;
			}
		}
        public Section(int sectionID) : this() { SectionID = sectionID; }

        #region Public Properties
		public string ApplicationKey
		{
			get{return appKey;}
			set
            {
                if(value != null && value.Trim().Length > 0)
                    appKey = value.ToLower().Replace(" ","_");
                else
                    appKey = null;
            }
		}

		public ApplicationType ApplicationType
		{
			get{return appType;}
			set{appType = value;}
		}

		ForumType forumType = ForumType.Normal;
		public ForumType ForumType {
			get {
				return forumType;
			}
			set {
				forumType = value;
			}
		}

		string navigateUrl;
		public string NavigateUrl {
			get {
				return navigateUrl;
			}
			set {
				navigateUrl = value;
			}
		}

        public int AutoDeleteThreshold {
            get {
                return autoDeleteThreshold;
            }
            set {
                autoDeleteThreshold = value;
            }
        }

        string newsgroupName;
        public string NewsgroupName {
            get { 
                if (newsgroupName != string.Empty)
                    return newsgroupName.ToLower();

                return name.Replace(" ", ".").ToLower();
            }
            set { newsgroupName = value; }
             
        }

        public int PostsToModerate {
            get {
                return postsToModerate;
            }
            set {
                postsToModerate = value;
            }
        }
        
        public bool EnablePostStatistics {
            get {
                return enablePostStatistics;
            }
            set {
                enablePostStatistics = value;
            }
        }

        public bool EnableAnonymousPosting {
            get {
                return enableAnonymousPosts;
            }
            set {
                enableAnonymousPosts = value;
            }
        }

        public bool IsSearchable {
            get {
                return isSearchable;
            }
            set {
                isSearchable = value;
            }
        }

        public bool EnableAutoDelete {
            get {
                return enableAutoDelete;
            }
            set {
                enableAutoDelete = value;
            }
        }

        public ArrayList Sections {
            get
            {
				if(sections == null)
					sections = new ArrayList();

            	return sections;
            }
            set { sections = value; }
        }

        

        #region CompareTo
        // *********************************************************************
        //  CompareTo
        //
        /// <summary>
        /// All forums have a SortOrder property. CompareTo compares on SortOrder
        /// to sort the forums appropriately.
        /// </summary>
        // ********************************************************************/
        public virtual int CompareTo(object value)
        {
            if (value == null) return 1;

            switch (SortBy) 
            {
                case SectionSortBy.Name:
                    return (this.Name.CompareTo( ((Section) value).Name) );

                case SectionSortBy.Thread:
                    return (this.TotalThreads.CompareTo( ((Section) value).TotalThreads) );

                case SectionSortBy.Post:
                    return (this.TotalPosts.CompareTo( ((Section) value).TotalPosts) );

                case SectionSortBy.LastPost:
                    return (this.MostRecentPostDate.CompareTo( ((Section) value).MostRecentPostDate) );

                case SectionSortBy.LastPostDescending:
                    return (  ((Section)value).MostRecentPostDate.CompareTo( this.MostRecentPostDate ) );

                default:
                    return (this.SortOrder.CompareTo( ((Section) value).SortOrder) );
            }
   
        }

        #endregion

    

        public enum SectionSortBy 
        {
            SortOrder,
            Name,
            Thread,
            Post,
            LastPost,
			LastPostDescending
        }

        public static SectionSortBy SortBy {
            get {
                return sortBy;
            }
            set {
                sortBy = value;
            }
        }

        // *********************************************************************
        //  IsAnnouncement
        //
        /// <summary>
        /// If post is locked and post date > 2 years
        /// </summary>
        // ********************************************************************/
        public virtual bool IsAnnouncement {
            get { 
                if (MostRecentPostDate > DateTime.Now.AddYears(2))
                    return true;
                else
                    return false;
            }
        }

        // *********************************************************************
        //  IsPrivate
        //
        /// <summary>
        /// Is the forum private, e.g. a role is required to access?
        /// </summary>
        // ********************************************************************/
        public virtual bool IsPrivate {
            get { return isPrivate; }
            set { isPrivate = value; }
        }

        /*************************** PROPERTY STATEMENTS *****************************/
        /// <summary>
        /// Specifies the unique identifier for the each forum.
        /// </summary>
        public int SectionID {
            get { return sectionID; }
            set {
                if (value < 0)
                    sectionID = 0;
                else
                    sectionID = value;
            }
        }

        
        // *********************************************************************
        //  ParentID
        //
        /// <summary>
        /// If ParentId > 0 this forum has a parent and is not a top-level forum
        /// </summary>
        // ********************************************************************/
        public int ParentID {
            get { return parentId; }
            set {
                if (value < 0)
                    parentId = 0;
                else
                    parentId = value;
            }
        }


		// *********************************************************************
		//  SettingsID
		//
		/// <summary>
		/// Specifies the unique identifier for the site the forum belongs to.
		/// </summary>
		// ********************************************************************/
		public int SettingsID 
		{
			get { return settingsID; }
			set 
			{
				if (value < 0)
					settingsID = 0;
				else
					settingsID = value;
			}
		}

        
        // *********************************************************************
        //  DisplayMask
        //
        /// <summary>
        /// Bit mask used to control what forums to display for the current user
        /// </summary>
        // ********************************************************************/
        public byte[] DisplayMask 
		{
            get { 
                return displayMask; 
            }
            set {
                displayMask = value;
            }
        }

               
        public int GroupID {
            get { return groupId; }
            set {
                if (value < 0)
                    groupId = 0;
                else
                    groupId = value;
            }
        }

        public int SortOrder {
            get { return sortOrder; }
            set { sortOrder = value; }
        }

        /// <summary>
        /// Indicates how many total posts the forum has received.
        /// </summary>
        public int TotalPosts {
            get { return totalPosts; }
            set {
                if (value < 0)
                    totalPosts = -1;
                else
                    totalPosts = value;
            }
        }


        /// <summary>
        /// Specifies the date/time of the most recent post to the forum.
        /// </summary>
        public DateTime MostRecentPostDate {
            get { return mostRecentPostDate; }
            set {
                mostRecentPostDate = value;
            }
        }

        /// <summary>
        /// Specifies the most recent post to the forum.
        /// </summary>
        public int MostRecentPostID {
            get { return mostRecentPostId; }
            set {
                mostRecentPostId = value;
            }
        }

        /// <summary>
        /// Specifies the most recent thread id to the forum.
        /// </summary>
        public int MostRecentThreadID {
            get { return mostRecentThreadId; }
            set {
                mostRecentThreadId = value;
            }
        }

        public int MostRecentThreadReplies {
            get { return mostRecentThreadReplies ; }
            set { mostRecentThreadReplies  = value; }

        }

        /// <summary>
        /// Specifies the author of the most recent post to the forum.
        /// </summary>
        public String MostRecentPostAuthor {
            get { return mostRecentUser; }
            set {
                mostRecentUser = value;
            }
        }

        /// <summary>
        /// Specifies the author of the most recent post to the forum.
        /// </summary>
        public String MostRecentPostSubject {
            get { return mostRecentPostSubject; }
            set {
                mostRecentPostSubject = value;
            }
        }

        /// <summary>
        /// Specifies the author of the most recent post to the forum.
        /// </summary>
        public int MostRecentPostAuthorID {
            get { return mostRecentAuthorID; }
            set {
                mostRecentAuthorID = value;
            }
        }

        /// <summary>
        /// Indicates how many total threads are in the forum.  A thread is a top-level post.
        /// </summary>
        public int TotalThreads {
            get { return totalThreads; }
            set {
                if (value < 0)
                    totalThreads = -1;
                else
                    totalThreads = value;
            }
        }

        /// <summary>
        /// Specifies how many days worth of posts to view per page when listing a forum's posts.
        /// </summary>
        public ThreadDateFilterMode DefaultThreadDateFilter {
            get { return threadDateFilter; }
            set { threadDateFilter = value; }
        }

        /// <summary>
        /// Specifies the name of the forum.
        /// </summary>
        public String Name {
            get { return name; }
            set { name = value; }
        }

        /// <summary>
        /// Specifies the description of the forum.
        /// </summary>
        public String Description {
            get { return description; }
            set { description = value; }
        }

        /// <summary>
        /// Specifies if the forum is isModerated or not.
        /// </summary>
        public bool IsModerated {
            get { return isModerated; }
            set { isModerated = value; }
        }

        /// <summary>
        /// Specifies if the forum is currently isActive or not.  InisActive forums do not appear in the
        /// ForumListView Web control listing.
        /// </summary>
        public bool IsActive {
            get { return isActive; }
            set { isActive = value; }
        }

        /// <summary>
        /// Returns the date/time the forum was created.
        /// </summary>
        public DateTime DateCreated {
            get { return dateCreated; }
            set { dateCreated = value; }
        }

        #endregion


        
        /// <summary>
        /// Property Owners (string)
        /// </summary>
        public string Owners
        {
            get {  return GetExtendedAttribute("SectionOwners"); }
            set
            {
                if(value != null && value.Trim().Length > 0)
                {
                    //Convert ; + zero or more spaces to just ;
                    SetExtendedAttribute("SectionOwners", Regex.Replace(value,";\\s*",";"));
                }
                else
                    SetExtendedAttribute("SectionOwners", null);
            }
        }

        /****************************************************************************/
		/// <summary>
		/// Property to access the collection of ForumPermissions applied to this forum. Use RoleName as the lookup
		/// </summary>
		public Hashtable PermissionSet 
        {
			get { return this.permissions; }
			set { permissions = value; }
		}

        public bool HasPermissions
        {
            get{ return (this.PermissionSet != null && this.PermissionSet.Count > 0);}
        }

        /// <summary>
        /// Each section type must return it's permission type
        /// </summary>
        public abstract PermissionBase DefaultRolePermission
        {
            get;
        }

        /// <summary>
        /// Provides the Method implementing the AccessCheckDelegate signature
        /// </summary>
        public abstract AccessCheckDelegate AccessCheck
        {
            get;
        }

        /// <summary>
        /// Provides the Method implementing the ValidateDelegate signature
        /// </summary>
        public abstract ValidatePermissionsDelegate ValidatePermissions
        {
            get;
        }

        public virtual PermissionBase OwnerPermission
        {
			get
			{
				throw new NotImplementedException("OwerPermission is not implemented on this Section");
			}
        }


        public virtual PermissionBase RolePermission( string roleName )
        {
            PermissionBase pb = PermissionSet[roleName] as PermissionBase;
            if(pb == null)
                pb = DefaultRolePermission;

            return pb;
        }

        string[] ownerArray = null;

		/// <summary>
		/// Validates if a user the owner of the current section. Owners are users who have
		/// elevated permissions but no specific roles assigned. If the user is the owner, 
		/// their role based security will be ignored
		/// </summary>
		/// <param name="user">User to evaluate</param>
		/// <returns>True if the user's UserName exists as part of the Owners property (split into an array)</returns>
        protected virtual bool IsOwner(User user)
        {
             if(ownerArray == null)
                ownerArray = Owners.Split(';');
     
            if(ownerArray != null && ownerArray[0].Length > 0)
            {
                foreach(string owner in ownerArray)
                {
                    if(string.Compare(owner,user.Username,true) == 0)
                        return true;
                }
            }

            return false;
        }

        

		/// <summary>
		/// A user can be in multiple roles. Resolved Permission Merges the permissions
		/// for the user and those valid for the current section
		/// </summary>
		/// <param name="user">User we are evaluating</param>
		/// <returns>A single unified permission for the section/user combination</returns>
        public PermissionBase ResolvePermission( User user ) 
        {
            if(IsOwner(user))
                return OwnerPermission;

            PermissionBase pbMaster = DefaultRolePermission;

            string[] roles = Roles.GetUserRoleNames(user.Username);
            PermissionBase pb = null;
            foreach(string role in roles)
            {
                pb = PermissionSet[role] as PermissionBase;  
                if(pb !=null)
                    pbMaster.Merge(pb);
            }

            return pbMaster;
        }		
        
        /// <summary>
        /// Specifies if autheticated users are able to post as anonymous.
        /// </summary>
        public bool EnableAnonymousPostingForUsers {
            get {
                try {
                    return bool.Parse( GetExtendedAttribute( "EnableAnonymousPostingForUsers" ) ); 
                } catch {
                    return false;
                }
            }
            set {
                SetExtendedAttribute( "EnableAnonymousPostingForUsers", value.ToString() );
            }
        }
    }
}
