using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Configuration;
using GPRP.GPRPComponents;
using GPRP.Entity;
using MyADO;

namespace GOA
{
    /// <summary>
    /// $codebehindclassname$ 的摘要说明    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class FormDetailFieldColumnRuleHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            context.Response.Buffer = true;
            context.Response.ExpiresAbsolute = DateTime.Now.AddDays(-1);
            context.Response.AddHeader("pragma", "no-cache");
            context.Response.AddHeader("cache-control", "");
            context.Response.CacheControl = "no-cache";

            string result = "false";
            result = CaculateColumn(context);

            context.Response.Write(result);
        }


        private string CaculateColumn(HttpContext context)
        {

            string RuleFieldName = context.Request.Params["RuleFieldName"];
            string FieldValue = context.Request.Params["FieldValue"];
            string RuleDetail = context.Request.Params["RuleDetail"];
            string RuleFieldDBType = context.Request.Params["RuleFieldDBType"];
            string[] splitStr=new string[1];
            splitStr[0]="|";
            DataTable dt = new DataTable();
            if (RuleFieldDBType.ToUpper() == "INT")
                dt.Columns.Add(RuleFieldName,typeof(System.Int32));
            else if (RuleFieldDBType.ToUpper() == "FLOAT")
                dt.Columns.Add(RuleFieldName,typeof(System.Double));
            else if (RuleFieldDBType.ToUpper() == "DOUBLE")
                dt.Columns.Add(RuleFieldName,typeof(System.Double));
            else if (RuleFieldDBType.ToUpper() == "NUMRIC")
                dt.Columns.Add(RuleFieldName,typeof(System.Double));
            else
                dt.Columns.Add(RuleFieldName, typeof(System.String));
            string[] FieldValueArray=FieldValue.Split(splitStr, StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < FieldValueArray.Length; i++)
            {
                DataRow row = dt.NewRow();
                row[RuleFieldName] = FieldValueArray[i];
                dt.Rows.Add(row);
                
            }
            return Convert.ToString(dt.Compute(RuleDetail,"1=1"));


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
