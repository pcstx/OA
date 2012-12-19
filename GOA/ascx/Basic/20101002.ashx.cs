using System;
using System.Data;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Text;

using GPRP.Web.UI;
using GPRP.GPRPComponents;
using GPRP.GPRPEnumerations;
using GPRP.GPRPBussiness;
using GPRP.Entity;
using MyADO;

namespace HRMWeb.aspx
{
    /// <summary>
    /// $codebehindclassname$ 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class _01010021 : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            context.Response.Buffer = true;
            context.Response.ExpiresAbsolute = DateTime.Now.AddDays(-1);
            context.Response.AddHeader("pragma", "no-cache");
            context.Response.AddHeader("cache-control", "");
            context.Response.CacheControl = "no-cache";

            string result = "";
            string PEEBIEC = context.Request.Params["PEEBIEC"];

            PEEBIEntity _PEEBIEntity = null;
            try
            {

                _PEEBIEntity = DbHelper.GetInstance().GetPEEBIEntityByKeyCol(PEEBIEC);
            }
            catch
            {

            }

            if (_PEEBIEntity != null)
            {
                result = "0|" + _PEEBIEntity.PEEBIEN;
            }
            else
            {
                result = "1|" + "该员工不存在";
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
