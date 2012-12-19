using System;
using System.Collections.Generic;
using System.Text;

namespace GPRP.Entity.Basic
{
    public class NewsUser
    {
        private int m_NewsId;
        private string  m_NewsTypeDetailName;
        private string m_NewsTypeUserName;

        public int NewsID
        {
            get { return m_NewsId; }
            set { m_NewsId = value; }
        }

        public string NewsTypeDetailName
        {
            get { return m_NewsTypeDetailName; }
            set { m_NewsTypeDetailName = value; }
        }

        public string NewsTypeuserName
        {
            get { return m_NewsTypeUserName; }
            set { m_NewsTypeUserName = value; }
        }
    }
}
