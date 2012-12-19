using System;
using System.Collections.Generic;
using System.Text;
namespace GPRP.Entity
{
    /// <summary>
    /// 实体
    /// </summary>
    public class Workflow_FormDetailRuleEntity
    {

        private int m_RuleID;//规则ID
        private int m_FormID;//表单ID
        private int m_GroupID;//明细组ID
        private int m_FieldIDTo;//附值字段
        private string m_RuleDetail;//赋值公式
        private int m_RuleType;//1:行规则;2:行规则
        /// <summary>
        ///规则ID
        /// </summary>
        public int RuleID
        {
            get { return m_RuleID; }
            set { m_RuleID = value; }
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
        ///明细组ID
        /// </summary>
        public int GroupID
        {
            get { return m_GroupID; }
            set { m_GroupID = value; }
        }
        /// <summary>
        ///附值字段        /// </summary>
        public int FiledIDTo
        {
            get { return m_FieldIDTo; }
            set { m_FieldIDTo = value; }
        }
        /// <summary>
        ///赋值公式        /// </summary>
        public string RuleDetail
        {
            get { return m_RuleDetail; }
            set { m_RuleDetail = value; }
        }
        /// <summary>
        ///1:行规则;2:行规则        /// </summary>
        public int RuleType
        {
            get { return m_RuleType; }
            set { m_RuleType = value; }
        }
    }
}