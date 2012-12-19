using System;
using System.ComponentModel;
using System.Text;
using System.Web.UI;
using GPRP.GPRPComponents;

namespace GPRP.GPRPControls
{
    /// <summary>
    /// 控钮控件。    /// </summary>
    [DefaultEvent("Click"), DefaultProperty("Text"), ToolboxData("<{0}:Button runat=server></{0}:Button>")]
    public class Button : WebControl, IPostBackEventHandler
    {
        /// <summary>
        /// 单击事件绑定的对象        /// </summary>
        protected static readonly object EventClick = new object();


        static Button()
        {
            EventClick = new object();
        }

        /// <summary>
        /// 构造函数        /// </summary>
        public Button()
        {
            this.ButtontypeMode = ButtonType.Normal;
        }

        /// <summary>
        /// 定义事件处理器        /// </summary>
        public event EventHandler Click
        {
            add
            {
                Events.AddHandler(EventClick, value);
            }
            remove
            {
                Events.RemoveHandler(EventClick, value);
            }
        }

      
        /// <summary>
        /// 跨页面提交链接        /// </summary>
        public virtual string PostBackUrl
        {
            get
            {
                string text1 = (string)this.ViewState["PostBackUrl"];
                if (text1 != null)
                {
                    return text1;
                }
                return string.Empty;
            }
            set
            {
                this.ViewState["PostBackUrl"] = value;
            }
        }

        /// <summary>
        /// 定义Onclick
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnClick(EventArgs e)
        {
            EventHandler clickHandler = (EventHandler)Events[EventClick];
            if (clickHandler != null)
            {
                clickHandler(this, e);
            }
        }

        /// <summary>
        /// 引发PostBack事件
        /// </summary>
        /// <param name="eventArgument"></param>
        public void RaisePostBackEvent(string eventArgument)
        {
            OnClick(new EventArgs());
        }

        /// <summary>
        /// 引发PostBack事件(实现IPostBackEventHandler接口)
        /// </summary>
        /// <param name="eventArgument"></param>
        void IPostBackEventHandler.RaisePostBackEvent(string eventArgument)
        {
            this.RaisePostBackEvent(eventArgument);
        }

        /// <summary>
        /// 重写OnPreRender
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
        }

        /// <summary>
        /// 加载ViewState
        /// </summary>
        /// <param name="savedState"></param>
        protected override void LoadViewState(object savedState)
        {
            if (savedState != null)
            {
                base.LoadViewState(savedState);
                string text1 = (string)this.ViewState["Text"];
                if (text1 != null)
                {
                    this.Text = text1;
                }
            }
        }

        /// <summary>
        /// 添加子对象        /// </summary>
        /// <param name="obj"></param>
        protected override void AddParsedSubObject(object obj)
        {
            if (this.HasControls())
            {
                base.AddParsedSubObject(obj);
            }
            else if (obj is LiteralControl)
            {
                this.Text = ((LiteralControl)obj).Text;
            }
            else
            {
                string text1 = this.Text;
                if (text1.Length != 0)
                {
                    this.Text = string.Empty;
                    base.AddParsedSubObject(new LiteralControl(text1));
                }
                base.AddParsedSubObject(obj);
            }
        }

