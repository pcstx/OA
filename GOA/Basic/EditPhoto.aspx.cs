using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GOA.Basic
{
    public partial class EditPhoto : System.Web.UI.Page
    {
        public string strImgPath = "";
        public int iEditWidth = -1;
        public int iEditHeight = -1;
        public int ogiImgWidth = -1;
        public int ogiImgHeight = -1;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request["ImgPath"]))
            {
                strImgPath = Request["ImgPath"].ToString();
                // strImgPath = "image/" + strImgPath;
            }
            if (!string.IsNullOrEmpty(Request["EditWidth"]))
            {
                iEditWidth = Convert.ToInt32(Request["EditWidth"]);
            }
            if (!string.IsNullOrEmpty(Request["EditHeight"]))
            {
                iEditHeight = Convert.ToInt32(Request["EditHeight"]);
            }
            if (!IsPostBack)
            {
                GetImgPath();

            }
        }

        private void GetImgPath()
        {
            if (strImgPath != "")
            {
                try
                {
                    //  System.Drawing.Image img = System.Drawing.Image.FromFile(Server.MapPath(strImgPath));
                    System.Drawing.Image img = System.Drawing.Image.FromFile(strImgPath);
                    ogiImgWidth = img.Width;
                    ogiImgHeight = img.Height;
                }
                catch { }
            }
        }
    }
}
