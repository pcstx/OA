//------------------------------------------------------------------------------
// <copyright company="Telligent Systems">
//     Copyright (c) Telligent Systems Corporation.  All rights reserved.
// </copyright> 
//------------------------------------------------------------------------------

using System;
using System.Collections;

namespace GPRP.GPRPComponents {

	/// <summary>
	/// Summary description for AuditSet.
	/// </summary>
	public class AuditSet {

        ArrayList records = new ArrayList();
        int totalRecords = 0;

        public int TotalRecords {
            get {
                return totalRecords;
            }
            set {
                totalRecords = value;
            }
        }

        public ArrayList Records {
            get {
                return records;
            }
        }

        public bool HasResults {
            get {
                if (records.Count > 0)
                    return true;
                return false;
            }
        }
	}
}
