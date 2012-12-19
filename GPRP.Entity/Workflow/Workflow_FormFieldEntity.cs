using System;
using System.Collections.Generic;
using System.Text;
namespace GPRP.Entity
{
    /// <summary>
    /// 实体
    /// </summary>
    public class Workflow_FormFieldEntity
    {

        private int m_FormID;//表单编号
        private int m_FieldID;//字段编号
        private int m_GroupID;//明细组ID
        private string m_FieldLabel;//字段显示名称
        private int m_CSSStyle;//显示样式
        private int m_DisplayOrder;//显示顺序
        private int m_IsDetail;//是否明细
        private DateTime m_CreateDate;//创建日期
        private int m_GroupLineDataSetID;//对应数据集
        private int m_TargetGroupID;//对应明细组
        /// <summary>
        ///表单编号
        /// </summary>
        public int FormID
        {
            get { return m_FormID; }
            set { m_FormID = value; }
        }
        /// <summary>
        ///字段编号
        /// </summary>
        public int FieldID
        {
            get { return m_FieldID; }
            set { m_FieldID = value; }
        }
        /// <summary>
        ///明细组ID
        /// </summary>
        public int GroupID
        {
            get { return m_GroupID; }
            set { m_GroupID = value; }
        }
        /// <summary>
        ///字段显示名称
        /// </summary>
        public string FieldLabel
        {
            get { return m_FieldLabel; }
            set { m_FieldLabel = value; }
        }
        /// <summary>
        ///显示样式
        /// </summary>
        public int CSSStyle
        {
            get { return m_CSSStyle; }
            set { m_CSSStyle = value; }
        }
        /// <summary>
        ///显示顺序
        /// </summary>
        public int DisplayOrder
        {
            get { return m_DisplayOrder; }
            set { m_DisplayOrder = value; }
        }
        /// <summary>
        ///是否明细
        /// </summary>
        public int IsDetail
        {
            get { return m_IsDetail; }
            set { m_IsDetail = value; }
        }
        /// <summary>
        ///创建日期
        /// </summary>
        public DateTime CreateDate
        {
            get { return m_CreateDate; }
            set { m_CreateDate = value; }
        }
        /// <summary>
        ///对应数据集
        /// </summary>
        public int GroupLineDataSetID
        {
            get { return m_GroupLineDataSetID; }
            set { m_GroupLineDataSetID = value; }
        }
        /// <summary>
        ///对应明细组
        /// </summary>
        public int TargetGroupID
        {
            get { return m_TargetGroupID; }
            set { m_TargetGroupID = value; }
        }
    }
}