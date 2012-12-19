//------------------------------------------------------------------------------
// <copyright company="Telligent Systems">
//     Copyright (c) Telligent Systems Corporation.  All rights reserved.
// </copyright> 
//------------------------------------------------------------------------------

// 修改说明：增加下载次数和真实文件名属性 
// 修改人：宝玉
// 修改日期：2005-03-14

using System;
using System.IO;
using System.Web;   
using System.Security;


namespace GPRP.GPRPComponents {
    /// <summary>
    /// Summary description for PostAttachment.
    /// </summary>
    public class PostAttachment {
        int length = 0;
        int userID = 0;
        int postID = 0;
        int forumID =0;
        DateTime created;
        string contentType;
        string fileName;
        byte[] content = new byte[0]; //修改，让其有默认值

		// 以下为新增属性
		Guid attachmentID = Guid.Empty; // 附件的ID
		int downloadCount = 0;	//　下载次数
		string realFileName;	// 真实文件名——保存在磁盘中的文件名

        public PostAttachment() 
		{
        }
		public PostAttachment (HttpPostedFile postedFile) 
		{
			DoPostAttachment(postedFile, Users.GetUser().UserID);  
		}

		public PostAttachment (HttpPostedFile postedFile, int userID) 
		{
			DoPostAttachment(postedFile, userID);  
		}


		private void DoPostAttachment(HttpPostedFile postedFile, int userID)
		{
			attachmentID = Guid.NewGuid();
			length = postedFile.ContentLength;
			contentType = postedFile.ContentType;

			// Get the filename
			//
			fileName = postedFile.FileName.Substring(postedFile.FileName.LastIndexOf("\\") + 1);
			
			// 将文件改名存储，不保留扩展名
			realFileName =  System.Guid.NewGuid().ToString();

			// Setup the byte array
			//
			content = new Byte[length];
			// Read in the attachment into a byte array
			//
			postedFile.InputStream.Read(content, 0, length);

		}

		public static string GetAttachmentPath(int userID)
		{
			string path = Path.Combine(CSConfiguration.GetConfig().AttachmentsPath, userID.ToString());
			return HttpContext.Current.Server.MapPath( path );
		}

		public static string GetAttachmentPath(int userID, string filename)
		{
			return Path.Combine(GetAttachmentPath(userID) , filename);
		}

		/// <summary>
		/// 扩展名
		/// </summary>
		public string Extension
		{
			get 
			{
				return Path.GetExtension(fileName);
			}
		}

		/// <summary>
		/// 唯一标志附件的ID
		/// </summary>
		public Guid AttachmentID
		{
			get 
			{
				return attachmentID;
			}
			set 
			{
				attachmentID = value;
			}
		}

		public int Length 
		{
			get 
			{
				return length;
			}
			set 
			{
				length = value;
			}
		}

		public string ContentType 
		{
			get 
			{
				return contentType;
			}
			set 
			{
				contentType = value;
			}
		}

		public string FileName 
		{
			get 
			{
				return fileName;
			}
			set 
			{
				fileName = value;
			}

		}

		/// <summary>
		/// 新增: 真实文件名，表示保存在磁盘中的文件名
		/// </summary>
		/// <remarks>如果保存在磁盘中，则文件上传后改名保存，新名称一般为一个Guid类型</remarks>
		public string RealFileName
		{
			get 
			{
				return realFileName;
			}
			set 
			{
				realFileName = value;
			}
		}

		public Byte[] Content 
		{
			get 
			{
				return content;
			}
			set 
			{
				content = value;
			}
		}

		public int PostID 
		{
			get { return postID; }
			set { postID = value; }
		}

		public int ForumID 
		{
			get { return forumID; }
			set { forumID = value; }
		}

		public int UserID 
		{
			get { return userID; }
			set { userID = value; }
		}

		/// <summary>
		/// 新增: 下载次数
		/// </summary>
		public int DownloadCount
		{
			get { return downloadCount;}
			set { downloadCount = value;}
		}

		public DateTime DateCreated 
		{
			get 
			{
				return created;
			}
			set 
			{
				created = value;
			}
		}

    }
}
