using System;
using System.Collections.Generic;
using System.Text;
namespace GPRP.Entity
{
    /// <summary>
    /// 实体
    /// </summary>
    public class Workflow_NodeLinkEntity
    {

        private int m_LinkID;//出口编号
        private string m_LinkName;//出口名称
        private int m_WorkflowID;//工作流编号
        private int m_StartNodeID;//起始节点
        private int m_TargetNodeID;//目标节点
        private int m_IsRejected;//是否退回转向
        private string m_Creator;//
        private DateTime m_CreateDate;//
        private string m_lastModifier;//
        private DateTime m_lastModifyDate;//
        /// <summary>
        ///出口编号
        /// </summary>
        public int LinkID
        {
            get { return m_LinkID; }
            set { m_LinkID = value; }
        }
        /// <summary>
        ///出口名称
        /// </summary>
        public string LinkName
        {
            get { return m_LinkName; }
            set { m_LinkName = value; }
        }
        /// <summary>
        ///工作流编号        /// </summary>
        public int WorkflowID
        {
            get { return m_WorkflowID; }
            set { m_WorkflowID = value; }
        }
        /// <summary>
        ///起始节点
        /// </summary>
        public int StartNodeID
        {
            get { return m_StartNodeID; }
            set { m_StartNodeID = value; }
        }
        /// <summary>
        ///目标节点
        /// </summary>
        public int TargetNodeID
        {
            get { return m_TargetNodeID; }
            set { m_TargetNodeID = value; }
        }
        /// <summary>
        ///是否退回转向        /// </summary>
        public int IsRejected
        {
            get { return m_IsRejected; }
            set { m_IsRejected = value; }
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

    public class NodeLink : Workflow_NodeLinkEntity
    {
        public string StartNodeName { get; set; }
        public string TargetNodeName { get; set; }
        public string SqlCondition { get; set; }
        public string x { get; set; }
        public string y { get; set; }
        public List<Workflow_NodeConditionEntity> NodeCondition { get; set; }
    } 
}