using System;
using System.Collections;
using System.ComponentModel;
//using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using eprintdll;
using System.IO;


namespace eprint
{
	/// <summary>
	/// PathFile 的摘要说明。
	/// </summary>
	public class PathFile : System.Web.UI.Page
	{
		private void Page_Load(object sender, System.EventArgs e)
		{
			// 在此处放置用户代码以初始化页面
			try
			{
				//防止外部数据提交的脚本				
				string sHttp=Request.Headers["referer"];
				string sServer=Request.Headers["host"]; 
				if (sServer.Equals(sHttp.Substring(7,sServer.Length))==false)
				{
					Console.WriteLine("你提交的路径有误，禁止从站点外部提交数据! "+sHttp);
					return ;
				}

				//提交的名称
				//string skey = Request.Params["key"];
				string skey= Request.QueryString.ToString();
//				string sfilename = Request.Params["filename"];

//				string designFilePath = Request.Params["designpath"];
			
				
				
				StreamReader sr=new StreamReader(Request.InputStream);
				string strXml =sr.ReadToEnd();

				eprintdll.PathFile pathfile = new eprintdll.PathFile(null,strXml,Request.PhysicalApplicationPath);

				string strReturn = pathfile.returnXml(skey); 
				
				Response.Write(strReturn);
			}
			catch (Exception ex)
			{
				Response.Write(ex.Message);
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
