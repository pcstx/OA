using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Drawing.Drawing2D;

using GPRP.Entity;
using GPRP.GPRPComponents;
using GPRP.Plugin;
namespace GPRP.GPRPBussiness
{
	/// <summary>
	/// 论坛工具类
	/// </summary>
	public class WebUtils
	{
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

		/// <summary>
		/// 返回论坛用户密码cookie明文
		/// </summary>
		/// <param name="key">密钥</param>
		/// <returns></returns>
		public static string GetCookiePassword(string key)
		{
			return DES.Decode(GetCookie("password"), key).Trim();

		}

		/// <summary>
		/// 返回论坛用户密码cookie明文
		/// </summary>
		/// <param name="password">密码密文</param>
		/// <param name="key">密钥</param>
		/// <returns></returns>
		public static string GetCookiePassword(string password, string key)
		{
			return DES.Decode(password, key);

		}
        /// <summary>
        /// 返回用户登录ID号
        /// </summary>
        /// <returns></returns>
        public static string GetCookieUser()
        {
            return GetCookie("userid").Trim();
        }

		/// <summary>
		/// 返回密码密文
		/// </summary>
		/// <param name="password">密码明文</param>
		/// <param name="key">密钥</param>
		/// <returns></returns>
		public static string SetCookiePassword(string password, string key)
		{
			return DES.Encode(password, key);
		}


		/// <summary>
		/// 返回用户安全问题答案的存储数据
		/// </summary>
		/// <param name="questionid">问题id</param>
		/// <param name="answer">答案</param>
		/// <returns></returns>
		public static string GetUserSecques(int questionid, string answer)
		{
			if (questionid > 0)
			{
				return Utils.MD5(answer + Utils.MD5(questionid.ToString())).Substring(15, 8);
			}
			return "";
		}
		
		/// <summary>
		/// 写论坛cookie值
		/// </summary>
		/// <param name="strName">项</param>
		/// <param name="strValue">值</param>
		public static void WriteCookie(string strName, string strValue)
		{
            HttpCookie cookie = HttpContext.Current.Request.Cookies[GeneralConfigs.GetConfig().CookieName];
			if (cookie == null)
			{
                cookie = new HttpCookie(GeneralConfigs.GetConfig().CookieName);
				cookie.Values[strName] = Utils.UrlEncode(strValue);
			}
			else
			{

				cookie.Values[strName] = Utils.UrlEncode(strValue);
                if (HttpContext.Current.Request.Cookies[GeneralConfigs.GetConfig().CookieName]["expires"] != null)
				{
                    int expires = Utils.StrToInt(HttpContext.Current.Request.Cookies[GeneralConfigs.GetConfig().CookieName]["expires"].ToString(), 0);
					if (expires > 0)
					{
                        cookie.Expires = DateTime.Now.AddMinutes(Utils.StrToInt(HttpContext.Current.Request.Cookies[GeneralConfigs.GetConfig().CookieName]["expires"].ToString(), 0));
					}
				}
			}

            string cookieDomain = GeneralConfigs.GetConfig().CookieDomain.Trim();
			if (cookieDomain != string.Empty && HttpContext.Current.Request.Url.Host.IndexOf(cookieDomain) > -1 && IsValidDomain(HttpContext.Current.Request.Url.Host))
				cookie.Domain = cookieDomain;

			HttpContext.Current.Response.AppendCookie(cookie);

		}


		/// <summary>
		/// 写cookie值
		/// </summary>
		/// <param name="strName">名称</param>
		/// <param name="intValue">值</param>
		public static void WriteCookie(string strName, int intValue)
		{
			WriteCookie(strName, intValue.ToString());
		}



		/// <summary>
		/// 写论坛登录用户的cookie
		/// </summary>
		/// <param name="uid">用户Id</param>
		/// <param name="expires">cookie有效期</param>
		/// <param name="passwordkey">用户密码Key</param>
		/// <param name="templateid">用户当前要使用的界面风格</param>
		/// <param name="invisible">用户当前的登录模式(正常或隐身)</param>
		public static void WriteUserCookie(string uid, string password, int expires)
		{
            //ShortUserInfo userinfo = Users.GetShortUserInfo(uid);
            //if (userinfo != null)
            //{
            HttpCookie cookie = new HttpCookie(GeneralConfigs.GetConfig().CookieName);
			cookie.Values["userid"] = uid.ToString();
            cookie.Values["password"] = Utils.UrlEncode(WebUtils.SetCookiePassword(password, GeneralConfigs.GetConfig().Passwordkey));
				
                //cookie.Values["tpp"] = userinfo.Tpp.ToString();
                //cookie.Values["ppp"] = userinfo.Ppp.ToString();
                //cookie.Values["pmsound"] = userinfo.Pmsound.ToString();
                //if (invisible != 0 || invisible != 1)
                //{
                //    invisible = userinfo.Invisible;
                //}
                //cookie.Values["invisible"] = invisible.ToString();

                //cookie.Values["referer"] = "index.aspx";
                //cookie.Values["sigstatus"] = userinfo.Sigstatus.ToString();
				cookie.Values["expires"] = expires.ToString();
				if (expires > 0)
				{
					cookie.Expires = DateTime.Now.AddMinutes(expires);
				}
                string cookieDomain = GeneralConfigs.GetConfig().CookieDomain.Trim();
				if (cookieDomain != string.Empty && HttpContext.Current.Request.Url.Host.IndexOf(cookieDomain) > -1 && IsValidDomain(HttpContext.Current.Request.Url.Host))
				{
					cookie.Domain = cookieDomain;
				}
				
				HttpContext.Current.Response.AppendCookie(cookie);
				
				
            //}
		}

		

		/// <summary>
		/// 获得论坛cookie值
		/// </summary>
		/// <param name="strName">项</param>
		/// <returns>值</returns>
		public static string GetCookie(string strName)
		{ 
            if (HttpContext.Current.Request.Cookies != null && HttpContext.Current.Request.Cookies[GeneralConfigs.GetConfig().CookieName] != null && HttpContext.Current.Request.Cookies[GeneralConfigs.GetConfig().CookieName][strName] != null)
			{
                return Utils.UrlDecode(HttpContext.Current.Request.Cookies[GeneralConfigs.GetConfig().CookieName][strName].ToString());
			}

			return "";
		}


