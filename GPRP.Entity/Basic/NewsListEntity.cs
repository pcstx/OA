using System;
using System.Collections.Generic;
using System.Text;

namespace GPRP.Entity.Basic
{
    /// <summary>
    /// 信息维护
    /// </summary>
   public class NewsListEntity
    {
      
        private string m_NewsTitle;
        private string m_NewsBody;
        private int m_NewsTypeId;
        private char m_IsPublish;
        private DateTime m_ExpDate;
        private DateTime m_CreateDate;
        private string m_Creator;
        private DateTime m_PublishDate;
        private string m_LastModifier;
        private DateTime m_LastModifyDate;

        public string NewsTitle
        {
            get { return m_NewsTitle; }
            set { m_NewsTitle = value; }
        }

        public string NewsBody
        {
            get { return m_NewsBody; }
            set { m_NewsBody = value; }
        }

        public int NewsTypeID
        {
            get { return m_NewsTypeId; }
            set { m_NewsTypeId = value; }
        }

        public char IsPublish
        {
            get { return m_IsPublish; }
            set { m_IsPublish = value; }
        }

        public DateTime ExpireDate
        {
            get { return m_ExpDate; }
            set { m_ExpDate = value; }
        }

        public DateTime CreateDate
        {
            get { return m_CreateDate; }
            set { m_CreateDate = value; }
        }

        public string Creator
        {
            get { return m_Creator; }
            set { m_Creator = value; }
        }

        public DateTime PublishDate
        {
            get { return m_PublishDate; }
            set { m_PublishDate = value; }
        }

        public string LastModifier
        {
            get { return m_LastModifier; }
            set { m_LastModifier = value; }
        }

        public DateTime LastModifyDate
        {
            get { return m_LastModifyDate; }
            set { m_LastModifyDate = value; }
        }





    }
}
