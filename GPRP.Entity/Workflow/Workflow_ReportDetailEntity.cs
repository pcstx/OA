using System;
using System.Collections.Generic;
using System.Text;
namespace GPRP.Entity
    {
    /// <summary>
    /// 实体
    /// </summary>
    public class Workflow_ReportDetailEntity
        {

        private int m_ReportID;//
        private int m_FieldID;//
        private byte   m_IsStatistics;//是否统计 0 否，1 是 
        private byte  m_IsOrder;//是否为排序字段 0否，1是
        private byte  m_OrderPattern;//排序方式：1 升/  2降
        private int m_OrderIndex;//排序顺序
        private int m_DisplayOrder;//字段显示顺序
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
        public int FieldID
            {
            get { return m_FieldID; }
            set { m_FieldID = value; }
            }
        /// <summary>
        ///是否统计 0 否，1 是 
        /// </summary>
        public byte  IsStatistics
            {
            get { return m_IsStatistics; }
            set { m_IsStatistics = value; }
            }
        /// <summary>
        ///是否为排序字段 0否，1是        /// </summary>
        public byte  IsOrder
            {
            get { return m_IsOrder; }
            set { m_IsOrder = value; }
            }
        /// <summary>
        ///排序方式：1 升/  2降        /// </summary>
        public byte  OrderPattern
            {
            get { return m_OrderPattern; }
            set { m_OrderPattern = value; }
            }
        /// <summary>
        ///排序顺序
        /// </summary>
        public int OrderIndex
            {
            get { return m_OrderIndex; }
            set { m_OrderIndex = value; }
            }
        /// <summary>
        ///字段显示顺序
        /// </summary>
        public int DisplayOrder
            {
            get { return m_DisplayOrder; }
            set { m_DisplayOrder = value; }
            }
        }
    }