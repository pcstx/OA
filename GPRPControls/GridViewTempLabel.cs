using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
namespace GPRP.GPRPControls
{
    public class GridViewTempLabel : ITemplate
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
        public GridViewTempLabel() : base()
        {
        }

        /// <summary>
        /// 构造表头列对象
        /// </summary>
        /// <param name="strColumnText">表头字符串</param>
        public GridViewTempLabel(string strColumnText)
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
        public GridViewTempLabel(string strTxtID,string strHeader, string strField, bool bReadOnly, int txtWidth)
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
                    Label lbl = new Label(); 
                    lbl.ID = m_strTxtID;
                    lbl.Width = Unit.Percentage(m_Width);
                    lbl.DataBinding += new EventHandler(lbl_DataBinding); 

                    container.Controls.Add(lbl);
                    break;

                default:
                    break;
            }

        }

        private void lbl_DataBinding(object sender, EventArgs e)
        {
            Label lbl = (Label)sender; 
            GridViewRow container = (GridViewRow)lbl.NamingContainer; 
            //lbl.Text = ((DataRowView)container.DataItem)[dataField].ToString();
            
            lbl.Text = DataBinder.Eval(container.DataItem, m_strField).ToString(); 
            lbl.Style.Add("TEXT-ALIGN", "right");
        }
    }
}
