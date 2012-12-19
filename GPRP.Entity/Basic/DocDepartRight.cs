using System;
using System.Collections.Generic;
using System.Text;


namespace GPRP.Entity.Basic
{
    public class DocDepartRight 
    {
        private int m_DepartmentId;
        private int m_FolderId;
        private string m_Permission;

        public int DepartMentID
        {
            get { return m_DepartmentId; }
            set { m_DepartmentId = value; }
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
