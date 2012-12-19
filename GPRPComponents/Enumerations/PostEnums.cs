//------------------------------------------------------------------------------
// <copyright company="Telligent Systems">
//     Copyright (c) Telligent Systems Corporation.  All rights reserved.
// </copyright> 
//------------------------------------------------------------------------------

using System;

namespace GPRP.GPRPComponents {
    
    public enum ThreadStatus {
        NotSet = 0,
        Answered,
        NotAnswered
    }

    /// <summary>
    /// The CreateEditPostMode enumeration determines what mode the PostDisplay Web control assumes.
    /// The options are NewPost, ReplyToPost, and EditPost.
    /// </summary>
    public enum CreateEditPostMode { 
        /// <summary>
        /// Specifies that the user is creating a new post.
        /// </summary>
        NewPost, 
        
        /// <summary>
        /// Specifies that the user is replying to an existing post.
        /// </summary>
        ReplyToPost, 
        
        /// <summary>
        /// Specifies that a  moderator or administrator is editing an existing post.
        /// </summary>
        EditPost,
		/// <summary>
		/// Specifies that this post is a private message from one user to another
		/// </summary>
        NewPrivateMessage,
		/// <summary>
		/// Specifies that this post is being used to report another post in the system.
		/// </summary>
		ReportingPost,
		/// <summary>
		/// Specifies that this post is a private message from one user to another
		/// </summary>
		ReplyPrivateMessage
    }

    [Flags()]
    public enum PostConfiguration {
        NotSet = 0,
        IsAnonymousPost = 1
    }
}
