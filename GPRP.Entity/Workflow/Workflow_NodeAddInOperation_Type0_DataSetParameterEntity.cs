using System;
using System.Collections.Generic;
using System.Text;
namespace GPRP.Entity
{
    /// <summary>
    /// 实体
    /// </summary>
    public class Workflow_NodeAddInOperation_Type0_DataSetParameterEntity
    {

        private int m_AddInOPID;//动作编号
        private string m_DSParameter;//参数
        private string m_TartgetValue;//参数值
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
        public string DSParameter
        {
            get { return m_DSParameter; }
            set { m_DSParameter = value; }
        }
        /// <summary>
        ///参数值        /// </summary>
        public string TartgetValue
        {
            get { return m_TartgetValue; }
            set { m_TartgetValue = value; }
        }
    }
}