		/// <summary>
		/// 清除论坛登录用户的cookie
		/// </summary>
		public static void ClearUserCookie()
		{
            ClearUserCookie(GeneralConfigs.GetConfig().CookieName);
		}
        public static void ClearCookie(string cookieName)
        {
            HttpCookie cookie = new HttpCookie(cookieName);
            cookie.Values.Clear();
            cookie.Expires = DateTime.Now.AddYears(-1);
        }
        public static void ClearUserCookie(string cookieName)
        {
            HttpCookie cookie = new HttpCookie(cookieName);
            cookie.Values.Clear();
            cookie.Expires = DateTime.Now.AddYears(-1);
            string cookieDomain = GeneralConfigs.GetConfig().CookieDomain.Trim();
            if (cookieDomain != string.Empty && HttpContext.Current.Request.Url.Host.IndexOf(cookieDomain) > -1 && IsValidDomain(HttpContext.Current.Request.Url.Host))
                cookie.Domain = cookieDomain;
            HttpContext.Current.Response.AppendCookie(cookie);
        }

		/// <summary>
		/// 产生验证码
		/// </summary>
		/// <param name="len">长度</param>
		/// <returns>验证码</returns>
		public static string CreateAuthStr(int len)
		{
            int number;
            StringBuilder checkCode = new StringBuilder();

            Random random = new Random();

            for (int i = 0; i < len; i++)
            {
                number = random.Next();

                if (number % 2 == 0)
                {
                    checkCode.Append((char)('0' + (char)(number % 10)));
                }
                else
                {
                    checkCode.Append((char)('A' + (char)(number % 26)));
                }

            }

            return checkCode.ToString();
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
		/// 创建主题缓存标志文件
		/// </summary>
		/// <returns></returns>
		public static bool CreateTopicCacheInfoFile()
		{
			string infofilepath = Utils.GetMapPath(GetShowTopicCacheDir() + "/cacheinfo.config");
			try
			{
				using (FileStream fs = new FileStream(infofilepath, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite))
				{
					Byte[] info = System.Text.Encoding.UTF8.GetBytes("<?xml version=\"1.0\"?>");
					fs.Write(info, 0, info.Length);
					fs.Close();
				}
				return true;
			}
			catch
			{
				return false;
			}
		}
		
		/// <summary>
		/// 向HTTP输出指定主题id的主题缓存文件, 其中某些时段会执行删除过期主题缓存文件
		/// </summary>
		/// <param name="tid">主题id</param>
		/// <param name="timeout">缓存文件的有效时间</param>
		/// <returns></returns>
		public static bool ResponseTopicCacheFile(int tid, int timeout)
		{
			TimeSpan s;
			if ((System.DateTime.Now.Minute >= System.DateTime.Now.Day) && (System.DateTime.Now.Minute <= System.DateTime.Now.Day + 10))
			{
				string infofilepath = Utils.GetMapPath(GetShowTopicCacheDir() + "/cacheinfo.config");
				if (System.IO.File.Exists(infofilepath))
				{
					s = (System.IO.File.GetCreationTime(infofilepath) - System.DateTime.Now);
				
					if (Math.Abs(s.TotalHours) > 1)
					{
						CreateTopicCacheInfoFile();
						DeleteTimeoutTopicCacheFiles(timeout);
					}
				}
				else
				{
					CreateTopicCacheInfoFile();
				}
			}

			
			string filepath = GetTopicCacheFilename(tid);
			if (System.IO.File.Exists(filepath))
			{
				s = (System.IO.File.GetCreationTime(filepath) - System.DateTime.Now);
				if (timeout > 0 && Math.Abs(s.TotalMinutes) > timeout)
				{
					DeleteTopicCacheFile(tid);
					return false;
				}
				System.Web.HttpContext.Current.Response.WriteFile(filepath);
				System.Web.HttpContext.Current.Response.End();
				return true;
			}
			return false;
		}

		/// <summary>
		/// 返回主题缓存文件名
		/// </summary>
		/// <param name="tid">主题id</param>
		/// <returns></returns>
		public static string GetTopicCacheFilename(int tid)
		{
			return Utils.GetMapPath(GetShowTopicCacheDir() + "/" + tid.ToString() + ".dntcache");
		}

		/// <summary>
		/// 删除所有过期的主题缓存文件
		/// </summary>
		/// <param name="timeout">超时时间</param>
		/// <returns></returns>
		public static bool DeleteTimeoutTopicCacheFiles(int timeout)
		{
			try
			{
				DirectoryInfo dirinfo = new DirectoryInfo(Utils.GetMapPath(GetShowTopicCacheDir()));
				TimeSpan s;
				foreach (FileSystemInfo file in dirinfo.GetFiles())
				{
					if (file != null && file.Extension.ToLower().Equals(".dntcache"))
					{
						s = (System.IO.File.GetCreationTime(file.FullName) - System.DateTime.Now);
						if (timeout > 0 && Math.Abs(s.TotalMinutes) > (timeout + 1))
						{
							try
							{
								file.Delete();
							}
							catch
							{
								//
							}
						}
					}
				}
			}
			catch
			{
				return false;
			}
			return true;

		}


		/// <summary>
		/// 创建主题缓存文件
		/// </summary>
		/// <param name="tid">主题id</param>
		/// <param name="pagetext">缓存的字符串内容</param>
		/// <returns></returns>
		public static bool CreateTopicCacheFile(int tid, string pagetext)
		{
			string filepath = GetTopicCacheFilename(tid);
			try
			{
				using (FileStream fs = new FileStream(filepath, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite))
				{
					Byte[] info = System.Text.Encoding.UTF8.GetBytes(pagetext);
					fs.Write(info, 0, info.Length);
					fs.Close();
					return true;
				}
				
			}
			catch
			{
				return false;
			}
		}

		/// <summary>
		/// 删除指定id的主题游客缓存
		/// </summary>
		/// <param name="tid">主题id</param>
		/// <returns></returns>
		public static bool DeleteTopicCacheFile(int tid)
		{
			string filepath = GetTopicCacheFilename(tid);
			try
			{
				System.IO.File.Delete(filepath);
				return true;
			}
			catch
			{
				return false;
			}
		}

		/// <summary>
		/// 删除指定id列表的主题游客缓存
		/// </summary>
		/// <param name="tidlist">主题id列表</param>
		/// <returns></returns>
		public static int DeleteTopicCacheFile(string tidlist)
		{
			int count = 0;

			string[] strNumber = Utils.SplitString(tidlist, ",");
			foreach (string tid in strNumber)
			{
				if (Utils.IsNumeric(tid) && DeleteTopicCacheFile(Int32.Parse(tid)))
				{
					count++;
				}
			}
			return count;
		}


		/// <summary>
		/// 返回"查看主题"的页面缓存目录
		/// </summary>
		/// <returns>缓存目录</returns>
		public static string GetShowTopicCacheDir()
		{
			return GetCacheDir("showtopic");
		}


		/// <summary>
		/// 返回指定目录的页面缓存目录
		/// </summary>
		/// <param name="path">目录</param>
		/// <returns>缓存目录</returns>
		public static string GetCacheDir(string path)
		{
			path = path.Trim();
			StringBuilder dir = new StringBuilder();
			dir.Append(BaseConfigs.GetForumPath);
			dir.Append("cache/");
			dir.Append(path);
			string cachedir = dir.ToString();
			if (!Directory.Exists(Utils.GetMapPath(cachedir)))
			{
				Utils.CreateDir(Utils.GetMapPath(cachedir));
			}
			return cachedir;
		}

		/// <summary>
		/// 保存上传头像
		/// </summary>
		/// <param name="MaxAllowFileSize">最大允许的头像文件尺寸(单位:KB)</param>
		/// <returns>保存文件的相对路径</returns>
		public static string SaveRequestAvatarFile(int userid, int MaxAllowFileSize)
		{

			string filename = Path.GetFileName(HttpContext.Current.Request.Files[0].FileName);
			string fileextname = Path.GetExtension(filename).ToLower();
			string filetype = HttpContext.Current.Request.Files[0].ContentType.ToLower();
			//int filesize = HttpContext.Current.Request.Files[0].ContentLength;
			// 判断 文件扩展名/文件大小/文件类型 是否符合要求
			if ((fileextname.Equals(".jpg") || fileextname.Equals(".gif") || fileextname.Equals(".png")) && (filetype.StartsWith("image")))
			{
				StringBuilder savedir = new StringBuilder();
				savedir.Append(BaseConfigs.GetForumPath);
				savedir.Append("avatars/upload/");
				int t1 = (int)((double)userid / (double)10000);
				savedir.Append(t1);
				savedir.Append("/");
				int t2 = (int)((double)userid / (double)200);
				savedir.Append(t2);
				savedir.Append("/");
				if (!Directory.Exists(Utils.GetMapPath(savedir.ToString())))
				{
					Utils.CreateDir(Utils.GetMapPath(savedir.ToString()));
					//System.IO.Directory.CreateDirectory(Utils.GetMapPath(savedir.ToString()));
				}
				string newfilename = savedir.ToString() + userid.ToString() + fileextname;
				//if (System.IO.File.Exists(Utils.GetMapPath(savedir.ToString() + userid.ToString() + ".jpg")))
				
				if (HttpContext.Current.Request.Files[0].ContentLength <= MaxAllowFileSize)
				{
                    File.Delete(Utils.GetMapPath(savedir.ToString()) + userid.ToString() + ".jpg");
				    File.Delete(Utils.GetMapPath(savedir.ToString()) + userid.ToString() + ".gif");
				    File.Delete(Utils.GetMapPath(savedir.ToString()) + userid.ToString() + ".png");

					HttpContext.Current.Request.Files[0].SaveAs(Utils.GetMapPath(newfilename));
					return newfilename;
				}
			}
			return "";
		}

		/// <summary>
		/// 加图片水印
		/// </summary>
		/// <param name="filename">文件名</param>
		/// <param name="watermarkFilename">水印文件名</param>
		/// <param name="watermarkStatus">图片水印位置</param>
		public static void AddImageSignPic(Image img, string filename, string watermarkFilename, int watermarkStatus, int quality, int watermarkTransparency)
		{
			Graphics g = Graphics.FromImage(img);
			//设置高质量插值法
			//g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
			//设置高质量,低速度呈现平滑程度
			//g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
			Image watermark = new Bitmap(watermarkFilename);

            if (watermark.Height >= img.Height || watermark.Width >= img.Width)
            {
                return;
            }

			ImageAttributes imageAttributes = new ImageAttributes();
			ColorMap colorMap = new ColorMap();

			colorMap.OldColor = Color.FromArgb(255, 0, 255, 0);
			colorMap.NewColor = Color.FromArgb(0, 0, 0, 0);
			ColorMap[] remapTable = {colorMap};

			imageAttributes.SetRemapTable(remapTable, ColorAdjustType.Bitmap);

			float transparency = 0.5F;
			if (watermarkTransparency >=1 && watermarkTransparency <=10)
			{
				transparency = (watermarkTransparency / 10.0F);
			}

			float[][] colorMatrixElements = {
												new float[] {1.0f,  0.0f,  0.0f,  0.0f, 0.0f},
												new float[] {0.0f,  1.0f,  0.0f,  0.0f, 0.0f},
												new float[] {0.0f,  0.0f,  1.0f,  0.0f, 0.0f},
												new float[] {0.0f,  0.0f,  0.0f,  transparency, 0.0f},
												new float[] {0.0f,  0.0f,  0.0f,  0.0f, 1.0f}
											};

			ColorMatrix colorMatrix = new ColorMatrix(colorMatrixElements);

			imageAttributes.SetColorMatrix(colorMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);

			int xpos = 0;
			int ypos = 0;

			switch(watermarkStatus)
			{
				case 1:
					xpos = (int)(img.Width * (float).01);
					ypos = (int)(img.Height * (float).01);
					break;
				case 2:
					xpos = (int)((img.Width * (float).50) - (watermark.Width / 2));
					ypos = (int)(img.Height * (float).01);
					break;
				case 3:
					xpos = (int)((img.Width * (float).99) - (watermark.Width));
					ypos = (int)(img.Height * (float).01);
					break;
				case 4:
					xpos = (int)(img.Width * (float).01);
					ypos = (int)((img.Height * (float).50) - (watermark.Height / 2));
					break;
				case 5:
					xpos = (int)((img.Width * (float).50) - (watermark.Width / 2));
					ypos = (int)((img.Height * (float).50) - (watermark.Height / 2));
					break;
				case 6:
					xpos = (int)((img.Width * (float).99) - (watermark.Width));
					ypos = (int)((img.Height * (float).50) - (watermark.Height / 2));
					break;
				case 7:
					xpos = (int)(img.Width * (float).01);
					ypos = (int)((img.Height * (float).99) - watermark.Height);
					break;
				case 8:
					xpos = (int)((img.Width * (float).50) - (watermark.Width / 2));
					ypos = (int)((img.Height * (float).99) - watermark.Height);
					break;
				case 9:
					xpos = (int)((img.Width * (float).99) - (watermark.Width));
					ypos = (int)((img.Height * (float).99) - watermark.Height);
					break;
			}

			g.DrawImage(watermark, new Rectangle(xpos, ypos, watermark.Width, watermark.Height), 0, 0, watermark.Width, watermark.Height, GraphicsUnit.Pixel, imageAttributes);
			//g.DrawImage(watermark, new System.Drawing.Rectangle(xpos, ypos, watermark.Width, watermark.Height), 0, 0, watermark.Width, watermark.Height, System.Drawing.GraphicsUnit.Pixel);
			
			ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders(); 
			ImageCodecInfo ici = null;
			foreach(ImageCodecInfo codec in codecs)
			{
				if (codec.MimeType.IndexOf("jpeg") > -1)
				{
					ici = codec;
				}
			}
			EncoderParameters encoderParams = new EncoderParameters();
			long[] qualityParam = new long[1];
			if (quality < 0 || quality > 100)
			{
				quality = 80;
			}
			qualityParam[0] = quality;

            EncoderParameter encoderParam = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, qualityParam);
			encoderParams.Param[0] = encoderParam;

			if (ici != null)
			{
				img.Save(filename, ici, encoderParams);
			}
			else
			{
				img.Save(filename);
			}
			
			g.Dispose();
			img.Dispose();
			watermark.Dispose();
			imageAttributes.Dispose();
		}


