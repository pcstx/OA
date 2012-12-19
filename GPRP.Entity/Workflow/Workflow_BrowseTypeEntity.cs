using System;
using System.Collections.Generic;
using System.Text;
namespace GPRP.Entity
{
    /// <summary>
    /// 实体
    /// </summary>
    public class Workflow_BrowseTypeEntity
    {

        private int m_BrowseTypeID;//编号
        private string m_BrowseTypeName;//浏览按钮名称
        private string m_BrowseTypeDesc;//浏览按钮描述
        private string m_BrowsePage;//浏览页面
        private string m_Useflag;//是否使用
        private string m_BrowseValueSql;//查询语句
        private string m_BrowseSqlParam;//参数
        /// <summary>
        ///编号
        /// </summary>
        public int BrowseTypeID
        {
            get { return m_BrowseTypeID; }
            set { m_BrowseTypeID = value; }
        }
        /// <summary>
        ///浏览按钮名称
        /// </summary>
        public string BrowseTypeName
        {
            get { return m_BrowseTypeName; }
            set { m_BrowseTypeName = value; }
        }
        /// <summary>
        ///浏览按钮描述
        /// </summary>
        public string BrowseTypeDesc
        {
            get { return m_BrowseTypeDesc; }
            set { m_BrowseTypeDesc = value; }
        }
        /// <summary>
        ///浏览页面
        /// </summary>
        public string BrowsePage
        {
            get { return m_BrowsePage; }
            set { m_BrowsePage = value; }
        }
        /// <summary>
        ///是否使用
        /// </summary>
        public string Useflag
        {
            get { return m_Useflag; }
            set { m_Useflag = value; }
        }
        /// <summary>
        ///查询语句
        /// </summary>
        public string BrowseValueSql
        {
            get { return m_BrowseValueSql; }
            set { m_BrowseValueSql = value; }
        }
        /// <summary>
        ///参数
        /// </summary>
        public string BrowseSqlParam
        {
            get { return m_BrowseSqlParam; }
            set { m_BrowseSqlParam = value; }
        }
    }
}