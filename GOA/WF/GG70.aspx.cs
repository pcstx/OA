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
    public partial class GG70 : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                AspNetPager1.PageSize = config.PageSize;
                AspNetPager2.PageSize = config.PageSize;
                ViewState["selectedLines"] = new ArrayList();
                BindGridView();
                BindGridView2();
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
            //再进行绑定一次
            CollectSelected();
            BindGridView();
        }

        protected void txtPageSize2_TextChanged(object sender, EventArgs e)
        {
            if (txtPageSize2.Text == "" || Convert.ToInt32(txtPageSize2.Text) == 0)
            {
                ViewState["PageSize2"] = config.PageSize;//每页显示的默认值
            }
            else
            {
                ViewState["PageSize2"] = Convert.ToInt32(txtPageSize2.Text);
            }
            AspNetPager2.PageSize = Convert.ToInt32(ViewState["PageSize2"]);
            //再进行绑定一次
            CollectSelected();
            BindGridView2();
        }

        #region gridView1 事件
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
            int index = -1;
            if (e.CommandName == "select")
            {
                index = Convert.ToInt32(e.CommandArgument);   //获取行号
                string keyCol = GridView1.DataKeys[index].Value.ToString();
                Response.Redirect(string.Format("GG70Add.aspx?DataSetID={0}", keyCol));
            }
        }
        //此类要进行dorpdownlist/chk控件的转换

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string KeyCol = GridView1.DataKeys[e.Row.RowIndex][0].ToString();
                CheckBox cb = (System.Web.UI.WebControls.CheckBox)e.Row.FindControl("Item") as CheckBox;
                ArrayList selectedLines = (ArrayList)ViewState["selectedLines"];
                if (selectedLines.Contains(KeyCol))
                {
                    cb.Checked = true;
                }
            }
        }
        #endregion

        #region gridView2 事件
        protected void GridView2_RowCreated(Object sender, GridViewRowEventArgs e)
        {
            // The GridViewCommandEventArgs class does not contain a 
            // property that indicates which row''s command button was
            // clicked. To identify which row''s button was clicked, use 
            // the button''s CommandArgument property by setting it to the 
            // row''s index.
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
            }
        }
        //1。更改key字段在GridView中的哪一行，默认都是第5行，第一二列为button 第三列为checkBox 第四列为edit字段，第五列 关键字列
        //2。通过关键字获取单条记录。


        protected void GridView2_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = -1;
            if (e.CommandName == "select")
            {
                index = Convert.ToInt32(e.CommandArgument);   //获取行号
                string keyCol = GridView2.DataKeys[index].Value.ToString();
                Response.Redirect(string.Format("GG70Add.aspx?DataSetID={0}", keyCol));
            }
        }
        //此类要进行dorpdownlist/chk控件的转换


        protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string KeyCol = GridView2.DataKeys[e.Row.RowIndex][0].ToString();
                CheckBox cb = (System.Web.UI.WebControls.CheckBox)e.Row.FindControl("Item") as CheckBox;
                ArrayList selectedLines = (ArrayList)ViewState["selectedLines"];
                if (selectedLines.Contains(KeyCol))
                {
                    cb.Checked = true;
                }
            }
        }
        #endregion

        #region aspnetPage1 分页代码
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

        #region aspnetPage2 分页代码
        //此类无须更改
        protected void AspNetPager2_PageChanged(object src, EventArgs e)
        {
            CollectSelected();
            BindGridView2();
        }
        protected void AspNetPager2_PageChanging(object src, EventArgs e)
        {
        }
        #endregion

        #region "gridview1 UI"
        //此类需要更改，主要是更改获取数据源的方法

        private void BindGridView()
        {

            string WhereCondition = " DataSetType=1 ";
            if (txtQDataSetName.Text != string.Empty)
            {
                WhereCondition += " and  a.DataSetName like '%" + txtQDataSetName.Text + "%'";
            }

            if (ddlDataSetType.SelectedValue != string.Empty)
            {
                WhereCondition += " and  a.DataSetType =" + Convert.ToInt32(ddlDataSetType.SelectedValue);
            }


            DataTable dt = DbHelper.GetInstance().GetDBRecords("a.*,b.DataSourceName,DataSetTypeT='SQL语句'", "Workflow_DataSet a left join Workflow_DataSource b on a.DataSourceID=b.DataSourceID", WhereCondition, "DataSetID", AspNetPager1.PageSize, AspNetPager1.CurrentPageIndex);
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

        #region "gridview2 UI"
        //此类需要更改，主要是更改获取数据源的方法


        private void BindGridView2()
        {

            string WhereCondition = " DataSetType=2 ";
            if (txtQDataSetName.Text != string.Empty)
            {
                WhereCondition += " and  a.DataSetName like '%" + txtQDataSetName.Text + "%'";
            }

            if (ddlDataSetType.SelectedValue != string.Empty)
            {
                WhereCondition += " and  a.DataSetType =" + Convert.ToInt32(ddlDataSetType.SelectedValue);
            }


            DataTable dt = DbHelper.GetInstance().GetDBRecords("a.*,b.DataSourceName,DataSetTypeT='存储过程' ", "Workflow_DataSet a left join Workflow_DataSource b on a.DataSourceID=b.DataSourceID", WhereCondition, "DataSetID", AspNetPager2.PageSize, AspNetPager2.CurrentPageIndex);
            if (dt.Rows.Count > 0)
                AspNetPager2.RecordCount = Convert.ToInt32(dt.Rows[0]["RecordCount"]);
            else
                AspNetPager2.RecordCount = 0;
            GridView2.DataSource = dt;
            GridView2.DataBind();
            BuildNoRecords(GridView2, dt);
        }

        #endregion

        #region "pannel"



        protected void btnSearchRecord_Click(object sender, EventArgs e)
        {
            AspNetPager1.CurrentPageIndex = 1;
            AspNetPager2.CurrentPageIndex = 1;
            ViewState["selectedLines"] = new ArrayList();
            BindGridView();
            BindGridView2();
        }



        protected void btnDel_Click(object sender, EventArgs e)
        {
            CollectSelected();
            ArrayList selectedLines = (ArrayList)ViewState["selectedLines"];
            for (int i = 0; i < selectedLines.Count; i++)
            {
                DbHelper.GetInstance().DeleteWorkflow_DataSet(Convert.ToInt32(selectedLines[i].ToString()));
            }
            BindGridView();
            BindGridView2();
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

            for (int i = 0; i < this.GridView2.Rows.Count; i++)
            {
                string KeyCol = GridView2.DataKeys[i][0].ToString().Trim();
                CheckBox cb = this.GridView2.Rows[i].FindControl("Item") as CheckBox;
                if (selectedLines.Contains(KeyCol) && !cb.Checked)
                    selectedLines.Remove(KeyCol);
                if (!selectedLines.Contains(KeyCol) && cb.Checked)
                    selectedLines.Add(KeyCol);
            }
        }

        #endregion

    }
}