		/// <summary>
		/// 增加图片文字水印
		/// </summary>
		/// <param name="filename">文件名</param>
		/// <param name="watermarkText">水印文字</param>
		/// <param name="watermarkStatus">图片水印位置</param>
		public static void AddImageSignText(Image img, string filename, string watermarkText, int watermarkStatus, int quality, string fontname, int fontsize)
		{
			//System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(img);
			//	.FromFile(filename);
			Graphics g = Graphics.FromImage(img);
			Font drawFont = new Font(fontname, fontsize, FontStyle.Regular, GraphicsUnit.Pixel);
			SizeF crSize;
			crSize = g.MeasureString(watermarkText, drawFont);

			float xpos = 0;
			float ypos = 0;

			switch(watermarkStatus)
			{
				case 1:
					xpos = (float)img.Width * (float).01;
					ypos = (float)img.Height * (float).01;
					break;
				case 2:
					xpos = ((float)img.Width * (float).50) - (crSize.Width / 2);
					ypos = (float)img.Height * (float).01;
					break;
				case 3:
					xpos = ((float)img.Width * (float).99) - crSize.Width;
					ypos = (float)img.Height * (float).01;
					break;
				case 4:
					xpos = (float)img.Width * (float).01;
					ypos = ((float)img.Height * (float).50) - (crSize.Height / 2);
					break;
				case 5:
					xpos = ((float)img.Width * (float).50) - (crSize.Width / 2);
					ypos = ((float)img.Height * (float).50) - (crSize.Height / 2);
					break;
				case 6:
					xpos = ((float)img.Width * (float).99) - crSize.Width;
					ypos = ((float)img.Height * (float).50) - (crSize.Height / 2);
					break;
				case 7:
					xpos = (float)img.Width * (float).01;
					ypos = ((float)img.Height * (float).99) - crSize.Height;
					break;
				case 8:
					xpos = ((float)img.Width * (float).50) - (crSize.Width / 2);
					ypos = ((float)img.Height * (float).99) - crSize.Height;
					break;
				case 9:
					xpos = ((float)img.Width * (float).99) - crSize.Width;
					ypos = ((float)img.Height * (float).99) - crSize.Height;
					break;
			}

//			System.Drawing.StringFormat StrFormat = new System.Drawing.StringFormat();
//			StrFormat.Alignment = System.Drawing.StringAlignment.Center;
//
//			g.DrawString(watermarkText, drawFont, new System.Drawing.SolidBrush(System.Drawing.Color.White), xpos + 1, ypos + 1, StrFormat);
//			g.DrawString(watermarkText, drawFont, new System.Drawing.SolidBrush(System.Drawing.Color.Black), xpos, ypos, StrFormat);
			g.DrawString(watermarkText, drawFont, new SolidBrush(Color.White), xpos + 1, ypos + 1);
			g.DrawString(watermarkText, drawFont, new SolidBrush(Color.Black), xpos, ypos);

			ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders(); 
			ImageCodecInfo ici = null;
			foreach(ImageCodecInfo codec in codecs)
			{
				if (codec.MimeType.IndexOf("jpeg") > -1)
				{
					ici = codec;
				}
			}
			EncoderParameters encoderParams = new EncoderParameters();
			long[] qualityParam = new long[1];
			if (quality < 0 || quality > 100)
			{
				quality = 80;
			}
			qualityParam[0] = quality;

            EncoderParameter encoderParam = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, qualityParam);
			encoderParams.Param[0] = encoderParam;

