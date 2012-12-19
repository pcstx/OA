using System;
using System.Collections.Generic;
using System.Text;
namespace GPRP.Entity
{
    /// <summary>
    /// 实体
    /// </summary>
    public class Workflow_HTMLTypeEntity
    {

        private int m_HTMLTypeID;//编号
        private string m_HTMLTypeName;//显示方式说明
        private string m_HTMLTypeDesc;//显示方式描述
        private string m_Useflag;//是否使用
        /// <summary>
        ///编号
        /// </summary>
        public int HTMLTypeID
        {
            get { return m_HTMLTypeID; }
            set { m_HTMLTypeID = value; }
        }
        /// <summary>
        ///显示方式说明
        /// </summary>
        public string HTMLTypeName
        {
            get { return m_HTMLTypeName; }
            set { m_HTMLTypeName = value; }
        }
        /// <summary>
        ///显示方式描述
        /// </summary>
        public string HTMLTypeDesc
        {
            get { return m_HTMLTypeDesc; }
            set { m_HTMLTypeDesc = value; }
        }
        /// <summary>
        ///是否使用
        /// </summary>
        public string Useflag
        {
            get { return m_Useflag; }
            set { m_Useflag = value; }
        }
    }
}