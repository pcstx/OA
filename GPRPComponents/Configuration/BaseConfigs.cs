﻿using System;

namespace GPRP.GPRPComponents
{
	/// <summary>
    /// 基本设置类
	/// </summary>
	public class BaseConfigs
	{
        private static object lockHelper = new object();
        private static System.Timers.Timer baseConfigTimer = new System.Timers.Timer(15000);
        private static BaseConfigInfo m_configinfo;

		/// <summary>
        /// 静态构造函数初始化相应实例和定时器
		/// </summary>
        static BaseConfigs()
        {
            m_configinfo = BaseConfigFileManager.LoadConfig();
            baseConfigTimer.AutoReset = true;
            baseConfigTimer.Enabled = true;
            baseConfigTimer.Elapsed += new System.Timers.ElapsedEventHandler(Timer_Elapsed); 
            baseConfigTimer.Start();
        }

        private static void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            ResetConfig();
        }

        /// <summary>
        /// 重设配置类实例        /// </summary>
        public static void ResetConfig()
        {
            m_configinfo = BaseConfigFileManager.LoadRealConfig();
        }

		public static BaseConfigInfo GetBaseConfig()
		{
            return m_configinfo;
		}

        /// <summary>
        /// 返回论坛路径
        /// </summary>
        public static string GetForumPath
        {
            get
            {
                return GetBaseConfig().Forumpath;
            }
        }
		/// <summary>
		/// 返回数据库连接串
		/// </summary>
		public static string GetDBConnectString
		{
			get
			{
				return GetBaseConfig().Dbconnectstring;
			}
        }
        /// <summary>
        /// 返回数据库连接串
        /// </summary>
        public static string GetDBOracleConnectString
        {
            get
            {
                return GetBaseConfig().DbOracleconnectstring;
            }
        }
        /// <summary>
        /// 返回数据库连接串
        /// </summary>
        public static string GetErpDbConnectString
        {
            get
            {
                return GetBaseConfig().ErpDbConnectString;
            }
        }
        /// <summary>
        /// 返回数据库连接串
        /// </summary>
        public static string GetQmsDbConnectString
        {
            get
            {
                return GetBaseConfig().QmsDbConnectString ;
            }
        }
        /// <summary>
        /// 数据库类型        /// </summary>
        public static string GetDbType
        {
            get
            {
                return GetBaseConfig().Dbtype;
            }
        }
        /// <summary>
        /// 数据库类型        /// </summary>
        public static string GetDbOracleType
        {
            get
            {
                return GetBaseConfig().DbtypeOracle;
            }
        }
        /// <summary>
        /// 数据库类型        /// </summary>
        public static string GetErpDbType
        {
            get
            {
                return GetBaseConfig().ErpDbType;
            }
        }

        /// <summary>
        ///QMS 数据库类型
        /// </summary>
        public static string GetQmsDbType
        {
            get
            {
                return GetBaseConfig().QmsDbType;
            }
        }

        /// <summary>
        /// 保存配置实例
        /// </summary>
        /// <param name="baseconfiginfo"></param>
        /// <returns></returns>
        public static bool SaveConfig(BaseConfigInfo baseconfiginfo)
        {
            BaseConfigFileManager acfm = new BaseConfigFileManager();
            BaseConfigFileManager.ConfigInfo = baseconfiginfo;
            return acfm.SaveConfig();
        }
	}
}
