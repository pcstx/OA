using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyADO;
using System.Data;
using System.Web.Script.Serialization;
using System.Web.Services;
using GPRP.GPRPBussiness;
using GPRP.Entity;
using GPRP.Web.UI;

namespace GOA.Index.chosen
{
    public partial class RequestBase : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LoadRequestBase();
        }

        private void LoadRequestBase()
        {
           // string userName = WebUtils.GetCookieUser();
          //  UserListEntity userEntity = DbHelper.GetInstance().GetUserListEntityByUserID(userName);

            string sql = @"SELECT top 5 cl.WorkflowID  ,b.WorkflowName,TotalNum=count(*) " +
                "FROM  Workflow_RequestBase cl left join  Workflow_Base b on  cl.WorkflowID=b.WorkflowID " +
                "where  cl.CurrentNodeType<>4 and cl.IsCancel=0 and " + userEntity.UserSerialID + " in (select * from Fun_GetIDTableByString(cl.CurrentOperatorID))  group by 	cl.WorkflowID  ,b.WorkflowName";

            DataTable dt = DbHelper.GetInstance().ExecDataTable(sql);

            foreach(DataRow d in dt.Rows)
            {
                Response.Write("<a class='request' href='#' WorkflowID='" + d["WorkflowID"] + "'>" + d["WorkflowName"] + "</a>("+d["TotalNum"]+")<p>");
            }
             

        }

    }
}
