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
using MyADO;

namespace GOA
{
    public partial class AW20 : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["type"].ToString() != "")
                {
                    ViewState["type"] = Request.QueryString["type"].ToString();
                    BindMain();
                }
            }

        }

        private void BindMain()
        {

            string sqlMain = "";
            string type = ViewState["type"].ToString();
            if (type == "1")//待办
            {
                sqlMain = @"SELECT b.FlowTypeID  ,ft.FormTypeName,TotalNum=count(*)
 FROM  Workflow_RequestAgentDetail a
 inner join Workflow_RequestBase rb on a.RequestID=rb.RequestID and a.NodeID=rb.CurrentNodeID and isnull(a.DeptLevel,0)=rb.CurrentDeptLevel
 inner join Workflow_Base b on  a.WorkflowID=b.WorkflowID
 inner join Workflow_FormType ft on  b.FlowTypeID=ft.FormTypeID
 where  rb.CurrentNodeType<>4 and a.IsCancel=0 and  rb.IsCancel=0 
 and (a.AgentID=" + userEntity.UserSerialID + " or a.OperatorID=" + userEntity.UserSerialID + " ) and a.OperatorID in (select * from Fun_GetIDTableByString(rb.CurrentOperatorID)) group by  b.FlowTypeID  ,ft.FormTypeName ";

                lbltitle.Text = "代理待办事宜 * 查看";
            }
            else if (type == "2")//已办
            {
                sqlMain = @"SELECT b.FlowTypeID  ,ft.FormTypeName,TotalNum=count(*)
 FROM  Workflow_RequestAgentDetail a
 inner join Workflow_RequestLog rl on a.RequestID=rl.RequestID and a.OperatorID=rl.OperatorID
 inner join Workflow_Base b on  a.WorkflowID=b.WorkflowID
 inner join Workflow_FormType ft on  b.FlowTypeID=ft.FormTypeID
 where  rl.AgentID=a.AgentID  and a.IsCancel=0  and (a.AgentID=" + userEntity.UserSerialID + "  or a.OperatorID=" + userEntity.UserSerialID + ")   group by  b.FlowTypeID  ,ft.FormTypeName ";

                lbltitle.Text = "代理已办事宜 * 查看";
            }
            else if (type == "3")//办结
            {
                sqlMain = @"SELECT b.FlowTypeID  ,ft.FormTypeName,TotalNum=count(*)
 FROM  Workflow_RequestAgentDetail a
 inner join Workflow_RequestLog rl on a.RequestID=rl.RequestID and a.OperatorID=rl.OperatorID
 inner join Workflow_RequestBase rb on a.RequestID=rb.RequestID 
  inner join Workflow_Base b on  a.WorkflowID=b.WorkflowID
 inner join Workflow_FormType ft on  b.FlowTypeID=ft.FormTypeID
 where  rb.CurrentNodeType=4 and a.IsCancel=0 and  rb.IsCancel=0 
 and  rl.AgentID=a.AgentID  and  (a.AgentID=" + userEntity.UserSerialID + "  or a.OperatorID=" + userEntity.UserSerialID + ")  and  a.OperatorID   in (select * from Fun_GetIDTableByString(rb.AllParticipator))   group by  b.FlowTypeID  ,ft.FormTypeName ";
                lbltitle.Text = "代理办结事宜 * 查看";
            }

            DataTable dt = new DataTable();
            dt = DbHelper.GetInstance().ExecDataTable(sqlMain);

            dlMain.DataSource = dt.DefaultView;
            dlMain.DataBind();
            if (dt != null)
            {
                int cnt = 0;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    cnt += Int32.Parse(dt.Rows[i]["TotalNum"].ToString());
                }
                lbltitle.Text += " （" + cnt.ToString() + "）";
            }

        }

        protected void dlMain_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                string FlowTypeID = ((DataRowView)e.Item.DataItem).Row["FlowTypeID"].ToString();
                Repeater repeaterSub = (Repeater)e.Item.FindControl("repeaterSub");
                if (repeaterSub != null)
                {

                    string sql = "";
                    string type = ViewState["type"].ToString();
                    if (type == "1")//待办
                    {
                        sql = @"SELECT a.WorkflowID  ,b.WorkflowName,TotalNum=count(*)
 FROM  Workflow_RequestAgentDetail a
inner join Workflow_RequestBase rb on a.RequestID=rb.RequestID and a.NodeID=rb.CurrentNodeID and isnull(a.DeptLevel,0)=rb.CurrentDeptLevel
inner join Workflow_Base b on  a.WorkflowID=b.WorkflowID
 where  rb.CurrentNodeType<>4 and a.IsCancel=0 and  rb.IsCancel=0 
 and b.FlowTypeID=" + FlowTypeID + " and (a.AgentID=" + userEntity.UserSerialID + "  or a.OperatorID=" + userEntity.UserSerialID + " )  and a.OperatorID in (select * from Fun_GetIDTableByString(rb.CurrentOperatorID)) group by 	a.WorkflowID  ,b.WorkflowName";
                    }
                    else if (type == "2")//已办
                    {
                        sql = @"SELECT a.WorkflowID  ,b.WorkflowName,TotalNum=count(*)
 FROM  Workflow_RequestAgentDetail a
inner join Workflow_RequestLog rl on a.RequestID=rl.RequestID and a.OperatorID=rl.OperatorID
inner join Workflow_Base b on  a.WorkflowID=b.WorkflowID
 where   a.IsCancel=0  and  rl.AgentID=a.AgentID  and b.FlowTypeID=" + FlowTypeID + " and (a.AgentID=" + userEntity.UserSerialID + "  or a.OperatorID=" + userEntity.UserSerialID + ")    group by 	a.WorkflowID  ,b.WorkflowName";
                    }
                    else if (type == "3")//办结
                    {
                        sql = @"SELECT a.WorkflowID  ,b.WorkflowName,TotalNum=count(*)
 FROM  Workflow_RequestAgentDetail a
 inner join Workflow_RequestLog rl on a.RequestID=rl.RequestID and a.OperatorID=rl.OperatorID 
inner join Workflow_RequestBase rb on a.RequestID=rb.RequestID 
inner join Workflow_Base b on  a.WorkflowID=b.WorkflowID
 where  rb.CurrentNodeType=4 and a.IsCancel=0 and  rb.IsCancel=0 and  rl.AgentID=a.AgentID  and b.FlowTypeID=" + FlowTypeID + " (a.AgentID=" + userEntity.UserSerialID + "  or a.OperatorID=" + userEntity.UserSerialID + ")   and  a.OperatorID in (select * from Fun_GetIDTableByString(rb.AllParticipator))  group by 	a.WorkflowID  ,b.WorkflowName";
                    }
                    repeaterSub.DataSource = DbHelper.GetInstance().ExecDataTable(sql);
                    repeaterSub.DataBind();
                }

            }

        }

    }
}
