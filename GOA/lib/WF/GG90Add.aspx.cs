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
    public partial class GG90Add :BasePage   
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Request.QueryString["AgentID"]!=null && Request.QueryString["AgentID"].ToString()!="")
                {
                    string keycol= Request.QueryString["AgentID"];
                    ViewState["status"]="Update";
                    SetControls(keycol);
                }
                else
                {
                this.divIsCancel.Visible=false;
                    ViewState["status"]="Add";
                    txtBeAgentPersonID.Value  = userEntity.UserSerialID.ToString() ;
                    txtBeAgentPersonName.Text = DbHelper.GetInstance().GetUserListEntityByKeyCol(userEntity.UserSerialID.ToString()).UserName;
                }
            }
        }

        private void SetControls(string keycol)
        {
            divIsCancel.Visible = true;
            string WCondition = string.Format("AgentID={0}", keycol);
            DataTable dt = DbHelper.GetInstance().GetDBRecords(" * ", "V_AgentInfo", WCondition, "");
            if (dt.Rows.Count > 0)
            {
               
                txtWorkflowName.Text = dt.Rows[0]["WorkflowName"].ToString();
                txtBeAgentPersonName.Text = dt.Rows[0]["BeAgentPersonName"].ToString();
                txtAgentPersonName.Text = dt.Rows[0]["AgentPersonName"].ToString();
                txtAgentID.Value = keycol; //代理设置编号
                txtWorkflowID.Value = dt.Rows[0]["WorkflowID"].ToString(); //流程编号
                txtBeAgentPersonID.Value = dt.Rows[0]["BeAgentPersonID"].ToString(); //被代理人的员工编号
                txtAgentPersonID.Value = dt.Rows[0]["AgentPersonID"].ToString(); //代理人的员工编号
                if (dt.Rows[0]["AgentStartDate"].ToString() != null) txtAgentStartDate.Text = dt.Rows[0]["AgentStartDate"].ToString(); //代理日期起
                if (dt.Rows[0]["AgentEndDate"].ToString() != null) txtAgentEndDate.Text = dt.Rows[0]["AgentEndDate"].ToString(); //代理日期止
                lblIsCancel.Text = dt.Rows[0]["IsCancel"].ToString() == "1" ? "是" : "否"; //是否已取消
                chkAllowCycle.Checked = (dt.Rows[0]["AllowCycle"].ToString() == "1"); //是否允许递归代理
                chkAllowCreate.Checked = (dt.Rows[0]["AllowCreate"].ToString() == "1"); //是否允许代理人发起流程
             //   txtWorkflowName.ReadOnly = true;
                ImgWorkflowID.Visible = false;

                if (dt.Rows[0]["IsCancel"].ToString() == "1")
                {
                    setTextStatus();
                }
            }
            else
            {

            }
        }

        private void setTextStatus()
        {
            btnSubmit.Visible = false;
            btnClear.Visible = false;

           // txtBeAgentPersonName.ReadOnly = true;
             ImgBeAgentPerson.Visible  = false ;
           //  txtAgentPersonName.ReadOnly = true;
             ImgAgentPerson.Visible = false;

          //   txtAgentStartDate.ReadOnly = true;
             AgentStartDateCalendarExtender.Enabled  = false;
           //  txtAgentEndDate.ReadOnly = true;
             AgentEndDateCalendarExtender.Enabled  = false;

            chkAllowCycle.Enabled=false;  //是否允许递归代理
            chkAllowCreate.Enabled=false ; //是否允许代理人发起流程
        }
    
        //此类一般不需要更改,因为保存的工作全部放在下面的SaveData中
     protected void btnSubmit_Click(object sender, EventArgs e)
     {
         string sResult = "-1";

         //保存
         Workflow_AgentSettingEntity _Workflow_AgentSettingEntity = new Workflow_AgentSettingEntity();
         _Workflow_AgentSettingEntity.AgentID = Convert.ToInt32((txtAgentID.Value == "") ? "0" : txtAgentID.Value);
         _Workflow_AgentSettingEntity.BeAgentPersonID = Convert.ToInt32(txtBeAgentPersonID.Value);
         _Workflow_AgentSettingEntity.AgentPersonID =  Convert.ToInt32(txtAgentPersonID.Value);
         if (txtAgentStartDate.Text.ToString() != "")
             _Workflow_AgentSettingEntity.AgentStartDate = Convert.ToDateTime(txtAgentStartDate.Text.ToString());
         else
             _Workflow_AgentSettingEntity.AgentStartDate = Convert.ToDateTime( System.DBNull.Value)  ;

         if (txtAgentEndDate.Text.ToString() != "")
             _Workflow_AgentSettingEntity.AgentEndDate = Convert.ToDateTime(txtAgentEndDate.Text.ToString());
         else
             _Workflow_AgentSettingEntity.AgentEndDate = Convert.ToDateTime( System.DBNull.Value)  ;

         _Workflow_AgentSettingEntity.CreateDate = Convert.ToDateTime(nowdatetime);
         _Workflow_AgentSettingEntity.Creator = userEntity.UserSerialID ;
         _Workflow_AgentSettingEntity.AllowCycle = chkAllowCycle.Checked ? "1" : "0";
         _Workflow_AgentSettingEntity.AllowCreate = chkAllowCreate.Checked ? "1" : "0";

         if (JudgeAgentBeforeAddAndUpdate(_Workflow_AgentSettingEntity))
             {
             sResult = SaveData(_Workflow_AgentSettingEntity);
             if (sResult == " -1")
                 {
                 // lblMsg.Text = ResourceManager.GetString("Operation_RECORD");
                 string strScript = "<script type='text/javascript'  lanuage='javascript'> alert('" + ResourceManager.GetString("Operation_RECORD") + "'); </script>";
                 ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", strScript, false);

                 }
             else
                 {
                 // lblMsg.Text = ResourceManager.GetString("");
                 string strScript = "<script type='text/javascript'  lanuage='javascript'> alert('" + ResourceManager.GetString("Button_GoComplete") + "'); </script>";

                 ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", strScript, false);

                 }
             }
         else
         //   lblMsg.Text = ResourceManager.GetString("REPLICATED_DATA");
             {
             string strScript = "<script type='text/javascript'  lanuage='javascript'> alert('" + ResourceManager.GetString("REPLICATED_DATA") + "'); </script>";
             ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", strScript, false);
             }


         System.Web.UI.ScriptManager.RegisterStartupScript(btnSubmit, this.GetType(), "ButtonHideScript", strButtonHideScript, false);
        
     }
    
       private  bool  JudgeAgentBeforeAddAndUpdate(Workflow_AgentSettingEntity _ASE )
        {
            string sResult = "-1";
            if (txtWorkflowID.Value.Trim().IndexOf(",") > 0)
            {
                string[] wfID = txtWorkflowID.Value.Trim().Split(new Char[] { ',' });
               
                for (int i = 0; i < wfID.GetLength(0); i++)
                {
                    _ASE.WorkflowID = Convert.ToInt32(wfID[i]);
                    sResult = DbHelper.GetInstance().JudgeAgentBeforeAddAndUpdate(_ASE);

                    if (sResult != "-1")
                    {
                        return false;
                    }
                }
                return true ;
            }

            else
            {
                _ASE.WorkflowID = Convert.ToInt32(txtWorkflowID.Value);

                sResult = DbHelper.GetInstance().JudgeAgentBeforeAddAndUpdate(_ASE );
              
                if ((sResult == "-1"))
                    return false  ;
                else 
                    return true;
            }
        }
     
        //此类要更改,完成赋值工作
       private string SaveData(Workflow_AgentSettingEntity _Workflow_AgentSettingEntity)
       {

           string sResult = "-1";

           if (txtWorkflowID.Value.Trim().IndexOf(",") > 0)
           {
               string[] wfID = txtWorkflowID.Value.Trim().Split(new Char[] { ',' });

               for (int i = 0; i < wfID.GetLength(0); i++)
               {
                   _Workflow_AgentSettingEntity.WorkflowID = Convert.ToInt32(wfID[i]);

                   if (ViewState["status"].ToString () == "Add")
                       sResult = DbHelper.GetInstance().AddWorkflow_AgentSetting(_Workflow_AgentSettingEntity);
                   else if (ViewState["status"].ToString() == "Update")
                       sResult = DbHelper.GetInstance().UpdateWorkflow_AgentSetting(_Workflow_AgentSettingEntity);
               }
               return sResult;
           }
           else
           {
               _Workflow_AgentSettingEntity.WorkflowID = Convert.ToInt32(txtWorkflowID.Value);

               if (ViewState["status"].ToString() == "Add")
                   sResult = DbHelper.GetInstance().AddWorkflow_AgentSetting(_Workflow_AgentSettingEntity);
               else if (ViewState["status"].ToString() == "Update")
                   sResult = DbHelper.GetInstance().UpdateWorkflow_AgentSetting(_Workflow_AgentSettingEntity);
               return sResult;
           }
       }

       protected void btnClear_Click(object sender, EventArgs e)
       {
           ViewState["status"] = "Add";

           btnSubmit.Visible = true ;
           btnClear.Visible = true ;

           ImgBeAgentPerson.Visible = true ;
           ImgAgentPerson.Visible = true ;

           AgentStartDateCalendarExtender.Enabled  = true ;
           AgentEndDateCalendarExtender.Enabled  = true ;

           chkAllowCycle.Enabled = true ; 
           chkAllowCreate.Enabled = true ;
          
           ImgWorkflowID.Visible = true ;

           System.Web.UI.ScriptManager.RegisterStartupScript(btnClear, this.GetType(), "ButtonHideScript", strButtonHideScript, false);
        
       }

       protected void btnReturn_Click(object sender, EventArgs e)
           {
           if (ViewState["status"].ToString () == "Add")
               Response.Redirect("GG90.aspx");
           else
               Response.Redirect(string.Format("GG90Detail.aspx?BeAgentPersonID={0}&AgentPersonID={1}", txtBeAgentPersonID.Value, txtAgentPersonID.Value));

           }
    }
}