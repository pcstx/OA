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
    public partial class GG5001 : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                DataTable dtFormType = DbHelper.GetInstance().GetDBRecords("*", "Workflow_FormType", "Useflag='1'", "DisplayOrder");
                ddlFlowTypeID.AddTableData(dtFormType, 0, 1, true, "Select");
                LoadBaseInfo();
            }
        }
        private void LoadBaseInfo()
        {
            string WorkflowID = DNTRequest.GetString("id");
            Workflow_BaseEntity _Workflow_BaseEntity = DbHelper.GetInstance().GetWorkflow_BaseEntityByKeyCol(WorkflowID);
            txtWorkflowID.Value = _Workflow_BaseEntity.WorkflowID.ToString();
            txtWorkflowName.Text = _Workflow_BaseEntity.WorkflowName;
            txtWorkflowDesc.Text = _Workflow_BaseEntity.WorkflowDesc;
            ddlFlowTypeID.SelectedValue = _Workflow_BaseEntity.FlowTypeID.ToString();
            string FormID = _Workflow_BaseEntity.FormID.ToString();
            txtFormID.Value = FormID;
            Workflow_FormBaseEntity _Workflow_FormBaseEntity = DbHelper.GetInstance().GetWorkflow_FormBaseEntityByKeyCol(FormID);
            txtFormN.Text = _Workflow_FormBaseEntity.FormName;
            chkIsValid.Checked = _Workflow_BaseEntity.IsValid == 1;
            chkIsMsgNotice.Checked = _Workflow_BaseEntity.IsMsgNotice == 1;
            chkIsMailNotice.Checked = _Workflow_BaseEntity.IsMailNotice == 1;
            chkIsTransfer.Checked = _Workflow_BaseEntity.IsTransfer == 1;
            txtDisplayOrder.Text = _Workflow_BaseEntity.DisplayOrder.ToString();
        }

        protected void hideModalPopupViaServer_Click(object sender, EventArgs e)
        {
            GPRP.GPRPControls.Button btn = (GPRP.GPRPControls.Button)sender;
            string sResult = "-1";
            if (btn.ID == "btnSubmit")
            {
                Workflow_BaseEntity _Workflow_BaseEntity = new Workflow_BaseEntity();
                _Workflow_BaseEntity.WorkflowID = txtWorkflowID.Value != string.Empty ? Convert.ToInt32(txtWorkflowID.Value) : 0;
                _Workflow_BaseEntity.WorkflowName = txtWorkflowName.Text;
                _Workflow_BaseEntity.WorkflowDesc = txtWorkflowDesc.Text;
                _Workflow_BaseEntity.FlowTypeID = Convert.ToInt32(ddlFlowTypeID.SelectedValue);
                _Workflow_BaseEntity.FormID = Convert.ToInt32(txtFormID.Value);
                _Workflow_BaseEntity.IsValid = chkIsValid.Checked ? 1 : 0;
                _Workflow_BaseEntity.IsMailNotice = chkIsMailNotice.Checked ? 1 : 0;
                _Workflow_BaseEntity.IsMsgNotice = chkIsMsgNotice.Checked ? 1 : 0;
                _Workflow_BaseEntity.IsTransfer = chkIsTransfer.Checked ? 1 : 0;
                _Workflow_BaseEntity.AttachDocPath = 0;
                _Workflow_BaseEntity.HelpDocPath = 0;
                _Workflow_BaseEntity.DisplayOrder = Convert.ToInt32(txtDisplayOrder.Text);
                sResult = DbHelper.GetInstance().UpdateWorkflow_Base(_Workflow_BaseEntity);
                if (sResult != "1")
                {
                    lblMsg.Text = ResourceManager.GetString("Operation_RECORD") + ":" + sResult;
                }
            }

            System.Web.UI.ScriptManager.RegisterStartupScript(btn, this.GetType(), "ButtonHideScript", strButtonHideScript, false);
        }
    }
}
