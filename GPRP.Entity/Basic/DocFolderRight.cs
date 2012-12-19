using System;
using System.Collections.Generic;
using System.Text;

namespace GPRP.Entity.Basic
{
   public class DocFolderRight
    {
       private string m_FolderName;
       private string m_FullFolderName;
       private int m_FatherID;

       public string FolderName
       {
           get { return m_FolderName; }
           set { m_FolderName = value; }

       }

       public string FullFolderName
       {
           get { return m_FullFolderName; }
           set { m_FullFolderName = value; }
       }


       public int FatherID
       {
           get { return m_FatherID; }
           set { m_FatherID = value; }
       }
    }
}
