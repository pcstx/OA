using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Configuration;
using System.Data.OleDb;
using System.IO;

namespace eprint
{
	/// <summary>
	/// getImage ��ժҪ˵����
	/// </summary>
	public class getImage : System.Web.UI.Page
	{
		private void Page_Load(object sender, System.EventArgs e)
		{
			// �ڴ˴������û������Գ�ʼ��ҳ��
/*			FileStream MyFileStream;
			long FileSize;
 
			MyFileStream = new FileStream("c:/photo/100_0018.JPG", FileMode.Open);
			FileSize = MyFileStream.Length;
      
			byte[] Buffer = new byte[(int)FileSize];
			MyFileStream.Read(Buffer, 0, (int)FileSize);
			MyFileStream.Close();
 
			//Response.Write("<b>File Contents: </b>");
			Response.BinaryWrite(Buffer);
*/
			string skey = Request.Params["key"];
			if (skey.Equals("readimage"))
			{
				Response.ContentType = "image/JPEG";
				Response.AppendHeader("Pragma","No-cache");
				Response.AppendHeader("Expires","0");
				Response.AppendHeader("Cache-control","no-cache");
				Response.AppendHeader("Refresh","10");

				

				string sTablename=Request.Params["sTablename"];
				string sImgname=Request.Params["sImgname"];
				string sKeyname=Request.Params["sKeyname"];
				string sKeyvalue=Request.Params["sKeyvalue"];
				
				try 
				{
				
					string strConn=ConfigurationSettings.AppSettings["eprintsample"];//oleDb��ͨ������
					OleDbConnection con = new OleDbConnection(strConn);
					con.Open();

					string sSql="select "+sImgname+" from "+sTablename+" where "+sKeyname+"='"+sKeyvalue+"'";
					OleDbCommand myComm = new OleDbCommand(sSql);
					myComm.Connection=con;
					OleDbDataReader ds = myComm.ExecuteReader();

					if (ds.Read()) 
					{
						byte[] buf = (byte[])ds[sImgname];
						if(buf!=null)
						{						
							Response.BinaryWrite(buf);
							
						}
					}
					ds.Close();
					myComm.Dispose();
					con.Close();

				

				} 
				catch (Exception e1)
				{
				}



			}
		}

		#region Web ������������ɵĴ���
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: �õ����� ASP.NET Web ���������������ġ�
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// �����֧������ķ��� - ��Ҫʹ�ô���༭���޸�
		/// �˷��������ݡ�
		/// </summary>
		private void InitializeComponent()
		{    
			this.Load += new System.EventHandler(this.Page_Load);
		}
		#endregion
	}
}
