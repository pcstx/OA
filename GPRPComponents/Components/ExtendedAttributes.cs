//------------------------------------------------------------------------------
// <copyright company="Telligent Systems">
//     Copyright (c) Telligent Systems Corporation.  All rights reserved.
// </copyright> 
//------------------------------------------------------------------------------

using System;
using System.Collections.Specialized;

namespace GPRP.GPRPComponents
{

	/// <summary>
	/// Provides standard implementation for simple extendent data storage
	/// </summary>
	[Serializable]
	public class ExtendedAttributes
	{
		public ExtendedAttributes()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		NameValueCollection	extendedAttributes = new NameValueCollection();

		public string GetExtendedAttribute(string name)	
		{
			string returnValue = extendedAttributes[name];

			if (returnValue	== null)
				return string.Empty;
			else
				return returnValue;
		}

		public void SetExtendedAttribute(string	name, string value)	
		{

			if((value == null) || (value == string.Empty))
				extendedAttributes.Remove(name);
			else
				extendedAttributes[name] = value;

		}

		public int ExtendedAttributesCount
		{
			get { return extendedAttributes.Count; }
		}

        protected bool GetBool(string name, bool defaultValue)
        {
            string b = GetExtendedAttribute(name);
            if(b == null || b.Trim().Length == 0)
                return defaultValue;

            return bool.Parse(b);
        }

        protected int GetInt(string name, int defaultValue)
        {
            string i = GetExtendedAttribute(name);
            if(i == null || i.Trim().Length == 0)
                return defaultValue;

            return Int32.Parse(i);
        }

        protected string GetString(string name, string defaultValue)
        {
            string v = GetExtendedAttribute(name);
            return (Globals.IsNullorEmpty(v)) ? defaultValue : v;
        }

		#region Serialization

        public SerializerData GetSerializerData()
        {
            SerializerData data = new SerializerData();
            //data.Bytes = Serializer.ConvertToBytes(this.extendedAttributes);
            
            string keys = null;
            string values = null;

            Serializer.ConvertFromNameValueCollection(this.extendedAttributes,ref keys, ref values);
            data.Keys = keys;
            data.Values = values;

            return data;
        }

        public void SetSerializerData(SerializerData data)
        {
//            if(data.Bytes != null)
//            {
//                try
//                {
//                    extendedAttributes = Serializer.ConvertToObject(data.Bytes) as NameValueCollection;
//                }
//                catch{}
//            }

            if(this.extendedAttributes == null || this.extendedAttributes.Count == 0)
            {
                this.extendedAttributes = Serializer.ConvertToNameValueCollection(data.Keys,data.Values);
            }

            if(this.extendedAttributes == null)
                extendedAttributes = new NameValueCollection();
        }
		#endregion
	}
}
