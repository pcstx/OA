using System;
using System.IO;
using System.Text;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text.RegularExpressions;
using System.Xml;
using System.Collections.Specialized;
using GPRP.GPRPComponents;
using GPRP.GPRPBussiness;
using GPRP.Entity;
using MyADO;

namespace GPRP.Web.UI
{
	/// <summary>
	/// 页面基类
	/// </summary>
	public class BasePage : System.Web.UI.Page
	{
        protected internal string strButtonHideScript;
		/// <summary>
		/// 站点配置信息
		/// </summary>
        protected internal GeneralConfigInfo config;
        /// <summary>
        /// 在线用户信息
        /// </summary>
        protected internal UserListEntity userEntity;
	    /// <summary>
		/// 当前用户的用户名
		/// </summary>
		protected internal string username;
		/// <summary>
		/// 当前用户的密码
		/// </summary>
		protected internal string password;
		/// <summary>
		/// 当前用户的用户ID
		/// </summary>
		protected internal string userid;
		/// <summary>
		/// 当前日期
		/// </summary>
		protected internal string nowdate;
		/// <summary>
		/// 当前时间
		/// </summary>
		protected internal string nowtime;
		/// <summary>
		/// 当前日期时间
		/// </summary>
		protected internal string nowdatetime;
		/// <summary>
		/// 当前页面Meta字段内容
		/// </summary>
		protected internal string meta = "";
		/// <summary>
		/// 当前页面Link字段内容
		/// </summary>
		protected internal string link;
		/// <summary>
		/// 当前页面中增加script
		/// </summary>
		protected internal string script;
		/// <summary>
		/// 当前页面检查到的错误数
		/// </summary>
		protected internal int page_err = 0;
		/// <summary>
		/// 提示文字
		/// </summary>
		protected internal string msgbox_text = "";
		/// <summary>
		/// 是否显示回退的链接
		/// </summary>
		protected internal string msgbox_showbacklink = "true";
		/// <summary>
		/// 回退链接的内容
		/// </summary>
		protected internal string msgbox_backlink= "javascript:history.back();";
		/// <summary>
		/// 返回到的页面url地址
		/// </summary>
		protected internal string msgbox_url = "";
		/// <summary>
		/// 页面内容
		/// </summary>
		protected internal  System.Text.StringBuilder templateBuilder = new System.Text.StringBuilder();
		/// <summary>
		/// 是否为需检测校验码页面
		/// </summary>
		protected bool isseccode = true;

		/// <summary>
		/// 当前页面开始载入时间(毫秒)
		/// </summary>
        private DateTime m_starttickDateTime ;

		/// <summary>
		/// 当前页面执行时间(毫秒)
		/// </summary>
        private double m_processtime;
        /// <summary>
        /// 当前页面开始载入时间(毫秒)
        /// </summary>
        public float m_starttick = Environment.TickCount;

		/// <summary>
		/// 当前页面名称
		/// </summary>
		public string pagename = DNTRequest.GetPageName();
     
        /// <summary>
        /// 查询次数统计
        /// </summary>
        public int querycount = 0;
   
        public string footer = "<div align=\"center\" style=\" padding-top:60px;font-size:11px; font-family: Arial;\"><hr style=\"height:1px; width:600; color:#CCCCCC; background:#CCCCCC; border: 0; \"/>Powered by <a style=\"COLOR: #000000\" href=\"http://www.geobyev.com\" target=\"_blank\">" + Utils.GetAssemblyProductName() + "</a> &nbsp;&copy; 2007-" + Utils.GetAssemblyCopyright().Split(',')[0] + ", <a style=\"COLOR: #000000;font-weight:bold\" href=\"http://www.geobyev.com\" target=\"_blank\">Geobyev Co.,Ltd.</a></div>";

        public string menuID = "0";
        protected DataTable dtUserRight = new DataTable();
        
