using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Timers;
using MyADO;
using System.Data;
 
namespace GOA
{
    public class Global : System.Web.HttpApplication
    {
        private static string[] usr;
        private static string content;
        private static List<Timer> ts = new List<Timer>();

        protected void Application_Start(object sender, EventArgs e)
        {
         
            CalendarNote(); //日历提醒             
        }

        public static void CalendarNote()
        {
            foreach (Timer t in ts)
            {
                t.Stop();
                t.Dispose();
            }
            ts.Clear();

            string sql = "[getCalendarTime]";
            DataTable dt= DbHelper.GetInstance().ExecuteDataset(CommandType.StoredProcedure, sql, null).Tables[0];

          //  string sql = "select CalendarEventTitle,CalendarEventUserID,CalendarEventNoteTime from dbo.CalendarEvent where CalendarEvent.CalendarEventNoteTime=(select min(CalendarEventNoteTime) from dbo.CalendarEvent where CalendarEventNoteTime>= getdate() and CalendarEventNote='true')";
            DataTable time = DbHelper.GetInstance().ExecuteDataset(CommandType.StoredProcedure, sql, null).Tables[0];

            foreach (DataRow dr in time.Rows)
            {
                string username = DbHelper.GetInstance().ExecSqlResult("select UserID from UserList where UserSerialID=" + dr["CalendarEventUserID"].ToString());
                usr = new string[] { username };
                content = dr["CalendarEventTitle"].ToString();

                DateTime dtNow = DateTime.Now;
                DateTime dtNote = DateTime.Parse(dr["t"].ToString());
                double mi = (dtNote - dtNow).TotalMilliseconds;
                              
                System.Timers.Timer t = new System.Timers.Timer();     //实例化Timer类，设置间隔时间为10000毫秒；  
                t.Interval = mi;
                t.Elapsed += new System.Timers.ElapsedEventHandler(r);  //到达时间的时候执行事件；   
                t.AutoReset = false;  //设置是执行一次（false）还是一直执行(true)；   
                t.Enabled = true;  //是否执行System.Timers.Timer.Elapsed事件；  
                ts.Add(t);  //增加
            }
        }

        private static  void r(object source, System.Timers.ElapsedEventArgs e)
        {             
            myAsynResult asyncResult = null;             
            //向Message类中添加该消息 
            Messages.Instance().AddMessage(content, asyncResult, usr );
            CalendarNote();
        }
              

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}