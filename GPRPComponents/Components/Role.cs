//------------------------------------------------------------------------------
// <copyright company="Telligent Systems">
//     Copyright (c) Telligent Systems Corporation.  All rights reserved.
// </copyright> 
//------------------------------------------------------------------------------

using System;
using System.Collections;

namespace GPRP.GPRPComponents {

    /// <summary>
    /// Summary description for Role.
    /// </summary>
    public class Role :	IComparable {

        Guid roleID = Guid.Empty;
        string name;
        string description;

        public Role() {
        }

        public Role (Guid roleID, string name) {
            this.roleID = roleID;
            this.name = name;
        }

        public Guid RoleID {
            get {
                return roleID;
            }
            set {
                roleID = value;
            }
        }

        public string Name {
            get {
                return name;
            }
            set {
                name = value;
            }
        }


        public string Description {
            get {
                return description;
            }
            set {
                description = value;
            }
        }    

		public override bool Equals(object obj) {
			Role rhs = obj as Role;

			if( rhs != null &&
				rhs.RoleID	== this.RoleID &&
				rhs.Name	== this.Name ) {

				return true;
			}
			else {
				return false;
			}
		}

        public bool IsRoleIDAssigned
        {
            get{ return RoleID != Guid.Empty;}
        }

		public override int GetHashCode()
		{
			return base.GetHashCode ();
		}


    
		#region IComparable Members

        public int CompareTo(object obj)
        {
            Role rhs = obj as Role;
            if( rhs != null ) 
            {
                if( this.RoleID == rhs.RoleID )
                    return 0;

                return string.Compare(this.Name,rhs.Name,true);
                //				if( this.RoleID < rhs.RoleID )
                //					return -1;
                //
                //				return 1;
                //			}
                //			else
                //				return -1;
            }
            else
                return -1;
        }

		#endregion
	}
}
