using System;
using System.Collections.Generic;
using System.Linq;
using System.Web; 
using System.Data;
using System.Web.Script.Serialization;
using System.Web.Services;
using GPRP.GPRPBussiness; 

namespace GOA.Index
{
    /// <summary>
    /// $codebehindclassname$ 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]

    public class cometHandler : IHttpAsyncHandler
    {

        #region IHttpAsyncHandler 成员

        public IAsyncResult BeginProcessRequest(HttpContext context, AsyncCallback cb, object extraData)
        {
            string localUser = WebUtils.GetCookieUser();  //当前登录用户
            string userName=context.Request["username"];   //发送用户

            warpIAsyncResult warp = new warpIAsyncResult(context, cb, extraData, localUser);
            string msg = context.Request["msg"];
            SigleMessage.Instance.SendMessage(msg, warp,userName);
            return warp;
        }

        public void EndProcessRequest(IAsyncResult result)
        {

        }

        #endregion

        #region IHttpHandler 成员

        public bool IsReusable
        {
            get { return false; }
        }

        public void ProcessRequest(HttpContext context)
        {

        }

        #endregion 
    }

    public class warpIAsyncResult : IAsyncResult
    {
        bool _IsCompleted = false;
        HttpContext context;
        AsyncCallback cb;
        object extraData;

        public string userName { get; set; }

        public HttpContext Context
        {
            get
            {
                return context;
            }
        }

        public warpIAsyncResult(HttpContext context, AsyncCallback cb, object extraData,string userName)
        {
            this.context = context;
            this.cb = cb;
            this.extraData = extraData;
            this.userName = userName;
        }

        public void Send(string msg)
        {
            if (context != null)
            {
                context.Response.ContentType = "text/plain";
                context.Response.Write(msg);
            }
            if (cb != null)
            {
                cb(this);
            }
            _IsCompleted = true;
        }


        #region IAsyncResult 成员

        public object AsyncState
        {
            get { return null; }
        }

        public System.Threading.WaitHandle AsyncWaitHandle
        {
            get { return null; }
        }

        public bool CompletedSynchronously
        {
            get { return false; }
        }

        public bool IsCompleted
        {
            get { return _IsCompleted; }
        }

        #endregion
    }

    public class SigleMessage
    {
        List<warpIAsyncResult> clients = null;
        private SigleMessage()
        {
            clients = new List<warpIAsyncResult>();
        }

        public static SigleMessage Instance
        {
            get
            {
                return innerClass._instance;
            }
        }

        public void SendMessage(string msg, warpIAsyncResult warp,string userName)
        {
            if (msg == "connectserver!@#")  //添加客户端到服务端
            {
                clients.Add(warp);
            }
            else  //否则循环客户端，发送数据
            {
                foreach (var item in clients)
                {
                //    if (item.userName ==userName) //判断用户名相同
                 //   { 
                        item.Send(string.Format("{0:yyyy-MM-dd HH:mm:ss}<br/>{1}", DateTime.Now, msg));
                //    } 
                }
                clients.Clear();
            }
        }


        class innerClass
        {
            static innerClass()
            {

            }
            public readonly static SigleMessage _instance = new SigleMessage();
        }
    }

}
