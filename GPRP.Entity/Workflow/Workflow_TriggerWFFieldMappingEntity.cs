using System;
using System.Collections.Generic;
using System.Text;
namespace GPRP.Entity
{
    /// <summary>
    /// 实体
    /// </summary>
    public class Workflow_TriggerWFFieldMappingEntity
    {
        private int m_MappingID;
        private int m_TriggerID;//
        private int m_TargetGroupID;//目标字段GroupID
        private int m_SourceGroupID;//来源字段GroupID
        private int m_TargetFieldID;//目标字段名称
        private string m_SourceFieldName;//来源(可以是用户定义，也可以是流程中的字段的表达式)
        private Int16 m_SourceFieldTypeID;//1:主字段 2：明细字段

        private Int16 m_OPCycleType;//针对明细字段的行数的给子流程赋值的执行次数0.一次 1.按明细行循环执行

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
        ///目标字段名称
        /// </summary>
        public int TargetFieldID
        {
            get { return m_TargetFieldID; }
            set { m_TargetFieldID = value; }
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
        ///来源字段GroupID
        /// </summary>
        public int SourceGroupID
        {
            get { return m_SourceGroupID; }
            set { m_SourceGroupID = value; }
        }
        /// <summary>
        ///来源(可以是用户定义，也可以是流程中的字段的表达式)
        /// </summary>
        public string SourceFieldName
        {
            get { return m_SourceFieldName; }
            set { m_SourceFieldName = value; }
        }

        /// <summary>
        ///1:主字段 2：明细字段
        /// </summary>
        public Int16 SourceFieldTypeID
        {
            get { return m_SourceFieldTypeID; }
            set { m_SourceFieldTypeID = value; }
        }

        /// <summary>
        ///针对明细字段的行数的给子流程赋值的执行次数0.一次 1.按明细行循环执行
        /// </summary>
        public Int16 OPCycleType
        {
            get { return m_OPCycleType; }
            set { m_OPCycleType = value; }
        }

    }

}