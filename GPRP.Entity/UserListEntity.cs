using System;
using System.Collections.Generic;
using System.Text;
namespace GPRP.Entity
{
    /// <summary>
    /// 实体
    /// </summary>
    public class UserListEntity
    {

        private int m_UserSerialID;//序号
        private string m_UserID;//用户ID
        private string m_PassWord;//密码
        private string m_UserName;//用户名
        private string m_UserType;//用户类型
        private string m_UserTypeN;//用户类型
        private string m_UserEmail;//Email
        private string m_UserCode;//员工编号
        private string m_DeptName;//所属部门
        private string m_UseFlag;//是否可用
        private string m_CreateUser;//
        private DateTime m_CreateDate;//
        private string m_LastModifier;//
        private DateTime m_LastModifyDate;//
        private string m_EquipmentTypeList;//其可处理的设备分类列表
        /// <summary>
        ///序号
        /// </summary>
        public int UserSerialID
        {
            get { return m_UserSerialID; }
            set { m_UserSerialID = value; }
        }
        /// <summary>
        ///用户ID
        /// </summary>
        public string UserID
        {
            get { return m_UserID; }
            set { m_UserID = value; }
        }
        /// <summary>
        ///密码
        /// </summary>
        public string PassWord
        {
            get { return m_PassWord; }
            set { m_PassWord = value; }
        }
        /// <summary>
        ///用户名
        /// </summary>
        public string UserName
        {
            get { return m_UserName; }
            set { m_UserName = value; }
        }
        /// <summary>
        ///用户类型
        /// </summary>
        public string UserType
        {
            get { return m_UserType; }
            set { m_UserType = value; }
        }
        /// <summary>
        ///用户类型
        /// </summary>
        public string UserTypeN
        {
            get { return m_UserTypeN; }
            set { m_UserTypeN = value; }
        }
        /// <summary>
        ///Email
        /// </summary>
        public string UserEmail
        {
            get { return m_UserEmail; }
            set { m_UserEmail = value; }
        }
        /// <summary>
        ///员工编号
        /// </summary>
        public string UserCode
        {
            get { return m_UserCode; }
            set { m_UserCode = value; }
        }
        /// <summary>
        ///所属部门
        /// </summary>
        public string DeptName
        {
            get { return m_DeptName; }
            set { m_DeptName = value; }
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

        public string EquipmentTypeList
            {
            get { return m_EquipmentTypeList; }
            set { m_EquipmentTypeList = value; }

            }
    }
}