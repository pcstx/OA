using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GPRP.Web.UI;
using System.Text;

namespace GOA.Basic
{
    public partial class Office : BasePage
    {
        public string MyPath = "";
        public string MyTailName = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                 MyPath = HttpUtility.UrlDecode(Context.Request.Params["ImgPath"]);
                 MyTailName = HttpUtility.UrlDecode(Context.Request.Params["tailName"]);
                
                 
            }
        }
    }
}
