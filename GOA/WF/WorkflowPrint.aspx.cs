using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Web.Services;
using GPRP.Web.UI;
using GPRP.GPRPComponents;
using GPRP.GPRPControls;
using GPRP.GPRPEnumerations;
using GPRP.GPRPBussiness;
using GPRP.Entity;
using YYControls;
using VBControls.VBProject;
using GOA.UserControl;
using MyADO;


namespace GOA.WF
{
    public partial class WorkflowPrint : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!Page.IsPostBack)
            {
                //string scripts = "<script language='javascript' type='text/javascript'>";
                //string RequestID = Request.QueryString["RequestID"].ToString();
                //string FormID=Request.QueryString["FormID"].ToString();
                //string WorkflowID = Request.QueryString["WorkflowID"].ToString();
                //string NodeID = Request.QueryString["WorkflowID"].ToString();
                ////获取数据
                //DataTable dtMain = DbHelper.GetInstance().ExecDataTable("select * from workflow_Form where RequestID='" + RequestID + "'");
                //scripts = scripts + "LoadXMLFiles('" + (sql_xmldata(dtMain)) + "','表头数据','DataSetMain');";
                //////获取各明细组
                ////DataTable dtDetailGroup = DbHelper.GetInstance().GetDetailFieldGroup(Convert.ToInt32(FormID), Convert.ToInt32(NodeID));
                ////for (int i=0;i<dtDetailGroup.Rows.Count;i++)
                ////{
                ////   int GroupID=Convert.ToInt32(dtDetailGroup.Rows[i]["GroupID"].ToString());
                ////   DataTable  dtGroup=DbHelper.GetInstance().GetDetailFieldValue(Convert.ToInt32(FormID), Convert.ToInt32(RequestID), GroupID);
                ////   scripts = scripts + "LoadXMLFiles('" + (sql_xmldata(dtGroup)) + "','" + dtDetailGroup.Rows[i]["GroupName"].ToString() + "','DataSetGroup"+GroupID.ToString()+"');";
                
                ////}
                //scripts = scripts + " Printer.Preview('WorkflowPrintForm_"+WorkflowID+"_"+NodeID+"');</script>";
                //System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "ButtonScript1", scripts, false);
            }

        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {

            string scripts = "<script language='javascript' type='text/javascript'>";
            string RequestID = Request.QueryString["RequestID"].ToString();
            string FormID = Request.QueryString["FormID"].ToString();
            string WorkflowID = Request.QueryString["WorkflowID"].ToString();
            string NodeID = Request.QueryString["WorkflowID"].ToString();
            string sql="";
            //获取数据
            if (WorkflowID=="7")
                sql = "select a.*,CostTypeName=d.LabelWord,CurrencyName=e.LabelWord,PayMethodName=f.LabelWord,IsAllOverLoad=case when a.IsOverLoadBudget='1' then '是' else '否' end,  b.UserName,c.RequestName,ApplyDeptProcess=dbo.fun_GetSignProcessRecord(" + RequestID + ",41,1),FeeDeptProcess=dbo.fun_GetSignProcessRecord(" + RequestID + ",42,1),FinanceProcess=dbo.fun_GetSignProcessRecord(" + RequestID + ",43,1),CEOProcessApply=dbo.fun_GetSignProcessRecord(" + RequestID + ",44,1) from workflow_Form a left outer join userlist b on a.applier=b.userSerialID left outer join workflow_RequestBase c on a.RequestID=c.RequestID left outer join Workflow_FieldDictSelect d on a.CostType=d.SelectNo left outer join Workflow_FieldDictSelect e on a.Currency=e.SelectNo left outer join Workflow_FieldDictSelect f on a.PayMethod=f.SelectNo where a.RequestID='" + RequestID + "'";
            else if (WorkflowID=="6")
                sql = "select a.*,CostTypeName=d.LabelWord,CurrencyName=e.LabelWord,PayMethodName=f.LabelWord,IsAllOverLoad=case when a.IsOverLoadBudget='1' then '是' else '否' end,  b.UserName,c.RequestName,ApplyDeptProcess=dbo.fun_GetSignProcessRecord(" + RequestID + ",34,1),FeeDeptProcess=dbo.fun_GetSignProcessRecord(" + RequestID + ",35,1),FinanceProcess=dbo.fun_GetSignProcessRecord(" + RequestID + ",36,1),CEOProcessApply=dbo.fun_GetSignProcessRecord(" + RequestID + ",37,1) from workflow_Form a left outer join userlist b on a.applier=b.userSerialID left outer join workflow_RequestBase c on a.RequestID=c.RequestID left outer join Workflow_FieldDictSelect d on a.CostType=d.SelectNo  left outer join Workflow_FieldDictSelect e on a.Currency=e.SelectNo left outer join Workflow_FieldDictSelect f on a.PayMethod=f.SelectNo where a.RequestID='" + RequestID + "'";
            else if (WorkflowID == "9")
                sql = "select a.*,CostTypeName=d.LabelWord,CurrencyName=e.LabelWord,PayMethodName=f.LabelWord,IsAllOverLoad=case when a.IsOverLoadBudget='1' then '是' else '否' end,  b.UserName,c.RequestName,ApplyDeptProcess=dbo.fun_GetSignProcessRecord(" + RequestID + ",52,1),FeeDeptProcess=dbo.fun_GetSignProcessRecord(" + RequestID + ",53,1),FinanceProcess=dbo.fun_GetSignProcessRecord(" + RequestID + ",54,1),CEOProcessApply=dbo.fun_GetSignProcessRecord(" + RequestID + ",55,1) from workflow_Form a left outer join userlist b on a.applier=b.userSerialID left outer join workflow_RequestBase c on a.RequestID=c.RequestID left outer join Workflow_FieldDictSelect d on a.CostType=d.SelectNo  left outer join Workflow_FieldDictSelect e on a.Currency=e.SelectNo left outer join Workflow_FieldDictSelect f on a.PayMethod=f.SelectNo where a.RequestID='" + RequestID + "'";


            DataTable dtMain = DbHelper.GetInstance().ExecDataTable(sql);
            scripts = scripts + "LoadXMLFiles('" + (sql_xmldata(dtMain)) + "','表头数据','DataSetMain');";
            //获取各明细组
            DataTable dtDetailGroup = DbHelper.GetInstance().GetDetailFieldGroup(Convert.ToInt32(FormID), Convert.ToInt32(NodeID));
            for (int i = 0; i < dtDetailGroup.Rows.Count; i++)
            {
                int GroupID = Convert.ToInt32(dtDetailGroup.Rows[i]["GroupID"].ToString());
                //DataTable dtGroup = DbHelper.GetInstance().GetDetailFieldValue(Convert.ToInt32(FormID), Convert.ToInt32(RequestID), GroupID);
                DataTable dtGroup = DbHelper.GetInstance().ExecDataTable("select a.*,b.PBDEPDN,IsOver=case when IsOverLoadBudget='1' then '是' else '否' end from workflow_FormDetail a  left outer join PBDEP b on a.FeeDeptCode=b.PBDEPID where RequestID='" + RequestID + "' and GroupID= " + GroupID.ToString());
                scripts = scripts + "LoadXMLFiles('" + (sql_xmldata(dtGroup)) + "','" + dtDetailGroup.Rows[i]["GroupName"].ToString() + "','DataSetGroup" + GroupID.ToString() + "');Printer.AddRelation('DataSetMain.RequestID="+"DataSetGroup" + GroupID.ToString() + ".RequestID');";

            }

            //加签核准记录
            //DataTable dt = DbHelper.GetInstance().GetDBRecords("a.ID,a.NodeID,b.NodeName,OperatorName=case when a.AgentID=0 then c.UserName else c.UserName+'→'+d.UserName end,a.OperateDateTime,OperateTypeN=case when a.OperateType=1 then '提交' else '退回' end,a.OperateComment,a.ReceivList,ReceivListN=''", "Workflow_RequestLog a left join Workflow_FlowNode b on (a.NodeID=b.NodeID and a.WorkflowID=b.WorkflowID) left join UserList c on a.OperatorID=c.UserSerialID left join UserList d on a.AgentID=d.UserSerialID", "a.RequestID=" + RequestID, "a.ID DESC");
            //for (int i = 0; i < dt.Rows.Count; i++)
            //{
            //    string ReceivList = dt.Rows[i]["ReceivList"].ToString();
            //    string ReceivListN = "";
            //    DataTable dtReceivList = DbHelper.GetInstance().GetDBRecords("UserName", "UserList", "UserSerialID in (" + ReceivList + ")", "");
            //    for (int j = 0; j < dtReceivList.Rows.Count; j++)
            //    {
            //        ReceivListN += dtReceivList.Rows[j]["UserName"].ToString() + ",";
            //    }
            //    dt.Rows[i]["ReceivListN"] = ReceivListN.Trim(new char[] { ',' });
            //}
            //scripts = scripts + "LoadXMLFiles('" + (sql_xmldata(dt)) + "','签核记录','DataSetProcess');Printer.AddRelation('DataSetMain.RequestID=DataSetProcess.RequestID');";
            //scripts = scripts + " Printer.Design('WorkflowPrintForm_" + WorkflowID + "_" + NodeID + "');</script>";
            scripts = scripts + " Printer.Preview('WorkflowPrintForm_" + WorkflowID + "_7" + "');</script>";
            System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "ButtonScript1", scripts, false);
        
        }


        ///根据传入的datatable,生成符合打印控件要求的XML
        private string sql_xmldata(DataTable ds)
        {


            string strFieldset = "";
            string[] tmpField = null;
            if (strFieldset.Equals("") == false)
            {
                tmpField = strFieldset.Split(',');
            }

            string strX = "<root>";
            string sTmp = "";
            string sTmp1 = "";
            int iField = 1;
            int iCount = 1;
            try
            {
                string sField = "";
                for (iField = 0; iField < ds.Columns.Count; iField++)
                {
                    sTmp1 = ds.Columns[iField].ColumnName;

                    string sT2 = sTmp1;
                    string sT3 = "15";
                    string sT4 = "0";

                    sTmp1 = sTmp1.ToLower();
                    string sFieldNameTmp = sTmp1;
                    sField += "<field>" + "<fieldname>" + sTmp1 + "</fieldname>";

                    sTmp1 = Convert.ToString(ds.Columns[iField].DataType);
                    sTmp1 = sTmp1.ToLower();
                    if (sTmp1.Equals("system.char") || sTmp1.Equals("system.string")) //char and varchar
                        sTmp1 = "字符";
                    if (sTmp1.Equals("system.decimal"))
                        sTmp1 = "实数";
                    if (sTmp1.Equals("system.int32"))
                        sTmp1 = "整数";

                    sField += "<datatype>" + sTmp1 + "</datatype>"
                        + "<displaylabel>" + sT2 + "</displaylabel>"
                        + "<size>" + sT3
                        + "</size><precision>" + sT4 + "</precision>"
                        + "<fieldkind>数据项</fieldkind><defaultvalue></defaultvalue><displayformat></displayformat><isnull>否</isnull>"
                        + "<iskey>否</iskey>"
                        + "<valid>否</valid><procvalid>否</procvalid><link>否</link><target>_blank</target><href></href><visible>"
                        + dataset_select_sub(tmpField, sFieldNameTmp) + "</visible><primarykey>否</primarykey>"
                        + "</field>";
                }
                for (int j = 0; j < ds.Rows.Count; j++)
                {
                    strX = strX + "<tr>";
                    iCount = ds.Columns.Count;
                    for (iField = 0; iField < iCount; iField++)
                    {
                        string colName = ds.Columns[iField].ColumnName;
                        if (colName == null)
                        {
                            strX = strX + "<fcnull></fcnull>";
                        }
                        else
                        {
                            string sT = ds.Rows[j][iField].ToString();
                            if (sT == null) sT = "";
                            sT = repxml(sT);
                            strX = strX + "<td>" + sT + "</td>";
                        }
                    }
                    strX = strX + "</tr>";
                }
                strX = strX + "<set><pages>1</pages>" + "<fields>" + sField + "</fields>" + "</set>";
                strX = strX + "</root>";
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                sTmp = e.Message;
            }
            return strX;
        }

        /// <summary>
        /// 替换特殊字符&、等
        /// </summary>
        /// <param name="s1"></param>
        /// <returns></returns>
        private static string repxml(string s1)
        {
            s1 = s1.Replace("&", "&amp;");
            s1 = s1.Replace("<", "&lt;");
            s1 = s1.Replace(">", "&gt;");
            s1 = s1.Replace("\r\n", " ");
            return s1;
        }


        /// <summary>
        ///   给dataset_select用来得到当前字段是否显示
        /// </summary>
        /// <param name="tmpField"></param>
        /// <param name="sFieldName"></param>
        /// <returns></returns>
        private string dataset_select_sub(string[] tmpField, string sFieldName)
        {
            if (tmpField != null)
            {
                for (int jjj = 0; jjj < tmpField.Length; jjj++)
                {
                    if (tmpField[jjj].ToUpper().Equals(sFieldName.ToUpper()))
                    {
                        return "否";
                    }
                }
            }
            return "是";
        }
    }
}
