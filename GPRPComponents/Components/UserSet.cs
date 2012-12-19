//------------------------------------------------------------------------------
// <copyright company="Telligent Systems">
//     Copyright (c) Telligent Systems Corporation.  All rights reserved.
// </copyright> 
//------------------------------------------------------------------------------

using System;
using System.Collections;

namespace GPRP.GPRPComponents {
    /// <summary>
    /// Summary description for UserSet.
    /// </summary>
    public class UserSet {

        ArrayList users = new ArrayList();
        int totalRecords = 0;

        public int TotalRecords {
            get {
                return totalRecords;
            }
            set {
                totalRecords = value;
            }
        }

        public ArrayList Users {
            get {
                return users;
            }
        }

        public bool HasResults {
            get {
                if (users.Count > 0)
                    return true;
                return false;
            }
        }

    }
}
