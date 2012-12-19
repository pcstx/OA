//------------------------------------------------------------------------------
// <copyright company="Telligent Systems">
//     Copyright (c) Telligent Systems Corporation.  All rights reserved.
// </copyright> 
//------------------------------------------------------------------------------

using System;
using System.Collections.Specialized;
using System.Text.RegularExpressions;

namespace GPRP.GPRPComponents
{
	/// <summary>
	/// Summary description for EmailFormatList.
	/// </summary>
	public class FormatList
	{
		public FormatList()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		//Internal collection
		NameValueCollection nvc = new NameValueCollection();

		/// <summary>
		/// Adds a new Formatting to the internal collection
		/// </summary>
		/// <param name="name"></param>
		/// <param name="value"></param>
		public void Add(string name, string value)
		{
			nvc.Add(FormatName(name),value);
		}

		/// <summary>
		/// Helper method used to apply regex formatting to our name value
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		private static string FormatName(string name)
		{
			return string.Format(@"\[{0}\]", name);
		}

		/// <summary>
		/// Iterates over the internal collection and applies the new formattings
		/// </summary>
		/// <param name="text"></param>
		/// <returns></returns>
		public string Format(string text)
		{
			if(nvc.Count == 0)
				return text;

			foreach(string key in nvc.AllKeys)
			{
                string value = nvc[key];
                if(value != null)
			        text = Regex.Replace(text, key, value , RegexOptions.IgnoreCase | RegexOptions.Compiled);
			}

			return text;
		}

		public static string Format(string input, string name, string text)
		{
			return Regex.Replace(input, FormatName(name), text , RegexOptions.IgnoreCase | RegexOptions.Compiled);
		}


	}
}
