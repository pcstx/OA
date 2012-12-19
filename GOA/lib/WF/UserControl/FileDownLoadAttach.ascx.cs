using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Collections;
using GPRP.GPRPComponents;
using MyADO;

namespace GOA.WF.UserControl
{
    public partial class FileDownLoadAttach : System.Web.UI.UserControl
    {

        public int IsEdit; //是否可编辑
        public int EmailSerialID;//邮件编号
        public int AttachmentSerialID;  //附件编号
        public DataTable dtAttach; ////保存附件的信息的值
        private string FileAttachPath;//文件上载的路径
        private string AttachFileTypeList;
        private float FileMaxSize;//文件上传的大小限制        public  int RightType;//当前用户对此流程实例的权限

        public FileDownLoadAttach()
        {
            FileAttachPath = GeneralConfigs.GetConfig().attachment_RequestAttach;
            FileMaxSize = GeneralConfigs.GetConfig().AttachmentFileSize;
            AttachFileTypeList = GeneralConfigs.GetConfig().AttachmentFileType;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                int MailId = Int32.Parse(Context.Request["MailID"].ToString());
                string action = Context.Request["action"].ToString();
                DataTable DtMail = new DataTable();
                if (action == "recv")
                {
                    DtMail = DbHelper.GetInstance().GetRecvEmailInfoById(MailId);
                    if (DtMail.Rows.Count > 0)
                    {
                        int UserMasterID = Int32.Parse(DtMail.Rows[0]["UserMasterID"].ToString());
                        EmailSerialID = UserMasterID; 
                    }
                }
                else if (action == "send")
                    EmailSerialID = MailId;
                else if (action == "dele")
                {
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
                        }else
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
                string AttachType = ((DataRowView)e.Row.DataItem).Row["AttachType"].ToString();
                AttachType= AttachType.Substring(1);
                AttachmentName = "http://" + url + "/" + AttachmentName + AttachType;
                string FileImageTagPath = ((DataRowView)e.Row.DataItem).Row["FileImageTagPath"].ToString();
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


        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "DownLoadAttach")
            {
                DownLoad(sender, e);
                //int index = Convert.ToInt32(e.CommandArgument.ToString());
                //string FullFileName = ((LinkButton)GridView1.Rows[index].FindControl("ID")).Text;
                //string FileName = ((LinkButton)GridView1.Rows[index].FindControl("name")).Text;
                //string Type = checktype(FileName);
                //System.Web.HttpResponse resp;
                //resp = Context.Response;
                //resp.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");
                //resp.AppendHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(FileName, System.Text.Encoding.UTF8).Replace("+", "%20"));
                //resp.ContentType = Type;
                //resp.WriteFile(FullFileName);
                //resp.Flush();
                //resp.End();

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


        protected void DownLoad(object sender, EventArgs e)
      {
          bool checkDele = false;
          for (int i = 0; i < GridView1.Rows.Count; i++)
          {
              if (((CheckBox)GridView1.Rows[i].FindControl("Item")).Checked == true)
              {
                  checkDele = true;
                  string FullFileName = GridView1.DataKeys[i].Values["AttachmentUrl"].ToString();
                  string FileName = GridView1.DataKeys[i].Values["AttachClientName"].ToString();
                  string Type = checktype(FileName); 
                  System.Web.HttpResponse resp;
                  resp = Context.Response;
                  resp.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");
                  resp.AppendHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(FileName, System.Text.Encoding.UTF8).Replace("+", "%20")); 
                  resp.ContentType = Type;
                  resp.WriteFile(FullFileName);
                  resp.Flush();
                  resp.End();

              }

          }

          if (!checkDele)
          {
              string strScript = "<script type=\"text/javascript\"  lanuage=\"javascript\"> \r\n" +
            "alert('请选择要下载的项！'); \r\n" +
           "</script> \r\n";
              System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "strScript2", strScript, false);
          }
         
      }

        /// <summary> 
        /// 根据文件的扩展名来获取对应的“输出流的HTTP MIME“类型 
        /// </summary> 
        /// <param name="filename"></param> 
        /// <returns></returns> 
        private string checktype(string filename)
        {
            string ContentType;
            switch (filename.Substring(filename.LastIndexOf(".")).Trim().ToLower())
            {
                case ".asf ":
                    ContentType = "video/x-ms-asf ";
                    break;
                case ".avi ":
                    ContentType = "video/avi ";
                    break;
                case ".doc ":
                    ContentType = "application/msword ";
                    break;
                case ".zip ":
                    ContentType = "application/zip ";
                    break;
                case ".xls ":
                    ContentType = "application/vnd.ms-excel ";
                    break;
                case ".gif ":
                    ContentType = "image/gif ";
                    break;
                case ".jpg ":
                    ContentType = "image/jpeg ";
                    break;
                case "jpeg ":
                    ContentType = "image/jpeg ";
                    break;
                case ".wav ":
                    ContentType = "audio/wav ";
                    break;
                case ".mp3 ":
                    ContentType = "audio/mpeg3 ";
                    break;
                case ".mpg ":
                    ContentType = "video/mpeg ";
                    break;
                case ".mepg ":
                    ContentType = "video/mpeg ";
                    break;
                case ".rtf ":
                    ContentType = "application/rtf ";
                    break;
                case ".html ":
                    ContentType = "text/html ";
                    break;
                case ".htm ":
                    ContentType = "text/html ";
                    break;
                case ".txt ":
                    ContentType = "text/plain ";
                    break;
                default:
                    ContentType = "application/octet-stream ";
                    break;
            }
            return ContentType;
        }
      
    }
}