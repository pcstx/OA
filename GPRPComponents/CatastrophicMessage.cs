//------------------------------------------------------------------------------
// <copyright company="Telligent Systems">
//     Copyright (c) Telligent Systems Corporation.  All rights reserved.
// </copyright> 
//------------------------------------------------------------------------------

using System;
using System.IO;
using System.Web;
using GPRP.GPRPEnumerations;

namespace GPRP.GPRPComponents
{
	/// <summary>
	/// Summary description for CatastrophicMessage.
	/// </summary>
	public class CatastrophicMessage
	{
		private CatastrophicMessage()
		{
		
		}

        public static void Write(HttpContext context, CSException csException, string filter, string errorFile)
        {
            CSConfiguration csConfig = CSConfiguration.GetConfig();
            string defaultLanguage = csConfig.DefaultLanguage;

            string path = "~/Languages/{0}/errors/{1}";
            StreamReader reader = new StreamReader( context.Server.MapPath(string.Format(path,defaultLanguage,errorFile)) );
            string html = reader.ReadToEnd();
            reader.Close();

            if(filter != null || filter.Trim().Length > 0)
            html = html.Replace(filter, csException.Message);
            
            context.Response.Write(html);
            context.Response.End();
        }
	}
}
