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
    public class IndexDragJson : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string j = getJson();
            context.Response.Write(j);
        }

        private string getJson()
        {
            List<IndexDrag> indexDrag = new List<IndexDrag>();

            DataTable ds = DbHelper.GetInstance().ExecDataTable("select * from dbo.IndexDrag");

            foreach (DataRow d in ds.Rows)
            {
                IndexDrag dg = new IndexDrag();
                dg.id = d["id"].ToString();
                dg.title = d["title"].ToString();
                dg.src = d["src"].ToString();
                dg.className = d["className"].ToString();
                dg.href = d["href"].ToString();                
                dg.height = d["height"] == DBNull.Value ? 0 : Convert.ToDouble(d["height"]);

                indexDrag.Add(dg);
            }

            return new JavaScriptSerializer().Serialize(indexDrag);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }


    public class IndexDrag
    {
        public string id { get; set; }
        public string title { get; set; }
        public string className { get; set; }
        public string src { get; set; }
        public string href { get; set; }
        public double height { get; set; } 
    }
}
