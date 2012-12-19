using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MyADO;
using GPRP.Web.UI;
using System.Data;

namespace GOA.Basic
{
    public partial class DeleteMail : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                AspNetPager1.PageSize = config.PageSize;
                BindGridView();
            }
        }

        protected void btnDelet_Click(object sender, EventArgs e)
        {
            bool checkDele = false;
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                if (((CheckBox)GridView1.Rows[i].FindControl("Item")).Checked == true)
                {
                    checkDele = true;
                    string szId = GridView1.DataKeys[i].Values["UserSerialID"].ToString(); 
                    DbHelper.GetInstance().DeleteEmailByUserSerialID(Int32.Parse(szId));
                  

                }
            }

            if (!checkDele)
            {
                string strScript = "<script type=\"text/javascript\"  lanuage=\"javascript\"> \r\n" +
              "alert('请选择要删除的项！'); \r\n" +
             "</script> \r\n";
                System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "strScript2", strScript, false);
            }
            BindGridView();
        
        }
        protected void txtPageSize_TextChanged(object sender, EventArgs e)
        { }
        private void BindGridView()
        {

            string WhereCondition = string.Format("e.UserID = '{0}'  and ISRead=2", userEntity.UserID);
            string tables = @"UserEmail e left join UserList l on ( case e.ReceiverID when '' then e.UserID else e.ReceiverID end )= l.UserID ";
            DataTable dt = DbHelper.GetInstance().GetDBRecords("case e.ReceiverID when '' then e.UserID  else e.ReceiverID end as RecvAdd,RecvName=l.UserName,MailTitle=e.MailTitle,Time=e.ReceiveTime,UserSerialID=e.UserSerialID", tables, WhereCondition, "e.ReceiveTime", AspNetPager1.PageSize, AspNetPager1.CurrentPageIndex);

            if (dt.Rows.Count > 0)
                AspNetPager1.RecordCount = Convert.ToInt32(dt.Rows[0]["RecordCount"]);
            else
                AspNetPager1.RecordCount = 0;

            GridView1.DataSource = dt;
            GridView1.DataBind();
            BuildNoRecords(GridView1, dt);
        }

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

   

        #region aspnetPage 分页代码
        protected void AspNetPager1_PageChanged(object src, EventArgs e)
        {

            BindGridView();

        }
        protected void AspNetPager1_PageChanging(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
        {
            Session["010101AspNetPageCurPage"] = e.NewPageIndex;
        }
        #endregion


        #region  gridview事件

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
                int index = Convert.ToInt32(e.CommandArgument);
                Response.Redirect("ShowMailList.aspx?action=dele&MailID=" + index);



            }
        }
        //此类要进行dorpdownlist/chk控件的转换
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

            }
        }

        #endregion
    }
}