		/// <summary>
		/// BasePage类构造函数
		/// </summary>
		public BasePage()
		{
            strButtonHideScript = "<script type=\"text/javascript\"  lanuage=\"javascript\"> \r\n" +
                        "buttonHidesuccess(); \r\n" +
                       "</script> \r\n";
            config = GeneralConfigs.GetConfig();
            //先看是否正常登陆
            userid=WebUtils.GetCookieUser();
            password = WebUtils.GetCookiePassword(config.Passwordkey);
            if (userid == "" || password == "")
            {
                Context.Response.Redirect("/login.aspx",true);
                return;
            }
            if (!Page.IsPostBack)
            {
                this.RegisterAdminPageClientScriptBlock();
            }
            if (userEntity == null)
            {
                userEntity = DbHelper.GetInstance().GetUserListEntityByUserID(userid);
            }

			if (config.Closed == 1 && pagename != "login.aspx" && pagename != "logout.aspx" && pagename != "register.aspx" )
			{
				ShowMessage(1);
				return;
			}

			nowdate = Utils.GetDate();
			nowtime = Utils.GetTime();
			nowdatetime = Utils.GetDateTime();

			link = "";
			script = "";
			isseccode =	Utils.InArray(pagename, config.Seccodestatus);

			if ((isseccode))
			{
				if (DNTRequest.GetString("vcode") == "")
				{
					if (pagename == "showforum.aspx")
					{
					}
					else if (pagename.EndsWith("ajax.aspx"))
					{
                        if (DNTRequest.GetString("t") == "quickreply")
					    {
    					    ResponseAjaxVcodeError();
					    }
					}
					else
					{
						AddErrLine("验证码错误");
					}
				}
				else
				{
				}
			}

		
		    m_starttickDateTime = DateTime.Now;
            

			ShowPage();
     
            m_processtime = DateTime.Now.Subtract(m_starttickDateTime).TotalMilliseconds / 1000;
		}

		
		
		
		#region 子方法



 

 


		/// <summary>
		/// 输出Ajax验证码错误信息


		/// </summary>
		private static void ResponseAjaxVcodeError()
		{
            System.Web.HttpContext.Current.Response.Clear();
            System.Web.HttpContext.Current.Response.ContentType = "Text/XML";
			System.Web.HttpContext.Current.Response.Expires = 0;
			
			System.Web.HttpContext.Current.Response.Cache.SetNoStore();			
			System.Text.StringBuilder xmlnode = new System.Text.StringBuilder();
			xmlnode.Append("<?xml version=\"1.0\" encoding=\"utf-8\"?>\r\n");
			xmlnode.Append("<error>验证码错误</error>");
			System.Web.HttpContext.Current.Response.Write(xmlnode.ToString());
			System.Web.HttpContext.Current.Response.End();
		}

		

		/// <summary>
		/// 设置页面定时转向
		/// </summary>
		public void SetMetaRefresh()
		{
			SetMetaRefresh(2, msgbox_url);
		}
		
		/// <summary>
		/// 设置页面定时转向
		/// </summary>
		/// <param name="sec">时间(秒)</param>
		public void SetMetaRefresh(int sec)
		{
			SetMetaRefresh(sec, msgbox_url);
		}
		
		/// <summary>
		/// 设置页面定时转向
		/// </summary>
		/// <param name="sec">时间(秒)</param>
		/// <param name="url">转向地址</param>
		public void SetMetaRefresh(int sec, string url)
		{

			meta = meta + "\r\n<meta http-equiv=\"refresh\" content=\"" + sec.ToString() + "; url=" + url + "\" />";
			//AddScript("window.setInterval('location.replace(\"" + url + "\");'," + (sec*1000).ToString() + ");");

		}

		/// <summary>
		/// 插入指定路径的CSS
		/// </summary>
		/// <param name="url">CSS路径</param>
		public void AddLinkCss(string url)
		{
			link = link + "\r\n<link href=\"" + url + "\" rel=\"stylesheet\" type=\"text/css\" />";
		}

		public void AddLinkRss(string url,string title)
		{
			link = link + "\r\n<link rel=\"alternate\" type=\"application/rss+xml\" title=\"" + title + "\" href=\"" + url + "\" />";

		}

		
		/// <summary>
		/// 插入指定路径的CSS
		/// </summary>
		/// <param name="url">CSS路径</param>
		public void AddLinkCss(string url,string linkid)
		{
			link = link + "\r\n<link href=\"" + url + "\" rel=\"stylesheet\" type=\"text/css\" id=\"" + linkid + "\" />";
		}
		
		
		/// <summary>
		/// 插入脚本内容到页面head中


