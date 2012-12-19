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
    public partial class GG500202 : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                DataTable dtOperatorType = DbHelper.GetInstance().GetDBRecords("TypeCode,TypeName", "Workflow_OperatorType", "1=1", "DisplayOrder");
                rblOperatorType.DataSource = dtOperatorType;
                rblOperatorType.DataValueField = "TypeCode";
                rblOperatorType.DataTextField = "TypeName";
                rblOperatorType.SelectedIndex = 0;
                rblOperatorType.DataBind();
                BindGridView();
                ReDisplayOperatorTypeDetail();
                ReDisplayOperatorContents();
            }
        }

        #region gridview 绑定
        //此类需要更改，主要是更改获取数据源的方法
        private void BindGridView()
        {
            string NodeID = DNTRequest.GetString("id");
            DataTable dt = DbHelper.GetInstance().sp_GetNodeOperatorDetail(NodeID);
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
                DbHelper.GetInstance().DeleteWorkflow_NodeOperatorDetail(RuleID);

                string NodeID = DNTRequest.GetString("id");
                Workflow_FlowNodeEntity _FlowNodeEntity = DbHelper.GetInstance().GetWorkflow_FlowNodeEntityByKeyCol(NodeID);
                DbHelper.GetInstance().sp_GeneratorWorkflowCreatorList(_FlowNodeEntity.WorkflowID.ToString());
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

        protected void rblOperatorType_SelectedIndexChanged(object sender, EventArgs e)
        {
            string NodeID = DNTRequest.GetString("id");
            ReDisplayOperatorTypeDetail();
            ReDisplayOperatorContents();
            BindGridView();
        }

        protected void ddlOperatorTypeDetail_SelectedIndexChanged(object sender, EventArgs e)
        {
            ImgObjectValue.OnClientClick = string.Format("return btnObjectValueClick('{0}')", ddlOperatorTypeDetail.SelectedValue);
            divCtrlObject3.Visible = "25,45".Contains(ddlOperatorTypeDetail.SelectedValue);
            BindGridView();
        }

        private void ReDisplayOperatorTypeDetail()
        {
            string TypeCode = rblOperatorType.SelectedValue;
            DataTable dtTypeDetail = DbHelper.GetInstance().GetDBRecords("TypeDetailCode,DetailTypeName", "Workflow_OperatorTypeDetail", "TypeCode='" + TypeCode + "'", "TypeDetailCode");
            ddlOperatorTypeDetail.AddTableData(dtTypeDetail, true, "Null");
        }

        private void ReDisplayOperatorContents()
        {
            string NodeID = DNTRequest.GetString("id");
            divCtrlObject1.Visible = rblOperatorType.SelectedValue == "10";
            divCtrlObject2.Visible = rblOperatorType.SelectedValue == "20" || rblOperatorType.SelectedValue == "30" || rblOperatorType.SelectedValue == "50";
            divCtrlObject3.Visible = false;
            if (rblOperatorType.SelectedValue == "20")
            {
                DataTable dtObjectList = DbHelper.GetInstance().GetDBRecords("c.FieldID,c.FieldLabel,c.DisplayOrder", "Workflow_FlowNode a,Workflow_Base b,Workflow_FormField c,Workflow_FieldDict d", "a.WorkflowID=b.WorkflowID and b.FormID=c.FormID and c.FieldID=d.FieldID and d.HTMLTypeID=8 and d.BrowseType=3 and a.NodeID=" + NodeID, "c.DisplayOrder");
                ddlObjectValue.AddTableData(dtObjectList, 0, 1, true, "Select");
            }
            else if (rblOperatorType.SelectedValue == "30")
            {
                DataTable dtObjectList = DbHelper.GetInstance().GetDBRecords("c.FieldID,c.FieldLabel,c.DisplayOrder", "Workflow_FlowNode a,Workflow_Base b,Workflow_FormField c,Workflow_FieldDict d", "a.WorkflowID=b.WorkflowID and b.FormID=c.FormID and c.FieldID=d.FieldID and d.HTMLTypeID=8 and d.BrowseType=2 and a.NodeID=" + NodeID, "c.DisplayOrder");
                ddlObjectValue.AddTableData(dtObjectList, 0, 1, true, "Select");
            }
            else if (rblOperatorType.SelectedValue == "50")
            {
                DataTable dtObjectList = DbHelper.GetInstance().GetDBRecords("a.NodeID,a.NodeName", "Workflow_FlowNode a,Workflow_FlowNode b", "a.WorkflowID=b.WorkflowID and b.NodeID=" + NodeID + " and a.NodeID != " + NodeID, "NodeID");
                ddlObjectValue.AddTableData(dtObjectList, 0, 1, true, "Select");
            }   
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Workflow_NodeOperatorDetailEntity _NodeOperatorDetailEntity = new Workflow_NodeOperatorDetailEntity();
            _NodeOperatorDetailEntity.RuleID = 0;
            _NodeOperatorDetailEntity.NodeID = DNTRequest.GetInt("id", 0);
            _NodeOperatorDetailEntity.RuleType = Convert.ToInt32(ddlOperatorTypeDetail.SelectedValue);
            _NodeOperatorDetailEntity.RuleCondition = "(1=1)";
            if (rblOperatorType.SelectedValue == "10")
            {
                _NodeOperatorDetailEntity.ObjectValue = txtObjectValue.Value;
                _NodeOperatorDetailEntity.RuleName = txtObjectValueN.Text;
            }
            else if (rblOperatorType.SelectedValue == "20" || rblOperatorType.SelectedValue == "30" || rblOperatorType.SelectedValue == "50")
            {
                _NodeOperatorDetailEntity.ObjectValue = ddlObjectValue.SelectedValue;
                _NodeOperatorDetailEntity.RuleName = ddlObjectValue.SelectedItem.Text;
            }
            else if (rblOperatorType.SelectedValue == "40")
            {
                _NodeOperatorDetailEntity.ObjectValue = "";
                _NodeOperatorDetailEntity.RuleName = ddlOperatorTypeDetail.SelectedItem.Text;
            }
            _NodeOperatorDetailEntity.RuleSeq = txtRuleSeq.Text != "" ? Convert.ToInt32(txtRuleSeq.Text) : 10;
            _NodeOperatorDetailEntity.SecurityStart = 0;
            _NodeOperatorDetailEntity.SecurityEnd = 100;
            _NodeOperatorDetailEntity.LevelStart = txtLevelS.Text != "" ? Convert.ToInt32(txtLevelS.Text) : 5;
            _NodeOperatorDetailEntity.LevelEnd = txtLevelE.Text != "" ? Convert.ToInt32(txtLevelE.Text) : 1;
            _NodeOperatorDetailEntity.SignType = chkSignType.Checked ? 1 : 0;
            DbHelper.GetInstance().AddWorkflow_NodeOperatorDetail(_NodeOperatorDetailEntity);

            string NodeID = DNTRequest.GetString("id");
            Workflow_FlowNodeEntity _FlowNodeEntity = DbHelper.GetInstance().GetWorkflow_FlowNodeEntityByKeyCol(NodeID);
            DbHelper.GetInstance().sp_GeneratorWorkflowCreatorList(_FlowNodeEntity.WorkflowID.ToString());

            BindGridView();
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                string Prefix = "GridView1$ctl" + (i + 2).ToString().PadLeft(2, '0') + "$";

                string RuleID = GridView1.DataKeys[i][0].ToString();
                Workflow_NodeOperatorDetailEntity _NodeOperatorDetailEntity = DbHelper.GetInstance().GetWorkflow_NodeOperatorDetailEntityByKeyCol(RuleID);
                _NodeOperatorDetailEntity.RuleSeq = DNTRequest.GetInt(Prefix + "RuleSeq", 9990);
                DbHelper.GetInstance().UpdateWorkflow_NodeOperatorDetail(_NodeOperatorDetailEntity);
            }

            string NodeID = DNTRequest.GetString("id");
            Workflow_FlowNodeEntity _FlowNodeEntity = DbHelper.GetInstance().GetWorkflow_FlowNodeEntityByKeyCol(NodeID);
            DbHelper.GetInstance().sp_GeneratorWorkflowCreatorList(_FlowNodeEntity.WorkflowID.ToString());

            BindGridView();
            ScriptManager.RegisterStartupScript(btnAdd, this.GetType(), "add", "alert('设定成功');", true);
        }
        #endregion
    }
}
