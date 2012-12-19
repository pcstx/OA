using System;
using System.Collections.Generic;
using System.Text;
namespace GPRP.Entity
{
    /// <summary>
    /// 实体
    /// </summary>
    public class Workflow_FormFieldGroupEntity
    {
        private int m_GroupID;//明细组ID
        private string m_GroupName;//明细组名称
        private string m_GroupDesc;//明细组描述        private int m_FormID;//表单ID
        private int m_DisplayOrder;//显示顺序
        /// <summary>
        ///明细组ID
        /// </summary>
        public int GroupID
        {
            get { return m_GroupID; }
            set { m_GroupID = value; }
        }
        /// <summary>
        ///明细组名称        /// </summary>
        public string GroupName
        {
            get { return m_GroupName; }
            set { m_GroupName = value; }
        }
        /// <summary>
        ///明细组描述        /// </summary>
        public string GroupDesc
        {
            get { return m_GroupDesc; }
            set { m_GroupDesc = value; }
        }
        /// <summary>
        ///表单ID
        /// </summary>
        public int FormID
        {
            get { return m_FormID; }
            set { m_FormID = value; }
        }
        /// <summary>
        ///显示顺序
        /// </summary>
        public int DisplayOrder
        {
            get { return m_DisplayOrder; }
            set { m_DisplayOrder = value; }
        }
    }
}