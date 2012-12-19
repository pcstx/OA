//------------------------------------------------------------------------------
// <copyright company="Telligent Systems">
//     Copyright (c) Telligent Systems Corporation.  All rights reserved.
// </copyright> 
//------------------------------------------------------------------------------

using System;
using System.Collections;

namespace GPRP.GPRPComponents {

    /// <summary>
    /// Summary description for UserSet.
    /// </summary>
    public class PostSet {

        ArrayList posts = new ArrayList();
        ArrayList replies = null;
        Post post =null;
        int totalRecords = 0;

        public int TotalRecords {
            get {
                return totalRecords;
            }
            set {
                totalRecords = value;
            }
        }

        public ArrayList Posts {
            get {
                return posts;
            }
        }

        public bool HasResults {
            get {
                if (posts.Count > 0)
                    return true;
                return false;
            }
        }

        public Post ThreadStarter
        {
            get{return post;}
        }

        public ArrayList Replies
        {
            get{ return replies;}
        }

        public bool Organize()
        {
            if(replies == null)
            {
                replies = new ArrayList();
                foreach(Post p in posts)
                {
                    if(p.PostLevel != 1)
                    {
                        replies.Add(p);
                    }
                    else
                    {
                        post = p;
                    }
                }
            }
            //No need to use the organized posts if the main
            //partent can not be found
            return ThreadStarter != null;
        }
        
    }
}
