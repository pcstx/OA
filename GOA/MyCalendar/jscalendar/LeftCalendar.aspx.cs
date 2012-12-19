using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MyADO;
using System.Data;
using GPRP.Entity.Basic;
using GPRP.Entity;
using System.Threading;

namespace GOA.MyCalendar.jscalendar
{
	public partial class LeftCalendar : System.Web.UI.Page
	{
        public int PermissionIndex;
        int preMonth, curMonth, nextMonth;
        int[] preMonthArray, curMonthArray, nextMonthArray;
        protected Calendar calendar1; //日历控件


		protected void Page_Load(object sender, EventArgs e)
		{
            if (!IsPostBack)
            {
                BindDropdownList();
      
            }

		}
      
        private void BindDropdownList()
        {
            int userid = Int32.Parse(Context.Session["UserID"].ToString());
            string WhereCondition = "C.CalendarPermissionUserID=" + userid + "and C.CalendarID=u.UserSerialID";
            string tables = @"CalendarPermission C,UserList u";
            DataTable dt = DbHelper.GetInstance().GetDBRecords("UserName=u.UserName,UserSerialID =u.UserSerialID", tables, WhereCondition, "u.UserSerialID",100 ,1 );
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                    dpOtherCalendar.Items.Add(new ListItem(dt.Rows[i]["UserName"].ToString(), dt.Rows[i]["UserSerialID"].ToString()));
                dpOtherCalendar.Items.Insert(0, new ListItem("自己", "0"));
            }

        }


