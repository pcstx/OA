using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
namespace GPRP.GPRPControls
{
    public class GridViewTempHtmlCheckBox : ITemplate
    {
        private DataControlRowType templateType;
        private bool m_bReadOnly;
        private string m_strTxtID;
        private string m_strColumnText;
        private string m_strField;
        private int m_Width = 100;


        /// <summary>
        /// 此无参构造必须实现，否则出错
        /// </summary>
        public GridViewTempHtmlCheckBox()
            : base()
        {
        }

        /// <summary>
        /// 构造表头列对象"<input id='Checkbox2' type='checkbox' onclick='CheckAll(this)'/>"+
        /// </summary>
        /// <param name="strColumnText">表头字符串</param>
        public GridViewTempHtmlCheckBox(string strColumnText)
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
        public GridViewTempHtmlCheckBox(string strTxtID, string strHeader, string strField, bool bReadOnly, int txtWidth)
        {

            templateType = DataControlRowType.DataRow;
            m_strTxtID = strTxtID;
            m_strField = strField;
            m_bReadOnly = bReadOnly;
            m_Width = txtWidth;
            m_strColumnText = strHeader;
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
                  
                    System.Web.UI.HtmlControls.HtmlInputCheckBox chk = new System.Web.UI.HtmlControls.HtmlInputCheckBox();
                    chk.ID = m_strTxtID;
                    chk.DataBinding += new EventHandler(chk_DataBinding);
                    container.Controls.Add(chk);

                    break;

                default:
                    break;
            }

        }

        private void chk_DataBinding(object sender, EventArgs e)
        {
            System.Web.UI.HtmlControls.HtmlInputCheckBox chk = (System.Web.UI.HtmlControls.HtmlInputCheckBox)sender;
            chk.Value = m_strField;
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
