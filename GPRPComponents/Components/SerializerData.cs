//------------------------------------------------------------------------------
// <copyright company="Telligent Systems">
//     Copyright (c) Telligent Systems Corporation.  All rights reserved.
// </copyright> 
//------------------------------------------------------------------------------

using System;

namespace GPRP.GPRPComponents
{
    /// <summary>
    /// Container for get/set data from the CS datastore
    /// </summary>
    public struct SerializerData
    {
        [Obsolete]
        public byte[] Bytes;
        public string Keys;
        public string Values;

        [Obsolete]
        public bool HasBytes
        {
            get{ return Bytes != null && Bytes.Length > 0;}
        }
    }
}
