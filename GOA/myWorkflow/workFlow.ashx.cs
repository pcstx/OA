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
    public class workFlow :  IHttpHandler
    {  
        public void ProcessRequest(HttpContext context)
        { 
            context.Response.ContentType = "text/plain"; 
            string type = context.Request["type"];

            if (type == "toHtml")
            {
                int workflowID =Convert.ToInt32(context.Request["WorkflowID"]);
                string Html= DBtoHtml(workflowID);
                context.Response.Write(Html);
            }
            else if (type == "CheckWorkflowName")
            {
                string WorkflowName = context.Request["WorkflowName"];
                string sql = "select count(*) from Workflow_Base where WorkflowName='"+WorkflowName+"';";
                int result = Convert.ToInt32(DbHelper.GetInstance().ExecSqlResult(sql));
                context.Response.Write(result);
            }
             else if (type == "FormBase")
            { 
                string FormName=    context.Request["FormName"];
                string FormDesc = context.Request["FormDesc"];
                int FormTypeID = Convert.ToInt32(context.Request["FormTypeID"]);

                int currentPages =Convert.ToInt32(context.Request["page"]);
                int pagesize =Convert.ToInt32(context.Request["pagesize"]);

                FormBase fb = new FormBase();
                fb.FormName = FormName;
                fb.FormDesc = FormDesc;
                fb.FormTypeID = FormTypeID;

                string json = getFormBase(currentPages,pagesize,fb);
                context.Response.Write(json);
            }
            else if (type == "FlowTypeID")
            {
               string json= getFormType();
               context.Response.Write(json);
            }
            else if(type=="workflowBase")
            {
                string jsonStr = context.Request["json"];
                int result = CreateWorkFlow(jsonStr);   //增加工作流 
                context.Response.Write(result);
            }
            else  //流程
            {  
                  string flowNode = context.Request["flownode"];
                  string nodeLink = context.Request["nodeLink"];
                  string WorkflowID = context.Request["WorkflowID"];
                  Dictionary<string, int> dic_wfn = new Dictionary<string, int>();
                  int startNode = 0;

                  delWorkFlow(WorkflowID);  //删除现有的

                  int nodeFlag= AddFlowNode(flowNode, dic_wfn,ref startNode); //增加节点

                  if (nodeFlag == 0)
                  {
                     nodeFlag=  AddNodeLink(nodeLink, dic_wfn,startNode);  //增加连线
                  } 
                   
                  context.Response.Write(nodeFlag);
            } 
        }

        private void delWorkFlow(string workflowID)
        {
            DataTable nodeID = DbHelper.GetInstance().GetDBRecords("NodeID", "workflow_FlowNode", " workflow_FlowNode.WorkflowID=" + workflowID, "workflow_FlowNode.NodeID");

            foreach (DataRow dr in nodeID.Rows)
            {
                string delWorkflow_NodeOperatorDetail = "delete from Workflow_NodeOperatorDetail where NodeID=" + dr["NodeID"];
                DbHelper.GetInstance().ExecSqlText(delWorkflow_NodeOperatorDetail);
            }
            string delWorkflow_FlowNodeSQL = "delete from [Workflow_FlowNode] where WorkflowID="+workflowID;
            DbHelper.GetInstance().ExecSqlText(delWorkflow_FlowNodeSQL);

            DataTable linkID = DbHelper.GetInstance().GetDBRecords("LinkID", "Workflow_NodeLink", " Workflow_NodeLink.WorkflowID=" + workflowID, "Workflow_NodeLink.LinkID");

            foreach (DataRow dr in linkID.Rows)
            {
                string delWorkflow_NodeCondition = "delete from Workflow_NodeCondition where LinkID=" +dr["LinkID"];
                DbHelper.GetInstance().ExecSqlText(delWorkflow_NodeCondition);
            }
         
            string delWorkflow_NodeLink = "delete from Workflow_NodeLink where WorkflowID="+workflowID;
            DbHelper.GetInstance().ExecSqlText(delWorkflow_NodeLink);

         
        }
               
        private int AddNodeLink(string nodeLink, Dictionary<string, int> dic_wfn,int startNode)
        { 
            int nodeFlag = 0;  
            JavaScriptSerializer js = new JavaScriptSerializer();
            List<GPRP.Entity.NodeLink> list_wnl = js.Deserialize<List<GPRP.Entity.NodeLink>>(nodeLink);
            if (list_wnl == null) return 0;

            foreach (var w in list_wnl)
            { 
                w.StartNodeID = dic_wfn[w.StartNodeName];
                w.TargetNodeID = dic_wfn[w.TargetNodeName];
             
                if (w.TargetNodeID == startNode)
                {
                    w.IsRejected = 1;  //目标节点为开始节点时为退回
                }
                 
                w.SqlCondition="("+w.SqlCondition+")";
                w.CreateDate = DateTime.Now;
                w.lastModifyDate = DateTime.Now;
                w.Creator = w.lastModifier = WebUtils.GetCookieUser();

                string id = SaveNodeLink(w, "Add");     //增加连线
                AddNodeCondition(Convert.ToInt32(id),w.NodeCondition);  //增加路径条件

                if (Convert.ToInt32(id) <= 0)
                {
                    nodeFlag = 1;
                    break;
                }
            }

            return nodeFlag;
        }

        private string AddNodeCondition(int LinkID,List<Workflow_NodeConditionEntity> wnc)
        {
            string result = "";
            Workflow_NodeConditionEntity _NodeConditionEntity = new Workflow_NodeConditionEntity();
            _NodeConditionEntity.LinkID = LinkID;
            
            DataTable dtMaxSeq = DbHelper.GetInstance().GetDBRecords("MaxBatchSeq=isnull(max(BatchSeq),0)+1", "Workflow_NodeCondition", "LinkID=" + LinkID, "");
            _NodeConditionEntity.BatchSeq = Convert.ToInt32(dtMaxSeq.Rows[0]["MaxBatchSeq"]);

            foreach (var i in wnc)
            {
                _NodeConditionEntity.BranchBatchSeq = i.BranchBatchSeq;
                _NodeConditionEntity.FieldID = i.FieldID;
                _NodeConditionEntity.SymbolCode = i.SymbolCode;
                _NodeConditionEntity.CompareToValue = i.CompareToValue;
                _NodeConditionEntity.AndOr = i.AndOr;

                result=DbHelper.GetInstance().AddWorkflow_NodeCondition(_NodeConditionEntity);  //增加到NodeConditiion表中
            }

            return result;
        }

        private int AddFlowNode(string flowNode, Dictionary<string, int> dic_wfn,ref int startNode)
        {
            int nodeFlag = 0;  
            JavaScriptSerializer js = new JavaScriptSerializer();
            List<FlowNode> list_wfn = js.Deserialize<List<FlowNode>>(flowNode);  //数组
            if (list_wfn == null) return 0;

            foreach (var w in list_wfn)
            {            
                if (Convert.ToInt32(w.OverTimeLen) > 0)
                {
                    w.IsOverTime = 1;  //超时提醒
                }
                w.DisplayOrder = 99990; //显示顺序

                int re = SaveFlowNode(w, "Add");  //增加
                dic_wfn.Add(w.nodeText, re);

                if (re < 0)
                {
                    nodeFlag = 1;
                    break;
                }
                else
                {
                    //增加操作人
                    foreach (var od in w.OperatorDetail)
                    {
                        od.NodeID = re;
                        od.SecurityEnd = 100;
                        string r= DbHelper.GetInstance().AddWorkflow_NodeOperatorDetail(od);  
                    }

                    if (w.NodeTypeID == 1)
                    {
                        startNode = re;
                        DbHelper.GetInstance().sp_GeneratorWorkflowCreatorList(w.WorkflowID.ToString());  //开始节点运行存储过程
                    }
                }
            }

            return nodeFlag;
        }
                
        private int CreateWorkFlow(string jsonStr)
        {
            JavaScriptSerializer js = new JavaScriptSerializer();
            Workflow_BaseEntity wb = js.Deserialize<Workflow_BaseEntity>(jsonStr);  
           
            return  SaveData(wb, "Add");   
        }

        private string getFormBase(int currentPages,int pageSize,FormBase fb)
        {
            string search = "";
            search += !string.IsNullOrEmpty(fb.FormName) ? " and FormName='" + fb.FormName + "'" : "";
            search += !string.IsNullOrEmpty(fb.FormDesc)  ? " and FormDesc='" + fb.FormDesc + "'" : "";
            search += fb.FormTypeID !=0 ? " and FormTypeID='" + fb.FormTypeID + "'" : "";
        
            DataTable dtFormBase = DbHelper.GetInstance().GetDBRecords("*", "Workflow_FormBase", "Useflag='1'" +search, "DisplayOrder", pageSize, currentPages);
            string total = DbHelper.GetInstance().ExecSqlResult("select count(*) from Workflow_FormBase where Useflag='1'"+search);
            List<FormBase> lft = new List<FormBase>();
             
            foreach (DataRow dr in dtFormBase.Rows)
            {
                FormBase ft = new FormBase();
                ft.FormID = Convert.ToInt32(dr["FormID"]);
                ft.FormName = dr["FormName"].ToString();
                ft.FormDesc = dr["FormDesc"].ToString();
                ft.FormTypeID = Convert.ToInt32(dr["FormTypeID"]);
                ft.DisplayOrder = Convert.ToInt32(dr["DisplayOrder"]);
                ft.Useflag = dr["Useflag"].ToString();
                lft.Add(ft);
            }

            var griddata = new { Rows = lft,Total=total };
            string json = new JavaScriptSerializer().Serialize(griddata);
             
            return json;
        }

        private string getFormType()
        {
            DataTable dtFormType = DbHelper.GetInstance().GetDBRecords("FormTypeID,FormTypeName", "Workflow_FormType", "Useflag='1'", "DisplayOrder");
            List<FormType> lft = new List<FormType>();

            foreach (DataRow dr in dtFormType.Rows)
            {
                FormType ft = new FormType();
                ft.name = dr["FormTypeName"].ToString();
                ft.value = Convert.ToInt32(dr["FormTypeID"]);
                lft.Add(ft);
            }

            JavaScriptSerializer js = new JavaScriptSerializer();
            string json=  js.Serialize(lft);
            return json;
        }
          
        /// <summary>
        /// 保存工作流
        /// </summary>
        /// <param name="nwf"></param>
        /// <param name="operationState"></param>
        /// <returns></returns>
        private int SaveData(Workflow_BaseEntity _Workflow_BaseEntity, string operationState)
        {  
            string sResult = "-1";
            if (operationState == "Add")
                sResult = DbHelper.GetInstance().AddWorkflow_Base(_Workflow_BaseEntity);
            else if (operationState == "Update")
                sResult = DbHelper.GetInstance().UpdateWorkflow_Base(_Workflow_BaseEntity);

            int wfid = _Workflow_BaseEntity.WorkflowID;
            return wfid;
        }
              
        private int SaveFlowNode(Workflow_FlowNodeEntity2 wfn,string operationState)
        {
            Workflow_FlowNodeEntity2 _Workflow_FlowNodeEntity = wfn;
            
            string sResult = "-1";
            if (operationState == "Add")
                sResult =DbHelper.GetInstance().AddWorkflow_FlowNode2(_Workflow_FlowNodeEntity);             
            else if (operationState == "Update")
                sResult =DbHelper.GetInstance().UpdateWorkflow_FlowNode(_Workflow_FlowNodeEntity);

            int wfnid = _Workflow_FlowNodeEntity.NodeID;
            return wfnid;
        }

        private string SaveNodeLink(GPRP.Entity.NodeLink wnl, string operationState)
        {
            GPRP.Entity.NodeLink _Workflow_NodeLinkEntity = wnl;
            
            string sResult = "-1";
            if (operationState == "Add")
                sResult = DbHelper.GetInstance().AddWorkflow_NodeLink2(_Workflow_NodeLinkEntity);
            else if (operationState == "Update")
                sResult = "";
             //   sResult = DbHelper.GetInstance().UpdateWorkflow_NodeLink(_Workflow_NodeLinkEntity);
            return sResult;
        }

        /// <summary>
        /// 反序列化json字符串
        /// </summary>
        /// <param name="strJson"></param>
        /// <returns></returns>
        private newWorkFlow JsonDeserialize(string strJson)
        { 
            JavaScriptSerializer js = new JavaScriptSerializer(); 
            return js.Deserialize<newWorkFlow>(strJson); 
        }

        private string ScriptSerialize(Workflow_FlowNodeEntity nwf)
        { 
            JavaScriptSerializer js = new JavaScriptSerializer();
            return js.Serialize(nwf);
        }


        private string DBtoHtml(int workflowID)
        {
            //////////////////////////////////////
            string rectHtml = "";
            DataTable dt = DbHelper.GetInstance().GetDBRecords("*", "workflow_FlowNode", " workflow_FlowNode.WorkflowID=" + workflowID, "workflow_FlowNode.DisplayOrder");
           
            for(int i=0;i<dt.Rows.Count;i++)
            {
                DataRow dr=dt.Rows[i]; 

                string rect =dr["NodeID"].ToString();
                string type = "";
                 int flowNodeType =Convert.ToInt32(dr["NodeTypeID"]);
                 switch (flowNodeType)
                 { 
                     case 1:
                         type = "start";
                         break; 
                     case 2:
                         type = "state";
                         break;
                     case 3:
                         type = "task";
                         break;
                     case 4:
                         type = "end";
                         break;
                 }

                /////////////////////////////////
                 string sql = "select * from Workflow_NodeOperatorDetail where NodeID="+rect;
                DataTable Rule=  DbHelper.GetInstance().ExecDataTable(sql);              
                string oper="[";

                foreach(DataRow rcDataRow in Rule.Rows)
                {
                    string RuleType = rcDataRow["RuleType"].ToString();
                    string ObjectValue =rcDataRow["ObjectValue"].ToString();
                    string RuleName =rcDataRow["RuleName"].ToString();
                    string LevelStart =rcDataRow["LevelStart"].ToString();
                    string LevelEnd = rcDataRow["LevelEnd"].ToString();
                    string Operator_SignType =rcDataRow["SignType"].ToString();
                    string RuleCondition =rcDataRow["RuleCondition"].ToString();
                    string RuleSeq =rcDataRow["RuleSeq"].ToString();

                    oper += "{\"RuleType\":" + RuleType + ",\"RuleCondition\":\"" + RuleCondition + "\",\"ObjectValue\":\"" + ObjectValue +
               "\",\"RuleName\":\"" + RuleName + "\",\"RuleSeq\":" + RuleSeq + ",\"LevelStart\":" + LevelStart + ",\"LevelEnd\":" + LevelEnd + ",\"SignType\":" + Operator_SignType + "},";   
               
                
                }

                if (oper != "[")
                {
                    oper = oper.Substring(0, oper.Length - 1);
                }
                oper += "]";
        
                ///////////////////////////////////
                
                string NodeName=dr["NodeName"].ToString();
                string NodeDesc = dr["NodeDesc"].ToString();
                string OverTimeLen = dr["OverTimeLen"].ToString();
                string SignType = dr["SignType"].ToString();  
                int x = dr["x"]==DBNull.Value ? 10: Convert.ToInt32(dr["x"]);
                int y = dr["y"] == DBNull.Value ? 10 : Convert.ToInt32(dr["y"]);
                int width = dr["width"] == DBNull.Value ? 100 : Convert.ToInt32(dr["width"]);
                int height = dr["height"] == DBNull.Value ? 50 : Convert.ToInt32(dr["height"]);

                rectHtml += rect + ":{ type: '" + type + "',text: { text: '" + NodeName +
                    "' },attr: { x: " + x + ", y: " + y + ",width: " + width + ", height: " + height +
                    " }, props: {  NodeName: {value: '" + NodeName + "' }, NodeDesc: { value: '" + NodeDesc +
                    "' },   OverTimeLen: { value: '" + OverTimeLen +
                    "' }, SignType: { value: '" +SignType+
                    "'  }, Operator: { value: '" + oper + "'} } },";
            }
            if (rectHtml != "")
            {
                rectHtml = rectHtml.Substring(0, rectHtml.Length - 1);
            }
            //////////////////////////////////

            string nodelinkHtml = "";
            DataTable NodeLinkDT = DbHelper.GetInstance().GetDBRecords("*", "workflow_NodeLink", " workflow_NodeLink.WorkflowID=" + workflowID, "workflow_NodeLink.LinkID");
            for (int i = 0; i < NodeLinkDT.Rows.Count; i++)
            {
                DataRow dr = NodeLinkDT.Rows[i];

                string WorkflowName = dr["LinkName"].ToString();
                int StartNodeID = Convert.ToInt32(dr["StartNodeID"]);
                int TargetNodeID = Convert.ToInt32(dr["TargetNodeID"]);
                string SqlCondition = dr["SqlCondition"].ToString();
                int x =dr["x"]==DBNull.Value?10: Convert.ToInt32(dr["x"]);
                int y =dr["y"]==DBNull.Value?10: Convert.ToInt32(dr["y"]);
                string path = "path" + i;
                nodelinkHtml += path + ": {  from: '" + StartNodeID + "',  to: '"+TargetNodeID+"',  dots: [  ],  text: {   text: '"+WorkflowName+
                    "'  },  textPos: {  x: "+x+",  y:"+y+"  },  props: { text: {  value: ''  },  LinkName: {  value: '"+WorkflowName+
                    "'   },  SqlCondition: {  value: \""+SqlCondition+"\", condition: ''  }}   },";
                
            }
            if (nodelinkHtml != "")
            {
                nodelinkHtml = nodelinkHtml.Substring(0, nodelinkHtml.Length - 1);
            }

            ///////////////////////////////////
           // string rectHtml = "";
            DataTable workflowDT = DbHelper.GetInstance().GetDBRecords("*", "Workflow_Base", "Workflow_Base.WorkflowID=" + workflowID, "Workflow_Base.DisplayOrder");

            string props = "";
            for (int i = 0; i < workflowDT.Rows.Count; i++)
            { 
                DataRow dr=workflowDT.Rows[i];

                string WorkflowName = dr["WorkflowName"].ToString();
                string WorkflowDesc = dr["WorkflowDesc"].ToString();
                int FlowTypeID = Convert.ToInt32(dr["FlowTypeID"]);
                int FormID = Convert.ToInt32(dr["FormID"]);
                bool IsValid = Convert.ToBoolean(dr["IsValid"]);
                bool IsMsgNotice = Convert.ToBoolean(dr["IsValid"]);
                bool IsMailNotice = Convert.ToBoolean(dr["IsValid"]);
                bool IsTransfer = Convert.ToBoolean(dr["IsTransfer"]);

                props += " props: {WorkflowName: {value: '"+WorkflowName+"' }, WorkflowDesc: { value: '"+WorkflowDesc
                    +"'},FlowTypeID: { value: "+FlowTypeID+"  }, FormID: {value:"+FormID+
                    " }, IsValid: { value: '"+IsValid+"'  },  IsMsgNotice: {   value:'"+IsMsgNotice+
                    "' },  IsMailNotice: {   value: '"+IsMailNotice+"'  }, IsTransfer: {  value:'"+IsTransfer+"' }  },";
            }
            if (props != "")
            {
                props = props.Substring(0, props.Length - 1);
            }
                ///////////////////////////////////
             
            string html = "({  states: { "+rectHtml+" },paths:{ "+nodelinkHtml+" },props:{ "+props+" } })";
            return html;
        }


        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }



    public class FormBase
    {
        public int FormID { get; set; }
        public string FormName { get; set; }
        public string FormDesc { get; set; }
        public int FormTypeID { get; set; }
        public int DisplayOrder { get; set; }
        public string Useflag { get; set; }
    }

    public class FormType 
    {
        public string name { get; set; }
        public int value { get; set; }
    }

   public class newWorkFlow
    {
        public int WorkflowID { get; set; }
        public string WorkflowName { get; set; }
        public string WorkflowDesc { get; set; }
        public int FlowTypeID { get; set; }
        public int FormID { get; set; }
        public int IsValid { get; set; }
        public int IsMailNotice { get; set; }
        public int IsMsgNotice { get; set; }
        public int IsTransfer { get; set; }
        public int AttachDocPath { get; set; }
        public int HelpDocPath { get; set; }
        public int DisplayOrder { get; set; } 
    }

   public class FlowNode : Workflow_FlowNodeEntity2
   {
       public string nodeText { get; set; }
       public List<Workflow_NodeOperatorDetailEntity> OperatorDetail { get; set; }
   }

   public class NodeLink : Workflow_NodeLinkEntity
   {
       public string StartNodeName { get; set; }
       public string TargetNodeName { get; set; }
       public string SqlCondition { get; set; }
       public List<Workflow_NodeConditionEntity> NodeCondition { get; set; }
   }

  
}
