using System;
using System.Collections.Generic;
using System.Text;
namespace GPRP.Entity
    {
    /// <summary>
    /// 实体
    /// </summary>
    public class Workflow_ReportMainEntity
        {

        private int m_ReportID;//
        private int m_ReportTypeID;//
        private string m_ReportName;//
        private int m_FormID;//
        private string m_WorkflowID;//
        /// <summary>
        ///
        /// </summary>
        public int ReportID
            {
            get { return m_ReportID; }
            set { m_ReportID = value; }
            }
        /// <summary>
        ///
        /// </summary>
        public int ReportTypeID
            {
            get { return m_ReportTypeID; }
            set { m_ReportTypeID = value; }
            }
        /// <summary>
        ///
        /// </summary>
        public string ReportName
            {
            get { return m_ReportName; }
            set { m_ReportName = value; }
            }
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
        public string WorkflowID
            {
            get { return m_WorkflowID; }
            set { m_WorkflowID = value; }
            }
        }
    }