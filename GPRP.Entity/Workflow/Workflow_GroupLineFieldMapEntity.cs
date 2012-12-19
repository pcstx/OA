using System;
using System.Collections.Generic;
using System.Text;
namespace GPRP.Entity
{
    /// <summary>
    /// 实体
    /// </summary>
    public class Workflow_GroupLineFieldMapEntity
    {

        private int m_FormID;//
        private int m_FieldID;//
        private int m_MappingID;//
        private string m_DataSetColumn;//
        private string m_TargetGroupField;//

        /// <summary>
        ///
        /// </summary>
        public int FormID
        {
            get { return m_FormID; }
            set { m_FormID = value; }
        }
        /// <summary>
        ///
        /// </summary>
        public int FieldID
        {
            get { return m_FieldID; }
            set { m_FieldID = value; }
        }
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
        public string DataSetColumn
        {
            get { return m_DataSetColumn; }
            set { m_DataSetColumn = value; }
        }
        /// <summary>
        ///
        /// </summary>
        public string TargetGroupField
        {
            get { return m_TargetGroupField; }
            set { m_TargetGroupField = value; }
        }
    }
}