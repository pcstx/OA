using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GPRP.Web.UI;
using GPRP.Entity.Basic;
using MyADO;
using GPRP.Entity;
using System.Data;
using FredCK.FCKeditorV2;
using GOA.UserControl;
using System.Collections;




namespace GOA.Basic
{
    public partial class WriteMail : BasePage
    {
        private ArrayList dtFilesUpLoad;    //字段附件
        private Int32[] FilesUpLoadFieldID; //字段附件字段ID


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
             
                 string szAction ="" ;
                if(Context.Request["action"].ToString()!=null)
                    szAction = Context.Request["action"].ToString();
                if (szAction == "ReSend" || szAction == "ChangeSend")
                {
                    string szTime = Context.Request["Time"].ToString();
                    string szSendTo = Context.Request["SendTo"].ToString();
                    //dpSendTo.Items.FindByText(szSendTo).Selected = true;
                    txtSendTo.Text = szSendTo;
                    txtTitle.Text = Context.Request["Title"].ToString();
                    string Content = Context.Request["Content"].ToString();
                    string AllContent = "<br><br><br><br><br><br><br><br> 在" + szTime + "对" + Context.Request["SendTo"].ToString() + "写道： <br>" + Content + "<br>--------------------------------------------------------------<br>";
               
                   FreeTextBox1.Text = AllContent;
                 }
                ScriptManager.RegisterOnSubmitStatement(this.FCKeditor1, FCKeditor1.GetType(), "FCKeditor1", "FCKUpdateLinkedField('" + FCKeditor1.ClientID + "');");
                BindGridView();
            }
         
   

        }



        #region aspnetPage 分页代码
        //此类无须更改
        protected void AspNetPager1_PageChanged(object src, EventArgs e)
        {

            BindGridView();

        }
        protected void AspNetPager1_PageChanging(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
        {
            Session["010101AspNetPageCurPage"] = e.NewPageIndex;
        }
        #endregion



        #region  gridView 事件
        protected void GridView1_RowCreated(Object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

            }
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "select")
            {
               



            }
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

            }
        }
        #endregion

        protected void Page_Init(object sender, EventArgs e)
        {

            FileUploadEmail.IsEdit = 1;


           

        }


        private void BindGridView()
        {

            string WhereCondition = "";
            string tables = @"UserList u";
            DataTable dt = DbHelper.GetInstance().GetDBRecords("UserName=u.UserName,UserAddress=u.UserID,u.UserSerialID", tables, WhereCondition, "u.UserSerialID", AspNetPager1.PageSize, AspNetPager1.CurrentPageIndex);
            if (dt.Rows.Count > 0)
                AspNetPager1.RecordCount = Convert.ToInt32(dt.Rows[0]["RecordCount"]);
            else
                AspNetPager1.RecordCount = 0;

            GridView1.DataSource = dt;
            GridView1.DataBind();
            BuildNoRecords(GridView1, dt);
        }

        public void BuildNoRecords(GridView gridView, DataTable ds)
        {
            try
            {
                if (ds.Rows.Count == 0)
                {
                    ds.Rows.Add(ds.NewRow());
                    gridView.DataSource = ds;
                    gridView.DataBind();
                    int columnCount = gridView.Rows[0].Cells.Count;
                    gridView.Rows[0].Cells.Clear();
                    gridView.Rows[0].Cells.Add(new TableCell());
                    gridView.Rows[0].Cells[0].ColumnSpan = columnCount;
                    gridView.Rows[0].Cells[0].Text = "No Records Found.";
                }
            }
            catch
            {
            }
        }

        protected void DropDownListInite()
        {
            //DataTable dtPEEBI = DbHelper.GetInstance().GetUserList();
            //if (dtPEEBI.Rows.Count > 0)
            //{
            //    for (int i = 0; i < dtPEEBI.Rows.Count; i++)
            //        dpSendTo.Items.Add(new ListItem(dtPEEBI.Rows[i]["UserName"].ToString(), dtPEEBI.Rows[i]["UserID"].ToString()));
            //}
        }



      

        protected void btnSendMail_Click(object sender, EventArgs e)
        {
           
            string szResult ="";
            string szUserSerialID = "";
          //  string szFileName = lblHideMessage.Text;
            string szAllName = "";
            string szAllSecret = "";
            string savePath = "..";
            DataTable dtValue = (DataTable)FileUploadEmail.dtAttach;
            szAllName = txtSendTo.Text;
            szAllSecret = txtlblScret.Text;
            EmailEntity _EmailEntity = new EmailEntity();
            _EmailEntity.UserID = userEntity.UserID;
            _EmailEntity.SenderID = szAllName;
            _EmailEntity.ReceiverID = "";
            _EmailEntity.SendTime = DateTime.Now;
            _EmailEntity.ReceiveTime = DateTime.Now;
            _EmailEntity.MailTitle = txtTitle.Text;
            _EmailEntity.MailContent = FreeTextBox1.Text;
            _EmailEntity.ISRead = 0;
            if (szAllSecret == "")
                _EmailEntity.SecretSenderID = "";
            else
                _EmailEntity.SecretSenderID = szAllSecret;
            if (dtValue.Rows.Count > 0)
                _EmailEntity.AttachID ="" ;
            else
                _EmailEntity.AttachID = "";

            _EmailEntity.UserMasterID = 0;
            szUserSerialID = DbHelper.GetInstance().AddEmailInfor(_EmailEntity);

           
            if (dtValue.Rows.Count > 0)
            {
                for (int jj = 0; jj < dtValue.Rows.Count; jj++)
                {
                    EmailAttachEntity _EmailAttachEntity = new EmailAttachEntity();
                    _EmailAttachEntity.AttachSize = Convert.ToSingle(dtValue.Rows[jj]["AttachSize"].ToString());
                    _EmailAttachEntity.AttachType = dtValue.Rows[jj]["AttachType"].ToString(); ;
                    _EmailAttachEntity.AttachClientName = dtValue.Rows[jj]["AttachClientName"].ToString();
                    _EmailAttachEntity.AttachmentName = dtValue.Rows[jj]["AttachmentName"].ToString() ;
                    _EmailAttachEntity.AttachmentUrl = Server.MapPath(savePath) + "\\" + dtValue.Rows[jj]["AttachmentName"].ToString();
                    _EmailAttachEntity.EmailSerialID = Int32.Parse(szUserSerialID);

                    szResult = DbHelper.GetInstance().AddEmailAttachmentInfor(_EmailAttachEntity);
                }

            }

            int iCount = 0;
            string[] szOneAllName = szAllName.Split(';');
            foreach (string szOneName in szOneAllName)
            {
                if (szOneName != "")
                {
                    string[] szName = szOneName.Split('(');
                    foreach (string szUserName in szName)
                    {
                        iCount++;
                        if (iCount == 1)
                        { 
                            _EmailEntity = new EmailEntity();
                            _EmailEntity.UserID = szUserName;
                            _EmailEntity.SenderID = "";
                            _EmailEntity.ReceiverID = userEntity.UserID;
                            _EmailEntity.SendTime = DateTime.Now;
                            _EmailEntity.ReceiveTime = DateTime.Now;
                            _EmailEntity.MailTitle = txtTitle.Text;
                            _EmailEntity.MailContent = FreeTextBox1.Text;
                            _EmailEntity.ISRead = 0;  //0: 未读   1: 已读  2:删除(非彻底删除)
                            _EmailEntity.SecretSenderID = "";
                            _EmailEntity.IsScret = 0;
                            if (dtValue.Rows.Count > 0)
                                _EmailEntity.AttachID = "";
                            else
                                _EmailEntity.AttachID = "";
                            _EmailEntity.UserMasterID = Int32.Parse(szUserSerialID);
                            szResult = DbHelper.GetInstance().AddEmailInfor(_EmailEntity);

                            }
                        else
                            iCount = 0;

                    }
                }
            }
            iCount = 0;
            string[] szOneSecretName = szAllSecret.Split(';');
            foreach (string szOneName in szOneSecretName)
            {
                if (szOneName != "")
                {
                    string[] szName = szOneName.Split('(');
                    foreach (string szUserName in szName)
                    {
                        iCount++;
                        if (iCount == 1)
                        {
                            _EmailEntity = new EmailEntity();
                            _EmailEntity.UserID = szUserName;
                            _EmailEntity.SenderID = "";
                            _EmailEntity.ReceiverID = userEntity.UserID;
                            _EmailEntity.SendTime = DateTime.Now;
                            _EmailEntity.ReceiveTime = DateTime.Now;
                            _EmailEntity.MailTitle = txtTitle.Text;
                            _EmailEntity.MailContent = FreeTextBox1.Text;
                            _EmailEntity.ISRead = 0;  //0: 未读   1: 已读  2:删除(非彻底删除)
                            _EmailEntity.SecretSenderID = "";
                            _EmailEntity.IsScret = 1;  //1:密件
                            if (dtValue.Rows.Count > 0)
                                _EmailEntity.AttachID = "" ;
                            else
                                _EmailEntity.AttachID = "";
                            _EmailEntity.UserMasterID = Int32.Parse(szUserSerialID);
                            szResult = DbHelper.GetInstance().AddEmailInfor(_EmailEntity);
                        }
                        else
                            iCount = 0;

                    }
                }
            }


           
            Response.Redirect("Success.aspx");
        
        }

        protected void DeleAttach_Click(object sender, EventArgs e)
        {
           
          
        }

        protected void txtPageSize_TextChanged(object sender, EventArgs e)
        { }

      
   

        protected void AddUser_Click(object sender, EventArgs e)
        {
            string szAllUserName = "";
            string szText = "";
            szText = HiddenField1.Value;
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                if (((CheckBox)GridView1.Rows[i].FindControl("item")).Checked == true)
                {
                    string szUserID = GridView1.Rows[i].Cells[3].Text;
                    string szUserName =GridView1.Rows[i].Cells[2].Text;
                    szAllUserName += szUserID + "(" + szUserName +");";
 
                }
            }
            if (szText.Equals("添加密件人"))
                txtlblScret.Text = szAllUserName;
            else if (szText.Equals("添加收件人"))
                txtSendTo.Text = szAllUserName;
            UserListModalPopupExtender.Hide();

        }

       
        protected void AddAttach_Click(object sender, EventArgs e)
        {
        
            string savePath = "UploadFiles";
           // lblMessage.Text = "";
            if (!System.IO.Directory.Exists(Server.MapPath(savePath)))
            { 
                System.IO.Directory.CreateDirectory(Server.MapPath(savePath));
            }
            if (FileUpload1.HasFile)
            {
                try
                {
                    FileUpload1.SaveAs(Server.MapPath(savePath) + "\\" + FileUpload1.FileName);
                    //lblMessage.Text = lblMessage.Text + "客户端路径：" + FileUpload1.PostedFile.FileName + "<br>" +
                    //              "文件名：" + System.IO.Path.GetFileName(FileUpload1.FileName) + "<br>" +
                    //              "文件扩展名：" + System.IO.Path.GetExtension(FileUpload1.FileName) + "<br>" +
                    //              "文件大小：" + FileUpload1.PostedFile.ContentLength + " KB<br>" +
                    //              "文件MIME类型：" + FileUpload1.PostedFile.ContentType + "<br>" +
                    //              "保存路径：" + Server.MapPath(savePath) + "\\" + FileUpload1.FileName +
                    //              "<hr>";
                    //lblMessage.Text = lblMessage.Text + "文件名：" + System.IO.Path.GetFileNameWithoutExtension(FileUpload1.FileName) + "  文件大小：" + FileUpload1.PostedFile.ContentLength + " KB";
                   // lblHideMessage.Text = System.IO.Path.GetFileName(FileUpload1.FileName);
                 
                }
                catch (Exception ex)
                {
                    //lblMessage.Text = "发生错误：" + ex.Message.ToString();
                }
            }
            else
            {
               // lblMessage.Text = "没有选择要上传的文件！";
            }
           showWriteModalPopup.Hide();
        
        }

    }
}
