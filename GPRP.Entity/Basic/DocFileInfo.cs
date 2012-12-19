using System;
using System.Collections.Generic;
using System.Text;

namespace GPRP.Entity.Basic
{
   public  class DocFileInfo
    {
       private int m_FileSerialID;
        private string m_FileName;
        private string m_FileFolderName;
        private int m_FileEdition;
        private string m_FileNote;
        private int m_FileES;
        private int m_FileSize;
        private DateTime m_FileModifyDate;
        private DateTime m_FileValidPeriod;
        private int m_FileModifyUserId;

        public int FileSerialID
        {
            get { return m_FileSerialID; }
            set { m_FileSerialID = value; }
        }

        public string FileName
        {
            get { return m_FileName; }
            set { m_FileName = value; }
        }

        public string FileFolderName
        {
            get { return m_FileFolderName; }
            set { m_FileFolderName = value; }
        }

        public int FileEdition
        {
            get { return m_FileEdition; }
            set { m_FileEdition = value; }
        }

        public string FileNote
        {
            get { return m_FileNote; }
            set { m_FileNote = value; }
        }

        public int FileES
        {
            get { return m_FileES; }
            set { m_FileES = value; }
        }

        public int FileSize
        {
            get { return m_FileSize; }
            set { m_FileSize = value; }
        }

         public int FileModifyUserId
        {
            get { return m_FileModifyUserId; }
            set { m_FileModifyUserId = value; }
        }

         public DateTime FileModifyDate
         {
             get { return m_FileModifyDate; }
             set { m_FileModifyDate = value; }
         }

         public DateTime FileValidPeriod
         {
             get { return m_FileValidPeriod; }
             set { m_FileValidPeriod = value; }
         }

    }
}
