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
using System.Text;
using System.IO;
using GPRP.Web.UI;
using GPRP.GPRPComponents;
using GPRP.GPRPControls;
using GPRP.GPRPEnumerations;
using GPRP.GPRPBussiness;
using GPRP.Entity;
using YYControls;
using VBControls.VBProject;
using MyADO;

namespace GOA.UserControl
{
    public partial class FileUploadEmailAttach:System.Web.UI.UserControl
    {

        public int IsEdit; //是否可编辑
        public int EmailSerialID;//邮件编号
        public int AttachmentSerialID;  //附件编号
        public DataTable dtAttach; ////保存附件的信息的值
        private string FileAttachPath;//文件上载的路径
        private string AttachFileTypeList;
        private float FileMaxSize;//文件上传的大小限制        public  int RightType;//当前用户对此流程实例的权限

        public FileUploadEmailAttach()
        {
            FileAttachPath = GeneralConfigs.GetConfig().attachment_RequestAttach;
            FileMaxSize = GeneralConfigs.GetConfig().AttachmentFileSize;
            AttachFileTypeList = GeneralConfigs.GetConfig().AttachmentFileType;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //int RequestID = Convert.ToInt32(Request.QueryString["RequestID"].ToString());
             
                string action = Context.Request["action"].ToString();
                DataTable DtMail = new DataTable();
                if (action == "ChangeSend")
                {
                    int MailId = Int32.Parse(Context.Request["MailID"].ToString());
                    DtMail = DbHelper.GetInstance().GetEmailInfoByID(MailId);
                    if (DtMail.Rows.Count > 0)
                    {
                        string ReceiverID = DtMail.Rows[0]["ReceiverID"].ToString();
                        if (ReceiverID != "")
                        {
                            DtMail = DbHelper.GetInstance().GetRecvEmailInfoById(MailId);
                            if (DtMail.Rows.Count > 0)
                            {
                                int UserMasterID = Int32.Parse(DtMail.Rows[0]["UserMasterID"].ToString());
                                EmailSerialID = UserMasterID;
                            }
                        }
                        else
                            EmailSerialID = MailId;
                    }
                }
                //获取附件信息
                DataTable dt = DbHelper.GetInstance().GetEmailAttachFile(EmailSerialID);
                dtAttach = dt;
                ViewState["dtAttach"] = dt;
                ArrayList selectLines = new ArrayList();
                ViewState["selectedLines"] = selectLines;
                BindGridView();
                if (IsEdit == 0 || RightType==1)
                {
                    DivEditArea.Visible = false;
                }
            }
            else
            {
                dtAttach = (DataTable)ViewState["dtAttach"];
            }
        }

