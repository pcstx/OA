using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using MyADO;
using GPRP.GPRPBussiness;

namespace GOA.MyCalendar
{
    public partial class Calendar : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGridView();
            }

        }

        #region aspnetPage 分页代码
        //此类无须更改
        protected void AspNetPager1_PageChanged(object src, EventArgs e)
        {

           // BindGridView();

        }
        protected void AspNetPager1_PageChanging(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
        {
            Session["010101AspNetPageCurPage"] = e.NewPageIndex;
        }
        #endregion



        #region  gridView 事件
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




            }
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DropDownList ddl_Permission = (DropDownList)e.Row.FindControl("DropDownList1");

                //ddl_Permission.Items.Insert(0, new ListItem("无权限", "0"));
                //ddl_Permission.Items.Insert(1, new ListItem("可读事件", "1"));
                //ddl_Permission.Items.Insert(2, new ListItem("可删除修改事件", "2"));
                //ddl_Permission.Items.Insert(3, new ListItem("可创建事件", "3"));
                ddl_Permission.SelectedValue = "可读事件";

            }
        }
        #endregion


        protected void txtPageSize_TextChanged(object sender, EventArgs e)
        {
            
        }

 

        protected void AddUser_Click(object sender, EventArgs e)
        {
            string szAllUserName = "";
            string szText = "";
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                if (((CheckBox)GridView1.Rows[i].FindControl("item")).Checked == true)
                {
                    string szUserID = GridView1.Rows[i].Cells[3].Text;
                    string szUserName = GridView1.Rows[i].Cells[2].Text;
                    DropDownList ddl = (DropDownList)GridView1.Rows[i].FindControl("DropDownList1");
                    string szPermission = ddl.SelectedItem.Text;
                    szAllUserName += szUserID + "(" + szPermission + ");";

                }
            }

         //txtSendTo.Text = szAllUserName;
            string strScript = "<script type=\"text/javascript\"  lanuage=\"javascript\"> \r\n" +
           "PostValue('" + szAllUserName + "'); \r\n" +
          "</script> \r\n";
            System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "strScript", strScript, false);
            UserListModalPopupExtender.Hide();

        }

        private void BindGridView()
        {
            DropDownList ddl;
            int userid = Int32.Parse(Context.Session["UserID"].ToString());
            //string userid = WebUtils.GetCookieUser();
            string WhereCondition = "u.UserSerialID !=" + userid;
            string tables = @"UserList u";
            DataTable dt = DbHelper.GetInstance().GetDBRecords("UserName=u.UserName,UserAddress=u.UserID,u.UserSerialID", tables, WhereCondition, "u.UserSerialID", AspNetPager1.PageSize, AspNetPager1.CurrentPageIndex);

            if (dt.Rows.Count > 0)
                AspNetPager1.RecordCount = Convert.ToInt32(dt.Rows[0]["RecordCount"]);
            else
                AspNetPager1.RecordCount = 0;

            GridView1.DataSource = dt;
            GridView1.DataBind();


            //if (dt.Rows.Count > 0)
            //{
            //    for (int i = 0; i <= dt.Rows.Count - 1; i++)
            //    {
            //        ddl = (DropDownList)GridView1.Rows[i].FindControl("DropDownList1");

            //        WhereCondition = "c.CalendarID=" + userid + "and c.CalendarPermissionUserID=" + Int32.Parse(dt.Rows[i]["UserSerialID"].ToString());
            //        tables = @"CalendarPermission c";
            //        DataTable dtPermission = DbHelper.GetInstance().GetDBRecords("c.CalendarPermission,c.CalendarPermissionSerialID", tables, WhereCondition, "c.CalendarPermissionSerialID", AspNetPager1.PageSize, AspNetPager1.CurrentPageIndex);

            //        if (dtPermission.Rows.Count > 0)
            //        {
            //            if (dtPermission.Rows[0]["CalendarPermission"].ToString().Trim() == "无权限")
            //            {
            //                ddl.SelectedIndex = 0;
            //            }
            //            else if (dtPermission.Rows[0]["CalendarPermission"].ToString().Trim() == "可读事件")
            //            {
            //                ddl.SelectedIndex = 1;
            //            }
            //            else if (dtPermission.Rows[0]["CalendarPermission"].ToString().Trim() == "可删除修改事件")
            //            {

            //                ddl.SelectedIndex = 2;
            //            }
            //            else if (dtPermission.Rows[0]["CalendarPermission"].ToString().Trim() == "可创建事件")
            //            {
            //                ddl.SelectedIndex = 3;
            //            }
            //        }
            //        else
            //            ddl.SelectedIndex = 0;
            //    }

            //}

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


        protected void EventNote(object sender, EventArgs e)
        {
            int id = Int32.Parse(Context.Session["UserID"].ToString());
            DataTable dtEventList = DbHelper.GetInstance().GetCalenderEventListByID(id);
            DateTime CalendarEventStartTime;
            int CalendarEventNoteBefore;
            int CalendarEventUserial;
            string CalendarEventNote;
            if (dtEventList.Rows.Count > 0)
            {
                for (int i = 0; i < dtEventList.Rows.Count; i++)
                {
                    CalendarEventStartTime = DateTime.Parse(dtEventList.Rows[i]["CalendarEventStartTime"].ToString());
                    CalendarEventNoteBefore = Int32.Parse(dtEventList.Rows[i]["CalendarEventNoteBefore"].ToString());
                    CalendarEventUserial = Int32.Parse(dtEventList.Rows[i]["CalendarEventUserial"].ToString());
                    CalendarEventNote = dtEventList.Rows[i]["CalendarEventNote"].ToString();
                    if (CalendarEventNote == "true")  //是否提醒
                    {
                        TimeSpan ts = CalendarEventStartTime - DateTime.Now;

                        if (ts.Minutes > 0 && ts.Hours<1)
                        {
                            if (ts.Minutes <= CalendarEventNoteBefore)  //开始时间在提醒时间区间内
                            {
                                //开始提醒


                            }
                        }
                        else
                        {
                            //已经过期了
                        }
                    }
                }
            }

        }


    }
}
