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
    public partial class GGC0 : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        if (!Page.IsPostBack)
            {
                AspNetPager1.PageSize = config.PageSize;
                //设置gridView的界面和数据
                ViewState["selectedLines"] = new ArrayList();
                bindControlValues();
                BindGridView(GridView1 );
              
            }
        }

        private void bindControlValues()
        {
        DataTable dtReportType = DbHelper.GetInstance().GetDBRecords("ReportTypeID,ReportTypeName", "Workflow_ReportType", "1=1", "DisplayOrder");
        ddlReportType.AddTableData(dtReportType, 0, 1, true, "Select");
            
            }
        //设置每页显示记录数，无须更改；如果要更改默认第页记录数，到config/geneal.config中更改 PageSize
        protected void txtPageSize_TextChanged(object sender, EventArgs e)
        {
            if (txtPageSize.Text == "" || Convert.ToInt32(txtPageSize.Text) == 0)
            {
                ViewState["PageSize1"] = config.PageSize;//每页显示的默认值
            }
            else
            {
                ViewState["PageSize1"] = Convert.ToInt32(txtPageSize.Text);
            }
            AspNetPager1.PageSize = Convert.ToInt32(ViewState["PageSize1"]);

            BindGridView(GridView1);
        }


        #region aspnetPage1 分页代码
        //此类无须更改
        protected void AspNetPager1_PageChanged(object src, EventArgs e)
        {
            BindGridView(GridView1 );
        }
        protected void AspNetPager1_PageChanging(object src, EventArgs e)
        {
        }
        #endregion

      
        #region gridview UI
        //此类需要更改，主要是更改获取数据源的方法
        private void BindGridView(GridView gridView)
        {
             DataTable dt=new DataTable ();
             string columns = @"r.ReportID
                              ,r.ReportTypeID
                              ,t.ReportTypeName
                              ,r.ReportName
                              ,r.FormID
                              ,f.FormName";
             string tables = @" Workflow_ReportMain r
                            inner join Workflow_ReportType t on r.ReportTypeID=t.ReportTypeID
                            left join  Workflow_FormBase f on r.FormID=f.FormID";
             string condition ="(1=1)";


             if (ddlReportType.SelectedIndex != 0)
                 condition += "r.ReportTypeID=" + ddlReportType.SelectedValue ;

            if (txtFormID.Value.Trim()!="")
                condition += "r.FormID=" + txtFormID.Value.Trim();

            if (txtReportName.Text.Trim() != "")
                condition += "r.ReportName like '%" + txtReportName.Text.Trim() + "%'";


            dt = DbHelper.GetInstance().GetDBRecords(columns, tables, condition, "t.DisplayOrder,r.ReportID", AspNetPager1.PageSize, AspNetPager1.CurrentPageIndex);
                 if (dt.Rows.Count > 0)
                     AspNetPager1.RecordCount = Convert.ToInt32(dt.Rows[0]["RecordCount"]);
                 else
                     AspNetPager1.RecordCount = 0;
            
             gridView.DataSource = dt;
             gridView.DataBind();
             BuildNoRecords(gridView, dt);
        
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


        protected void btnSearch_Click(object sender, EventArgs e)
        {
       // AspNetPager1.CurrentPageIndex = 1;
        BindGridView(GridView1);
            }

    }
}