        private void BindGridView()
        {
            dtAttach = (DataTable)ViewState["dtAttach"];
            GridView1.DataSource = dtAttach;
            GridView1.DataBind();
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string KeyCol = e.Row.DataItemIndex.ToString()   ;
                CheckBox cb = (System.Web.UI.WebControls.CheckBox)e.Row.FindControl("Item") as CheckBox;
                ArrayList selectedLines = (ArrayList)ViewState["selectedLines"];
                if (selectedLines.Contains(KeyCol))
                {
                    cb.Checked = true;
                }
                string url = DNTRequest.GetCurrentFullHost();
                string AttachmentName = ((DataRowView)e.Row.DataItem).Row["AttachmentName"].ToString();
                string AttachClientName = ((DataRowView)e.Row.DataItem).Row["AttachClientName"].ToString();
                //string AttachType = ((DataRowView)e.Row.DataItem).Row["AttachType"].ToString();
                //AttachType = AttachType.Substring(1);
                AttachmentName = "http://" + url + "/" + AttachmentName;
                string FileImageTagPath = ((DataRowView)e.Row.DataItem).Row["FileImageTagPath"].ToString();
                if (FileImageTagPath == "")
                    FileImageTagPath = DbHelper.GetInstance().GetImagePath(((DataRowView)e.Row.DataItem).Row["AttachType"].ToString());
                string AttachSize = ((DataRowView)e.Row.DataItem).Row["AttachSize"].ToString();
                string fileHTML = "<img src='" + FileImageTagPath + "'/>&nbsp;<a href='" + AttachmentName + "' target='_blank'>" + AttachClientName + "</a>&nbsp(" + AttachSize + "&nbsp;MB)";
                Label lblFile = (System.Web.UI.WebControls.Label)e.Row.FindControl("lblFile");
                lblFile.Text = fileHTML;

                if (IsEdit == 0)
                {
                    cb.Enabled = false;
                }
            }
        }


        protected void UploadFile(object sender, EventArgs e)
        {
            try
            {
                string fpath = Path.Combine(Request.PhysicalApplicationPath, FileAttachPath );
                string OCOriginalFileName = "";
                string OCExistFileName = "";

                if (fileUp.HasFile)
                {
                    OCOriginalFileName = fileUp.FileName;
                    string fileType = System.IO.Path.GetExtension(OCOriginalFileName).ToLower();

                    //检查是否是允许的档案
                    if (!(","+AttachFileTypeList+",").Contains("," + fileType + ","))
                    {
                        System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "Nofile", "<script language='javascript'>alert('上载的档案类型被禁止');</script>", false);
                    }
                    else
                    {
                        //检查是否超过文件大小限制
                        float fileSize=fileUp.PostedFile.ContentLength;
                        if (fileSize < FileMaxSize * 1024 * 1024)
                        {
                            Random random1 = new Random();
                            int intRandom = random1.Next(100);
                            OCExistFileName = EmailSerialID.ToString() + "_" + AttachmentSerialID.ToString() + "_" + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString().PadLeft(2, '0') + DateTime.Now.Day.ToString().PadLeft(2, '0') + DateTime.Now.Hour.ToString().PadLeft(2, '0') + DateTime.Now.Minute.ToString().PadLeft(2, '0') + DateTime.Now.Second.ToString().PadLeft(2, '0') + DateTime.Now.Millisecond.ToString().PadLeft(3, '0') + intRandom.ToString().PadLeft(3, '0') + fileType;
                            
                            fileUp.SaveAs(Path.Combine(fpath, OCExistFileName));
                            dtAttach=(DataTable)ViewState["dtAttach"];
                            DataRow row=dtAttach.NewRow();
                            //FileServerName,a.FileType,a.FileClientName,a.FileSize,b.ImagePath
                            
                            string url = DNTRequest.GetCurrentFullHost();
                            //string fileServerURL = "http://" + url + "/" + FileAttachPath + "/" + OCExistFileName;
                            string fileServerURL = FileAttachPath + "\\" + OCExistFileName;
                            row["AttachmentName"] = fileServerURL;
                            row["AttachType"] = fileType;
                            row["AttachClientName"] = OCOriginalFileName;
                            row["AttachSize"] = Math.Round(fileSize/1024/1024,2).ToString();
                           // row["Uploader"] = Uploader.ToString();
                          //  row["UploadDate"] = Utils.GetDateTime();
                            //获取文件图标的Path
                            string ImageTagPath = DbHelper.GetInstance().GetFileImageTagPath(fileType);
                           // row["ImagePath"] = ImageTagPath;
                            //row["RequestID"] = RequestID;
                            //row["FieldID"] = FieldID;
                            dtAttach.Rows.Add(row);
                            ViewState["dtAttach"] = dtAttach;
                            BindGridView();

                        }
                        else
                        {
                            System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "Nofile", "<script language='javascript'>alert('上载的档案大小超过最大限制"+FileMaxSize.ToString()+"MB');"+"</script>", false);                        
                        }
                    }
                }
            }
            catch (Exception err)
            {
                string strMessage = "上载档案发生错误,错误为" + err.Message.Replace("\r", "").Replace("\n", " ");
                System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "Nofile", "<script language='javascript'>alert('" + Utils.ReplaceStrToScript(strMessage)+ "');</script>", false);
            }
        }


        protected void RemoveAttach(object sender, EventArgs e)
        {

            CollectSelected();
            dtAttach = (DataTable)ViewState["dtAttach"];
            ArrayList selectLines = (ArrayList)ViewState["selectedLines"];
            for (int i = selectLines.Count; i > 0; i--)
            {
                dtAttach.Rows.RemoveAt(Convert.ToInt32(selectLines[i - 1]));
            }
            selectLines.Clear();
            ViewState["selectedLines"] = selectLines;
            ViewState["dtAttach"] = dtAttach;
            BindGridView();
        }


        private void CollectSelected()
        {
            ArrayList selectedLines = (ArrayList)ViewState["selectedLines"];
            
            for (int i = 0; i < this.GridView1.Rows.Count; i++)
            {

                string KeyCol = GridView1.Rows[i].DataItemIndex.ToString();
                CheckBox cb = this.GridView1.Rows[i].FindControl("Item") as CheckBox;
                if (selectedLines.Contains(KeyCol) && !cb.Checked)
                    selectedLines.Remove(KeyCol);
                if (!selectedLines.Contains(KeyCol) && cb.Checked)
                    selectedLines.Add(KeyCol);
            }
        }

        public void GetFilesValue()
        {
            dtAttach = (DataTable)ViewState["dtAttach"];
        }

    }
}