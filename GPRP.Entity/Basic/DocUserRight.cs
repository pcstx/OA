using System;
using System.Collections.Generic;
using System.Text;

namespace GPRP.Entity.Basic
{
   public class DocUserRight
    {
        private int m_UserId;
        private int m_FolderId;
        private string m_Permission;


        public int UserID
        {
            get { return m_UserId; }
            set { m_UserId = value; }
        }

        public int FolderID
        {
            get { return m_FolderId; }
            set { m_FolderId = value; }
        }

        public string Permission
        {
            get { return m_Permission; }
            set { m_Permission = value; }
        }
    }
}
