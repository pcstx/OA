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
    public partial class GG20 : BasePage
    {
        private static string strOperationState;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                txtDataSetName.AddAttributes("readonly", "true");

                ViewState["selectedLines"] = new ArrayList();
                AspNetPager1.PageSize = config.PageSize;
                BindGridView();
                
                DataTable dtHTMLType = DbHelper.GetInstance().GetDBRecords("HTMLTypeID,HTMLTypeName,HTMLTypeDesc", "Workflow_HTMLType", "Useflag='1'", "HTMLTypeID");
                ddlHTMLType.AddTableData(dtHTMLType, 0, 2, true, "Select");
                DataTable dtDataType = DbHelper.GetInstance().GetDBRecords("*", "Workflow_FieldDataType", "Useflag='1'", "DataTypeID");
                ddlDataType.AddTableData(dtDataType, 0, 1, true, "Select");
                DataTable dtBrowseType = DbHelper.GetInstance().GetDBRecords("*", "Workflow_BrowseType", "Useflag='1'", "BrowseTypeID");
                ddlBrowseType.AddTableData(dtBrowseType, 0, 2, true, "Select");
                txtDisplayOrder.Text = "990";
            }
        }

        //设置每页显示记录数，无须更改；如果要更改默认第页记录数，到config/geneal.config中更改 PageSize
        protected void txtPageSize_TextChanged(object sender, EventArgs e)
        {
            if (txtPageSize.Text == "" || Convert.ToInt32(txtPageSize.Text) == 0)
            {
                ViewState["PageSize"] = config.PageSize;//每页显示的默认值
            }
            else
            {
                ViewState["PageSize"] = Convert.ToInt32(txtPageSize.Text);
            }
            AspNetPager1.PageSize = Convert.ToInt32(ViewState["PageSize"]);
            CollectSelected();
            BindGridView();
        }

        #region gridview 绑定
        //此类需要更改，主要是更改获取数据源的方法
        private void BindGridView()
        {
            string WhereCondition = "1=1";
            if (txtQFieldName.Text != string.Empty)
            {
                WhereCondition += "and FieldName like '%" + txtQFieldName.Text + "%'";
            }
            if (txtQFieldDesc.Text != string.Empty)
            {
                WhereCondition += "and FieldDesc like '%" + txtQFieldDesc.Text + "%'";
            }
            if (rblFieldType.SelectedValue != string.Empty)
            {
                WhereCondition += "and FieldTypeID = " + rblFieldType.SelectedValue;
            }
            DataTable dt = DbHelper.GetInstance().GetDBRecords("*", "Workflow_FieldDict", WhereCondition, "FieldName", AspNetPager1.PageSize, AspNetPager1.CurrentPageIndex);
            if (dt.Rows.Count > 0)
                AspNetPager1.RecordCount = Convert.ToInt32(dt.Rows[0]["RecordCount"]);
            else
                AspNetPager1.RecordCount = 0;
            ExtendDatatable(dt);
            GridView1.DataSource = dt;
            GridView1.DataBind();
            BuildNoRecords(GridView1, dt);
        }
        private void ExtendDatatable(DataTable dt)
        {
            dt.Columns.Add(new DataColumn("HTMLTypeN",typeof(System.String)));
            for(int i=0; i<dt.Rows.Count;i++)
            {
                string FieldID = dt.Rows[i]["FieldID"].ToString();
                Workflow_FieldDictEntity _Workflow_FieldDictEntity = DbHelper.GetInstance().GetWorkflow_FieldDictEntityByKeyCol(FieldID);
                Workflow_HTMLTypeEntity _Workflow_HTMLTypeEntity = DbHelper.GetInstance().GetWorkflow_HTMLTypeEntityByKeyCol(_Workflow_FieldDictEntity.HTMLTypeID.ToString());
                string HTMLTypeN = _Workflow_HTMLTypeEntity.HTMLTypeDesc;
                if (_Workflow_FieldDictEntity.HTMLTypeID == 8
                    && _Workflow_FieldDictEntity.BrowseType > 0)
                {
                    Workflow_BrowseTypeEntity _Workflow_BrowseTypeEntity = DbHelper.GetInstance().GetWorkflow_BrowseTypeEntityByKeyCol(_Workflow_FieldDictEntity.BrowseType.ToString());
                    HTMLTypeN = HTMLTypeN + "-" + _Workflow_BrowseTypeEntity.BrowseTypeDesc;
                }
                dt.Rows[i]["HTMLTypeN"] = HTMLTypeN;
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
        #endregion

        #region aspnetPage 分页代码
        //此类无须更改
        protected void AspNetPager1_PageChanged(object src, EventArgs e)
        {
            CollectSelected();
            BindGridView();
        }
        protected void AspNetPager1_PageChanging(object src, EventArgs e)
        {
        }
        #endregion

        #region gridView 事件 --类型
        protected void GridView1_RowCreated(Object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
            }
        }
        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = -1;
            if (e.CommandName == "select")
            {
                programmaticAddModalPopup.Show();
                strOperationState = "Update";
                index = Convert.ToInt32(e.CommandArgument);   //获取行号
                string FieldID = GridView1.DataKeys[index][0].ToString().Trim();
                Workflow_FieldDictEntity _Workflow_FieldDictEntity = DbHelper.GetInstance().GetWorkflow_FieldDictEntityByKeyCol(FieldID);
                txtFieldID.Value = _Workflow_FieldDictEntity.FieldID.ToString();
                txtFieldName.Text = _Workflow_FieldDictEntity.FieldName;
                txtFieldDesc.Text = _Workflow_FieldDictEntity.FieldDesc;
                ddlHTMLType.SelectedValue = _Workflow_FieldDictEntity.HTMLTypeID.ToString();
                ddlDataType.SelectedValue = _Workflow_FieldDictEntity.DataTypeID.ToString();
                txtTextLength.Text = _Workflow_FieldDictEntity.TextLength.ToString();
                ddlDateformat.SelectedValue = _Workflow_FieldDictEntity.Dateformat;
                txtTextHeight.Text = _Workflow_FieldDictEntity.TextHeight.ToString();
                chkIsHTML.Checked = _Workflow_FieldDictEntity.IsHTML.Equals("1");
                ddlBrowseType.SelectedValue = _Workflow_FieldDictEntity.BrowseType.ToString();
                lblDictType.Text = rblFieldType.SelectedValue == "1" ? "主字段" : "明细字段";
                rblStaticOrDynamic.SelectedValue = _Workflow_FieldDictEntity.IsDynamic;
                txtDataSetID.Value = _Workflow_FieldDictEntity.DataSetID.ToString();
                ReDisplayDataSet();
                ddlValueColumn.SelectedValue = _Workflow_FieldDictEntity.ValueColumn;
                ddlTextColumn.SelectedValue = _Workflow_FieldDictEntity.TextColumn;
                divSelectInfoDetailStatic.Visible = rblStaticOrDynamic.SelectedValue == "0";
                divSelectInfoDetailDynamic.Visible = rblStaticOrDynamic.SelectedValue == "1";
                DataTable dtSelectList = DbHelper.GetInstance().GetDBRecords("*", "Workflow_FieldDictSelect", "FieldID=" + FieldID, "DisplayOrder");
                SmartGridView1.DataSource = dtSelectList;
                SmartGridView1.DataBind();
                ViewState["dtSelectList"] = dtSelectList;
                ReShowPageContent();


                DataTable dtUsedField = DbHelper.GetInstance().GetDBRecords("*", "Workflow_FormField", "FieldID=" + FieldID, "FieldID");
                if (dtUsedField.Rows.Count > 0)
                {
                    txtFieldName.AddAttributes("readonly", "true");
                    txtFieldDesc.AddAttributes("readonly", "true");
                    ddlHTMLType.Attributes.Add("disabled", "true");
                    ddlBrowseType.Attributes.Add("disabled", "true");
                    ddlDataType.Attributes.Add("disabled", "true");
                    rblStaticOrDynamic.Attributes.Add("disabled", "true");
                }
            }
            BindGridView();
        }
        //此类要进行dorpdownlist/chk控件的转换
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string KeyCol = ((DataRowView)e.Row.DataItem).Row["FieldID"].ToString();
                if (KeyCol != string.Empty)
                {
                    CheckBox cb = (System.Web.UI.WebControls.CheckBox)e.Row.FindControl("Item") as CheckBox;
                    ArrayList selectedLines = (ArrayList)ViewState["selectedLines"];
                    if (selectedLines.Contains(KeyCol))
                    {
                        cb.Checked = true;
                    }
                }
            }
        }
        #endregion

        protected void btnSearchRecord_Click(object sender, EventArgs e)
        {
            AspNetPager1.CurrentPageIndex = 1;
            ViewState["selectedLines"] = new ArrayList();
            BindGridView();
            System.Web.UI.ScriptManager.RegisterStartupScript(btnQuery, this.GetType(), "ButtonHideScript", strButtonHideScript, false);
            programmaticQueryModalPopup.Hide();
        }

        //此类一般不需要更改,因为保存的工作全部放在下面的SaveData中
        protected void hideModalPopupViaServer_Click(object sender, EventArgs e)
        {
            GPRP.GPRPControls.Button btn = (GPRP.GPRPControls.Button)sender;
            string sResult = "-1";
            if (btn.ID == "btnSubmitAndClose" || btn.ID == "btnSubmit")
            {
                //保存
                sResult = SaveData();
                if (sResult == "-1")
                {
                    lblMsg.Text = ResourceManager.GetString("Operation_RECORD");
                }
                else
                {
                    //refresh gridview
                    if (btn.ID == "btnSubmitAndClose")
                    {
                        programmaticAddModalPopup.Hide();
                        txtDisplayOrder.Text = "990";
                    }
                }
                CollectSelected();
                BindGridView();
            }
            System.Web.UI.ScriptManager.RegisterStartupScript(btn, this.GetType(), "ButtonHideScript", strButtonHideScript, false);
        }

        private string SaveData()
        {
            Workflow_FieldDictEntity _Workflow_FieldDictEntity = new Workflow_FieldDictEntity();
            _Workflow_FieldDictEntity.FieldID = Convert.ToInt32(txtFieldID.Value != string.Empty ? txtFieldID.Value : "0");
            _Workflow_FieldDictEntity.FieldName = txtFieldName.Text;
            _Workflow_FieldDictEntity.FieldDesc = txtFieldDesc.Text;
            _Workflow_FieldDictEntity.DataTypeID = Convert.ToInt32(ddlDataType.SelectedValue);
            
            if (ddlHTMLType.SelectedValue == "1")//label
            {
                _Workflow_FieldDictEntity.FieldDBType = string.Format("{1}({0})", 200, SqlDbType.VarChar);
                _Workflow_FieldDictEntity.SqlDbType = SqlDbType.VarChar.ToString();
                _Workflow_FieldDictEntity.SqlDbLength = 200;
            }
            else if (ddlHTMLType.SelectedValue == "2")//textbox
            {
                switch (ddlDataType.SelectedValue)
                {
                    case "1":
                        _Workflow_FieldDictEntity.FieldDBType = string.Format("{1}({0})", txtTextLength.Text, SqlDbType.VarChar);
                        _Workflow_FieldDictEntity.SqlDbType = SqlDbType.VarChar.ToString();
                        _Workflow_FieldDictEntity.SqlDbLength = Convert.ToInt32(txtTextLength.Text);
                        break;
                    case "2":
                        _Workflow_FieldDictEntity.FieldDBType = SqlDbType.BigInt.ToString();
                        _Workflow_FieldDictEntity.SqlDbType = SqlDbType.BigInt.ToString();
                        _Workflow_FieldDictEntity.SqlDbLength = 4;
                        break;
                    case "3":
                        _Workflow_FieldDictEntity.FieldDBType = SqlDbType.Float.ToString();
                        _Workflow_FieldDictEntity.SqlDbType = SqlDbType.Float.ToString();
                        _Workflow_FieldDictEntity.SqlDbLength = 8;
                        break;
                    case "4":
                        _Workflow_FieldDictEntity.FieldDBType = SqlDbType.DateTime.ToString();
                        _Workflow_FieldDictEntity.SqlDbType = SqlDbType.DateTime.ToString();
                        _Workflow_FieldDictEntity.SqlDbLength = 16;
                        break;
                    case "5":
                        _Workflow_FieldDictEntity.FieldDBType = string.Format("{1}({0})", 8, SqlDbType.VarChar);
                        _Workflow_FieldDictEntity.SqlDbType = SqlDbType.VarChar.ToString();
                        _Workflow_FieldDictEntity.SqlDbLength = 8;
                        break;
                }
            }
            else if (ddlHTMLType.SelectedValue == "3")//textarea
            {
                _Workflow_FieldDictEntity.FieldDBType = string.Format("{1}({0})", 1000, SqlDbType.VarChar);
                _Workflow_FieldDictEntity.SqlDbType = SqlDbType.VarChar.ToString();
                _Workflow_FieldDictEntity.SqlDbLength = 1000;
            }
            else if (ddlHTMLType.SelectedValue == "4" //checkboxlist
                || ddlHTMLType.SelectedValue == "5")//dropdownlist
            {
                _Workflow_FieldDictEntity.FieldDBType = string.Format("{1}({0})", 600, SqlDbType.VarChar);
                _Workflow_FieldDictEntity.SqlDbType = SqlDbType.VarChar.ToString();
                _Workflow_FieldDictEntity.SqlDbLength = 600;
            }
            else if (ddlHTMLType.SelectedValue == "8")//browsebutton
            {
                _Workflow_FieldDictEntity.FieldDBType = string.Format("{1}({0})", 600, SqlDbType.VarChar);
                _Workflow_FieldDictEntity.SqlDbType = SqlDbType.VarChar.ToString();
                _Workflow_FieldDictEntity.SqlDbLength = 600;
            }
            else if (ddlHTMLType.SelectedValue == "6" //checkbox
                || ddlHTMLType.SelectedValue == "7") //uploadfile
            {
                _Workflow_FieldDictEntity.FieldDBType = string.Format("{1}({0})", 50, SqlDbType.VarChar);
                _Workflow_FieldDictEntity.SqlDbType = SqlDbType.VarChar.ToString();
                _Workflow_FieldDictEntity.SqlDbLength = 50;
            }
            else
            {
                _Workflow_FieldDictEntity.FieldDBType = string.Format("{1}({0})", 50, SqlDbType.VarChar);
                _Workflow_FieldDictEntity.SqlDbType = SqlDbType.VarChar.ToString();
                _Workflow_FieldDictEntity.SqlDbLength = 50;
            }
            _Workflow_FieldDictEntity.HTMLTypeID = Convert.ToInt32(ddlHTMLType.Text);
            _Workflow_FieldDictEntity.FieldTypeID = Convert.ToInt32(rblFieldType.SelectedValue);//主字段/明细字段
            _Workflow_FieldDictEntity.ValidateType = string.Empty;
            _Workflow_FieldDictEntity.TextLength = Convert.ToInt32(txtTextLength.Text != string.Empty ? txtTextLength.Text : "0");
            _Workflow_FieldDictEntity.Dateformat = ddlDateformat.SelectedValue;
            _Workflow_FieldDictEntity.TextHeight = Convert.ToInt32(txtTextHeight.Text != string.Empty ? txtTextHeight.Text : "0");
            _Workflow_FieldDictEntity.IsHTML = chkIsHTML.Checked ? "1" : "0";
            _Workflow_FieldDictEntity.BrowseType = Convert.ToInt32(ddlBrowseType.SelectedValue);
            _Workflow_FieldDictEntity.IsDynamic = rblStaticOrDynamic.SelectedValue;
            _Workflow_FieldDictEntity.DataSetID = txtDataSetID.Value != string.Empty ? Convert.ToInt32(txtDataSetID.Value) : 0;
            _Workflow_FieldDictEntity.ValueColumn = ddlValueColumn.SelectedValue;
            _Workflow_FieldDictEntity.TextColumn = ddlTextColumn.SelectedValue;
            _Workflow_FieldDictEntity.dtSelectList = (DataTable)ViewState["dtSelectList"];
            string sResult = "-1";
            if (strOperationState == "Add")
                sResult = DbHelper.GetInstance().AddWorkflow_FieldDict(_Workflow_FieldDictEntity);
            else if (strOperationState == "Update")
                sResult = DbHelper.GetInstance().UpdateWorkflow_FieldDict(_Workflow_FieldDictEntity);
            return sResult;
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            strOperationState = "Add";
            ReShowPageContent();
            lblDictType.Text = rblFieldType.SelectedValue == "1" ? "主字段" : "明细字段";
            DataTable dtSelectList = DbHelper.GetInstance().GetDBRecords("*", "Workflow_FieldDictSelect", "FieldID=0", "DisplayOrder");
            ViewState["dtSelectList"] = dtSelectList;
            SmartGridView1.DataSource = dtSelectList;
            programmaticAddModalPopup.Show();
            CollectSelected();
            BindGridView();
        }


        protected void btnDel_Click(object sender, EventArgs e)
        {
            CollectSelected();
            ArrayList selectedLines = (ArrayList)ViewState["selectedLines"];
            for (int i = 0; i < selectedLines.Count; i++)
            {
                //DbHelper.GetInstance().DeleteProblemType(selectedLines[i].ToString());
            }
            BindGridView();
            ViewState["selectedLines"] = new ArrayList();
        }

        protected void btn_DeleteRows(object sender, EventArgs e)
        {
            DataTable dtSelectList = (DataTable)ViewState["dtSelectList"];
            for (int i = SmartGridView1.Rows.Count - 1; i >= 0; i--)
            {
                CheckBox cb = SmartGridView1.Rows[i].FindControl("Item") as CheckBox;
                if (cb.Checked)
                    dtSelectList.Rows.RemoveAt(i);
            }
            for (int i = 0; i < dtSelectList.Rows.Count; i++)
            {
                dtSelectList.Rows[i]["DisplayOrder"] = (i + 1) * 10;
            }
            ViewState["dtSelectList"] = dtSelectList;
            SmartGridView1.DataSource = dtSelectList;
            SmartGridView1.DataBind();
            BindGridView();
        }

        protected void btn_AddRow(object sender, EventArgs e)
        {
            DataTable dtSelectList = (DataTable)ViewState["dtSelectList"];
            DataRow drSelect = dtSelectList.NewRow();
            drSelect["SelectNo"] = 0;
            drSelect["FieldID"] = 0;
            drSelect["LabelWord"] = txtLabelWord.Text;
            drSelect["DisplayOrder"] = txtDisplayOrder.Text;
            dtSelectList.Rows.Add(drSelect);
            DataView dvSelectList = new DataView(dtSelectList);
            dvSelectList.Sort = "DisplayOrder";
            dtSelectList = dvSelectList.ToTable();
            for (int i = 0; i < dtSelectList.Rows.Count; i++)
            {
                dtSelectList.Rows[i]["DisplayOrder"] = (i + 1) * 10;
            }
            ViewState["dtSelectList"] = dtSelectList;
            SmartGridView1.DataSource = dtSelectList;
            SmartGridView1.DataBind();
            BindGridView();
        }

        protected void txtDataSet_TextChanged(object sender, EventArgs e)
        {
            ReDisplayDataSet();
        }

        private void ReDisplayDataSet()
        {
            try
            {
                string DataSetID = txtDataSetID.Value;
                Workflow_DataSetEntity _DataSetEntity = DbHelper.GetInstance().GetWorkflow_DataSetEntityByKeyCol(DataSetID);
                txtDataSetName.Text = _DataSetEntity.DataSetName;
                string[] ColumnList = _DataSetEntity.ReturnColumns.Split(new char[] { ',' });
                ddlValueColumn.Items.Clear();
                ddlTextColumn.Items.Clear();
                ddlValueColumn.Items.Add(new ListItem("--请选择--", ""));
                ddlTextColumn.Items.Add(new ListItem("--请选择--", ""));
                for (int i = 0; i < ColumnList.Length; i++)
                {
                    ddlValueColumn.Items.Add(ColumnList[i]);
                    ddlTextColumn.Items.Add(ColumnList[i]);
                }
            }
            catch
            {
            }
        }

        protected void ddlDataType_SelectedIndexChanged(object sender, EventArgs e)
        {
            ReShowPageContent();
            BindGridView();
        }

        protected void rblFieldType_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGridView();
        }

        protected void rblStaticOrDynamic_SelectedIndexChanged(object sender, EventArgs e)
        {
            divSelectInfoDetailStatic.Visible = rblStaticOrDynamic.SelectedValue == "0";
            divSelectInfoDetailDynamic.Visible = rblStaticOrDynamic.SelectedValue == "1";
        }

        private void ReShowPageContent()
        {
            divDataType.Visible = false;
            divTextLength.Visible = false;
            divDateformat.Visible = false;
            divTextHeight.Visible = false;
            divIsHTML.Visible = false;
            divBrowseType.Visible = false;
            divSelectInfo.Visible = false;
            divSelectInfoDetail.Visible = false;

            if (ddlHTMLType.SelectedValue == "2")//textbox
            {
                divDataType.Visible = true;
                divTextLength.Visible = ddlDataType.SelectedValue == "1";
                divDateformat.Visible = ddlDataType.SelectedValue == "4" || ddlDataType.SelectedValue == "5";

                string DateType = ddlDataType.SelectedValue == "4" ? "1" : "2";
                DataTable dtDateFormat = DbHelper.GetInstance().GetDBRecords("DateFormatID,DateFormatText", "Workflow_DateFormat", "Useflag='1' and DateType=" + DateType, "DisplayOrder");
                ddlDateformat.AddTableData(dtDateFormat, 0, 1, true, "Null");
            }
            else if (ddlHTMLType.SelectedValue == "3")//textarea
            {
                divTextHeight.Visible = true;
                divIsHTML.Visible = true;
            }
            else if (ddlHTMLType.SelectedValue == "4" //checkboxlist
                || ddlHTMLType.SelectedValue == "5")//dropdownlist
            {
                divSelectInfo.Visible = true;
                divSelectInfoDetail.Visible = true;
            }
            else if (ddlHTMLType.SelectedValue == "8")//browsebutton
            {
                divBrowseType.Visible = true;
            }
            else if (ddlHTMLType.SelectedValue == "6" //checkbox
                || ddlHTMLType.SelectedValue == "7") //uploadfile
            {

            }
            divSelectInfoDetailStatic.Visible = rblStaticOrDynamic.SelectedValue == "0";
            divSelectInfoDetailDynamic.Visible = rblStaticOrDynamic.SelectedValue == "1";
        }

        private void CollectSelected()
        {
            ArrayList selectedLines = (ArrayList)ViewState["selectedLines"];
            for (int i = 0; i < this.GridView1.Rows.Count; i++)
            {
                string KeyCol = GridView1.DataKeys[i][0].ToString().Trim();
                CheckBox cb = this.GridView1.Rows[i].FindControl("Item") as CheckBox;
                if (selectedLines.Contains(KeyCol) && !cb.Checked)
                    selectedLines.Remove(KeyCol);
                if (!selectedLines.Contains(KeyCol) && cb.Checked)
                    selectedLines.Add(KeyCol);
            }
        }

        [WebMethod]
        public static string SetAddViewState()
        {
            strOperationState = "Add";
            return "";
        }
    }
}