		/// </summary>
		/// <param name="scriptstr">不包括<script></script>的脚本主体字符串</param>
		public void AddScript(string scriptstr)
		{
			AddScript(scriptstr, "javascript");
		}

		/// <summary>
		/// 插入脚本内容到页面head中


		/// </summary>
		/// <param name="scriptstr">不包括<script>
		/// <param name="scripttype">脚本类型(值为：vbscript或javascript,默认为javascript)</param>
		public void AddScript(string scriptstr,string scripttype)
		{
			if (!scripttype.ToLower().Equals("vbscript") && !scripttype.ToLower().Equals("vbscript"))
			{
				scripttype = "javascript";
			}
			script = script + "\r\n<script type=\"text/" + scripttype + "\">" + scriptstr + "</script>";
		}		
		
		/// <summary>
		/// 插入指定Meta
		/// </summary>
		/// <param name="metastr">Meta项</param>
		public void AddMeta(string metastr)
		{
			meta = meta + "\r\n<meta " + metastr + " />";
		}

        /// <summary>
        /// 更新页面Meta
        /// </summary>
        /// <param name="Seokeywords">关键词</param>
        /// <param name="Seodescription">说明</param>
        /// <param name="Seohead">其它增加项</param>
        public void UpdateMetaInfo(string Seokeywords, string Seodescription, string Seohead)
        {
            string[] metaArray = Utils.SplitString(meta, "\r\n");
            //设置为空,并在下面代码中进行重新赋值


            meta = "";
            foreach (string metaoption in metaArray)
            {
                //找出keywords关键字


                if (metaoption.ToLower().IndexOf("name=\"keywords\"") > 0)
                {
                    if (Seokeywords != null && Seokeywords.Trim() != "")
                    {
                        meta += "<meta name=\"keywords\" content=\"" + Utils.RemoveHtml(Seokeywords).Replace("\"", " ") + "\" />\r\n";
                        continue;
                    }
                }

                //找出description关键字


                if (metaoption.ToLower().IndexOf("name=\"description\"") > 0)
                {
                    if (Seodescription != null && Seodescription.Trim() != "")
                    {
                        meta += "<meta name=\"description\" content=\"" + Utils.RemoveHtml(Seodescription).Replace("\"", " ") + "\" />\r\n";
                        continue;
                    }
                }

                meta = meta + metaoption + "\r\n";
            }

            meta = meta + Seohead;
        }


        /// <summary>
        /// 添加页面Meta信息
        /// </summary>
        /// <param name="Seokeywords">关键词</param>
        /// <param name="Seodescription">说明</param>
        /// <param name="Seohead">其它增加项</param>
        public void AddMetaInfo(string Seokeywords, string Seodescription, string Seohead)
        {
            if (Seokeywords != "")
            {
                meta = meta + "<meta name=\"keywords\" content=\"" + Utils.RemoveHtml(Seokeywords).Replace("\"", " ") + "\" />\r\n";
            }
            if (Seodescription != "")
            {
                meta = meta + "<meta name=\"description\" content=\"" + Utils.RemoveHtml(Seodescription).Replace("\"", " ") + "\" />\r\n";
            }
            meta = meta + Seohead;
        }

		/// <summary>
		/// 增加错误提示
		/// </summary>
		/// <param name="errinfo">错误提示信息</param>
		public void AddErrLine(string errinfo)
		{
			if (msgbox_text.Length == 0)
			{
				msgbox_text = msgbox_text + errinfo;
			}
			else
			{
				msgbox_text = msgbox_text + "<br />" + errinfo;
			}
			page_err++;
		}

		/// <summary>
		/// 增加提示信息
		/// </summary>
		/// <param name="strinfo">提示信息</param>
		public void AddMsgLine(string strinfo)
		{
			if (msgbox_text.Length == 0)
			{
				msgbox_text = msgbox_text + strinfo;
			}
			else
			{
				msgbox_text = msgbox_text + "<br />" +strinfo;
			}
		}


		/// <summary>
		/// 是否已经发生错误
		/// </summary>
		/// <returns>有错误则返回true, 无错误则返回false</returns>
		public bool IsErr()
		{
			return page_err > 0;
		}

