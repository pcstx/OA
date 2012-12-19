using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using MyADO;

namespace GOA.Basic
{
    /// <summary>
    /// $codebehindclassname$ 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class addItem1 : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string addItemFlagName = context.Request.Params["addItemFlagName"];
            string type = context.Request.Params["type"];
            string MsgValue =HttpUtility.UrlDecode(context.Request.Params["MsgValue"]);
            int result = 0;

            result = DbHelper.GetInstance().DoInsertItem(addItemFlagName, type);
            string TableName = "PBDEPADD";
            string szresult = DbHelper.GetInstance().DoInsertSysTable(TableName, addItemFlagName, type, MsgValue);
            context.Response.Write(result);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}
