using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using GPRP.Entity.Basic;
using MyADO;
using GPRP.Web.UI;
using System.Data;

namespace GOA.Basic
{
    public partial class OfficeCheckInOut : BasePage
    {
        public string MyPath = "";
        public string MyTailName = "";
        public int MyFileES = 0;
        public string fileEdtion = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            MyPath = HttpUtility.UrlDecode(Context.Request.Params["ImgPath"]);
            MyTailName = HttpUtility.UrlDecode(Context.Request.Params["tailName"]);
            MyFileES = Convert.ToInt32(HttpUtility.UrlDecode(Context.Request.Params["fileES"]));
            if (Session["fileEdition"] != null)
            {
              fileEdtion = Session["fileEdition"].ToString();
            }
            
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string RootPath = "J:/RD";
            string strTempPath = "FileTemp";
            string filePath = RootPath + "/" + strTempPath;
            string fileName = MyPath.Substring(MyPath.LastIndexOf("/") + 1);
            string CopyFilePath = "";
            string  folderName = MyPath.Substring(0, MyPath.LastIndexOf("/"));
            DocFileInfo _docFileInfo = DbHelper.GetInstance().GetFileInfoEntityByFileName(fileName, folderName);
            if (!File.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }
            CopyFilePath = filePath + "/" + fileName.Substring(0,fileName.LastIndexOf("."));
            DataTable dtEdition = DbHelper.GetInstance().GetFileEditionInfoByID(_docFileInfo.FileSerialID);

            if (fileEdtion == "EDITIONTEXTISNULL")
            {
               
                fileEdtion = (dtEdition.Rows.Count + 1).ToString() + ".0";
            }
            else
            {
                fileEdtion = fileEdtion  +"_" + (dtEdition.Rows.Count + 1).ToString() + ".0";
            }

            CopyFilePath = CopyFilePath + "_" + fileEdtion + fileName.Substring(fileName.LastIndexOf(".") );
            try {
                  File.Copy(MyPath, CopyFilePath);

            }catch(Exception ex)
            {
          
            }

            DocFileEdition _docFileEdition = new DocFileEdition();
            _docFileEdition.FileID = _docFileInfo.FileSerialID;
            _docFileEdition.FileEdition = fileEdtion;
            _docFileEdition.FileUrl = CopyFilePath;
            _docFileEdition.ModifyDate = DateTime.Now;
            _docFileEdition.ModifyUserName = userEntity.UserName;
            _docFileEdition.FileNote = "";
            _docFileEdition.FileName = fileName;
            DbHelper.GetInstance().AddFileEditionInfo(_docFileEdition);
        
        }
    }
}
