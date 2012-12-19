using System;
using System.Collections.Generic;
using System.Text;

namespace GPRP.Entity.Basic
{
   public  class CalendarEventEntity
    {
       private int m_Userial;
       private string  m_Title;
       private string m_Content;
       private int m_Type;
       private DateTime m_StartTime;
       private DateTime m_EndTime;
       private string m_Invite;
       private string m_EmailNote;
       private string m_Note;
       private int m_NoteBefore;
       private string m_Repeat;
       private int m_RepeatRate;
       private int m_UserID;

       public int UserID
       {
           set { m_UserID = value; }
           get { return m_UserID; }
       }

       public int Userial
       {
           set { m_Userial = value; }
           get { return m_Userial; }

       }

       public string Title
       {
           set { m_Title = value; }
           get { return m_Title; }

       }
       public string Content
       {
           set { m_Content = value; }
           get { return m_Content; }

       }

       public int Type
       {
           set { m_Type = value; }
           get { return m_Type; }

       }
       public DateTime StartTime
       {
           set { m_StartTime = value; }
           get { return m_StartTime; }

       }

       public DateTime EndTime
       {
           set { m_EndTime = value; }
           get { return m_EndTime; }

       }
       public string Invite
       {
           set { m_Invite = value; }
           get { return m_Invite; }

       }

       public string EmailNote
       {
           set { m_EmailNote = value; }
           get { return m_EmailNote; }

       }
       public string Note
       {
           set { m_Note = value; }
           get { return m_Note; }

       }

       public int NoteBefore
       {
           set { m_NoteBefore = value; }
           get { return m_NoteBefore; }

       }
       public string Repeat
       {
           set { m_Repeat = value; }
           get { return m_Repeat; }

       }

       public int RepeatRate
       {
           set { m_RepeatRate = value; }
           get { return m_RepeatRate; }

       }

    }
}
