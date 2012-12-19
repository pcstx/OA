using System;
using System.Collections.Generic;
using System.Text;

namespace GPRP.Entity.Basic
{
   public class CalendarPermission
    {
       private int m_CalendarPermissionSerialID;
       private int m_CalendarID;
       private string m_CalendarPer;
       private int m_CalendarPermissionUserID;


       public int CalendarPermissionSerialID
       {
           get { return m_CalendarPermissionSerialID; }
           set { m_CalendarPermissionSerialID = value; }
       }


       public int CalendarID
       {
           get { return m_CalendarID; }
           set { m_CalendarID = value; }
       }

 

       public string CalendarPer
       {
           get { return m_CalendarPer; }
           set { m_CalendarPer = value; }
       }


       public int CalendarPermissionUserID
       {
           get { return m_CalendarPermissionUserID; }
           set { m_CalendarPermissionUserID = value; }
       }

    }
}
