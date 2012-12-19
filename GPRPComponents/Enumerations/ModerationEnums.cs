//------------------------------------------------------------------------------
// <copyright company="Telligent Systems">
//     Copyright (c) Telligent Systems Corporation.  All rights reserved.
// </copyright> 
//------------------------------------------------------------------------------

using System;

namespace GPRP.GPRPEnumerations
{
    public enum ModeratePostSetting {
        ToggleLock,
        ToggleAnnouncement
    }

    public enum ModerateUserSetting {
        ToggleModerate = 0,
        ToggleApproval,
        ToggleAvatarApproved,
        ToggleForceLogin,
        PasswordReset,
        PasswordChanged,
        UserBanned,
        UserUnbanned,
        UserEdited,
        UserModerated,
        UserUnmoderated
    }

    public enum ModerationLevel {
        Moderated = 0,
        Unmoderated = 1,
        AutoUnmoderate = 2,
        CannotBeUnmoderated = 3,
        RequireGlobalModeratorApproval = 4,
        RequireAdminApproval = 5
    }

    public enum ModeratorActions {
        ApprovePost = 1,
        EditPost = 2,
        MovePost = 3,
        DeletePost = 4,
        LockPost = 5,
        UnlockPost = 6,
        MergePost = 7,
        SplitPost = 8,
        EditUser = 9,
        UnmoderateUser = 10,
        ModerateUser = 11,
        BanUser = 12,
        UnbanUser = 13,
        ResetPassword = 14,
        ChangePassword = 15,
        PostIsAnnouncement = 16,
        PostIsNotAnnoucement = 17,
        UnApprovePost = 18,
		DeleteSection = 19
    }
    
    public enum UserModerationAction {  
        MessageAction = 0,
        ForumAction = 1
    }
}
