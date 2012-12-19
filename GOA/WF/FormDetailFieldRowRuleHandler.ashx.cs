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
    public class FormDetailFieldRowRuleHandler : IHttpHandler
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
            result = CaculateRule(context);

            context.Response.Write(result);
        }

        private string CaculateRule(HttpContext context)
        {
            string RuleFieldName = context.Request.Params["RuleFieldName"];
            string FieldValue = context.Request.Params["FieldValue"];
            string RuleDetail = context.Request.Params["RuleDetail"];
            string FieldDBType = context.Request.Params["FieldDBType"];
            return  DbHelper.GetInstance().GetDetailFormRowRuleValue(RuleFieldName, FieldValue, RuleDetail, FieldDBType);
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
