using System;
using System.Data;
using System.Collections.Generic;
using System.Text;

namespace GPRP.Entity
{
    /// <summary>
    /// 实体
    /// </summary>
    public class SysGroupEntity
    {

        private int m_GroupID;//用户组编号
        private string m_GroupName;//用户组名称
        private string m_GroupDesc;//用户组描述
        private int m_GroupType;//1: 指定 9:All
        private int m_DisplayOrder;//显示顺序
        private string m_UseFlag;//是否可用
        private string m_CreateUser;//
        private DateTime m_CreateDate;//
        private string m_LastModifier;//
        private DateTime m_LastModifyDate;//
        private DataTable m_dtUserList;
        /// <summary>
        ///用户组编号
        /// </summary>
        public int GroupID
        {
            get { return m_GroupID; }
            set { m_GroupID = value; }
        }
        /// <summary>
        ///用户组名称
        /// </summary>
        public string GroupName
        {
            get { return m_GroupName; }
            set { m_GroupName = value; }
        }
        /// <summary>
        ///用户组描述
        /// </summary>
        public string GroupDesc
        {
            get { return m_GroupDesc; }
            set { m_GroupDesc = value; }
        }
        /// <summary>
        ///1: 指定 9:All
        /// </summary>
        public int GroupType
        {
            get { return m_GroupType; }
            set { m_GroupType = value; }
        }
        /// <summary>
        ///显示顺序
        /// </summary>
        public int DisplayOrder
        {
            get { return m_DisplayOrder; }
            set { m_DisplayOrder = value; }
        }
        /// <summary>
        ///是否可用
        /// </summary>
        public string UseFlag
        {
            get { return m_UseFlag; }
            set { m_UseFlag = value; }
        }
        /// <summary>
        ///
        /// </summary>
        public string CreateUser
        {
            get { return m_CreateUser; }
            set { m_CreateUser = value; }
        }
        /// <summary>
        ///
        /// </summary>
        public DateTime CreateDate
        {
            get { return m_CreateDate; }
            set { m_CreateDate = value; }
        }
        /// <summary>
        ///
        /// </summary>
        public string LastModifier
        {
            get { return m_LastModifier; }
            set { m_LastModifier = value; }
        }
        /// <summary>
        ///
        /// </summary>
        public DateTime LastModifyDate
        {
            get { return m_LastModifyDate; }
            set { m_LastModifyDate = value; }
        }
        /// <summary>
        ///
        /// </summary>
        public DataTable dtUserList
        {
            get { return m_dtUserList; }
            set { m_dtUserList = value; }
        }
    }
}