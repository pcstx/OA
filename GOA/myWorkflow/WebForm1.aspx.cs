using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GOA.myWorkflow
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string mySting = "Mario Gamito";
            this.Hidden1.Value = mySting;
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            this.Hidden1.Value = "change this value";
        }
    }
}
