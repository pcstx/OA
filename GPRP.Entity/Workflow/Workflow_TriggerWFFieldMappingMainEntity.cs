using System;
using System.Collections.Generic;
using System.Text;
namespace GPRP.Entity
{
    /// <summary>
    /// 实体
    /// </summary>
    public class Workflow_TriggerWFFieldMappingMainEntity
    {
        private int m_MappingID;
        private int m_TriggerID;//
        private int m_TargetGroupID;//目标字段GroupID
        private byte m_OPCycleType;//针对明细字段的行数的给子流程赋值的执行次数0.一次 1.按明细行循环执行

        /// <summary>
        ///
        /// </summary>
        public int MappingID
        {
            get { return m_MappingID; }
            set { m_MappingID = value; }
        }
        /// <summary>
        ///
        /// </summary>
        public int TriggerID
        {
            get { return m_TriggerID; }
            set { m_TriggerID = value; }
        }

        /// <summary>
        ///目标字段GroupID
        /// </summary>
        public int TargetGroupID
        {
            get { return m_TargetGroupID; }
            set { m_TargetGroupID = value; }
        }



        /// <summary>
        ///针对明细字段的行数的给子流程赋值的执行次数0.一次 1.按明细行循环执行
        /// </summary>
        public byte OPCycleType
        {
            get { return m_OPCycleType; }
            set { m_OPCycleType = value; }
        }

    }

}