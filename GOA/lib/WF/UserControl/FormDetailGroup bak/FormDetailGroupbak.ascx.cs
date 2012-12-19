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

namespace GeobyWorkflow.UserControl
{
    public partial class FormDetailGroup : System.Web.UI.UserControl
    {
        public int NodeID;  //节点编号
        public int GroupID; //明细字段组        public int WorkflowID; //工作流程编号
        public int RequestID;  //工作流程实例编号
        public int FormID;     //表单编号
        public int IsGroupView;     //明细字段组是否可见
        public int IsGroupEdit;     //明细字段组是否可编辑
        public int IsGroupAdd;      //明细字段组是否可增加
        public int IsGroupDelete;   //明细字段组是否可删除

        public DataTable dtValue; ////明细字段组的值        public DataTable dtField;//明细组字段的定义信息
        private DataTable dtColumnIndex; //记录各明细字段在GridView中的顺序
        private DataTable dtColumnRuleResult;// 通过明细组的列规则计算而来的结果.暂时没有用途
        public int RightType;//当前用户对此流程实例的操作权限
        public FormDetailGroup()
        {
            dtColumnIndex = new DataTable();
            dtColumnIndex.Columns.Add("ColumnIndex", typeof(System.Int32));
            dtColumnIndex.Columns.Add("FieldID", typeof(System.Int32));
            dtColumnIndex.Columns.Add("FieldName", typeof(System.String));
            dtColumnIndex.Columns.Add("FieldHTMLType", typeof(System.Int32));
            dtColumnIndex.Columns.Add("FieldDBType", typeof(System.String));
            dtColumnIndex.Columns.Add("FieldValidType", typeof(System.Int32));
            dtColumnIndex.Columns.Add("ValidTimeTypeName", typeof(System.String));
            dtColumnIndex.Columns.Add("IsMandatory", typeof(System.Int32));
            dtColumnIndex.Columns.Add("IsEdit", typeof(System.Int32));
            dtColumnIndex.Columns.Add("TemplateType", typeof(System.String));
        }
        protected void Page_Init(object sender, EventArgs e)
        {
            //生成GridView的内容,循环dtField,生成GridView的column
            //先检查有哪些列规则
            if (RightType == 1)
            {
                btnAdd.Visible = false;
                btnDel.Visible = false;
            }
            else
            {
                if (IsGroupAdd == 0)
                {
                    btnAdd.Visible = false;
                }
                if (IsGroupDelete == 0)
                {
                    btnDel.Visible = false;
                }
            }
            for (int i = 0; i < dtField.Rows.Count; i++)
            {

                string FieldID = dtField.Rows[i]["FieldID"].ToString();
                string FieldName = dtField.Rows[i]["FieldName"].ToString();
                string FieldLable = dtField.Rows[i]["FieldLabel"].ToString();
                string FieldDefaultValue = dtField.Rows[i]["DefaultValue"].ToString();
                int FieldHTMLTypeID = Convert.ToInt32(dtField.Rows[i]["HTMLTypeID"].ToString());
                int FieldTextLength = Convert.ToInt32(dtField.Rows[i]["TextLength"].ToString());
                int FieldTextHeight = Convert.ToInt32(dtField.Rows[i]["TextHeight"].ToString());
                int FieldValidType = Convert.ToInt32(dtField.Rows[i]["BasicValidType"].ToString());
                string ValidTimeTypeName = dtField.Rows[i]["ValidTimeTypeName"].ToString();
                int IsView = Convert.ToInt32(dtField.Rows[i]["IsView"].ToString());
                int IsEdit = Convert.ToInt32(dtField.Rows[i]["IsEdit"].ToString());
                int IsMandatory = Convert.ToInt32(dtField.Rows[i]["IsMandatory"].ToString());
                string FieldDataType = dtField.Rows[i]["FieldDBType"].ToString();
                int FieldDataTypeID = Convert.ToInt32(dtField.Rows[i]["DataTypeID"].ToString());
                string FieldDateFormat = dtField.Rows[i]["Dateformat"].ToString();
                ////检查行列规则
                DataTable dtRule = DbHelper.GetInstance().GetFormDetailRule(FormID, GroupID, FieldName);
                if (IsView == 1)
                {
                    DataRow row = dtColumnIndex.NewRow();
                    row["ColumnIndex"] = i + 2;
                    row["FieldID"] = FieldID;
                    row["FieldName"] = FieldName;
                    row["FieldHTMLType"] = FieldHTMLTypeID;
                    row["FieldDBType"] = FieldDataType;
                    row["FieldValidType"] = FieldValidType;
                    row["ValidTimeTypeName"] = ValidTimeTypeName;
                    row["IsMandatory"] = IsMandatory;
                    row["IsEdit"] = IsEdit;


                    if (IsGroupEdit == 0 || RightType == 1)   //只读
                    {
                        BoundField mBoundField = new BoundField();
                        mBoundField.HeaderText = FieldLable;
                        mBoundField.DataField = FieldName;
                        //mBoundField.ItemStyle.Width = Unit.Pixel(FieldTextLength); ;
                        GridView1.Columns.Add(mBoundField);
                    }
                    else
                    {
                        //TemplateField a=new TemplateField();
                        GridViewTemplate templateField = new GridViewTemplate();
                        templateField.ColumnHearText = FieldLable;
                        templateField.ColumnValue = FieldName;
                        templateField.FieldHTMLType = FieldHTMLTypeID;
                        templateField.FieldID = Convert.ToInt32(FieldID);
                        templateField.FieldName = FieldName;
                        templateField.FieldDBType = FieldDataType;
                        templateField.FieldValidType = FieldValidType;
                        templateField.ValidTimeTypeName = ValidTimeTypeName;
                        templateField.ColumnID = FieldID;
                        templateField.dtRule = dtRule;
                        templateField.dtColumnsIndex = dtColumnIndex;
                        templateField.FieldDataType = FieldDataTypeID;
                        templateField.FieldDateFormat = FieldDateFormat;
                        templateField.IsMandtory = IsMandatory;
                        //}
                        switch (FieldHTMLTypeID)
                        {
                            case 1:     //Label
                                templateField.WebControlType = ControlType.Label;
                                row["TemplateType"] = "Label";

                                break;
                            case 2:     //Textbox
                                if (IsEdit == 1)
                                {
                                    templateField.WebControlType = ControlType.TextBox;
                                    row["TemplateType"] = "TextBox";
                                }
                                else
                                {
                                    templateField.WebControlType = ControlType.TextBoxIncludeHidden;
                                    row["TemplateType"] = "TextBoxIncludeHidden";
                                }
                                break;
                            case 3:   //TextArea
                                templateField.WebControlType = ControlType.TextBox;
                                row["TemplateType"] = "TextBox";
                                break;
                            case 4:  //checkboxList
                                row["TemplateType"] = "checkboxList";
                                break;
                            case 5: // dropdownlist
                                templateField.WebControlType = ControlType.DropDownList;
                                row["TemplateType"] = "dropdownlist";
                                break;
                            case 6: //checkbox
                                templateField.WebControlType = ControlType.CheckBox;
                                row["TemplateType"] = "CheckBox";
                                break;
                            case 7:   //uploadFile
                                break;
                            case 8:
                                //templateField.Width = 200;
                                templateField.WebControlType = ControlType.TextBrowse;
                                templateField.BrowsePage = dtField.Rows[i]["BrowsePage"].ToString();
                                templateField.BrowseType = dtField.Rows[i]["BrowseTypeName"].ToString();
                                templateField.cellIndex = i + 2;
                                row["TemplateType"] = "TextBrowse";
                                break;

                        }
                        dtColumnIndex.Rows.Add(row);
                        if (IsEdit == 0)
                        {
                            templateField.ReadOnly = true;
                            if (FieldHTMLTypeID == 2)
                            {
                            }
                        }
                        else
                        {
                            templateField.ReadOnly = false;
                        }

                        templateField.ControlStyle.CssClass = "MaskedEditFocusOn";
                        templateField.ShowTemplate();
                        GridView1.Columns.Add(templateField);
                    }
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                ArrayList selectLines = new ArrayList();
                ViewState["selectedLines"] = selectLines;
                dtValue = DbHelper.GetInstance().GetDetailFieldValue(FormID, RequestID, GroupID);
                ViewState["dtValue"] = dtValue;

                ViewState["PageSize"] = GeneralConfigs.GetConfig().PageSize;
                txtPageSize.Text = GeneralConfigs.GetConfig().PageSize.ToString();
                GridView1.PageSize = (int)ViewState["PageSize"];
                BindGridView();
                ((Panel)this.Parent).Height = new Unit( dtValue.Rows.Count* 20 + 100);
                btnAdd.ScriptContent = "setGroupPanelHeight('" + GroupID.ToString() + "','ADD')";
                btnDel.ScriptContent = "setGroupPanelHeight('" + GroupID.ToString() + "','DEL')";
            }
            else
            {  
            }
            
        }

        protected void AddDetailNewRow(object sender, EventArgs e)
        {
            CollectSelected();
            DataTable dtValue = (DataTable)ViewState["dtValue"];
            DataRow row = dtValue.NewRow();

            for (int i = 0; i < dtField.Rows.Count; i++)
            {
                string FieldID = dtField.Rows[i]["FieldID"].ToString();
                string FieldName = dtField.Rows[i]["FieldName"].ToString();
                string FieldLable = dtField.Rows[i]["FieldLabel"].ToString();
                string FieldDefaultValue = dtField.Rows[i]["DefaultValue"].ToString();
                int FieldHTMLTypeID = Convert.ToInt32(dtField.Rows[i]["HTMLTypeID"].ToString());
                int FieldTextLength = Convert.ToInt32(dtField.Rows[i]["TextLength"].ToString());
                int FieldTextHeight = Convert.ToInt32(dtField.Rows[i]["TextHeight"].ToString());
                int FieldValidType = Convert.ToInt32(dtField.Rows[i]["BasicValidType"].ToString());
                string ValidTimeTypeName = dtField.Rows[i]["ValidTimeTypeName"].ToString();
                int IsView = Convert.ToInt32(dtField.Rows[i]["IsView"].ToString());
                int IsEdit = Convert.ToInt32(dtField.Rows[i]["IsEdit"].ToString());
                int IsMandatory = Convert.ToInt32(dtField.Rows[i]["IsMandatory"].ToString());
                if (FieldDefaultValue != "")
                {
                    row[FieldName] = FieldDefaultValue;
                }
            }

            dtValue.Rows.Add(row);
            ViewState["dtValue"] = dtValue;

            DataTable dtColumnRule = DbHelper.GetInstance().GetFormDetailColumnRule(FormID, GroupID);
            for (int j = 0; j < dtColumnRule.Rows.Count; j++)
            {
                string TargetFieldID = dtColumnRule.Rows[j]["FieldIDTo"].ToString();
                string TargetFieldName = dtColumnRule.Rows[j]["FieldName"].ToString();
                string TargetFieldControlID = "field" + TargetFieldID;
                int TargetFieldHTMLType = Convert.ToInt32(dtColumnRule.Rows[j]["HTMLTypeID"].ToString());
                object computeValue = dtValue.Compute(dtColumnRule.Rows[j]["ruleDetail"].ToString(), "1=1");
                switch (TargetFieldHTMLType)
                {
                    case 1:     //Label
                        //System.Web.UI.WebControls.Label lblField = (System.Web.UI.WebControls.Label)((PlaceHolder)(this.Page.FindControl("placeHolder"))).FindControl(TargetFieldControlID);
                        //lblField.Text = Convert.ToString(computeValue);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Loading", "document.getElementById('" + TargetFieldControlID + "').innerText=" + computeValue.ToString(), true);
                        //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "Loading", "document.getElementById('" + TargetFieldControlID + "Previous').innerText='" + Convert.ToString(computeValue) + "';", true);
                        break;
                    case 2:     //Textbox
                        //GPRP.GPRPControls.TextBox txtField = (GPRP.GPRPControls.TextBox)((PlaceHolder)(this.Page.FindControl("placeHolder"))).FindControl(TargetFieldControlID);

                        //txtField.Text = Convert.ToString(computeValue); ;
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "Loading", "document.getElementById('" + TargetFieldControlID + "').value='" + Convert.ToString(computeValue) + "';", true);
                        //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "Loading", "document.getElementById('" + TargetFieldControlID + "Previous').value='" + Convert.ToString(computeValue) + "';", true);
                        break;
                    case 3:   //TextArea
                        //GPRP.GPRPControls.TextBox txtAreaField = (GPRP.GPRPControls.TextBox)((PlaceHolder)(this.Page.FindControl("placeHolder"))).FindControl(TargetFieldControlID);
                        //txtAreaField.Text = Convert.ToString(computeValue); ;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Loading", "document.getElementById('" + TargetFieldControlID + "').value=" + computeValue.ToString(), true);
                        //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "Loading", "document.getElementById('" + TargetFieldControlID + "Previous').value='" + Convert.ToString(computeValue) + "';", true);
                        break;
                    case 4:  //checkboxList
                        //GPRP.GPRPControls.CheckBoxList chkLstField = (GPRP.GPRPControls.CheckBoxList)((PlaceHolder)(this.Page.FindControl("placeHolder"))).FindControl(TargetFieldControlID);
                        //chkLstField.SelectedValue = Convert.ToString(computeValue); ;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Loading", "document.getElementById('" + TargetFieldControlID + "').value=" + computeValue.ToString(), true);
                        //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "Loading", "document.getElementById('" + TargetFieldControlID + "Previous').value='" + Convert.ToString(computeValue) + "';", true);
                        break;
                    case 5: // dropdownlist
                        //GPRP.GPRPControls.DropDownList ddlField = (GPRP.GPRPControls.DropDownList)((PlaceHolder)(this.Page.FindControl("placeHolder"))).FindControl(TargetFieldControlID);
                        //ddlField.SelectedValue = Convert.ToString(computeValue); ;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Loading", "document.getElementById('" + TargetFieldControlID + "').value=" + computeValue.ToString(), true);
                        //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "Loading", "document.getElementById('" + TargetFieldControlID + "Previous').value='" + Convert.ToString(computeValue) + "';", true);
                        break;
                    case 6: //checkbox
                        //System.Web.UI.WebControls.CheckBox chkField = (System.Web.UI.WebControls.CheckBox)((PlaceHolder)(this.Page.FindControl("placeHolder"))).FindControl(TargetFieldControlID);

                        break;
                    case 7:   //uploadFile
                        //System.Web.UI.WebControls.CheckBox upField = new System.Web.UI.WebControls.CheckBox();

                        break;
                    case 8:
                        //HiddenField hiddenField = (System.Web.UI.WebControls.HiddenField)((PlaceHolder)(this.Page.FindControl("placeHolder"))).FindControl(TargetFieldControlID);
                        //hiddenField.Value = Convert.ToString(computeValue); ;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Loading", "document.getElementById('" + TargetFieldControlID + "').value=" + computeValue.ToString(), true);
                        //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "Loading", "document.getElementById('" + TargetFieldControlID + "Previous').value='" + Convert.ToString(computeValue) + "';", true);
                        break;

                }

            }
            
            BindGridView();
           
            //
            //((AjaxControlToolkit.TabPanel)this.Parent.Parent).Height = new Unit(RowCount * 50 + 80);
        }

