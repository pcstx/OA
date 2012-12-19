//------------------------------------------------------------------------------
// <copyright company="Telligent Systems">
//     Copyright (c) Telligent Systems Corporation.  All rights reserved.
// </copyright> 
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Text;

namespace GPRP.GPRPComponents {


    public class StringTransforms {

        // *********************************************************************
        //  ToDelimitedString
        //
        /// <summary>
        /// Private helper function to convert a collection to delimited string array
        /// </summary>
        /// 
        // ********************************************************************/
        public static string ToDelimitedString(ICollection collection, string delimiter) {

            StringBuilder delimitedString = new StringBuilder();

            // Hashtable is perfomed on Keys
            //
            if (collection is Hashtable) {

                foreach (object o in ((Hashtable) collection).Keys) {
                    delimitedString.Append( o.ToString() + delimiter);
                }
            }

            // ArrayList is performed on contained item
            //
            if (collection is ArrayList) {
                foreach (object o in (ArrayList) collection) {
                    delimitedString.Append( o.ToString() + delimiter);
                }
            }

            // String Array is performed on value
            //
            if (collection is String[]) {
                foreach (string s in (String[]) collection) {
                    delimitedString.Append( s + delimiter);
                }
            }

            return delimitedString.ToString().TrimEnd(Convert.ToChar(delimiter));
        }

						
		/// <summary>
		/// Unicode×ªÒåÐòÁÐ
		/// </summary>
		/// <param name="rawText"></param>
		/// <returns></returns>
		/// <author>±¦Óñ</author>
		public static string UnicodeEncode(string rawText)
		{
			if (rawText == null || rawText == string.Empty)
				return rawText;
			string text = "";
			foreach(int c in rawText)
			{
				string t = "";
				if (c > 126)
				{
					text += "\\u";
					t = c.ToString("x");
					for (int x = 0; x < 4 - t.Length; x++)
					{
						text += "0";
					}
				}
				else
				{
					t = ((char)c).ToString();
				}
				text += t;
			}
			
			return text;
		}

    }
}
