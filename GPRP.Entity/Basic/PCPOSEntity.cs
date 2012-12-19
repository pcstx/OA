using System;
using System.Collections.Generic;
using System.Text;

namespace GPRP.Entity
{
    /// <summary>
    /// 职位实体
    /// </summary>
    public class PCPOSEntity
    {
        private string m_PCPOSDC;//职位代码
        private string m_PCPOSDN;//职位名称
        private string m_PCPOSDEN;//职位英文名称
        private string m_PCPOSDTWN;//职位繁体名称
        private string m_PCPOSPDC;//上级职位代码
        private string m_PCPOSUS;//职位是否启用的标记
        private int m_PCPOSOI;   //排序顺序

        /// <summary>
        /// 职位代码
        /// </summary>
        public string DeptCode
        {
            get { return m_PCPOSDC; }
            set { m_PCPOSDC = value; }


        }
        /// <summary>
        /// 职位名称
        /// </summary>
        public string DeptName
        {
            get { return m_PCPOSDN; }
            set { m_PCPOSDN = value; }
        }
        /// <summary>
        /// 上级职位代码
        /// </summary>
        public string ParentDeptCode
        {
            get { return m_PCPOSPDC; }
            set { m_PCPOSPDC = value; }

        }
        /// <summary>
        /// 职位是否启用的标记
        /// </summary>
        public string DeptIsValid
        {
            get { return m_PCPOSUS; }
            set { m_PCPOSUS = value; }
        }

        /// <summary>
        /// 排序顺序
        /// </summary>
        public int DeptOrderItem
        {
            get { return m_PCPOSOI; }
            set { m_PCPOSOI = value; }


        }
        /// <summary>
        /// 职位英文名称
        /// </summary>
        public string DeptEName
        {
            get { return m_PCPOSDEN; }
            set { m_PCPOSDEN = value; }

        }
        /// <summary>
        /// 职位繁体名称
        /// </summary>
        public string DeptTWName
        {
            get { return m_PCPOSDTWN; }
            set { m_PCPOSDTWN = value; }

        }

    }
}
