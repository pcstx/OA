using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using MyADO;
using GPRP.Entity.Basic;

namespace GOA.Basic
{
    public partial class Edition : System.Web.UI.Page
    {
        public string MyPath = "";
        public string MyTailName = "";
        public string fileName = "";
        public string folderName = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            MyPath = HttpUtility.UrlDecode(Context.Request.Params["ImgPath"]);
            MyTailName = HttpUtility.UrlDecode(Context.Request.Params["tailName"]);
            fileName = MyPath.Substring(MyPath.LastIndexOf("/") + 1);
            folderName = MyPath.Substring(0, MyPath.LastIndexOf("/"));
            if (Session["fileEdition"] != null)
                ckEdition.Checked = true;
           
        }

   
        protected void EditionBtn_Click(object sender, EventArgs e)
        {
            string fileEdition = "";
            DocFileInfo _docFileInfo = DbHelper.GetInstance().GetFileInfoEntityByFileName(fileName, folderName);
        //    DataTable dtEdition = DbHelper.GetInstance().GetFileEditionInfoByID(_docFileInfo.FileSerialID);
            if(ckEdition.Checked)
            {

                _docFileInfo.FileEdition = 1;
                if (txEdition.Text == "")
                {
                    fileEdition = "EDITIONTEXTISNULL";
                }
                else
                {
                    fileEdition = txEdition.Text;

                }
                Session["fileEdition"] = fileEdition;
            

            }else      
            {
                _docFileInfo.FileEdition = 0;
                Session["fileEdition"] = null;
                
            }
            try
            {
               
                DbHelper.GetInstance().UpDateFileInfoByFileName(_docFileInfo);
            }catch(Exception ex)
            {
                ShowStr.Text = "设置错误，请重新设置";
            }finally{
               ShowStr.Text="设置成功！";
            }

        }
    }
}
