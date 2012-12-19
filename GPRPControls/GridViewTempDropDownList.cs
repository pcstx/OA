using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
namespace GPRP.GPRPControls
{
    public class GridViewTempDropDownList : ITemplate
    {
        private DataControlRowType templateType;
        private bool m_bReadOnly;
        private string m_strTxtID;
        private string m_strColumnText;
        private string m_strField;
        private int m_Width=100;
        private DataTable dt;
        private bool mAutoPostBack = false;
        private int m_FieldID = 0;
        private int m_FieldHTMLType = 0;
        private int m_FieldValidType = 0;
        private string m_ValidTimeTypeName;
        public delegate void SelectedDropDownList(Object sender,string SelectValue);
        public event SelectedDropDownList selectDropDownList;
/// <summary>
        /// 此无参构造必须实现，否则出错
        /// </summary>
        public GridViewTempDropDownList() : base()
        {
        }

        /// <summary>
        /// 构造表头列对象
        /// </summary>
        /// <param name="strColumnText">表头字符串</param>
        public GridViewTempDropDownList(string strColumnText)
        {
            templateType = DataControlRowType.Header;
            m_strColumnText = strColumnText;
        }

        /// <summary>
        /// 构造元素行对象
        /// </summary>
        /// <param name="strTxtID">当前TextBox控件ID</param>
        /// <param name="strField">当前TextBox控件绑定的字段</param>
        /// <param name="bReadOnly">TextBox是否只读</param>
        public GridViewTempDropDownList(string strTxtID, string strHeader, string strField, bool bReadOnly, int txtWidth, DataTable data, bool AutoPostBack)
        {
             dt = data;
            templateType = DataControlRowType.DataRow;
            m_strTxtID = strTxtID;
            m_strField = strField;
            m_bReadOnly = bReadOnly;
            m_Width = txtWidth;
            mAutoPostBack = AutoPostBack;
            m_strColumnText = strHeader;
            
        }

        /// <summary>
        /// 构造元素行对象
        /// </summary>
        /// <param name="strTxtID">当前TextBox控件ID</param>
        /// <param name="strField">当前TextBox控件绑定的字段</param>
        /// <param name="bReadOnly">TextBox是否只读</param>
        public GridViewTempDropDownList(string strTxtID, string strHeader, string strField, bool bReadOnly, int txtWidth, DataTable data, bool AutoPostBack, int FieldID, int FieldHTMLType, int FieldValidType, string ValidTimeTypeName)
        {
            dt = data;
            templateType = DataControlRowType.DataRow;
            m_strTxtID = strTxtID;
            m_strField = strField;
            m_bReadOnly = bReadOnly;
            m_Width = txtWidth;
            mAutoPostBack = AutoPostBack;
            m_strColumnText = strHeader;
            m_FieldID = FieldID;
            m_FieldHTMLType = FieldHTMLType;
            m_FieldValidType = FieldValidType;
            m_ValidTimeTypeName = ValidTimeTypeName;


        }

        public void InstantiateIn(System.Web.UI.Control container)
        {
            switch (templateType)
            {
                case DataControlRowType.Header:
                    Literal l = new Literal();
                    l.Text = m_strColumnText;
                    container.Controls.Add(l);
                    break;

                case DataControlRowType.DataRow:
                    DropDownList drr = new DropDownList();
                    drr.ID = m_strTxtID;
                    drr.DataBinding += new EventHandler(DropDowList_DataBinding);
                    drr.AutoPostBack = mAutoPostBack;
                    if (mAutoPostBack) drr.SelectedIndexChanged += new EventHandler(drr_SelectedIndexChanged);
                    if (m_FieldValidType > 0)
                    {
                        drr.Attributes.Add(m_ValidTimeTypeName, "javascript:FormDetailFieldValidate(this,this.value,'" + m_FieldValidType.ToString() + "')");
                    }

                    container.Controls.Add(drr);

                    break;

                default:
                    break;
            }

        }

        void drr_SelectedIndexChanged(object sender, EventArgs e)
        {
            //获取当前触发事件的的DropDownList
            DropDownList dpl = (DropDownList)sender;
            if (dpl.SelectedIndex != -1)
            {
                if (selectDropDownList != null)
                {
                    selectDropDownList(dpl,dpl.SelectedValue.ToString());
                }
            } 
        }

        private void DropDowList_DataBinding(object sender, EventArgs e)
        {
            DropDownList drr = (DropDownList)sender;
            //drr.AppendDataBoundItems = true;
            drr.AddTableData(dt,false);
            
        }

        //protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        //{
        //    if (e.Row.RowType == DataControlRowType.DataRow)
        //    {
        //        //可以在这里访问数据库的其它字段的值，可以设置默认选择项，具体应用，看自己的发挥了。
        //        //下面只是例子，举一反三，不再费话了
        //        DataRowView gv = (DataRowView)e.Row.DataItem;
        //        int itemSeleted = Int32.Parse(gv.Row["id"].ToString()) > 3 ? 0 : Int32.Parse(gv.Row["id"].ToString());
        //        DropDownList dr = (DropDownList)e.Row.FindControl("dropdown");
        //        dr.SelectedIndex = itemSeleted;
        //    }
        //}

    }
}
