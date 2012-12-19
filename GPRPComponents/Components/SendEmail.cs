using System;
using System.Collections.Generic;
using System.Text;

namespace GPRP.GPRPComponents
{
    public class SendEmail
    {
        public static bool mailSend(string host, bool ssl, string from, string to, string subject, string body)
        {
            System.Net.Mail.SmtpClient mail = new System.Net.Mail.SmtpClient();
            mail.Host = host;//smtp
            //mail.Credentials = new System.Net.NetworkCredential(userName, pwd);
            //mail.EnableSsl = ssl;//发送连接套接层是否加密 例如用gmail发是加密的 
            System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage(from, to);

            //System.Net.Mail.MailAddress a = new System.Net.Mail.MailAddress();
            message.Body = body;
            message.Subject = subject;
            message.SubjectEncoding = System.Text.Encoding.GetEncoding("gb2312");
            message.BodyEncoding = System.Text.Encoding.GetEncoding("gb2312");
            message.IsBodyHtml = true;
            try
            {
                mail.Send(message);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool mailSend(string host, bool ssl, string from, string[] toList, string subject, string body)
        {
            System.Net.Mail.SmtpClient mail = new System.Net.Mail.SmtpClient();
            mail.Host = host;//smtp
            //mail.Credentials = new System.Net.NetworkCredential(userName, pwd);
            //mail.EnableSsl = ssl;//发送连接套接层是否加密 例如用gmail发是加密的 
            System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage();
            message.From = new System.Net.Mail.MailAddress(from);

            for (int i = 0; i < toList.Length; i++)
            {
                if (toList[i] != string.Empty)
                {
                    message.To.Add(toList[i]);
                }
            }

            message.Body = body;
            message.Subject = subject;
            message.SubjectEncoding = System.Text.Encoding.GetEncoding("gb2312");
            message.BodyEncoding = System.Text.Encoding.GetEncoding("gb2312");
            message.IsBodyHtml = true;
            try
            {
                mail.Send(message);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool mailSend(string host, bool ssl, string from, string to, string cc, string subject, string body)
        {
            System.Net.Mail.SmtpClient mail = new System.Net.Mail.SmtpClient();
            mail.Host = host;//smtp
            //mail.Credentials = new System.Net.NetworkCredential(userName, pwd);
            //mail.EnableSsl = ssl;//发送连接套接层是否加密 例如用gmail发是加密的 
            System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage(from, to);
            if (cc != string.Empty)
            {
                message.CC.Add(cc);
            }
            message.Body = body;
            message.Subject = subject;
            message.SubjectEncoding = System.Text.Encoding.GetEncoding("gb2312");
            message.BodyEncoding = System.Text.Encoding.GetEncoding("gb2312");
            message.IsBodyHtml = true;
            try
            {
                mail.Send(message);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool mailSend(string host, bool ssl, string from, string[] toList, string[] ccList, string subject, string body)
            {
            System.Net.Mail.SmtpClient mail = new System.Net.Mail.SmtpClient();
            mail.Host = host;//smtp
            //mail.Credentials = new System.Net.NetworkCredential(userName, pwd);
            //mail.EnableSsl = ssl;//发送连接套接层是否加密 例如用gmail发是加密的 
            System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage();
            message.From = new System.Net.Mail.MailAddress(from);

            for (int i = 0; i < toList.Length; i++)
                {
                if (toList[i] != string.Empty)
                    {
                    message.To.Add(toList[i]);
                    }
                }
            for (int i = 0; i < ccList.Length; i++)
                {
                if (ccList[i] != string.Empty)
                    {
                    message.CC.Add(ccList[i]);
                    }
                }

            message.Body = body;
            message.Subject = subject;
            message.SubjectEncoding = System.Text.Encoding.GetEncoding("gb2312");
            message.BodyEncoding = System.Text.Encoding.GetEncoding("gb2312");
            message.IsBodyHtml = true;
            try
                {
                mail.Send(message);
                return true;
                }
            catch
                {
                return false;
                }
            }
    }
}
