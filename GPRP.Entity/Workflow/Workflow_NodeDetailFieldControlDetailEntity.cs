using System;
using System.Collections.Generic;
using System.Text;
namespace GPRP.Entity
{
    /// <summary>
    /// 实体
    /// </summary>
    public class Workflow_NodeDetailFieldControlDetailEntity
    {
        private int m_NodeID;//节点ID
        private int m_GroupID;//明细组
        private int m_FieldID;//字段ID
        private int m_IsView;//是否显示
        private int m_IsEdit;//是否可编辑
        private int m_IsMandatory;//是否必填
        private int m_BasicValidType;//基本验证方式
        private int m_ValidTimeType;//验证时机
        /// <summary>
        ///节点ID
        /// </summary>
        public int NodeID
        {
            get { return m_NodeID; }
            set { m_NodeID = value; }
        }
        /// <summary>
        ///明细组        /// </summary>
        public int GroupID
        {
            get { return m_GroupID; }
            set { m_GroupID = value; }
        }
        /// <summary>
        ///字段ID
        /// </summary>
        public int FieldID
        {
            get { return m_FieldID; }
            set { m_FieldID = value; }
        }
        /// <summary>
        ///是否显示
        /// </summary>
        public int IsView
        {
            get { return m_IsView; }
            set { m_IsView = value; }
        }
        /// <summary>
        ///是否可编辑        /// </summary>
        public int IsEdit
        {
            get { return m_IsEdit; }
            set { m_IsEdit = value; }
        }
        /// <summary>
        ///是否必填
        /// </summary>
        public int IsMandatory
        {
            get { return m_IsMandatory; }
            set { m_IsMandatory = value; }
        }
        /// <summary>
        ///基本验证方式
        /// </summary>
        public int BasicValidType
        {
            get { return m_BasicValidType; }
            set { m_BasicValidType = value; }
        }
        /// <summary>
        ///验证时机
        /// </summary>
        public int ValidTimeType
        {
            get { return m_ValidTimeType; }
            set { m_ValidTimeType = value; }
        }
    }
}