//------------------------------------------------------------------------------
// <copyright company="Telligent Systems">
//     Copyright (c) Telligent Systems Corporation.  All rights reserved.
// </copyright> 
//------------------------------------------------------------------------------

using System;
using System.Web;
using GPRP.GPRPEnumerations;
namespace GPRP.GPRPComponents
{
	/// <summary>
	/// Summary description for CSExceptionModule.
	/// </summary>
	public class CSExceptionModule : ICSModule
	{
		public CSExceptionModule()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		#region ICSModule Members

		public void Init(CSApplication csa, System.Xml.XmlNode node)
		{
			csa.CSException +=new CSExceptionHandler(csa_CSException);
		}

		#endregion

		private void csa_CSException(CSException csEx, CSEventArgs e)
		{
			CSContext csContext = CSContext.Current;

			if (csEx.ExceptionType != CSExceptionType.UnknownError && csContext.IsWebRequest) 
			{
				RedirectToMessage(csContext.Context, csEx);
			} 
		}

		private static void RedirectToMessage (HttpContext context, CSException exception) 
		{

			if ((exception.InnerException != null) && ( exception.InnerException is CSException)) 
			{
				CSException inner = (CSException) exception.InnerException;
			}
			context.Response.Redirect(Globals.GetSiteUrls().Message( exception.ExceptionType ), true);
		}

	}
}
