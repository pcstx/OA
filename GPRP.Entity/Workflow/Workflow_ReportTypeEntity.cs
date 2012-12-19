using System;
using System.Collections.Generic;
using System.Text;
namespace GPRP.Entity
{
    /// <summary>
    /// 实体
    /// </summary>
    public class Workflow_ReportTypeEntity
    {

        private int m_ReportTypeID;//报表种类ID
        private string m_ReportTypeName;//报表种类名称
        private string m_ReportTypeDesc;//报表种类描述
        private int m_DisplayOrder;//显示顺序
        /// <summary>
        ///报表种类ID
        /// </summary>
        public int ReportTypeID
        {
            get { return m_ReportTypeID; }
            set { m_ReportTypeID = value; }
        }
        /// <summary>
        ///报表种类名称
        /// </summary>
        public string ReportTypeName
        {
            get { return m_ReportTypeName; }
            set { m_ReportTypeName = value; }
        }
        /// <summary>
        ///报表种类描述
        /// </summary>
        public string ReportTypeDesc
        {
            get { return m_ReportTypeDesc; }
            set { m_ReportTypeDesc = value; }
        }
        /// <summary>
        ///显示顺序
        /// </summary>
        public int DisplayOrder
        {
            get { return m_DisplayOrder; }
            set { m_DisplayOrder = value; }
        }
    }
}