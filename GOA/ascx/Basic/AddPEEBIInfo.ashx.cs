using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using MyADO;
using HRMWeb.aspx;

namespace GOA.Basic
{
    /// <summary>
    /// $codebehindclassname$ 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class AddPEEBIInfo : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            //context.Response.Buffer = true;
            //context.Response.ExpiresAbsolute = DateTime.Now.AddDays(-1);
            //context.Response.AddHeader("pragma", "no-cache");
            //context.Response.AddHeader("cache-control", "");
            //context.Response.CacheControl = "no-cache";

            string action = "";
            string result = "false";
            if (context.Request.Params["action"] != null)
            {
                action = context.Request.Params["action"];
                if (action == "edit") result = DoUpdate(context);
                if (action == "Get") result = GetPEEBIEntity(context);
                if (action == "add") result = DoInsert(context);
                if (action == "del") result = DoDelete(context);
            }
            context.Response.Write(result);
        }
        private string GetPEEBIEntity(HttpContext context)
        {
            return "";
        }

        private string DoUpdate(HttpContext context)
        {
            return "";
        }

        private string DoInsert(HttpContext context)
        {

            string result = "";
            string PEEBITEMPIDStr = context.Request.Params["PEEBITEMPID"];
            //add Rio
            int icount = 0;
            string szItemName = "";
            string szItemValue = "";
            string AddItemStr = context.Request.Params["str"];
            string[] PEEBITEMPID = PEEBITEMPIDStr.Split('|');

            string[] ItemStr = AddItemStr.Split('|');
            foreach (string id in PEEBITEMPID)
            {

                foreach (string istr in ItemStr)
                {
                    string[] ItemValue = istr.Split('=');
                    foreach (string i in ItemValue)
                    {
                        icount++;
                        if (icount % 2 != 0)
                            szItemName = i.ToString();
                        else
                        {
                            szItemValue = i.ToString();
                            result = DbHelper.GetInstance().AddTempDeptInforTemp(id, szItemName, szItemValue);
                            if (result == "-1")
                            {
                                result = "-1";

                            }
                            result = DbHelper.GetInstance().UpdateSysColumShow("PEEBITEMP", szItemName);
                            if (result == "-1")
                            {
                                result = "-1";

                            }

                        }
                    }
                }
            }
            return result;
           
        }

        private string DoDelete(HttpContext context)
        {
            return "";
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
