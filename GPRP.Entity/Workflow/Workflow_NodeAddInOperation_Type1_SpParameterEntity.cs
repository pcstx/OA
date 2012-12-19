using System;
using System.Collections.Generic;
using System.Text;

namespace GPRP.Entity
{
    /// <summary>
    /// 实体
    /// </summary>
    public class Workflow_NodeAddInOperation_Type1_SpParameterEntity
    {
        private int m_AddInOPID;//动作编号
        private string m_SpParameter;//参数
        private string m_ParameterType;//参数类型
        private string m_ParameterDirection;//传值方式
        private int m_ParameterSize;//参数Size
        private string m_TartgetValue;//参数值
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
        ///参数
        /// </summary>
        public string SpParameter
        {
            get { return m_SpParameter; }
            set { m_SpParameter = value; }
        }
        /// <summary>
        ///参数类型
        /// </summary>
        public string ParameterType
        {
            get { return m_ParameterType; }
            set { m_ParameterType = value; }
        }
        /// <summary>
        ///传值方式
        /// </summary>
        public string ParameterDirection
        {
            get { return m_ParameterDirection; }
            set { m_ParameterDirection = value; }
        }
        /// <summary>
        ///参数Size
        /// </summary>
        public int ParameterSize
        {
            get { return m_ParameterSize; }
            set { m_ParameterSize = value; }
        }
        /// <summary>
        ///参数值
        /// </summary>
        public string TartgetValue
        {
            get { return m_TartgetValue; }
            set { m_TartgetValue = value; }
        }
        /// <summary>
        ///1:主字段 2：明细字段
        /// </summary>
        public int FieldTypeID
        {
            get { return m_FieldTypeID; }
            set { m_FieldTypeID = value; }
        }
    }
}