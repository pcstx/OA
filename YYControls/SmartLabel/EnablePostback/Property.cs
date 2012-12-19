using System;
using System.Collections.Generic;
using System.Text;

using System.ComponentModel;
using System.Web.UI;

namespace YYControls
{
    /// <summary>
    /// SmartLabel类的属性部分    /// </summary>
    public partial class SmartLabel
    {
        /// <summary>
        /// 使Label支持回发(Postback)的隐藏控件的后缀名        /// </summary>
        [
        Browsable(true),
        Description("使Label支持回发(Postback)的隐藏控件的后缀名"),
        Category("扩展"),
        DefaultValue("EnablePostback")
        ]
        public virtual string HiddenFieldPostfix
        {
            get
            {
                string s = (string)ViewState["HiddenFieldPostfix"];

                return (s == null) ? "EnablePostback" : s;
            }
            set
            {
                ViewState["HiddenFieldPostfix"] = value;
            }
        }

        /// <summary>
        /// 是否启用Label控件的回发(Postback)
        /// </summary>
        [
        Browsable(true),
        Description("是否启用Label控件的回发(Postback)"),
        Category("扩展"),
        DefaultValue(false)
        ]
        public virtual bool EnablePostback
        {
            get
            {
                bool? b = (bool?)ViewState["EnablePostback"];

                return (b == null) ? false : (bool)b;
            }

            set
            {
                ViewState["EnablePostback"] = value;
            }
        }
    }
}
