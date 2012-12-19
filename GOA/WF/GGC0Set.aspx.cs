using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Web.Services;
using GPRP.Web.UI;
using GPRP.GPRPComponents;
using GPRP.GPRPControls;
using GPRP.GPRPEnumerations;
using GPRP.GPRPBussiness;
using GPRP.Entity;
using YYControls;
using VBControls.VBProject;
using MyADO;

namespace GOA
{
    public partial class GGC0Set : BasePage
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            bindGridView();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
            }
        }

        private void bindGridView()
        {
            DataTable dtMainField = new DataTable();
            dtMainField = DbHelper.GetInstance().GetWorkflow_UserDefinedReportTableByReportID(DNTRequest.GetString("ReportID"));

            GridView1.DataSource = dtMainField;
            GridView1.DataBind();

            for (int i = 0; i < dtMainField.Rows.Count; i++)
            {
                string FieldID = dtMainField.Rows[i]["FieldID"].ToString();
                string FieldName = dtMainField.Rows[i]["FieldName"].ToString();
                int DataTypeID = Convert.ToInt32(dtMainField.Rows[i]["DataTypeID"]);
                string FieldDefaultValue = dtMainField.Rows[i]["DefaultValue"].ToString();
                int FieldHTMLTypeID = Convert.ToInt32(dtMainField.Rows[i]["HTMLTypeID"].ToString());
                int FieldTextLength = Convert.ToInt32(dtMainField.Rows[i]["TextLength"].ToString());
                int FieldTextHeight = Convert.ToInt32(dtMainField.Rows[i]["TextHeight"].ToString());
                int IsDynamic = Convert.ToInt32(dtMainField.Rows[i]["IsDynamic"].ToString());
                int DataSetID = Convert.ToInt32(dtMainField.Rows[i]["DataSetID"].ToString());
                string ValueColumn = dtMainField.Rows[i]["ValueColumn"].ToString();
                string TextColumn = dtMainField.Rows[i]["TextColumn"].ToString();

                switch (FieldHTMLTypeID)
                {
                    case 1:     //Label
                        break;
                    case 2:     //Textbox
                        //根据数据类型，来判断要创建的比较符dropdownlist的数据源

                        //如果数据类型DataTypeID=1,2,3是文本，直接加一个textbox
                        //DataTypeID=4,再加两个日期选择textbox, 两个比较符dropdownlist
                        if (DataTypeID == 1 || DataTypeID == 2 || DataTypeID == 3)
                        {
                            CreateCompareDropDownList(i, Int32.Parse(FieldID), DataTypeID);

                            GPRP.GPRPControls.TextBox txtField = new GPRP.GPRPControls.TextBox();
                            txtField.ID = "field" + FieldID;
                            txtField.Text = FieldDefaultValue;
                            txtField.Width = new Unit(100);
                            if (DataTypeID == 2 || DataTypeID == 3)
                            {
                                txtField.Attributes.Add("onkeypress", "javascript: return checkkey(this.value,event);");
                            }

                            txtField.Attributes.Add("onkeydown", "javascript:cbConditionChecked('" + ((CheckBox)GridView1.Rows[i].FindControl("cbCondition")).ClientID + "')");

                            ((PlaceHolder)GridView1.Rows[i].FindControl("placeHolder")).Controls.Add(txtField);
                        }
                        else
                        {
                            //如果是日期类型

                            CreateDateTimeTypeFieldControl(i, Int32.Parse(FieldID), DataTypeID);

                        }

                        break;
                    case 3:   //TextArea
                        CreateCompareDropDownList(i, Int32.Parse(FieldID), DataTypeID);

                        GPRP.GPRPControls.TextBox txtAreaField = new GPRP.GPRPControls.TextBox();
                        txtAreaField.ID = "field" + FieldID;
                        txtAreaField.TextMode = TextBoxMode.MultiLine;
                        txtAreaField.Width = new Unit(FieldTextLength);
                        txtAreaField.Rows = FieldTextHeight;
                        txtAreaField.Text = FieldDefaultValue;

                        txtAreaField.Attributes.Add("onkeydown", "javascript:cbConditionChecked('" + ((CheckBox)GridView1.Rows[i].FindControl("cbCondition")).ClientID + "')");

                        ((PlaceHolder)GridView1.Rows[i].FindControl("placeHolder")).Controls.Add(txtAreaField);
                        break;
                    case 4:  //checkboxList
                        CreateCompareDropDownList(i, Int32.Parse(FieldID), DataTypeID);

                        GPRP.GPRPControls.CheckBoxList chkLstField = new GPRP.GPRPControls.CheckBoxList();

                        chkLstField.RepeatDirection = RepeatDirection.Horizontal;
                        chkLstField.ID = "field" + FieldID;
                        chkLstField.RepeatColumns = 6;

                        //获得数据源

                        DataTable dtList = DbHelper.GetInstance().GetMultiSelectDataSource(Convert.ToInt32(FieldID), IsDynamic, DataSetID, ValueColumn, TextColumn);
                        chkLstField.AddTableData(dtList);
                        chkLstField.DataTextField = TextColumn;
                        chkLstField.DataValueField = ValueColumn;

                        chkLstField.Attributes.Add("onclick", "javascript:cbConditionChecked('" + ((CheckBox)GridView1.Rows[i].FindControl("cbCondition")).ClientID + "')");

                        ((PlaceHolder)GridView1.Rows[i].FindControl("placeHolder")).Controls.Add(chkLstField);
                        break;
                    case 5: // dropdownlist
                        CreateCompareDropDownList(i, Int32.Parse(FieldID), DataTypeID);

                        GPRP.GPRPControls.DropDownList ddlField = new GPRP.GPRPControls.DropDownList();
                        ddlField.ID = "field" + FieldID;
                        ddlField.Width = new Unit(120);
                        DataTable dtDrop = DbHelper.GetInstance().GetMultiSelectDataSource(Convert.ToInt32(FieldID), IsDynamic, DataSetID, ValueColumn, TextColumn);
                        ddlField.AddTableData(dtDrop);
                        ddlField.DataTextField = TextColumn;
                        ddlField.DataValueField = ValueColumn;

                        ddlField.SelectedValue = FieldDefaultValue;

                        ddlField.Attributes.Add("onfocus", "javascript:cbConditionChecked('" + ((CheckBox)GridView1.Rows[i].FindControl("cbCondition")).ClientID + "')");
                        ((PlaceHolder)GridView1.Rows[i].FindControl("placeHolder")).Controls.Add(ddlField);
                        break;
                    case 6: //checkbox,不用加比较符下拉框


                        System.Web.UI.WebControls.CheckBox chkField = new System.Web.UI.WebControls.CheckBox();
                        chkField.ID = "field" + FieldID;
                        chkField.Checked = false;
                        chkField.Attributes.Add("onfocus", "javascript:cbConditionChecked('" + ((CheckBox)GridView1.Rows[i].FindControl("cbCondition")).ClientID + "')");
                        ((PlaceHolder)GridView1.Rows[i].FindControl("placeHolder")).Controls.Add(chkField);
                        break;
                    case 7:   //uploadFile
                        break;

                    case 8:  //浏览形式按钮
                        CreateCompareDropDownList(i, Int32.Parse(FieldID), DataTypeID);

                        string BrowsePage = dtMainField.Rows[i]["BrowsePage"].ToString();
                        string BrowseTypeName = dtMainField.Rows[i]["BrowseTypeName"].ToString();
                        GPRP.GPRPControls.TextBox txtBrowseField = new GPRP.GPRPControls.TextBox();
                        txtBrowseField.ID = "fieldText" + FieldID;
                        txtBrowseField.Width = new Unit(120);
                        System.Web.UI.WebControls.HiddenField txtBrowseFieldValue = new System.Web.UI.WebControls.HiddenField();
                        txtBrowseFieldValue.ID = "field" + FieldID;
                        txtBrowseField.AddAttributes("readonly", "true");

                        //   txtBrowseField.Attributes.Add("ontextchanged", "javascript:cbConditionChecked('" + ((CheckBox)GridView1.Rows[i].FindControl("cbCondition")).ClientID + "')");
                        ((PlaceHolder)GridView1.Rows[i].FindControl("placeHolder")).Controls.Add(txtBrowseField);
                        ((PlaceHolder)GridView1.Rows[i].FindControl("placeHolder")).Controls.Add(txtBrowseFieldValue);
                        ImageButton imageButton = new ImageButton();
                        imageButton.ID = "fieldImage" + FieldID;
                        imageButton.ImageAlign = ImageAlign.Middle;
                        imageButton.ToolTip = "搜索";
                        imageButton.ImageUrl = "../images/arrow_black.gif";
                        imageButton.OnClientClick = "return btnBrowseFieldClick('" + txtBrowseField.ID + "','" + txtBrowseFieldValue.ID + "','" + BrowsePage + "','" + BrowseTypeName + "');";

                        imageButton.Attributes.Add("onfocus", "javascript:cbConditionChecked('" + ((CheckBox)GridView1.Rows[i].FindControl("cbCondition")).ClientID + "')");
                        ((PlaceHolder)GridView1.Rows[i].FindControl("placeHolder")).Controls.Add(imageButton);

                        break;
                }
            }
        }

        #region "创建Gridview中的条件设置控件"
        /// <summary>
        /// 创建比较符控件
        /// </summary>
        /// <param name="i"></param>
        /// <param name="FieldID"></param>
        /// <param name="DataTypeID"></param>

        private void CreateCompareDropDownList(int i, int FieldID, int DataTypeID)
        {
            GPRP.GPRPControls.DropDownList ddlField = new GPRP.GPRPControls.DropDownList();

            ddlField.ID = "ddlCompare" + FieldID;
            ddlField.Width = new Unit(100);
            DataTable dtDrop = DbHelper.GetInstance().GetWorkflow_UserDefinedReportConditionDropDownListSourceByDataTypeID(DataTypeID);
            ddlField.AddTableData(dtDrop);
            ddlField.DataTextField = "SymbolName";
            ddlField.DataValueField = "CompareSymbol";

            ddlField.Attributes.Add("onfocus", "javascript:cbConditionChecked('" + ((CheckBox)GridView1.Rows[i].FindControl("cbCondition")).ClientID + "')");
            ((PlaceHolder)GridView1.Rows[i].FindControl("placeHolder")).Controls.Add(ddlField);

        }

        /// <summary>
        /// 创建日期类型控件选框
        /// </summary>
        /// <param name="i"></param>
        /// <param name="FieldID"></param>
        /// <param name="DataTypeID"></param>

        private void CreateDateTimeTypeFieldControl(int i, int FieldID, int DataTypeID)
        {
            GPRP.GPRPControls.DropDownList ddlField = new GPRP.GPRPControls.DropDownList();
            ddlField.ID = "ddlCompareT1" + FieldID;
            ddlField.Width = new Unit(100);
            DataTable dtDrop = DbHelper.GetInstance().GetWorkflow_UserDefinedReportConditionDropDownListSourceByDataTypeID(DataTypeID);
            ddlField.AddTableData(dtDrop);
            ddlField.DataTextField = "SymbolName";
            ddlField.DataValueField = "CompareSymbol";

            ddlField.Attributes.Add("onfocus", "javascript:cbConditionChecked('" + ((CheckBox)GridView1.Rows[i].FindControl("cbCondition")).ClientID + "')");

            ((PlaceHolder)GridView1.Rows[i].FindControl("placeHolder")).Controls.Add(ddlField);

            //建立一个textbox
            GPRP.GPRPControls.TextBox txtField1 = new GPRP.GPRPControls.TextBox();
            txtField1.ID = "fieldT1" + FieldID;
            txtField1.Width = new Unit(100);
            txtField1.ReadOnly = true;
            txtField1.Attributes.Add("onfocus", "javascript:cbConditionChecked('" + ((CheckBox)GridView1.Rows[i].FindControl("cbCondition")).ClientID + "')");
            ((PlaceHolder)GridView1.Rows[i].FindControl("placeHolder")).Controls.Add(txtField1);

            //建立一个日期选择控件
            AjaxControlToolkit.CalendarExtender ceStart = new AjaxControlToolkit.CalendarExtender();
            ceStart.ID = "ceS" + FieldID;
            ceStart.TargetControlID = txtField1.ID;
            ceStart.Format = "yyyy-MM-dd";
            ceStart.PopupPosition = AjaxControlToolkit.CalendarPosition.TopRight;
            ;

            ((PlaceHolder)GridView1.Rows[i].FindControl("placeHolder")).Controls.Add(ceStart);

            GPRP.GPRPControls.DropDownList ddlField2 = new GPRP.GPRPControls.DropDownList();
            ddlField2.ID = "ddlCompareT2" + FieldID;
            ddlField2.Width = new Unit(100);
            DataTable dtDrop2 = DbHelper.GetInstance().GetWorkflow_UserDefinedReportConditionDropDownListSourceByDataTypeID(DataTypeID);
            ddlField2.AddTableData(dtDrop2);
            ddlField2.DataTextField = "SymbolName";
            ddlField2.DataValueField = "CompareSymbol";

            ddlField2.Attributes.Add("onfocus", "javascript:cbConditionChecked('" + ((CheckBox)GridView1.Rows[i].FindControl("cbCondition")).ClientID + "')");

            ((PlaceHolder)GridView1.Rows[i].FindControl("placeHolder")).Controls.Add(ddlField2);

            //建立一个textbox
            GPRP.GPRPControls.TextBox txtField2 = new GPRP.GPRPControls.TextBox();
            txtField2.ID = "fieldT2" + FieldID;
            txtField2.Width = new Unit(100);
            txtField2.ReadOnly = true;
            txtField2.Attributes.Add("onfocus", "javascript:cbConditionChecked('" + ((CheckBox)GridView1.Rows[i].FindControl("cbCondition")).ClientID + "')");

            ((PlaceHolder)GridView1.Rows[i].FindControl("placeHolder")).Controls.Add(txtField2);

            //建立一个日期选择控件
            AjaxControlToolkit.CalendarExtender ceEnd = new AjaxControlToolkit.CalendarExtender();
            ceEnd.ID = "ceE" + FieldID;
            ceEnd.TargetControlID = txtField2.ID;
            ceEnd.Format = "yyyy-MM-dd";
            ceEnd.PopupPosition = AjaxControlToolkit.CalendarPosition.TopRight;
            ((PlaceHolder)GridView1.Rows[i].FindControl("placeHolder")).Controls.Add(ceEnd);


        }
        #endregion

        //Show Header/Footer of Gridview with Empty Data Source 
        private void BuildNoRecords(GridView gridView, DataTable ds)
        {
            try
            {
                if (ds.Rows.Count == 0)
                {
                    ds.Rows.Add(ds.NewRow());
                    gridView.DataSource = ds;
                    gridView.DataBind();
                    int columnCount = gridView.Rows[0].Cells.Count;
                    gridView.Rows[0].Cells.Clear();
                    gridView.Rows[0].Cells.Add(new TableCell());
                    gridView.Rows[0].Cells[0].ColumnSpan = columnCount;
                    gridView.Rows[0].Cells[0].Text = "No Records Found.";
                }
            }
            catch
            {
            }
        }

        #region "GridView事件"

        //此类要进行dorpdownlist/chk控件的转换
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                DataRowView dv = (DataRowView)e.Row.DataItem;
                string strIsShow = dv.Row["IsShow"].ToString();

                System.Web.UI.WebControls.CheckBox cbIsShow = (System.Web.UI.WebControls.CheckBox)e.Row.FindControl("cbIsShow");
                cbIsShow.Checked = strIsShow.Equals("1");
                cbIsShow.Visible = strIsShow.Equals("1");
            }
        }
        #endregion

        //搜寻
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string strCondition = " rm.ReportID=" + DNTRequest.GetString("ReportID"),
                   strColumns = "";
            SortedList slOrder = new SortedList(), slColumns = new SortedList(), slStatistics = new SortedList();
            DataTable dt = DbHelper.GetInstance().GetWorkflow_ReportDetailDataTableByKeyCol(DNTRequest.GetString("ReportID"));

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < GridView1.Rows.Count; i++)
                {

                    DataKey dk = GridView1.DataKeys[i];
                    int GroupID = Convert.ToInt32(dk[1]);
                    string FieldID = dk[2].ToString();
                    string FieldName = dk[3].ToString();
                    int DataTypeID = Convert.ToInt32(dk[4]);
                    int FieldHTMLTypeID = Convert.ToInt32(dk[5]);
                    string tablePrefix = "";

                    if (dt.Select("FieldID=" + FieldID).Length > 0)
                    {
                        DataRow dvF = dt.Select("FieldID=" + FieldID)[0];  //用于设置排序及统计字段



                        System.Web.UI.WebControls.CheckBox cbIsShow = (System.Web.UI.WebControls.CheckBox)GridView1.Rows[i].FindControl("cbIsShow");

                        if (GroupID == 0)
                            tablePrefix = "m.";
                        else
                            tablePrefix = "d.";

                        //设置排序表达式
                        if (dvF["IsOrder"].ToString() == "1")
                        {
                            if (dvF["OrderPattern"].ToString() == "2")
                                slOrder.Add(dvF["OrderIndex"], tablePrefix + FieldName + " DESC");
                            else
                                slOrder.Add(dvF["OrderIndex"], tablePrefix + FieldName);
                        }

                        //设置列的显示顺序、统计字段顺序
                        if (cbIsShow.Checked)
                        {
                            slColumns.Add(dvF["DisplayOrder"], tablePrefix + FieldName);
                            if (dvF["IsStatistics"].ToString() == "1")
                                slStatistics.Add(dvF["DisplayOrder"], tablePrefix + FieldName);
                        }

                        System.Web.UI.WebControls.CheckBox cbCondition = (System.Web.UI.WebControls.CheckBox)GridView1.Rows[i].FindControl("cbCondition");
                        PlaceHolder ph = (PlaceHolder)GridView1.Rows[i].FindControl("placeHolder");

                        if (cbCondition.Checked)
                        {
                            switch (FieldHTMLTypeID)
                            {
                                case 1:     //Label
                                    break;
                                case 2:     //Textbox
                                    if (DataTypeID == 1 || DataTypeID == 2 || DataTypeID == 3)
                                    {
                                        GPRP.GPRPControls.DropDownList ddlField = (GPRP.GPRPControls.DropDownList)ph.FindControl("ddlCompare" + FieldID);
                                        GPRP.GPRPControls.TextBox txtField = (GPRP.GPRPControls.TextBox)ph.FindControl("field" + FieldID);

                                        if (txtField.Text.Trim() != "")
                                        {
                                            if (DataTypeID == 1)
                                                if (ddlField.SelectedValue.ToUpper() == "LIKE" || ddlField.SelectedValue.ToUpper().Replace(" ", "") == "NOTLIKE")
                                                    strCondition += " and " + tablePrefix + FieldName + ddlField.SelectedValue + "'%" + txtField.Text.Trim() + "%'";
                                                else
                                                    strCondition += " and " + tablePrefix + FieldName + ddlField.SelectedValue + "'" + txtField.Text.Trim() + "'";
                                            else
                                                strCondition += " and " + tablePrefix + FieldName + ddlField.SelectedValue + txtField.Text.Trim();
                                        }
                                    }
                                    else
                                    {
                                        GPRP.GPRPControls.DropDownList ddlField1 = (GPRP.GPRPControls.DropDownList)ph.FindControl("ddlCompareT1" + FieldID);
                                        GPRP.GPRPControls.DropDownList ddlField2 = (GPRP.GPRPControls.DropDownList)ph.FindControl("ddlCompareT2" + FieldID);
                                        GPRP.GPRPControls.TextBox txtField1 = (GPRP.GPRPControls.TextBox)ph.FindControl("fieldT1" + FieldID);
                                        GPRP.GPRPControls.TextBox txtField2 = (GPRP.GPRPControls.TextBox)ph.FindControl("fieldT2" + FieldID);

                                        if (txtField1.Text.Trim() != "")
                                        {
                                            strCondition += " and " + tablePrefix + FieldName + ddlField1.SelectedValue + "'" + txtField1.Text.Trim() + "'";
                                        }
                                        if (txtField2.Text.Trim() != "")
                                        {
                                            strCondition += " and " + tablePrefix + FieldName + ddlField2.SelectedValue + "'" + txtField2.Text.Trim() + "'";
                                        }
                                    }

                                    break;
                                case 3:   //TextArea
                                    GPRP.GPRPControls.DropDownList ddlField3 = (GPRP.GPRPControls.DropDownList)ph.FindControl("ddlCompare" + FieldID);
                                    GPRP.GPRPControls.TextBox txtField3 = (GPRP.GPRPControls.TextBox)ph.FindControl("field" + FieldID);

                                    if (txtField3.Text.Trim() != "")
                                    {
                                        if (ddlField3.SelectedValue.ToUpper() == "LIKE" || ddlField3.SelectedValue.ToUpper().Replace(" ", "") == "NOTLIKE")
                                            strCondition += " and " + tablePrefix + FieldName + ddlField3.SelectedValue + "'%" + txtField3.Text.Trim() + "%'";
                                        else
                                            strCondition += " and " + tablePrefix + FieldName + ddlField3.SelectedValue + "'" + txtField3.Text.Trim() + "'";
                                    }

                                    break;
                                case 4:  //checkboxList
                                    GPRP.GPRPControls.DropDownList ddlField4 = (GPRP.GPRPControls.DropDownList)ph.FindControl("ddlCompare" + FieldID);
                                    GPRP.GPRPControls.CheckBoxList chkLstField = (GPRP.GPRPControls.CheckBoxList)ph.FindControl("field" + FieldID); ;

                                    if (chkLstField.SelectedIndex >= 0)
                                    {
                                        for (int j = 0; j < chkLstField.Items.Count; j++)
                                        {
                                            if (chkLstField.Items[j].Selected)
                                            {
                                                if (ddlField4.SelectedValue.ToUpper() == "LIKE" || ddlField4.SelectedValue.ToUpper().Replace(" ", "") == "NOTLIKE")
                                                    strCondition += " and " + tablePrefix + FieldName + ddlField4.SelectedValue + "'%" + chkLstField.Items[j].Value + "%'";
                                                else
                                                    strCondition += " and " + tablePrefix + FieldName + ddlField4.SelectedValue + "'" + chkLstField.Items[j].Value + "'";
                                            }
                                        }
                                    }

                                    break;
                                case 5: // dropdownlist

                                    GPRP.GPRPControls.DropDownList ddlField5 = (GPRP.GPRPControls.DropDownList)ph.FindControl("ddlCompare" + FieldID);
                                    GPRP.GPRPControls.DropDownList dropField = (GPRP.GPRPControls.DropDownList)ph.FindControl("field" + FieldID); ;

                                    if (ddlField5.SelectedValue.ToUpper() == "LIKE" || ddlField5.SelectedValue.ToUpper().Replace(" ", "") == "NOTLIKE")
                                        strCondition += " and " + tablePrefix + FieldName + ddlField5.SelectedValue + "'%" + dropField.SelectedValue + "%'";
                                    else
                                        strCondition += " and " + tablePrefix + FieldName + ddlField5.SelectedValue + "'" + dropField.SelectedValue + "'";

                                    break;
                                case 6: //checkbox,不用加比较符下拉框



                                    System.Web.UI.WebControls.CheckBox chkField = (System.Web.UI.WebControls.CheckBox)ph.FindControl("field" + FieldID);

                                    if (chkField.Checked)
                                        strCondition += " and " + tablePrefix + FieldName + "='1'";

                                    break;
                                case 7:   //uploadFile
                                    break;

                                case 8:  //浏览形式按钮
                                    GPRP.GPRPControls.DropDownList ddlField8 = (GPRP.GPRPControls.DropDownList)ph.FindControl("ddlCompare" + FieldID);
                                    System.Web.UI.WebControls.HiddenField txtBrowseFieldValue = (System.Web.UI.WebControls.HiddenField)ph.FindControl("field" + FieldID); ;

                                    if (txtBrowseFieldValue.Value.Trim() != "")
                                    {
                                        if (ddlField8.SelectedValue.ToUpper() == "LIKE" || ddlField8.SelectedValue.ToUpper().Replace(" ", "") == "NOTLIKE")
                                            strCondition += " and " + tablePrefix + FieldName + ddlField8.SelectedValue + "'%" + txtBrowseFieldValue.Value.Trim() + "%'";
                                        else
                                            strCondition += " and " + tablePrefix + FieldName + ddlField8.SelectedValue + "'" + txtBrowseFieldValue.Value.Trim() + "'";
                                    }
                                    break;
                            }
                        }
                    }
                }
                //对SortedList进行分析

                string strOrder = "", //排序
                       strSum = "";//用来sum的列



                for (int i = 0; i < slColumns.Keys.Count; i++)
                    strColumns += slColumns.GetValueList()[i].ToString() + ",";

                for (int i = 0; i < slOrder.Keys.Count; i++)
                    strOrder += slOrder.GetValueList()[i].ToString() + ",";

                for (int i = 0; i < slStatistics.Keys.Count; i++)
                    strSum += slStatistics.GetValueList()[i].ToString() + ",";

                /*     string strGroupBy = "";//Groupby的列（strColumns中除去strSum之后的字串）
                        SortedList slGroupBy = new SortedList(); 
                        slGroupBy = slColumns;

                   if (slGroupBy.Count > 0 && slStatistics.Count>0)
                       {
                       for (int i = 0; i < slStatistics.Keys.Count; i++)
                           slGroupBy.Remove(slStatistics.GetKey[i]);
                       }

                   for (int i = 0; i < slGroupBy.Keys.Count; i++)
                       strGroupBy += slGroupBy.GetValueList()[i].ToString() + ",";
   
                   strGroupBy = strGroupBy.Substring(0, strGroupBy.Length - 1);
                */
                strColumns = strColumns.Substring(0, strColumns.Length - 1);
                strOrder = strOrder.Substring(0, strOrder.Length - 1);
                strSum = strSum.Substring(0, strSum.Length - 1);

                if (strColumns == "")
                {
                    //  string strScript = "<script type='text/javascript'  lanuage='javascript'> alert('" + ResourceManager.GetString("Operation_RECORD") + "'); </script>";
                    string strScript = "<script type='text/javascript'  lanuage='javascript'> alert('您没有选择任何报表显示字段，请确认！'); </script>";
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", strScript, false);

                }
                else
                {
                    Response.Redirect("GGC0Report.aspx?ReportID=" + DNTRequest.GetString("ReportID") + "&strCondition=" + Server.UrlEncode(strCondition) + "&strColumns=" + Server.UrlEncode(strColumns) + "&strOrder=" + Server.UrlEncode(strOrder) + "&strGroupBy=" + Server.UrlEncode(strSum));
                }
            }


            System.Web.UI.ScriptManager.RegisterStartupScript(btnSearch, this.GetType(), "ButtonHideScript", strButtonHideScript, false);
        }

        //清空所有条件

        protected void btnClear_Click(object sender, EventArgs e)
        {
            bindGridView();
            System.Web.UI.ScriptManager.RegisterStartupScript(btnClear, this.GetType(), "ButtonHideScript", strButtonHideScript, false);
        }


    }
}
