//------------------------------------------------------------------------------
// <copyright company="Telligent Systems">
//     Copyright (c) Telligent Systems Corporation.  All rights reserved.
// </copyright> 
//------------------------------------------------------------------------------

using System;
using System.Collections;
using GPRP.GPRPEnumerations;


namespace GPRP.GPRPComponents {

    /// <summary>
	/// This class defines the properties that makeup a forum.
	/// </summary>
	public class Group : IComparable, ICloneable    {
		int groupID;
        string name;
        ArrayList sections;
        int sortOrder = 0;
        bool hideForums = false;
        ApplicationType _applicationType;
        string newsgroupName;

        public Group() {}

        public Group(string name) {
            this.name = name;
        }

        public int CompareTo(object value) {

            if (value == null) return 1;

            int compareOrder = ((Group)value).SortOrder;

            if (this.SortOrder == compareOrder) return 0;
            if (this.SortOrder < compareOrder) return -1;
            if (this.SortOrder > compareOrder) return 1;
            return 0;
        }

		/*************************** PROPERTY STATEMENTS *****************************/
		/// <summary>
		/// Specifies the unique identifier for the each forum.
		/// </summary>

        public int GroupID {
            get { 
                return groupID; 
            }

            set {
                if (value < 0)
                    groupID = 0;
                else
                    groupID = value;
            }
        }

        public ArrayList Sections {
            get { 
                return sections; 
            }
            set { sections = value; }
        }

        public bool HasSections
        {
            get { return (Sections != null && Sections.Count > 0);}   
        }

        public String Name {
            get { return name; }
            set { name = value; }
        }

        
        
        public string NewsgroupName {
            get { return newsgroupName.ToLower(); }
            set { newsgroupName = value; }
             
        }

        public int SortOrder {
            get { return sortOrder; }
            set { sortOrder = value; }
        }

        public bool HideSections {
            get { return hideForums; }
            set { hideForums = value; }
        }

        /// <summary>
        /// Property ApplicationType (ApplicationType)
        /// </summary>
        public ApplicationType ApplicationType
        {
            get {  return this._applicationType; }
            set {  this._applicationType = value; }
        }
        #region ICloneable Members

        public object Clone()
        {
            Group g = new Group(this.name);
            g.Sections = this.sections;
            g.sortOrder = this.sortOrder;
            g.groupID = this.groupID;
            g._applicationType = this._applicationType;
            g.newsgroupName = this.newsgroupName;
            return g;
        }

        #endregion
    }
}
