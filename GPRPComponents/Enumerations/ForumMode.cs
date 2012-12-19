//------------------------------------------------------------------------------
// <copyright company="Telligent Systems">
//     Copyright (c) Telligent Systems Corporation.  All rights reserved.
// </copyright> 
//------------------------------------------------------------------------------

using System;

namespace GPRP.GPRPComponents {
	/// <summary>
	/// Summary description for ForumGroupViewMode.
	/// </summary>
	public enum ControlUserMode {
		User,
		Moderator,
		Administrator,
        BlogOwner,
        BlogSystemAdministrator,
        BlogUser,
		GalleryOwner,
		GallerySystemAdministrator,
		GalleryUser,
		CalendarOwner,
		CalendarSystemAdministrator,
		CalendarUsers
	}

	public enum ForumType {
		Normal		= 0,
		Link		= 10,
		KB			= 20,
		FAQ			= 30,
		Newsletter	= 40,
        Deleted     = 50,
        Reporting   = 60
	}
}