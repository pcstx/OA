using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using GPRP.Entity.Basic;
using MyADO;
using System.Data;
using System.Web.SessionState;
using System.Web.Script.Serialization;
using System.Text;
using GPRP.Entity;

namespace GOA.MyCalendar
{
    /// <summary>
    /// $codebehindclassname$ 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class CalendarEvent : IHttpHandler, IRequiresSessionState 
    {

        public void ProcessRequest(HttpContext context)
        {
            string myaction = context.Request.Params["action"];
            string temp = "";
            if (myaction.ToLower() == "add")
            {
                string Title = context.Request.Params["TxtTitle"];
                string Content = context.Request.Params["TxtContent"];
                string Type = context.Request.Params["drType"];
                string BeginTime = context.Request.Params["TxtBeginTime"];
                string EndTime = context.Request.Params["TxtEndTime"];
                string Invite = context.Request.Params["TxtInvite"];
                string ckRemind = context.Request.Params["ckTiXing"];  //是否提醒
                string Remind = context.Request.Params["drTiXing"];   //多久提醒
                string ckRepeat = context.Request.Params["ckRepeat"];  //是否重复
                string RepeatRate = context.Request.Params["drRepeatRate"];  //重复频率
                string ckEmailNote = context.Request.Params["ckEmailNote"];  //是否邮件提醒

                CalendarEventEntity _CalendarEvent = new CalendarEventEntity();
                _CalendarEvent.Content = Content;
                _CalendarEvent.EmailNote = ckEmailNote;
                _CalendarEvent.EndTime = DateTime.Parse(EndTime);
                _CalendarEvent.Invite = Invite;
                _CalendarEvent.Note = ckRemind;
                _CalendarEvent.NoteBefore = Int16.Parse(Remind);
                _CalendarEvent.Repeat = ckRepeat;
                _CalendarEvent.RepeatRate = Int16.Parse(RepeatRate);
                _CalendarEvent.StartTime = DateTime.Parse(BeginTime);
                _CalendarEvent.Title = Title;
                _CalendarEvent.Type = Int16.Parse(Type);
                _CalendarEvent.UserID = Int32.Parse(context.Session["UserID"].ToString());
                int id = DbHelper.GetInstance().AddNewsCalenderEvent(_CalendarEvent);

              
                     string[] UserList = Invite.Split(',');
                    foreach(string UserID in UserList)
                    {
                        if (UserID != "")
                        {
                            if (ckEmailNote == "true")  //发邮件提醒
                            {
                                EmailEntity _EmailEntity = new EmailEntity();
                                _EmailEntity.UserID = context.Session["UserID"].ToString();
                                _EmailEntity.SenderID = UserID;
                                _EmailEntity.ReceiverID = context.Session["UserID"].ToString();
                                _EmailEntity.SendTime = DateTime.Now;
                                _EmailEntity.ReceiveTime = DateTime.Now;
                                _EmailEntity.MailTitle = "[提醒]" + Title;
                                _EmailEntity.MailContent = Content;
                                _EmailEntity.ISRead = 0;  //0: 未读   1: 已读  2:删除(非彻底删除)
                                _EmailEntity.SecretSenderID = "";
                                _EmailEntity.IsScret = 0;
                                _EmailEntity.AttachID = "";
                                _EmailEntity.UserMasterID = 0;
                                string szResult = DbHelper.GetInstance().AddEmailInfor(_EmailEntity);
                            }
                            //添加到提醒好友的事件中
                            _CalendarEvent.Content = Content;
                            _CalendarEvent.EmailNote = ckEmailNote;
                            _CalendarEvent.EndTime = DateTime.Parse(EndTime);
                            _CalendarEvent.Invite = Invite;
                            _CalendarEvent.Note = ckRemind;
                            _CalendarEvent.NoteBefore = Int16.Parse(Remind);
                            _CalendarEvent.Repeat = ckRepeat;
                            _CalendarEvent.RepeatRate = Int16.Parse(RepeatRate);
                            _CalendarEvent.StartTime = DateTime.Parse(BeginTime);
                            _CalendarEvent.Title = Title;
                            _CalendarEvent.Type = Int16.Parse(Type);
                            _CalendarEvent.UserID =  Int32.Parse( UserID);
                            int idd = DbHelper.GetInstance().AddNewsCalenderEvent(_CalendarEvent);

                        }

                    }

                

                if (ckRemind=="true")    //提醒
                {}

                if (ckRepeat == "true")   //重复
                { }

                Global.CalendarNote(); //提醒定时器

            }
            else if (myaction.ToLower() == "show")
            {
                int UserID = Int32.Parse(context.Session["UserID"].ToString());
                int id = Int32.Parse(context.Request.Params["activity.id"].ToString());
                StringBuilder jsonBuilder = new StringBuilder();
                DataTable dtCalendar = DbHelper.GetInstance().GetCalenderEventByID(id);
                if (dtCalendar.Rows.Count > 0)
                {
                   
                    if (dtCalendar.Rows.Count > 0)
                    {
                        jsonBuilder.Append("{\"");
                        jsonBuilder.Append(dtCalendar.TableName);
                        jsonBuilder.Append("\":[");
                        for (int i = 0; i < dtCalendar.Rows.Count; i++)
                        {
                            jsonBuilder.Append("{");
                            for (int j = 0; j < dtCalendar.Columns.Count; j++)
                            {
                                jsonBuilder.Append("\"");
                                jsonBuilder.Append(dtCalendar.Columns[j].ColumnName);
                                jsonBuilder.Append("\":\"");
                                jsonBuilder.Append(dtCalendar.Rows[i][j].ToString());
                                jsonBuilder.Append("\",");
                            }

                            jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
                            jsonBuilder.Append("},");
                        }
                        jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
                        jsonBuilder.Append("]");
                        jsonBuilder.Append("}");
                    }
                }

                context.Response.Write(jsonBuilder);

            }
            else if (myaction.ToLower() == "delete")
            {
            }
            else if (myaction.ToLower() == "update")
            {
                int id = Int32.Parse(context.Request.Form["activity.id"].ToString());
                CalendarEventEntity _CalendarEventEntity = DbHelper.GetInstance().GetCalendarEntityByID(id);

                string Title = context.Request.Params["TxtTitle"];
                string Content = context.Request.Params["TxtContent"];
                string Type = context.Request.Params["drType"];
                string BeginTime = context.Request.Params["TxtBeginTime"];
                string EndTime = context.Request.Params["TxtEndTime"];
                string Invite = context.Request.Params["TxtInvite"];
                string ckRemind = context.Request.Params["ckTiXing"];  //是否提醒
                string Remind = context.Request.Params["drTiXing"];   //多久提醒
                string ckRepeat = context.Request.Params["ckRepeat"];  //是否重复
                string RepeatRate = context.Request.Params["drRepeatRate"];  //重复频率
                string ckEmailNote = context.Request.Params["ckEmailNote"];  //是否邮件提醒

                _CalendarEventEntity.Content = Content;
                _CalendarEventEntity.EmailNote = ckEmailNote;
                _CalendarEventEntity.EndTime = DateTime.Parse(EndTime);
                _CalendarEventEntity.Invite = Invite;
                _CalendarEventEntity.Note = ckRemind;
                _CalendarEventEntity.NoteBefore = Int16.Parse(Remind);
                _CalendarEventEntity.Repeat = ckRepeat;
                _CalendarEventEntity.RepeatRate = Int16.Parse(RepeatRate);
                _CalendarEventEntity.StartTime = DateTime.Parse(BeginTime);
                _CalendarEventEntity.Title = Title;
                _CalendarEventEntity.Type = Int16.Parse(Type);

                int id_update = DbHelper.GetInstance().UpDateCalendarEvnetByID(_CalendarEventEntity);
                Global.CalendarNote(); //提醒定时器
            }
            else if (myaction.ToLower() == "save")
            {
                string title = context.Request.Form["activity.title"];
                DateTime startTime = DateTime.Parse(context.Request.Form["activity.startTime"]);
                DateTime endTime = DateTime.Parse(context.Request.Form["activity.endTime"]);
                CalendarEventEntity _CalendarEventEntity = new CalendarEventEntity();
                _CalendarEventEntity.UserID = Int32.Parse(context.Session["UserID"].ToString());
                _CalendarEventEntity.Type = 0;
                _CalendarEventEntity.Title = title;
                _CalendarEventEntity.StartTime = startTime;
                _CalendarEventEntity.RepeatRate = 0;
                _CalendarEventEntity.Repeat = "";
                _CalendarEventEntity.NoteBefore = 0;
                _CalendarEventEntity.Note = "";
                _CalendarEventEntity.Invite = "";
                _CalendarEventEntity.EndTime = endTime;
                _CalendarEventEntity.EmailNote = "";
                _CalendarEventEntity.Content = "";
                int id = DbHelper.GetInstance().AddNewsCalenderEvent(_CalendarEventEntity);
                if (id != 0)
                {
                    SortedDictionary<string, object> values = new SortedDictionary<string, object>();
                    values.Add("returnStr", "success");
                    values.Add("id", id);
                    context.Response.Write(new JavaScriptSerializer().Serialize(values));
                }
                Global.CalendarNote(); //提醒定时器
            }
            else if (myaction.ToLower() == "loadmonth")
            {
                DateTime startTime = DateTime.Parse(context.Request.Form["activity.startTime"]);
                DateTime endTime = DateTime.Parse(context.Request.Form["activity.endTime"]);
                int PermissionUserID = Int32.Parse(context.Request.Params["PerssionUserID"].ToString());
                int UserID;
                if (PermissionUserID == 0)

                    UserID = Int32.Parse(context.Session["UserID"].ToString());
                else
                    UserID = PermissionUserID;
                ////本月第一天时间 
                //DateTime dt_First = startTime.AddDays(-(startTime.Day) + 1);
                ////将本月月数+1 
                //DateTime dt2 = startTime.AddMonths(1);
                ////本月最后一天时间 
                //DateTime dt_Last = dt2.AddDays(-(startTime.Day));
               DataTable dtMonth = DbHelper.GetInstance().GetCalendarByDate(startTime, endTime, UserID);
               StringBuilder jsonBuilder = new StringBuilder();
               if (dtMonth.Rows.Count > 0)
               {
                 
                   if (dtMonth.Rows.Count > 0)
                   {
                       jsonBuilder.Append("{\"");
                       jsonBuilder.Append(dtMonth.TableName);
                       jsonBuilder.Append("\":[");
                       for (int i = 0; i < dtMonth.Rows.Count; i++)
                       {
                           jsonBuilder.Append("{");
                           for (int j = 0; j < dtMonth.Columns.Count; j++)
                           {
                               jsonBuilder.Append("\"");
                               jsonBuilder.Append(dtMonth.Columns[j].ColumnName);
                               jsonBuilder.Append("\":\"");
                               jsonBuilder.Append(dtMonth.Rows[i][j].ToString());
                               jsonBuilder.Append("\",");
                           }

                           jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
                           jsonBuilder.Append("},");
                       }
                       jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
                       jsonBuilder.Append("]");
                       jsonBuilder.Append("}");
                   }
               }
               context.Response.Write(jsonBuilder);

            }
            else if (myaction.ToLower() == "loadweek")
            {
                DateTime startTime = DateTime.Parse(context.Request.Form["activity.startTime"]);
                DateTime endTime = DateTime.Parse(context.Request.Form["activity.endTime"]);
                int PermissionUserID = Int32.Parse( context.Request.Params["PerssionUserID"].ToString());
                int UserID;
                if(PermissionUserID==0)
                
                     UserID = Int32.Parse(context.Session["UserID"].ToString());
                else 
                    UserID = PermissionUserID;
                //int weeknow = Convert.ToInt32(startTime.DayOfWeek);
                ////星期日 获取weeknow为0 
                //weeknow = weeknow == 0 ? 7 : weeknow;
                //int daydiff = (-1) * weeknow + 1;
                //int dayadd = 7 - weeknow;
                ////本周第一天
                //DateTime datebegin = startTime.AddDays(daydiff);
                ////本周最后一天
                //DateTime dateend = startTime.AddDays(dayadd);

                DataTable dtWeek = DbHelper.GetInstance().GetCalendarByDate(startTime, endTime, UserID);
                StringBuilder jsonBuilder = new StringBuilder();
                if (dtWeek.Rows.Count > 0)
                {
                    if (dtWeek.Rows.Count > 0)
                    {
                        jsonBuilder.Append("{\"");
                        jsonBuilder.Append(dtWeek.TableName);
                        jsonBuilder.Append("\":[");
                        for (int i = 0; i < dtWeek.Rows.Count; i++)
                        {
                            jsonBuilder.Append("{");
                            for (int j = 0; j < dtWeek.Columns.Count; j++)
                            {
                                jsonBuilder.Append("\"");
                                jsonBuilder.Append(dtWeek.Columns[j].ColumnName);
                                jsonBuilder.Append("\":\"");
                                jsonBuilder.Append(dtWeek.Rows[i][j].ToString());
                                jsonBuilder.Append("\",");
                            }

                            jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
                            jsonBuilder.Append("},");
                        }
                        jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
                        jsonBuilder.Append("]");
                        jsonBuilder.Append("}");
                    }

                    context.Response.Write(jsonBuilder);
                }
            }
     
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}
