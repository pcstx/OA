using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using GPRP.Web.UI;
using GPRP.GPRPComponents;
using MyADO;
using GPRP.GPRPControls;
using GPRP.GPRPEnumerations;
using GPRP.GPRPBussiness;
using GPRP.Entity;
using YYControls;
using VBControls.VBProject;

namespace GOA
    {
    public partial class AW30 :BasePage
        {
        protected void Page_Load(object sender, EventArgs e)
            {
            if (!IsPostBack)
                {
                BindMain();
                }

            }


        private void BindMain()
            {
//            //未完成


            string sqlUMain = @" SELECT b.FlowTypeID  ,ft.FormTypeName,TotalNum=count(*)
 FROM  Workflow_RequestAgentDetail a
 inner join Workflow_RequestBase rb  on a.RequestID=rb.RequestID and a.OperatorID=rb.Creator
 inner join Workflow_Base b on  a.WorkflowID=b.WorkflowID
 inner join Workflow_FormType ft on  b.FlowTypeID=ft.FormTypeID
 where   a.IsCancel=0  and  (a.AgentID=" + userEntity.UserSerialID + "  or a.OperatorID=" + userEntity.UserSerialID + ") 	and  rb.CurrentNodeType<>4   and (a.OperatorID 	in (select * from Fun_GetIDTableByString(rb.CurrentOperatorID)) or  a.OperatorID in (select * from Fun_GetIDTableByString(rb.AllParticipator))) group by b.FlowTypeID  ,ft.FormTypeName";
            
            DataTable udt = new DataTable();
            udt = DbHelper.GetInstance().ExecDataTable(sqlUMain);

            dlUMain.DataSource = udt.DefaultView;
            dlUMain.DataBind();
            if (udt != null)
                {
                int cnt = 0;
                for (int i = 0; i < udt.Rows.Count; i++)
                    {
                    cnt += Int32.Parse(udt.Rows[i]["TotalNum"].ToString());
                    }
                this.lblUnfinish.Text += " （" + cnt.ToString() + "）";
                }

            //办结事宜
//            string sqlMain = @"SELECT b.FlowTypeID  ,ft.FormTypeName,TotalNum=count(*)
// FROM  Workflow_RequestAgentDetail a
// inner join Workflow_RequestBase rb on a.RequestID=rb.RequestID  
// inner join Workflow_Base b on  a.WorkflowID=b.WorkflowID
// inner join Workflow_FormType ft on  b.FlowTypeID=ft.FormTypeID
// where  rb.CurrentNodeType=4 and a.IsCancel=0 and  rb.IsCancel=0 
// and (a.AgentID=" + userEntity.UserSerialID + "  or a.OperatorID=" + userEntity.UserSerialID + ")  and  a.OperatorID   in (select * from Fun_GetIDTableByString(rb.AllParticipator))   group by  b.FlowTypeID  ,ft.FormTypeName ";

            string sqlMain = @"SELECT b.FlowTypeID  ,ft.FormTypeName,TotalNum=count(*)
 FROM  Workflow_RequestAgentDetail a
 inner join Workflow_RequestLog rl on a.RequestID=rl.RequestID and a.OperatorID=rl.OperatorID
 inner join Workflow_RequestBase rb on a.RequestID=rb.RequestID  and a.OperatorID=rb.Creator
  inner join Workflow_Base b on  a.WorkflowID=b.WorkflowID
 inner join Workflow_FormType ft on  b.FlowTypeID=ft.FormTypeID
 where  rb.CurrentNodeType=4 and a.IsCancel=0 and  rb.IsCancel=0 
 and  rl.AgentID=a.AgentID  and  (a.AgentID=" + userEntity.UserSerialID + "  or a.OperatorID=" + userEntity.UserSerialID + ")  and  a.OperatorID   in (select * from Fun_GetIDTableByString(rb.AllParticipator))   group by  b.FlowTypeID  ,ft.FormTypeName";

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
                lblFinish.Text += " （" + cnt.ToString() + "）";
                }
            }

        //未完成
        protected void dlUMain_ItemDataBound(object sender, DataListItemEventArgs e)
            {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {
                string FlowTypeID = ((DataRowView)e.Item.DataItem).Row["FlowTypeID"].ToString();
                Repeater repeaterUSub = (Repeater)e.Item.FindControl("repeaterUSub");
                if (repeaterUSub != null)
                    {
                    string sql = @"SELECT a.WorkflowID  ,b.WorkflowName,TotalNum=count(*)
 FROM  Workflow_RequestAgentDetail a
inner join Workflow_RequestBase rb on a.RequestID=rb.RequestID   and a.OperatorID=rb.Creator
inner join Workflow_Base b on  a.WorkflowID=b.WorkflowID
inner join Workflow_FormType ft on  b.FlowTypeID=ft.FormTypeID
 where  rb.CurrentNodeType<>4 and a.IsCancel=0 and  rb.IsCancel=0 
 and b.FlowTypeID=" + FlowTypeID + "  and (a.AgentID=" + userEntity.UserSerialID + "  or a.OperatorID=" + userEntity.UserSerialID + ")  and (a.OperatorID   in (select * from Fun_GetIDTableByString(rb.CurrentOperatorID)) or  a.OperatorID in (select * from Fun_GetIDTableByString(rb.AllParticipator)))    group by a.WorkflowID  ,b.WorkflowName";

                    repeaterUSub.DataSource = DbHelper.GetInstance().ExecDataTable(sql);
                    repeaterUSub.DataBind();
                    }
                }
            }
        //已完成
        protected void dlMain_ItemDataBound(object sender, DataListItemEventArgs e)
            {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {
                string FlowTypeID = ((DataRowView)e.Item.DataItem).Row["FlowTypeID"].ToString();
                Repeater repeaterSub = (Repeater)e.Item.FindControl("repeaterSub");
                if (repeaterSub != null)
                    {
                   string  sql = @"SELECT a.WorkflowID  ,b.WorkflowName,TotalNum=count(*)
 FROM  Workflow_RequestAgentDetail a
 inner join Workflow_RequestLog rl on a.RequestID=rl.RequestID and a.OperatorID=rl.OperatorID 
inner join Workflow_RequestBase rb on a.RequestID=rb.RequestID  and a.OperatorID=rb.Creator
inner join Workflow_Base b on  a.WorkflowID=b.WorkflowID
 where  rb.CurrentNodeType=4 and a.IsCancel=0 and  rb.IsCancel=0 and  rl.AgentID=a.AgentID  and b.FlowTypeID=" + FlowTypeID + " (a.AgentID=" + userEntity.UserSerialID + "  or a.OperatorID=" + userEntity.UserSerialID + ")   and  a.OperatorID in (select * from Fun_GetIDTableByString(rb.AllParticipator))  group by 	a.WorkflowID  ,b.WorkflowName";

                    repeaterSub.DataSource = DbHelper.GetInstance().ExecDataTable(sql);
                    repeaterSub.DataBind();
                    }
                }
            }
        }
    }
