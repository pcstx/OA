/*---------------------------------------------------------------------------
 * 版权说明：　
 * 单元名称：SaveHtml.ashx
 * 单元描述：保存HTML。很奇怪，我建立aspx页面使用ajax就会报错，只能使用ashx页面，原因不明，而保存图片又必须在aspx页面，并且设置AspCompat="true"，如果
 * 能放到一个页面最好
 * 修改日志
 * 修改人   修改日期    修改内容 
----------------------------------------------------------------------------*/
using System;
using System.Data;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.IO;

namespace ImageEditDemo
{
    /// <summary>
    /// $codebehindclassname$ 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class SaveHtml : IHttpHandler
    {
        private string SaveHtmlPath = "SaveImage.html";
        private int iWidth = -1;
        private int iHeight = -1;
        private string fullname = "";
        public void ProcessRequest(HttpContext context)
        {

            if (!string.IsNullOrEmpty(context.Request["getKey"]))
            {
                iWidth = Convert.ToInt32(context.Request["width"]);
                iHeight = Convert.ToInt32(context.Request["height"]);
                fullname = context.Request["fullname"];
                Save(context.Request["getKey"].ToString(), context);
            }
        }
        private void Save(string strValue, HttpContext context)
        {
            if (iWidth != -1 && iHeight != -1)
            {
                SaveHtmlTx(strValue, context);
                context.Server.Transfer("SaveImage.aspx?width=" + iWidth.ToString() + "&height=" + iHeight.ToString());
            }
        }
        private void SaveHtmlTx(string strValue, HttpContext context)
        {
            string strStart = "<html xmlns:v><head><meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\" /><title></title><style>v\\:*{behavior:url(#default#VML);}</style>"
                +"<script language=\"javascript\" type=\"text/javascript\">"
                    +"function PreviewImage(divImage, upload, width, height) {"
                    + "try {var imgPath ='"+fullname+"' ; var Browser_Agent = navigator.userAgent;if (Browser_Agent.indexOf(\"Firefox\") != -1) { imgPath = upload.files[0].getAsDataURL(); "
                    +"document.getElementById(divImage).innerHTML = \"<img id='editBodyImg' src='\" + imgPath + \"' width='\" + width + \"' height='\" + height + \"'/>\";}"
                    + "else {var Preview = document.getElementById(divImage);  Preview.filters.item(\"DXImageTransform.Microsoft.AlphaImageLoader\").src = imgPath;"
                    + "Preview.style.width = width; Preview.style.height = height; }} catch (e) { }} </script>"
                + "</head><body onload=\"PreviewImage('editBodyImg'," + iWidth + "," + iHeight + ");\">";
            File.WriteAllText(context.Server.MapPath(SaveHtmlPath), strStart + strValue + "<script>var shape=document.getElementsByTagName('shape');for(var i=0;i<shape.length;i++){shape[i].fillcolor='none'}</script></body></html>");
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}
