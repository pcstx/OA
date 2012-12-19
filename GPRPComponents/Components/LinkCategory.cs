//------------------------------------------------------------------------------
// <copyright company="Telligent Systems">
//     Copyright (c) Telligent Systems Corporation.  All rights reserved.
// </copyright> 
//------------------------------------------------------------------------------

using System;

namespace GPRP.GPRPComponents 
{
	/// <summary>
	/// A category into which links will be placed
	/// </summary>
	public class LinkCategory : IComparable
	{
		#region Constructor

		public LinkCategory() 
		{ }

		#endregion


		#region Properties

		public Int32 LinkCategoryID {
			get {
				return _blogLinkCategoryID;
			}
			set {
				_blogLinkCategoryID = value;
			}
		}
		private Int32 _blogLinkCategoryID;

		public Int32 ForumID {
			get {
				return _forumID;
			}
			set {
				_forumID = value;
			}
		}
		private Int32 _forumID;

		public String Name {
			get {
				return _name;
			}
			set {
				_name = value;
			}
		}
		private String _name;

		public String Description {
			get {
				return _description;
			}
			set {
				_description = value;
			}
		}
		private String _description;

		public Boolean IsEnabled {
			get {
				return _isEnabled;
			}
			set {
				_isEnabled = value;
			}
		}
		private Boolean _isEnabled;

		public Int32 SortOrder {
			get {
				return _sortOrder;
			}
			set {
				_sortOrder = value;
			}
		}
		private Int32 _sortOrder = 0;

		#endregion


		#region IComparable Members

		public int CompareTo(object obj)
		{
			if(obj is LinkCategory)
			{
				LinkCategory category = (LinkCategory)obj;
				return _sortOrder.CompareTo(category._sortOrder);
			}
			throw new ArgumentException("Specified object is not of type LinkCategory");
		}

		#endregion
	}
}
