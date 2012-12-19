using System;
using System.Collections.Generic;
using System.Text;

namespace GPRP.Entity.Basic
{
   public class DocSysRoleRight
    {
        private int m_SysRoleId;
        private int m_FolderId;
        private string m_Permission;

        public int SysRoldID
        {
            get { return m_SysRoleId; }
            set { m_SysRoleId = value; }
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
