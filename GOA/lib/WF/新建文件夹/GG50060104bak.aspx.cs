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

namespace GeobyWorkflow
{
    public partial class GG50060104 : BasePage
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
                DataTable dtNodeAddInOperation_Type1 = DbHelper.GetInstance().GetDBRecords("DataSourceID,DataSourceTable,GroupID", "Workflow_NodeAddInOperation_Type1", "AddInOPID=" + DNTRequest.GetString("opid"), "");
                int DataSourceID = Convert.ToInt32(dtNodeAddInOperation_Type1.Rows[0]["DataSourceID"]);
                string DataSourceTable = dtNodeAddInOperation_Type1.Rows[0]["DataSourceTable"].ToString();
                string GroupID = dtNodeAddInOperation_Type1.Rows[0]["GroupID"].ToString();
                if(GroupID == "0")
                {
                    rblFieldType.SelectedValue = "1";
                    rblFieldType.Enabled = false;
                    ReDisplayFieldList();
                }
                DataTable dtColumns = DbHelper.GetInstance().GetAllStoredProcedureParameters(DataSourceID, DataSourceTable);
                ddlParameterList.AddTableData(dtColumns, 0, 0, true, "Null");
                
                DataTable dtComputeType = DbHelper.GetInstance().GetDBRecords("ComputeTypeID, ComputeTypeName", "Workflow_ComputeType", "Useflag=1", "DisplayOrder");
                lbFunctionType.DataSource = dtComputeType;
                lbFunctionType.DataValueField = "ComputeTypeID";
                lbFunctionType.DataTextField = "ComputeTypeName";
                lbFunctionType.DataBind();

                ReDisplayFunctionList();

