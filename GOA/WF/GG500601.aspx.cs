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
    public partial class GG500601 : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                ViewState["selectedLines"] = new ArrayList();
                DataTable dtDBCaculateType = DbHelper.GetInstance().GetDBRecords("CaculateTypeID,CaculateTypeName", "Workflow_DBCaculateType", "Useflag=1", "DisplayOrder");
                ddlCaculateType.AddTableData(dtDBCaculateType, 0, 1, true, "Null");

                DataTable dtGroupID = DbHelper.GetInstance().GetDBRecords("d.GroupID,d.GroupName", "Workflow_FlowNode a,Workflow_Base b,Workflow_FormBase c,Workflow_FormFieldGroup d", "a.WorkflowID=b.WorkflowID and b.FormID=c.FormID and c.FormID=d.FormID and a.NodeID=" + DNTRequest.GetString("id"), "d.DisplayOrder");
                DataRow drGroup = dtGroupID.NewRow();
                drGroup["GroupID"] = 0;
                drGroup["GroupName"] = "主字段";
                dtGroupID.Rows.InsertAt(drGroup, 0);
                ddlGroupID.AddTableData(dtGroupID, 0, 1, true, "Null");

                DataTable dtOPCycleType = DbHelper.GetInstance().GetDBRecords("CycleTypeID,CycleTypeName", "Workflow_OperationCycleType", "Useflag=1", "DisplayOrder");
                ddlOPCycleType.AddTableData(dtOPCycleType, 0, 1, false, "Null");
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
            DataTable dt = DbHelper.GetInstance().sp_GetNodeAddInOperation_Type1(arlParams);
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
                LinkButton btnSelect = (LinkButton)e.Row.FindControl("btnSelect");
                LinkButton btnSelect2 = (LinkButton)e.Row.FindControl("btnSelect2");
                LinkButton btnSelect3 = (LinkButton)e.Row.FindControl("btnSelect3");
                LinkButton btnSelect4 = (LinkButton)e.Row.FindControl("btnSelect4");
                string GroupID = DataBinder.Eval(e.Row.DataItem, "GroupID").ToString();
                string CaculateType = DataBinder.Eval(e.Row.DataItem, "CaculateType").ToString();

                btnSelect.Visible = GroupID != "0";
                btnSelect2.Visible = CaculateType != "2" && CaculateType != "3";
                btnSelect3.Visible = CaculateType != "3";
                btnSelect4.Visible = CaculateType == "3";
            }
        }
        #endregion

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Workflow_NodeAddInOperation_Type1Entity _Type1Entity = new Workflow_NodeAddInOperation_Type1Entity();
            _Type1Entity.NodeID = DNTRequest.GetInt("id", 0);
            _Type1Entity.CaculateType = Convert.ToInt32(ddlCaculateType.SelectedValue);
            _Type1Entity.DataSourceID = Convert.ToInt32(txtDataSourceID.Value);
            _Type1Entity.DataSourceTable = ddlDataSourceTable.SelectedValue;
            _Type1Entity.GroupID = Convert.ToInt32(ddlGroupID.SelectedValue);
            _Type1Entity.SelectRange = "(1=1)";
            _Type1Entity.OPCycleType = Convert.ToInt32(ddlOPCycleType.SelectedValue);
            _Type1Entity.OPTime = DNTRequest.GetInt("t", 0);
            _Type1Entity.OPCondition = "(1=1)";
            _Type1Entity.CreateSID = userEntity.UserSerialID;
            _Type1Entity.CreateDate = DateTime.Now;
            string AddInOPID = DbHelper.GetInstance().AddWorkflow_NodeAddInOperation_Type1(_Type1Entity);

            if (ddlCaculateType.SelectedValue == "3")
            {
                DataTable dtSpParameter = DbHelper.GetInstance().GetAllStoredProcedureParameters(_Type1Entity.DataSourceID, _Type1Entity.DataSourceTable);
                for (int i = 0; i < dtSpParameter.Rows.Count; i++)
                {
                    if ("ReturnValue" == dtSpParameter.Rows[i]["ParameterDirection"].ToString())
                    {
                        continue;
                    }
                    Workflow_NodeAddInOperation_Type1_SpParameterEntity _Type1_SpParameterEntity = new Workflow_NodeAddInOperation_Type1_SpParameterEntity();
                    _Type1_SpParameterEntity.AddInOPID = Int32.Parse(AddInOPID);
                    _Type1_SpParameterEntity.SpParameter = dtSpParameter.Rows[i]["ParameterName"].ToString();
                    _Type1_SpParameterEntity.ParameterType = dtSpParameter.Rows[i]["ParameterType"].ToString();
                    _Type1_SpParameterEntity.ParameterSize = Convert.ToInt32(dtSpParameter.Rows[i]["ParameterSize"]);
                    _Type1_SpParameterEntity.ParameterDirection = dtSpParameter.Rows[i]["ParameterDirection"].ToString();
                    _Type1_SpParameterEntity.TartgetValue = "";
                    _Type1_SpParameterEntity.FieldTypeID = 0;
                    DbHelper.GetInstance().AddWorkflow_NodeAddInOperation_Type1_SpParameter(_Type1_SpParameterEntity);
                }
            }
            BindGridView();
            System.Web.UI.ScriptManager.RegisterStartupScript(btnAdd, this.GetType(), "ButtonHideScript", strButtonHideScript, false);
        }

        protected void btnDel_Click(object sender, EventArgs e)
        {
            CollectSelected();
            ArrayList selectedLines = (ArrayList)ViewState["selectedLines"];
            for (int i = 0; i < selectedLines.Count; i++)
            {
                DbHelper.GetInstance().DeleteWorkflow_NodeAddInOperation_Type1(selectedLines[i].ToString(), userEntity.UserSerialID.ToString());
            }
            ViewState["selectedLines"] = new ArrayList();
            BindGridView();
            System.Web.UI.ScriptManager.RegisterStartupScript(btnDel, this.GetType(), "ButtonHideScript", strButtonHideScript, false);
        }

        protected void ddlCaculateType_SelectedIndexChanged(object sender, EventArgs e)
        {
            ReDisplayDataSourceTable();
            BindGridView();
        }

        protected void txtDataSource_TextChanged(object sender, EventArgs e)
        {
            ReDisplayDataSourceTable();
            BindGridView();
        }
        private void ReDisplayDataSourceTable()
        {
            if (txtDataSourceID.Value != "")
            {
                int DataSourceID = Int32.Parse(txtDataSourceID.Value);
                if ("0,1,2".Contains(ddlCaculateType.SelectedValue))
                {
                    DataTable dtTableList = DbHelper.GetInstance().GetAllTableNames(DataSourceID);
                    ddlDataSourceTable.AddTableData(dtTableList, 0, 0, true, "Null");
                }
                else if ("3" == ddlCaculateType.SelectedValue)
                {
                    DataTable dtSpList = DbHelper.GetInstance().GetAllStoredProcedureNames(DataSourceID);
                    ddlDataSourceTable.AddTableData(dtSpList, 0, 0, true, "Null");
                }
                else
                    ddlDataSourceTable.Items.Clear();
            }
        }

        protected void ddlGroupID_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlGroupID.SelectedValue == "0")
            {
                ddlOPCycleType.SelectedIndex = 0;
                ddlOPCycleType.Enabled = false;
            }
            else
            {
                ddlOPCycleType.Enabled = true;
            }
            BindGridView();
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
    }
}
