using System;
using System.Collections.Generic;
using System.Text;

namespace GPRP.Entity
{
   /// <summary>
   /// 实体
   /// </summary>
   public class Workflow_DataSourceEntity
  {

        private int m_DataSourceID;//数据源编号
        private string m_DataSourceName;//数据源名称
        private string m_DataSourceDBType;//数据源数据库类型(Sql Server , Oracle)
        private string m_ConnectString;//连接字符串
        /// <summary>
        ///数据源编号        /// </summary>
        public int DataSourceID
        {
             get { return m_DataSourceID; }
             set { m_DataSourceID = value;}
        }
        /// <summary>
        ///数据源名称        /// </summary>
        public string DataSourceName
        {
             get { return m_DataSourceName; }
             set { m_DataSourceName = value;}
        }
        /// <summary>
        ///数据源数据库类型(Sql Server , Oracle)
        /// </summary>
        public string DataSourceDBType
        {
             get { return m_DataSourceDBType; }
             set { m_DataSourceDBType = value;}
        }
        /// <summary>
        ///连接字符串        /// </summary>
        public string ConnectString
        {
             get { return m_ConnectString; }
             set { m_ConnectString = value;}
        }
  }
}