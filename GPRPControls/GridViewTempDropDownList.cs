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
        /// ���޲ι������ʵ�֣��������
        /// </summary>
        public GridViewTempDropDownList() : base()
        {
        }

        /// <summary>
        /// �����ͷ�ж���
        /// </summary>
        /// <param name="strColumnText">��ͷ�ַ���</param>
        public GridViewTempDropDownList(string strColumnText)
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
        /// ����Ԫ���ж���
        /// </summary>
        /// <param name="strTxtID">��ǰTextBox�ؼ�ID</param>
        /// <param name="strField">��ǰTextBox�ؼ��󶨵��ֶ�</param>
        /// <param name="bReadOnly">TextBox�Ƿ�ֻ��</param>
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
            //��ȡ��ǰ�����¼��ĵ�DropDownList
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
