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
    public partial class GG5004 : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BindGridView();
            }
        }

        #region gridview 绑定
        //此类需要更改，主要是更改获取数据源的方法
        private void BindGridView()
        {
            DataTable dt = DbHelper.GetInstance().GetDBRecords("a.NodeID,a.NodeName,a.NodeDesc,a.WorkflowID,a.NodeTypeID,NodeTypeN=b.NodeTypeName,a.WithdrawTypeID,a.ArchiveFlag,c.WithdrawTypeName", "Workflow_FlowNode a,Workflow_NodeType b,Workflow_WithdrawType c", "a.NodeTypeID=b.NodeTypeID and a.WithdrawTypeID=c.WithdrawTypeID and a.WorkflowID=" + DNTRequest.GetString("id"), "a.DisplayOrder");
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
        }
        //此类要进行dorpdownlist/chk控件的转换
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string ArchiveFlag = ((DataRowView)e.Row.DataItem).Row["ArchiveFlag"].ToString();
                System.Web.UI.WebControls.CheckBox chkArchiveFlag = (System.Web.UI.WebControls.CheckBox)e.Row.FindControl("ArchiveFlag");
                chkArchiveFlag.Checked = ArchiveFlag.Equals("1");

                System.Web.UI.WebControls.DropDownList ddlWithdrawTypeID = (System.Web.UI.WebControls.DropDownList)e.Row.FindControl("WithdrawTypeID");
                ddlWithdrawTypeID.SelectedValue = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "WithdrawTypeID"));
            }
        }
        #endregion

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            Workflow_FlowNodeEntity _FlowNodeEntity = new Workflow_FlowNodeEntity();
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                string Prefix = "GridView1$ctl" + (i + 2).ToString().PadLeft(2, '0') + "$";
                _FlowNodeEntity.NodeID = Convert.ToInt32(GridView1.DataKeys[i][0]);
                _FlowNodeEntity.WithdrawTypeID = DNTRequest.GetString(Prefix + "WithdrawTypeID");
                _FlowNodeEntity.ArchiveFlag = DNTRequest.GetString(Prefix + "ArchiveFlag") == "on" ? 1 : 0;
                DbHelper.GetInstance().UpdateWorkflow_FlowNode2(_FlowNodeEntity);
            }
            ScriptManager.RegisterStartupScript(btnSubmit, this.GetType(), "add", "alert('设置成功');", true);
            BindGridView();
        }

        public DataTable dtWithdrawType()
        {
            DataTable dt = DbHelper.GetInstance().GetDBRecords("WithdrawTypeID,WithdrawTypeName", "Workflow_WithdrawType", "Useflag=1", "DisplayOrder");
            return dt;
        }
    }
}
