﻿using System;
using System.Collections.Generic;
using System.Text;

using System.Web.UI.WebControls;
using System.Web.UI;

[assembly: System.Web.UI.WebResource("YYControls.SmartListBox.Resources.Icon.bmp", "image/bmp")]

namespace YYControls
{
    /// <summary>
    /// SmartListBox类，继承自ListBox
    /// </summary>
    [ToolboxData(@"<{0}:SmartListBox runat='server'></{0}:SmartListBox>")]
    [System.Drawing.ToolboxBitmap(typeof(YYControls.Resources.Icon), "SmartListBox.bmp")]
    public partial class SmartListBox : ListBox
    {
        /// <summary>
        /// 构造函数        /// </summary>
        public SmartListBox()
        {

        }

        /// <summary>
        /// 将控件的内容呈现到指定的编写器中
        /// </summary>
        /// <param name="writer">writer</param>
        protected override void RenderContents(HtmlTextWriter writer) 
        {
            // 呈现Option或OptionGroup
            OptionGroupRenderContents(writer);
        }
    }
}
