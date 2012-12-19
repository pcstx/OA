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
	/// Provides a common hook into SiteUrls for each application
	/// </summary>
	public abstract class ApplicationUrls
	{
		public ApplicationUrls()
		{
			
		}

		/// <summary>
		/// Creates an instance of the provider using Activator. This instance should be
		/// cached since it is an expesivie operation
		/// </summary>
		public static object CreateInstance(Provider dataProvider)
		{
			return CreateInstance(dataProvider, null);
		}

		/// <summary>
		/// Creates an instance of the provider using Activator. This instance should be
		/// cached since it is an expesivie operation
		/// </summary>
		public static object CreateInstance(Provider dataProvider, params object[] args )
		{
			//Get the type
			Type type  = Type.GetType(dataProvider.Type);

			object newObject = null;
			if(type != null)
			{
				if(args != null)
					newObject = Activator.CreateInstance(type, args);
				else
					newObject = Activator.CreateInstance(type);
			}
            
			if(newObject == null) //If we can not create an instance, throw an exception
				ProviderException(dataProvider.Name);

			return newObject;
		}

        protected string FormatUrl(string name)
        {
            return Globals.GetSiteUrls().UrlData.FormatUrl(name);
        }

        protected string FormatUrl(string name, params object[] parameters)
        {
            return Globals.GetSiteUrls().UrlData.FormatUrl(name, parameters);
        }

		#region Exception
		private static void ProviderException(string providerName)
		{
			CSConfiguration config = CSConfiguration.GetConfig();
			HttpContext context = HttpContext.Current;
			if (context != null) 
			{
                    
				// We can't load the dataprovider
				//
				StreamReader reader = new StreamReader( context.Server.MapPath("~/Languages/" + config.DefaultLanguage + "/errors/DataProvider.htm") );
				string html = reader.ReadToEnd();
				reader.Close();

				html = html.Replace("[DATAPROVIDERCLASS]", providerName);
				html = html.Replace("[DATAPROVIDERASSEMBLY]", providerName);
				context.Response.Write(html);
				context.Response.End();
			} 
			else 
			{
				throw new CSException(CSExceptionType.DataProvider, "Unable to load " + providerName);
			}
		}
		#endregion
	}
}