		/// <summary>
		/// 设置要转向的url
		/// </summary>
		/// <param name="strurl">要转向的url</param>
		public void SetUrl(string strurl)
		{
			msgbox_url = strurl;
		}
		/// <summary>
		/// 设置回退链接的内容


		/// </summary>
		/// <param name="strback">回退链接的内容</param>
		public void SetBackLink(string strback)
		{
			msgbox_backlink = strback;
		}

		/// <summary>
		/// 设置是否显示回退链接
		/// </summary>
		/// <param name="link">要显示则为true, 否则为false</param>
		public void SetShowBackLink(bool link)
		{
			if (link)
			{
				msgbox_showbacklink = "true";
			}
			else
			{
				msgbox_showbacklink = "false";
			}
		}

		public void ShowMessage(byte mode)
		{
			ShowMessage("", mode);
		}
		
		public void ShowMessage(string hint, byte mode)
		{
			System.Web.HttpContext.Current.Response.Clear();
			System.Web.HttpContext.Current.Response.Write("<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">\r\n<html xmlns=\"http://www.w3.org/1999/xhtml\">\r\n<head><title>");
			string title;
			string body;
			switch (mode)
			{
				case 1:
					title = "论坛已关闭";
					body = config.Closedreason;
					break;
				case 2:
					title = "禁止访问";
					body = hint;
					break;
				default:
					title = "提示";
					body = hint;
					break;
			}
			System.Web.HttpContext.Current.Response.Write(title);
			System.Web.HttpContext.Current.Response.Write(" - ");
			System.Web.HttpContext.Current.Response.Write(config.Forumtitle);
			System.Web.HttpContext.Current.Response.Write(" - Powered by Discuz!NT</title><meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\">");
			System.Web.HttpContext.Current.Response.Write(meta);
			System.Web.HttpContext.Current.Response.Write("<style type=\"text/css\"><!-- body { margin: 20px; font-family: Tahoma, Verdana; font-size: 14px; color: #333333; background-color: #FFFFFF; }a {color: #1F4881;text-decoration: none;}--></style></head><body><div style=\"border: #cccccc solid 1px; padding: 20px; width: 500px; margin:auto\" align=\"center\">");
			System.Web.HttpContext.Current.Response.Write(body);
			System.Web.HttpContext.Current.Response.Write("</div><br /><br /><br /><div style=\"border: 0px; padding: 0px; width: 500px; margin:auto\"><strong>当前服务器时间:</strong> ");
			System.Web.HttpContext.Current.Response.Write(Utils.GetDateTime());
			System.Web.HttpContext.Current.Response.Write("<br /><strong>当前页面</strong> ");
			System.Web.HttpContext.Current.Response.Write(pagename);
			System.Web.HttpContext.Current.Response.Write("<br /><strong>可选择操作:</strong> ");
            //if (userkey == "")
            //{
            //    System.Web.HttpContext.Current.Response.Write("<a href=\"login.aspx\">登录</a> | <a href=\"register.aspx\">注册</a>");
            //}
            //else
            //{
            //    System.Web.HttpContext.Current.Response.Write("<a href=\"logout.aspx?userkey=" + userkey + "\">退出</a>");
            //    if (useradminid == 1)
            //    {
            //        System.Web.HttpContext.Current.Response.Write(" | <a href=\"logout.aspx?userkey=" + userkey + "\">系统管理</a>");
            //    }
            //}
			System.Web.HttpContext.Current.Response.Write("</div></body></html>");
			System.Web.HttpContext.Current.Response.End();
		}

		/// <summary>
		/// 得到当前页面的载入时间供模板中调用(单位:毫秒)
		/// </summary>
		/// <returns>当前页面的载入时间</returns>
		public double Processtime
		{
			get {return m_processtime;}
		}

		#endregion

		/// <summary>
		/// 页面处理虚方法
		/// </summary>
		protected virtual void ShowPage()
		{
			return;
		}

		/// <summary>
		/// OnUnload事件处理
		/// </summary>
		/// <param name="e"></param>
		protected override void OnUnload(EventArgs e)
		{
			base.OnUnload (e);
		}

		/// <summary>
		/// 控件初始化时计算执行时间
		/// </summary>
		/// <param name="e"></param>
		protected override void OnInit(EventArgs e)
		{
            m_processtime = (Environment.TickCount - m_starttick) / 1000;
			base.OnInit (e);
        }

       

