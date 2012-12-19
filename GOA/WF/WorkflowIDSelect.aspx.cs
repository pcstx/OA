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
    public partial class WorkflowIDSelect : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {

                DataTable dtFormType = DbHelper.GetInstance().GetDBRecords("*", "Workflow_FormType", "Useflag='1'", "DisplayOrder");
                ddlFlowTypeID.AddTableData(dtFormType, 0, 1, true, "Select");

                if (DNTRequest.GetString("FormID") != "" && DNTRequest.GetString("FormID") != null)
                {
                    ImgForm.Enabled = false;
                    txtFormID.Value = DNTRequest.GetString("FormID");
                    txtFormN.Text = DbHelper.GetInstance().GetWorkflow_FormBaseEntityByKeyCol(DNTRequest.GetString("FormID")).FormName;
                }

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
Workflow_Base a
left join Workflow_FormType b on a.FlowTypeID=b.FormTypeID
left join Workflow_FormBase c on a.FormID=c.FormID
";
            string WhereCondition = "1=1";
            if (txtQWorkflowName.Text != string.Empty)
            {
                WhereCondition += " and  a.WorkflowName like '%" + txtQWorkflowName.Text + "%'";
            }
            if (txtFormID.Value != string.Empty)
            {
                WhereCondition += " and a.FormID  in  (" + txtFormID.Value + ")";
            }

            if (ddlFlowTypeID.SelectedValue != "0")
            {
                WhereCondition += " and a.FlowTypeID = '" + ddlFlowTypeID.SelectedValue + "'";
            }

            DataTable dt = DbHelper.GetInstance().GetDBRecords("a.*,FlowTypeN=b.FormTypeName,FormN=c.FormName", Tables, WhereCondition, "a.FlowTypeID,a.DisplayOrder", AspNetPager1.PageSize, AspNetPager1.CurrentPageIndex);
            if (dt.Rows.Count > 0)
                AspNetPager1.RecordCount = Convert.ToInt32(dt.Rows[0]["RecordCount"]);
            else
                AspNetPager1.RecordCount = 0;

            GridView1.DataSource = dt;
            GridView1.DataBind();
            BuildNoRecords(GridView1, dt);

            if (DNTRequest.GetString("IsSingle") != null && DNTRequest.GetString("IsSingle") == "1")
            {
                //((CheckBox)GridView1.Columns[1].FindControl("Item")).Visible = false;
                GridView1.Columns[1].Visible = false;
                btnSelect.Visible = false;
            }

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

        }
        //此类要进行dorpdownlist/chk控件的转换

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string IsValid = ((DataRowView)e.Row.DataItem).Row["IsValid"].ToString();
                System.Web.UI.WebControls.CheckBox chkIsValid = (System.Web.UI.WebControls.CheckBox)e.Row.FindControl("chkIsValid");
                chkIsValid.Checked = IsValid.Equals("1");


                string KeyCol = ((DataRowView)e.Row.DataItem).Row["WorkflowID"].ToString() + "_" + ((DataRowView)e.Row.DataItem).Row["WorkflowName"].ToString();
                CheckBox cb = (System.Web.UI.WebControls.CheckBox)e.Row.FindControl("Item") as CheckBox;
                ArrayList selectedLines = (ArrayList)ViewState["selectedLines"];
                if (selectedLines.Contains(KeyCol))
                {
                    cb.Checked = true;
                }
            }
        }
        #endregion

        protected void btnSearchRecord_Click(object sender, EventArgs e)
        {
            AspNetPager1.CurrentPageIndex = 1;
            ViewState["selectedLines"] = new ArrayList();
            BindGridView();
            System.Web.UI.ScriptManager.RegisterStartupScript(btnSearch, this.GetType(), "ButtonHideScript", strButtonHideScript, false);
        }


        private void CollectSelected()
        {
            ArrayList selectedLines = (ArrayList)ViewState["selectedLines"];
            for (int i = 0; i < this.GridView1.Rows.Count; i++)
            {
                string KeyCol = GridView1.DataKeys[i][0].ToString().Trim() + "_" + GridView1.Rows[i].Cells[4].Text.ToString().Trim();
                CheckBox cb = this.GridView1.Rows[i].FindControl("Item") as CheckBox;
                if (selectedLines.Contains(KeyCol) && !cb.Checked)
                    selectedLines.Remove(KeyCol);
                if (!selectedLines.Contains(KeyCol) && cb.Checked)
                    selectedLines.Add(KeyCol);
            }
        }

        protected void btnSelect_Click(object sender, EventArgs e)
        {
            CollectSelected();
            ArrayList selectedLines = (ArrayList)ViewState["selectedLines"];

            string sCode = "";
            string sName = "";
            for (int i = 0; i < selectedLines.Count; i++)
            {
                string[] sl = selectedLines[i].ToString().Split(new char[] { '_' });
                sCode += sl[0].ToString() + ",";
                sName += sl[1].ToString() + ",";

            }
            if (sCode.Length > 0)
                sCode = sCode.Substring(0, sCode.Length - 1);
            if (sName.Length > 0)
                sName = sName.Substring(0, sName.Length - 1);
            string strButtonSelectScript = "btnSelectClick('" + sCode + "','" + sName + "');";
            System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "strButtonSelectScript", strButtonSelectScript, true);
        }

    }
}
