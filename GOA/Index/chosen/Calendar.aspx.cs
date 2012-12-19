using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GPRP.Web.UI;
using System.Data;
using MyADO;
using GPRP.Entity;

namespace GOA.Index.chosen
{
    public partial class Calendar  : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            getCalendar();
        }

        private void getCalendar()
        {
            string WhereCondition = string.Format("l.UserID = '{0}' and convert(varchar(10),e.CalendarEventStartTime,120)= convert(varchar(10),getdate(),120)", userEntity.UserID);
            string tables = @"CalendarEvent e left join UserList l on e.CalendarEventUserID = l.UserSerialID ";
            DataTable dt = DbHelper.GetInstance().GetDBRecords("Userial=e.CalendarEventUserial ,Title=e.CalendarEventTitle", tables, WhereCondition, "e.CalendarEventStartTime", 5, 1);

            foreach (DataRow d in dt.Rows)
            {
                Response.Write("<a class='request' href='#'  Userial='" + d["Userial"] + "' >" + d["Title"] + "</a><p>");
            }
        }
    }
}
