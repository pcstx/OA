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
    public partial class GGB0Add :BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
             if (!Page.IsPostBack)
            {
                 
                bindControlValues();
              
              
            }
        }

        private void bindControlValues()
        {
        DataTable dtReportType = DbHelper.GetInstance().GetDBRecords("ReportTypeID,ReportTypeName", "Workflow_ReportType", "1=1", "DisplayOrder");
        ddlReportType.AddTableData(dtReportType, 0, 1, true, "Select");
            
            }

        protected void btnSubmit_Click(object sender, EventArgs e)
            {
            //保存
            Workflow_ReportMainEntity _rm = new Workflow_ReportMainEntity();
            _rm.ReportID = 0;
            _rm.ReportName = txtReportName.Text.Trim();
            _rm.ReportTypeID = Convert.ToInt32(ddlReportType.SelectedValue);
            _rm.FormID = Convert.ToInt32(txtFormID.Value.Trim());
            _rm.WorkflowID = txtWorkflowID.Value.Trim();

            string sResult = DbHelper.GetInstance().AddWorkflow_ReportMain(_rm);

            if (sResult == " -1")
                {
                string strScript = "<script type='text/javascript'  lanuage='javascript'> alert('" + ResourceManager.GetString("Operation_RECORD") + "'); </script>";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", strScript, false);

                }
            else
                {
                string strScript = "<script type='text/javascript'  lanuage='javascript'> alert('" + ResourceManager.GetString("Button_GoComplete") + "'); </script>";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", strScript, false);

                }  
            divWF.Visible = true;

            System.Web.UI.ScriptManager.RegisterStartupScript(btnSubmit, this.GetType(), "ButtonHideScript", strButtonHideScript, false);

            }
       
    }
}
