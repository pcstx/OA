//------------------------------------------------------------------------------
// <copyright company="Telligent Systems">
//     Copyright (c) Telligent Systems Corporation.  All rights reserved.
// </copyright> 
//------------------------------------------------------------------------------

using System;

namespace GPRP.GPRPComponents 
{
	/// <summary>
	/// A hyperlink displayed in a blog link category
	/// </summary>
	public class Link : IComparable
	{

		#region Properties

		public Int32 LinkID {
			get {
				return _blogLinkID;
			}
			set {
				_blogLinkID = value;
			}
		}
		private Int32 _blogLinkID;

		public Int32 LinkCategoryID {
			get {
				return _blogLinkCategoryID;
			}
			set {
				_blogLinkCategoryID = value;
			}
		}
		private Int32 _blogLinkCategoryID;

		public String Title {
			get {
				return _title;
			}
			set {
				_title = value;
			}
		}
		private String _title;

		public String Url {
			get {
				return _url;
			}
			set {
				_url = value;
			}
		}
		private String _url;

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

		#region Constructor

		public Link() 
		{ }

		#endregion

		#region IComparable Members

		public int CompareTo(object obj)
		{
			if(obj is Link)
			{
				Link link = (Link)obj;
				return _sortOrder.CompareTo(link._sortOrder);
			}
			throw new ArgumentException("Specified object is not of type Link");
		}

		#endregion
	}
}
