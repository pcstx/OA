using System;
using System.Collections.Generic;
using System.Text;

using System.ComponentModel;
using System.Web.UI;

namespace YYControls
{
    /// <summary>
    /// SmartListBox类的属性部分    /// </summary>
    public partial class SmartListBox
    {
        /// <summary>
        /// 用于添加SmartListBox的分组项的ListItem的Value值        /// </summary>
        [
        Browsable(true),
        Description("用于添加ListBox的分组项的ListItem的Value值"),
        Category("扩展")
        ]
        public virtual string OptionGroupValue
        {
            get
            {
                string s = (string)ViewState["OptionGroupValue"];

                return (s == null) ? "optgroup" : s;
            }
            set
            {
                ViewState["OptionGroupValue"] = value;
            }
        }
    }
}
