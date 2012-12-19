using System;
using System.Collections.Generic;
using System.Text;
namespace GPRP.Entity
{
    /// <summary>
    /// 实体
    /// </summary>
    public class Workflow_DataSetEntity
    {

        private int m_DataSetID;//数据集编号
        private int m_DataSetType;//数据集类型
        private string m_DataSetName;//数据集名称
        private int m_DataSourceID;//数据源ID(Workflow_DataSource 中的DataSourceID)
        private string m_TableList;//表列表 /调用的存储过程名称
        private string m_FieldList;//字段列表
        private string m_QueryCondition;//查询条件 / 存储过程参数
        private string m_OrderBy;//排序
        private string m_QuerySql;//完整的查询语句
        private string m_ReturnColumns;//最终返回字段名
        private string m_ReturnColumnsName;//返回字段的描述
        private string m_DataSetPKColumns;//数据集的主键

        /// <summary>
        ///数据集编号
        /// </summary>
        public int DataSetID
        {
            get { return m_DataSetID; }
            set { m_DataSetID = value; }
        }
        /// <summary>
        ///数据集类型  1 表示通过QuerySql获得，２表示通过StoreProcedure
        /// </summary>
        public int DataSetType
        {
            get { return m_DataSetType; }
            set { m_DataSetType = value; }
        }
        /// <summary>
        ///数据集名称
        /// </summary>
        public string DataSetName
        {
            get { return m_DataSetName; }
            set { m_DataSetName = value; }
        }
        /// <summary>
        ///数据源ID(Workflow_DataSource 中的DataSourceID)
        /// </summary>
        public int DataSourceID
        {
            get { return m_DataSourceID; }
            set { m_DataSourceID = value; }
        }
        /// <summary>
        ///表列表 /调用的存储过程名称
        /// </summary>
        public string TableList
        {
            get { return m_TableList; }
            set { m_TableList = value; }
        }
        /// <summary>
        ///字段列表
        /// </summary>
        public string FieldList
        {
            get { return m_FieldList; }
            set { m_FieldList = value; }
        }
        /// <summary>
        ///查询条件 / 存储过程参数值
        /// </summary>
        public string QueryCondition
        {
            get { return m_QueryCondition; }
            set { m_QueryCondition = value; }
        }
        /// <summary>
        ///排序
        /// </summary>
        public string OrderBy
        {
            get { return m_OrderBy; }
            set { m_OrderBy = value; }
        }
        /// <summary>
        ///完整的查询语句
        /// </summary>
        public string QuerySql
        {
            get { return m_QuerySql; }
            set { m_QuerySql = value; }
        }
        /// <summary>
        ///表/存储过程 最终返回字段名
        /// </summary>
        public string ReturnColumns
        {
            get { return m_ReturnColumns; }
            set { m_ReturnColumns = value; }
        }

        /// <summary>
        /// 返回的字段描述

        /// </summary>
        public string ReturnColumnsName
        {
            get { return m_ReturnColumnsName; }
            set { m_ReturnColumnsName = value; }
        }


        /// <summary>
        /// 数据集的主键
        /// </summary>
        public string DataSetPKColumns
        {
            get { return m_DataSetPKColumns; }
            set { m_DataSetPKColumns = value; }
        }




    }





}