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
using AjaxControlToolkit;
using GOA.UserControl;

namespace GOA
{
    public partial class GG500201 : BasePage
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            RefreshDetailGroup();
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        private void RefreshDetailGroup()
        {
            for (int i = TabContainer1.Tabs.Count - 1; i >= 1; i--)
            {
                TabContainer1.Tabs.RemoveAt(i);
            }

            string FormID = DNTRequest.GetString("fmid");
            DataTable dtDetailGroup = DbHelper.GetInstance().GetDBRecords("a.NodeID,d.FormID,d.GroupID,d.GroupName", "Workflow_FlowNode a,Workflow_Base b,Workflow_FormBase c,Workflow_FormFieldGroup d", "a.WorkflowID=b.WorkflowID and b.FormID=c.FormID and c.FormID=d.FormID and a.NodeID=" + DNTRequest.GetString("id"), "d.DisplayOrder");
            for (int i = 0; i < dtDetailGroup.Rows.Count; i++)
            {
                TabPanel tp = new TabPanel();
                DetialFieldControl _DetialFieldControl = (DetialFieldControl)(Page.LoadControl("UserControl/DetialFieldControl.ascx"));
                _DetialFieldControl.id = DNTRequest.GetString("id");
                _DetialFieldControl.gid = dtDetailGroup.Rows[i]["GroupID"].ToString();
                tp.Controls.Add(_DetialFieldControl);
                TabContainer1.Tabs.Add(tp);
                TabContainer1.Tabs[1 + i].HeaderText = string.Format("明细字段({0})", dtDetailGroup.Rows[i]["GroupName"]);
            }
        }
    }
}
