//------------------------------------------------------------------------------
// <copyright company="Telligent Systems">
//     Copyright (c) Telligent Systems Corporation.  All rights reserved.
// </copyright> 
//------------------------------------------------------------------------------

using System;

namespace GPRP.GPRPComponents {
    /// <summary>
    /// Summary description for SearchResultSet.
    /// </summary>
    public class SearchResultSet : PostSet {

        double searchDuration = 0;

        /// <summary>
        /// Returns the value in milliseconds that it took for the search to complete
        /// </summary>
        public double SearchDuration {
            get {
                return searchDuration;
            }
            set {
                searchDuration = value;
            }
        }

    }
}
