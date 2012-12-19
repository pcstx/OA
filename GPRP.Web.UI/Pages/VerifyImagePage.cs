using System;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Diagnostics;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Drawing.Drawing2D;

using GPRP.Plugin.VerifyImage;
using GPRP.Entity;
using GPRP.GPRPComponents;

namespace GPRP.Web.UI.Pages
{
	/// <summary>
	/// 验证码图片页面类
	/// </summary>
	public class VerifyImagePage : System.Web.UI.Page
	{

		
		/// <summary>
		/// 初始化
		/// </summary>
		/// <param name="e"></param>
		override protected void OnInit(EventArgs e)
		{
			//
			base.OnInit(e);

			GeneralConfigInfo config = GeneralConfigs.GetConfig();
			string bgcolor = DNTRequest.GetQueryString("bgcolor").Trim();
			int textcolor = DNTRequest.GetQueryInt("textcolor", 1);
            string[] bgcolorArray = bgcolor.Split(',');
            
            Color bg = Color.White;
            
            if (bgcolorArray.Length == 1 && bgcolor != string.Empty)
            {
                bg = Utils.ToColor(bgcolor);
            }
            else if (bgcolorArray.Length == 3 && Utils.IsNumericArray(bgcolorArray))
            {                
                bg = Color.FromArgb(Utils.StrToInt(bgcolorArray[0], 255), Utils.StrToInt(bgcolorArray[1], 255), Utils.StrToInt(bgcolorArray[2], 255));
            }
            VerifyImageInfo verifyimg = VerifyImageProvider.GetInstance(config.VerifyImageAssemly).GenerateImage( CreateAuthStr(5, false), 120, 60, bg, textcolor);
            //IVerifyImage verifyimg = new IVerifyImage(OnlineUsers.UpdateInfo(config.Passwordkey, config.Onlinetimeout).Verifycode, 90, 50, bg, textcolor);

			Bitmap image = verifyimg.Image;

            System.Web.HttpContext.Current.Response.ContentType = verifyimg.ContentType;
          
			//MemoryStream ms = new MemoryStream();
            
			image.Save(this.Response.OutputStream, verifyimg.ImageFormat);
			//System.Web.HttpContext.Current.Response.OutputStream.Write(ms.ToArray(), 0, (int)ms.Length);
		}
        /// <summary>
        /// 产生验证码
        /// </summary>
        /// <param name="len">长度</param>
        /// <param name="OnlyNum">是否仅为数字</param>
        /// <returns></returns>
        public static string CreateAuthStr(int len, bool OnlyNum)
        {
            int number;
            StringBuilder checkCode = new StringBuilder();

            for (int i = 0; i < len; i++)
            {
                if (!OnlyNum)
                {
                    number = verifycodeRandom.Next(0, verifycodeRange.Length);
                }
                else
                {
                    number = verifycodeRandom.Next(0, 10);
                }
                checkCode.Append(verifycodeRange[number]);
            }

            return checkCode.ToString();
        }
        /// <summary>
        /// 验证码生成的取值范围
        /// </summary>
        private static string[] verifycodeRange = { "0","1","2","3","4","5","6","7","8","9",
                                                    "a","b","c","d","e","f","g",
                                                    "h",    "j","k",    "m","n",
                                                        "p","q",    "r","s","t",
                                                    "u","v","w",    "x","y"
        
                                                  };
        /// <summary>
        /// 生成验证码所使用的随机数发生器
        /// </summary>
        private static Random verifycodeRandom = new Random();

	}
}