        protected void DeleteDetailRow(object sender, EventArgs e)
        {
            CollectSelected();
            DataTable dtValue = (DataTable)ViewState["dtValue"];
            ArrayList selectLines = (ArrayList)ViewState["selectedLines"];
            for (int i = selectLines.Count; i > 0; i--)
            {
                dtValue.Rows.RemoveAt(Convert.ToInt32(selectLines[i - 1]));
            }
            selectLines.Clear();
            ViewState["selectedLines"] = selectLines;
            ViewState["dtValue"] = dtValue;

            DataTable dtColumnRule = DbHelper.GetInstance().GetFormDetailColumnRule(FormID, GroupID);
            for (int j = 0; j < dtColumnRule.Rows.Count; j++)
            {
                string TargetFieldID = dtColumnRule.Rows[j]["FieldIDTo"].ToString();
                string TargetFieldName = dtColumnRule.Rows[j]["FieldName"].ToString();
                string TargetFieldControlID = "field" + TargetFieldID;
                int TargetFieldHTMLType = Convert.ToInt32(dtColumnRule.Rows[j]["HTMLTypeID"].ToString());
                object computeValue = dtValue.Compute(dtColumnRule.Rows[j]["ruleDetail"].ToString(), "1=1");
                switch (TargetFieldHTMLType)
                {
                    case 1:     //Label
                        //System.Web.UI.WebControls.Label lblField = (System.Web.UI.WebControls.Label)((PlaceHolder)(this.Page.FindControl("placeHolder"))).FindControl(TargetFieldControlID);
                        //lblField.Text = Convert.ToString(computeValue);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Loading", "document.getElementById('" + TargetFieldControlID + "').innerText=" + computeValue.ToString(), true);
                        //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "Loading", "document.getElementById('" + TargetFieldControlID + "Previous').innerText='" + Convert.ToString(computeValue) + "';", true);
                        break;
                    case 2:     //Textbox
                        //GPRP.GPRPControls.TextBox txtField = (GPRP.GPRPControls.TextBox)((PlaceHolder)(this.Page.FindControl("placeHolder"))).FindControl(TargetFieldControlID);

                        //txtField.Text = Convert.ToString(computeValue); ;
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "Loading", "document.getElementById('" + TargetFieldControlID + "').value='" + Convert.ToString(computeValue) + "';", true);
                        //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "Loading", "document.getElementById('" + TargetFieldControlID + "Previous').value='" + Convert.ToString(computeValue) + "';", true);
                        break;
                    case 3:   //TextArea
                        //GPRP.GPRPControls.TextBox txtAreaField = (GPRP.GPRPControls.TextBox)((PlaceHolder)(this.Page.FindControl("placeHolder"))).FindControl(TargetFieldControlID);
                        //txtAreaField.Text = Convert.ToString(computeValue); ;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Loading", "document.getElementById('" + TargetFieldControlID + "').value=" + computeValue.ToString(), true);
                        //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "Loading", "document.getElementById('" + TargetFieldControlID + "Previous').value='" + Convert.ToString(computeValue) + "';", true);
                        break;
                    case 4:  //checkboxList
                        //GPRP.GPRPControls.CheckBoxList chkLstField = (GPRP.GPRPControls.CheckBoxList)((PlaceHolder)(this.Page.FindControl("placeHolder"))).FindControl(TargetFieldControlID);
                        //chkLstField.SelectedValue = Convert.ToString(computeValue); ;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Loading", "document.getElementById('" + TargetFieldControlID + "').value=" + computeValue.ToString(), true);
                        //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "Loading", "document.getElementById('" + TargetFieldControlID + "Previous').value='" + Convert.ToString(computeValue) + "';", true);
                        break;
                    case 5: // dropdownlist
                        //GPRP.GPRPControls.DropDownList ddlField = (GPRP.GPRPControls.DropDownList)((PlaceHolder)(this.Page.FindControl("placeHolder"))).FindControl(TargetFieldControlID);
                        //ddlField.SelectedValue = Convert.ToString(computeValue); ;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Loading", "document.getElementById('" + TargetFieldControlID + "').value=" + computeValue.ToString(), true);
                        // ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "Loading", "document.getElementById('" + TargetFieldControlID + "Previous').value='" + Convert.ToString(computeValue) + "';", true);
                        break;
                    case 6: //checkbox
                        //System.Web.UI.WebControls.CheckBox chkField = (System.Web.UI.WebControls.CheckBox)((PlaceHolder)(this.Page.FindControl("placeHolder"))).FindControl(TargetFieldControlID);

                        break;
                    case 7:   //uploadFile
                        //System.Web.UI.WebControls.CheckBox upField = new System.Web.UI.WebControls.CheckBox();

                        break;
                    case 8:
                        //HiddenField hiddenField = (System.Web.UI.WebControls.HiddenField)((PlaceHolder)(this.Page.FindControl("placeHolder"))).FindControl(TargetFieldControlID);
                        //hiddenField.Value = Convert.ToString(computeValue); ;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Loading", "document.getElementById('" + TargetFieldControlID + "').value=" + computeValue.ToString(), true);
                        //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "Loading", "document.getElementById('" + TargetFieldControlID + "Previous').value='" + Convert.ToString(computeValue) + "';", true);
                        break;

                }
            }
            BindGridView();
            
        }

