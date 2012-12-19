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

using GPRP.GPRPControls;
using GPRP.GPRPComponents;
namespace AJAXWeb.ascx
{
       
    public partial class UCOperationBanner : System.Web.UI.UserControl
    { public delegate void ButtonClick(string ClientID);
   
        public event ButtonClick btnClick;
        protected void Page_Load(object sender, EventArgs e)
        { 
           //根据用户的权限来是否显示哪个按钮
            Page.ClientScript.RegisterStartupScript(this.GetType(), btnEditMode.ClientID, "<script>document.getElementById('" + btnEditMode.ClientID.Replace("_", "$") + "').disabled=" + GeneralConfigs.GetConfig().ButtonEnable + ";</script>");
            Page.ClientScript.RegisterStartupScript(this.GetType(), btnSubmit.ClientID, "<script>document.getElementById('" + btnSubmit.ClientID.Replace("_", "$") + "').disabled=" + GeneralConfigs.GetConfig().ButtonEnable + ";</script>");
            Page.ClientScript.RegisterStartupScript(this.GetType(), btnDel.ClientID, "<script>document.getElementById('" + btnDel.ClientID.Replace("_", "$") + "').disabled=" + GeneralConfigs.GetConfig().ButtonEnable + ";</script>");
        
              
        }
        public void ButtnEnable(string Opearation)
        {
            switch (Opearation)
            {
                case "btnBrowseMode":
                  
                 //Page.ClientScript.RegisterStartupScript(this.GetType(), "Button", "<script>document.getElementById('"+btnSubmit.ClientID.Replace("_","$")+"').disabled=true;</script>");
                    break;
                case "btnEditMode":
                   // btnSubmit.Enabled = true;
                   // Page.ClientScript.RegisterStartupScript(this.GetType(), "Button", "<script>document.getElementById('"+btnSubmit.ClientID.Replace("_","$")+"').disabled=false;</script>");
        
                   // btnDel.Enabled = false;
                    break;
                case "btnAdd":

                    break;
                case "btnSubmit":

                    break;
                case "btnDel":

                    break;
            
            }
        
        }
       protected void btnBrowseMode_Click(object sender, EventArgs e)
        {
            GPRP.GPRPControls.Button btn = (GPRP.GPRPControls.Button)sender; 
            if (btnClick != null)
            {
                btnClick(btn.ID);
            }
        }

    }
}