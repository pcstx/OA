using System;
using System.Collections.Generic;
using System.Text;

namespace GPRP.Entity
{
    /// <summary>
    /// 部门实体
    /// </summary>
    public class PBDEPEntity
    {
        private string m_PBDEPID;//部门ID
        private string m_PBDEPDC;//部门代码
        private string m_PBDEPDN;//部门名称
        private string m_PBDEPDEN;//部门英文名称
        private string m_PBDEPDTWN;//部门繁体名称
        private string m_PBDEPPID;//上级部门ID
        private string m_PBDEPPDC;//上级部门代码
        private string m_PBDEPUS;//部门是否启用的标记
        private int m_PBDEPOI;   //排序顺序

        /// <summary>
        /// 部门ID
        /// </summary>
        public string DeptID
        {
            get { return m_PBDEPID; }
            set { m_PBDEPID = value; }
        }
        /// <summary>
        /// 部门代码
        /// </summary>
        public string DeptCode
        {
            get { return m_PBDEPDC; }
            set { m_PBDEPDC = value; }
        }
        /// <summary>
        /// 部门名称
        /// </summary>
        public string DeptName
        {
            get { return m_PBDEPDN; }
            set { m_PBDEPDN = value; }
        }
        /// <summary>
        /// 上级部门ID
        /// </summary>
        public string ParentDeptID
        {
            get { return m_PBDEPPID; }
            set { m_PBDEPPID = value; }
        }
        /// <summary>
        /// 上级部门代码
        /// </summary>
        public string ParentDeptCode
        {
            get { return m_PBDEPPDC; }
            set { m_PBDEPPDC = value; }

        }
        /// <summary>
        /// 部门是否启用的标记
        /// </summary>
        public string DeptIsValid
        {
            get { return m_PBDEPUS; }
            set { m_PBDEPUS = value; }
        }

        /// <summary>
        /// 排序顺序
        /// </summary>
        public int DeptOrderItem
        {
            get { return m_PBDEPOI; }
            set { m_PBDEPOI = value; }
        }
        /// <summary>
        /// 部门英文名称
        /// </summary>
        public string DeptEName
        {
            get { return m_PBDEPDEN; }
            set { m_PBDEPDEN = value; }
        }
        /// <summary>
        /// 部门繁体名称
        /// </summary>
        public string DeptTWName
        {
            get { return m_PBDEPDTWN; }
            set { m_PBDEPDTWN = value; }
        }
    }
}
