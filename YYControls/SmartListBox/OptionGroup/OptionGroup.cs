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
    /// SmartListBox类的属性部分    /// </summary>
    public partial class SmartListBox
    {
        /// <summary>
        /// 呈现Option或OptionGroup
        /// </summary>
        /// <param name="writer">writer</param>
        private void OptionGroupRenderContents(HtmlTextWriter writer)
        {
            // 是否需要呈现OptionGroup的EndTag
            bool writerEndTag = false;

            foreach (ListItem li in this.Items)
            {
                // 如果没有optgroup属性则呈现Option
                if (li.Value != this.OptionGroupValue)
                {
                    // 呈现Option
                    RenderListItem(li, writer);
                }
                // 如果有optgroup属性则呈现OptionGroup
                else
                {
                    if (writerEndTag)
                        // 呈现OptionGroup的EndTag
                        OptionGroupEndTag(writer);
                    else
                        writerEndTag = true;

                    // 呈现OptionGroup的BeginTag
                    OptionGroupBeginTag(li, writer);
                }
            }

            if (writerEndTag)
                // 呈现OptionGroup的EndTag
                OptionGroupEndTag(writer);
        }

        /// <summary>
        /// 呈现OptionGroup的BeginTag
        /// </summary>
        /// <param name="li">OptionGroup数据项</param>
        /// <param name="writer">writer</param>
        private void OptionGroupBeginTag(ListItem li, HtmlTextWriter writer)
        {
            writer.WriteBeginTag("optgroup");

            // 写入OptionGroup的label
            writer.WriteAttribute("label", li.Text);

            foreach (string key in li.Attributes.Keys)
            {
                // 写入OptionGroup的其它属性
                writer.WriteAttribute(key, li.Attributes[key]);
            }

            writer.Write(HtmlTextWriter.TagRightChar);
            writer.WriteLine();
        }

        /// <summary>
        /// 呈现OptionGroup的EndTag
        /// </summary>
        /// <param name="writer">writer</param>
        private void OptionGroupEndTag(HtmlTextWriter writer)
        {
            writer.WriteEndTag("optgroup");
            writer.WriteLine();
        }

        /// <summary>
        /// 呈现Option
        /// </summary>
        /// <param name="li">Option数据项</param>
        /// <param name="writer">writer</param>
        private void RenderListItem(ListItem li, HtmlTextWriter writer)
        {
            writer.WriteBeginTag("option");

            // 写入Option的Value
            writer.WriteAttribute("value", li.Value, true);

            if (li.Selected)
            {
                // 如果该Option被选中则写入selected
                writer.WriteAttribute("selected", "selected", false);
            }

            foreach (string key in li.Attributes.Keys)
            {
                // 写入Option的其它属性
                writer.WriteAttribute(key, li.Attributes[key]);
            }

            writer.Write(HtmlTextWriter.TagRightChar);

            // 写入Option的Text
            HttpUtility.HtmlEncode(li.Text, writer);

            writer.WriteEndTag("option");
            writer.WriteLine();
        }
    }
}
