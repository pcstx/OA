using System;
using System.Collections.Generic;
using System.Text;

namespace GPRP.Entity
{
    public class PSSMETable
    {
        private string m_PSSMEMC;	//模块号
        private string m_PSSMEMN;	//菜单显示名称
        private string m_PSSMEMD;	//菜单描述
        private string m_PSSMEMP;	//连接的程序
        private string m_PSSMEPMC; //父项模块号
        private string m_PSSMEUS;	//是否使用的状况
        private string m_PSSMEMVS;	//鼠标移上的样式
        private string m_PSSMEMOS;	//鼠标移开的样式
        private string m_PSSMEMSS;	//选中后的样式
        private string m_PSSMEOWT;	//打开连接的目的窗口（_blank,_parent,_self,_mainFrame等）
        private string m_PSSMEUIP;	//使用的图标的路径
        private string m_PSSMEILST;	//一级菜单的下属ID号集合
        private string m_PSSMEMLST;	//一级菜单的下属模块号集合
        private int m_PSSMEID;	//流水ID号
        private int m_PSSMEOR; //显示顺序
        private string m_PSSMEMNEN;//菜单显示名称英文名
        private string m_PSSMEPRO = "2";//是否在开启流程的时候使用
        private string m_PSSMEMNTW;//菜单显示名称繁体版
        private int m_PSSMESOID; //CSS中要用到。主要是界面排序上的值问题,跟m_PSSMEOR是有区别的，此排序是在所有同级目录的排序
        private string m_PSSMEOLST;//下级且仅下一级菜单的PSSMEOID集合 跟 m_PSSMEILST 是有区别的。
        ///<summary>
        ///模块号
        ///</summary>
        public string ModleCode
        {
            get { return m_PSSMEMC; }
            set { m_PSSMEMC = value; }
        }
        ///<summary>
        ///菜单显示名称
        ///</summary>
        public string MenuName
        {
            get { return m_PSSMEMN; }
            set { m_PSSMEMN = value; }
        }  ///<summary>
        ///菜单描述
        ///</summary>
        public string MenuDescription
        {
            get { return m_PSSMEMD; }
            set { m_PSSMEMD = value; }
        }  ///<summary>
        ///连接的程序
        ///</summary>
        public string MenuLink
        {
            get { return m_PSSMEMP; }
            set { m_PSSMEMP = value; }
        }  ///<summary>
        ///父项模块号
        ///</summary>
        public string ModleParentCode
        {
            get { return m_PSSMEPMC; }
            set { m_PSSMEPMC = value; }
        }  ///<summary>
        ///是否使用的状况
        ///</summary>
        public string MenuIsValid
        {
            get { return m_PSSMEUS; }
            set { m_PSSMEUS = value; }
        }  ///<summary>
        ///鼠标移上的样式
        ///</summary>
        public string MenuMouseOverCss
        {
            get { return m_PSSMEMVS; }
            set { m_PSSMEMVS = value; }
        }  ///<summary>
        ///鼠标移开的样式
        ///</summary>
        public string MenuMouseLeaveCss
        {
            get { return m_PSSMEMOS; }
            set { m_PSSMEMOS = value; }
        }  ///<summary>
        ///选中后的样式
        ///</summary>
        public string MenuMouseDownCss
        {
            get { return m_PSSMEMSS; }
            set { m_PSSMEMSS = value; }
        }  ///<summary>
        ///打开连接的目的窗口（_blank,_parent,_self,_mainFrame等）
        ///</summary>
        public string MenuLinkTarget
        {
            get { return m_PSSMEOWT; }
            set { m_PSSMEOWT = value; }
        }  ///<summary>
        ///使用的图标的路径
        ///</summary>
        public string MenuImageUrl
        {
            get { return m_PSSMEUIP; }
            set { m_PSSMEUIP = value; }
        }  ///<summary>
        ///一级菜单的下属ID号集合
        ///</summary>
        public string MouldIDList
        {
            get { return m_PSSMEILST; }
            set { m_PSSMEILST = value; }
        }  ///<summary>
        ///一级菜单的下属模块号集合
        ///</summary>
        public string ModleCodeList
        {
            get { return m_PSSMEMLST; }
            set { m_PSSMEMLST = value; }
        }
        ///<summary>
        ///流水ID号
        ///</summary>
        public int ModleID
        {
            get { return m_PSSMEID; }
            set { m_PSSMEID = value; }
        }
        ///<summary>
        ///显示顺序
        ///</summary>
        public int OrderBy
        {
            get { return m_PSSMEOR; }
            set { m_PSSMEOR = value; }
        }
        /// <summary>
        /// 菜单显示名称英文名
        /// </summary>
        public string MouldNameEn
        {
            get { return m_PSSMEMNEN; }
            set { m_PSSMEMNEN = value; }
        }
        /// <summary>
        /// 是否在开启流程的时候使用 
        /// 0 不启动流程的时候不显示 1 启动流程的时候要显示 2 不跟流程有关
        /// 默认为2
        /// </summary>
        public string IsProcess
        {
            get { return m_PSSMEPRO; }
            set { m_PSSMEPRO = value; }
        }
        /// <summary>
        /// 菜单显示名称繁体版
        /// </summary>
        public string MouldNameTW
        {
            get { return m_PSSMEMNTW; }
            set { m_PSSMEMNTW = value; }
        }
        /// <summary>
        /// CSS中要用到。主要是界面排序上的值问题,跟m_PSSMEOR是有区别的，此排序是在所有同级目录的排序
        /// </summary>
        public int AllOrderBy
        {
            get { return m_PSSMESOID; }
            set { m_PSSMESOID = value; }

        }
        /// <summary>
        /// 下级且仅下一级菜单的PSSMEOID集合 跟 m_PSSMEILST 是有区别的。
        /// </summary>
        public string AllOrderByList
        {
            get { return m_PSSMEOLST; }
            set { m_PSSMEOLST = value; }
        }
    }
}
