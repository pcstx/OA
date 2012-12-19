using System;
using System.Collections.Generic;
using System.Text;
namespace GPRP.Entity
{
    /// <summary>
    /// 实体
    /// </summary>
    public class Workflow_NodeTriggerConditionEntity
    {

        private int m_ConditionID;//条件ID
        private byte m_BatchSeq;//条件批次
        private byte m_BranchBatchSeq;//批次分支
        private int m_TriggerID;//触发ID
        private int m_FieldID;//触发条件字段ID
        private string m_SymbolCode;//比较符号
        //  private int m_CompareFieldID;//比较字段ID
        private string m_CompareToValue;//用于比较的值

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
        public byte BatchSeq
        {
            get { return m_BatchSeq; }
            set { m_BatchSeq = value; }
        }
        /// <summary>
        ///批次分支
        /// </summary>
        public byte BranchBatchSeq
        {
            get { return m_BranchBatchSeq; }
            set { m_BranchBatchSeq = value; }
        }
        /// <summary>
        ///触发ID
        /// </summary>
        public int TriggerID
        {
            get { return m_TriggerID; }
            set { m_TriggerID = value; }
        }
        /// <summary>
        ///触发条件字段ID
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
        /*    /// <summary>
            ///比较字段ID
            /// </summary>
            public int CompareFieldID
                {
                get { return m_CompareFieldID; }
                set { m_CompareFieldID = value; }
                }*/
        /// <summary>
        ///用于比较的值
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