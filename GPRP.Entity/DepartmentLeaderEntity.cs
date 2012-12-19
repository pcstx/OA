using System;
using System.Collections.Generic;
using System.Text;
namespace GPRP.Entity
{
    /// <summary>
    /// 实体
    /// </summary>
    public class DepartmentLeaderEntity
    {

        private int m_DeptID;//部门ID
        private int m_UserSerialID;//部门负责人
        private string m_lastModifier;//
        private DateTime m_lastModifyDate;//
        /// <summary>
        ///部门ID
        /// </summary>
        public int DeptID
        {
            get { return m_DeptID; }
            set { m_DeptID = value; }
        }
        /// <summary>
        ///部门负责人
        /// </summary>
        public int UserSerialID
        {
            get { return m_UserSerialID; }
            set { m_UserSerialID = value; }
        }
        /// <summary>
        ///
        /// </summary>
        public string lastModifier
        {
            get { return m_lastModifier; }
            set { m_lastModifier = value; }
        }
        /// <summary>
        ///
        /// </summary>
        public DateTime lastModifyDate
        {
            get { return m_lastModifyDate; }
            set { m_lastModifyDate = value; }
        }
    }
}