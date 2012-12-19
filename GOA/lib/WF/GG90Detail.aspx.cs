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
    public partial class GG90Detail : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
            {

            if (!Page.IsPostBack)
                {
                if (Request.QueryString["AgentPersonID"] != null && Request.QueryString["BeAgentPersonID"] != null)
                    {
                    string aPid = Request.QueryString["BeAgentPersonID"].ToString();
                    string pid = Request.QueryString["AgentPersonID"].ToString();

                    this.txtQAgentPersonID.Value = pid;
                    this.txtQBeAgentPersonID.Value = aPid;

                    this.txtQAgentPersonName.Text = DbHelper.GetInstance().GetUserListEntityByKeyCol(pid).UserName;
                    this.txtQBeAgentPersonName.Text = DbHelper.GetInstance().GetUserListEntityByKeyCol(aPid).UserName;



                    AspNetPager1.PageSize = config.PageSize;
                    //设置gridView的界面和数据
                    ViewState["selectedLines"] = new ArrayList();
                    BindGridView();
                    }
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
        #region gridView 事件

        protected void GridView1_RowCreated(Object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {

            }
        }
        //1。更改key字段在GridView中的哪一行，默认都是第5行，第一二列为button 第三列为checkBox 第四列为edit字段，第五列 关键字列
        //2。通过关键字获取单条记录。
        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
        if (e.CommandName == "select")
            {
            int index = Convert.ToInt32(e.CommandArgument);   //获取行号
            string keyCol = GridView1.DataKeys[index].Value.ToString();
            string url = string.Format("GG90Add.aspx?AgentID={0}", keyCol);
            Response.Redirect(url );
            }
        }
        //此类要进行dorpdownlist/chk控件的转换
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string KeyCol = GridView1.DataKeys[e.Row.RowIndex].Value.ToString();
                CheckBox cb = (System.Web.UI.WebControls.CheckBox)e.Row.FindControl("Item") as CheckBox;
              //  ArrayList selectedLines = (ArrayList)ViewState["selectedLines"];
                string IsCancel = ((DataRowView)e.Row.DataItem).Row["IsCancel"].ToString();

                if ( (IsCancel == "0"))
                {
                    cb.Checked = false;
                    cb.Visible = true;
                }
                else
                {
                    cb.Checked = true;
                    cb.Visible = false;
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
        #region gridview UI
        //此类需要更改，主要是更改获取数据源的方法
        private void BindGridView()
        {
            string WhereCondition = "1=1 ";
            if (this.ddlIsCancel.SelectedValue != string.Empty)
                WhereCondition += " and   IsCancel ='" + ddlIsCancel.SelectedValue + "'";

            if (txtQWorkflowID.Value != string.Empty)
                WhereCondition += " and   WorkflowID in (" + txtQWorkflowID.Value + ")";

            if (txtQBeAgentPersonID.Value != string.Empty)
                WhereCondition += "and  BeAgentPersonID =" + txtQBeAgentPersonID.Value + "";

            if (txtQBeAgentPersonID.Value != string.Empty)
                WhereCondition += "and  AgentPersonID= " + txtQAgentPersonID.Value + "";

            if (txtQAgentStartDate.Text != string.Empty)
                WhereCondition += "and ( AgentStartDate >= '" + txtQAgentStartDate.Text + "'  or  AgentStartDate is null)";

            if (txtQAgentEndDate.Text != string.Empty)
                WhereCondition += "and ( AgentEndDate<= '" + txtQAgentEndDate.Text + "' or  AgentEndDate is null)";

            DataTable dt = DbHelper.GetInstance().GetDBRecords("*", "V_AgentInfo", WhereCondition, " AgentID desc,IsCancel", AspNetPager1.PageSize, AspNetPager1.CurrentPageIndex);
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
        //gridViewUI显示

        #endregion

        protected void btnSearchRecord_Click(object sender, EventArgs e)
        {
            AspNetPager1.CurrentPageIndex = 1;
            ViewState["selectedLines"] = new ArrayList();
            BindGridView();

            System.Web.UI.ScriptManager.RegisterStartupScript(btnSearchRecord, this.GetType(), "ButtonHideScript", strButtonHideScript, false);
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
                if (!selectedLines.Contains(KeyCol) && (cb.Checked && cb.Visible== true))
                    selectedLines.Add(KeyCol);
            }
        }

        protected void btnCancelAgent_Click(object sender, EventArgs e)
        {
            try
            {
                CollectSelected();
                ArrayList selectedLines = (ArrayList)ViewState["selectedLines"];
                for (int i = 0; i < selectedLines.Count; i++)
                {
                    //此处需修改，在SqlDBBasic中添加Delete function
                    DbHelper.GetInstance().CancelWorkflow_AgentSetting(userEntity.UserSerialID, (selectedLines[i].ToString()));
                }


                string strScript = "<script type='text/javascript'  lanuage='javascript'> alert('" + ResourceManager.GetString("Button_GoComplete") + "'); </script>";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", strScript, false);
            }
            catch
            {
                string strScript = "<script type='text/javascript'  lanuage='javascript'> alert('" + ResourceManager.GetString("Operation_RECORD") + "'); </script>";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", strScript, false);
            }
            finally
            {
                BindGridView();
                ViewState["selectedLines"] = new ArrayList();
            }

            System.Web.UI.ScriptManager.RegisterStartupScript(btnCancelAgent, this.GetType(), "ButtonHideScript", strButtonHideScript, false);
        }

    }
}