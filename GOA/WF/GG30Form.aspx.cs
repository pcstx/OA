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
    public partial class GG30Form : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                DataTable dtFormType = DbHelper.GetInstance().GetDBRecords("*", "Workflow_FormType", "Useflag='1'", "DisplayOrder");
                ddlFormTypeID.AddTableData(dtFormType, 0, 1, true, "Select");
                LoadBaseInfo();
            }
        }
        private void LoadBaseInfo()
        {
            string FormID = DNTRequest.GetString("id");
            Workflow_FormBaseEntity _FormBaseEntity = DbHelper.GetInstance().GetWorkflow_FormBaseEntityByKeyCol(FormID);
            txtFormID.Value = _FormBaseEntity.FormID.ToString();
            txtFormName.Text = _FormBaseEntity.FormName;
            txtFormDesc.Text = _FormBaseEntity.FormDesc;
            ddlFormTypeID.SelectedValue = _FormBaseEntity.FormTypeID.ToString();
            txtDisplayOrder.Text = _FormBaseEntity.DisplayOrder.ToString();
            chkUseFlag.Checked = _FormBaseEntity.Useflag.Equals("1");
        }

        protected void hideModalPopupViaServer_Click(object sender, EventArgs e)
        {
            GPRP.GPRPControls.Button btn = (GPRP.GPRPControls.Button)sender;
            if (btn.ID == "btnSubmit")
            {
                Workflow_FormBaseEntity _FormBaseEntity = new Workflow_FormBaseEntity();
                _FormBaseEntity.FormID = Convert.ToInt32(txtFormID.Value != string.Empty ? txtFormID.Value : "0");
                _FormBaseEntity.FormName = txtFormName.Text;
                _FormBaseEntity.FormDesc = txtFormDesc.Text;
                _FormBaseEntity.FormTypeID = Convert.ToInt32(ddlFormTypeID.SelectedValue);
                _FormBaseEntity.DisplayOrder = Convert.ToInt32(txtDisplayOrder.Text);
                _FormBaseEntity.Useflag = chkUseFlag.Checked ? "1" : "0";
                _FormBaseEntity.Creator = userEntity.UserID;
                _FormBaseEntity.CreateDate = DateTime.Now;
                _FormBaseEntity.lastModifier = userEntity.UserID;
                _FormBaseEntity.lastModifyDate = DateTime.Now;
                string sResult = DbHelper.GetInstance().UpdateWorkflow_FormBase(_FormBaseEntity);
                if (sResult != "1")
                {
                    lblMsg.Text = ResourceManager.GetString("Operation_RECORD") + ":" + sResult;
                }
            }

            System.Web.UI.ScriptManager.RegisterStartupScript(btn, this.GetType(), "ButtonHideScript", strButtonHideScript, false);
        }
    }
}
