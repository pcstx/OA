using System;
using System.Collections.Generic;
using System.Text;

namespace GPRP.Entity.Basic
{
   public class MyCalendar
    {
        private int m_ID;
        private string m_UID;
        private string m_EID;
        private string m_EName;
        private string m_STime;
        private string m_ETime;
        private string m_CTime;
        private string m_MEMO;

        public int ID
        {
            get { return m_ID; }
            set { m_ID = value; }
        }

        public string UID
        {
            get { return m_UID; }
            set { m_UID = value; }
        }

        public string EID
        {
            get { return m_EID; }
            set { m_EID = value; }
        }

        public string EName
        {
            get { return m_EName; }
            set { m_EName = value; }
        }

        public string STime
        {
            get { return m_STime; }
            set { m_STime = value; }
        }

        public string ETime
        {
            get { return m_ETime; }
            set { m_ETime = value; }
        }


        public string CTime
        {
            get { return m_CTime; }
            set { m_CTime = value; }
        }


        public string MEMO
        {
            get { return m_MEMO; }
            set { m_MEMO = value; }
        }
             


    }
}
