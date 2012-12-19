using System;
using System.ComponentModel;
using System.Text;
using System.Web.UI;
using GPRP.GPRPComponents;

namespace GPRP.GPRPControls
{
    /// <summary>
    /// Label控件。 失败,未完成
    /// </summary>
    [DefaultProperty("Text"), ToolboxData("<{0}:Label runat=server></{0}:Label>")]
    public class GPLabel : WebControl
    {
         static GPLabel()
        {
           
        }

        /// <summary>
        /// 构造函数
         /// </summary>
        public GPLabel()
        {
            
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
        /// 添加子对象
        /// </summary>
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
        /// 输出html,在浏览器中显示控件
        /// </summary>
        /// <param name="output"></param>
        protected override void Render(HtmlTextWriter output)
        {
            
                StringBuilder sb = new System.Text.StringBuilder();
               // sb.Append("if (typeof(Page_ClientValidate) == 'function') { if (Page_ClientValidate() == false) { return false; }}");    //保证验证函数的执行
                output.Write(" <span id=span id=\"" + this.ClientID + "\">"+ this.Text +"</span>");

        }
        #region Property Text 按钮文字

        /// <summary>
        /// 按钮文字 " 提 交 "
        /// </summary>

        [Bindable(true), Category("Appearance"), DefaultValue("")]
        public string Text
        {
            get
            {
                object obj = ViewState["LabelText"];
                return obj == null ? ResourceManager.GetString("Label_NULL") : (string)obj;
            }
            set
            {
                ViewState["LabelText"] = ResourceManager.GetString(value);
            }
        }
        #endregion
    }
}
