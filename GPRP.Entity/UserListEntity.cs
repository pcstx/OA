using System;
using System.Collections.Generic;
using System.Text;
namespace GPRP.Entity
{
    /// <summary>
    /// ʵ��
    /// </summary>
    public class UserListEntity
    {

        private int m_UserSerialID;//���
        private string m_UserID;//�û�ID
        private string m_PassWord;//����
        private string m_UserName;//�û���
        private string m_UserType;//�û�����
        private string m_UserTypeN;//�û�����
        private string m_UserEmail;//Email
        private string m_UserCode;//Ա�����
        private string m_DeptName;//��������
        private string m_UseFlag;//�Ƿ����
        private string m_CreateUser;//
        private DateTime m_CreateDate;//
        private string m_LastModifier;//
        private DateTime m_LastModifyDate;//
        private string m_EquipmentTypeList;//��ɴ�����豸�����б�
        /// <summary>
        ///���
        /// </summary>
        public int UserSerialID
        {
            get { return m_UserSerialID; }
            set { m_UserSerialID = value; }
        }
        /// <summary>
        ///�û�ID
        /// </summary>
        public string UserID
        {
            get { return m_UserID; }
            set { m_UserID = value; }
        }
        /// <summary>
        ///����
        /// </summary>
        public string PassWord
        {
            get { return m_PassWord; }
            set { m_PassWord = value; }
        }
        /// <summary>
        ///�û���
        /// </summary>
        public string UserName
        {
            get { return m_UserName; }
            set { m_UserName = value; }
        }
        /// <summary>
        ///�û�����
        /// </summary>
        public string UserType
        {
            get { return m_UserType; }
            set { m_UserType = value; }
        }
        /// <summary>
        ///�û�����
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
        ///Ա�����
        /// </summary>
        public string UserCode
        {
            get { return m_UserCode; }
            set { m_UserCode = value; }
        }
        /// <summary>
        ///��������
        /// </summary>
        public string DeptName
        {
            get { return m_DeptName; }
            set { m_DeptName = value; }
        }
        /// <summary>
        ///�Ƿ����
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