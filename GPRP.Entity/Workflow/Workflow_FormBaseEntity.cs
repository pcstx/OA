using System;
using System.Collections.Generic;
using System.Text;
namespace GPRP.Entity
{
    /// <summary>
    /// 实体
    /// </summary>
    public class Workflow_FormBaseEntity
    {

        private int m_FormID;//编号
        private string m_FormName;//表单名称
        private string m_FormDesc;//表单描述
        private int m_FormTypeID;//表单分类
        private int m_DisplayOrder;//显示顺序
        private string m_Useflag;//是否使用
        private string m_Creator;//
        private DateTime m_CreateDate;//
        private string m_lastModifier;//
        private DateTime m_lastModifyDate;//
        /// <summary>
        ///编号
        /// </summary>
        public int FormID
        {
            get { return m_FormID; }
            set { m_FormID = value; }
        }
        /// <summary>
        ///表单名称
        /// </summary>
        public string FormName
        {
            get { return m_FormName; }
            set { m_FormName = value; }
        }
        /// <summary>
        ///表单描述
        /// </summary>
        public string FormDesc
        {
            get { return m_FormDesc; }
            set { m_FormDesc = value; }
        }
        /// <summary>
        ///表单分类
        /// </summary>
        public int FormTypeID
        {
            get { return m_FormTypeID; }
            set { m_FormTypeID = value; }
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
        ///是否使用
        /// </summary>
        public string Useflag
        {
            get { return m_Useflag; }
            set { m_Useflag = value; }
        }
        /// <summary>
        ///
        /// </summary>
        public string Creator
        {
            get { return m_Creator; }
            set { m_Creator = value; }
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