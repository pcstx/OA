//------------------------------------------------------------------------------
// <copyright company="Telligent Systems">
//     Copyright (c) Telligent Systems Corporation.  All rights reserved.
// </copyright> 
//------------------------------------------------------------------------------

using System;
using GPRP.GPRPEnumerations;
namespace GPRP.GPRPComponents
{
	/// <summary>
	/// Summary description for CSLink.
	/// </summary>
	public class CSLink
	{

		public CSLink(string name, string resourceName, string text, string navigateUrl, string roles)
		{
			_name = name;
			_resourceName = resourceName;
			_text = text;
			_navigateUrl = navigateUrl;
			_roles = roles;
		}

		private string _name = null;
		private string _resourceName = null;
		private string _text = null;
		private string _navigateUrl = null;
		private string _roles = null;

		public string Text
		{
			get
			{
				if(_resourceName != null)
					return ResourceManager.GetString(_resourceName);
				else
					return _text;
			}
		}

		public string ResourceName
		{
			get
			{
				return _resourceName;
			}
		}

		public string NavigateUrl
		{
			get
			{
				return _navigateUrl;
			}
		}

		public string Name
		{
			get
			{
				return _name;
			}
		}

		public string Roles
		{
			get
			{
				return _roles;
			}
		}
	}
}
