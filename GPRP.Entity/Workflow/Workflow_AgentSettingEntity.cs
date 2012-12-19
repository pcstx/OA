using System;
using System.Collections.Generic;
using System.Text;
namespace GPRP.Entity
{
    /// <summary>
    /// 实体
    /// </summary>
    public class Workflow_AgentSettingEntity
    {

        private int m_AgentID;//代理设置编号
        private int m_WorkflowID;//流程编号
        private int m_BeAgentPersonID;//被代理人的员工编号
        private int m_AgentPersonID;//代理人的员工编号
        private DateTime m_AgentStartDate;//代理日期起
        private DateTime m_AgentEndDate;//代理日期止
        private DateTime m_CreateDate;//创建日期
        private int m_Creator;//创建者
        private string m_IsCancel;//是否已取消
        private string m_AllowCycle;//是否允许递归代理
        private string m_AllowCreate;//是否允许代理人发起流程
        private int m_CancelOperator;//取消操作者
        private DateTime m_CancelDate;//取消时间

        /// <summary>
        ///代理设置编号
        /// </summary>
        public int AgentID
        {
            get { return m_AgentID; }
            set { m_AgentID = value; }
        }
        /// <summary>
        ///流程编号
        /// </summary>
        public int WorkflowID
        {
            get { return m_WorkflowID; }
            set { m_WorkflowID = value; }
        }
        /// <summary>
        ///被代理人的员工编号
        /// </summary>
        public int BeAgentPersonID
        {
            get { return m_BeAgentPersonID; }
            set { m_BeAgentPersonID = value; }
        }
        /// <summary>
        ///代理人的员工编号
        /// </summary>
        public int AgentPersonID
        {
            get { return m_AgentPersonID; }
            set { m_AgentPersonID = value; }
        }
        /// <summary>
        ///代理日期起
        /// </summary>
        public DateTime AgentStartDate
        {
            get { return m_AgentStartDate; }
            set { m_AgentStartDate = value; }
        }
        /// <summary>
        ///代理日期止
        /// </summary>
        public DateTime AgentEndDate
        {
            get { return m_AgentEndDate; }
            set { m_AgentEndDate = value; }
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
        public int Creator
        {
            get { return m_Creator; }
            set { m_Creator = value; }
        }
        /// <summary>
        ///是否已取消
        /// </summary>
        public string IsCancel
        {
            get { return m_IsCancel; }
            set { m_IsCancel = value; }
        }
        /// <summary>
        ///是否允许递归代理
        /// </summary>
        public string AllowCycle
        {
            get { return m_AllowCycle; }
            set { m_AllowCycle = value; }
        }
        /// <summary>
        ///是否允许代理人发起流程
        /// </summary>
        public string AllowCreate
        {
            get { return m_AllowCreate; }
            set { m_AllowCreate = value; }
        }
        /// <summary>
        ///取消操作者
        /// </summary>
        public int CancelOperator
        {
            get { return m_CancelOperator; }
            set { m_CancelOperator = value; }
        }
        /// <summary>
        ///取消时间
        /// </summary>
        public DateTime CancelDate
        {
            get { return m_CancelDate; }
            set { m_CancelDate = value; }
        }
    }
}