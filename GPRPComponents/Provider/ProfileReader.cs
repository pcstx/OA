//------------------------------------------------------------------------------
// <copyright company="Telligent Systems">
//     Copyright (c) Telligent Systems Corporation.  All rights reserved.
// </copyright> 
//------------------------------------------------------------------------------

using System;
using System.Globalization;
using Microsoft.ScalableHosting.Profile;

namespace GPRP.GPRPComponents
{
	/// <summary>
	/// Provides read only access to a users profile data. This class can be used to create instances of the 
	/// SettingsPropertyValueCollection without the need to create a full instance of ProfileBase. It's primary usable will
	/// to return multiple instances of user/profile data.
	/// </summary>
	public class ProfileReader
	{
        /// <summary>
        /// To instantiate a new instance of ProfileReader, you must supply an 
        /// instance of ProfileData or the individual ProfileData Properties. If PropertyNames or 
        /// PropertyValues is null, and exception will be thrown.
        /// </summary>
        /// <param name="pd">Object representing a users profile data</param>
        public ProfileReader(ProfileData pd)
        {           
            _propertyValue = ProfileReader.Parse(pd);
        }

        /// <summary>
        /// To instantiate a new instance of ProfileReader, you must supply an 
        /// instance of ProfileData or the individual ProfileData Properties. If PropertyNames or 
        /// PropertyValues is null, and exception will be thrown.
        /// </summary>
        /// <param name="names">Names of the properties</param>
        /// <param name="values">Values</param>
        /// <param name="binary">Any binary serlialized objects</param>
        public ProfileReader(string names, string values, byte[] binary)
        {         
            ProfileData pd = new ProfileData();
            pd.PropertyNames = names;
            pd.PropertyValues = values;
            pd.PropertyValuesBinary = binary;
            _propertyValue = ProfileReader.Parse(pd);
        }

        private SettingsPropertyValueCollection _propertyValue = null;

        /// <summary>
        /// During the constructor, SettingsPropertyValueCollection is created. This collection
        /// contains the values of the users profile.
        /// </summary>
        public SettingsPropertyValueCollection PropertyValues
        {
            get
            {
                return _propertyValue;
            }
        }

        /// <summary>
        /// Returns the specified property values.
        /// </summary>
        public object this[string propertyName]
        {
            get
            {
                if(_propertyValue == null)
                    return null;
                
                SettingsPropertyValue p = PropertyValues[propertyName];
                if(p == null)
                    return null;

                return p.PropertyValue;
                
            }
        }

        /// <summary>
        /// Generates an instance of SettingsPropertyValueCollection containing a users property values.
        /// </summary>
        public static SettingsPropertyValueCollection Parse(ProfileData pd)
        {
       
                SettingsPropertyValueCollection spvc = GetPropertyValues(pd);
                return spvc;
        }

        /// <summary>
        /// Generates a SettingsPropertyValueCollection for the specified ProfileData.
        /// </summary>
        internal static SettingsPropertyValueCollection GetPropertyValues(ProfileData pd)
        {
            //NOTE: Can we cache/clone/copy this collection. Chances are when we use it, we will
            //access it many times in a row.
            SettingsPropertyValueCollection spvc = new SettingsPropertyValueCollection();

            foreach (SettingsProperty p in ProfileBase.Properties)
            {
                if (p.SerializeAs == SettingsSerializeAs.ProviderSpecific)
                {
                    if (p.PropertyType.IsPrimitive || (p.PropertyType == typeof(string)))
                    {
                        p.SerializeAs = SettingsSerializeAs.String;
                    }
                    else
                    {
                        p.SerializeAs = SettingsSerializeAs.Xml;
                    }
                }
                spvc.Add(new SettingsPropertyValue(p));
            }

            try
            {
                ParseProfileData(pd, spvc);
            }
            catch (Exception)
            {
            }
            return spvc;
        }

        /// <summary>
        /// Parses the ProfileData object and populates the SettingsPropertyValueCollection with the values
        /// </summary>
        /// <param name="pd">Data to parse</param>
        /// <param name="properties">Data result container</param>
        internal static void ParseProfileData(ProfileData pd, SettingsPropertyValueCollection properties)
        {
            if(pd.PropertyNames == null)
                return;

            string[] names = pd.PropertyNames.Split(':');
            for (int i = 0; i < (names.Length / 4); i++)
            {
                string propName = names[i * 4];
                SettingsPropertyValue spv = properties[propName];
                if (spv == null)
                {
                    continue;
                }
                int index = int.Parse(names[(i * 4) + 2], CultureInfo.InvariantCulture);
                int len = int.Parse(names[(i * 4) + 3], CultureInfo.InvariantCulture);
                if (len == -1)
                {
                    if (!spv.Property.PropertyType.IsValueType)
                    {
                        spv.PropertyValue = null;
                        spv.IsDirty = false;
                        spv.Deserialized = true;
                    }
                    continue;
                }
                if (((names[(i * 4) + 1] == "S") && (index >= 0)) && (len > 0) && (pd.PropertyValues != null) && (pd.PropertyValues.Length >= (index + len)))
                {
                    spv.SerializedValue = pd.PropertyValues.Substring(index, len);
                }
                if (((names[(i * 4) + 1] == "B") && (index >= 0)) && (len > 0) && (pd.PropertyValuesBinary != null) && (pd.PropertyValuesBinary.Length >= (index + len)))
                {
                    byte[] buffer = new byte[len];
                    Buffer.BlockCopy(pd.PropertyValuesBinary, index, buffer, 0, len);
                    spv.SerializedValue = buffer;
                }
            }
        }
	}

    /// <summary>
    /// Special container object for profile data.
    /// </summary>
    public struct ProfileData
    {
        public string PropertyNames;
        public string PropertyValues;
        public byte[] PropertyValuesBinary;
    }
}
