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
    public partial class GG90 : BasePage 
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!Page.IsPostBack)
            {
                AspNetPager1.PageSize = config.PageSize;
                AspNetPager2.PageSize = config.PageSize;
                //设置gridView的界面和数据
                ViewState["selectedLines"] = new ArrayList();
                BindGridView(GridView1 );
                BindGridView(GridView2);
            }
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

        protected void txtPageSize2_TextChanged(object sender, EventArgs e)
            {
            if (txtPageSize2.Text == "" || Convert.ToInt32(txtPageSize2.Text) == 0)
                {
                ViewState["PageSize2"] = config.PageSize;//每页显示的默认值
                }
            else
                {
                ViewState["PageSize2"] = Convert.ToInt32(txtPageSize.Text);
                }
            AspNetPager2.PageSize = Convert.ToInt32(ViewState["PageSize2"]);

            BindGridView(GridView2);
            }

        #region gridView 事件

    
        //1。更改key字段在GridView中的哪一行，默认都是第5行，第一二列为button 第三列为checkBox 第四列为edit字段，第五列 关键字列
        //2。通过关键字获取单条记录。
        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "select")
            {
                int  index = Convert.ToInt32(e.CommandArgument);   //获取行号
                string beAgentID = GridView1.DataKeys[index][0].ToString();
                string agentID = GridView1.DataKeys[index][1].ToString();
                Response.Redirect(string.Format("GG90Detail.aspx?BeAgentPersonID={0}&AgentPersonID={1}", beAgentID, agentID));   
                }

            if (e.CommandName == "cancelagent")
                {
                int index = Convert.ToInt32(e.CommandArgument);   //获取行号
                string beAgentID = GridView1.DataKeys[index][0].ToString();
                string agentID = GridView1.DataKeys[index][1].ToString();
                CancelAllNotCancelAgent(beAgentID,agentID);
                BindGridView(GridView1);
                }
            }
      
   
        protected void GridView2_RowCommand(object sender, GridViewCommandEventArgs e)
            {
            if (e.CommandName == "select")
                {
                int index = Convert.ToInt32(e.CommandArgument);   //获取行号
                string beAgentID = GridView2.DataKeys[index][0].ToString();
                string agentID = GridView2.DataKeys[index][1].ToString();
                Response.Redirect(string.Format("GG90Detail.aspx?BeAgentPersonID={0}&AgentPersonID={1}",beAgentID,agentID ));
                }

            if (e.CommandName == "cancelagent")
                {
                int index = Convert.ToInt32(e.CommandArgument);   //获取行号
                string beAgentID = GridView2.DataKeys[index][0].ToString();
                string agentID = GridView2.DataKeys[index][1].ToString();
                CancelAllNotCancelAgent(beAgentID, agentID);
                BindGridView(GridView2);
                }
            }

       
        #endregion

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

        #region aspnetPage2 分页代码
        //此类无须更改
        protected void AspNetPager2_PageChanged(object src, EventArgs e)
            {
            BindGridView(GridView2);
            }
        protected void AspNetPager2_PageChanging(object src, EventArgs e)
            {
            }
        #endregion

        #region gridview UI
        //此类需要更改，主要是更改获取数据源的方法
        private void BindGridView(GridView gridView)
        {
             DataTable dt=new DataTable ();
             if (gridView.ID == "GridView1")
                 {
                 string condition = string.Format(" BeAgentPersonID={0} or AgentPersonID={0} " ,userEntity.UserSerialID  );
                 dt = DbHelper.GetInstance().GetDBRecords("  * ", "V_AgentSummaryInfo", condition , " BeAgentPersonID ", AspNetPager1.PageSize, AspNetPager1.CurrentPageIndex);
                 if (dt.Rows.Count > 0)
                     AspNetPager1.RecordCount = Convert.ToInt32(dt.Rows[0]["RecordCount"]);
                 else
                     AspNetPager1.RecordCount = 0;
                 }
             else
                 {
                 string condition = string.Format(" BeAgentPersonID<>{0} and  AgentPersonID<>{0} ", userEntity.UserSerialID);
                 dt = DbHelper.GetInstance().GetDBRecords("  * ", "V_AgentSummaryInfo", condition, " BeAgentPersonID ", AspNetPager2.PageSize, AspNetPager2.CurrentPageIndex);
                 if (dt.Rows.Count > 0)
                     AspNetPager2.RecordCount = Convert.ToInt32(dt.Rows[0]["RecordCount"]);
                 else
                     AspNetPager2.RecordCount = 0;
                 }
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

        private void   CancelAllNotCancelAgent(string beAgentID,  string agentID)
            {
            string result = "-1";
            result = DbHelper.GetInstance().CancelBatchWorkflow_AgentSetting(beAgentID, agentID, userEntity.UserSerialID );
            if ( Convert.ToInt32(result) > 0)
                {
               string  strScript = "<script type=\"text/javascript\"  lanuage=\"javascript\"> \r\n" +
                       "  alert('代理全部成功收回！');" +
                      "</script> \r\n";
                ScriptManager.RegisterClientScriptBlock(this,this.GetType(),"alert",strScript ,false );

                }
            else
                {
                  string  strScript = "<script type=\"text/javascript\"  lanuage=\"javascript\"> \r\n" +
                       "  alert('代理收回失败！');" +
                      "</script> \r\n";
                ScriptManager.RegisterClientScriptBlock(this,this.GetType(),"alert",strScript ,false );
                
                }
            }
    }
}