        #region aspxrewrite 配置

        protected string aspxrewriteurl = "";


        /// <summary>
        /// 设置关于页面链接的显示样式        /// </summary>
        /// <param name="forumid"></param>
        /// <param name="pageid"></param>
        /// <returns></returns>
        protected string ShowForumAspxRewrite(string forumid, int pageid)
        {
            return ShowForumAspxRewrite(Utils.StrToInt(forumid, 0),
                                        pageid <= 0 ? 0 : pageid);
        }

    
        protected string ShowForumAspxRewrite(int forumid, int pageid)
        {
            //当使用伪aspx
            if (config.Aspxrewrite == 1)
            {
                if (pageid > 0)
                {
                    return "showforum-" + forumid + "-" + pageid + config.Extname;
                }
                else
                {
                    return "showforum-" + forumid + config.Extname;
                }
            }
            else
            {
                if (pageid > 0)
                {
                    return "showforum.aspx?forumid=" + forumid + "&page=" + pageid;
                }
                else
                {
                    return "showforum.aspx?forumid=" + forumid;
                }
            }
           
        }

        /// <summary>
        /// 设置关于页面链接的显示样式        /// </summary>
        /// <param name="forumid"></param>
        /// <param name="pageid"></param>
        /// <returns></returns>
        protected string ShowTopicAspxRewrite(string topicid, int pageid)
        {
            return ShowTopicAspxRewrite(Utils.StrToInt(topicid, 0),
                                        pageid <= 0 ? 0 : pageid);
        }

        protected string ShowTopicAspxRewrite(int topicid, int pageid)
        {
            //当使用伪aspx
            if (config.Aspxrewrite == 1)
            {
                if (pageid > 0)
                {
                    return "showtopic-" + topicid + "-" + pageid + config.Extname;
                }
                else
                {
                    return "showtopic-" + topicid + config.Extname;
                }
            }
            else
            {
                if (pageid > 0)
                {
                    return "showtopic.aspx?topicid=" + topicid + "&page=" + pageid;
                }
                else
                {
                    return "showtopic.aspx?topicid=" + topicid;
                }
            }
        }

       


        protected string UserInfoAspxRewrite(int userid)
        {
            //当使用伪aspx
            if (config.Aspxrewrite == 1)
            {
                return "userinfo-" + userid + config.Extname;
            }
            else
            {
                return "userinfo.aspx?userid=" + userid;
            }
            
        }

        /// <summary>
        /// 设置关于userinfo页面链接的显示样式        /// </summary>
        /// <param name="forumid"></param>
        /// <param name="pageid"></param>
        /// <returns></returns>
        protected string UserInfoAspxRewrite(string userid)
        {
            return UserInfoAspxRewrite(Utils.StrToInt(userid, 0));
        }


        protected string RssAspxRewrite(int forumid)
        {
            //当使用伪aspx
            if (config.Aspxrewrite == 1)
            {
                return "rss-" + forumid + config.Extname;
            }
            else
            {
                return "rss.aspx?forumid=" + forumid;
            }

        }

        /// <summary>
        /// 设置关于userinfo页面链接的显示样式        /// </summary>
        /// <param name="forumid"></param>
        /// <param name="pageid"></param>
        /// <returns></returns>
        protected string RssAspxRewrite(string forumid)
        {
            return RssAspxRewrite(Utils.StrToInt(forumid, 0));
        }

        #endregion

