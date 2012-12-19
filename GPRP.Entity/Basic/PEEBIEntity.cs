using System;
using System.Collections.Generic;
using System.Text;
namespace GPRP.Entity
{
    /// <summary>
    /// 实体
    /// </summary>
    public class PEEBIEntity
    {

        //private string m_PBPRTTN;//职称
        //private string m_PEEBIENUP;//直接参与上司
        private string m_PEEBIEC;//员工编号
        private string m_PEEBIEN;//姓名
        //private string m_PEEBIEEN;//英文名
        //private string m_PEEBIETWN;//繁体中文名
        //private string m_PEEBIEG;//性别
        //private string m_PBSEXCN;//性别
        private DateTime m_PEEBIBD;//出生日期
       // private int m_PEEBIEA;//年龄
        private string m_PEEBIDEP;//所属部门
        // private string m_PBDEPDN;//部门
        //private string m_PEEBIDEPLink;//所属部门全称
        //private string m_PBDUTDN;//职务
        private string m_PEEBIDT;//职务
        private string m_PEEBIPC;//岗位
        //private string m_PBPOSPN;//岗位
        //private string m_PBPOCCC;//岗位种类
        //private string m_PBPOCCN;//岗位种类
        //private string m_PBEWTTN;//工种类别
        //private string m_PEEBIWT;//工种类别
        //private string m_PEEBIPT;//岗位性质
        //private string m_PBPOTTN;//岗位性质
        //private string m_PEEBIWP;//工序种类
        //private string m_PEEBIWPN;//工序种类
        //private string m_PEEBIET;//人员分类
        //private string m_PEEBIETN;//人员分类
        private string m_PEEBIDL;//直接主管
        //private string m_PEEBIDN;//职称
        //private DateTime m_PEEBIDNC;//评定时间
        //private double m_PEEBIWL;//工龄
        private DateTime m_PEEBIED;//入厂时间
       // private string m_PEEBIGF;//毕业学校
       // private string m_PEEBIEL;//学历
       // private string m_PBEDULN;//学历
       // private string m_PEEBILM;//专业
       // private DateTime m_PEEBIGD;//毕业时间
       // private string m_PEEBIIN;//身份证
       // private DateTime m_PEEBIIP;//发证日期
       // private int m_PEEBIVY;//有效期(年)
       // private string m_PEEBIRT;//户口
       //private string m_PBRETTN;//户口性质
       // private string m_PEEBIENA;//民族
        //private string m_PBNATNN;//民族
        //private string m_PBMARCN;//婚况
        //private string m_PEEBIMI;//婚况
        //private string m_PEEBINP;//籍贯
        //private string m_PEEBINPFullName;//籍贯
        //private string m_PEEBIRA;//户籍地址
        //private string m_PEEBISA;//现住地址
        private string m_PEEBICP;//联系电话
        //private string m_PEEBIECN;//联系人姓名
        //private string m_PEEBIECP;//紧急联系人电话
        private string m_PEEBIES;//员工状态
        //private string m_PBESTTN;//员工状态
        //private string m_PBESETN;//员工来源
        //private string m_PEEBIEST;//来源类别
        //private string m_PEEBIESN;//来源渠道
        //private Byte[] m_PEEBIEP;//照片
        //private string m_PEEBIEPHORN;//是否有照片
        //private DateTime m_PEETNTD;//转正时间
        //private string m_PEEBIML; //Email
        //private string m_PEEBIOAAU; //是否有OA帐号
      //  private string m_PEEBIDepID;
        private int m_PEEBIID;

  
        /// <summary>
        ///员工编号
        /// </summary>
        public string PEEBIEC
        {
            get { return m_PEEBIEC; }
            set { m_PEEBIEC = value; }
        }
        /// <summary>
        ///姓名
        /// </summary>
        public string PEEBIEN
        {
            get { return m_PEEBIEN; }
            set { m_PEEBIEN = value; }
        }
     
        /// <summary>
        ///出生日期
        /// </summary>
        public DateTime PEEBIBD
        {
            get { return m_PEEBIBD; }
            set { m_PEEBIBD = value; }
        }
 
        /// <summary>
        ///所属部门
        /// </summary>
        public string PEEBIDEP
        {
            get { return m_PEEBIDEP; }
            set { m_PEEBIDEP = value; }
        }

        /// <summary>
        ///职务
        /// </summary>
        public string PEEBIDT
        {
            get { return m_PEEBIDT; }
            set { m_PEEBIDT = value; }
        }
        /// <summary>
        ///岗位
        /// </summary>
        public string PEEBIPC
        {
            get { return m_PEEBIPC; }
            set { m_PEEBIPC = value; }
        }
  
  
     
        /// <summary>
        ///直接主管
        /// </summary>
        public string PEEBIDL
        {
            get { return m_PEEBIDL; }
            set { m_PEEBIDL = value; }
        }

        /// <summary>
        ///入厂时间
        /// </summary>
        public DateTime PEEBIED
        {
            get { return m_PEEBIED; }
            set { m_PEEBIED = value; }
        }

        /// <summary>
        ///联系电话
        /// </summary>
        public string PEEBICP
        {
            get { return m_PEEBICP; }
            set { m_PEEBICP = value; }
        }

        /// <summary>
        ///员工状态
        /// </summary>
        public string PEEBIES
        {
            get { return m_PEEBIES; }
            set { m_PEEBIES = value; }
        }

        ///// <summary>
        /////部门编号
        ///// </summary>
        //public string PEEBIDEPID
        //{
        //    get { return m_PEEBIDepID; }
        //    set { m_PEEBIDepID = value; }
        //}

        /// <summary>
        ///用户编号
        /// </summary>
        public int PEEBIID
        {
            get { return m_PEEBIID; }
            set { m_PEEBIID = value; }
        }
 
    }
}