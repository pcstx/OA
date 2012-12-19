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
    public partial class MW60 : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SetControlValue();
                AspNetPager1.PageSize = config.PageSize;
                ViewState["selectedLines"] = new ArrayList();
                BindGridView();

            }
        }

        private void SetControlValue()
        {
            DataTable dtNodeType = DbHelper.GetInstance().GetDBRecords("*", "Workflow_NodeType", "1=1", "DisplayOrder");
            ddlNodeTypeID.AddTableData(dtNodeType, 0, 1, true, "Select");

            DataTable dtFormType = DbHelper.GetInstance().GetDBRecords("*", "Workflow_FormType", "1=1", "DisplayOrder");
            ddlFormType.AddTableData(dtFormType, 0, 1, true, "Select");
        }

        private void BindGridView()
        {
            DataTable dt = new DataTable();

            dt = DbHelper.GetInstance().GetWorkflowMonitorSearchResult(GetArrayListParameter(), AspNetPager1.PageSize, AspNetPager1.CurrentPageIndex);

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

        protected void btnQuery_Click(object sender, EventArgs e)
        {
            CollectSelected();
            BindGridView();
        }

        protected void btnDel_Click(object sender, EventArgs e)
        {
            CollectSelected();
            ArrayList selectedLines = (ArrayList)ViewState["selectedLines"];
            for (int i = 0; i < selectedLines.Count; i++)
            {
                //此处需修改，在SqlDBBasic中添加Delete function
                //  DbHelper.GetInstance().DeleteWorkflow_ (Convert.ToInt32(selectedLines[i].ToString()));
            }
            BindGridView();
            ViewState["selectedLines"] = new ArrayList();
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
            //再进行绑定一次

            CollectSelected();
            BindGridView();
        }

        #region gridView 事件
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
                string keyCol = GridView1.DataKeys[index].Value.ToString();
                //第二处待修改位置

            }
        }
        //此类要进行dorpdownlist/chk控件的转换


        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    string KeyCol = GridView1.DataKeys[e.Row.RowIndex].Value.ToString();
                    CheckBox cb = (System.Web.UI.WebControls.CheckBox)e.Row.FindControl("Item") as CheckBox;
                    ArrayList selectedLines = (ArrayList)ViewState["selectedLines"];
                    if (selectedLines.Contains(KeyCol))
                    {
                        cb.Checked = true;
                    }
                }
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

        private ArrayList GetArrayListParameter()
        {

            ArrayList al = new ArrayList();

            //表单类型 --待办、已办、办结、我的请求（已完成、未完成）用到

            if (ddlFormType.SelectedValue == "0")
                al.Add(System.DBNull.Value);
            else
                al.Add(Int32.Parse(ddlFormType.SelectedValue));

            al.Add(userEntity.UserSerialID);//用户序号

            //流程ID--可多选

            if (txtWorkflowID.Value == "")
                al.Add(System.DBNull.Value);
            else
                al.Add((txtWorkflowID.Value));

            //--操作节点类型（Create,Approve,Realize,Process）

            if (ddlNodeTypeID.SelectedValue == "0")
                al.Add(System.DBNull.Value);
            else
                al.Add(Int32.Parse(ddlNodeTypeID.SelectedValue));

            //--开始创建日期

            if (txtStartDate.Text == "")
                al.Add(System.DBNull.Value);
            else
                al.Add(txtStartDate.Text.ToString());

            //--终止创建日期
            if (txtEndDate.Text == "")
                al.Add(System.DBNull.Value);
            else
                al.Add(txtEndDate.Text.ToString());


            //--创建人

            if (txtCreatorID.Value == "")
                al.Add(System.DBNull.Value);
            else
                al.Add(Int32.Parse(txtCreatorID.Value));

            //是否有效
            if (ddlStatus.SelectedValue == "")
                al.Add(System.DBNull.Value);
            else
                al.Add(Convert.ToByte(ddlStatus.SelectedValue));



            return al;
        }
    }
}
