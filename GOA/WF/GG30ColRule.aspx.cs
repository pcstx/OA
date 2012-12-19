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
    public partial class GG30ColRule : BasePage
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
                DataTable dtMailField = DbHelper.GetInstance().GetDBRecords("a.FieldID,a.FieldLabel", "Workflow_FormField a", "a.IsDetail=0 and a.FormID=" + DNTRequest.GetString("fid"), "a.DisplayOrder");
                ddlFieldIDTo.AddTableData(dtMailField, 0, 1, true, "Select");

                DataTable dtDetailField = DbHelper.GetInstance().GetDBRecords("a.FieldID,a.FieldLabel", "Workflow_FormField a", string.Format("a.GroupID={0} and a.FormID={1}", DNTRequest.GetString("gid"), DNTRequest.GetString("fid")), "a.DisplayOrder");
                lbFieldList.DataSource = dtDetailField;
                lbFieldList.DataValueField = "FieldID";
                lbFieldList.DataTextField = "FieldLabel";
                lbFieldList.DataBind();

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
            string WhereCondion = string.Format("a.FormID=b.FormID and b.GroupID=0 and a.FieldIDTo=b.FieldID and RuleType=2 and a.FormID={0} and a.GroupID={1}", DNTRequest.GetString("fid"), DNTRequest.GetString("gid"));
            DataTable dt = DbHelper.GetInstance().GetDBRecords("a.RuleID,a.FormID,a.GroupID,a.FieldIDTo,FieldIDToN=b.FieldLabel,b.FieldLabel,a.RuleDetail,RuleDetailN='',a.RuleType", "Workflow_FormDetailRule a,Workflow_FormField b", WhereCondion, "a.RuleID");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dt.Rows[i]["RuleDetailN"] = GetRuleDetail(dt.Rows[i]["RuleID"].ToString())[1];
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
                string RuleID = GridView1.DataKeys[index][0].ToString().Trim();
                DbHelper.GetInstance().UpdateWorkflow_FormDetailRule4Calculate(RuleID);
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
                DbHelper.GetInstance().ExecSqlText(string.Format(" select {0} from Workflow_FormDetail where RequestID=0", RuleDetail[0]));
            }
            catch
            {
                ScriptManager.RegisterStartupScript(btnAdd, this.GetType(), "add", "alert('赋值表达式不正确,无法添加');", true);
                return;
            }


            Workflow_FormDetailRuleEntity _FormDetailRuleEntity = new Workflow_FormDetailRuleEntity();
            _FormDetailRuleEntity.FormID = DNTRequest.GetInt("fid", 0);
            _FormDetailRuleEntity.GroupID = DNTRequest.GetInt("gid", 0);
            _FormDetailRuleEntity.RuleType = 2;
            _FormDetailRuleEntity.FiledIDTo = Convert.ToInt32(ddlFieldIDTo.SelectedValue);
            _FormDetailRuleEntity.RuleDetail = RuleDetail[0];
            DataTable dtComputeRoute = (DataTable)ViewState["dtComputeRoute"];
            DbHelper.GetInstance().AddWorkflow_FormDetailRule(_FormDetailRuleEntity, dtComputeRoute);
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
                    string RuleID = GridView1.DataKeys[i][0].ToString().Trim();
                    DbHelper.GetInstance().DeleteWorkflow_FormDetailRule(RuleID);
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

        private string[] GetRuleDetail(string RuleID)
        {
            string[] RuleDetail = new string[2] { "", "" };
            DataTable dt = DbHelper.GetInstance().GetDBRecords("*", "Workflow_ComputeRoute", "RuleID=" + RuleID, "RouteID,RouteOrder");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (Convert.ToInt32(dt.Rows[i]["ComputeType"]) == 1)
                {
                    DataTable dtDetail = DbHelper.GetInstance().GetDBRecords("b.ComputeSymbol", "Workflow_ComputeRoute a,Workflow_ComputeSymbol b", string.Format("a.RouteValue=b.ComputeID and a.RouteID={0}", dt.Rows[i]["RouteID"]), "");
                    RuleDetail[0] = RuleDetail[0] + dtDetail.Rows[0]["ComputeSymbol"].ToString();
                    RuleDetail[1] = RuleDetail[1] + dtDetail.Rows[0]["ComputeSymbol"].ToString();
                }
                else if (Convert.ToInt32(dt.Rows[i]["ComputeType"]) == 2)
                {
                    DataTable dtDetail = DbHelper.GetInstance().GetDBRecords("c.FieldName,b.FieldLabel", "Workflow_ComputeRoute a,Workflow_FormField b,Workflow_FieldDict c", string.Format("a.RouteValue=b.FieldID and b.FieldID=c.FieldID and a.RouteID={0} and b.FormID={1} and b.GroupID={2}", dt.Rows[i]["RouteID"], DNTRequest.GetString("fid"), DNTRequest.GetString("gid")), "");
                    RuleDetail[0] = RuleDetail[0] + dtDetail.Rows[0]["FieldName"].ToString();
                    RuleDetail[1] = RuleDetail[1] + dtDetail.Rows[0]["FieldLabel"].ToString();
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
                    DataTable dtDetail = DbHelper.GetInstance().GetDBRecords("c.FieldName,b.FieldLabel", "Workflow_FormField b,Workflow_FieldDict c", string.Format("b.FieldID={0} and b.FieldID=c.FieldID and b.FormID={1} and b.GroupID={2}", dtComputeRoute.Rows[i]["RouteValue"], DNTRequest.GetString("fid"), DNTRequest.GetString("gid")), "");
                    RuleDetail[0] = RuleDetail[0] + dtDetail.Rows[0]["FieldName"].ToString();
                    RuleDetail[1] = RuleDetail[1] + dtDetail.Rows[0]["FieldLabel"].ToString();
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
    }
}