                DataTable dtComputeRoute = DbHelper.GetInstance().GetDBRecords("ComputeType,RouteValue,RouteOrder", "Workflow_ComputeRoute", "RouteID=0", "RouteOrder");
                ViewState["dtComputeRoute"] = dtComputeRoute;
                BindGridView();
            }
        }

        #region gridview 绑定
        //此类需要更改，主要是更改获取数据源的方法
        private void BindGridView()
        {
            DataTable dt = DbHelper.GetInstance().GetDBRecords("a.AddInOPID,a.SpParameter,a.TartgetValue,a.FieldTypeID,TartgetValueN='',b.FieldTypeName", "Workflow_NodeAddInOperation_Type1_SpParameter a left join Workflow_FieldType b on a.FieldTypeID=b.FieldTypeID", "a.AddInOPID=" + DNTRequest.GetString("opid"), "a.AddInOPID,a.SpParameter");
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
            int index = -1;
            if (e.CommandName == "select")
            {
                index = Convert.ToInt32(e.CommandArgument);   //获取行号
                string SpParameter = GridView1.DataKeys[index][0].ToString().Trim();
                DataTable dtComputeRoute = DbHelper.GetInstance().GetDBRecords("ComputeType,RouteValue,RouteOrder", "Workflow_ComputeRouteSpParameter", string.Format("AddInOPID={0} and TargetField='{1}'", DNTRequest.GetString("opid"), SpParameter), "RouteOrder");
                ViewState["dtComputeRoute"] = dtComputeRoute;
                string[] RuleDetail = GetRuleDetail();
                lblExpression.Text = RuleDetail[1];
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

        protected void btnUndo_Click(object sender, EventArgs e)
        {
            DataTable dtComputeRoute = (DataTable)ViewState["dtComputeRoute"];
            if (dtComputeRoute.Rows.Count >= 1)
            {
                dtComputeRoute.Rows.RemoveAt(dtComputeRoute.Rows.Count - 1);
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
                DbHelper.GetInstance().ExecSqlText(string.Format(" select {0} from Workflow_Form{1} where RequestID=0", RuleDetail[0], rblFieldType.SelectedValue == "1" ? "" : "Detail"));
            }
            catch
            {
                ScriptManager.RegisterStartupScript(btnAdd, this.GetType(), "add", "alert('赋值表达式不正确,无法添加');", true);
                return;
            }

            DataTable dtComputeRoute = (DataTable)ViewState["dtComputeRoute"];
            Workflow_NodeAddInOperation_Type1_SpParameterEntity _Type1_SpParameterEntity = new Workflow_NodeAddInOperation_Type1_SpParameterEntity();
            _Type1_SpParameterEntity.AddInOPID = DNTRequest.GetInt("opid", 0);
            _Type1_SpParameterEntity.SpParameter = ddlParameterList.SelectedValue;
            _Type1_SpParameterEntity.TartgetValue = GetRuleDetail()[0];
            _Type1_SpParameterEntity.FieldTypeID = Int32.Parse(rblFieldType.SelectedValue);
            DbHelper.GetInstance().UpdateWorkflow_NodeAddInOperation_Type1_SpParameter(_Type1_SpParameterEntity, dtComputeRoute);
            dtComputeRoute.Rows.Clear();
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
                    string SpParameter = GridView1.DataKeys[i][0].ToString().Trim();
                    Workflow_NodeAddInOperation_Type1_SpParameterEntity _Type1_SpParameterEntity = new Workflow_NodeAddInOperation_Type1_SpParameterEntity();
                    _Type1_SpParameterEntity.AddInOPID = DNTRequest.GetInt("opid", 0);
                    _Type1_SpParameterEntity.SpParameter = SpParameter;
                    _Type1_SpParameterEntity.TartgetValue = "";
                    _Type1_SpParameterEntity.FieldTypeID = 0;
                    DataTable dtComputeRoute = (DataTable)ViewState["dtComputeRoute"];
                    DataTable dtComputeRoute2 = dtComputeRoute.Clone();
                    DbHelper.GetInstance().UpdateWorkflow_NodeAddInOperation_Type1_SpParameter(_Type1_SpParameterEntity, dtComputeRoute2);
                }
            }
            BindGridView();
            System.Web.UI.ScriptManager.RegisterStartupScript(btnAdd, this.GetType(), "ButtonHideScript", strButtonHideScript, false);
        }

        protected void lbFieldList_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dtComputeRoute = (DataTable)ViewState["dtComputeRoute"];
            DataRow drComputeRoute = dtComputeRoute.NewRow();
            drComputeRoute["ComputeType"] = 2;
            drComputeRoute["RouteValue"] = lbFieldList.SelectedValue;
            drComputeRoute["RouteOrder"] = dtComputeRoute.Rows.Count + 1;
            dtComputeRoute.Rows.Add(drComputeRoute);
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
            DataTable dtComputeRoute = (DataTable)ViewState["dtComputeRoute"];
            DataRow drComputeRoute = dtComputeRoute.NewRow();
            drComputeRoute["ComputeType"] = 1;
            drComputeRoute["RouteValue"] = lbFunctionList.SelectedValue;
            drComputeRoute["RouteOrder"] = dtComputeRoute.Rows.Count + 1;
            dtComputeRoute.Rows.Add(drComputeRoute);
            string[] RuleDetail = GetRuleDetail();
            lblExpression.Text = RuleDetail[1];

            BindGridView();
            lbFunctionList.SelectedIndex = -1;
        }

        private string[] GetRuleDetail(DataRow dr)
        {
            string AddInOPID = dr["AddInOPID"].ToString();
            string SpParameter = dr["SpParameter"].ToString();
            string FieldTypeID = dr["FieldTypeID"].ToString();
            string[] RuleDetail = new string[2] { "", "" };
            DataTable dt = DbHelper.GetInstance().GetDBRecords("*", "Workflow_ComputeRouteSpParameter", string.Format("AddInOPID={0} and SpParameter='{1}'", AddInOPID, SpParameter), "RouteID,RouteOrder");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (Convert.ToInt32(dt.Rows[i]["ComputeType"]) == 1)
                {
                    DataTable dtDetail = DbHelper.GetInstance().GetDBRecords("b.ComputeSymbol", "Workflow_ComputeRouteSpParameter a,Workflow_ComputeSymbol b", string.Format("a.RouteValue=b.ComputeID and a.RouteID={0}", dt.Rows[i]["RouteID"]), "");
                    RuleDetail[0] = RuleDetail[0] + dtDetail.Rows[0]["ComputeSymbol"].ToString();
                    RuleDetail[1] = RuleDetail[1] + dtDetail.Rows[0]["ComputeSymbol"].ToString();
                }
                else if (Convert.ToInt32(dt.Rows[i]["ComputeType"]) == 2)
                {
                    //DataTable dtField = DbHelper.GetInstance().GetDBRecords("e.FieldName,d.FieldLabel", "Workflow_NodeAddInOperation_Type1 a,Workflow_FlowNode b,Workflow_Base c,Workflow_FormField d,Workflow_FieldDict e", string.Format("a.NodeID=b.NodeID and b.WorkflowID=c.WorkflowID and c.FormID=d.FormID and d.FieldID=e.FieldID and d.GroupID={1} and a.AddInOPID={0} and d.FieldID={2}", DNTRequest.GetString("opid"), FieldTypeID == "1" ? "0" : "a.GroupID", dt.Rows[i]["RouteValue"]), "");
                    string GroupID = FieldTypeID == "1" ? "0" : (FieldTypeID == "2" ? "a.GroupID" : "-1");
                    string sql = string.Format(@"
SELECT   FieldID,
         FieldLabel,
         FieldName
FROM     (SELECT d.FieldID,
                 d.FieldLabel,
                 e.FieldName,
                 d.DisplayOrder
          FROM   Workflow_NodeAddInOperation_Type1 AS a, Workflow_FlowNode AS b, Workflow_Base AS c, Workflow_FormField AS d, Workflow_FieldDict AS e
          WHERE  a.NodeID = b.NodeID
                 AND b.WorkflowID = c.WorkflowID
                 AND c.FormID = d.FormID
                 AND d.FieldID = e.FieldID
                 AND {1} = d.GroupID
                 AND a.AddInOPID = {0}
          UNION
          SELECT FieldID,
                 FieldDesc AS FieldLabel,
                 FieldName,
                 99990 AS DisplayOrder
          FROM   Workflow_FieldDict
          WHERE  BuiltInFlag = 1
                 AND '{1}'='0') AS aa
WHERE FieldID={2}
ORDER BY DisplayOrder;
", DNTRequest.GetString("opid"), GroupID, dt.Rows[i]["RouteValue"]);
                    DataTable dtField = DbHelper.GetInstance().ExecDataTable(sql);
                    RuleDetail[0] = RuleDetail[0] + dtField.Rows[0]["FieldName"].ToString();
                    RuleDetail[1] = RuleDetail[1] + dtField.Rows[0]["FieldLabel"].ToString();
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
            DataTable dtComputeRoute = (DataTable)ViewState["dtComputeRoute"];
            for (int i = 0; i < dtComputeRoute.Rows.Count; i++)
            {
                if (Convert.ToInt32(dtComputeRoute.Rows[i]["ComputeType"]) == 1)
                {
                    DataTable dtDetail = DbHelper.GetInstance().GetDBRecords("b.ComputeSymbol", "Workflow_ComputeSymbol b", string.Format("ComputeID='{0}'", dtComputeRoute.Rows[i]["RouteValue"]), "");
                    RuleDetail[0] = RuleDetail[0] + dtDetail.Rows[0]["ComputeSymbol"].ToString();
                    RuleDetail[1] = RuleDetail[1] + dtDetail.Rows[0]["ComputeSymbol"].ToString();
                }
                else if (Convert.ToInt32(dtComputeRoute.Rows[i]["ComputeType"]) == 2)
                {
                    //DataTable dtField = DbHelper.GetInstance().GetDBRecords("e.FieldName,d.FieldLabel", "Workflow_NodeAddInOperation_Type1 a,Workflow_FlowNode b,Workflow_Base c,Workflow_FormField d,Workflow_FieldDict e", string.Format("a.NodeID=b.NodeID and b.WorkflowID=c.WorkflowID and c.FormID=d.FormID and d.FieldID=e.FieldID and d.GroupID={1} and a.AddInOPID={0} and d.FieldID={2}", DNTRequest.GetString("opid"), rblFieldType.SelectedValue == "1" ? "0" : "a.GroupID", dtComputeRoute.Rows[i]["RouteValue"]), "");
                    string GroupID = rblFieldType.SelectedValue == "1" ? "0" : (rblFieldType.SelectedValue == "2" ? "a.GroupID" : "-1");
                    string sql = string.Format(@"
SELECT   FieldID,
         FieldLabel,
         FieldName
FROM     (SELECT d.FieldID,
                 d.FieldLabel,
                 e.FieldName,
                 d.DisplayOrder
          FROM   Workflow_NodeAddInOperation_Type1 AS a, Workflow_FlowNode AS b, Workflow_Base AS c, Workflow_FormField AS d, Workflow_FieldDict AS e
          WHERE  a.NodeID = b.NodeID
                 AND b.WorkflowID = c.WorkflowID
                 AND c.FormID = d.FormID
                 AND d.FieldID = e.FieldID
                 AND {1} = d.GroupID
                 AND a.AddInOPID = {0}
          UNION
          SELECT FieldID,
                 FieldDesc AS FieldLabel,
                 FieldName,
                 99990 AS DisplayOrder
          FROM   Workflow_FieldDict
          WHERE  BuiltInFlag = 1
                 AND '{1}'='0') AS aa
WHERE FieldID={2}
ORDER BY DisplayOrder;
", DNTRequest.GetString("opid"), GroupID, dtComputeRoute.Rows[i]["RouteValue"]);
                    DataTable dtField = DbHelper.GetInstance().ExecDataTable(sql);
                    RuleDetail[0] = RuleDetail[0] + dtField.Rows[0]["FieldName"].ToString();
                    RuleDetail[1] = RuleDetail[1] + dtField.Rows[0]["FieldLabel"].ToString();
                }
                else
                {
                    RuleDetail[0] = RuleDetail[0] + dtComputeRoute.Rows[i]["RouteValue"].ToString();
                    RuleDetail[1] = RuleDetail[1] + dtComputeRoute.Rows[i]["RouteValue"].ToString();
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
            DataTable dtComputeRoute = (DataTable)ViewState["dtComputeRoute"];
            DataRow drComputeRoute = dtComputeRoute.NewRow();
            drComputeRoute["ComputeType"] = 1;
            drComputeRoute["RouteValue"] = btnComputeOperation.ID.Replace("ComputeOperation", "");
            drComputeRoute["RouteOrder"] = dtComputeRoute.Rows.Count + 1;
            dtComputeRoute.Rows.Add(drComputeRoute);
            string[] RuleDetail = GetRuleDetail();
            lblExpression.Text = RuleDetail[1];

            BindGridView();
        }

        protected void btnConstant_Click(object sender, EventArgs e)
        {
            DataTable dtComputeRoute = (DataTable)ViewState["dtComputeRoute"];
            DataRow drComputeRoute = dtComputeRoute.NewRow();
            drComputeRoute["ComputeType"] = rblConstantType.SelectedValue;
            if (rblConstantType.SelectedValue == "3")
            {
                drComputeRoute["RouteValue"] = txtConstantValue.Text != "" ? txtConstantValue.Text : "0";
            }
            else if (rblConstantType.SelectedValue == "4")
            {
                drComputeRoute["RouteValue"] = "'" + txtConstantValue.Text + "'";
            }
            drComputeRoute["RouteOrder"] = dtComputeRoute.Rows.Count + 1;
            dtComputeRoute.Rows.Add(drComputeRoute);
            string[] RuleDetail = GetRuleDetail();
            lblExpression.Text = RuleDetail[1];

            BindGridView();
        }

        protected void rblFieldType_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dtComputeRoute = (DataTable)ViewState["dtComputeRoute"];
            dtComputeRoute.Rows.Clear();
            lblExpression.Text = "";
            ReDisplayFieldList();
            BindGridView();
        }

        private void ReDisplayFieldList()
        {
            string GroupID = rblFieldType.SelectedValue == "1" ? "0" : (rblFieldType.SelectedValue == "2" ? "a.GroupID" : "-1");
            string sql = string.Format(@"
SELECT   FieldID,
         FieldLabel,
         FieldName
FROM     (SELECT d.FieldID,
                 d.FieldLabel,
                 e.FieldName,
                 d.DisplayOrder
          FROM   Workflow_NodeAddInOperation_Type1 AS a, Workflow_FlowNode AS b, Workflow_Base AS c, Workflow_FormField AS d, Workflow_FieldDict AS e
          WHERE  a.NodeID = b.NodeID
                 AND b.WorkflowID = c.WorkflowID
                 AND c.FormID = d.FormID
                 AND d.FieldID = e.FieldID
                 AND {1} = d.GroupID
                 AND a.AddInOPID = {0}
          UNION
          SELECT FieldID,
                 FieldDesc AS FieldLabel,
                 FieldName,
                 99990 AS DisplayOrder
          FROM   Workflow_FieldDict
          WHERE  BuiltInFlag = 1
                 AND '{1}'='0') AS aa
ORDER BY DisplayOrder;
", DNTRequest.GetString("opid"), GroupID);
            DataTable dt = DbHelper.GetInstance().ExecDataTable(sql);
            lbFieldList.DataValueField = "FieldID";
            lbFieldList.DataTextField = "FieldLabel";
            lbFieldList.DataBind();
        }
    }
}