			if (ici != null)
			{
				img.Save(filename, ici, encoderParams);
			}
			else
			{
				img.Save(filename);
			}
			g.Dispose();
			//bmp.Dispose();
			img.Dispose();
		}

		/// <summary>
		/// 判断是否有上传的文件
		/// </summary>
		/// <returns>是否有上传的文件</returns>
		public static bool IsPostFile()
		{
			for (int i = 0; i < HttpContext.Current.Request.Files.Count; i++)
			{
				if (HttpContext.Current.Request.Files[i].FileName != "")
				{
					return true;
				}
			}
			return false;
		}
		
		
		/// <summary>
		/// 保存上传的文件
		/// </summary>
		/// <param name="forumid">版块id</param>
		/// <param name="MaxAllowFileCount">最大允许的上传文件个数</param>
		/// <param name="MaxSizePerDay">每天允许的附件大小总数</param>
		/// <param name="MaxFileSize">单个最大允许的文件字节数</param>/// 
		/// <param name="TodayUploadedSize">今天已经上传的附件字节总数</param>
		/// <param name="AllowFileType">允许的文件类型, 以string[]形式提供</param>
		/// <param name="config">附件保存方式 0=按年/月/日存入不同目录 1=按年/月/日/论坛存入不同目录 2=按论坛存入不同目录 3=按文件类型存入不同目录</param>
		/// <param name="watermarkstatus">图片水印位置</param>
		/// <param name="filekey">File控件的Key(即Name属性)</param>
		/// <returns>文件信息结构</returns>
        public static AttachmentInfo[] SaveRequestFiles(int forumid, int MaxAllowFileCount, int MaxSizePerDay, int MaxFileSize, int TodayUploadedSize, string AllowFileType, int watermarkstatus, GeneralConfigInfo config, string filekey)
		{
			string[] tmp = Utils.SplitString(AllowFileType, "\r\n");
			string[] AllowFileExtName = new string[tmp.Length];
			int[] MaxSize = new int[tmp.Length];
			

			for (int i = 0; i < tmp.Length; i++)
			{
				AllowFileExtName[i] = Utils.CutString(tmp[i],0, tmp[i].LastIndexOf(","));
				MaxSize[i] = Utils.StrToInt(Utils.CutString(tmp[i], tmp[i].LastIndexOf(",") + 1), 0);
			}
				
			int saveFileCount = 0;
			
			int fCount = HttpContext.Current.Request.Files.Count;

			for (int i = 0; i < fCount; i++)
			{
				if (!HttpContext.Current.Request.Files[i].FileName.Equals("") && HttpContext.Current.Request.Files.AllKeys[i].Equals(filekey))
				{
					saveFileCount++;
				}
			}

			AttachmentInfo[] attachmentinfo = new AttachmentInfo[saveFileCount];
			if (saveFileCount > MaxAllowFileCount)
				return attachmentinfo;

			saveFileCount = 0;

			Random random = new Random(unchecked((int)DateTime.Now.Ticks)); 


			for (int i = 0; i < fCount; i++)
			{
				if (!HttpContext.Current.Request.Files[i].FileName.Equals("") && HttpContext.Current.Request.Files.AllKeys[i].Equals(filekey))
				{
					string filename = Path.GetFileName(HttpContext.Current.Request.Files[i].FileName);
					string fileextname = Utils.CutString(filename, filename.LastIndexOf(".") + 1).ToLower();
					string filetype = HttpContext.Current.Request.Files[i].ContentType.ToLower();
					int filesize = HttpContext.Current.Request.Files[i].ContentLength;
					string newfilename = "";
						
					attachmentinfo[saveFileCount] = new AttachmentInfo();

					attachmentinfo[saveFileCount].Sys_noupload = "";

					// 判断 文件扩展名/文件大小/文件类型 是否符合要求
					if (!(Utils.IsImgFilename(filename) && !filetype.StartsWith("image")))
					{
						int extnameid = Utils.GetInArrayID(fileextname, AllowFileExtName);

						if (extnameid >= 0 && (filesize <= MaxSize[extnameid]) && (MaxFileSize >= filesize /*|| MaxAllSize == 0*/) && (MaxSizePerDay - TodayUploadedSize >= filesize))
						{
							TodayUploadedSize = TodayUploadedSize + filesize;
							string UploadDir = Utils.GetMapPath(BaseConfigs.GetForumPath + "upload/");
							StringBuilder savedir = new StringBuilder("");
							//附件保存方式 0=按年/月/日存入不同目录 1=按年/月/日/论坛存入不同目录 2=按论坛存入不同目录 3=按文件类型存入不同目录
							if (config.Attachsave == 1)
							{
								savedir.Append(DateTime.Now.ToString("yyyy"));
								savedir.Append(Path.DirectorySeparatorChar);
								savedir.Append(DateTime.Now.ToString("MM"));
								savedir.Append(Path.DirectorySeparatorChar);
								savedir.Append(DateTime.Now.ToString("dd"));
								savedir.Append(Path.DirectorySeparatorChar);
								savedir.Append(forumid.ToString());
								savedir.Append(Path.DirectorySeparatorChar);
							}	
							else if (config.Attachsave == 2)
							{
								savedir.Append(forumid);
								savedir.Append(Path.DirectorySeparatorChar);
							}
							else if (config.Attachsave == 3)
							{
								savedir.Append(fileextname);
								savedir.Append(Path.DirectorySeparatorChar);
							}
							else
							{
								savedir.Append(DateTime.Now.ToString("yyyy"));
								savedir.Append(Path.DirectorySeparatorChar);
								savedir.Append(DateTime.Now.ToString("MM"));
								savedir.Append(Path.DirectorySeparatorChar);
								savedir.Append(DateTime.Now.ToString("dd"));
								savedir.Append(Path.DirectorySeparatorChar);
							}
							// 如果指定目录不存在则建立
							if (!Directory.Exists(UploadDir + savedir.ToString()))
							{
								Utils.CreateDir(UploadDir + savedir.ToString());
							}
						
							newfilename = savedir.ToString() + (Environment.TickCount & int.MaxValue).ToString() +  i.ToString() + random.Next(1000,9999).ToString() + "." + fileextname;
						
						
							try
							{
								// 如果是bmp jpg png图片类型
								if ((fileextname == "bmp" || fileextname == "jpg" || fileextname == "jpeg" || fileextname == "png") && filetype.StartsWith("image"))
								{
														
									Image img = Image.FromStream(HttpContext.Current.Request.Files[i].InputStream);
									if (config.Attachimgmaxwidth > 0 && img.Width > config.Attachimgmaxwidth)
									{
										attachmentinfo[saveFileCount].Sys_noupload = "图片宽度为" + img.Width.ToString() + ", 系统允许的最大宽度为" + config.Attachimgmaxwidth.ToString();
	
									}
									if (config.Attachimgmaxheight > 0 && img.Height > config.Attachimgmaxheight)
									{
										attachmentinfo[saveFileCount].Sys_noupload = "图片高度为" + img.Width.ToString() + ", 系统允许的最大高度为" + config.Attachimgmaxheight.ToString();
									}									
									if (attachmentinfo[saveFileCount].Sys_noupload == "")
									{
										if (watermarkstatus == 0)
										{
											HttpContext.Current.Request.Files[i].SaveAs(UploadDir + newfilename);
											attachmentinfo[saveFileCount].Filesize = filesize;
										}
										else
										{
											if (config.Watermarktype == 1 && File.Exists(Utils.GetMapPath(BaseConfigs.GetForumPath + "watermark/" + config.Watermarkpic)))
											{
												AddImageSignPic(img, UploadDir + newfilename, Utils.GetMapPath(BaseConfigs.GetForumPath + "watermark/" + config.Watermarkpic), config.Watermarkstatus, config.Attachimgquality, config.Watermarktransparency);
											}
											else
											{
												string watermarkText;
												watermarkText = config.Watermarktext.Replace("{1}", config.Forumtitle);
												watermarkText = watermarkText.Replace("{2}", "http://" + DNTRequest.GetCurrentFullHost() + "/");
												watermarkText = watermarkText.Replace("{3}", Utils.GetDate());
												watermarkText = watermarkText.Replace("{4}", Utils.GetTime());
												AddImageSignText(img, UploadDir + newfilename, watermarkText, config.Watermarkstatus, config.Attachimgquality, config.Watermarkfontname, config.Watermarkfontsize);
											}
											// 获得加水印后的文件长度
											attachmentinfo[saveFileCount].Filesize = new FileInfo(UploadDir + newfilename).Length;
										}
									}
								}
								else
								{
									HttpContext.Current.Request.Files[i].SaveAs(UploadDir + newfilename);
									attachmentinfo[saveFileCount].Filesize = filesize;
								}
							}
							catch
							{
								if (!(Utils.FileExists(UploadDir + newfilename)))
								{
									HttpContext.Current.Request.Files[i].SaveAs(UploadDir + newfilename);
									attachmentinfo[saveFileCount].Filesize = filesize;
								}
							}
							
							try
							{
							    //加载文件预览类指定方法
                                IPreview preview = PreviewProvider.GetInstance(fileextname.Trim());
							    if (preview != null)
                                    preview.OnSaved(UploadDir + newfilename);
							}
							catch
							{}
						
						}
						else
						{
							if (extnameid < 0)
							{
								attachmentinfo[saveFileCount].Sys_noupload = "文件格式无效";
							}
							else if (MaxSizePerDay - TodayUploadedSize < filesize)
							{
								attachmentinfo[saveFileCount].Sys_noupload = "文件大于今天允许上传的字节数";
							}
							else if (filesize > MaxSize[extnameid])
							{
								attachmentinfo[saveFileCount].Sys_noupload = "文件大于该类型附件允许的字节数";
							}
							else
							{
								attachmentinfo[saveFileCount].Sys_noupload = "文件大于单个文件允许上传的字节数";
							}
						}
					}
					else
					{
						attachmentinfo[saveFileCount].Sys_noupload = "文件格式无效";
					}
					attachmentinfo[saveFileCount].Filename = newfilename;
					attachmentinfo[saveFileCount].Description = fileextname;
					attachmentinfo[saveFileCount].Filetype = filetype;
					attachmentinfo[saveFileCount].Attachment = filename;
					attachmentinfo[saveFileCount].Downloads = 0;
					attachmentinfo[saveFileCount].Postdatetime = DateTime.Now.ToString();
					attachmentinfo[saveFileCount].Sys_index = i;
					saveFileCount++;
				}
			}
			return attachmentinfo;
			
		}