        public void BindGridView()
        {
            DataTable dtValue = (DataTable)ViewState["dtValue"];
            string a = ((Panel)(this.Parent)).ID;
            ((Panel)(this.Parent)).Height = new Unit(dtValue.Rows.Count * 150 + 100);
            if (dtValue.Rows.Count > 0)
                AspNetPager1.RecordCount = Convert.ToInt32(dtValue.Rows.Count);
            else
                AspNetPager1.RecordCount = 0;
            GridView1.DataSource = dtValue;
            GridView1.DataBind();
            //RowCount = GridView1.Rows.Count;
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string KeyCol = e.Row.DataItemIndex.ToString();
                CheckBox cb = (System.Web.UI.WebControls.CheckBox)e.Row.FindControl("Item") as CheckBox;
                ArrayList selectedLines = (ArrayList)ViewState["selectedLines"];
                if (selectedLines.Contains(KeyCol))
                {
                    cb.Checked = true;
                }


                for (int i = 0; i < dtField.Rows.Count; i++)
                {

                    string FieldID = dtField.Rows[i]["FieldID"].ToString();
                    string FieldName = dtField.Rows[i]["FieldName"].ToString();
                    string FieldLable = dtField.Rows[i]["FieldLabel"].ToString();
                    string FieldDefaultValue = dtField.Rows[i]["DefaultValue"].ToString();
                    int FieldHTMLTypeID = Convert.ToInt32(dtField.Rows[i]["HTMLTypeID"].ToString());
                    int FieldTextLength = Convert.ToInt32(dtField.Rows[i]["TextLength"].ToString());
                    int FieldTextHeight = Convert.ToInt32(dtField.Rows[i]["TextHeight"].ToString());
                    int FieldValidType = Convert.ToInt32(dtField.Rows[i]["BasicValidType"].ToString());
                    string ValidTimeTypeName = dtField.Rows[i]["ValidTimeTypeName"].ToString();
                    int IsView = Convert.ToInt32(dtField.Rows[i]["IsView"].ToString());
                    int IsEdit = Convert.ToInt32(dtField.Rows[i]["IsEdit"].ToString());
                    int IsMandatory = Convert.ToInt32(dtField.Rows[i]["IsMandatory"].ToString());
                    string FieldDataType = dtField.Rows[i]["FieldDBType"].ToString();
                    ////处理DropDownList,checkbox,TextBrowse的数据绑定
                    string FieldValue = Convert.ToString(DataBinder.Eval(e.Row.DataItem, FieldName));
                    switch (FieldHTMLTypeID)
                    {

                        case 5: // dropdownlist
                            CheckBox cbField = (System.Web.UI.WebControls.CheckBox)e.Row.Cells[i + 2].Controls[0];
                            if (FieldValue == "1")
                            {
                                cbField.Checked = true;
                            }
                            else
                            {
                                cbField.Checked = false;
                            }
                            break;
                        case 6: //checkbox
                            GPRP.GPRPControls.DropDownList ddlField = (GPRP.GPRPControls.DropDownList)e.Row.Cells[i + 2].Controls[0];
                            if (FieldValue != "")
                            {
                                ddlField.SelectedValue = FieldValue;
                            }
                        
                            break;
                        case 8:

                            GPRP.GPRPControls.TextBox txtField = (GPRP.GPRPControls.TextBox)e.Row.Cells[i + 2].Controls[0];
                            System.Web.UI.WebControls.HiddenField txtValueField = (System.Web.UI.WebControls.HiddenField)e.Row.Cells[i + 2].Controls[1];
                            txtValueField.Value = FieldValue;

                            string BrowseTypeName = dtField.Rows[i]["BrowseTypeName"].ToString();
                            string BrowseValueSql = dtField.Rows[i]["BrowseValueSql"].ToString();
                            string BrowseSqlParam = dtField.Rows[i]["BrowseSqlParam"].ToString();
                            if (FieldValue != "")
                            {


                                txtField.Text = DbHelper.GetInstance().GetBrowseFieldText(BrowseValueSql, BrowseSqlParam, FieldValue);
                            }
                               
                            


                            break;
                    
                    }
                }



            }
        }

        //Show Header/Footer of Gridview with Empty Data Source 
        public void BuildNoRecords(GridView gridView, DataTable ds)
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

        #region aspnetPage 分页代码

        protected void txtPageSize_TextChanged(object sender, EventArgs e)
        {
            if (txtPageSize.Text == "" || Convert.ToInt32(txtPageSize.Text) == 0)
            {
                ViewState["PageSize"] = GeneralConfigs.GetConfig().PageSize;//每页显示的默认值
            }
            else
            {
                ViewState["PageSize"] = Convert.ToInt32(txtPageSize.Text);
            }
            AspNetPager1.PageSize = Convert.ToInt32(ViewState["PageSize"]);
            GridView1.PageSize = (int)ViewState["PageSize"];
            CollectSelected();
            BindGridView();
            //RowCount = GridView1.Rows.Count;;
        }
        //此类无须更改
        protected void AspNetPager1_PageChanged(object src, EventArgs e)
        {
            CollectSelected();
            GridView1.PageIndex = AspNetPager1.CurrentPageIndex - 1;
            BindGridView();
            //RowCount = GridView1.Rows.Count;;
        }
        protected void AspNetPager1_PageChanging(object src, EventArgs e)
        {
        }
        #endregion

        public void CollectSelected()
        {
            ArrayList selectedLines = (ArrayList)ViewState["selectedLines"];
            dtValue = (DataTable)ViewState["dtValue"];
            for (int i = 0; i < this.GridView1.Rows.Count; i++)
            {

                string KeyCol = GridView1.Rows[i].DataItemIndex.ToString();
                CheckBox cb = this.GridView1.Rows[i].FindControl("Item") as CheckBox;
                if (selectedLines.Contains(KeyCol) && !cb.Checked)
                    selectedLines.Remove(KeyCol);
                if (!selectedLines.Contains(KeyCol) && cb.Checked)
                    selectedLines.Add(KeyCol);

                DataRow row = dtValue.Rows[Convert.ToInt32(KeyCol)];
                for (int j = 0; j < dtField.Rows.Count; j++)
                {
                    string FieldID = dtField.Rows[j]["FieldID"].ToString();
                    string FieldName = dtField.Rows[j]["FieldName"].ToString();
                    int FieldHTMLTypeID = Convert.ToInt32(dtField.Rows[j]["HTMLTypeID"].ToString());
                    int IsView = Convert.ToInt32(dtField.Rows[j]["IsView"].ToString());
                    int IsEdit = Convert.ToInt32(dtField.Rows[j]["IsEdit"].ToString());
                    int IsMandatory = Convert.ToInt32(dtField.Rows[j]["IsMandatory"].ToString());
                    if (IsView == 1 && IsGroupEdit == 1&& RightType == 2)
                    {
                        switch (FieldHTMLTypeID)
                        {
                            case 1:     //Label
                                System.Web.UI.WebControls.Literal lblControl = (System.Web.UI.WebControls.Literal)GridView1.Rows[i].Cells[2 + j].Controls[0];
                                row[FieldName] = lblControl.Text;
                                break;
                            case 2:     //Textbox
                                if (IsEdit == 1 && RightType == 2)
                                {
                                    GPRP.GPRPControls.TextBox txtField = (GPRP.GPRPControls.TextBox)GridView1.Rows[i].Cells[2 + j].Controls[0];
                                    row[FieldName] = txtField.Text;
                                }
                                else
                                {
                                    System.Web.UI.WebControls.HiddenField hiddenField = (System.Web.UI.WebControls.HiddenField)GridView1.Rows[i].Cells[2 + j].Controls[1];
                                    row[FieldName] = hiddenField.Value;
                                }
                                break;
                            case 3:   //TextArea
                                GPRP.GPRPControls.TextBox txtAreaField = (GPRP.GPRPControls.TextBox)GridView1.Rows[i].Cells[2 + j].Controls[0];
                                row[FieldName] = txtAreaField.Text;
                                break;
                            case 4:  //checkboxList
                                GPRP.GPRPControls.CheckBoxList chkListField = (GPRP.GPRPControls.CheckBoxList)GridView1.Rows[i].Cells[2 + j].Controls[0];
                                break;
                            case 5: // dropdownlist
                                GPRP.GPRPControls.DropDownList ddlField = (GPRP.GPRPControls.DropDownList)GridView1.Rows[i].Cells[2 + j].Controls[0];
                                row[FieldName] = ddlField.SelectedValue;
                                break;
                            case 6: //checkbox
                                System.Web.UI.WebControls.CheckBox chField = (System.Web.UI.WebControls.CheckBox)GridView1.Rows[i].Cells[2 + j].Controls[0];
                                row[FieldName] = chField.Checked ? "1" : "0";
                                break;
                            case 7:   //uploadFile
                                break;
                            case 8:
                                System.Web.UI.WebControls.HiddenField hiddenFieldValue = (System.Web.UI.WebControls.HiddenField)GridView1.Rows[i].Cells[2 + j].Controls[1];
                                row[FieldName] = hiddenFieldValue.Value;
                                break;

                        }
                    }
                }
            }
            ViewState["dtValue"] = dtValue;
        }
    }
}