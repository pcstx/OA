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
    public partial class GG5002 : BasePage
    {
        private static string strOperationState;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                DataTable dtNodeType = DbHelper.GetInstance().GetDBRecords("*", "Workflow_NodeType", "1=1", "DisplayOrder");
                ddlNodeTypeID.AddTableData(dtNodeType, 0, 1, true, "Select");
                txtOverTimeLen.Text = "0";
                ViewState["selectedLines"] = new ArrayList();
                AspNetPager1.PageSize = config.PageSize;
                BindGridView();
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
            string Tables = @"
Workflow_FlowNode a left join Workflow_NodeType b on a.NodeTypeID=b.NodeTypeID
";
            string WhereCondition = "a.WorkflowID="+DNTRequest.GetString("id");
            DataTable dt = DbHelper.GetInstance().GetDBRecords("a.*,NodeTypeN=b.NodeTypeName", Tables, WhereCondition, "a.DisplayOrder", AspNetPager1.PageSize, AspNetPager1.CurrentPageIndex);
            if (dt.Rows.Count > 0)
                AspNetPager1.RecordCount = Convert.ToInt32(dt.Rows[0]["RecordCount"]);
            else
                AspNetPager1.RecordCount = 0;

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
                string NodeID = GridView1.DataKeys[index][0].ToString().Trim();
                Workflow_FlowNodeEntity _Workflow_FlowNodeEntity = DbHelper.GetInstance().GetWorkflow_FlowNodeEntityByKeyCol(NodeID);
                txtNodeID.Value = _Workflow_FlowNodeEntity.NodeID.ToString();
                txtNodeName.Text = _Workflow_FlowNodeEntity.NodeName;
                txtNodeDesc.Text = _Workflow_FlowNodeEntity.NodeDesc;
                ddlNodeTypeID.SelectedValue = _Workflow_FlowNodeEntity.NodeTypeID.ToString();
                chkIsOverTime.Checked = _Workflow_FlowNodeEntity.IsOverTime == 1;
                txtOverTimeLen.Text = _Workflow_FlowNodeEntity.OverTimeLen.ToString();
                chkSignType.Checked = _Workflow_FlowNodeEntity.SignType == 1;
                txtDisplayOrder.Text = _Workflow_FlowNodeEntity.DisplayOrder.ToString();
            }
        }
        //此类要进行dorpdownlist/chk控件的转换
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string KeyCol = ((DataRowView)e.Row.DataItem).Row["NodeID"].ToString();
                CheckBox cb = (System.Web.UI.WebControls.CheckBox)e.Row.FindControl("Item") as CheckBox;
                ArrayList selectedLines = (ArrayList)ViewState["selectedLines"];
                if (selectedLines.Contains(KeyCol))
                {
                    cb.Checked = true;
                }
            }
        }
        #endregion

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
                    }
                }
                CollectSelected();
                BindGridView();
            }
            System.Web.UI.ScriptManager.RegisterStartupScript(btn, this.GetType(), "ButtonHideScript", strButtonHideScript, false);
        }

        private string SaveData()
        {
            Workflow_FlowNodeEntity _Workflow_FlowNodeEntity = new Workflow_FlowNodeEntity();
            _Workflow_FlowNodeEntity.NodeID = txtNodeID.Value != string.Empty ? Convert.ToInt32(txtNodeID.Value) : 0;
            _Workflow_FlowNodeEntity.NodeName = txtNodeName.Text;
            _Workflow_FlowNodeEntity.NodeDesc = txtNodeDesc.Text;
            _Workflow_FlowNodeEntity.WorkflowID = DNTRequest.GetInt("id", 0);
            _Workflow_FlowNodeEntity.NodeTypeID = Convert.ToInt32(ddlNodeTypeID.SelectedValue);
            _Workflow_FlowNodeEntity.DisplayOrder = Convert.ToInt32(txtDisplayOrder.Text);
            _Workflow_FlowNodeEntity.IsOverTime = chkIsOverTime.Checked ? 1 : 0;
            _Workflow_FlowNodeEntity.OverTimeLen = txtOverTimeLen.Text == "" ? (24 * 7).ToString() : txtOverTimeLen.Text;
            _Workflow_FlowNodeEntity.SignType = chkSignType.Checked ? 1 : 0;

            string sResult = "-1";
            if (strOperationState == "Add")
                sResult = DbHelper.GetInstance().AddWorkflow_FlowNode(_Workflow_FlowNodeEntity);
            else if (strOperationState == "Update")
                sResult = DbHelper.GetInstance().UpdateWorkflow_FlowNode(_Workflow_FlowNodeEntity);
            return sResult;
        }

        protected void btnDel_Click(object sender, EventArgs e)
        {
            CollectSelected();
            ArrayList selectedLines = (ArrayList)ViewState["selectedLines"];
            for (int i = 0; i < selectedLines.Count; i++)
            {
                DbHelper.GetInstance().DeleteWorkflow_FlowNode(selectedLines[i].ToString());
            }
            BindGridView();
            ViewState["selectedLines"] = new ArrayList();
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

        protected void chkIsOverTime_CheckedChanged(object sender, EventArgs e)
        {
            if (chkIsOverTime.Checked)
            {
                txtOverTimeLen.Text = txtOverTimeLen.Text != "" || txtOverTimeLen.Text == "0" ? (24 * 7).ToString() : txtOverTimeLen.Text;
                txtOverTimeLen.Attributes.Remove("readonly");
            }
            else
            {
                txtOverTimeLen.AddAttributes("readonly", "true");
                txtOverTimeLen.Text = "0";
            }
            BindGridView();
        }

        [WebMethod]
        public static string SetAddViewState()
        {
            strOperationState = "Add";
            return "";
        }
    }
}
