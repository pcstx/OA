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
    public partial class RecieveMail : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            getMail();
        }

        private void getMail()
        {
            string WhereCondition = string.Format("e.UserID = '{0}' and ReceiverID!='' and ISRead!=2 ", userEntity.UserID);
            string tables = @"UserEmail e left join UserList l on e.ReceiverID = l.UserID ";
            DataTable dt = DbHelper.GetInstance().GetDBRecords("RecvAdd=e.ReceiverID ,RecvName=l.UserName,MailTitle=e.MailTitle,Time=e.ReceiveTime,UserSerialID=e.UserSerialID", tables, WhereCondition, "e.ReceiveTime", 5,1);

            foreach (DataRow d in dt.Rows)
            {
                Response.Write("<a class='request' href='#'  UserSerialID='" + d["UserSerialID"] + "' >" + d["MailTitle"] + "</a><p>");
            }
        }

    }
}
