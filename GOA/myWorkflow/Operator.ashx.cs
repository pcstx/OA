using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GPRP.Entity;
using MyADO;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Data;
using GPRP.Web.UI;
using GPRP.GPRPBussiness;

namespace GOA.myWorkflow
{
    /// <summary>
    /// $codebehindclassname$ 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class Operator : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string type = context.Request["type"];

            if (type == "operator")
            {
                string o = getOperatorType();
                context.Response.Write(o);
      
            }
            else if (type == "TypeDetail")
            {
                string TypeCode = context.Request["TypeCode"];
                string WorkflowID=context.Request["id"];
                string restult = getOperatorTypeDetail(TypeCode,WorkflowID);
                context.Response.Write(restult);
            }
            else if (type == "")
            { 
                
            }                  
                      
        }

        private List<OperatorTypeDetail> ReDisplayOperatorContents(string TypeCode,string WorkflowID)
        {
            DataTable dtObjectList = new DataTable();

            if (TypeCode == "20")
            {
                dtObjectList = DbHelper.GetInstance().GetDBRecords("c.FieldID,c.FieldLabel,c.DisplayOrder", 
                    "Workflow_Base b,Workflow_FormField c,Workflow_FieldDict d",
                    "b.FormID=c.FormID and c.FieldID=d.FieldID and d.HTMLTypeID=8 and d.BrowseType=3 and b.WorkflowID=" + WorkflowID, 
                    "c.DisplayOrder");
            }
            else if (TypeCode == "30")
            {
                dtObjectList = DbHelper.GetInstance().GetDBRecords("c.FieldID,c.FieldLabel,c.DisplayOrder",
                    "Workflow_Base b,Workflow_FormField c,Workflow_FieldDict d",
                    "b.FormID=c.FormID and c.FieldID=d.FieldID and d.HTMLTypeID=8 and d.BrowseType=2 and b.WorkflowID=" + WorkflowID,
                    "c.DisplayOrder");
            }
            else if (TypeCode == "50")
            {
                //DataTable dtObjectList = DbHelper.GetInstance().GetDBRecords("a.NodeID,a.NodeName", 
                //    "Workflow_FlowNode a,Workflow_FlowNode b", 
                //    "a.WorkflowID=b.WorkflowID and b.NodeID=" + NodeID + " and a.NodeID != " + NodeID, 
                //    "NodeID");
            }

            List<OperatorTypeDetail> lotd = new List<OperatorTypeDetail>();

            foreach (DataRow dr in dtObjectList.Rows)
            {
                OperatorTypeDetail otd = new OperatorTypeDetail();
                otd.DetailTypeName = dr["FieldLabel"].ToString();
                otd.TypeDetailCode = Convert.ToInt32(dr["FieldID"]);
                lotd.Add(otd);
            }

            return lotd;
        }

        private string getOperatorTypeDetail(string TypeCode, string WorkflowID)
        { 
            DataTable dtTypeDetail = DbHelper.GetInstance().GetDBRecords("TypeDetailCode,DetailTypeName", "Workflow_OperatorTypeDetail", "TypeCode='" + TypeCode + "'", "TypeDetailCode");
            List<OperatorTypeDetail> lotd = new List<OperatorTypeDetail>();

            foreach (DataRow dr in dtTypeDetail.Rows)
            {
                OperatorTypeDetail otd = new OperatorTypeDetail();
                otd.DetailTypeName = dr["DetailTypeName"].ToString();
                otd.TypeDetailCode = Convert.ToInt32(dr["TypeDetailCode"]);
                lotd.Add(otd); 
            }

            List<OperatorTypeDetail> OperatorContents_list = ReDisplayOperatorContents(TypeCode, WorkflowID);
            var griddata = new { OperatorTypeDetail = lotd, ObjectValue = OperatorContents_list };

            string json = new JavaScriptSerializer().Serialize(griddata);
            return json;            
        }

        private string getOperatorType()
        {
            DataTable dtOperatorType = DbHelper.GetInstance().GetDBRecords("TypeCode,TypeName", "Workflow_OperatorType", "1=1", "DisplayOrder");

            List<OperatorType> ot_list = new List<OperatorType>();

            foreach (DataRow dr in dtOperatorType.Rows)
            {
                OperatorType ot = new OperatorType();
                ot.TypeCode = Convert.ToInt32(dr["TypeCode"]);
                ot.TypeName = dr["TypeName"].ToString();
              
                ot_list.Add(ot);
            }

            JavaScriptSerializer js = new JavaScriptSerializer();
            string json = js.Serialize(ot_list);
            return json;
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

public class OperatorType
{
    public int TypeCode { get; set; }
    public string TypeName { get; set; } 
}

public class OperatorTypeDetail
{
    public int TypeDetailCode { get; set; }
    public string DetailTypeName { get; set; }
}