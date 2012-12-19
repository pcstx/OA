using System;

namespace GPRP.Entity
{
	/// <summary>
	/// ShowtopicPageAttachmentInfo ��ժҪ˵����
	/// </summary>
	public class ShowtopicPageAttachmentInfo : AttachmentInfo
	{
		private int m_getattachperm; //���ظ���Ȩ��
		private int m_attachimgpost; //�����Ƿ�ΪͼƬ
		private int m_allowread; //�����Ƿ�������ȡ
		private string m_preview = string.Empty; //Ԥ����Ϣ
		
		/// <summary>
		/// ���ظ���Ȩ��
		/// </summary>
		public int Getattachperm
		{
			get { return m_getattachperm;}
			set { m_getattachperm = value;}
		}

		/// <summary>
		/// �����Ƿ�ΪͼƬ
		/// </summary>
		public int Attachimgpost
		{
			get { return m_attachimgpost;}
			set { m_attachimgpost = value;}
		}

		/// <summary>
		/// �����Ƿ�������ȡ
		/// </summary>
		public int Allowread
		{
			get { return m_allowread;}
			set { m_allowread = value;}
		}
		
		/// <summary>
		/// Ԥ����Ϣ
		/// </summary>
		public string Preview
		{
		    get { return m_preview; }
		    set { m_preview = value; }
		}

	}
}