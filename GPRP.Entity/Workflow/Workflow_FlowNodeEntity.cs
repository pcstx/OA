using System;
using System.Collections.Generic;
using System.Text;
namespace GPRP.Entity
{
    /// <summary>
    /// 实体
    /// </summary>
    public class Workflow_FlowNodeEntity
    {

        private int m_NodeID;//节点编号
        private string m_NodeName;//节点名称
        private string m_NodeDesc;//节点描述
        private int m_WorkflowID;//工作流编号
        private int m_NodeTypeID;//节点类型
        private int m_DisplayOrder;//显示顺序
        private int m_IsOverTime;//是否超时提醒
        private string m_OverTimeLen;//超时时长(H)
        private int m_SignType;//是否会签
        private string m_WithdrawTypeID;//强制回收
        private int m_ArchiveFlag;//强制归档
        /// <summary>
        ///节点编号
        /// </summary>
        public int NodeID
        {
            get { return m_NodeID; }
            set { m_NodeID = value; }
        }
        /// <summary>
        ///节点名称
        /// </summary>
        public string NodeName
        {
            get { return m_NodeName; }
            set { m_NodeName = value; }
        }
        /// <summary>
        ///节点描述
        /// </summary>
        public string NodeDesc
        {
            get { return m_NodeDesc; }
            set { m_NodeDesc = value; }
        }
        /// <summary>
        ///工作流编号        /// </summary>
        public int WorkflowID
        {
            get { return m_WorkflowID; }
            set { m_WorkflowID = value; }
        }
        /// <summary>
        ///节点类型
        /// </summary>
        public int NodeTypeID
        {
            get { return m_NodeTypeID; }
            set { m_NodeTypeID = value; }
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
        ///是否超时提醒
        /// </summary>
        public int IsOverTime
        {
            get { return m_IsOverTime; }
            set { m_IsOverTime = value; }
        }
        /// <summary>
        ///超时时长(H)
        /// </summary>
        public string OverTimeLen
        {
            get { return m_OverTimeLen; }
            set { m_OverTimeLen = value; }
        }
        /// <summary>
        ///是否会签
        /// </summary>
        public int SignType
        {
            get { return m_SignType; }
            set { m_SignType = value; }
        }
        /// <summary>
        ///强制回收
        /// </summary>
        public string WithdrawTypeID
        {
            get { return m_WithdrawTypeID; }
            set { m_WithdrawTypeID = value; }
        }
        /// <summary>
        ///强制归档
        /// </summary>
        public int ArchiveFlag
        {
            get { return m_ArchiveFlag; }
            set { m_ArchiveFlag = value; }
        }
    }

    public class Workflow_FlowNodeEntity2:Workflow_FlowNodeEntity
    {

        private int x;     
        private int y; 
        private int width; 
        private int height; 
       
        public int X
        {
            get { return x; }
            set { x = value; }
        }

        public int Y
        {
            get { return y; }
            set { y = value; }
        }

        public int Width
        {
            get { return width; }
            set { width = value; }
        }

        public int Height
        {
            get { return height; }
            set { height = value; }
        }

    }

}