//------------------------------------------------------------------------------
// <copyright company="Telligent Systems">
//     Copyright (c) Telligent Systems Corporation.  All rights reserved.
// </copyright> 
//------------------------------------------------------------------------------

using System;

namespace GPRP.GPRPEnumerations {

    /// <summary>
    /// The ViewOptions enumeration determines how the posts for a particular forum are displayed.
    /// The options are NotSet, meaning the default is used; Flat; Mixed; and Threaded.
    /// </summary>
    public enum ViewOptions { 
        /// <summary>
        /// When the forum is visited by an anonymous user, their ViewOptions are NotSet.
        /// Pass this value in to have the default forum view setting used.
        /// </summary>
        NotSet = -1, 
        
        /// <summary>
        /// Specifies to display the forum in a Flat mode.
        /// </summary>
        Flat = 0, 
        
        /// <summary>
        /// Specifies to display the forum in the Mixed mode.
        /// </summary>
        Mixed = 1, 
        
        /// <summary>
        /// Specifies to display the forum in Threaded mode.
        /// </summary>
        Threaded = 2 
    }

}