		/// <summary>
		/// 返回访问过的论坛的列表html
		/// </summary>
		/// <param name="count">最大显示条数</param>
		/// <returns>列表html</returns>
		public static string GetVisitedForumsOptions(int count)
		{
			if (GetCookie("visitedforums").Trim() == "")
			{
				return "";
			}
			StringBuilder sb = new StringBuilder();
			string[] strfid = Utils.SplitString(GetCookie("visitedforums"), ",");
			for (int fidIndex = 0; fidIndex < strfid.Length; fidIndex++)
			{
                //ForumInfo foruminfo = Forums.GetForumInfo(Utils.StrToInt(strfid[fidIndex], 0));
                //if (foruminfo != null)
                //{
                //    sb.Append("<option value=\"");
                //    sb.Append(strfid[fidIndex]);
                //    sb.Append("\">");
                //    sb.Append(foruminfo.Name);
                //    sb.Append("</option>");
                //}
			}
			return sb.ToString();
		}


		/// <summary>
		/// 增加已访问版块id到历史记录cookie
		/// </summary>
		/// <param name="fid">要加入的版块id</param>
		public static void UpdateVisitedForumsOptions(int fid)
		{
			if (GetCookie("visitedforums").Trim() == "")
			{
				WriteCookie("visitedforums", fid.ToString());
			}
			else
			{
				bool fidExists = false;
				string[] strfid = Utils.SplitString(GetCookie("visitedforums"), ",");
				for (int fidIndex = 0; fidIndex < strfid.Length; fidIndex++)
				{
					if (strfid[fidIndex] == fid.ToString())
					{
						fidExists = true;
					}
				}
				if (!fidExists)
				{
					WriteCookie("visitedforums", fid.ToString() + "," + GetCookie("visitedforums"));
				}
			}
			return;
		}