        public void RegisterAdminPageClientScriptBlock()
        {
            string script = "<div id=\"success\" style=\"position:absolute;z-index:300;height:120px;width:284px;left:50%;top:50%;margin-left:-150px;margin-top:-80px;\">\r\n" +
                "	<div id=\"Layer2\" style=\"position:absolute;z-index:300;width:270px;height:90px;background-color: #FFFFFF;border:solid #000000 1px;font-size:14px;\">\r\n" +
                "		<div id=\"Layer4\" style=\"height:26px;background:#f1f1f1;line-height:26px;padding:0px 3px 0px 3px;font-weight:bolder;\">操作提示</div>\r\n" +
                "		<div id=\"Layer5\" style=\"height:64px;line-height:150%;padding:0px 3px 0px 3px;\" align=\"center\"><BR /><table><tr><td valign=top><img border=\"0\" src=\"../../../images/ajax_loading.gif\"  /></td><td valign=middle style=\"font-size: 14px;\" >正在执行当前操作, 请稍等...<BR /></td></tr></table><BR /></div>\r\n" +
                "	</div>\r\n" +
                "	<div id=\"Layer3\" style=\"position:absolute;width:270px;height:90px;z-index:299;left:4px;top:5px;background-color: #E8E8E8;\"></div>\r\n" +
                "</div>\r\n" +
                "<script  type=\"text/javascript\"> \r\n" +
                "document.getElementById('success').style.display = \"none\"; \r\n" +
                "function buttonHidesuccess() {\r\n " +
              "document.getElementById('success').style.display = \"none\"; \r\n" +
                "}\r\n" +
                "</script> \r\n" +
                "<script language=\"JavaScript1.2\" src=\"../JScript/divcover.js\"></script>\r\n";

#if NET1			
			RegisterClientScriptBlock("Page", script);
#else
            ClientScript.RegisterClientScriptBlock(this.GetType(), "Page", script);
#endif
        }
        public void ButtonHideScript(Control mcontrol)
        {
            string script = "<script type=\"text/javascript\"  lanuage=\"javascript\"> \r\n" +
                     "buttonHidesuccess(); \r\n" +
                    "</script> \r\n";
#if NET1			
			RegisterClientScriptBlock("Page", script);
#else
           //Page.ClientScript.RegisterStartupScript(this.GetType(), "ButtonHideScript", script);
            System.Web.UI.ScriptManager.RegisterStartupScript(mcontrol, this.GetType(), "ButtonHideScript", script, false);
          

#endif
        }
#if NET1
		public override void RegisterStartupScript(string key, string scriptstr)
#else
        public new void RegisterStartupScript(string key, string scriptstr)
#endif
        {
            if ((key == "PAGETemplate") || (key == "PAGE"))
            {
                string script = "";

                if (key == "PAGE")
                {
                    script = "<script> \r\n" +
                        "var bar=0;\r\n" +
                        "document.getElementById('success').style.display = \"block\";  \r\n" +
                        "document.getElementById('Layer5').innerHTML ='<BR>操作成功执行<BR>';  \r\n" +
                        "count() ; \r\n" +
                        "function count(){ \r\n" +
                        "bar=bar+4; \r\n" +
                        "if (bar<99) \r\n" +
                        "{setTimeout(\"count()\",100);} \r\n" +
                        "else { \r\n" +
                        "document.getElementById('success').style.display = \"none\"; \r\n" +//HideOverSels('success');
                        scriptstr + "} \r\n" +
                        "} \r\n" +
                        "</script> \r\n" +
                        "<script> window.onload = function(){};</script>\r\n";//HideOverSels('success')
                }

                if (key == "PAGETemplate")
                {
                    script = "<script> \r\n" +
                        "var bar=0;\r\n success.style.display = \"block\";  \r\n" +
                        "document.getElementById('Layer5').innerHTML = '<BR>" + scriptstr + "<BR>';  \r\n" +
                        "count() ; \r\n" +
                        "function count(){ \r\n" +
                        "bar=bar+4; \r\n" +
                        "if (bar<99) \r\n" +
                        "{setTimeout(\"count()\",100);} \r\n" +
                        "else { \r\n" +
                        "document.getElementById('success').style.display = \"none\"; \r\n" +//HideOverSels('success');
                        "}} \r\n" +
                        "</script> \r\n" +
                        "<script> window.onload = function(){};</script>\r\n";//HideOverSels('success')
                }
#if NET1
				base.RegisterStartupScript(key, script);
#else
                ClientScript.RegisterStartupScript(this.GetType(), key, script);
#endif

            }
            else
            {
#if NET1
				base.RegisterStartupScript(key, scriptstr);
#else
                ClientScript.RegisterStartupScript(this.GetType(), key, scriptstr);
#endif
            }
        }


        public void CallBaseRegisterStartupScript(string key, string scriptstr)
        {
#if NET1
			base.RegisterStartupScript(key, scriptstr);
#else
            ClientScript.RegisterStartupScript(this.GetType(), key, scriptstr);
#endif
        }

