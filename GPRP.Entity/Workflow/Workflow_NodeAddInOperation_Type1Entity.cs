using System;
using System.Collections.Generic;
using System.Text;
namespace GPRP.Entity
{
    /// <summary>
    /// 实体
    /// </summary>
    public class Workflow_NodeAddInOperation_Type1Entity
    {

        private int m_AddInOPID;//动作编号
        private int m_NodeID;//节点ID
        private int m_CaculateType;//处理方式(0.Insert 1.Upadate 2.Delete3.调用存储过程)
        private int m_DataSourceID;//数据源编号
        private string m_DataSourceTable;//数据源目标表
        private int m_GroupID;//明细组
        private string m_SelectRange;//取值范围
        private int m_OPCycleType;//执行方式(0:一次 1:循环)
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
        ///处理方式(0.Insert 1.Upadate 2.Delete3.调用存储过程)
        /// </summary>
        public int CaculateType
        {
            get { return m_CaculateType; }
            set { m_CaculateType = value; }
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
        ///数据源目标表
        /// </summary>
        public string DataSourceTable
        {
            get { return m_DataSourceTable; }
            set { m_DataSourceTable = value; }
        }
        /// <summary>
        ///明细组
        /// </summary>
        public int GroupID
        {
            get { return m_GroupID; }
            set { m_GroupID = value; }
        }
        /// <summary>
        ///取值范围
        /// </summary>
        public string SelectRange
        {
            get { return m_SelectRange; }
            set { m_SelectRange = value; }
        }
        /// <summary>
        ///执行方式(0:一次 1:循环)
        /// </summary>
        public int OPCycleType
        {
            get { return m_OPCycleType; }
            set { m_OPCycleType = value; }
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