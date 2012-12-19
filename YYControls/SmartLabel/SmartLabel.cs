using System;
using System.Collections.Generic;
using System.Text;

using System.Web.UI.WebControls;
using System.Web.UI;

[assembly: System.Web.UI.WebResource("YYControls.SmartLabel.Resources.ScriptLibrary.js", "text/javascript")]

namespace YYControls
{
    /// <summary>
    /// SmartLabel类，继承自DropDownList
    /// </summary>
    [ToolboxData(@"<{0}:SmartLabel runat='server'></{0}:SmartLabel>")]
    [System.Drawing.ToolboxBitmap(typeof(YYControls.Resources.Icon), "SmartLabel.bmp")]
    public partial class SmartLabel : Label
    {
        /// <summary>
        /// 构造函数        /// </summary>
        public SmartLabel()
        {

        }

        /// <summary>
        /// OnPreRender
        /// </summary>
        /// <param name="e">e</param>
        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            // 实现Label控件的回发(Postback)功能
            ImplementPostback();
        }
    }
}
