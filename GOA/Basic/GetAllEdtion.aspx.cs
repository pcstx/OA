using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MyADO;
using System.Collections;
using System.Data;
using GPRP.Web.UI;
using GPRP.Entity.Basic;

namespace GOA.Basic
{
    public partial class GetAllEdtion : BasePage
    {
        private string MyPath = "";
        private string fileName = "";
        private string folderName = "";
        private int fileId = 0;
       

        protected void Page_Load(object sender, EventArgs e)
        {
            MyPath = HttpUtility.UrlDecode(Context.Request.Params["ImgPath"]);
            fileName = MyPath.Substring(MyPath.LastIndexOf("/") + 1);
            folderName = MyPath.Substring(0, MyPath.LastIndexOf("/"));
            DocFileInfo _docFileInfo = DbHelper.GetInstance().GetFileInfoEntityByFileName(fileName, folderName);
            fileId = _docFileInfo.FileSerialID;

            if (!Page.IsPostBack)
            {
                AspNetPager1.PageSize = config.PageSize;
                BindGridView();
            }

        }

        protected void txtPageSize_TextChanged(object sender, EventArgs e)
        {
            if (txtPageSize.Text == "" || Convert.ToInt32(txtPageSize.Text) == 0)
            {
                Session["010101PageSize"] = config.PageSize;//每页显示的默认值
            }
            else
            {
                Session["010101PageSize"] = Convert.ToInt32(txtPageSize.Text);
            }
            AspNetPager1.PageSize = Convert.ToInt32(Session["010101PageSize"]);
            //再进行绑定一次
            BindGridView();

            System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "ButtonHideScript", strButtonHideScript, false);
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

        protected void GridView1_RowCreated(Object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

            }
        }

       
        #endregion

        #region gridView 事件
        //1。更改key字段在GridView中的哪一行，默认都是第5行，第一二列为button 第三列为checkBox 第四列为edit字段，第五列 关键字列
        //2。通过关键字获取单条记录。
        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "select")
            {
                string edition = e.CommandArgument.ToString();
                string TailName = edition.Substring(edition.LastIndexOf(".") + 1);
                string path = "OfficeCheckInOut.aspx?ImgPath=" +edition + "&tailName=" + TailName+ "&fileES=" + 0;
                Response.Redirect(path);



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

     
        private void BindGridView()
        {

            string WhereCondition = "FileID ="+fileId;
            string tables = @"Doc_FileEdition a";
            DataTable dt = DbHelper.GetInstance().GetDBRecords("FileEdition=a.FileEdition,ModifyUser=a.ModifyUser, ModifyDate=a.ModifyDate,FileNote=a.FileNote,FileID=a.FileID,FileUrl=a.FileUrl,FileName=a.FileName", tables, WhereCondition, "a.ModifyDate", AspNetPager1.PageSize, AspNetPager1.CurrentPageIndex);
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


   }
}
