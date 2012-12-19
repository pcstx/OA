using System;
using System.Collections.Generic;
using System.Text;

namespace GPRP.Entity
{
    public class SysTableColumnEntity
    {
        private string m_TableName;
        private string m_ColName;
        private string m_ColType;
        private string m_ColDescriptionCN;
        private string m_ColDescriptionEN;
        private string m_ColDescriptionTW;
        private string m_ColIsShow;
        private int m_ColShowOrder;
        private string m_UserID;
        private string m_ColMatchTable;
        private string m_MatchTableColValue;
        private string m_MatchTableColTextCN;
        private string m_MatchTableColTextEN;
        private string m_MatchTableColTextTW;
        private string m_MatchTableSqlText;
        private string m_MatchTableTree;
        private string m_IsSearchCondi;
        private int m_ColWidth;
        private string m_IsKey;

        /// <summary>
        /// 表名称
        /// </summary>
        public string TableName
        {
            get { return m_TableName; }
            set { m_TableName = value; }
        }
        /// <summary>
        /// 列名
        /// </summary>
        public string ColName
        {
            get { return m_ColName; }
            set { m_ColName = value; }
        }
        /// <summary>
        /// 类型 Label CheckBox DropDownList Image TextBox
        /// </summary>
        public string ColType
        {
            get { return m_ColType; }
            set { m_ColType = value; }
        }
        /// <summary>
        /// 列名的中文描述
        /// </summary>
        public string ColDescriptionCN
        {
            get { return m_ColDescriptionCN; }
            set { m_ColDescriptionCN = value; }
        }
        /// <summary>
        /// 列名的英文描述
        /// </summary>
        public string ColDescriptionEN
        {
            get { return m_ColDescriptionEN; }
            set { m_ColDescriptionEN = value; }
        }
        /// <summary>
        /// 列名的繁体描述
        /// </summary>
        public string ColDescriptionTW
        {
            get { return m_ColDescriptionTW; }
            set { m_ColDescriptionTW = value; }
        }
        /// <summary>
        /// 是否显示
        /// </summary>
        public string ColIsShow
        {
            get { return m_ColIsShow; }
            set { m_ColIsShow = value; }
        }
        /// <summary>
        /// 显示的顺序
        /// </summary>
        public int ColShowOrder
        {
            get { return m_ColShowOrder; }
            set { m_ColShowOrder = value; }
        }
        /// <summary>
        /// 用户ID号
        /// </summary>
        public string UserID
        {
            get { return m_UserID; }
            set { m_UserID = value; }
        }
        /// <summary>
        /// 如果是CheckBox /DropDownList  那么对应的表名
        /// </summary>
        public string ColMatchTable
        {
            get { return m_ColMatchTable; }
            set { m_ColMatchTable = value; }
        }
        /// <summary>
        /// 对应表名对应的值列名称
        /// </summary>
        public string MatchTableColValue
        {
            get { return m_MatchTableColValue; }
            set { m_MatchTableColValue = value; }
        }
        /// <summary>
        /// 对应表名 列显示内容 （中文）
        /// </summary>
        public string MatchTableColTextCN
        {
            get { return m_MatchTableColTextCN; }
            set { m_MatchTableColTextCN = value; }
        }
        /// <summary>
        /// 对应表名 列显示内容 （英文）
        /// </summary>
        public string MatchTableColTextEN
        {
            get { return m_MatchTableColTextEN; }
            set { m_MatchTableColTextEN = value; }
        }
        /// <summary>
        /// 对应表名 列显示内容 （繁体）
        /// </summary>
        public string MatchTableColTextTW
        {
            get { return m_MatchTableColTextTW; }
            set { m_MatchTableColTextTW = value; }
        }
        /// <summary>
        /// 可能用到的sql
        /// </summary>
        public string MatchTableSqlText
        {
            get { return m_MatchTableSqlText; }
            set { m_MatchTableSqlText = value; }
        }
        /// <summary>
        /// 是否树型显示
        /// </summary>
        public string MatchTableTree
        {
            get { return m_MatchTableTree; }
            set { m_MatchTableTree = value; }
        }
        /// <summary>
        /// 是否作为搜索条件
        /// </summary>
        public string IsSearchCondi
        {
            get { return m_IsSearchCondi; }
            set { m_IsSearchCondi = value; }
        }
        /// <summary>
        /// 列的宽度
        /// </summary>
        public int ColWidth
        {
            get { return m_ColWidth; }
            set { m_ColWidth = value; }
        }
        /// <summary>
        /// 该字段是否为key 
        /// </summary>
        public string IsKey
        {
            get { return m_IsKey; }
            set { m_IsKey = value; }
        }
    }
}
