using System;
using System.Collections.Generic;
using System.Text;

namespace GPRP.Entity.Basic
{
    public class DocFileEdition
    {
        private int m_FileID;
        private string m_FileEdition;
        private string m_ModifyUserName;
        private DateTime m_ModifyDate;
        private string m_FileNote;
        private string m_FileUrl;
        private string m_FileName;


        public string FileName
        {
            get { return m_FileName; }
            set { m_FileName = value; }
        }
        public string FileUrl
        {
            get { return m_FileUrl; }
            set { m_FileUrl = value; }
 
        }

        public int FileID
        {
            get { return m_FileID; }
            set { m_FileID = value; }
        
        }


        public string FileEdition
        {
            get { return m_FileEdition; }
            set { m_FileEdition = value; }
        }


        public string ModifyUserName
        {
            get { return m_ModifyUserName; }
            set { m_ModifyUserName = value; }
        }


        public DateTime ModifyDate
        {
            get { return m_ModifyDate; }
            set { m_ModifyDate = value; }
        }


        public string FileNote
        {
            get { return m_FileNote; }
            set { m_FileNote = value; }
        }
    }
}
