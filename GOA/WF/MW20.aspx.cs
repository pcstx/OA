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
    public partial class MW20 : BasePage
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
                sqlMain = @"SELECT distinct	b.FlowTypeID,   ft.FormTypeName,TotalNum=count(*)
                        FROM  Workflow_RequestBase cl
                        left join  Workflow_Base b on  cl.WorkflowID=b.WorkflowID
                        left join Workflow_FormType ft on  b.FlowTypeID=ft.FormTypeID
                        where cl.CurrentNodeType<>4 and cl.IsCancel=0 and " + userEntity.UserSerialID +
                       " in (select * from Fun_GetIDTableByString(cl.CurrentOperatorID))  group by 	b.FlowTypeID,	ft.FormTypeName ";

                lbltitle.Text = "待办事宜 * 查看";
            }
            else if (type == "2")//已办
            {
                sqlMain = @"SELECT distinct	b.FlowTypeID,   ft.FormTypeName,TotalNum=count(*)
                        FROM  Workflow_RequestBase cl
                        left join  Workflow_Base b on  cl.WorkflowID=b.WorkflowID
                        left join Workflow_FormType ft on  b.FlowTypeID=ft.FormTypeID
                        where cl.CurrentNodeType<>4 and cl.IsCancel=0 and   " + userEntity.UserSerialID + " in (select * from Fun_GetIDTableByString(cl.AllParticipator)) and  " + userEntity.UserSerialID + " not in (select * from Fun_GetIDTableByString(cl.CurrentOperatorID))  group by 	b.FlowTypeID,	ft.FormTypeName ";
                lbltitle.Text = "已办事宜 * 查看";
            }
            else if (type == "3")//办结
            {
                sqlMain = @"SELECT distinct	b.FlowTypeID,   ft.FormTypeName,TotalNum=count(*)
                        FROM  Workflow_RequestBase cl
                        left join  Workflow_Base b on  cl.WorkflowID=b.WorkflowID
                        left join Workflow_FormType ft on  b.FlowTypeID=ft.FormTypeID
                        where cl.CurrentNodeType=4 and cl.IsCancel=0 and " + userEntity.UserSerialID +
                   " in (select * from Fun_GetIDTableByString(cl.AllParticipator))  group by 	b.FlowTypeID,	ft.FormTypeName ";
                lbltitle.Text = "办结事宜 * 查看";
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
                        sql = @"SELECT cl.WorkflowID  ,b.WorkflowName,TotalNum=count(*)
                                                    FROM  Workflow_RequestBase cl
                                                    left join  Workflow_Base b on  cl.WorkflowID=b.WorkflowID
                                                 
                                                    where  cl.CurrentNodeType<>4 and cl.IsCancel=0 and  " + userEntity.UserSerialID + " in (select * from Fun_GetIDTableByString(cl.CurrentOperatorID))  and b.FlowTypeID=" + FlowTypeID + " group by 	cl.WorkflowID  ,b.WorkflowName";


                    }
                    else if (type == "2")//已办
                    {
                        sql = @"SELECT cl.WorkflowID  ,b.WorkflowName,TotalNum=count(*)
                                    FROM  Workflow_RequestBase cl
                                    left join  Workflow_Base b on  cl.WorkflowID=b.WorkflowID
                                    where  cl.CurrentNodeType<>4 and cl.IsCancel=0 and  " + userEntity.UserSerialID + " in (select * from Fun_GetIDTableByString(cl.AllParticipator)) and  " + userEntity.UserSerialID + " not in (select * from Fun_GetIDTableByString(cl.CurrentOperatorID)) and b.FlowTypeID=" + FlowTypeID + " group by 	cl.WorkflowID  ,b.WorkflowName";
                    }
                    else if (type == "3")//办结
                    {
                        sql = @"SELECT cl.WorkflowID  ,b.WorkflowName,TotalNum=count(*)
                                    FROM  Workflow_RequestBase cl
                                    left join  Workflow_Base b on  cl.WorkflowID=b.WorkflowID
                                    where  cl.CurrentNodeType=4 and cl.IsCancel=0 and  " + userEntity.UserSerialID + " in (select * from Fun_GetIDTableByString(cl.AllParticipator))  and b.FlowTypeID=" + FlowTypeID + " group by 	cl.WorkflowID  ,b.WorkflowName";
                    }
                    repeaterSub.DataSource = DbHelper.GetInstance().ExecDataTable(sql);
                    repeaterSub.DataBind();
                }
            }
        }
    }
}
