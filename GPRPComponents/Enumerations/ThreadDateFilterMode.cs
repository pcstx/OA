//------------------------------------------------------------------------------
// <copyright company="Telligent Systems">
//     Copyright (c) Telligent Systems Corporation.  All rights reserved.
// </copyright> 
//------------------------------------------------------------------------------

using System;

namespace GPRP.GPRPEnumerations
{

    public enum ThreadDateFilterMode {
        LastVisit,
        OneDay,
        TwoDays,
        ThreeDays,
        OneWeek,
        TwoWeeks,
        OneMonth,
        TwoMonths,
        ThreeMonths,
        SixMonths,
        OneYear,
        All
    }

    public enum ThreadUsersFilter {
        All,
        HideTopicsParticipatedIn,
        HideTopicsNotParticipatedIn,
        HideTopicsByAnonymousUsers,
        HideTopicsByNonAnonymousUsers
    }
}