        ///// <summary>
        ///// 替换原始字符串中的脏字词语
        ///// </summary>
        ///// <param name="text">原始字符串</param>
        ///// <returns>替换后的结果</returns>
        //public static string BanWordFilter(string text)
        //{
        //    StringBuilder sb = new StringBuilder(text);
        //    string[,] str = "";// Caches.GetBanWordList();
        //    int count = str.Length / 2;
        //    for (int i = 0; i < count; i++)
        //    {
        //        sb.Replace(str[i,0], str[i,1]);
        //    }
        //    return sb.ToString();
        //}

        ///// <summary>
        ///// 判断字符串是否包含脏字词语
        ///// </summary>
        ///// <param name="text">原始字符串</param>
        ///// <returns>如果包含则返回true, 否则反悔false</returns>
        //public static bool InBanWordArray(string text)
        //{
        //    string[,] str = "";// Caches.GetBanWordList();
        //    int count = str.Length / 2;
        //    for (int i = 0; i < count; i++)
        //    {
        //        if (text.IndexOf(str[i,0]) > -1)
        //        {
        //            return true;
        //        }
        //    }
        //    return false;
        //}

		/// <summary>
		/// 返回星星图片html
		/// </summary>
		/// <param name="starcount">星星总数</param>
		/// <param name="starthreshold">星星阀值</param>
		/// <returns>星星图片html</returns>
		public static string GetStarImg(int starcount, int starthreshold)
		{
			StringBuilder sb = new StringBuilder();
			int len = starcount / (starthreshold * starthreshold);
			for (int i = 0; i < len; i++)
			{
				sb.Append("<img src=\"star_level3.gif\" />");
			}
			starcount = starcount - len * starthreshold * starthreshold;

			len = starcount / starthreshold;
			for (int i = 0; i < len; i++)
			{
				sb.Append("<img src=\"star_level2.gif\" />");
			}
			starcount = starcount - len * starthreshold;

			len = starcount;
			for (int i = 0; i < len; i++)
			{
				sb.Append("<img src=\"star_level1.gif\" />");
			}

			return sb.ToString();
		}

