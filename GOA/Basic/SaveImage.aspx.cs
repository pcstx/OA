/*---------------------------------------------------------------------------
 * 版权说明：
 * 单元名称：SaveImage.aspx
 * 单元描述：保存图片，目前图片剪裁还未做，只是将html保存为图片了，还需要图片做裁剪，注意测试多浏览器下面的截图效果
 * 能放到一个页面最好
 * 修改日志
 * 修改人   修改日期    修改内容 
----------------------------------------------------------------------------*/
using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;

namespace ImageEditDemo
{
    public partial class SaveImage : System.Web.UI.Page
    {
        private string SaveHtmlPath = "SaveImage.html";
        private int iWidth = -1;
        private int iHeight = -1;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request["width"]) && !string.IsNullOrEmpty(Request["height"]))
            {
                iWidth = Convert.ToInt32(Request["width"]);
                iHeight = Convert.ToInt32(Request["height"]);
                Save();
            }
        }
        private void Save()
        {
            string url = Server.MapPath(SaveHtmlPath);//获取物理文件地址
            CuteImage thumb = new CuteImage(url, iWidth, iHeight, iWidth, iHeight);//注意截图区域。多分辨率测试
            System.Drawing.Bitmap x = thumb.GetBitmap();//获取剪裁图像
            string FileName = DateTime.Now.ToString("yyyyMMddhhmmss");//图片名
            string strFilePath = Server.MapPath("~/") + FileName + ".jpg";
            x.Save(strFilePath);//图片保存路径
            if (File.Exists(url))
            {
               //File.Delete(url);//删除html文件，避免冗余数据
            }
            Response.Write(strFilePath);
            Response.End();
        }
    }
}
