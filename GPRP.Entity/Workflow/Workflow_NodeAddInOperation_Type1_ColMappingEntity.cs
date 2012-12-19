﻿using System;
using System.Collections.Generic;
using System.Text;
namespace GPRP.Entity
{
    /// <summary>
    /// 实体
    /// </summary>
    public class Workflow_NodeAddInOperation_Type1_ColMappingEntity
    {

        private int m_AddInOPID;//动作编号
        private string m_TargetFieldName;//目标字段
        private string m_TartgetValue;//目标表达式
        private int m_FieldTypeID;//1:主字段 2：明细字段
        /// <summary>
        ///动作编号
        /// </summary>
        public int AddInOPID
        {
            get { return m_AddInOPID; }
            set { m_AddInOPID = value; }
        }
        /// <summary>
        ///目标字段
        /// </summary>
        public string TargetFieldName
        {
            get { return m_TargetFieldName; }
            set { m_TargetFieldName = value; }
        }
        /// <summary>
        ///目标表达式        /// </summary>
        public string TartgetValue
        {
            get { return m_TartgetValue; }
            set { m_TartgetValue = value; }
        }
        /// <summary>
        ///1:主字段 2：明细字段        /// </summary>
        public int FieldTypeID
        {
            get { return m_FieldTypeID; }
            set { m_FieldTypeID = value; }
        }
    }
}