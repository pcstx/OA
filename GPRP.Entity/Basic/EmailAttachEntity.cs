using System;
using System.Collections.Generic;
using System.Text;

namespace GPRP.Entity.Basic
{
    public class EmailAttachEntity
    {
        private int m_EmailSerialID;
     
        private string m_AttachmentName;
        private string m_AttachmentUrl;
        private string m_AttachType;
        private float m_AttachSize;
        private string m_AttachClientName;

        public string AttachType
        {
            get { return m_AttachType; }
            set { m_AttachType = value; }
        }

        public float AttachSize
        {
            get { return m_AttachSize; }
            set { m_AttachSize = value; }
        }

        public string AttachClientName
        {
            get { return m_AttachClientName; }
            set { m_AttachClientName = value; }
        }
        public string AttachmentName
        {
            get { return m_AttachmentName; }
            set { m_AttachmentName = value; }
        }

        public string AttachmentUrl
        {
            get { return m_AttachmentUrl; }
            set { m_AttachmentUrl = value; }
        }

        public int EmailSerialID
        {
            get { return m_EmailSerialID; }
            set { m_EmailSerialID = value; }
        }
    }
}
