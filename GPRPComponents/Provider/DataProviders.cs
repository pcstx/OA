//------------------------------------------------------------------------------
// <copyright company="Telligent Systems">
//     Copyright (c) Telligent Systems Corporation.  All rights reserved.
// </copyright> 
//------------------------------------------------------------------------------

using System;
using System.Configuration;
using System.IO;
using System.Reflection;
using System.Web;
using GPRP.GPRPEnumerations;

namespace GPRP.GPRPComponents
{
	/// <summary>
	/// DataProviders is responible for loading and managing the various CS DataProviders
	/// </summary>
	public sealed class DataProviders
	{
        /// <summary>
        /// This class can not be instantiated
        /// </summary>
		private DataProviders()
		{
		}

        private static void GetDataStoreParameters(Provider dataProvider, out string connectionString, out string databaseOwner)
        {
            databaseOwner = dataProvider.Attributes["databaseOwner"];
            if(databaseOwner == null || databaseOwner.Trim().Length == 0)
                databaseOwner = ConfigurationSettings.AppSettings[dataProvider.Attributes["databaseOwnerStringName"]];

            connectionString = dataProvider.Attributes["connectionString"];
            if(connectionString == null || connectionString.Trim().Length == 0)
                connectionString = ConfigurationSettings.AppSettings[dataProvider.Attributes["connectionStringName"]];
        }

        /// <summary>
        /// Creates an instance of the provider using Activator. This instance should be
        /// cached since it is an expesivie operation
        /// </summary>
        public static object CreateInstance(Provider dataProvider)
        {
            //Find the current attributes
            string connectionString = null; //dataProvider.Attributes["connectionString"];
            string databaseOwner = null;// dataProvider.Attributes["databaseOwner"];

            GetDataStoreParameters(dataProvider, out connectionString, out databaseOwner);

            //Get the type
            Type type  = Type.GetType(dataProvider.Type);

            object newObject = null;
            if(type != null)
            {
                newObject =  Activator.CreateInstance(type,new object[]{databaseOwner,connectionString});  
            }
            
            if(newObject == null) //If we can not create an instance, throw an exception
                ProviderException(dataProvider.Name);

            return newObject;
        }

        /// <summary>
        /// Creates and Caches the ConstructorInfo for the specified provider. 
        /// </summary>
        public static ConstructorInfo CreateConstructorInfo (Provider dataProvider) 
        {

            // The assembly should be in \bin or GAC, so we simply need
            // to get an instance of the type
            //
            CSConfiguration config = CSConfiguration.GetConfig();
            ConstructorInfo providerCnstr = null;
            try 
            {
                //string providerTypeName = ((Provider) config.Providers[providerName]).Type;
                Type type  = Type.GetType( dataProvider.Type );

                // Insert the type into the cache
                //
                Type[] paramTypes = new Type[2];
                paramTypes[0] = typeof(string);
                paramTypes[1] = typeof(string);

                providerCnstr = type.GetConstructor(paramTypes);

            } 
            catch 
            {
                ProviderException(dataProvider.Name);
            }

           if(providerCnstr == null)
               ProviderException(dataProvider.Name);

            return providerCnstr;
        }

        /// <summary>
        /// Creates an instance of the specified provider using the Cached
        /// ConstructorInfo from CreateConstructorInfo
        /// </summary>
        public static object Invoke(Provider dataProvider)
        {
            object[] paramArray = new object[2];

            
            string dbOwner = null; 
            string connstring = null;

            GetDataStoreParameters(dataProvider, out connstring, out dbOwner);

            paramArray[0] = dbOwner;
            paramArray[1] = connstring;

            return CreateConstructorInfo(dataProvider).Invoke(paramArray);
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
