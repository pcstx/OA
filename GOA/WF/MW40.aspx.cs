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
    public partial class MW40 : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataTable dtNodeType = DbHelper.GetInstance().GetDBRecords("*", "Workflow_NodeType", "1=1", "DisplayOrder");
                ddlNodeTypeID.AddTableData(dtNodeType, 0, 1, true, "Select");


                DataTable dtFormType = DbHelper.GetInstance().GetDBRecords("*", "Workflow_FormType", "1=1", "DisplayOrder");
                ddlFormType.AddTableData(dtFormType, 0, 1, true, "Select");

            }
        }


        protected void btnQuery_Click(object sender, EventArgs e)
        {
            string url = "MW50.aspx?Type=5&RequestID=" + txtRequestID.Text;
            url += "&IsCancel=" + ddlStatus.SelectedValue;
            url += "&FormTypeID=" + ddlFormType.SelectedValue;
            url += "&WorkflowID=" + txtWorkflowID.Value;
            url += "&NodeTypeID=" + ddlNodeTypeID.SelectedValue;
            url += "&CreatorID=" + txtCreatorID.Value;
            url += "&StartDate=" + txtStartDate.Text;
            url += "&EndDate=" + txtEndDate.Text;
            url += "&DeptID=" + txtDeptID.Value;
            url += "&Title=" + Server.UrlEncode(txtFormTitle.Text);

            Response.Redirect(url);

        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            txtWorkflowID.Value = "";
            txtWorkflowN.Text = "";

            txtRequestID.Text = "";
            ddlStatus.SelectedIndex = -1;
            ddlNodeTypeID.SelectedIndex = -1;
            ddlFormType.SelectedIndex = -1;


            txtStartDate.Text = "";
            txtEndDate.Text = "";

            txtCreator.Text = "";
            txtCreatorID.Value = "";

            txtDeptID.Value = "";
            txtDeptName.Text = "";

            txtFormTitle.Text = "";
        }

    }


}
