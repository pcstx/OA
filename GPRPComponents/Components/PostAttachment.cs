//------------------------------------------------------------------------------
// <copyright company="Telligent Systems">
//     Copyright (c) Telligent Systems Corporation.  All rights reserved.
// </copyright> 
//------------------------------------------------------------------------------

// �޸�˵�����������ش�������ʵ�ļ������� 
// �޸��ˣ�����
// �޸����ڣ�2005-03-14

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
        byte[] content = new byte[0]; //�޸ģ�������Ĭ��ֵ

		// ����Ϊ��������
		Guid attachmentID = Guid.Empty; // ������ID
		int downloadCount = 0;	//�����ش���
		string realFileName;	// ��ʵ�ļ������������ڴ����е��ļ���

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
			
			// ���ļ������洢����������չ��
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
		/// ��չ��
		/// </summary>
		public string Extension
		{
			get 
			{
				return Path.GetExtension(fileName);
			}
		}

		/// <summary>
		/// Ψһ��־������ID
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
		/// ����: ��ʵ�ļ�������ʾ�����ڴ����е��ļ���
		/// </summary>
		/// <remarks>��������ڴ����У����ļ��ϴ���������棬������һ��Ϊһ��Guid����</remarks>
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
		/// ����: ���ش���
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
