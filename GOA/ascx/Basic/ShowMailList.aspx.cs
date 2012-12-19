using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GPRP.Web.UI;
using System.Data;
using MyADO;
using System.IO;

namespace GOA.Basic
{
    public partial class ShowMailList : BasePage
    {

        private int g_MailId;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                FileDownLoadEmail.IsEdit = 1;

            }

        }

        protected void Page_Init(object sender, EventArgs e)
        {
            int MailId = Int32.Parse(Context.Request["MailID"].ToString());
            g_MailId = MailId;
            string action = Context.Request["action"].ToString();
            DataTable DtMail = new DataTable();
            if (action == "recv")
                DtMail = DbHelper.GetInstance().GetRecvEmailInfoById(MailId);
            else if (action == "send")
            {
                DtMail = DbHelper.GetInstance().GetSendEmailInfoById(MailId);
                if (DtMail.Rows.Count > 1)
                    btnAllSendMail.Visible = true;
                btnReSendMail.Text = "Button_SendTwice";

            }
            else if (action == "dele")
            {
                DtMail = DbHelper.GetInstance().GetDeleteEmailInfoById(MailId);
                if (DtMail.Rows.Count > 1)
                    btnAllSendMail.Visible = true;
                btnReSendMail.Text = "Button_SendTwice";

            }
            if (DtMail.Rows.Count > 0)
            {
                lblTitle.Text = DtMail.Rows[0]["MailTitle"].ToString();
                if (action == "recv")
                {
                    string szName = "";

                    int UserMasterID = Int32.Parse(DtMail.Rows[0]["UserMasterID"].ToString());
                    MailId = UserMasterID;
                    DataTable DtUser = DbHelper.GetInstance().GetRecvEmailInfoByUserMasterID(UserMasterID);
                    if (DtUser.Rows.Count > 1)
                        btnAllSendMail.Visible = true;

                    for (int j = 0; j < DtUser.Rows.Count; j++)
                    {
                        string id = DtUser.Rows[j]["UserID"].ToString();
                        string name = DtUser.Rows[j]["UserName"].ToString();
                        int IsScret = Int32.Parse(DtUser.Rows[j]["IsScret"].ToString());
                        if (IsScret == 0)
                            szName = szName + id + "(" + name + ");";
                    }
                    lbUserName.Text = DtMail.Rows[0]["RecvAdd"].ToString() + "(" + DtMail.Rows[0]["RecvName"].ToString() + ");";
                    lblSender.Text = szName;

                }
                else if (action == "send")
                {
                    lblSender.Text = DtMail.Rows[0]["SendAdd"].ToString();
                    lbUserName.Text = userEntity.UserID + "(" + userEntity.UserName + ");";
                    btnAllSendMail.Visible = true;
                    btnRecv.Visible = true;
                }
                else if (action == "dele")
                {
                    lblSender.Text = DtMail.Rows[0]["SendAdd"].ToString();
                    lbUserName.Text = DtMail.Rows[0]["RecvAdd"].ToString();
                    btnAllSendMail.Visible = true;
                    //btnRecv.Visible = true;

                }
                lbContent.Text = DtMail.Rows[0]["MailContent"].ToString();
                lbltime.Text = DtMail.Rows[0]["Time"].ToString();

            }

            //DataTable DtMailAttach = DbHelper.GetInstance().GetEmailAttachInfoById(MailId);
            //string strText = "";
            //if (DtMailAttach.Rows.Count > 0)
            //{
            //    for (int i = 0; i < DtMailAttach.Rows.Count; i++)
            //    {

            //        strText = "附件" + (i + 1).ToString() + ":";
            //        string AttachmentName = DtMailAttach.Rows[i]["AttachClientName"].ToString();
            //        strText += AttachmentName;
            //        LinkButton linkBtnAttValue = new LinkButton();
            //        linkBtnAttValue.ID = "fujian" + i.ToString();
            //        linkBtnAttValue.Text = strText;
            //        linkBtnAttValue.CommandArgument = DtMailAttach.Rows[i]["AttachmentName"].ToString() + DtMailAttach.Rows[i]["AttachType"].ToString();
            //        linkBtnAttValue.Command += new CommandEventHandler(this.button_Click);

            //        //string AttachmentUrl = DtMailAttach.Rows[i]["AttachmentUrl"].ToString();
            //        //ViewState["Url"] = AttachmentUrl;
            //        placeHolder.Controls.Add(linkBtnAttValue);
            //    }
            //}

            //  Attachment.Text = strText;
        }


        //protected void button_Click(object sender, CommandEventArgs e)
        //{

        //    string AttachmentUrl = e.CommandArgument.ToString();
        //    FileDownload("E:\\Gegon_Code\\GeogonOA\\GeogonOA\\GOA\\attachment\\RequestAttach\\0_0_20120530154751192036.jpg");
        //}

    
        protected void btnRecv_Click(object sender, EventArgs e)  //回收信件
        {
            DataTable DtUser = DbHelper.GetInstance().GetRecvEmailInfoByUserMasterID(g_MailId);
            if (DtUser.Rows.Count > 0)
            {
                for (int i = 0; i < DtUser.Rows.Count; i++)
                {
                    int iIsRead = Int32.Parse(DtUser.Rows[i]["ISRead"].ToString());
                    if (iIsRead == 0) //没读的邮件回收，即删除
                    {
                        int id = Int32.Parse(DtUser.Rows[i]["UserSerialID"].ToString());
                        int iResult = DbHelper.GetInstance().DeleteEmailByUserSerialID(id);
                    }
                }
            }
        }



        protected void btnReSendMail_Click(object sender, EventArgs e)
        {
            GPRP.GPRPControls.Button btn = (GPRP.GPRPControls.Button)sender;
            string szText = "";
            szText = btn.Text;
            if (szText.Trim().Equals("再次发送"))
                Context.Response.Redirect("WriteMail.aspx?action=ReSend&SendTo=" + lblSender.Text + "&Content=" + lbContent.Text + "&Title=Re:" + lblTitle.Text + "&Time=" + lbltime.Text);
            else if (szText.Trim().Equals("回复"))
                Context.Response.Redirect("WriteMail.aspx?action=ReSend&SendTo=" + lbUserName.Text + "&Content=" + lbContent.Text + "&Title=Re:" + lblTitle.Text + "&Time=" + lbltime.Text);
     
        
        }


        protected void btnAllSendMail_Click(object sender, EventArgs e ) //全部回复
        {
            Context.Response.Redirect("WriteMail.aspx?action=ReSend&SendTo=" + lblSender.Text + lbUserName.Text + "&Content=" + lbContent.Text + "&Title=Re:" + lblTitle.Text + "&Time=" + lbltime.Text);
     
        }

        protected void btnChangeSendMail_Click(object sender, EventArgs e)//转发
        {
            string szSendTo = "";
            Context.Response.Redirect("WriteMail.aspx?action=ChangeSend&SendTo= " + szSendTo + "&Content=" + lbContent.Text + "&Title=Re:" + lblTitle.Text + "&Time=" + lbltime.Text + "&MailID=" + g_MailId);
        }
    }
}
