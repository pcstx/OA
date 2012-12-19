using System;
using System.Collections.Generic;
using System.Text;
namespace GPRP.Entity
{
    /// <summary>
    /// 实体
    /// </summary>
    public class PSACNEntity
    {

        private string m_PSACNCO;//代码
        private string m_PSACNNCN;//简体说明
        private string m_PSACNNEN;//英文说明
        private string m_PSACNNTW;//繁体说明
        private string m_PSACNPRE;//前缀
        private string m_PSACNDAT;//日期类型
        private int m_PSACNLEN;//编号长度
        private int m_PSACNCUR;//当前编号
        private string m_PSACNPDA;//上一日期
        /// <summary>
        ///代码
        /// </summary>
        public string PSACNCO
        {
            get { return m_PSACNCO; }
            set { m_PSACNCO = value; }
        }
        /// <summary>
        ///简体说明
        /// </summary>
        public string PSACNNCN
        {
            get { return m_PSACNNCN; }
            set { m_PSACNNCN = value; }
        }
        /// <summary>
        ///英文说明
        /// </summary>
        public string PSACNNEN
        {
            get { return m_PSACNNEN; }
            set { m_PSACNNEN = value; }
        }
        /// <summary>
        ///繁体说明
        /// </summary>
        public string PSACNNTW
        {
            get { return m_PSACNNTW; }
            set { m_PSACNNTW = value; }
        }
        /// <summary>
        ///前缀
        /// </summary>
        public string PSACNPRE
        {
            get { return m_PSACNPRE; }
            set { m_PSACNPRE = value; }
        }
        /// <summary>
        ///日期类型
        /// </summary>
        public string PSACNDAT
        {
            get { return m_PSACNDAT; }
            set { m_PSACNDAT = value; }
        }
        /// <summary>
        ///编号长度
        /// </summary>
        public int PSACNLEN
        {
            get { return m_PSACNLEN; }
            set { m_PSACNLEN = value; }
        }
        /// <summary>
        ///当前编号
        /// </summary>
        public int PSACNCUR
        {
            get { return m_PSACNCUR; }
            set { m_PSACNCUR = value; }
        }
        /// <summary>
        ///上一日期
        /// </summary>
        public string PSACNPDA
        {
            get { return m_PSACNPDA; }
            set { m_PSACNPDA = value; }
        }
    }
}
