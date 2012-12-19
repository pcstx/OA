//------------------------------------------------------------------------------
// <copyright company="Telligent Systems">
//     Copyright (c) Telligent Systems Corporation.  All rights reserved.
// </copyright> 
//------------------------------------------------------------------------------

using System;
using GPRP.GPRPEnumerations;
namespace GPRP.GPRPComponents
{
    /// <summary>
    /// The Permission enum identifies how the bits stored in the mask apply. If the bit is on in the allow mask
    /// then the right is allowed, if the bit is turned on in the deny mask then the right is turned off
    /// 
    /// Add more bit values to this enum as you extend the functionality of the software
    /// </summary>
    public abstract class PermissionBase 
    {
        #region Private Members
        int				settingsID;
        Guid			roleID;
        string roleName;
        int sectionID;
        ApplicationType appType = ApplicationType.Unknown;
        //Permission
        Permission		allowMask;
        Permission		denyMask;
        bool			implied;
        #endregion

        #region Cnstr
        public PermissionBase() 
        {
            settingsID = -1;
            allowMask = 0;
            denyMask = (Permission)(long)-1;
            implied	= true;
        }

        public PermissionBase( int site, Guid role, string name, Permission allow, Permission deny, bool impliedPermissions ) 
        {
            settingsID = site;
            roleID = role;
            roleName = name;
            allowMask = allow;
            denyMask = deny;
            implied = impliedPermissions;
        }
        #endregion

        #region Core Public Properties
        public int SettingsID 
        {
            get{ return settingsID;}
            set{ settingsID = value; }
        }

        public Guid RoleID 
        {
            get{ return roleID; }
            set{ roleID = value; }
        }

        public string Name 
        {
            get{ return roleName; }
            set{ roleName = value; }
        }

        public int SectionID
        {
            get {return sectionID;}
            set {sectionID = value;}
        }
	
        public bool Implied 
        {
            get{ return implied; }
            set{ implied = value; }
        }

        public Permission AllowMask 
        {
            get{ return allowMask; }
            set{ allowMask = value; }
        }
       
        public Permission DenyMask 
        {
            get{ return denyMask; }
            set{ denyMask = value; }
        }

        public ApplicationType ApplicationType 
        {
            get{ return appType; }
            set{ appType = value; }
        }
        #endregion

        #region GetBin
        public bool GetBit( Permission mask ) 
        {
            bool bReturn = false;
				
            if(( denyMask & mask ) == mask )
                bReturn = false;

            if(( allowMask & mask ) == mask )
                bReturn = true;
				
            return bReturn;
        }
        #endregion

        #region Public Methods

        public void SetBit( Permission mask, AccessControlEntry accessControl ) 
        {

            switch( accessControl )
            {
                case AccessControlEntry.Allow:
                    allowMask	|= (Permission)((long)mask & (long)-1);
                    denyMask	&= ~(Permission)((long)mask & (long)-1);
                    break;
                case AccessControlEntry.NotSet:
                    allowMask	&= ~(Permission)((long)mask & (long)-1);
                    denyMask	&= ~(Permission)((long)mask & (long)-1);
                    break;
                default:
                    allowMask	&= ~(Permission)((long)mask & (long)-1);
                    denyMask	|= (Permission)((long)mask & (long)-1);
                    break;
            }
        }
		/// <summary>
		/// This method merges the supplied permissions with the current permissions to come up with an
		/// updated permission set. The logic is that and Implied Allow overrides an Implied Deny, but 
		/// and Explicit Deny overrides an Implicit Allow, while an Explicit Allow overrides an Explicit
		/// Deny. This gives us a least restrictive security system.
		/// </summary>
		/// <param name="permissionBase">The permission to merge with the current permission set</param>
        public void Merge( PermissionBase permissionBase ) 
        {
            this.allowMask	|= permissionBase.AllowMask;
            this.denyMask	|= permissionBase.DenyMask;

            if( this.implied ) 
            {
			
                if( permissionBase.Implied ) 
                {
                    this.allowMask	|= permissionBase.AllowMask;
                    this.denyMask	|= permissionBase.DenyMask;
                }
                else 
                {
					// this logic takes the DenyMasks and coverts the ON bits to off bits and the off bits to ON bits. This gives
					// us a reverse mask of the deny. Deny describes what is currently denied, but to turn off an allow bit we need
					// to perform an exclive or on the inverse of the deny mask. This has the result of turning any ALLOW bits off
					// that the deny bit was set to on (before doing the inverse).
                    this.allowMask	= (Permission)(( (long)this.allowMask & ( (long)-1 ^  (long)permissionBase.DenyMask )) | (long)permissionBase.AllowMask);
                    this.denyMask	|= permissionBase.DenyMask;
					this.implied = false;
                }
            }
            else 
            {

                if( permissionBase.Implied ) 
                {
//                    this.allowMask |= permissionBase.AllowMask;
					// take the implied allow mask, and turn off any bits, that are explicited denied
					this.allowMask	|= (Permission)(( (long)this.allowMask & ( (long)-1 ^  (long)this.DenyMask )) | (long)permissionBase.AllowMask);
					this.denyMask  |= permissionBase.DenyMask;
                }
                else 
                {
//                    this.allowMask	 = (Permission)(( (long)this.allowMask & ( (long)-1 ^ (long)permissionBase.DenyMask )) | (long)permissionBase.AllowMask);
					this.allowMask	|= permissionBase.AllowMask;
					this.denyMask	|= permissionBase.DenyMask;
                }
            }
        }

        #endregion

        public static void RedirectOrExcpetion(CSExceptionType csEx)
        {
            CSContext context = CSContext.Current;

            if(context.IsWebRequest && !context.IsAuthenticated)
            {
                context.Context.Response.Redirect(Globals.GetSiteUrls().Login);
                context.Context.Response.End();
            }
            else
                throw new CSException(csEx);
        }

        public static void RedirectOrExcpetion(CSExceptionType csEx, string message)
        {
            CSContext context = CSContext.Current;

            if(context.IsWebRequest && !context.IsAuthenticated)
            {
                context.Context.Response.Redirect(Globals.GetSiteUrls().Login);
                context.Context.Response.End();
            }
            else
                throw new CSException(csEx, message);
        }

    }

 
 

    /// <summary>
    /// Signature for a method which can check a user's permissions to a section
    /// </summary>
    public delegate void AccessCheckDelegate(Section section, Permission permission, User user, Post post);

    /// <summary>
    /// Signature for a mthod which can validate a user's permissions. returns true if the user can access the section
    /// </summary>
    public delegate bool ValidatePermissionsDelegate(Section section, Permission permission, User user, Post p);

}
