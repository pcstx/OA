using System;
using System.Collections.Generic;
using System.Text;
namespace GPRP.Entity
{
    /// <summary>
    /// 实体
    /// </summary>
    public class PBPOSEntity
    {

        private string m_PBPOSPC;//代码
        private string m_PBPOSPN;//岗位名称(简体)
        private string m_PBPOSPEN;//岗位名称(英文)
        private string m_PBPOSPTWN;//岗位名称(繁体)
        private string m_PBPOSBD;//部门
        private string m_PBPOSPD;//岗位描述
        private string m_PBPOSRB;//岗位职能说明书路径
        private string m_PBPOSRBN;//岗位职能说明书名称
        private string m_PBPOSUS;//是否使用
        private string m_PBDUTDC;//
        private string m_PBDUTDN;//
        private string m_PBPOTTC;//员工岗位性质代码
        private string m_PBPOTTN;//员工岗位性质名称
        private string m_PBPOCCC;//员工岗位种类代码
        private string m_PBPOCCN;//员工岗位种类名称

        private string m_PBPOSWP;//工序种类
        private string m_PBPOSWPPN;//工序种类
        private string m_PBPOSET; //人员分类
        private string m_PBPOSETN;//人员分类
        private int m_PBPOSOI;//排序顺序

        /// <summary>
        ///代码
        /// </summary>
        public string PBPOSPC
        {
            get { return m_PBPOSPC; }
            set { m_PBPOSPC = value; }
        }
        /// <summary>
        ///岗位名称(简体)
        /// </summary>
        public string PBPOSPN
        {
            get { return m_PBPOSPN; }
            set { m_PBPOSPN = value; }
        }
        /// <summary>
        ///岗位名称(英文)
        /// </summary>
        public string PBPOSPEN
        {
            get { return m_PBPOSPEN; }
            set { m_PBPOSPEN = value; }
        }
        /// <summary>
        ///岗位名称(繁体)
        /// </summary>
        public string PBPOSPTWN
        {
            get { return m_PBPOSPTWN; }
            set { m_PBPOSPTWN = value; }
        }
        /// <summary>
        ///部门
        /// </summary>
        public string PBPOSBD
        {
            get { return m_PBPOSBD; }
            set { m_PBPOSBD = value; }
        }
        /// <summary>
        ///岗位描述
        /// </summary>
        public string PBPOSPD
        {
            get { return m_PBPOSPD; }
            set { m_PBPOSPD = value; }
        }
        /// <summary>
        ///岗位职能说明书路径
        /// </summary>
        public string PBPOSRB
        {
            get { return m_PBPOSRB; }
            set { m_PBPOSRB = value; }
        }
        /// <summary>
        ///岗位职能说明书名称
        /// </summary>
        public string PBPOSRBN
        {
            get { return m_PBPOSRBN; }
            set { m_PBPOSRBN = value; }
        }
        /// <summary>
        ///是否使用
        /// </summary>
        public string PBPOSUS
        {
            get { return m_PBPOSUS; }
            set { m_PBPOSUS = value; }
        }
        /// <summary>
        ///排序顺序
        /// </summary>
        public int PBPOSOI
        {
            get { return m_PBPOSOI; }
            set { m_PBPOSOI = value; }
        }
        /// <summary>
        ///职务的英文名称代码
        /// </summary>
        public string PBDUTDC
        {
            get { return m_PBDUTDC; }
            set { m_PBDUTDC = value; }
        }
        /// <summary>
        ///职务的英文名称
        /// </summary>
        public string PBDUTDN
        {
            get { return m_PBDUTDN; }
            set { m_PBDUTDN = value; }
        }
        /// <summary>
        ///员工岗位性质代码
        /// </summary>
        public string PBPOTTC
        {
            get { return m_PBPOTTC; }
            set { m_PBPOTTC = value; }
        }
        /// <summary>
        ///员工岗位性质名称
        /// </summary>
        public string PBPOTTN
        {
            get { return m_PBPOTTN; }
            set { m_PBPOTTN = value; }
        }
        /// <summary>
        ///员工岗位种类代码
        /// </summary>
        public string PBPOCCC
        {
            get { return m_PBPOCCC; }
            set { m_PBPOCCC = value; }
        }
        /// <summary>
        ///员工岗位种类名称
        /// </summary>
        public string PBPOCCN
        {
            get { return m_PBPOCCN; }
            set { m_PBPOCCN = value; }
        }

        /// <summary>
        ///工序种类
        /// </summary>
        public string PBPOSWP
        {
            get { return m_PBPOSWP; }
            set { m_PBPOSWP = value; }
        }
        /// <summary>
        ///工序种类
        /// </summary>
        public string PBPOSWPPN
        {
            get { return m_PBPOSWPPN; }
            set { m_PBPOSWPPN = value; }
        }
        /// <summary>
        ///人员分类
        /// </summary>
        public string PBPOSET
        {
            get { return m_PBPOSET; }
            set { m_PBPOSET = value; }
        }
        /// <summary>
        ///人员分类
        /// </summary>
        public string PBPOSETN
        {
            get { return m_PBPOSETN; }
            set { m_PBPOSETN = value; }
        }
    }
}