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
    public partial class GG5003 : BasePage
    {
        private static string strOperationState;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                string WorkflowID = DNTRequest.GetString("id");
                DataTable dtNodeList = DbHelper.GetInstance().GetDBRecords("*", "Workflow_FlowNode", "WorkflowID=" + WorkflowID, "DisplayOrder");
                ddlStartNodeID.AddTableData(dtNodeList, 0, 1, true, "Select");
                ddlTargetNodeID.AddTableData(dtNodeList, 0, 1, true, "Select");

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
Workflow_NodeLink a left join Workflow_FlowNode b on a.StartNodeID=b.NodeID left join Workflow_FlowNode c on a.TargetNodeID=c.NodeID
";
            string WhereCondition = "a.WorkflowID=" + DNTRequest.GetString("id");
            DataTable dt = DbHelper.GetInstance().GetDBRecords("a.*,StartNodeName=b.NodeName,TargetNodeName=c.NodeName", Tables, WhereCondition, "a.StartNodeID,a.LinkID", AspNetPager1.PageSize, AspNetPager1.CurrentPageIndex);
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
                string LinkID = GridView1.DataKeys[index][0].ToString().Trim();
                Workflow_NodeLinkEntity _Workflow_NodeLinkEntity = DbHelper.GetInstance().GetWorkflow_NodeLinkEntityByKeyCol(LinkID);
                txtLinkID.Value = _Workflow_NodeLinkEntity.LinkID.ToString();
                txtLinkName.Text = _Workflow_NodeLinkEntity.LinkName;
                ddlStartNodeID.SelectedValue = _Workflow_NodeLinkEntity.StartNodeID.ToString();
                ddlTargetNodeID.SelectedValue = _Workflow_NodeLinkEntity.TargetNodeID.ToString();
                chkIsRejected.Checked = _Workflow_NodeLinkEntity.IsRejected == 1;
            }
        }
        //此类要进行dorpdownlist/chk控件的转换
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string KeyCol = ((DataRowView)e.Row.DataItem).Row["LinkID"].ToString();
                CheckBox cb = (System.Web.UI.WebControls.CheckBox)e.Row.FindControl("Item") as CheckBox;
                ArrayList selectedLines = (ArrayList)ViewState["selectedLines"];
                if (selectedLines.Contains(KeyCol))
                {
                    cb.Checked = true;
                }

                string IsRejected = ((DataRowView)e.Row.DataItem).Row["IsRejected"].ToString();
                System.Web.UI.WebControls.CheckBox chkIsRejected = (System.Web.UI.WebControls.CheckBox)e.Row.FindControl("chkIsRejected");
                chkIsRejected.Checked = IsRejected.Equals("1");
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
            Workflow_NodeLinkEntity _Workflow_NodeLinkEntity = new Workflow_NodeLinkEntity();
            _Workflow_NodeLinkEntity.LinkID = txtLinkID.Value != string.Empty ? Convert.ToInt32(txtLinkID.Value) : 0;
            _Workflow_NodeLinkEntity.LinkName = txtLinkName.Text;
            _Workflow_NodeLinkEntity.WorkflowID = DNTRequest.GetInt("id", 0);
            _Workflow_NodeLinkEntity.IsRejected = chkIsRejected.Checked ? 1 : 0;
            _Workflow_NodeLinkEntity.StartNodeID = Convert.ToInt32(ddlStartNodeID.SelectedValue);
            _Workflow_NodeLinkEntity.TargetNodeID = Convert.ToInt32(ddlTargetNodeID.SelectedValue);
            _Workflow_NodeLinkEntity.Creator = userEntity.UserID;
            _Workflow_NodeLinkEntity.CreateDate = DateTime.Now;
            _Workflow_NodeLinkEntity.lastModifier = userEntity.UserID;
            _Workflow_NodeLinkEntity.lastModifyDate = DateTime.Now;

            string sResult = "-1";
            if (strOperationState == "Add")
                sResult = DbHelper.GetInstance().AddWorkflow_NodeLink(_Workflow_NodeLinkEntity);
            else if (strOperationState == "Update")
                sResult = DbHelper.GetInstance().UpdateWorkflow_NodeLink(_Workflow_NodeLinkEntity);
            return sResult;
        }

        protected void btnDel_Click(object sender, EventArgs e)
        {
            CollectSelected();
            ArrayList selectedLines = (ArrayList)ViewState["selectedLines"];
            for (int i = 0; i < selectedLines.Count; i++)
            {
                DbHelper.GetInstance().DeleteWorkflow_NodeLink(selectedLines[i].ToString());
            }
            ViewState["selectedLines"] = new ArrayList();
            BindGridView();
            System.Web.UI.ScriptManager.RegisterStartupScript(btnDel, this.GetType(), "ButtonHideScript", strButtonHideScript, false);
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
