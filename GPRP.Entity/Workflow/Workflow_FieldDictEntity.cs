using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace GPRP.Entity
{
    /// <summary>
    /// 实体
    /// </summary>
    public class Workflow_FieldDictEntity
    {
        private int m_FieldID;//编号
        private string m_FieldName;//字段名称
        private string m_FieldDesc;//字段描述
        private int m_DataTypeID;//数据类型
        private string m_FieldDBType;//数据库表示方法
        private int m_HTMLTypeID;//显示方式
        private int m_FieldTypeID;//1:主字段;2:明细字段
        private string m_ValidateType;//数据验证方式
        private int m_TextLength;//单行文本框-文本长度
        private string m_Dateformat;//单行文本框-日期格式
        private int m_TextHeight;//多行文本框-高度
        private string m_IsHTML;//多行文本框-Html编辑字段
        private int m_BrowseType;//浏览按钮-类型
        private string m_IsDynamic;//是否来自表
        private int m_DataSetID;//数据源
        private string m_ValueColumn;//value列名
        private string m_TextColumn;//Text列名
        private string m_DefaultValue;//
        private string m_SqlDbType;//
        private int m_SqlDbLength;//
        private DataTable m_dtSelectList;
        /// <summary>
        ///编号
        /// </summary>
        public int FieldID
        {
            get { return m_FieldID; }
            set { m_FieldID = value; }
        }
        /// <summary>
        ///字段名称
        /// </summary>
        public string FieldName
        {
            get { return m_FieldName; }
            set { m_FieldName = value; }
        }
        /// <summary>
        ///字段描述
        /// </summary>
        public string FieldDesc
        {
            get { return m_FieldDesc; }
            set { m_FieldDesc = value; }
        }
        /// <summary>
        ///数据类型
        /// </summary>
        public int DataTypeID
        {
            get { return m_DataTypeID; }
            set { m_DataTypeID = value; }
        }
        /// <summary>
        ///数据库表示方法
        /// </summary>
        public string FieldDBType
        {
            get { return m_FieldDBType; }
            set { m_FieldDBType = value; }
        }
        /// <summary>
        ///显示方式
        /// </summary>
        public int HTMLTypeID
        {
            get { return m_HTMLTypeID; }
            set { m_HTMLTypeID = value; }
        }
        /// <summary>
        ///1:主字段;2:明细字段
        /// </summary>
        public int FieldTypeID
        {
            get { return m_FieldTypeID; }
            set { m_FieldTypeID = value; }
        }
        /// <summary>
        ///数据验证方式
        /// </summary>
        public string ValidateType
        {
            get { return m_ValidateType; }
            set { m_ValidateType = value; }
        }
        /// <summary>
        ///单行文本框-文本长度
        /// </summary>
        public int TextLength
        {
            get { return m_TextLength; }
            set { m_TextLength = value; }
        }
        /// <summary>
        ///单行文本框-日期格式
        /// </summary>
        public string Dateformat
        {
            get { return m_Dateformat; }
            set { m_Dateformat = value; }
        }
        /// <summary>
        ///多行文本框-高度
        /// </summary>
        public int TextHeight
        {
            get { return m_TextHeight; }
            set { m_TextHeight = value; }
        }
        /// <summary>
        ///多行文本框-Html编辑字段
        /// </summary>
        public string IsHTML
        {
            get { return m_IsHTML; }
            set { m_IsHTML = value; }
        }
        /// <summary>
        ///浏览按钮-类型
        /// </summary>
        public int BrowseType
        {
            get { return m_BrowseType; }
            set { m_BrowseType = value; }
        }
        /// <summary>
        ///是否来自表
        /// </summary>
        public string IsDynamic
        {
            get { return m_IsDynamic; }
            set { m_IsDynamic = value; }
        }
        /// <summary>
        ///数据源
        /// </summary>
        public int DataSetID
        {
            get { return m_DataSetID; }
            set { m_DataSetID = value; }
        }
        /// <summary>
        ///value列名
        /// </summary>
        public string ValueColumn
        {
            get { return m_ValueColumn; }
            set { m_ValueColumn = value; }
        }
        /// <summary>
        ///Text列名
        /// </summary>
        public string TextColumn
        {
            get { return m_TextColumn; }
            set { m_TextColumn = value; }
        }
        /// <summary>
        ///
        /// </summary>
        public string DefaultValue
        {
            get { return m_DefaultValue; }
            set { m_DefaultValue = value; }
        }
        /// <summary>
        ///
        /// </summary>
        public string SqlDbType
        {
            get { return m_SqlDbType; }
            set { m_SqlDbType = value; }
        }
        /// <summary>
        ///
        /// </summary>
        public int SqlDbLength
        {
            get { return m_SqlDbLength; }
            set { m_SqlDbLength = value; }
        }
        /// <summary>
        ///dtSelectList
        /// </summary>
        public DataTable dtSelectList
        {
            get { return m_dtSelectList; }
            set { m_dtSelectList = value; }
        }
    }
}

        