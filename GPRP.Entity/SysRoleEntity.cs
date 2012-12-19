using System;
using System.Data;
using System.Collections.Generic;
using System.Text;

namespace GPRP.Entity
{
    /// <summary>
    /// 实体
    /// </summary>
    public class SysRoleEntity
    {

        private int m_RoleID;//角色编号
        private string m_RoleName;//角色名称
        private string m_RoleDesc;//角色描述
        private int m_DisplayOrder;//显示顺序
        private string m_UseFlag;//是否可用
        private string m_CreateUser;//
        private DateTime m_CreateDate;//
        private string m_LastModifier;//
        private DateTime m_LastModifyDate;//
        private DataTable m_dtUserList;
        /// <summary>
        ///角色编号
        /// </summary>
        public int RoleID
        {
            get { return m_RoleID; }
            set { m_RoleID = value; }
        }
        /// <summary>
        ///角色名称
        /// </summary>
        public string RoleName
        {
            get { return m_RoleName; }
            set { m_RoleName = value; }
        }
        /// <summary>
        ///角色描述
        /// </summary>
        public string RoleDesc
        {
            get { return m_RoleDesc; }
            set { m_RoleDesc = value; }
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