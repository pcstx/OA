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
	/// getImage 的摘要说明。
	/// </summary>
	public class getImage : System.Web.UI.Page
	{
		private void Page_Load(object sender, System.EventArgs e)
		{
			// 在此处放置用户代码以初始化页面
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
				
					string strConn=ConfigurationSettings.AppSettings["eprintsample"];//oleDb的通用连接
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

		#region Web 窗体设计器生成的代码
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: 该调用是 ASP.NET Web 窗体设计器所必需的。
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// 设计器支持所需的方法 - 不要使用代码编辑器修改
		/// 此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{    
			this.Load += new System.EventHandler(this.Page_Load);
		}
		#endregion
	}
}
