using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GPRP.Entity;
using GPRP.GPRPComponents;
using GPRP.Web.UI;
using MyADO;
using System.Data;
using System.Web.Script.Serialization;
using System.Web.Services;

namespace GOA.Index
{
    public partial class Index : BasePage
    { 
        protected void Page_Load(object sender, EventArgs e)
        {           
            Literalusername.Text = userEntity.UserName;
            if (!Page.IsPostBack)
            {
                if (Session["gopage"] != null && Session["gopage"].ToString() != "")
                {
                   
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "gopage", string.Format("var main = document.getElementById(\"main\");main.src=\"{0}\";", gopage), true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "gopage", string.Format("OpenNewWindow(\"{0}\")", Session["gopage"].ToString()), true);
                    Session.Remove("gopage");
                }
            }

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Global.CalendarNote();
            //String content = TextBox1.Text;
            //myAsynResult asyncResult = null;

            //string[] username = t1.Text.Split(',');
            //Messages.Instance().AddMessage(content, asyncResult,username);
        }

       
    }
 

}
