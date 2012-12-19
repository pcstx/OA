using System;
using System.Collections.Generic;
using System.Text;

namespace GPRP.Entity
{
    /// <summary>
    /// 内部邮件
    /// </summary>
    public class EmailEntity
    {
        private string m_UserId;
        private string m_SenderId;
        private string m_ReceiverId;
        private string m_MailTitle;
        private string m_MailContent;
        private string m_AttachId;
        private DateTime m_ReceiveTime;
        private DateTime m_SendTime;
        private int m_IsRead;
        private int m_UserMasterID;
        private string m_SecretSenderID;
        private int m_IsScret;

        public string UserID
        {
            get { return m_UserId; }
            set { m_UserId = value; }
        }

        public string SenderID
        {
            get { return m_SenderId; }
            set { m_SenderId = value; }
        }

        public string ReceiverID
        {
            get { return m_ReceiverId; }
            set { m_ReceiverId = value; }
        }
        public string MailTitle
        {
            get { return m_MailTitle; }
            set { m_MailTitle = value; }
        }
        public string MailContent
        {
            get { return m_MailContent; }
            set { m_MailContent = value; }
        }
        public string AttachID
        {
            get { return m_AttachId; }
            set { m_AttachId = value; }
        }
        public DateTime ReceiveTime
        {
            get { return m_ReceiveTime; }
            set { m_ReceiveTime = value; }
        }
        public DateTime SendTime
        {
            get { return m_SendTime; }
            set { m_SendTime = value; }
        }

        public int ISRead
        {
            get { return m_IsRead; }
            set { m_IsRead = value; }
        }

        public int UserMasterID
        {
            get { return m_UserMasterID; }
            set { m_UserMasterID = value; }
        }

        public string SecretSenderID
        {
            get { return m_SecretSenderID; }
            set { m_SecretSenderID = value; }
        }

        public int IsScret
        {
            get { return m_IsScret; }
            set { m_IsScret = value; }
        }
    }
}
