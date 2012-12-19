using System;
using System.Collections.Generic;
using System.Text;

using System.Web.UI.WebControls;
using System.Web.UI;

[assembly: System.Web.UI.WebResource("YYControls.SmartDropDownList.Resources.Icon.bmp", "image/bmp")]

namespace YYControls
{
    /// <summary>
    /// SmartDropDownList类，继承自DropDownList
    /// </summary>
    [ToolboxData(@"<{0}:SmartDropDownList runat='server'></{0}:SmartDropDownList>")]
    [System.Drawing.ToolboxBitmap(typeof(YYControls.Resources.Icon), "SmartDropDownList.bmp")]
    public partial class SmartDropDownList : DropDownList
    {
        /// <summary>
        /// 构造函数        /// </summary>
        public SmartDropDownList()
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
