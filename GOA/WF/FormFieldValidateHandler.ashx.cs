using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Text.RegularExpressions;
using System.Configuration;
using GPRP.GPRPComponents;
using GPRP.Entity;
using MyADO;

namespace GOA
{
    /// <summary>
    /// 处理表单的字段的基本数据验证
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class FormFieldValidateHandler : IHttpHandler
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
            result = FieldValidate(context);

            context.Response.Write(result);
        }


        private string FieldValidate(HttpContext context)
        {
            
            string ValidType = context.Request.Params["ValidType"];
            string ValieValue=context.Request.Params["ValidValue"];
            //获取验证规则
            //DataTable dtInspect = DbHelper.GetInstance().GetBasicValidateType(ValidType);
            Workflow_BasicValidTypeEntity validRule = DbHelper.GetInstance().GetWorkflow_BasicValidTypeEntityByKeyCol(ValidType);

            if (validRule != null && ValieValue != "")
            {
                string regularExpression = validRule.ValidRule;
                string validErroMsg = validRule.ValidErrorMsg;

                if (Regex.IsMatch(ValieValue, regularExpression))
                {
                    return "true| ";
                }
                else
                {
                    return "false|" + validErroMsg;
                }
            }
            else
            {
                return "true| ";
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
