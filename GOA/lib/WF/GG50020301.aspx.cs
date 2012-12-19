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
    public partial class GG50020301 : BasePage
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
                DataTable dtParameterList = DbHelper.GetInstance().GetDBRecords("a.ParameterName,ParameterNameDetail=a.ParameterName+'-'+a.ParameterType", "Workflow_DataSetParameter a,Workflow_NodeAddInOperation_Type0 b", "a.DataSetID=b.DataSetID and AddInOPID=" + DNTRequest.GetString("opid"), "");
                ddlParameterList.AddTableData(dtParameterList, 0, 1, true, "Select");

                DataTable dtMailField = DbHelper.GetInstance().GetDBRecords("c.FieldID,c.FieldLabel,d.FieldName", "Workflow_NodeAddInOperation_Type0 aa,Workflow_FlowNode a,Workflow_Base b,Workflow_FormField c,Workflow_FieldDict d", "aa.NodeID=a.NodeID and a.WorkflowID=b.WorkflowID and b.FormID=c.FormID and c.FieldID=d.FieldID and c.IsDetail=0 and aa.AddInOPID=" + DNTRequest.GetString("opid"), "c.DisplayOrder");
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

                DataTable dtComputeRouteDetail = DbHelper.GetInstance().GetDBRecords("ComputeType,RouteValue,RouteOrder", "Workflow_ComputeRouteDetail", "RouteID=0", "RouteOrder");
                ViewState["dtComputeRouteDetail"] = dtComputeRouteDetail;
                BindGridView();
            }
        }

        #region gridview 绑定
        //此类需要更改，主要是更改获取数据源的方法
        private void BindGridView()
        {
            DataTable dt = DbHelper.GetInstance().GetDBRecords("a.AddInOPID,a.DSParameter,a.TartgetValue,TartgetValueN='',b.DataSetID,c.ParameterType,c.ParameterDirection", "Workflow_NodeAddInOperation_Type0_DataSetParameter a,Workflow_NodeAddInOperation_Type0 b, Workflow_DataSetParameter c", "a.AddInOPID=b.AddInOPID and a.DSParameter=c.ParameterName and b.DataSetID=c.DataSetID and a.AddInOPID=" + DNTRequest.GetString("opid"), "");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dt.Rows[i]["TartgetValueN"] = GetRuleDetail(dt.Rows[i])[1];
            }
            GridView1.DataSource = dt;
            GridView1.DataBind();
            BuildNoRecords(GridView1, dt);
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
            }
            BindGridView();
        }
        //此类要进行dorpdownlist/chk控件的转换
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
            }
        }
        #endregion

        protected void btnComputeOperation_Click(object sender, EventArgs e)
        {
            System.Web.UI.WebControls.Button btnComputeOperation = (System.Web.UI.WebControls.Button)sender;
            DataTable dtComputeRouteDetail = (DataTable)ViewState["dtComputeRouteDetail"];
            DataRow drComputeRoute = dtComputeRouteDetail.NewRow();
            drComputeRoute["ComputeType"] = 1;
            drComputeRoute["RouteValue"] = btnComputeOperation.ID.Replace("ComputeOperation", "");
            drComputeRoute["RouteOrder"] = dtComputeRouteDetail.Rows.Count + 1;
            dtComputeRouteDetail.Rows.Add(drComputeRoute);
            string[] RuleDetail = GetRuleDetail();
            lblExpression.Text = RuleDetail[1];

            BindGridView();
        }

        private string[] GetRuleDetail(DataRow dr)
        {
            string AddInOPID = dr["AddInOPID"].ToString();
            string DSParameter = dr["DSParameter"].ToString();

            string[] RuleDetail = new string[2] { "", "" };
            DataTable dt = DbHelper.GetInstance().GetDBRecords("*", "Workflow_ComputeRouteDetail", string.Format("AddInOPID={0} and DSParameter='{1}'", AddInOPID, DSParameter), "RouteID,RouteOrder");
            if (dt.Rows.Count == 0)
            {
                RuleDetail[0] = dr["TartgetValue"].ToString();
                RuleDetail[1] = dr["TartgetValue"].ToString();
            }
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (Convert.ToInt32(dt.Rows[i]["ComputeType"]) == 1)
                {
                    DataTable dtDetail = DbHelper.GetInstance().GetDBRecords("b.ComputeSymbol", "Workflow_ComputeRouteDetail a,Workflow_ComputeSymbol b", string.Format("a.RouteValue=b.ComputeID and a.RouteID={0}", dt.Rows[i]["RouteID"]), "");
                    RuleDetail[0] = RuleDetail[0] + dtDetail.Rows[0]["ComputeSymbol"].ToString();
                    RuleDetail[1] = RuleDetail[1] + dtDetail.Rows[0]["ComputeSymbol"].ToString();
                }
                else if (Convert.ToInt32(dt.Rows[i]["ComputeType"]) == 2)
                {
                    DataTable dtDetail = DbHelper.GetInstance().GetDBRecords("c.FieldID,c.FieldLabel,d.FieldName", "Workflow_NodeAddInOperation_Type0 aa,Workflow_FlowNode a,Workflow_Base b,Workflow_FormField c,Workflow_FieldDict d", string.Format("aa.NodeID=a.NodeID and a.WorkflowID=b.WorkflowID and b.FormID=c.FormID and c.FieldID=d.FieldID and c.IsDetail=0 and aa.AddInOPID={0} and c.FieldID={1} ", DNTRequest.GetString("opid"), dt.Rows[i]["RouteValue"]), "");
                    RuleDetail[0] = RuleDetail[0] + (dtDetail.Rows.Count > 0 ? dtDetail.Rows[0]["FieldName"].ToString() : "");
                    RuleDetail[1] = RuleDetail[1] + (dtDetail.Rows.Count > 0 ? dtDetail.Rows[0]["FieldLabel"].ToString() : "");
                }
                else
                {
                    RuleDetail[0] = RuleDetail[0] + dt.Rows[i]["RouteValue"].ToString();
                    RuleDetail[1] = RuleDetail[1] + dt.Rows[i]["RouteValue"].ToString();
                }
            }
            return RuleDetail;
        }

        private string[] GetRuleDetail()
        {
            string[] RuleDetail = new string[2] { "", "" };
            DataTable dtComputeRouteDetail = (DataTable)ViewState["dtComputeRouteDetail"];
            for (int i = 0; i < dtComputeRouteDetail.Rows.Count; i++)
            {
                if (Convert.ToInt32(dtComputeRouteDetail.Rows[i]["ComputeType"]) == 1)
                {
                    DataTable dtDetail = DbHelper.GetInstance().GetDBRecords("b.ComputeSymbol", "Workflow_ComputeSymbol b", string.Format("ComputeID='{0}'", dtComputeRouteDetail.Rows[i]["RouteValue"]), "");
                    RuleDetail[0] = RuleDetail[0] + dtDetail.Rows[0]["ComputeSymbol"].ToString();
                    RuleDetail[1] = RuleDetail[1] + dtDetail.Rows[0]["ComputeSymbol"].ToString();
                }
                else if (Convert.ToInt32(dtComputeRouteDetail.Rows[i]["ComputeType"]) == 2)
                {
                    DataTable dtDetail = DbHelper.GetInstance().GetDBRecords("c.FieldID,c.FieldLabel,d.FieldName", "Workflow_NodeAddInOperation_Type0 aa,Workflow_FlowNode a,Workflow_Base b,Workflow_FormField c,Workflow_FieldDict d", "aa.NodeID=a.NodeID and a.WorkflowID=b.WorkflowID and b.FormID=c.FormID and c.FieldID=d.FieldID and c.IsDetail=0 and aa.AddInOPID=" + DNTRequest.GetString("opid") + " and c.FieldID=" + dtComputeRouteDetail.Rows[i]["RouteValue"].ToString(), "c.DisplayOrder");
                    RuleDetail[0] = RuleDetail[0] + dtDetail.Rows[0]["FieldName"].ToString();
                    RuleDetail[1] = RuleDetail[1] + dtDetail.Rows[0]["FieldLabel"].ToString();
                }
                else
                {
                    RuleDetail[0] = RuleDetail[0] + dtComputeRouteDetail.Rows[i]["RouteValue"].ToString();
                    RuleDetail[1] = RuleDetail[1] + dtComputeRouteDetail.Rows[i]["RouteValue"].ToString();
                }
            }
            return RuleDetail;
        }

        protected void btnUndo_Click(object sender, EventArgs e)
        {
            DataTable dtComputeRouteDetail = (DataTable)ViewState["dtComputeRouteDetail"];
            if (dtComputeRouteDetail.Rows.Count >= 1)
            {
                dtComputeRouteDetail.Rows.RemoveAt(dtComputeRouteDetail.Rows.Count - 1);
            }
            string[] RuleDetail = GetRuleDetail();
            lblExpression.Text = RuleDetail[1];

            BindGridView();
            System.Web.UI.ScriptManager.RegisterStartupScript(btnUndo, this.GetType(), "ButtonHideScript", strButtonHideScript, false);
        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            string[] RuleDetail = GetRuleDetail();
            try
            {
                DbHelper.GetInstance().ExecSqlText(string.Format(" select {0} from Workflow_Form where RequestID=0", RuleDetail[0]));
            }
            catch
            {
                ScriptManager.RegisterStartupScript(btnAdd, this.GetType(), "add", "alert('赋值表达式不正确,无法添加');", true);
                return;
            }
            DataTable dtComputeRouteDetail = (DataTable)ViewState["dtComputeRouteDetail"];
            Workflow_NodeAddInOperation_Type0_DataSetParameterEntity _DataSetParameterEntity = new Workflow_NodeAddInOperation_Type0_DataSetParameterEntity();
            _DataSetParameterEntity.AddInOPID = DNTRequest.GetInt("opid", 0);
            _DataSetParameterEntity.DSParameter = ddlParameterList.SelectedValue;
            _DataSetParameterEntity.TartgetValue = GetRuleDetail()[0];
            DbHelper.GetInstance().UpdateWorkflow_NodeAddInOperation_Type0_DataSetParameter(_DataSetParameterEntity, dtComputeRouteDetail);
            dtComputeRouteDetail.Rows.Clear();
            lblExpression.Text = "";
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
                    string DSParameter = GridView1.DataKeys[i][0].ToString().Trim();
                    DbHelper.GetInstance().DeleteNodeAddInOperation_Type0_DataSetParameter(DNTRequest.GetString("opid"), DSParameter);
                }
            }
            BindGridView();
            System.Web.UI.ScriptManager.RegisterStartupScript(btnAdd, this.GetType(), "ButtonHideScript", strButtonHideScript, false);
        }


        protected void lbFieldList_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dtComputeRouteDetail = (DataTable)ViewState["dtComputeRouteDetail"];
            DataRow drComputeRoute = dtComputeRouteDetail.NewRow();
            drComputeRoute["ComputeType"] = 2;
            drComputeRoute["RouteValue"] = lbFieldList.SelectedValue;
            drComputeRoute["RouteOrder"] = dtComputeRouteDetail.Rows.Count + 1;
            dtComputeRouteDetail.Rows.Add(drComputeRoute);
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
            DataTable dtComputeRouteDetail = (DataTable)ViewState["dtComputeRouteDetail"];
            DataRow drComputeRoute = dtComputeRouteDetail.NewRow();
            drComputeRoute["ComputeType"] = 1;
            drComputeRoute["RouteValue"] = lbFunctionList.SelectedValue;
            drComputeRoute["RouteOrder"] = dtComputeRouteDetail.Rows.Count + 1;
            dtComputeRouteDetail.Rows.Add(drComputeRoute);
            string[] RuleDetail = GetRuleDetail();
            lblExpression.Text = RuleDetail[1];

            BindGridView();
            lbFunctionList.SelectedIndex = -1;
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

        protected void btnConstant_Click(object sender, EventArgs e)
        {
            DataTable dtComputeRouteDetail = (DataTable)ViewState["dtComputeRouteDetail"];
            DataRow drComputeRoute = dtComputeRouteDetail.NewRow();
            drComputeRoute["ComputeType"] = rblConstantType.SelectedValue;
            if (rblConstantType.SelectedValue == "3")
            {
                drComputeRoute["RouteValue"] = txtConstantValue.Text != "" ? txtConstantValue.Text : "0";
            }
            else if (rblConstantType.SelectedValue == "4")
            {
                drComputeRoute["RouteValue"] = "'" + txtConstantValue.Text + "'";
            }
            drComputeRoute["RouteOrder"] = dtComputeRouteDetail.Rows.Count + 1;
            dtComputeRouteDetail.Rows.Add(drComputeRoute);
            string[] RuleDetail = GetRuleDetail();
            lblExpression.Text = RuleDetail[1];

            BindGridView();
        }
    }
}
