using System;
using System.Collections.Generic;
using System.Text;

using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.Web;

namespace YYControls
{
    /// <summary>
    /// SmartLabel类的属性部分    /// </summary>
    public partial class SmartLabel
    {
        /// <summary>
        /// 实现Label控件的回发(Postback)功能
        /// </summary>
        private void ImplementPostback()
        {
            if (this.EnablePostback)
            {
                // 使Label支持回发(Postback)的隐藏控件的ID
                string hiddenFieldId = string.Concat(this.ClientID, "_", HiddenFieldPostfix);

                // 注册隐藏控件
                Page.ClientScript.RegisterHiddenField(hiddenFieldId, "");

                // 注册客户端脚本
                this.Page.ClientScript.RegisterClientScriptResource(this.GetType(),
                    "YYControls.SmartLabel.Resources.ScriptLibrary.js");

                // 表单提交前将Label控件的的值赋给隐藏控件
                this.Page.ClientScript.RegisterOnSubmitStatement(this.GetType(),
                    string.Format("yy_sl_enablePostback_{0}",
                        this.ClientID),
                    string.Format("yy_sl_copyTextToHiddenField('{0}', '{1}')",
                        this.ClientID,
                        hiddenFieldId));
            }
        }

        /// <summary>
        /// 获取或设置 YYControls.SmartLabel 控件的文本内容        /// </summary>
        public override string Text
        {
            get
            {
                try
                {
                    if (this.EnablePostback && !string.IsNullOrEmpty(HttpContext.Current.Request[string.Concat(this.ClientID, "_", HiddenFieldPostfix)]))
                    {
                        // 隐藏控件的值
                        return HttpContext.Current.Request[string.Concat(this.ClientID, "_", HiddenFieldPostfix)];
                    }
                    else
                    {
                        return base.Text;
                    }
                }
                catch
                {
                    return base.Text;
                }
            }
            set
            {
                try
                {
                    if (this.EnablePostback && !string.IsNullOrEmpty(HttpContext.Current.Request[string.Concat(this.ClientID, "_", HiddenFieldPostfix)]))
                    {
                        // 隐藏控件的值
                        base.Text = HttpContext.Current.Request[string.Concat(this.ClientID, "_", HiddenFieldPostfix)];
                    }
                    else
                    {
                        base.Text = value;
                    }
                }
                catch
                {
                    base.Text = value;
                }
            }
        }
    }
}
