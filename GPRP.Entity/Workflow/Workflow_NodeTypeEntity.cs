using System;
using System.Collections.Generic;
using System.Text;
namespace GPRP.Entity
{
    /// <summary>
    /// 实体
    /// </summary>
    public class Workflow_NodeTypeEntity
    {

        private int m_NodeTypeID;//类型编号
        private string m_NodeTypeName;//类型说明
        private int m_DisplayOrder;//显示顺序
        /// <summary>
        ///类型编号
        /// </summary>
        public int NodeTypeID
        {
            get { return m_NodeTypeID; }
            set { m_NodeTypeID = value; }
        }
        /// <summary>
        ///类型说明
        /// </summary>
        public string NodeTypeName
        {
            get { return m_NodeTypeName; }
            set { m_NodeTypeName = value; }
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