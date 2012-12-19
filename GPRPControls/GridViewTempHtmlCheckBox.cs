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
        /// ���޲ι������ʵ�֣��������
        /// </summary>
        public GridViewTempHtmlCheckBox()
            : base()
        {
        }

        /// <summary>
        /// �����ͷ�ж���"<input id='Checkbox2' type='checkbox' onclick='CheckAll(this)'/>"+
        /// </summary>
        /// <param name="strColumnText">��ͷ�ַ���</param>
        public GridViewTempHtmlCheckBox(string strColumnText)
        {
            templateType = DataControlRowType.Header;
            m_strColumnText = strColumnText;
        }

        /// <summary>
        /// ����Ԫ���ж���
        /// </summary>
        /// <param name="strTxtID">��ǰTextBox�ؼ�ID</param>
        /// <param name="strField">��ǰTextBox�ؼ��󶨵��ֶ�</param>
        /// <param name="bReadOnly">TextBox�Ƿ�ֻ��</param>
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
        //        //����������������ݿ�������ֶε�ֵ����������Ĭ��ѡ�������Ӧ�ã����Լ��ķ����ˡ�
        //        //����ֻ�����ӣ���һ���������ٷѻ���
        //        DataRowView gv = (DataRowView)e.Row.DataItem;
        //        int itemSeleted = Int32.Parse(gv.Row["id"].ToString()) > 3 ? 0 : Int32.Parse(gv.Row["id"].ToString());
        //        DropDownList dr = (DropDownList)e.Row.FindControl("dropdown");
        //        dr.SelectedIndex = itemSeleted;
        //    }
        //}
    }
}
