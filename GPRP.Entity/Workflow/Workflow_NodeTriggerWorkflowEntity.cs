using System;
using System.Collections.Generic;
using System.Text;
namespace GPRP.Entity
{
    /// <summary>
    /// 实体
    /// </summary>
    public class Workflow_NodeTriggerWorkflowEntity
    {

        private int m_TriggerID;//编号
        private int m_WorkflowID;//父流程编号

        private int m_NodeID;//节点编号
        private int m_OPTime;//触发动作时机(0:节点到达时，1节点离开时，2当前节点操作时)
        private string m_OPCondition;//触发条件(流程中的字段值符合某条件)
        private int m_TriggerWFID;//触发的工作流编号
        private int m_TriggerWFCreator;//触发的工作流的创建者(0. 父流程的创建者1.父流程的节点操作者 2.父流程的字段值)
        private int m_WFCreateNode;//对应创建者类型1的节点编号
        private int m_WFCreateFieldID;//对应创建者类型2的字段编号
        private DateTime m_CreateDate;//创建日期
        private int m_CreateSID;//创建者
        private int m_IsCancel;//是否已失效：0-否，1-已失效
        private DateTime m_CancelDate;//失效日期
        private int m_CancelSID;//触发失效者


        /// <summary>
        ///编号
        /// </summary>
        public int TriggerID
        {
            get { return m_TriggerID; }
            set { m_TriggerID = value; }
        }
        /// <summary>
        ///父流程编号
        /// </summary>
        public int WorkflowID
        {
            get { return m_WorkflowID; }
            set { m_WorkflowID = value; }
        }
        /// <summary>
        ///节点编号
        /// </summary>
        public int NodeID
        {
            get { return m_NodeID; }
            set { m_NodeID = value; }
        }
        /// <summary>
        ///触发动作时机(0:节点到达时，1节点离开时，2当前节点操作时)
        /// </summary>
        public int OPTime
        {
            get { return m_OPTime; }
            set { m_OPTime = value; }
        }
        /// <summary>
        ///触发条件(流程中的字段值符合某条件)
        /// </summary>
        public string OPCondition
        {
            get { return m_OPCondition; }
            set { m_OPCondition = value; }
        }
        /// <summary>
        ///触发的工作流编号
        /// </summary>
        public int TriggerWFID
        {
            get { return m_TriggerWFID; }
            set { m_TriggerWFID = value; }
        }
        /// <summary>
        ///触发的工作流的创建者(0. 父流程的创建者1.父流程的节点操作者 2.父流程的字段值)
        /// </summary>
        public int TriggerWFCreator
        {
            get { return m_TriggerWFCreator; }
            set { m_TriggerWFCreator = value; }
        }
        /// <summary>
        ///对应创建者类型1的节点编号
        /// </summary>
        public int WFCreateNode
        {
            get { return m_WFCreateNode; }
            set { m_WFCreateNode = value; }
        }
        /// <summary>
        ///对应创建者类型2的字段编号
        /// </summary>
        public int WFCreateFieldID
        {
            get { return m_WFCreateFieldID; }
            set { m_WFCreateFieldID = value; }
        }

        /// <summary>
        ///创建日期
        /// </summary>
        public DateTime CreateDate
        {
            get { return m_CreateDate; }
            set { m_CreateDate = value; }
        }
        /// <summary>
        ///创建者

        /// </summary>
        public int CreateSID
        {
            get { return m_CreateSID; }
            set { m_CreateSID = value; }
        }
        /// <summary>
        ///是否已失效：0-否，1-已失效

        /// </summary>
        public int IsCancel
        {
            get { return m_IsCancel; }
            set { m_IsCancel = value; }
        }
        /// <summary>
        ///失效日期
        /// </summary>
        public DateTime CancelDate
        {
            get { return m_CancelDate; }
            set { m_CancelDate = value; }
        }
        /// <summary>
        ///触发失效者

        /// </summary>
        public int CancelSID
        {
            get { return m_CancelSID; }
            set { m_CancelSID = value; }
        }

    }
}