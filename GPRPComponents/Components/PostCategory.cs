//------------------------------------------------------------------------------
// <copyright company="Telligent Systems">
//     Copyright (c) Telligent Systems Corporation.  All rights reserved.
// </copyright> 
//------------------------------------------------------------------------------

using System;
using System.Data.SqlTypes;
using GPRP.GPRPEnumerations;
namespace GPRP.GPRPComponents 
{
	/// <summary>
	/// Summary description for PostCategories.
	/// </summary>
	public class PostCategory : IComparable
	{

		#region Private Members

		private int _categoryID;
		private int _sectionID;
		private string _name;
		private CategoryType _categoryType;
		private bool _isEnabled;
		private int _parentID;
		private string _description;
		private string _path;
		private DateTime _dateCreated = SqlDateTime.MinValue.Value;

		// Derived properties
		private int _totalThreads;
		private int _totalSubThreads;
		private DateTime _mostRecentPostDate = SqlDateTime.MinValue.Value;
		private DateTime _mostRecentSubPostDate = SqlDateTime.MinValue.Value;

		#endregion


		#region Constructor

		public PostCategory() 
		{ }

		#endregion


		#region Public Properties

		public int CategoryID
		{
			get { return _categoryID; }
			set { _categoryID = value; }
		}

		public int SectionID
		{
			get { return _sectionID; }
			set { _sectionID = value; }
		}

		public string Name
		{
			get { return _name; }
			set { _name = value; }
		}

		public CategoryType CategoryType
		{
			get { return _categoryType; }
			set { _categoryType = value; }
		}

		public bool IsEnabled
		{
			get { return _isEnabled; }
			set { _isEnabled = value; }
		}

		public int ParentID
		{
			get { return _parentID; }
			set { _parentID = value; }
		}

		public string Description
		{
			get { return _description; }
			set { _description = value; }
		}

		public DateTime DateCreated
		{
			get { return _dateCreated; }
			set { _dateCreated = value; }
		}

		public int TotalThreads
		{
			get { return _totalThreads; }
			set { _totalThreads = value; }
		}

		public int TotalSubThreads
		{
			get { return _totalSubThreads; }
			set { _totalSubThreads = value; }
		}

		public DateTime MostRecentPostDate
		{
			get { return _mostRecentPostDate; }
			set { _mostRecentPostDate = value; }
		}

		public DateTime MostRecentSubPostDate
		{
			get { return _mostRecentSubPostDate; }
			set { _mostRecentSubPostDate = value; }
		}

		public string Path
		{
			get { return _path; }
			set { _path = value; }
		}

		#endregion


		#region IComparable Members

		public int CompareTo(object obj)
		{
			if(obj is PostCategory)
			{
				PostCategory category = (PostCategory)obj;
				return _name.CompareTo(category._name);
			}
			throw new ArgumentException("Specified object is not of type PostCategory");
		}

		#endregion
	}
}
