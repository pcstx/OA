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
    public partial class MW30 : BasePage
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
            //未完成
            string sqlUMain = @"SELECT distinct	b.FlowTypeID,   ft.FormTypeName,TotalNum=count(*)
                        FROM  Workflow_RequestBase cl
                        left join  Workflow_Base b on  cl.WorkflowID=b.WorkflowID
                        left join Workflow_FormType ft on  b.FlowTypeID=ft.FormTypeID
                        where cl.CurrentNodeType<>4 and cl.IsCancel=0 and cl.Creator=" + userEntity.UserSerialID + " group by 	b.FlowTypeID,	ft.FormTypeName ";

            DataTable udt = DbHelper.GetInstance().ExecDataTable(sqlUMain);

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
            string sqlMain = @"SELECT distinct	b.FlowTypeID,   ft.FormTypeName,TotalNum=count(*)
                        FROM  Workflow_RequestBase cl
                        left join  Workflow_Base b on  cl.WorkflowID=b.WorkflowID
                        left join Workflow_FormType ft on  b.FlowTypeID=ft.FormTypeID
                        where cl.CurrentNodeType=4 and cl.IsCancel=0 and  cl.Creator=" + userEntity.UserSerialID +
                   "   group by 	b.FlowTypeID,	ft.FormTypeName ";


            DataTable dt = DbHelper.GetInstance().ExecDataTable(sqlMain);
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
                    string sql = @"SELECT cl.WorkflowID  ,b.WorkflowName,TotalNum=count(*)
                                                    FROM  Workflow_RequestBase cl
                                                    left join  Workflow_Base b on  cl.WorkflowID=b.WorkflowID
                                                  
                                                    where  cl.CurrentNodeType<>4 and cl.IsCancel=0 and   cl.Creator=" + userEntity.UserSerialID + "  and b.FlowTypeID=" + (FlowTypeID) + " group by 	cl.WorkflowID  ,b.WorkflowName";

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
                    string sql = @"SELECT cl.WorkflowID  ,b.WorkflowName,TotalNum=count(*)
                                    FROM  Workflow_RequestBase cl
                                    left join  Workflow_Base b on  cl.WorkflowID=b.WorkflowID
                                    where  cl.CurrentNodeType=4 and cl.IsCancel=0 and   cl.Creator=" + userEntity.UserSerialID + "   and b.FlowTypeID=" + (FlowTypeID) + " group by 	cl.WorkflowID  ,b.WorkflowName";

                    repeaterSub.DataSource = DbHelper.GetInstance().ExecDataTable(sql);
                    repeaterSub.DataBind();
                }
            }
        }
    }
}
