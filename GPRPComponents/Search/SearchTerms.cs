//------------------------------------------------------------------------------
// <copyright company="Telligent Systems">
//     Copyright (c) Telligent Systems Corporation.  All rights reserved.
// </copyright> 
//------------------------------------------------------------------------------

namespace GPRP.GPRPComponents
{
    public struct SearchTerms
    {
        public string[] And;
        public string[] Or;

		public bool HasTerms
		{
			get
			{
				return (And != null && And.Length > 0) || (Or != null && Or.Length > 0);
			}
		}
    }
}