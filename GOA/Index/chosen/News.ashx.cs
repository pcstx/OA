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

namespace GOA.Index.chosen
{
    /// <summary>
    /// $codebehindclassname$ 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class News : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            string type = context.Request["type"];

            if (type == "pic")
            {
              //  context.Response.Write("http://img4.cache.netease.com/cnews/2012/8/16/20120816185853c13c9.jpg");
                context.Response.Write("http://www.baidu.com");
            }
            else
            {
                context.Response.Write("Hello World");
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
