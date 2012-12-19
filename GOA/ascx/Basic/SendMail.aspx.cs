using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GPRP.Web.UI;
using GPRP.Entity;
using MyADO;
using System.Data;

namespace GOA.Basic
{
    public partial class SendMail : BasePage
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
                    DbHelper.GetInstance().UpdateISReadByID(Int32.Parse(szId), 2);
                   

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
        {
        }

 


        private void BindGridView()
        {

            string WhereCondition = string.Format("e.UserID = '{0}' and e.ISRead ={1} and UserMasterID=0", userEntity.UserID, 0);
            string tables = @"UserEmail e left join UserList l on e.SenderID = l.UserID";
            DataTable dt = DbHelper.GetInstance().GetDBRecords("SendAdd=e.SenderID ,SendName=l.UserName,MailTitle=e.MailTitle,Time=e.SendTime,e.UserSerialID", tables, WhereCondition, "e.SendTime", AspNetPager1.PageSize, AspNetPager1.CurrentPageIndex);
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
        //此类无须更改
        protected void AspNetPager1_PageChanged(object src, EventArgs e)
        {

            BindGridView();

        }
        protected void AspNetPager1_PageChanging(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
        {
            Session["010101AspNetPageCurPage"] = e.NewPageIndex;
        }
        #endregion

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
                int index = Convert.ToInt32(e.CommandArgument);
                Response.Redirect("ShowMailList.aspx?action=send&MailID=" + index);



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
