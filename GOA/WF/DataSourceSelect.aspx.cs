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
    public partial class DataSourceSelect : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {

               this.HiddenDataSourceID.Value =  Request.QueryString["datasourceid"];
                BindGridView();
            }
        }

        #region aspnetPage 分页代码
        //此类无须更改
        protected void AspNetPager1_PageChanged(object src, EventArgs e)
        {
            GridView1.PageIndex = AspNetPager1.CurrentPageIndex - 1;

            BindGridView();
        }
        protected void AspNetPager1_PageChanging(object src, EventArgs e)
        {
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
            GridView1.PageSize = (int)ViewState["PageSize"];

            BindGridView();
        }
        #endregion

        protected void SearchDataSource(object sender, EventArgs e)
        {
            BindGridView();
        }

        private void BindGridView()
        {
            lblMsg.Text = "";
            string dsid =  txtDataSourceID.Text;
            string dsName = txtDataSourceName.Text;

            string whereCondition = "1=1";
            if (dsid != "")
            {
                whereCondition +=  " and DataSourceID like '%" + dsid  + "%'";
            }
            if (dsName  != "")
            {
                whereCondition +=  " and DataSourceName like '%" + dsName  + "%'";

            }

            DataTable dt = DbHelper.GetInstance().GetDBRecords("*", "Workflow_DataSource", whereCondition, "DataSourceID", AspNetPager1.PageSize, AspNetPager1.CurrentPageIndex);
            GridView1.DataSource = dt;

            if (dt.Rows.Count > 0)
                AspNetPager1.RecordCount = Convert.ToInt32(dt.Rows.Count);
            else
                AspNetPager1.RecordCount = 0;

            GridView1.DataBind();

            if (dt.Rows.Count <= 0)
            {
                lblMsg.Text = "没有搜索到符合条件的数据源资料";
                lblMsg.ForeColor = System.Drawing.Color.Red;

            }

        }
    }
}
