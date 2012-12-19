using System;
using System.Collections.Generic;
using System.Text;
namespace GPRP.Entity
{
    /// <summary>
    /// 实体
    /// </summary>
    public class Workflow_RequestBaseEntity
    {

        private int m_RequestID;//
        private int m_WorkflowID;//
        private int m_LastNodeID;//
        private string m_LastRuleID;//
        private string m_LastRuleType;//
        private int m_LastNodeType;//
        private int m_LastDeptLevel;//
        private string m_LastOperatorID;//
        private int m_CurrentNodeID;//
        private string m_CurrentRuleID;//
        private string m_CurrentRuleType;//
        private int m_CurrentNodeType;//
        private int m_CurrentDeptLevel;//
        private string m_CurrentOperatorID;//
        private DateTime m_CreateDate;//
        private int m_Creator;//
        private string m_RequestStatus;//
        private string m_RequestName;//
        private int m_IsCancel;//
        private string m_AllParticipator;//
        /// <summary>
        ///
        /// </summary>
        public int RequestID
        {
            get { return m_RequestID; }
            set { m_RequestID = value; }
        }
        /// <summary>
        ///
        /// </summary>
        public int WorkflowID
        {
            get { return m_WorkflowID; }
            set { m_WorkflowID = value; }
        }
        /// <summary>
        ///
        /// </summary>
        public int LastNodeID
        {
            get { return m_LastNodeID; }
            set { m_LastNodeID = value; }
        }
        /// <summary>
        ///
        /// </summary>
        public string LastRuleID
        {
            get { return m_LastRuleID; }
            set { m_LastRuleID = value; }
        }
        /// <summary>
        ///
        /// </summary>
        public string LastRuleType
        {
            get { return m_LastRuleType; }
            set { m_LastRuleType = value; }
        }
        /// <summary>
        ///
        /// </summary>
        public int LastNodeType
        {
            get { return m_LastNodeType; }
            set { m_LastNodeType = value; }
        }
        /// <summary>
        ///
        /// </summary>
        public int LastDeptLevel
        {
            get { return m_LastDeptLevel; }
            set { m_LastDeptLevel = value; }
        }
        /// <summary>
        ///
        /// </summary>
        public string LastOperatorID
        {
            get { return m_LastOperatorID; }
            set { m_LastOperatorID = value; }
        }
        /// <summary>
        ///
        /// </summary>
        public int CurrentNodeID
        {
            get { return m_CurrentNodeID; }
            set { m_CurrentNodeID = value; }
        }
        /// <summary>
        ///
        /// </summary>
        public string CurrentRuleID
        {
            get { return m_CurrentRuleID; }
            set { m_CurrentRuleID = value; }
        }
        /// <summary>
        ///
        /// </summary>
        public string CurrentRuleType
        {
            get { return m_CurrentRuleType; }
            set { m_CurrentRuleType = value; }
        }
        /// <summary>
        ///
        /// </summary>
        public int CurrentNodeType
        {
            get { return m_CurrentNodeType; }
            set { m_CurrentNodeType = value; }
        }
        /// <summary>
        ///
        /// </summary>
        public int CurrentDeptLevel
        {
            get { return m_CurrentDeptLevel; }
            set { m_CurrentDeptLevel = value; }
        }
        /// <summary>
        ///
        /// </summary>
        public string CurrentOperatorID
        {
            get { return m_CurrentOperatorID; }
            set { m_CurrentOperatorID = value; }
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
        public int Creator
        {
            get { return m_Creator; }
            set { m_Creator = value; }
        }
        /// <summary>
        ///
        /// </summary>
        public string RequestStatus
        {
            get { return m_RequestStatus; }
            set { m_RequestStatus = value; }
        }
        /// <summary>
        ///
        /// </summary>
        public string RequestName
        {
            get { return m_RequestName; }
            set { m_RequestName = value; }
        }
        /// <summary>
        ///
        /// </summary>
        public int IsCancel
        {
            get { return m_IsCancel; }
            set { m_IsCancel = value; }
        }
        /// <summary>
        ///
        /// </summary>
        public string AllParticipator
        {
            get { return m_AllParticipator; }
            set { m_AllParticipator = value; }
        }
    }
}