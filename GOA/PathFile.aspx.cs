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
	/// PathFile ��ժҪ˵����
	/// </summary>
	public class PathFile : System.Web.UI.Page
	{
		private void Page_Load(object sender, System.EventArgs e)
		{
			// �ڴ˴������û������Գ�ʼ��ҳ��
			try
			{
				//��ֹ�ⲿ�����ύ�Ľű�				
				string sHttp=Request.Headers["referer"];
				string sServer=Request.Headers["host"]; 
				if (sServer.Equals(sHttp.Substring(7,sServer.Length))==false)
				{
					Console.WriteLine("���ύ��·�����󣬽�ֹ��վ���ⲿ�ύ����! "+sHttp);
					return ;
				}

				//�ύ������
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
