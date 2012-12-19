using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using GPRP.Web.UI;
using GPRP.Entity.Basic;
using System.Data;
using MyADO;

namespace GOA.Basic
{
    public partial class ShowFiles : BasePage
    {
        private string MyPath = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            MyPath = HttpUtility.UrlDecode(Context.Request.Params["ImgPath"]);
          
            if (!IsPostBack)
            {
                DeleteBtn.Attributes.Add("onclick", "return confirm('确定要删吗?');");
                CancelBtn.Attributes.Add("onclick", "return confirm('确定要作废吗?');");
                GetFileList(MyPath);
                //初始化将所有文件的版本控制设为0
               DbHelper.GetInstance().UpDateFileInfo();
            }
            GetPermission();
  
        }



        private void GetPermission()
        {

            string FolderPath = MyPath.Replace('/','\\');
            DataTable dtFolderInfo = DbHelper.GetInstance().GetFolderFromName(FolderPath);
            DeleteBtn.Disabled = true;
            CheckInBtn.Disabled = true;
            CheckOutBtn.Disabled = true;
            CancelBtn.Disabled = true;
            EditionBtn.Disabled = true;
            if(dtFolderInfo.Rows.Count > 0)
            {
                int FolderID = Int32.Parse(dtFolderInfo.Rows[0]["FolderSerialID"].ToString());
                DataTable dtFolderPermission = DbHelper.GetInstance().GetFolderPermissFromID(userEntity.UserSerialID,FolderID);
                if (dtFolderPermission.Rows.Count > 0)
                {
                    string Permission = dtFolderPermission.Rows[0]["Permission"].ToString();
                    string[] szSplitPermission = Permission.Split(',');
                    foreach (string finalPermission in szSplitPermission)
                    {
                       if(finalPermission!="")
                       {

                          if (finalPermission == "2") //可修改
                          {
                             CheckInBtn.Disabled = false;
                             CheckOutBtn.Disabled = false;
                             EditionBtn.Disabled = false;
            
                          }
                          if (finalPermission == "3") //可删除
                          {
                             DeleteBtn.Disabled = false;
                          }
                          if (finalPermission == "4") //可作废
                          {
                             CancelBtn.Disabled = false;
                          }
                       }
                    }
                }
            }
 
        }

        private void GetFileList(string MyPath)
        {
            DirectoryInfo dirInfo = new DirectoryInfo(MyPath);
            string imgStr = "";
            Random rd = new Random(1);
            foreach (FileInfo file in dirInfo.GetFiles("*.*"))
            {

                string MyStr="";
                int index;
                string  tailname;
                string IdName ;

                int FileES=0;
                DateTime dtTime = new DateTime();
                index = file.Name.LastIndexOf(".");
                tailname = file.Name.Substring(index + 1);
                IdName = rd.Next().ToString();
                DataTable  dt =DbHelper.GetInstance().GetFileInfoByName(file.Name,MyPath);
                if(dt.Rows.Count==0)
                {
                     DocFileInfo _DocFileInfo = new DocFileInfo();
                    _DocFileInfo.FileEdition = 0;
                    _DocFileInfo.FileES=0;
                    _DocFileInfo.FileFolderName=MyPath;
                    _DocFileInfo.FileModifyDate=DateTime.Now;
                    _DocFileInfo.FileModifyUserId=userEntity.UserSerialID;
                    _DocFileInfo.FileName=file.Name;
                    _DocFileInfo.FileNote="";
                    _DocFileInfo.FileSize=0;
                    _DocFileInfo.FileValidPeriod = DateTime.Now.AddMonths(1);
                    FileES = 0;
                    DbHelper.GetInstance().AddNewsFileInfo(_DocFileInfo);
                }else
                {
                    FileES = Int32.Parse(dt.Rows[0]["FileES"].ToString());
                    dtTime = DateTime.Parse(dt.Rows[0]["FileValidPeriod"].ToString());
         
                    //DocFileInfo _docFileInfo = DbHelper.GetInstance().GetFileInfoEntityByFileName(file.Name, MyPath);
                    //_docFileInfo.FileEdition = 0;
                    //DbHelper.GetInstance().UpDateFileInfoByFileName(_docFileInfo);
                }

                if (dtTime > DateTime.Now)  //是否过期
                {
                    MyStr = "<a href=\"javascript:void()\" onmousedown=\"Get('" + file.FullName.Replace("\\", "/") + "','" + tailname + "','" + IdName + "','" + FileES + "')\">";
                    if (tailname == "doc" || tailname == "docx")
                        MyStr = MyStr + "<img src=\"../images/ico_file_word.PNG\" id='" + IdName + "'/>" + file.Name + "</a>";
                    else if (tailname == "xls" || tailname == "xlsx")
                        MyStr = MyStr + "<img src=\"../images/ico_file_excel.PNG\" id='" + IdName + "'/>" + file.Name + "</a>";
                    else if (tailname == "ppt")
                        MyStr = MyStr + "<img src=\"../images/ico_file_ppt.PNG\" id='" + IdName + "'/>" + file.Name + "</a>";
                    else if (tailname == "pdf")
                        MyStr = MyStr + "<img src=\"../images/ico_file_pdf.PNG\" id='" + IdName + "'/>" + file.Name + "</a>";
                    else
                        MyStr = MyStr + "<img src=\"../images/ico_file_content.PNG\" id='" + IdName + "'/>" + file.Name + "</a>";

                    imgStr = imgStr + MyStr;
                }
           }
           showfiles.InnerHtml = imgStr;
        }



