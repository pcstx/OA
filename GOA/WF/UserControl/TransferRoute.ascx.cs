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

namespace GOA.UserControl
{
    public partial class TransferRoute : System.Web.UI.UserControl
    {
        public int RequestID;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                txtRequestID.Value = RequestID.ToString();
                ViewState["PageSize"] = 3;
                BindGridView();
            }
        }

        //设置每页显示记录数，无须更改；如果要更改默认第页记录数，到config/geneal.config中更改 PageSize
        protected void txtPageSize_TextChanged(object sender, EventArgs e)
        {
            if (txtPageSize.Text == "" || Convert.ToInt32(txtPageSize.Text) == 0)
            {
                ViewState["PageSize"] = 3;//每页显示的默认值
            }
            else
            {
                ViewState["PageSize"] = Convert.ToInt32(txtPageSize.Text);
            }
            BindGridView();
        }

        #region gridview 绑定
        //此类需要更改，主要是更改获取数据源的方法

        private void BindGridView()
        {
            GridView1.PageSize = (int)ViewState["PageSize"];
            DataTable dt = DbHelper.GetInstance().GetDBRecords("a.ID,a.NodeID,b.NodeName,OperatorName=case when a.AgentID=0 then c.UserName else c.UserName+'→'+d.UserName end,a.OperateDateTime,OperateTypeN=case when a.OperateType=1 then '提交' else '退回' end,a.OperateComment,a.ReceivList,ReceivListN=''", "Workflow_RequestLog a left join Workflow_FlowNode b on (a.NodeID=b.NodeID and a.WorkflowID=b.WorkflowID) left join UserList c on a.OperatorID=c.UserSerialID left join UserList d on a.AgentID=d.UserSerialID", "a.RequestID=" + txtRequestID.Value, "a.ID DESC");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string ReceivList = dt.Rows[i]["ReceivList"].ToString();
                string ReceivListN = "";
                DataTable dtReceivList = DbHelper.GetInstance().GetDBRecords("UserName", "UserList", "UserSerialID in (" + ReceivList + ")", "");
                for (int j = 0; j < dtReceivList.Rows.Count; j++)
                {
                    ReceivListN += dtReceivList.Rows[j]["UserName"].ToString() + ",";
                }
                dt.Rows[i]["ReceivListN"] = ReceivListN.Trim(new char[] { ',' });
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
        //此类要进行dorpdownlist/chk控件的转换
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
        }
        #endregion

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            BindGridView();
        }
    }
}