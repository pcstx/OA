//------------------------------------------------------------------------------
// <copyright company="Telligent Systems">
//     Copyright (c) Telligent Systems Corporation.  All rights reserved.
// </copyright> 
//------------------------------------------------------------------------------

using System;
using System.Reflection;


namespace GPRP.GPRPComponents
{
	public abstract class ExtensionModule
	{
		public abstract void Init (Provider provider);
		public abstract void ProcessRequest();

		public static ExtensionModule Instance (Provider provider)
		{
			try
			{
				if(provider == null)
					return null;

				// Use the cache because the reflection used later is expensive
				//
				string cacheKey = "Module-" + provider.Name;
				ConstructorInfo constructor = CSCache.Get(cacheKey) as ConstructorInfo;
				ExtensionModule module = null;

				// Is the module already cached?
				//
				if(constructor == null)
				{
					Type type = Type.GetType( provider.Type );

					if(type == null)
						return null;

					constructor = type.GetConstructor(Type.EmptyTypes);

					// Insert the type into the cache
					//
					CSCache.Max(cacheKey, constructor); //Did not have a cache time specified
				} 
            
				module = (ExtensionModule)constructor.Invoke(null);

				// Initialize the module for the request
				//
				module.Init(provider);

				return module;
			}
			catch
			{
				return null;
			}
		}
	}
}
