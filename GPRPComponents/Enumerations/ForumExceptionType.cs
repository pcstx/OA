//------------------------------------------------------------------------------
// <copyright company="Telligent Systems">
//     Copyright (c) Telligent Systems Corporation.  All rights reserved.
// </copyright> 
//------------------------------------------------------------------------------

using System;

namespace GPRP.GPRPEnumerations
{

    // Do Not change numbering
    public enum CSExceptionType {
        DataProvider = 0,
        AdministrationAccessDenied = 1,
        PostEditAccessDenied = 2,
        ModerateAccessDenied = 3,
        PostDuplicate = 4,
        FileNotFound = 5,
        SectionNotFound = 6,
        PostPendingModeration = 8,
        PostNotFound = 9,
        PostIDParameterNotSpecified = 10,
        PostProblem = 11,
        CreateUser = 36,
        UserAccountCreated = 7,
        UserAccountPending = 23,
        UserAccountCreatedAuto = 24,
        UserAccountDisapproved = 25,
        UserAccountBanned = 26,
        UserProfileUpdated = 13,
        UserNotFound = 14,
        UserPasswordChangeSuccess = 15,
        UserPasswordChangeFailed = 16,
        UserInvalidCredentials = 17,
        ForumNoUnansweredPosts = 18,
        PrivateMessagesDisabledByUser = 19,
        UserSearchNotFound = 20,
        ForumNoActivePosts = 21,
        PrivateMessageToSelfNotAllowd = 22,
        UserUnknownLoginError = 27,
        EmailUnableToSend = 28,
        UserAccountRegistrationDisabled = 29,
        UserLoginDisabled = 30,
        AccessDenied = 31,
        PostAccessDenied = 32,
        GroupNotFound = 33,
        EmailTemplateNotFound = 34,
        SearchUnknownError = 35,
        PostReplyAccessDenied = 36,
        PostAnnounceAccessDenied = 37,
        PostEditPermissionExpired = 38,
        PostLocked = 39,
        PostDeletePermissionExpired = 40,
        PostDeleteAccessDenied = 41,
        SkinNotSet = 42,
        SkinNotFound = 43,
        ReturnURLRequired = 44,
        SearchNoResults = 45,
		PostInvalidAttachmentType = 46,
		GeneralAccessDenied = 47,
		EmailSentToUser = 48,
		PostAttachmentTooLarge = 49,
		PostAttachmentsNotAllowed = 50,
        UserPasswordAnswerChangeSuccess = 51,
        UserPasswordAnswerChangeFailed = 52,
        RoleNotFound = 53,
        RoleUpdated = 54,
        RoleDeleted = 55,
        RoleOperationUnavailable = 56,
        ResourceNotFound = 57,
        PostCategoryNotFound = 58,
        WeblogNotFound = 59,
        WeblogPostNotFound = 60,
        PermissionApplicationUnknown = 61,
		RedirectFailure = 62,
        UnRegisteredSite = 63,
        SiteUrlDataProvider = 64,
		FloodDenied = 65,
		SiteSettingsInvalidXML = 66,
		PostDeleteAccessDeniedHasReplies = 67,
		ForumNoUnreadPosts = 68,
        UserPasswordResetSuccess = 69,

		UnauthorizedAccessException = 200, // 当操作系统因 I/O 错误或指定类型的安全错误而拒绝访问时所引发的异常
		SecurityException = 201,	// 检测到安全性错误时引发的异常。

        ApplicationStart = 997,
        ApplicationStop = 998,

        UnknownError = 999
    }

}