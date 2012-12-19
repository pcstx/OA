using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using MyADO;
using System.Data;

namespace GOA.Basic
{
    /// <summary>
    /// $codebehindclassname$ 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class GetAddItem : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string szId= context.Request.Params["PEEBITEMPID"];
            string result = "";
                DataTable dtName = DbHelper.GetInstance().GetSysTableByTableName("PEEBITEMP");
                if (dtName.Rows.Count > 0)
                {
                    for (int i = 0; i < dtName.Rows.Count; i++)
                    {
                        string szColName = dtName.Rows[i]["ColName"].ToString();   //控件id
                        string szColDescriptionCN = dtName.Rows[i]["ColDescriptionCN"].ToString(); //控件说明
                        string szColType = dtName.Rows[i]["ColType"].ToString(); //控件中要输入值的类型
                        DataTable dt2 = DbHelper.GetInstance().DoGetAddItem(szId);
                            if (dt2 != null)
                            {
                                for (int j = 0; j < dt2.Rows.Count; j++)
                                {
                                    string Value = dt2.Rows[j][szColName].ToString();  //控件值
                                    result += szColName + "|" + szColDescriptionCN + "|" + szColType + "|" + Value + "|";
                                }
                           }

                    }
                context.Response.Write(result);
            }
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
