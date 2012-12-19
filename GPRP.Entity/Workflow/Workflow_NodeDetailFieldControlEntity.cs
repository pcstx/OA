using System;
using System.Collections.Generic;
using System.Text;
namespace GPRP.Entity
{
    /// <summary>
    /// 实体
    /// </summary>
    public class Workflow_NodeDetailFieldControlEntity
    {

        private int m_NodeID;//节点ID
        private int m_GroupID;//明细组ID
        private int m_IsView;//是否显示
        private int m_IsAdd;//是否可增加
        private int m_IsEdit;//是否可编辑
        private int m_IsDelete;//是否可删除
        /// <summary>
        ///节点ID
        /// </summary>
        public int NodeID
        {
            get { return m_NodeID; }
            set { m_NodeID = value; }
        }
        /// <summary>
        ///明细组ID
        /// </summary>
        public int GroupID
        {
            get { return m_GroupID; }
            set { m_GroupID = value; }
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
        ///是否可增加        /// </summary>
        public int IsAdd
        {
            get { return m_IsAdd; }
            set { m_IsAdd = value; }
        }
        /// <summary>
        ///是否可编辑        /// </summary>
        public int IsEdit
        {
            get { return m_IsEdit; }
            set { m_IsEdit = value; }
        }
        /// <summary>
        ///是否可删除        /// </summary>
        public int IsDelete
        {
            get { return m_IsDelete; }
            set { m_IsDelete = value; }
        }
    }
}