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
    public partial class GG500203 : BasePage
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            DataTable dtComputeOperation = DbHelper.GetInstance().GetDBRecords("a.ComputeID,a.ComputeSymbol", "Workflow_ComputeSymbol a,Workflow_ComputeType b", "a.ComputeTypeID=b.ComputeTypeID and b.Useflag=0 and a.Useflag=1", "a.ComputeTypeID,a.DisplayOrder");
            for (int i = 0; i < dtComputeOperation.Rows.Count; i++)
            {
                System.Web.UI.WebControls.Button btn = new System.Web.UI.WebControls.Button();
                btn.ID = "ComputeOperation" + dtComputeOperation.Rows[i]["ComputeID"].ToString();
                btn.Text = dtComputeOperation.Rows[i]["ComputeSymbol"].ToString();
                btn.Width = 30;
                btn.Click += new EventHandler(this.btnComputeOperation_Click);
                phOperationButtonList.Controls.Add(btn);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                txtDataSetName.AddAttributes("readonly", "true");
                txtDataSourceName.AddAttributes("readonly", "true");
                divOperationCulculate.Visible = false;
                divOperationBrowse.Visible = false;
                DataTable dtOperationType = DbHelper.GetInstance().GetDBRecords("TypeID, TypeValue", "Workflow_AddInOperationType", "Useflag=1", "DisplayOrder");
                rblAddInOperationType.DataSource = dtOperationType;
                rblAddInOperationType.DataValueField = "TypeID";
                rblAddInOperationType.DataTextField = "TypeValue";
                rblAddInOperationType.DataBind();
                rblAddInOperationType.SelectedIndex = 0;

                DataTable dtMailField = DbHelper.GetInstance().GetDBRecords("c.FieldID,c.FieldLabel,d.FieldName", "Workflow_FlowNode a,Workflow_Base b,Workflow_FormField c,Workflow_FieldDict d", "a.WorkflowID=b.WorkflowID and b.FormID=c.FormID and c.FieldID=d.FieldID and c.IsDetail=0 and a.NodeID=" + DNTRequest.GetString("id"), "c.DisplayOrder");
                ddlFieldIDTo.AddTableData(dtMailField, 0, 1, true, "Select");

                lbFieldList.DataSource = dtMailField;
                lbFieldList.DataValueField = "FieldID";
                lbFieldList.DataTextField = "FieldLabel";
                lbFieldList.DataBind();

                DataTable dtComputeType = DbHelper.GetInstance().GetDBRecords("ComputeTypeID, ComputeTypeName", "Workflow_ComputeType", "Useflag=1", "DisplayOrder");
                lbFunctionType.DataSource = dtComputeType;
                lbFunctionType.DataValueField = "ComputeTypeID";
                lbFunctionType.DataTextField = "ComputeTypeName";
                lbFunctionType.DataBind();

                ReDisplayFunctionList();

                DataTable dtComputeRouteOperation = DbHelper.GetInstance().GetDBRecords("ComputeType,RouteValue,RouteOrder", "Workflow_ComputeRouteOperation", "RouteID=0", "RouteOrder");
                ViewState["dtComputeRouteOperation"] = dtComputeRouteOperation;
                BindGridView();
            }
        }

        #region gridview 绑定

        //此类需要更改，主要是更改获取数据源的方法
        private void BindGridView()
        {
            ArrayList arlParams = new ArrayList();
            arlParams.Add("");
            arlParams.Add(DNTRequest.GetString("id"));
            arlParams.Add(DNTRequest.GetString("t"));
            DataTable dt = DbHelper.GetInstance().sp_GetNodeAddInOperation_Type0(arlParams);
            ExtendDatatable(dt);
            GridView1.DataSource = dt;
            GridView1.DataBind();
            BuildNoRecords(GridView1, dt);
        }

        private void ExtendDatatable(DataTable dt)
        {
            dt.Columns.Add(new DataColumn("CaculateValueN", typeof(System.String)));
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dt.Rows[i]["CaculateValueN"] = GetRuleDetail(dt.Rows[i])[1];
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

        #region gridView 事件 --类型
        protected void GridView1_RowCreated(Object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
            }
        }
        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "select")
            {
                //int index = Convert.ToInt32(e.CommandArgument);   //获取行号
                //string RuleID = GridView1.DataKeys[index][0].ToString().Trim();
                //DataTable dtComputeRouteOperation = DbHelper.GetInstance().GetDBRecords("ComputeType,RouteValue,RouteOrder", "Workflow_ComputeRouteOperation", "RouteID=" + RuleID, "RouteOrder");
                //ViewState["dtComputeRouteOperation"] = dtComputeRouteOperation;
                //string[] RuleDetail = GetRuleDetail();
                //lblExpression.Text = RuleDetail[1];
            }
            BindGridView();
        }
        //此类要进行dorpdownlist/chk控件的转换

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string CaculateType = DataBinder.Eval(e.Row.DataItem, "CaculateType").ToString();
                LinkButton btnSelect = (LinkButton)e.Row.FindControl("btnSelect");
                btnSelect.Visible = CaculateType == "3";
                LinkButton btnSelectDataSource = (LinkButton)e.Row.FindControl("btnSelectDataSource");
                btnSelectDataSource.Visible = CaculateType == "4";
            }
        }
        #endregion


        protected void btnUndo_Click(object sender, EventArgs e)
        {
            DataTable dtComputeRouteOperation = (DataTable)ViewState["dtComputeRouteOperation"];
            if (dtComputeRouteOperation.Rows.Count >= 1)
            {
                dtComputeRouteOperation.Rows.RemoveAt(dtComputeRouteOperation.Rows.Count - 1);
            }
            string[] RuleDetail = GetRuleDetail();
            lblExpression.Text = RuleDetail[1];

            BindGridView();
            System.Web.UI.ScriptManager.RegisterStartupScript(btnUndo, this.GetType(), "ButtonHideScript", strButtonHideScript, false);
        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Workflow_FieldDictEntity _FieldDictEntity = DbHelper.GetInstance().GetWorkflow_FieldDictEntityByKeyCol(ddlFieldIDTo.SelectedValue);
            string[] RuleDetail = GetRuleDetail();
            if (_FieldDictEntity.HTMLTypeID != 8 && rblAddInOperationType.SelectedValue == "1")
            {
                try
                {
                    DbHelper.GetInstance().ExecSqlText(string.Format(" select {0} from Workflow_Form where RequestID=0", RuleDetail[0]));
                }
                catch
                {
                    BindGridView();
                    ScriptManager.RegisterStartupScript(btnAdd, this.GetType(), "add", "alert('赋值表达式不正确,无法添加');", true);
                    return;
                }
            }

            DataTable dtComputeRouteOperation = (DataTable)ViewState["dtComputeRouteOperation"];
            DataTable dtComputeRouteOperation2;
            Workflow_NodeAddInOperation_Type0Entity _Type0Entity = new Workflow_NodeAddInOperation_Type0Entity();
            _Type0Entity.AddInOPID = txtAddInOPID.Value != "" ? Convert.ToInt32(txtAddInOPID.Value) : 0;
            _Type0Entity.NodeID = DNTRequest.GetInt("id", 0);
            _Type0Entity.OPTime = DNTRequest.GetInt("t", 0);
            _Type0Entity.TargetFieldID = Int32.Parse(ddlFieldIDTo.SelectedValue);
            if (_FieldDictEntity.HTMLTypeID == 8)
            {
                dtComputeRouteOperation2 = dtComputeRouteOperation.Clone();
                _Type0Entity.CaculateType = 2;
                _Type0Entity.CaculateValue = txtBrowseValue.Value;
            }
            else
            {
                dtComputeRouteOperation2 = dtComputeRouteOperation;
                _Type0Entity.CaculateType = Int32.Parse(rblAddInOperationType.SelectedValue);
                _Type0Entity.CaculateValue = RuleDetail[0];
            }
            _Type0Entity.DataSetID = txtDataSetID.Value != "" ? Int32.Parse(txtDataSetID.Value) : 0;
            _Type0Entity.ValueField = ddlValueColumn.SelectedValue;
            _Type0Entity.DataSourceID = txtDataSourceID.Value != "" ? Int32.Parse(txtDataSourceID.Value) : 0;
            _Type0Entity.StorageProcedure = ddlStorageProcedure.SelectedValue;
            _Type0Entity.OutputParameter = ddlOutputParameter.SelectedValue;
            _Type0Entity.OPCondition = "(1=1)";
            _Type0Entity.CreateSID = userEntity.UserSerialID;
            _Type0Entity.CreateDate = DateTime.Now;
            string AddInOPID = DbHelper.GetInstance().AddWorkflow_NodeAddInOperation_Type0(_Type0Entity, dtComputeRouteOperation2);
            dtComputeRouteOperation.Rows.Clear();
            lblExpression.Text = "";
            if (rblAddInOperationType.SelectedValue == "4")
            {
                DataTable dtSpParameter = DbHelper.GetInstance().GetAllStoredProcedureParameters(_Type0Entity.DataSourceID, _Type0Entity.StorageProcedure);
                for (int i = 0; i < dtSpParameter.Rows.Count; i++)
                {
                    if ("ReturnValue" == dtSpParameter.Rows[i]["ParameterDirection"].ToString())
                    {
                        continue;
                    }
                    Workflow_NodeAddInOperation_Type0_SpParameterEntity _Type0_SpParameterEntity = new Workflow_NodeAddInOperation_Type0_SpParameterEntity();
                    _Type0_SpParameterEntity.AddInOPID = Int32.Parse(AddInOPID);
                    _Type0_SpParameterEntity.SpParameter = dtSpParameter.Rows[i]["ParameterName"].ToString();
                    _Type0_SpParameterEntity.ParameterType = dtSpParameter.Rows[i]["ParameterType"].ToString();
                    _Type0_SpParameterEntity.ParameterSize = Convert.ToInt32(dtSpParameter.Rows[i]["ParameterSize"]);
                    _Type0_SpParameterEntity.ParameterDirection = dtSpParameter.Rows[i]["ParameterDirection"].ToString();
                    _Type0_SpParameterEntity.TartgetValue = "";
                    DbHelper.GetInstance().AddWorkflow_NodeAddInOperation_Type0_SpParameter(_Type0_SpParameterEntity);
                }
            }
            BindGridView();

            System.Web.UI.ScriptManager.RegisterStartupScript(btnAdd, this.GetType(), "ButtonHideScript", strButtonHideScript, false);
        }
        protected void btnDel_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                CheckBox cb = this.GridView1.Rows[i].FindControl("Item") as CheckBox;
                if (cb.Checked)
                {
                    string AddInOPID = GridView1.DataKeys[i][0].ToString().Trim();
                    DbHelper.GetInstance().DeleteWorkflow_NodeAddInOperation_Type0(AddInOPID, userEntity.UserSerialID.ToString());
                }
            }
            BindGridView();
            System.Web.UI.ScriptManager.RegisterStartupScript(btnAdd, this.GetType(), "ButtonHideScript", strButtonHideScript, false);
        }


        protected void lbFieldList_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dtComputeRouteOperation = (DataTable)ViewState["dtComputeRouteOperation"];
            DataRow drComputeRoute = dtComputeRouteOperation.NewRow();
            drComputeRoute["ComputeType"] = 2;
            drComputeRoute["RouteValue"] = lbFieldList.SelectedValue;
            drComputeRoute["RouteOrder"] = dtComputeRouteOperation.Rows.Count + 1;
            dtComputeRouteOperation.Rows.Add(drComputeRoute);
            string[] RuleDetail = GetRuleDetail();
            lblExpression.Text = RuleDetail[1];

            BindGridView();
            lbFieldList.SelectedIndex = -1;
        }
        protected void lbFunctionType_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGridView();
            ReDisplayFunctionList();
        }

        protected void lbFunctionList_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dtComputeRouteOperation = (DataTable)ViewState["dtComputeRouteOperation"];
            DataRow drComputeRoute = dtComputeRouteOperation.NewRow();
            drComputeRoute["ComputeType"] = 1;
            drComputeRoute["RouteValue"] = lbFunctionList.SelectedValue;
            drComputeRoute["RouteOrder"] = dtComputeRouteOperation.Rows.Count + 1;
            dtComputeRouteOperation.Rows.Add(drComputeRoute);
            string[] RuleDetail = GetRuleDetail();
            lblExpression.Text = RuleDetail[1];

            BindGridView();
            lbFunctionList.SelectedIndex = -1;
        }

        private string[] GetRuleDetail(DataRow dr)
        {
            string AddInOPID = dr["AddInOPID"].ToString();
            string TargetFieldID = dr["TargetFieldID"].ToString();
            string CaculateType = dr["CaculateType"].ToString();

            string[] RuleDetail = new string[2] { "", "" };
            if (CaculateType == "1")
            {
                DataTable dtComputeRouteOperation = DbHelper.GetInstance().GetDBRecords("*", "Workflow_ComputeRouteOperation", "AddInOPID=" + AddInOPID, "AddInOPID,RouteOrder");
                for (int i = 0; i < dtComputeRouteOperation.Rows.Count; i++)
                {
                    if (Convert.ToInt32(dtComputeRouteOperation.Rows[i]["ComputeType"]) == 1)
                    {
                        DataTable dtDetail = DbHelper.GetInstance().GetDBRecords("b.ComputeSymbol", "Workflow_ComputeRouteOperation a,Workflow_ComputeSymbol b", string.Format("a.RouteValue=b.ComputeID and a.RouteID={0}", dtComputeRouteOperation.Rows[i]["RouteID"]), "");
                        RuleDetail[0] = RuleDetail[0] + dtDetail.Rows[0]["ComputeSymbol"].ToString();
                        RuleDetail[1] = RuleDetail[1] + dtDetail.Rows[0]["ComputeSymbol"].ToString();
                    }
                    else if (Convert.ToInt32(dtComputeRouteOperation.Rows[i]["ComputeType"]) == 2)
                    {
                        DataTable dtDetail = DbHelper.GetInstance().GetDBRecords("c.FieldID,c.FieldLabel,d.FieldName", "Workflow_FlowNode a,Workflow_Base b,Workflow_FormField c,Workflow_FieldDict d", "a.WorkflowID=b.WorkflowID and b.FormID=c.FormID and c.FieldID=d.FieldID and c.IsDetail=0 and a.NodeID=" + DNTRequest.GetString("id") + " and c.FieldID=" + dtComputeRouteOperation.Rows[i]["RouteValue"].ToString(), "c.DisplayOrder");
                        RuleDetail[0] = RuleDetail[0] + dtDetail.Rows[0]["FieldName"].ToString();
                        RuleDetail[1] = RuleDetail[1] + dtDetail.Rows[0]["FieldLabel"].ToString();
                    }
                    else
                    {
                        RuleDetail[0] = RuleDetail[0] + dtComputeRouteOperation.Rows[i]["RouteValue"].ToString();
                        RuleDetail[1] = RuleDetail[1] + dtComputeRouteOperation.Rows[i]["RouteValue"].ToString();
                    }
                }
            }
            else if (CaculateType == "2")
            {
                string CaculateValue = dr["CaculateValue"].ToString();
                RuleDetail[0] = CaculateValue;
                Workflow_FieldDictEntity _FieldDictEntity = DbHelper.GetInstance().GetWorkflow_FieldDictEntityByKeyCol(TargetFieldID);
                if (_FieldDictEntity.BrowseType == 1)
                {
                    DataTable dt = DbHelper.GetInstance().ExecDataTable(string.Format("select PEEBIEC,PEEBIEN from PEEBI where PEEBIEC in ('{0}')", CaculateValue.Replace(",", "','")));
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        RuleDetail[1] = RuleDetail[1] + dt.Rows[i]["PEEBIEN"] + ",";
                    }
                }
                else if (_FieldDictEntity.BrowseType == 2)
                {
                    DataTable dt = DbHelper.GetInstance().ExecDataTable(string.Format("select PBDEPID,PBDEPDN from PBDEP where PBDEPID in ({0})", CaculateValue));
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        RuleDetail[1] = RuleDetail[1] + dt.Rows[i]["PBDEPDN"] + ",";
                    }
                }
                else if (_FieldDictEntity.BrowseType == 3)
                {
                    DataTable dt = DbHelper.GetInstance().ExecDataTable(string.Format("select UserSerialID,UserName from UserList where UserSerialID in ({0})", CaculateValue == "" ? "0" : CaculateValue));
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        RuleDetail[1] = RuleDetail[1] + dt.Rows[i]["UserName"] + ",";
                    }
                }
                RuleDetail[1] = RuleDetail[1].Trim(new char[] { ',' });
            }
            return RuleDetail;
        }

        private string[] GetRuleDetail()
        {
            string[] RuleDetail = new string[2] { "", "" };
            DataTable dtComputeRouteOperation = (DataTable)ViewState["dtComputeRouteOperation"];
            for (int i = 0; i < dtComputeRouteOperation.Rows.Count; i++)
            {
                if (Convert.ToInt32(dtComputeRouteOperation.Rows[i]["ComputeType"]) == 1)
                {
                    DataTable dtDetail = DbHelper.GetInstance().GetDBRecords("b.ComputeSymbol", "Workflow_ComputeSymbol b", string.Format("ComputeID='{0}'", dtComputeRouteOperation.Rows[i]["RouteValue"]), "");
                    RuleDetail[0] = RuleDetail[0] + dtDetail.Rows[0]["ComputeSymbol"].ToString();
                    RuleDetail[1] = RuleDetail[1] + dtDetail.Rows[0]["ComputeSymbol"].ToString();
                }
                else if (Convert.ToInt32(dtComputeRouteOperation.Rows[i]["ComputeType"]) == 2)
                {
                    DataTable dtDetail = DbHelper.GetInstance().GetDBRecords("c.FieldID,c.FieldLabel,d.FieldName", "Workflow_FlowNode a,Workflow_Base b,Workflow_FormField c,Workflow_FieldDict d", "a.WorkflowID=b.WorkflowID and b.FormID=c.FormID and c.FieldID=d.FieldID and c.IsDetail=0 and a.NodeID=" + DNTRequest.GetString("id") + " and c.FieldID=" + dtComputeRouteOperation.Rows[i]["RouteValue"].ToString(), "c.DisplayOrder");
                    RuleDetail[0] = RuleDetail[0] + dtDetail.Rows[0]["FieldName"].ToString();
                    RuleDetail[1] = RuleDetail[1] + dtDetail.Rows[0]["FieldLabel"].ToString();
                }
                else
                {
                    RuleDetail[0] = RuleDetail[0] + dtComputeRouteOperation.Rows[i]["RouteValue"].ToString();
                    RuleDetail[1] = RuleDetail[1] + dtComputeRouteOperation.Rows[i]["RouteValue"].ToString();
                }
            }
            return RuleDetail;
        }

        private void ReDisplayFunctionList()
        {
            string ComputeTypeID = lbFunctionType.SelectedValue;
            DataTable dtFuncionList = DbHelper.GetInstance().GetDBRecords("ComputeID,ComputeSymbol", "Workflow_ComputeSymbol", string.Format("ComputeTypeID='{0}' and Useflag=1", ComputeTypeID), "DisplayOrder");
            lbFunctionList.DataSource = dtFuncionList;
            lbFunctionList.DataValueField = "ComputeID";
            lbFunctionList.DataTextField = "ComputeSymbol";
            lbFunctionList.DataBind();
        }

        protected void btnComputeOperation_Click(object sender, EventArgs e)
        {
            System.Web.UI.WebControls.Button btnComputeOperation = (System.Web.UI.WebControls.Button)sender;
            DataTable dtComputeRouteOperation = (DataTable)ViewState["dtComputeRouteOperation"];
            DataRow drComputeRoute = dtComputeRouteOperation.NewRow();
            drComputeRoute["ComputeType"] = 1;
            drComputeRoute["RouteValue"] = btnComputeOperation.ID.Replace("ComputeOperation", "");
            drComputeRoute["RouteOrder"] = dtComputeRouteOperation.Rows.Count + 1;
            dtComputeRouteOperation.Rows.Add(drComputeRoute);
            string[] RuleDetail = GetRuleDetail();
            lblExpression.Text = RuleDetail[1];

            BindGridView();
        }

        protected void btnConstant_Click(object sender, EventArgs e)
        {
            DataTable dtComputeRouteOperation = (DataTable)ViewState["dtComputeRouteOperation"];
            DataRow drComputeRoute = dtComputeRouteOperation.NewRow();
            drComputeRoute["ComputeType"] = rblConstantType.SelectedValue;
            if (rblConstantType.SelectedValue == "3")
            {
                drComputeRoute["RouteValue"] = txtConstantValue.Text != "" ? txtConstantValue.Text : "0";
            }
            else if (rblConstantType.SelectedValue == "4")
            {
                drComputeRoute["RouteValue"] = "'" + txtConstantValue.Text + "'";
            }
            drComputeRoute["RouteOrder"] = dtComputeRouteOperation.Rows.Count + 1;
            dtComputeRouteOperation.Rows.Add(drComputeRoute);
            string[] RuleDetail = GetRuleDetail();
            lblExpression.Text = RuleDetail[1];

            BindGridView();
        }

        protected void ddlFieldIDTo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlFieldIDTo.SelectedIndex == 0)
            {
                divOperationCulculate.Visible = false;
                divOperationBrowse.Visible = false;
                return;
            }
            divOperationExpression.Visible = rblAddInOperationType.SelectedValue == "1";
            divOperationDataSet.Visible = rblAddInOperationType.SelectedValue == "3";
            divOperationDataSource.Visible = rblAddInOperationType.SelectedValue == "4";
            Workflow_FieldDictEntity _FieldDictEntity = DbHelper.GetInstance().GetWorkflow_FieldDictEntityByKeyCol(ddlFieldIDTo.SelectedValue);
            divOperationCulculate.Visible = (_FieldDictEntity.HTMLTypeID != 8);
            divOperationBrowse.Visible = (_FieldDictEntity.HTMLTypeID == 8);
            if (_FieldDictEntity.HTMLTypeID == 8)
            {
                txtBrowseValue.Value = "";
                txtBrowseText.Text = "";
                Workflow_BrowseTypeEntity _BrowseTypeEntity = DbHelper.GetInstance().GetWorkflow_BrowseTypeEntityByKeyCol(_FieldDictEntity.BrowseType.ToString());
                ImgBrowse.OnClientClick = string.Format("return btnBrowseClick('txtBrowseValue','txtBrowseText','{0}');", _BrowseTypeEntity.BrowsePage);
            }
            BindGridView();
        }

        protected void rblAddInOperationType_SelectedIndexChanged(object sender, EventArgs e)
        {
            divOperationExpression.Visible = rblAddInOperationType.SelectedValue == "1";
            divOperationDataSet.Visible = rblAddInOperationType.SelectedValue == "3";
            divOperationDataSource.Visible = rblAddInOperationType.SelectedValue == "4";

            BindGridView();
        }

        protected void txtDataSet_TextChanged(object sender, EventArgs e)
        {
            ReDisplayDataSet();
            BindGridView();
        }

        protected void txtDataSource_TextChanged(object sender, EventArgs e)
        {
            string DataSourceID = txtDataSourceID.Value;
            DataTable dtSP = DbHelper.GetInstance().GetAllStoredProcedureNames(Int32.Parse(DataSourceID));
            ddlStorageProcedure.AddTableData(dtSP, 0, 0, true, "Null");

            BindGridView();
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
                ddlValueColumn.Items.Add(new ListItem("--请选择--", ""));
                for (int i = 0; i < ColumnList.Length; i++)
                {
                    ddlValueColumn.Items.Add(ColumnList[i]);
                }
            }
            catch
            {
            }
        }

        protected void ddlStorageProcedure_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string DataSourceID = txtDataSourceID.Value;
                string StoreProcedure = ddlStorageProcedure.SelectedValue;
                DataTable dtParameters = DbHelper.GetInstance().GetAllStoredProcedureParameters(Int32.Parse(DataSourceID), StoreProcedure);
                for (int i = dtParameters.Rows.Count - 1; i >= 0; i--)
                {
                    if (!dtParameters.Rows[i]["ParameterDirection"].ToString().Contains("Output"))
                    {
                        dtParameters.Rows.RemoveAt(i);
                    }
                }
                ddlOutputParameter.AddTableData(dtParameters, 0, 0, true, "Null");
            }
            catch
            {
            }
            BindGridView();
        }
    }
}
