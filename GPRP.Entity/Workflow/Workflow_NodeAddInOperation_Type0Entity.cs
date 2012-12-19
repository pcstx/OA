using System;
using System.Collections.Generic;
using System.Text;
namespace GPRP.Entity
{
    /// <summary>
    /// 实体
    /// </summary>
    public class Workflow_NodeAddInOperation_Type0Entity
    {

        private int m_AddInOPID;//动作编号
        private int m_NodeID;//节点ID
        private int m_TargetFieldID;//目标字段
        private int m_CaculateType;//1:计算公式 2:浏览画面 3:数据集和 4:存储过程
        private string m_CaculateValue;//计算值
        private int m_DataSetID;//数据集编号
        private string m_ValueField;//值字段
        private int m_DataSourceID;//数据源编号
        private string m_StorageProcedure;//存储过程名
        private string m_OutputParameter;//显示字段
        private int m_OPTime;//动作时机
        private string m_OPCondition;//动作条件
        private DateTime m_CreateDate;//创建时间
        private int m_CreateSID;//创建人
        private int m_IsCancel;//是否删除
        private DateTime m_CancelDate;//删除日期
        private int m_CancelSID;//删除人
        /// <summary>
        ///动作编号
        /// </summary>
        public int AddInOPID
        {
            get { return m_AddInOPID; }
            set { m_AddInOPID = value; }
        }
        /// <summary>
        ///节点ID
        /// </summary>
        public int NodeID
        {
            get { return m_NodeID; }
            set { m_NodeID = value; }
        }
        /// <summary>
        ///目标字段
        /// </summary>
        public int TargetFieldID
        {
            get { return m_TargetFieldID; }
            set { m_TargetFieldID = value; }
        }
        /// <summary>
        ///1:计算公式 2:浏览画面 3:数据集和 4:存储过程
        /// </summary>
        public int CaculateType
        {
            get { return m_CaculateType; }
            set { m_CaculateType = value; }
        }
        /// <summary>
        ///计算值
        /// </summary>
        public string CaculateValue
        {
            get { return m_CaculateValue; }
            set { m_CaculateValue = value; }
        }
        /// <summary>
        ///数据集编号
        /// </summary>
        public int DataSetID
        {
            get { return m_DataSetID; }
            set { m_DataSetID = value; }
        }
        /// <summary>
        ///值字段
        /// </summary>
        public string ValueField
        {
            get { return m_ValueField; }
            set { m_ValueField = value; }
        }
        /// <summary>
        ///数据源编号
        /// </summary>
        public int DataSourceID
        {
            get { return m_DataSourceID; }
            set { m_DataSourceID = value; }
        }
        /// <summary>
        ///存储过程名
        /// </summary>
        public string StorageProcedure
        {
            get { return m_StorageProcedure; }
            set { m_StorageProcedure = value; }
        }
        /// <summary>
        ///显示字段
        /// </summary>
        public string OutputParameter
        {
            get { return m_OutputParameter; }
            set { m_OutputParameter = value; }
        }
        /// <summary>
        ///动作时机
        /// </summary>
        public int OPTime
        {
            get { return m_OPTime; }
            set { m_OPTime = value; }
        }
        /// <summary>
        ///动作条件
        /// </summary>
        public string OPCondition
        {
            get { return m_OPCondition; }
            set { m_OPCondition = value; }
        }
        /// <summary>
        ///创建时间
        /// </summary>
        public DateTime CreateDate
        {
            get { return m_CreateDate; }
            set { m_CreateDate = value; }
        }
        /// <summary>
        ///创建人
        /// </summary>
        public int CreateSID
        {
            get { return m_CreateSID; }
            set { m_CreateSID = value; }
        }
        /// <summary>
        ///是否删除
        /// </summary>
        public int IsCancel
        {
            get { return m_IsCancel; }
            set { m_IsCancel = value; }
        }
        /// <summary>
        ///删除日期
        /// </summary>
        public DateTime CancelDate
        {
            get { return m_CancelDate; }
            set { m_CancelDate = value; }
        }
        /// <summary>
        ///删除人
        /// </summary>
        public int CancelSID
        {
            get { return m_CancelSID; }
            set { m_CancelSID = value; }
        }
    }
}