        /// <summary>
        /// 检查cookie是否有效
        /// </summary>
        /// <returns></returns>
        public bool CheckCookie()
        {
            config = GeneralConfigs.GetConfig();

            // 如果IP访问列表有设置则进行判断
            if (config.Adminipaccess.Trim() != "")
            {
                string[] regctrl = Utils.SplitString(config.Adminipaccess, "\n");
                if (!Utils.InIPArray(DNTRequest.GetIP(), regctrl))
                {
                    Context.Response.Redirect(BaseConfigs.GetForumPath + "aspx/login.aspx");
                    return false;
                }
            }
            return true;
        }
        /// <summary>
        /// 论坛提示信息
        /// </summary>
        /// <returns></returns>
        protected string GetShowMessage()
        {
            string message = "<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">";
            message += "<html xmlns=\"http://www.w3.org/1999/xhtml\"><head><title>您没有权限运行当前程序!</title><meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\">";
            message += "<link href=\"../styles/default.css\" type=\"text/css\" rel=\"stylesheet\"></head><body><br><br><div style=\"width:100%\" align=\"center\">";
            message += "<div align=\"center\" style=\"width:660px; border:1px dotted #FF6600; background-color:#FFFCEC; margin:auto; padding:20px;\"><img src=\"../images/hint.gif\" border=\"0\" alt=\"提示:\" align=\"absmiddle\" width=\"11\" height=\"13\" /> &nbsp;";
            message += "您没有权限运行当前程序,请您以论坛创始人身份登陆后台进行操作!</div></div></body></html>";
            return message;
        }
        public void LoadRegisterStartupScript(string key, string scriptstr)
        {
            string message = "程序执行中... <BR /> 当前操作可能要运行一段时间.<BR />您可在此期间进行其它操作<BR /><BR />";

            string script = "<script> \r\n" +
                "var bar=0;\r\n success.style.display = \"block\";  \r\n" +
                "document.getElementById('Layer5').innerHTML ='" + message + "';  \r\n" +
                "count() ; \r\n" +
                "function count(){ \r\n" +
                "bar=bar+2; \r\n" +
                "if (bar<99) \r\n" +
                "{setTimeout(\"count()\",100);} \r\n" +
                "else { \r\n" +
                "	document.getElementById('success').style.display = \"none\"; \r\n" +//HideOverSels('success');
                scriptstr + "} \r\n" +
                "} \r\n" +
                "</script> \r\n" +
                "<script> window.onload = function(){};</script>\r\n";//HideOverSels('success')

            CallBaseRegisterStartupScript(key, script);

        }

        private bool saveState = false;

        private bool IsRestore
        {
            get
            {
                if (Request.QueryString["IsRestore"] != null && Request.QueryString["IsRestore"] == "1" && Request.Form["__VIEWSTATE"] == null)
                    return true;
                else
                    return false;
            }
        }

        private string RestoreKey
        {
            get { return Request.QueryString["key"]; }
        }

        public bool SavePageState
        {
            get { return this.saveState; }
            set { this.saveState = value; }
        }

        private NameValueCollection postData = null;

