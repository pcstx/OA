//------------------------------------------------------------------------------
// <copyright company="Telligent Systems">
//     Copyright (c) Telligent Systems Corporation.  All rights reserved.
// </copyright> 
//------------------------------------------------------------------------------

using System;


namespace GPRP.GPRPComponents {

    public class Rating {
        User user;
        int rating = 0;

        public int Value {
            get { return rating; }
            set { rating = value; }
        }

        public User User {
            get { return user; }
            set { user = value; }
        }
    }
}
