//------------------------------------------------------------------------------
// <copyright company="Telligent Systems">
//     Copyright (c) Telligent Systems Corporation.  All rights reserved.
// </copyright> 
//------------------------------------------------------------------------------

using System;

namespace GPRP.GPRPEnumerations
{
    /// <summary>
    /// Indicates the return status for creating a new user.
    /// </summary>
    public enum CreateUserStatus { 
        /// <summary>
        /// The user was not created for some unknown reason.
        /// </summary>
        UnknownFailure, 
        
        /// <summary>
        /// The user's account was successfully created.
        /// </summary>
        Created, 
        
        /// <summary>
        /// The user's account was not created because the user's desired username is already being used.
        /// </summary>
        DuplicateUsername, 
        
        /// <summary>
        /// The user's account was not created because the user's email address is already being used.
        /// </summary>
        DuplicateEmailAddress, 
        
        /// <summary>
        /// The user's account was not created because the user's desired username did not being with an
        /// alphabetic character.
        /// </summary>
        InvalidFirstCharacter,

        /// <summary>
        /// The username has been previously disallowed.
        /// </summary>
        DisallowedUsername,

        /// <summary>
        /// The user record has been updated
        /// </summary>
        Updated,

        /// <summary>
        /// The user record has been deleted
        /// </summary>
        Deleted,

        InvalidQuestionAnswer,

		InvalidPassword, 

        
		/// <summary>
		/// Í«≥∆÷ÿ∏¥
		/// </summary>
		DuplicateNickname

       
    }
}