        /// <summary>
        /// 输出html,在浏览器中显示控件        /// </summary>
        /// <param name="output"></param>
        protected override void Render(HtmlTextWriter output)
        {
            if (this.HintInfo != "")
            {
                output.WriteBeginTag("span id=\"" + this.ClientID + "\"  onmouseover=\"showhintinfo(this," + this.HintLeftOffSet + "," + this.HintTopOffSet + ",'" + this.HintTitle + "','" + this.HintInfo + "','" + this.HintHeight + "','" + this.HintShowType + "');\" onmouseout=\"hidehintinfo();\">");
            }
            if (this.IfShowDivOfButton)
            {
                output.WriteBeginTag("span id=\"" + this.ClientID + "\"  onmouseover=\"showhintinfo_btn(this," + this.HintLeftOffSet + "," + this.HintTopOffSet + ",'" + this.HintHeight + "','" + this.HintShowType + "');\" onmouseout=\"hidehintinfo_btn();\">");
          
            }
            if (this.Width.IsEmpty == true)
            { this.Width = 100; }
            
            
            if (this.AutoPostBack)
            {
                StringBuilder sb = new System.Text.StringBuilder();



                if (_Page_ClientValidate) sb.Append("if (typeof(Page_ClientValidate) == 'function') { if (Page_ClientValidate() == false) { return false; }");
                if (ScriptContent != "")
                {
                   sb.Append("if (" + ScriptContent +"==false) {return false;}");
                    //sb.Append(  ScriptContent );
                }
                if(_Page_ClientValidate) sb.Append("}");    //保证验证函数的执行
                //sb.Append("javascript:{if (typeof(Page_ClientValidate)!='function'|| Page_ClientValidate())__doPostBack('"+this.UniqueID+"','')}"); 
                //sb.Append("if(window.confirm('are you sure?')==false) return false;");        //自定义客户端脚本
               
               if (this.Disable) sb.Append("this.disabled=true;");
                if (ShowPostDiv)
                {
                    sb.Append("document.getElementById('success').style.display = 'block';");//HideOverSels('success');
                }

                if (ValidateForm)
                {
                    sb.Append("if(validate(this.form)){");
#if NET1
					sb.Append(Page.GetPostBackEventReference(this,"") + ";}");    //用__doPostBack来提交，保证按钮的服务器端click事件执行
#else
                    sb.Append(Page.ClientScript.GetPostBackEventReference(this, "") + ";}");    //用__doPostBack来提交，保证按钮的服务器端click事件执行
#endif
                }        // disable所有自身
                else
                {
#if NET1
					sb.Append(Page.GetPostBackEventReference(this,"") + ";");    //用__doPostBack来提交，保证按钮的服务器端click事件执行
#else
                    sb.Append(Page.ClientScript.GetPostBackEventReference(this, "") + ";");    //用__doPostBack来提交，保证按钮的服务器端click事件执行
#endif
                }




                if (this.ButtontypeMode == ButtonType.Normal)
                {
                    output.Write("<span><button type=\"button\" style=\"width:" + this.Width.Value.ToString() + "px;\" class=\"" + this.CSS + "\" id=\"" + this.UniqueID + "\" onclick=\"" + sb.ToString() + "\">" + this.Text + "</button></span>");
                }

                if (this.ButtontypeMode == ButtonType.WithImage)
                {
                    output.Write("<span><button type=\"button\"  style=\"width:" + this.Width.Value.ToString() + "px;\" class=\"" + this.CSS + "\" id=\"" + this.UniqueID + "\" onclick=\"" + sb.ToString() + "\"><img src=\"" + this.ButtonImgUrl + "\"/>" + this.Text + "</button></span>");
                }
               
            }
            else
            {
                if (this.ButtontypeMode == ButtonType.Normal)
                {

                    output.Write("<span><button type=\"button\" style=\"width:" + this.Width.Value.ToString() + "px;\"  class=\"" + this.CSS + "\" id=\"" + this.UniqueID + "\" onclick=\"" + ScriptContent + "\">" + this.Text + "</button></span>");
                }

                if (this.ButtontypeMode == ButtonType.WithImage)
                {
                    output.Write("<span><button type=\"button\"  style=\"width:" + this.Width.Value.ToString() + "px;\" class=\"" + this.CSS + "\" id=\"" + this.UniqueID + "\" onclick=\"" + ScriptContent + "\"><img src=\"" + this.ButtonImgUrl + "\"/>" + this.Text + "</button></span>");
                }
            }


            if (this.HintInfo != "")
            {
                output.WriteEndTag("span");
            }
            if (this.IfShowDivOfButton)
            {
                output.WriteEndTag("span");
            }
        }

   
        private bool _autoPostBack = true;

