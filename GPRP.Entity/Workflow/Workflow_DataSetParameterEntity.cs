using System;
using System.Collections.Generic;
using System.Text;
namespace GPRP.Entity
{
    /// <summary>
    /// 实体
    /// </summary>
    public class Workflow_DataSetParameterEntity
    {

        private int m_DataSetID;//数据集ID
        private string m_ParameterName;//SP参数名称
        private string m_ParameterType;//StoredProcedure数据类型
        private string m_ParameterDirection;//输入/输出
        private int m_ParameterSize;//
        private string m_ParameterValue;//参数值
        private string m_ParameterDesc;//
        /// <summary>
        ///数据集ID
        /// </summary>
        public int DataSetID
        {
            get { return m_DataSetID; }
            set { m_DataSetID = value; }
        }
        /// <summary>
        ///SP参数名称
        /// </summary>
        public string ParameterName
        {
            get { return m_ParameterName; }
            set { m_ParameterName = value; }
        }
        /// <summary>
        ///StoredProcedure数据类型
        /// </summary>
        public string ParameterType
        {
            get { return m_ParameterType; }
            set { m_ParameterType = value; }
        }
        /// <summary>
        ///输入/输出
        /// </summary>
        public string ParameterDirection
        {
            get { return m_ParameterDirection; }
            set { m_ParameterDirection = value; }
        }
        /// <summary>
        ///
        /// </summary>
        public int ParameterSize
        {
            get { return m_ParameterSize; }
            set { m_ParameterSize = value; }
        }
        /// <summary>
        ///参数值
        /// </summary>
        public string ParameterValue
        {
            get { return m_ParameterValue; }
            set { m_ParameterValue = value; }
        }
        /// <summary>
        ///
        /// </summary>
        public string ParameterDesc
        {
            get { return m_ParameterDesc; }
            set { m_ParameterDesc = value; }
        }
    }
}