		/// <summary>
		/// 返回当前页面是否是跨站提交
		/// </summary>
		/// <returns>当前页面是否是跨站提交</returns>
		public static bool IsCrossSitePost()
		{
		
			// 如果不是提交则为true
			if (!DNTRequest.IsPost())
			{
				return true;
			}
			return IsCrossSitePost(DNTRequest.GetUrlReferrer(), DNTRequest.GetHost());
		}
		
		/// <summary>
		/// 判断是否是跨站提交
		/// </summary>
		/// <param name="urlReferrer">上个页面地址</param>
		/// <param name="host">论坛url</param>
		/// <returns></returns>
		public static bool IsCrossSitePost(string urlReferrer, string host)
		{
			if (urlReferrer.Length < 7)
			{
				return true;
			}
			// 移除http://
//			string tmpReferrer = urlReferrer.Remove(0, 7);
//			if (tmpReferrer.IndexOf(":") > -1)
//				tmpReferrer = tmpReferrer.Substring(0, tmpReferrer.IndexOf(":"));
//			else
//				tmpReferrer = tmpReferrer.Substring(0, tmpReferrer.IndexOf('/'));

			Uri u = new Uri(urlReferrer);
			return u.Host != host;
		}

		/// <summary>
		/// 获得Assembly版本号
		/// </summary>
		/// <returns></returns>
		public static string GetAssemblyVersion()
		{
			Assembly myAssembly = Assembly.GetExecutingAssembly();
			FileVersionInfo myFileVersion = FileVersionInfo.GetVersionInfo(myAssembly.Location);
			return string.Format("{0}.{1}.{2}",myFileVersion.FileMajorPart, myFileVersion.FileMinorPart, myFileVersion.FileBuildPart);
		}

		/// <summary>
		/// 帖子中是否包含[hide]...[/hide]
		/// </summary>
		/// <param name="str">帖子内容</param>
		/// <returns>是否包含</returns>
		public static bool IsHidePost(string str)
		{
			return (str.IndexOf("[hide]") >= 0) && (str.IndexOf("[/hide]") > 0);
		}

