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
using System.IO;

namespace ImageEditDemo
{
    public partial class _Default : System.Web.UI.Page
    {
        public  string strImgPath = "";
        public  int iEditWidth = -1;
        public  int iEditHeight = -1;
        public  int ogiImgWidth=-1;
        public  int ogiImgHeight=-1;
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
        /// <summary>
        /// 初始化获取图片的原始宽度和高度，避免客户端加载过慢
        /// </summary>
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
                    iEditWidth = ogiImgWidth;
                    iEditHeight = ogiImgHeight;
                }
                catch {}
            }
        }
    }
}
