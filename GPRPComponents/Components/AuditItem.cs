//------------------------------------------------------------------------------
// <copyright company="Telligent Systems">
//     Copyright (c) Telligent Systems Corporation.  All rights reserved.
// </copyright> 
//------------------------------------------------------------------------------

using System;
using GPRP.GPRPEnumerations;

namespace GPRP.GPRPComponents
{

    /// <summary>
    /// This class encapsulates the an audit entry.
    /// </summary>
	public class AuditItem {

        #region Members
        ModeratorActions action;
        int postID = -1;
        int userID = -1;
        string userName = "";
        int sectionID = -1;
        int moderatorID = -1;
        string moderatorName = "";
        DateTime dateModerated = DateTime.MinValue;
        string notes = "";
        #endregion

        #region Properties
        public ModeratorActions Action {
            get { return action; }
            set { action = value; }
        }

        public int PostID {
            get { return postID; }
            set { postID = value; }
        }

        public int UserID {
            get { return userID; }
            set { userID = value; }
        }

        public string UserName {
            get { return userName; }
            set { userName = value; }
        }

        public int SectionID {
            get { return sectionID; }
            set { sectionID = value; }
        }

        public int ModeratorID {
            get { return moderatorID; }
            set { moderatorID = value; }
        }

        public string ModeratorName {
            get { return moderatorName; }
            set { moderatorName = value; }
        }

        public DateTime DateModerated {
            get { return dateModerated; }
            set { dateModerated = value; }
        }

        public string Notes {
            get { return notes; }
            set { notes = value; }
        }
        #endregion        
	}
}
