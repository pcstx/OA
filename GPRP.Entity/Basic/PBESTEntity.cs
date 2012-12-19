using System;
using System.Collections.Generic;
using System.Text;
namespace GPRP.Entity
{
    /// <summary>
    /// 实体
    /// </summary>
    public class PBESTEntity
    {

        private string m_PBESTTC;//员工状态类别代码
        private string m_PBESTTN;//员工状态类别名称
        private string m_PBESTTEN;//员工状态的英文名称
        private string m_PBESTTTWN;//员工状态的繁体名称
        private string m_PBESTUS;//是否使用的状况
        private int m_PBESTOI;//排序顺序
        /// <summary>
        ///员工状态类别代码
        /// </summary>
        public string PBESTTC
        {
            get { return m_PBESTTC; }
            set { m_PBESTTC = value; }
        }
        /// <summary>
        ///员工状态类别名称
        /// </summary>
        public string PBESTTN
        {
            get { return m_PBESTTN; }
            set { m_PBESTTN = value; }
        }
        /// <summary>
        ///员工状态的英文名称
        /// </summary>
        public string PBESTTEN
        {
            get { return m_PBESTTEN; }
            set { m_PBESTTEN = value; }
        }
        /// <summary>
        ///员工状态的繁体名称
        /// </summary>
        public string PBESTTTWN
        {
            get { return m_PBESTTTWN; }
            set { m_PBESTTTWN = value; }
        }
        /// <summary>
        ///是否使用的状况
        /// </summary>
        public string PBESTUS
        {
            get { return m_PBESTUS; }
            set { m_PBESTUS = value; }
        }
        /// <summary>
        ///排序顺序
        /// </summary>
        public int PBESTOI
        {
            get { return m_PBESTOI; }
            set { m_PBESTOI = value; }
        }
    }
}