		/// <summary>
		/// 返回显示魔法表情flash层的xhtml字符串
		/// </summary>
		/// <param name="magic">魔法表情id</param>
		/// <returns></returns>
		public static string ShowTopicMagic(int magic)
		{
			if (magic <= 0)
			{
				return "";
			}
			return "\r\n<!-- DNT Magic (ID:" + magic.ToString() + ") -->\r\n<object width=\"400\" height=\"300\" id=\"dntmagic\" classid=\"clsid:d27cdb6e-ae6d-11cf-96b8-444553540000\" codebase=\"http://fpdownload.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=7,0,0,0\" align=\"middle\" style=\"position:absolute;z-index:111;display:none;\"> \r\n<param name=\"allowScriptAccess\" value=\"sameDomain\" />\r\n<param name=\"movie\" value=\"magic/swf/" + magic.ToString() + ".swf\" />\r\n<param name=\"loop\" value=\"false\" />\r\n<param name=\"menu\" value=\"false\" />\r\n<param name=\"quality\" value=\"high\" />\r\n<param name=\"scale\" value=\"noscale\" />\r\n<param name=\"salign\" value=\"lt\" />\r\n<param name=\"wmode\" value=\"transparent\" /><param name=\"PLAY\" value=\"false\" /> \r\n<embed src=\"magic/swf/" + magic.ToString() + ".swf\" width=\"400\" height=\"300\" loop=\"false\" align=\"middle\" menu=\"false\" quality=\"high\" scale=\"noscale\" salign=\"lt\" wmode=\"transparent\" play=\"false\" allowScriptAccess=\"sameDomain\" type=\"application/x-shockwave-flash\" pluginspage=\"http://www.macromedia.com/go/getflashplayer\" /> \r\n</object>\r\n<script language=\"javascript\">\r\nfunction $(id){\r\n\treturn document.getElementById(id);\r\n}\r\nfunction playFlash(){\r\n\tdivElement = $('dntmagic');\r\n\tdivElement.style.display = '';\r\n\tdivElement.style.left = (document.documentElement.clientWidth /2 - parseInt(divElement.offsetWidth)/2)+\"px\";\r\n\tdivElement.style.top = (document.documentElement.clientHeight /2 - parseInt(divElement.offsetHeight)/2 )+\"px\";\r\n\tsetTimeout(\"hiddenFlash()\", 5000);\r\n}\r\n\r\nfunction hiddenFlash() {\r\n\t$('dntmagic').style.display = 'none';\r\n}\r\nplayFlash();\r\n</script>\r\n<!-- DNT Magic End -->\r\n";
		}

        private static RegexOptions options = RegexOptions.IgnoreCase;

        public static Regex[] r = new Regex[4];

        static WebUtils()
        {
            r[0] = new Regex(@"(\r\n)", options);
            r[1] = new Regex(@"(\n)", options);
            r[2] = new Regex(@"(\r)", options);
            r[3] = new Regex(@"(<br />)", options);
        }

		/// <summary>
		/// 给帖子内容加上干扰码
		/// </summary>
		/// <param name="message">帖子内容</param>
		/// <returns>加入干扰码后的帖子内容</returns>
		public static string AddJammer(string message)
		{
            //Match m;
            //string jammer = Caches.GetJammer();

            //m = r[0].Match(message);
            //if (m.Success)
            //{
            //    message = message.Replace(m.Groups[0].ToString(), jammer + "\r\n");
            //}

            //m = r[1].Match(message);
            //if (m.Success)
            //{
            //    message = message.Replace(m.Groups[0].ToString(), jammer + "\n");
            //}
   
            //m = r[2].Match(message);
            //if (m.Success)
            //{
            //    message = message.Replace(m.Groups[0].ToString(), jammer + "\r");
            //}

            //m = r[3].Match(message);
            //if (m.Success)
            //{
            //    message = message.Replace(m.Groups[0].ToString(), jammer + "<br />");
            //}

            return message;// +jammer;

		}


		/// <summary>
		/// 是否是过滤的用户名
		/// </summary>
		/// <param name="str"></param>
		/// <param name="stringarray"></param>
		/// <returns></returns>
		public static bool IsBanUsername(string str, string stringarray)
		{
			if (stringarray == null || stringarray == "") 
				return false;

			stringarray = Regex.Escape(stringarray).Replace(@"\*",@"[\w-]*");
			Regex r;
			foreach (string strarray in Utils.SplitString(stringarray.ToLower(), "\\n"))
			{
                r = new Regex(string.Format("^{0}$", strarray));
				if (r.IsMatch(str) && (!strarray.Trim().Equals("")))
				{
					return true;
				}
			}
			return false;

			//return Utils.IsCompriseStr(str, stringarray, "\n");
		}



		/// <summary>
		/// 从cookie中获取转向页
		/// </summary>
		/// <returns></returns>
		public static string GetReUrl()
		{
			if (DNTRequest.GetString("reurl").Trim() != "")
			{
				Utils.WriteCookie("reurl", DNTRequest.GetString("reurl").Trim());
				return DNTRequest.GetString("reurl").Trim();
			}
			else
			{
				if (Utils.GetCookie("reurl") == "")
				{
					return "index.aspx";
				}
				else
				{
					return Utils.GetCookie("reurl");
				}
			}
		}

		/// <summary>
		/// 是否为有效域
		/// </summary>
		/// <param name="host">域名</param>
		/// <returns></returns>
		public static bool IsValidDomain(string host)
		{
			Regex r = new Regex(@"^\d+$");
			if (host.IndexOf(".") == -1)
			{
				return false;
			}
			return r.IsMatch(host.Replace(".", string.Empty)) ? false : true;
		}


		/// <summary>
		/// 更新路径url串中的扩展名
		/// </summary>
		/// <param name="pathlist">路径url串</param>
		/// <param name="extname">扩展名</param>
		/// <returns></returns>
		public static string UpdatePathListExtname(string pathlist, string extname)
		{
			return pathlist.Replace("{extname}", extname);
		}

        public static void CreateTextImage(string filename, string watermarkText, int quality, string fontname, int fontsize, Color fontcolor)
		{
            Font drawFont = new Font(fontname, fontsize, FontStyle.Regular, GraphicsUnit.Pixel);
            
		    Bitmap bmp = new Bitmap(100, 50);
            Graphics g = Graphics.FromImage(bmp);
            SizeF crSize;
            crSize = g.MeasureString(watermarkText, drawFont);
            bmp = new Bitmap((int)crSize.Width, (int)crSize.Height);
            
            g = Graphics.FromImage(bmp);
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.CompositingQuality = CompositingQuality.HighQuality;
            g.Clear(Color.Transparent);
            g.FillRectangle(new SolidBrush(Color.Transparent), 0, 0, crSize.Width, crSize.Height);
           
            float xpos = 0;
            float ypos = 0;


            //g.DrawString(watermarkText, drawFont, new SolidBrush(Color.White), xpos + 1, ypos + 1);
            g.DrawString(watermarkText, drawFont, new SolidBrush(fontcolor), xpos, ypos);

            bmp.Save(filename, ImageFormat.Png);
            g.Dispose();
            //bmp.Dispose();
            bmp.Dispose();
        }
		
		
	}// end class
}
