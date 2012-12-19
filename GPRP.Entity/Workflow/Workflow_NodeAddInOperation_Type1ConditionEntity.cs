using System;
using System.Collections.Generic;
using System.Text;
namespace GPRP.Entity
{
    /// <summary>
    /// 实体
    /// </summary>
    public class Workflow_NodeAddInOperation_Type1ConditionEntity
    {

        private int m_ConditionID;//条件ID
        private int m_BatchSeq;//条件批次
        private int m_BranchBatchSeq;//批次分支
        private int m_AddInOPID;//节点ID
        private int m_FieldID;//字段ID
        private string m_SymbolCode;//比较符号
        private string m_CompareToValue;//比较对象
        private string m_AndOr;//分支关系
        /// <summary>
        ///条件ID
        /// </summary>
        public int ConditionID
        {
            get { return m_ConditionID; }
            set { m_ConditionID = value; }
        }
        /// <summary>
        ///条件批次
        /// </summary>
        public int BatchSeq
        {
            get { return m_BatchSeq; }
            set { m_BatchSeq = value; }
        }
        /// <summary>
        ///批次分支
        /// </summary>
        public int BranchBatchSeq
        {
            get { return m_BranchBatchSeq; }
            set { m_BranchBatchSeq = value; }
        }
        /// <summary>
        ///节点ID
        /// </summary>
        public int AddInOPID
        {
            get { return m_AddInOPID; }
            set { m_AddInOPID = value; }
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
        ///比较符号
        /// </summary>
        public string SymbolCode
        {
            get { return m_SymbolCode; }
            set { m_SymbolCode = value; }
        }
        /// <summary>
        ///比较对象
        /// </summary>
        public string CompareToValue
        {
            get { return m_CompareToValue; }
            set { m_CompareToValue = value; }
        }
        /// <summary>
        ///分支关系
        /// </summary>
        public string AndOr
        {
            get { return m_AndOr; }
            set { m_AndOr = value; }
        }
    }
}