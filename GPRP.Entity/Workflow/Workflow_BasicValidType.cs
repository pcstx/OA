using System;
using System.Collections.Generic;
using System.Text;
namespace GPRP.Entity
{
    /// <summary>
    /// 实体
    /// </summary>
    public class Workflow_BasicValidTypeEntity
    {

        private int m_ValidTypeID;//验证方式编号
        private string m_ValidTypeDesc;//验证方式描述
        private string m_ValidErrorMsg;//验证不通过时的提示信息
        private string  m_ValidRule;//验证用的正则表达式
        /// <summary>
        ///验证方式编号
        /// </summary>
        public int ValidTypeID
        {
            get { return m_ValidTypeID; }
            set { m_ValidTypeID = value; }
        }
        /// <summary>
        ///验证方式描述
        /// </summary>
        public string ValidTypeDesc
        {
            get { return m_ValidTypeDesc; }
            set { m_ValidTypeDesc = value; }
        }
        /// <summary>
        ///验证不通过时的提示信息
        /// </summary>
        public string ValidErrorMsg
        {
            get { return m_ValidErrorMsg; }
            set { m_ValidErrorMsg = value; }
        }
        /// <summary>
        ///验证用的正则表达式        /// </summary>
        public string  ValidRule
        {
            get { return m_ValidRule; }
            set { m_ValidRule = value; }
        }
    }
}