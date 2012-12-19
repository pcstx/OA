using System;

namespace GPRP.GPRPComponents
{
	/// <summary>
	/// 基本设置描述类, 加[Serializable]标记为可序列化
	/// </summary>
	[Serializable]
	public class BaseConfigInfo : IConfigInfo
    {
        #region 私有字段
        private string m_forumpath = "/";			// 论坛在站点内的路径

        private string m_dbconnectstring = "Data Source=;User ID=dntuser;Password=;Initial Catalog=;Pooling=true";		// 数据库连接串-格式(中文为用户修改的内容)：Data Source=数据库服务器地址;User ID=您的数据库用户名;Password=您的数据库用户密码;Initial Catalog=数据库名称;Pooling=true
        private string m_dbtype = "";
        private string mUserID = "";
        private string mPassword = "";
        private string mDataServer = "";

        private string m_DbOracleconnectstring = "";
        private string m_UserIDOracle;
        private string m_PasswordOracle;
        private string m_DataServerOracle;
        private string m_DbtypeOracle;

        private string m_ErpDbConnectString;
        private string m_ErpUserID;
        private string m_ErpPassword;
        private string m_ErpServer;
        private string m_ErpDbType;

        private string m_QmsDbConnectString;
        private string m_QmsDbType;

        #endregion

        #region 属性
        /// <summary>
        /// 论坛在站点内的路径        /// </summary>
        public string Forumpath
        {
            get { return m_forumpath; }
            set { m_forumpath = value; }
        }
        /// <summary>
		/// 数据库连接串
		/// 格式(中文为用户修改的内容)：
		///    Data Source=数据库服务器地址;
		///    User ID=您的数据库用户名;
		///    Password=您的数据库用户密码;
		///    Initial Catalog=数据库名称;Pooling=true
		/// </summary>
		public string Dbconnectstring
		{
			get { return m_dbconnectstring;}
			set { m_dbconnectstring = value;}
		}

        public string UserID
        {
            get { return mUserID; }
            set { mUserID = value; }
        }
        public string Password
        {
            get { return mPassword; }
            set { mPassword = value; }
        }
        public string DataServer
        {
            get { return mDataServer; }
            set { mDataServer = value; }
        }
        /// <summary>
        /// 数据库类型        /// </summary>
        public string Dbtype
        {
            get { return m_dbtype; }
            set { m_dbtype = value; }
        }

        public string DbOracleconnectstring
        {
            get { return m_DbOracleconnectstring; }
            set { m_DbOracleconnectstring = value; }
        }
        public string UserIDOracle
        {
            get { return m_UserIDOracle; }
            set { m_UserIDOracle = value; }
        }
        public string PasswordOracle
        {
            get { return m_PasswordOracle; }
            set { m_PasswordOracle = value; }
        }
        public string DataServerOracle
        {
            get { return m_DataServerOracle; }
            set { m_DataServerOracle = value; }
        }
        public string DbtypeOracle
        {
            get { return m_DbtypeOracle; }
            set { m_DbtypeOracle = value; }
        }

        public string ErpDbConnectString
        {
            get { return m_ErpDbConnectString; }
            set { m_ErpDbConnectString = value; }
        }
        public string ErpUserID
        {
            get { return m_ErpUserID; }
            set { m_ErpUserID = value; }
        }
        public string ErpPassword
        {
            get { return m_ErpPassword; }
            set { m_ErpPassword = value; }
        }
        public string ErpServer
        {
            get { return m_ErpServer; }
            set { m_ErpServer = value; }
        }
        public string ErpDbType
        {
            get { return m_ErpDbType; }
            set { m_ErpDbType = value; }
        }


        public string QmsDbConnectString
        {
            get { return m_QmsDbConnectString; }
            set { m_QmsDbConnectString = value; }
        }
      
      
        public string QmsDbType
        {
            get { return m_QmsDbType; }
            set { m_QmsDbType = value; }
        }
        #endregion
    }
}