        private NameValueCollection PostData
        {
            get
            {
                if (this.IsRestore)
                    return this.postData;
                return Request.Form;
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            if (this.IsRestore)
            {
                ArrayList modifiedControls = new ArrayList();
                foreach (string key in PostData.AllKeys)
                {
                    System.Web.UI.Control control = FindControl(key);
                    if (control is IPostBackDataHandler)
                        if (((IPostBackDataHandler)control).LoadPostData(key, PostData))
                            modifiedControls.Add(control);
                }
                // 发生 PostDataChanged 事件在所有已变动的控件上:
                foreach (IPostBackDataHandler control in modifiedControls)
                    control.RaisePostDataChangedEvent();
            }
            base.OnLoad(e);
        }

        private enum FavoriteStatus { Show, Hidden, Full, Exist }

        private FavoriteStatus GetFavoriteStatus()
        {
            return FavoriteStatus.Show; //正常收藏
        }

        protected void CreateExcel(DataTable dtsysTableColumn, DataSet ds, string typeid, string FileName, string IsNumber)
        {
            DataTable dtColumn = dtsysTableColumn;//.Select("","ColShowOrder Asc");
            System.Web.HttpResponse resp;

            ////resp = Page.Response;
            ////resp=System.Web.UI.Page.
            resp = Context.Response;
            resp.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");

            resp.AppendHeader("Content-Disposition", "attachment;filename=" + FileName);

            resp.ContentType = "application/ms-excel";

            StringBuilder sb = new StringBuilder();
            //data = ds.DataSetName + "\n"; 
            int count = 0;

            foreach (DataTable tb in ds.Tables)
            {
                //data += tb.TableName + "\n"; 
                sb.AppendLine("<meta http-equiv=\"Content-Type\" content=\"text/html; charset=gb2312\">");
                sb.AppendLine("<table cellspacing=\"0\" cellpadding=\"5\" rules=\"all\" border=\"1\">");
                //写出列名 
                //sb.AppendLine("<table>");
                //sb.AppendLine("<tr style=\"font-weight: bold; white-space: nowrap;\">");
                sb.AppendLine("<tr>");
                foreach (DataRow drColumn in dtColumn.Rows)
                {
                    if (drColumn["ColIsShow"].ToString() == "1")
                    {
                        foreach (DataColumn column in tb.Columns)
                        {
                            if (drColumn["ColName"].ToString().ToUpper() == column.ColumnName.ToUpper())
                            {
                                sb.AppendLine("<td>" + drColumn["ColDescriptionCN"].ToString() + "</td>");
                            }
                        }
                    }
                }

                sb.AppendLine("</tr>");

                //写出数据 
                foreach (DataRow row in tb.Rows)
                {
                    sb.Append("<tr>");
                    foreach (DataRow drColumn in dtColumn.Rows)
                    {
                        if (drColumn["ColIsShow"].ToString() == "1")
                        {
                            foreach (DataColumn column in tb.Columns)
                            {
                                if (drColumn["ColName"].ToString().ToUpper() == column.ColumnName.ToUpper())
                                {
                                    if (drColumn["TypeName"].ToString() == "smalldatetime" || drColumn["TypeName"].ToString() == "datetime")
                                    {
                                        string formatDate = "";
                                        if (row[column].ToString() != "")
                                        {
                                            formatDate = Convert.ToDateTime(row[column]).ToShortDateString();
                                            //截取
                                        }
                                        sb.Append("<td>" + formatDate + "</td>");
                                    }
                                    else
                                    {
                                        if (IsNumber == "True" && (drColumn["TypeName"].ToString() == "int" || drColumn["TypeName"].ToString() == "float"))
                                            sb.Append("<td>" + row[column].ToString() + "</td>");
                                        else
                                            sb.Append("<td style=\"vnd.ms-excel.numberformat:@\">" + row[column].ToString() + "</td>");
                                    }
                                }
                            }
                        }
                    }

                    sb.AppendLine("</tr>");
                    count++;
                }
                sb.AppendLine("</table>");
            }

            //resp.Write(sb.ToString());
            //resp.End();

        }

        #region 调用自定义的方法对VIEWSTATE进行操作

        protected void DiscuzForumSavePageState(object viewState)
        {
            string keyid = userid + "_" + this.GetType().Name.Trim();
            DiscuzControlContainer dcc = DiscuzControlContainer.GetContainer();
            dcc.AddNormalComponent(keyid, viewState);
        }

        protected object DiscuzForumLoadPageState()
        {
            string keyid = userid + "_" + this.GetType().Name.Trim();
            DiscuzControlContainer dcc = DiscuzControlContainer.GetContainer();
            object viewState = (object)dcc.GetNormalComponentDataObject(keyid);
            dcc.RemoveComponentByName(keyid);
            if (viewState != null) return viewState;
            else return null;
        }

        #endregion

        #region 把VIEWSTATE以组件的形式存入容器

        protected override void SavePageStateToPersistenceMedium(object viewState)
        {
            base.SavePageStateToPersistenceMedium(viewState);
        }

        protected override object LoadPageStateFromPersistenceMedium()
        {
            object o = new object();
            try
            {
                o = base.LoadPageStateFromPersistenceMedium();
            }
            catch
            {
                o = null;
            }
            return o;
        }

        #endregion
    }
}