        protected void GetValue(object sender, EventArgs e)
        {
            PermissionIndex = 0;
            int CalendarId =Int32.Parse(dpOtherCalendar.SelectedValue);
            int PermissionUserID = Int32.Parse(Context.Session["UserID"].ToString());
            if (PermissionUserID == 0)
                PermissionIndex = 3;  //可创建事件
            CalendarPermission _CalendarPermission = new CalendarPermission();
             _CalendarPermission = DbHelper.GetInstance().GetCalendarPermissionEntityByID(CalendarId, PermissionUserID);
            if (_CalendarPermission != null)
            {
                string Permission = _CalendarPermission.CalendarPer;
                if (Permission == "无权限")
                    PermissionIndex = 0;
                else if (Permission == "可读事件")
                    PermissionIndex = 1;
                else if (Permission == "可删除修改事件")
                    PermissionIndex = 2;
                else if (Permission == "可创建事件")
                    PermissionIndex = 3;
            }

            string strScript = "<script type=\"text/javascript\"  lanuage=\"javascript\"> \r\n" +
             "find('" + PermissionIndex + "'); \r\n" +
            "</script> \r\n";
            System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "strScript", strScript, false);
       
        
        
        }

        private void calendar_PreRender(object sender, EventArgs e)
        {
            Thread t = Thread.CurrentThread;
            System.Globalization.CultureInfo c = (System.Globalization.CultureInfo)t.CurrentCulture.Clone();
            c.DateTimeFormat.DayNames = new string[] { "日", "一", "二", "三", "四", "五", "六" };
            c.DateTimeFormat.FirstDayOfWeek = DayOfWeek.Monday;
            t.CurrentCulture = c;
        }

        private void calendar_DayRender(object sender, DayRenderEventArgs e)
        {

            CalendarDay day = e.Day; //事件参数e，包含了年月日等信息
            TableCell cell = e.Cell;

            preMonth = day.Date.Month;
            curMonth = (preMonth + 1 > 12) ? 1 : (preMonth + 1);
            nextMonth = (curMonth + 1 > 12) ? 1 : (curMonth + 1);

            curMonthArray = getBlogArray(day.Date.Year, curMonth);
            preMonthArray = getBlogArray(day.Date.Year, preMonth);
            nextMonthArray = getBlogArray(day.Date.Year, nextMonth);

            int j = 0;
            if (day.Date.Month.Equals(preMonth))
            {
                while (preMonthArray[j] != 0)
                {
                    if (day.Date.Day == preMonthArray[j])
                    {
                        cell.Controls.Clear();
                        cell.Controls.Add(new LiteralControl("<a href='showBlog.aspx?tid=" + day.Date.Year.ToString() + day.Date.Month.ToString() + day.Date.Day.ToString() + "' style='color:#0000ff' target='main'>" + day.Date.Day.ToString() + "</a>"));
                    }
                    j++;
                }

            }
            else if (day.Date.Month.Equals(nextMonth))
            {
                while (nextMonthArray[j] != 0)
                {
                    if (day.Date.Day == nextMonthArray[j])
                    {
                        cell.Controls.Clear();
                        cell.Controls.Add(new LiteralControl("<a href='showBlog.aspx?tid=" + day.Date.Year.ToString() + day.Date.Month.ToString() + day.Date.Day.ToString() + "' style='color:#0000ff' target='main'>" + day.Date.Day.ToString() + "</a>"));
                    }
                    j++;
                }

            }
            else if (day.Date.Month.Equals(curMonth))
            {
                while (curMonthArray[j] != 0)
                {
                    if (day.Date.Day == curMonthArray[j])
                    {
                        cell.Controls.Clear();
                        cell.Controls.Add(new LiteralControl("<a href='showBlog.aspx?tid=" + day.Date.Year.ToString() + day.Date.Month.ToString() + day.Date.Day.ToString() + "' style='color:#0000ff' target='main'>" + day.Date.Day.ToString() + "</a>"));
                    }
                    j++;
                }

            }

        }


        private int[] getBlogArray(int year, int month)
        {
            int[] array = new int[31];
            //int i;
            //for (i = 0; i < 31; i++)
            //    array[i] = 0;
            //i = 0;
            //OleDbConnection conn = (new DBconn()).getConn();
            //OleDbCommand cmd = new OleDbCommand("SELECT [posttime] FROM [blogdata] WHERE month([posttime])=@month AND year([posttime])=@year", conn);
            //cmd.Parameters.Add("@month", OleDbType.Integer, 2, "month");
            //cmd.Parameters["@month"].Value = month;
            //cmd.Parameters.Add("@year", OleDbType.Integer, 2, "year");
            //cmd.Parameters["@year"].Value = year;

            //OleDbDataReader r = cmd.ExecuteReader();
            //while (r.Read())
            //{
            //    array[i++] = r.GetDateTime(0).Day;
            //}
            //r.Close();
            //conn.Close();
           return array;
        }




        protected void Share_Button(object sender, EventArgs e)
        {
            string szAllName;
            szAllName = txtSendTo.Text;
            int iCount = 0;
             
            string[] szOneAllName = szAllName.Split(';');
            int CalendarID=0, PermissionUserID=0;
            foreach (string szOneName in szOneAllName)
            {
                if (szOneName != "")
                {
                    string[] szName = szOneName.Split('(');
                    CalendarPermission _CalendarPermission = new CalendarPermission();
                    foreach (string szUserName in szName)
                    {

                        iCount++;
                        if (iCount == 1)
                        {
                            _CalendarPermission = new CalendarPermission();
                            UserListEntity _UserListEntity = new UserListEntity();
                            _UserListEntity = DbHelper.GetInstance().GetUserListEntityByUserID(szUserName);

                            if (_UserListEntity != null)
                            {
                                _CalendarPermission.CalendarPermissionUserID = _UserListEntity.UserSerialID;   //共享人员ID
                                PermissionUserID = _UserListEntity.UserSerialID;
                            }
                            else
                                _CalendarPermission.CalendarID = 0;
                         
                            _CalendarPermission.CalendarID = Int32.Parse(Context.Session["UserID"].ToString());
                            CalendarID = Int32.Parse(Context.Session["UserID"].ToString());

                        }
                        else
                        {
                            string[] szPermission = szUserName.Split(')');
                            int iPerCount = 0;
                            foreach (string szPermissionName in szPermission)
                            {
                                iPerCount++;
                                if (iPerCount == 1)
                                {
                                    
                                        _CalendarPermission.CalendarPer = szPermissionName;   //权限
                                        CalendarPermission _Calendar = DbHelper.GetInstance().GetCalendarPermissionEntityByID(CalendarID, PermissionUserID);
                                        if (_Calendar == null)
                                        {
                                            string szIndex = DbHelper.GetInstance().AddNewsCalenderPermission(_CalendarPermission);   //添加
                                        }
                                        else
                                        {
                                            string szIndex = DbHelper.GetInstance().UpDateCalendarPermission(_CalendarPermission);  //更新
                                        }
                                    
                                }
                                else
                                    iPerCount = 0;
                            }
                            iCount = 0;
                        }

                    }
                }
            }


 
        }
	}
}
