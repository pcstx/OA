using System;
using System.IO;
using System.Xml.Serialization;

namespace GPRP.GPRPComponents
{
	/// <summary>
	/// SerializationHelper ��ժҪ˵����
	/// </summary>
	public class SerializationHelper
	{
		private SerializationHelper()
		{
		}

		/// <summary>
		/// �����л�
		/// </summary>
		/// <param name="type">��������</param>
		/// <param name="filename">�ļ�·��</param>
		/// <returns></returns>
		public static object Load(Type type, string filename)
		{
			FileStream fs = null;
			try
			{
				// open the stream...
				fs = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
				XmlSerializer serializer = new XmlSerializer(type);
				return serializer.Deserialize(fs);
			}
			catch(Exception ex)
			{
				throw ex;
			}
			finally
			{
				if(fs != null)
					fs.Close();
			}
		}


		/// <summary>
		/// ���л�
		/// </summary>
		/// <param name="obj">����</param>
		/// <param name="filename">�ļ�·��</param>
		public static void Save(object obj, string filename)
		{
			FileStream fs = null;
			// serialize it...
			try
			{
				fs = new FileStream(filename, FileMode.Create, FileAccess.Write, FileShare.ReadWrite);
				XmlSerializer serializer = new XmlSerializer(obj.GetType());
				serializer.Serialize(fs, obj);	
			}
			catch(Exception ex)
			{
				throw ex;
			}
			finally
			{
				if(fs != null)
					fs.Close();
			}

		}
	}
}
