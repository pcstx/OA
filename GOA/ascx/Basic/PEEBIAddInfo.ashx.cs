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
    public class PEEBIAddInfo1 : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            int result = 0;
            string szResult = "";
            string addItemFlagName = context.Request.Params["addItemFlagName"];
            string type = context.Request.Params["type"];
            string MsgValue = HttpUtility.UrlDecode(context.Request.Params["MsgValue"]);
            string PEEBITEMPIDStr = context.Request.Params["PEEBITEMPID"];
            string szFlagStr = "NULL";
            if (type == "int")
                szFlagStr = "0";
            else if (type == "datetime")
                szFlagStr = "0000-0-0";
            if (PEEBITEMPIDStr != "")
            {
                result = DbHelper.GetInstance().DoInsertItem(addItemFlagName, type);
                if (PEEBITEMPIDStr.IndexOf('|') > 0)
                {
                    string[] PEEBITEMPIDList = PEEBITEMPIDStr.Split('|');
                    foreach (string PEEBITEMPID in PEEBITEMPIDList)
                    {
                        if (PEEBITEMPID!="")
                          szResult = DbHelper.GetInstance().UpdateTempDeptInforTemp(PEEBITEMPID, addItemFlagName, szFlagStr);
                    }
                }
                else
                    szResult = DbHelper.GetInstance().UpdateTempDeptInforTemp(PEEBITEMPIDStr, addItemFlagName, szFlagStr);

                string TableName = "PEEBITEMP";
                string szresult = DbHelper.GetInstance().DoInsertSysTable(TableName, addItemFlagName, type, MsgValue);
            }
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
