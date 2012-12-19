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
    public partial class AW60 : BasePage
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

            string sqlMain = @"SELECT distinct	b.FlowTypeID,   ft.FormTypeName
                        FROM  Workflow_CreatorList cl
                        left join  Workflow_Base b on  cl.WorkflowID=b.WorkflowID
                        left join Workflow_FormType ft on  b.FlowTypeID=ft.FormTypeID
                        where cl.IsCancel='0'and cl.UserSerialID=" + userEntity.UserSerialID;

            DataTable dt = new DataTable();
            dt = DbHelper.GetInstance().ExecDataTable(sqlMain);

            dlMain.DataSource = dt.DefaultView;
            dlMain.DataBind();
        }

        protected void dlMain_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                string FlowTypeID = ((DataRowView)e.Item.DataItem).Row["FlowTypeID"].ToString();
                DataList dlSub = (DataList)e.Item.FindControl("dlSub");
                if (dlSub != null)
                {
                    string sql = @"SELECT cl.WorkflowID  ,b.WorkflowName
                                    FROM  Workflow_CreatorList cl
                                    left join  Workflow_Base b on  cl.WorkflowID=b.WorkflowID
                                    left join Workflow_FormType ft on  b.FlowTypeID=ft.FormTypeID
                                    where cl.IsCancel='0' and cl.UserSerialID=" + userEntity.UserSerialID + " and b.FlowTypeID=" + Convert.ToInt32(FlowTypeID);

                    dlSub.DataSource = DbHelper.GetInstance().ExecDataTable(sql);
                    dlSub.DataBind();
                }

                if (e.Item.ItemIndex % 2 == 0 && e.Item.ItemIndex > 0)
                {
                    e.Item.Controls.Add(new LiteralControl("</tr><tr>"));
                }
            }
        }
    }
}
