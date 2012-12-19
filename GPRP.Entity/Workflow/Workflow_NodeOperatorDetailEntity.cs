using System;
using System.Collections.Generic;
using System.Text;
namespace GPRP.Entity
{
    /// <summary>
    /// 实体
    /// </summary>
    public class Workflow_NodeOperatorDetailEntity
    {

        private int m_RuleID;//规则编号
        private int m_NodeID;//节点编号
        private int m_RuleType;//抓取方式
        private string m_RuleCondition;//批次条件
        private string m_ObjectValue;//对象值
        private string m_RuleName;//名称
        private int m_RuleSeq;//批次
        private int m_SecurityStart;//领导级别(S)
        private int m_SecurityEnd;//安全级别(E)
        private int m_LevelStart;//起始级别
        private int m_LevelEnd;//结束级别
        private int m_SignType;//是否会签
        /// <summary>
        ///规则编号
        /// </summary>
        public int RuleID
        {
            get { return m_RuleID; }
            set { m_RuleID = value; }
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
        ///抓取方式
        /// </summary>
        public int RuleType
        {
            get { return m_RuleType; }
            set { m_RuleType = value; }
        }
        /// <summary>
        ///批次条件
        /// </summary>
        public string RuleCondition
        {
            get { return m_RuleCondition; }
            set { m_RuleCondition = value; }
        }
        /// <summary>
        ///对象值        /// </summary>
        public string ObjectValue
        {
            get { return m_ObjectValue; }
            set { m_ObjectValue = value; }
        }
        /// <summary>
        ///名称
        /// </summary>
        public string RuleName
        {
            get { return m_RuleName; }
            set { m_RuleName = value; }
        }
        /// <summary>
        ///批次
        /// </summary>
        public int RuleSeq
        {
            get { return m_RuleSeq; }
            set { m_RuleSeq = value; }
        }
        /// <summary>
        ///领导级别(S)
        /// </summary>
        public int SecurityStart
        {
            get { return m_SecurityStart; }
            set { m_SecurityStart = value; }
        }
        /// <summary>
        ///安全级别(E)
        /// </summary>
        public int SecurityEnd
        {
            get { return m_SecurityEnd; }
            set { m_SecurityEnd = value; }
        }
        /// <summary>
        ///起始级别
        /// </summary>
        public int LevelStart
        {
            get { return m_LevelStart; }
            set { m_LevelStart = value; }
        }
        /// <summary>
        ///结束级别
        /// </summary>
        public int LevelEnd
        {
            get { return m_LevelEnd; }
            set { m_LevelEnd = value; }
        }
        /// <summary>
        ///是否会签
        /// </summary>
        public int SignType
        {
            get { return m_SignType; }
            set { m_SignType = value; }
        }
    }
}