    protected void CheckInClick(object sender, EventArgs e) //签入
    {
        string FileFullName = hiddenFileName.Value;
        string FileES = hiddenFileES.Value;

        string fileName;
        string fileSuffix;
        fileName = FileFullName.Substring(FileFullName.LastIndexOf("/") + 1);
        fileSuffix = fileName.Substring(fileName.LastIndexOf('.') + 1);

        if (Int32.Parse(FileES) == 1) //已签出
        {
            DataTable dt = DbHelper.GetInstance().GetFileInfoByName(fileName, MyPath);
            if (dt.Rows.Count > 0)
            {
                int ModifyUserID = Int32.Parse(dt.Rows[0]["FileModifyUserID"].ToString());
                int userId = userEntity.UserSerialID;
                if (userId == ModifyUserID) //此用户签出,可以将文件签入
                {
                    DocFileInfo _docFileInfo = DbHelper.GetInstance().GetFileInfoEntityByFileName(fileName, MyPath);
                    _docFileInfo.FileES = 0;
                    _docFileInfo.FileModifyDate = DateTime.Now;
                    _docFileInfo.FileModifyUserId = userEntity.UserSerialID;
                    DbHelper.GetInstance().UpDateFileInfoByFileName(_docFileInfo);
                    string strScript = "";
                    strScript = "<script type=\"text/javascript\"  lanuage=\"javascript\"> \r\n" +
               "alert(\"文件签入成功！\"); \r\n" +
              "</script> \r\n";
                    System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "strScript", strScript, false);
                    GetFileList(MyPath);

                }
                else  //不是此用户签出，所以不可以将文件签入
                {
                    string strScript = "";
                    strScript = "<script type=\"text/javascript\"  lanuage=\"javascript\"> \r\n" +
               "alert(\"此文件已经被其它用户以独占的形式签出，不可以签入！\"); \r\n" +
              "</script> \r\n";
                    System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "strScript", strScript, false);

                }
            }

        }
        else if (Int32.Parse(FileES) == 0)
        {
            string strScript = "";
            strScript = "<script type=\"text/javascript\"  lanuage=\"javascript\"> \r\n" +
       "alert(\"此文件没被签出，无效的签入！\"); \r\n" +
      "</script> \r\n";
            System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "strScript", strScript, false);

        }
    }


     protected void CheckOutClick(object sender, EventArgs e)  //签出
     {
         string FileFullName = hiddenFileName.Value;
         string FileES = hiddenFileES.Value;

         string fileName;
         string fileSuffix;
         fileName = FileFullName.Substring(FileFullName.LastIndexOf("/") + 1);
         fileSuffix =fileName.Substring(fileName.LastIndexOf('.') + 1);

         if (Int32.Parse(FileES) == 0)  //未签出
         {
             DocFileInfo _docFileInfo = DbHelper.GetInstance().GetFileInfoEntityByFileName(fileName, MyPath);
             _docFileInfo.FileES = 1;
             _docFileInfo.FileModifyDate = DateTime.Now;
             _docFileInfo.FileModifyUserId = userEntity.UserSerialID;
             DbHelper.GetInstance().UpDateFileInfoByFileName(_docFileInfo);  //更新成签出状态
             string strScript = "";
             strScript = "<script type=\"text/javascript\"  lanuage=\"javascript\"> \r\n" +
        "CheckInOut('" + FileFullName + "','" + fileSuffix + "','" + FileES + "'); \r\n" +
       "</script> \r\n";
             System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "strScript", strScript, false);
             GetFileList(MyPath);
         }
         else if (Int32.Parse(FileES) == 1) //已签出
         {
             DataTable dt = DbHelper.GetInstance().GetFileInfoByName(fileName, MyPath);
             if (dt.Rows.Count > 0)
             {
                 int ModifyUserID = Int32.Parse(dt.Rows[0]["FileModifyUserID"].ToString());
                 int userId = userEntity.UserSerialID;
                 if (userId == ModifyUserID) //此用户签出,可以继续再签出
                 {
                     DocFileInfo _docFileInfo = DbHelper.GetInstance().GetFileInfoEntityByFileName(fileName, MyPath);
                     _docFileInfo.FileES = 1;
                     _docFileInfo.FileModifyDate = DateTime.Now;
                     _docFileInfo.FileModifyUserId = userEntity.UserSerialID;
                     DbHelper.GetInstance().UpDateFileInfoByFileName(_docFileInfo);
                     string strScript = "";
                     strScript = "<script type=\"text/javascript\"  lanuage=\"javascript\"> \r\n" +
                "CheckInOut('" + FileFullName + "','" + fileSuffix + "','" + FileES + "'); \r\n" +
               "</script> \r\n";
                     System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "strScript", strScript, false);
                     GetFileList(MyPath);

                 }
                 else  //不是此用户签出，所以不可以再签出
                 {
                     string strScript = "";
                     strScript = "<script type=\"text/javascript\"  lanuage=\"javascript\"> \r\n" +
                "alert(\"此文件已经被其它用户以独占的形式签出，不可以签出！\"); \r\n" +
               "</script> \r\n";
                     System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "strScript", strScript, false);

                 }
             }

         }

     }

     protected void DeleteClick(object sender, EventArgs e)
     {
         string FileFullName = hiddenFileName.Value;
         File.Delete(FileFullName);
         GetFileList(MyPath);
     }

     protected void CancelClick(object sender, EventArgs e)
     {
         string FileFullName = hiddenFileName.Value;
         string fileName = FileFullName.Substring(FileFullName.LastIndexOf("/") + 1);
         string folderName = FileFullName.Substring(0, FileFullName.LastIndexOf("/"));
         DocFileInfo _docFileInfo = DbHelper.GetInstance().GetFileInfoEntityByFileName(fileName, folderName);
         _docFileInfo.FileValidPeriod = DateTime.Now;
         DbHelper.GetInstance().UpDateFileInfoByFileName(_docFileInfo);
         GetFileList(MyPath);
     }

     protected void SearchFile(object sender, EventArgs e)
     {
         string SearchStrKey = fileKey.Value;
         if (SearchStrKey == "")
         {
             //Response.Write("<script lanuage=\"javascript\">alert(\"搜索关键字不能为空！\");</script>");
             string strScript = "";
             strScript = "<script type=\"text/javascript\"  lanuage=\"javascript\"> \r\n" +
        "alert(\"搜索关键字不能为空！\"); \r\n" +
       "</script> \r\n";
             System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "strScript", strScript, false);
             return ;
         }
         string ShowFileStr="";
         string imgStr = "";
         string IdName = "";
         showfiles.InnerHtml = "";
         Random rd = new Random(1);
         DataTable dtSearch= DbHelper.GetInstance().GetFileByFileName(SearchStrKey);
         if (dtSearch.Rows.Count > 0)
         {
             for (int i = 0; i < dtSearch.Rows.Count; i++)
             {
                 string fileName = dtSearch.Rows[i]["FileName"].ToString();
                 string folderPath = dtSearch.Rows[i]["FileFoldName"].ToString();
                 string tailname = fileName.Substring(fileName.LastIndexOf('.') + 1);
                 string fullPath = folderPath +"/"+ fileName;
                 string FileES = dtSearch.Rows[i]["FileES"].ToString();
                 IdName = rd.Next().ToString();



                 ShowFileStr = "<a href=\"javascript:void()\" onmousedown=\"Get('" + fullPath.Replace("\\", "/") + "','" + tailname + "','" + IdName + "','" + FileES + "')\">";
                 if (tailname == "doc" || tailname == "docx")
                     ShowFileStr = ShowFileStr + "<img src=\"../images/ico_file_word.PNG\" id='" + IdName + "'/>" + fileName + "</a>";
                 else if (tailname == "xls" || tailname == "xlsx")
                     ShowFileStr = ShowFileStr + "<img src=\"../images/ico_file_excel.PNG\" id='" + IdName + "'/>" + fileName + "</a>";
                 else if (tailname == "ppt")
                     ShowFileStr = ShowFileStr + "<img src=\"../images/ico_file_ppt.PNG\" id='" + IdName + "'/>" + fileName + "</a>";
                 else if (tailname == "pdf")
                     ShowFileStr = ShowFileStr + "<img src=\"../images/ico_file_pdf.PNG\" id='" + IdName + "'/>" + fileName + "</a>";
                 else
                     ShowFileStr = ShowFileStr + "<img src=\"../images/ico_file_content.PNG\" id='" + IdName + "'/>" + fileName + "</a>";

                 imgStr = imgStr + ShowFileStr;
             }

         }
         else
             imgStr = "未搜索到任何相关文件";

         fileKey.Value = "";
         showfiles.InnerHtml = imgStr;

          
     }

     protected void btnFileUpload_Click(object sender, EventArgs e)   
    {

        if (EndTime.Value == "")
        {
            lblMessage.Text = "请设置上传文件的有效期";
        }
        else
        {
            if (FileUpLoad1.HasFile)
            {
                //判断文件是否小于10Mb   
                if (FileUpLoad1.PostedFile.ContentLength < 10485760)
                {
                    try
                    {
                        //上传文件并指定上传目录的路径   
                        FileUpLoad1.PostedFile.SaveAs(MyPath
                           + "/" + FileUpLoad1.FileName);
                        lblMessage.Text = "上传成功!";

                        string foldTName = MyPath.Substring(0, MyPath.LastIndexOf("\\"));
                        string foldName = foldTName.Substring(foldTName.LastIndexOf("\\") + 1);
                        DataTable dt = DbHelper.GetInstance().GetFileInfoByName(FileUpLoad1.FileName, MyPath);
                        if (dt.Rows.Count == 0)
                        {
                            DocFileInfo _DocFileInfo = new DocFileInfo();
                            _DocFileInfo.FileEdition = 0;
                            _DocFileInfo.FileES = 0;
                            _DocFileInfo.FileFolderName = MyPath;
                            _DocFileInfo.FileModifyDate = DateTime.Now;
                            _DocFileInfo.FileModifyUserId = userEntity.UserSerialID;
                            _DocFileInfo.FileName = FileUpLoad1.FileName;
                            _DocFileInfo.FileNote = "";
                            _DocFileInfo.FileSize = 0;
                            _DocFileInfo.FileValidPeriod = DateTime.Parse(EndTime.Value);
                            DbHelper.GetInstance().AddNewsFileInfo(_DocFileInfo);
                        }


                    }
                    catch (Exception ex)
                    {
                        lblMessage.Text = "出现异常,无法上传!";
                    }

                }
                else
                {
                    lblMessage.Text = "上传文件不能大于10MB!";
                }
            }
            else
            {
                lblMessage.Text = "尚未选择文件!";
            }

          
        }

            //if (AttachFile.HasFile)
            //{
            //    string FileName = this.AttachFile.FileName;//获取上传文件的文件名,包括后缀
            //    string ExtenName = System.IO.Path.GetExtension(FileName);//获取扩展名
            //    string SaveFileName = MyPath + "/" + FileName; //System.IO.Path.Combine(System.Web.HttpContext.Current.Request.MapPath("UpLoads/"), DateTime.Now.ToString("yyyyMMddhhmm") + ExtenName);//合并两个路径为上传到服务器上的全路径
            //    AttachFile.MoveTo(SaveFileName, Brettle.Web.NeatUpload.MoveToOptions.Overwrite);
            //   // string url = "UpLoads/" + DateTime.Now.ToString("yyyyMMddhhmmss") + ExtenName; //文件保存的路径
            //    float FileSize = (float)System.Math.Round((float)AttachFile.ContentLength / 1024000, 1); //获取文件大小并保留小数点后一位,单位是M

            //}

    }
   }  



    
}