        /// <summary>
        /// AutoPostBack属性        /// </summary>
        public bool AutoPostBack
        {
            set
            {
                this._autoPostBack = value;
            }
            get
            {
                return this._autoPostBack;
            }
        }
        private string _css = "ManagerButton";
        /// <summary>
        /// 所需要CSS，默认为 ManagerButton
        /// </summary>
        public string CSS
        {
            get { return _css; }
            set { _css=value; }
        }
        private bool _disable = true;
        /// <summary>
        /// 是否要disable
        /// </summary>
        public bool Disable
        {
            get { return _disable; }
            set { _disable = value; }
        
        }
        private bool _showPostDiv = true;
        /// <summary>
        /// 是否显示"正在提交"信息
        /// </summary>
        public bool ShowPostDiv
        {
            set
            {
                this._showPostDiv = value;
            }
            get
            {
                return this._showPostDiv;
            }
        }
        private bool _Page_ClientValidate = false;
        /// <summary>
        /// 是否要进行服务器控件的验证        /// </summary>
        public bool Page_ClientValidate
        {
            set { _Page_ClientValidate = value; }
            get { return this._Page_ClientValidate;}
        }
        private bool mIfShowDivOfButton=false;
        /// <summary>
        /// 当导出按钮上才会用到，鼠标移上去，会下拉一下框，上面有想要层
        /// HintInfo 不能共存，一个为空，一个为真，一个不为空，一个为假
        /// 或者同时为空和假        /// </summary>
        public bool IfShowDivOfButton
        {
            set { mIfShowDivOfButton = value; }
            get { return this.mIfShowDivOfButton; }
        }

        #region 定义是否调用js函数validate(this.form);进行数据校验

        private bool _validateForm = false;

        /// <summary>
        /// 定义是否调用js函数validate(this.form);进行数据校验
        /// </summary>
        public bool ValidateForm
        {
            set
            {
                this._validateForm = value;
            }
            get
            {
                return this._validateForm;
            }
        }
        #endregion

        #region properytyButtontypeMode 按钮样式
        
        /// <summary>
        /// 按钮枚举样式(Normal:普通, WithImage:带图)
        /// </summary>
        public enum ButtonType
        {
            Normal,   //普通
            WithImage,  //带图
        }

        /// <summary>
        /// 按钮样式
        /// </summary>
        public ButtonType ButtontypeMode
        {
            get
            {
                object obj = ViewState["ButtontypeMode"];
                return obj == null ? ButtonType.WithImage : (ButtonType)obj;
            }
            set
            {
                ViewState["ButtontypeMode"] = value;
            }
        }
        #endregion


        #region Property Text 按钮文字

        /// <summary>
        /// 按钮文字 " 提 交 "
        /// </summary>

        [Bindable(true), Category("Appearance"), DefaultValue(" Default  提 交  ")]
        public string Text
        {
            get
            {
                object obj = ViewState["ButtonText"];
                return obj == null ? ResourceManager.GetString("Button_DafaultSubmit") : (string)obj;
            }
            set
            {
                ViewState["ButtonText"] = ResourceManager.GetString(value);
            }
        }
        #endregion


        #region Property ButtonImgUrl 图版按钮链接

        /// <summary>
        /// 图片按钮链接
        /// </summary>
        [Description("图版按钮链接"), DefaultValue("../images/submit.gif")]
        public string ButtonImgUrl
        {
            get
            {
                object obj = ViewState["ButtonImgUrl"];
                return obj == null ? "../images/submit.gif" : (string)obj;
            }
            set
            {
                ViewState["ButtonImgUrl"] = value;
            }
        }

        #endregion


        #region Property XpBGImgFilePath XP背景图片路径

        /// <summary>
        /// XP背景图片路径
        /// </summary>
        [Description("图版按钮链接"), DefaultValue("../images/")]
        public string XpBGImgFilePath
        {
            get
            {
                object obj = ViewState["XpBGImgFilePath"];
                return obj == null ? "../images/" : (string)obj;
            }
            set
            {
                ViewState["XpBGImgFilePath"] = value;
            }
        }

        #endregion


        #region Property ScriptContent 要加载的客户端脚本内容
        /// <summary>
        /// 要加载的客户端脚本内容        /// </summary>
        [Description("要加载的客户端脚本内容"), DefaultValue("")]
        public string ScriptContent
        {
            get
            {
                object obj = ViewState["ScriptContent"];
                return obj == null ? "" : (string)obj;
            }
            set
            {
                ViewState["ScriptContent"] = value;
            }
        }

        #endregion
    }
}