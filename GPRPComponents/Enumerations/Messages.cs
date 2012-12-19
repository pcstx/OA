//------------------------------------------------------------------------------
// <copyright company="Telligent Systems">
//     Copyright (c) Telligent Systems Corporation.  All rights reserved.
// </copyright> 
//------------------------------------------------------------------------------

using System;

namespace GPRP.GPRPEnumerations
{
    /// <summary>
    /// The EmailTypeEnum enumeration determines what type of message is to be displayed
    /// </summary>
    public enum Messages {

        UnableToAdminister = 1,
        UnableToEditPost = 2,
        UnableToModerate = 3,
        DuplicatePost = 4,
        FileNotFound = 5,
        UnknownForum = 6,
        NewAccountCreated = 7,
        PostPendingModeration = 8,
        PostDoesNotExist = 9,
        PostIdParameterNotSpecified = 10,
        ProblemPosting = 11,
        UnableToViewMessage = 12,
        UserProfileUpdated = 13,
        UserDoesNotExist = 14,
        UserPasswordChangeSuccess = 15,
        UserPasswordChangeFailed = 16,
        InvalidCredentials = 17,
        NoUnansweredPosts = 18,
        NoPostsInForum = 19,
        NoUsersFound = 20,
        NoActivePosts = 21,
        PrivateMessageToSelfNotAllowed = 22,
        NewAccountPending = 23,
        NewAccountCreatedAutomatic = 24,
        UserAccountStillPending = 25,
        UserAccountBanned = 26,
        UnspecifiedLoginError = 27,
        UnableToSendEmail = 28
    }
}
