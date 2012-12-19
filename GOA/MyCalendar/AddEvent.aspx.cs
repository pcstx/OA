using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GPRP.Entity.Basic;
using MyADO;
using System.Data;

namespace GOA.Calendar
{
    public partial class AddEvent : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string action = Context.Request.Params["action"];
            int UserID = Int32.Parse(Context.Session["UserID"].ToString());

            if (action != null)
            {
                if (action.ToLower() == "show")
                {
                    int id = Int32.Parse(Context.Request.Params["activity.id"].ToString());
                    CalendarEventEntity _CalendarEventEntity = DbHelper.GetInstance().GetCalendarEntityByID(id);
                    TxtTitle.Text = _CalendarEventEntity.Title;
                    TxtContent.Text = _CalendarEventEntity.Content;
                    if (_CalendarEventEntity.Type == 1)
                        drType.SelectedValue = "提醒";
                    else if (_CalendarEventEntity.Type == 2)
                        drType.SelectedValue = "会议邀请";
                    else
                        drType.SelectedValue = "";

                    TxtBeginTime.Text = _CalendarEventEntity.StartTime.ToString();
                    TxtEndTime.Text = _CalendarEventEntity.EndTime.ToString();
                    TxtInvite.Text = _CalendarEventEntity.Invite;

                    if (_CalendarEventEntity.EmailNote == "true")
                        ckEmailNote.Checked = true;
                    else
                        ckEmailNote.Checked = false;

                    if (_CalendarEventEntity.Note == "true")
                        ckTiXing.Checked = true;
                    else
                        ckTiXing.Checked = false;

                    if (_CalendarEventEntity.NoteBefore == 1)
                        drTiXing.SelectedValue = "10分钟";
                    else if (_CalendarEventEntity.NoteBefore == 2)
                        drTiXing.SelectedValue = "20分钟";
                    else if (_CalendarEventEntity.NoteBefore == 3)
                        drTiXing.SelectedValue = "30分钟";
                    else if (_CalendarEventEntity.NoteBefore == 4)
                        drTiXing.SelectedValue = "60分钟";
                    else
                        drTiXing.SelectedValue = "";


                    if (_CalendarEventEntity.Repeat == "true")
                        ckRepeat.Checked = true;
                    else
                        ckRepeat.Checked = false;

                    if (_CalendarEventEntity.RepeatRate == 1)
                        drRepeatRate.SelectedValue = "每天";
                    else if (_CalendarEventEntity.RepeatRate == 2)
                        drRepeatRate.SelectedValue = "每周";
                    else if (_CalendarEventEntity.RepeatRate == 3)
                        drRepeatRate.SelectedValue = "每月";
                    else
                        drRepeatRate.SelectedValue = "";



                }
            }

        }

        protected void txtPageSize_TextChanged(object sender, EventArgs e)
        { }

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
              
            }
        }
        #endregion


        private void BindGridView()
        {
            DropDownList ddl;
            int userid = Int32.Parse(Context.Session["UserID"].ToString());
            string WhereCondition = "";
            string tables = @"UserList u ";
            DataTable dt = DbHelper.GetInstance().GetDBRecords("UserName=u.UserName,UserAddress=u.UserID,u.UserSerialID", tables, WhereCondition, "u.UserSerialID", AspNetPager1.PageSize, AspNetPager1.CurrentPageIndex);
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

        protected void AddUser_Click(object sender, EventArgs e)
        { 
        
        }

    }
}
