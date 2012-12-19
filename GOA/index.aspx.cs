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
    public partial class index : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lblCurrentUserName.Text = userEntity.UserName;
            if (!Page.IsPostBack)
            {
                if (Session["gopage"] != null && Session["gopage"].ToString() != "")
                {
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "gopage", string.Format("var main = document.getElementById(\"main\");main.src=\"{0}\";", gopage), true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "gopage", string.Format("OpenNewWindow(\"{0}\")", Session["gopage"].ToString()), true);
                    Session.Remove("gopage");
                }

                //if (userEntity.UserType != "0")
                //    {


                // //DataTable dtGroup=DbHelper.GetInstance().GetUserSysGroup(userEntity.UserSerialID.ToString());
                // //if (dtGroup.Rows.Count > 0)
                // //    Session["GroupId"] = dtGroup.Rows[0]["GroupID"].ToString();
                // //else
                // //    Session["GroupId"] = "0";

                // //   this.main.Attributes.Remove("src");
                // // //  this.main.Attributes.Add("src", "SM/InfoForSupplier.aspx");



                // //   DataTable dt = new DataTable();
                // //   dt = DbHelper.GetInstance().GetDBRecords(" top 6  AnnounceID ,AnnounceTitle,convert(varchar(10),CreateDate,120) CreateDate", "GP_Announce", "ExpiredDate>=getdate() and IsPublish=1 and (AnnounceReceiptor='All' or AnnounceReceiptor like '%" + userEntity.UserID + "%')", "ExpiredDate desc");

                // //   if (dt.Rows.Count > 0)
                // //       {
                // //       string ac = "<ul id='newsList'>";
                // //       for (int i = 0; i < dt.Rows.Count; i++)
                // //           {
                // //           ac += "<li><a href='SM/AnnounceDetail.aspx?Aid=" + dt.Rows[i][0].ToString() + "' target='_blank'>" + (dt.Rows[i][1].ToString().Length > 20 ? dt.Rows[i][1].ToString().Substring(0, 20) + "…" : dt.Rows[i][1].ToString().PadRight(26, '√').Replace("√", "&nbsp;&nbsp;&nbsp;")) + "&nbsp;&nbsp;" + dt.Rows[i][2].ToString() + "</a></li>";
                // //           }
                // //       AnnounceContent.InnerHtml = ac + "</ul>";
                // //       divAnnounce.Visible = true;
                // //       }
                // //   else
                // //       divAnnounce.Visible = false;

                  

                //    }
                //else
                //    divAnnounce.Visible = false;
            }
        }
    }
}
