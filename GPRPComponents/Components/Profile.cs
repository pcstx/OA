//------------------------------------------------------------------------------
// <copyright company="Telligent Systems">
//     Copyright (c) Telligent Systems Corporation.  All rights reserved.
// </copyright> 
//------------------------------------------------------------------------------

using System;

using Microsoft.ScalableHosting.Profile;

namespace GPRP.GPRPComponents
{
	/// <summary>
	/// Summary description for Profile.
	/// </summary>
	public class Profile
	{
        enum ProfileState
        {
            Anonymous,
            Read,
            ReadWrite
        };

        private ProfileBase profilebase = null;
        private ProfileReader reader = null;
        private SettingsPropertyCollection defualtProfileSettings = null;
        private ProfileState state = ProfileState.Anonymous;
        #region cnstr

        public Profile()
        {
            state = ProfileState.Anonymous;
            defualtProfileSettings = ProfileBase.Properties;
        }

        /// <summary>
        /// Creates a readonly instance of the strongly typed CommunityServer Profile
        /// </summary>
        /// <param name="pd"></param>
		public Profile(ProfileData pd)
		{
            reader = new ProfileReader(pd);
            state = ProfileState.Read;
            
            
		}

        /// <summary>
        /// Creates a new Instance of the CS Profile. Values are editable and can be commited by calling Save();
        /// </summary>
        /// <param name="username"></param>
        public Profile(ProfileBase pb)
        {
            //Profile Base does not have any data unitl a value is first accessed.
            //So we need to store the profile base instead of SettingsPropertyValueCollection
            profilebase = pb;
            state = ProfileState.ReadWrite;
        }
        #endregion

        #region Profile Core

        protected object GetObject(string key)
        {
            switch(state)
            {
                case ProfileState.Anonymous:
                    return defualtProfileSettings[key].DefaultValue;
                    
                case ProfileState.Read:
                    return reader[key];
                    
                case ProfileState.ReadWrite:
                    return profilebase[key];
                    
                default: 
                    throw new ArgumentException("ProfileState was not set or is an invalid value");
            }
        }

        /// <summary>
        /// Provides read access to the internal SettingsPropertyValueCollection.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        protected string GetString(string key)
        {
            string s = GetObject(key) as string;
            return s == null ? string.Empty : s;
        }

		// 修改，对于某些Int型不能正常转化
		protected int GetInt(string key)
		{
			string num = GetString(key);
			try
			{
				return Int32.Parse(num,System.Globalization.CultureInfo.InvariantCulture);
			}
			catch
			{
				try
				{
					return Convert.ToInt32(GetObject(key));
				}
				catch
				{
					return 0;
				}
			}
		}

        /// <summary>
        /// Providers write access to the internal SettingsPropertyValueCollection. If ReadOnly mode = true, 
        /// the data will not be saved. (if in debug mode, an exception will be thrown)
        /// </summary>
        /// <param name="key">Data Key</param>
        /// <param name="obj">Data Value</param>
        protected void Set(string key, object obj)
        {
            if(!IsReadOnly)
               profilebase[key] = obj;
            else
            {
                #if DEBUG
                    throw new Exception("Profile Object is in readonly mode. Data can not be updated");
                #endif
            }            
        }

        /// <summary>
        /// Save can only occur when IsReadOnly returns false. If in debug mode an exception will be thrown.
        /// </summary>
        public void Save()
        {
            if(!IsReadOnly)
                profilebase.Save();
            else
            {
                #if DEBUG
                  throw new Exception("Profile Object is in readonly mode. Data can not be updated");
                #endif
            }
        }

        /// <summary>
        /// Returns true if the current profile can not be written too. This will happen when a ProfileData object is used
        /// to populate the SettingsPropertyValueCollection values. (ie, ProfileReader is used).
        /// </summary>
        public bool IsReadOnly
        {
            get
            {
                return state != ProfileState.ReadWrite;
            }
        }
        #endregion

        #region Properties

        ///	<summary>
        ///	Controls the skin the user views the site with
        ///	</summary>
        public DateTime	BirthDate 
        {
            get	
            {
                try	
                {
                    return (DateTime)GetObject("birthdate");
                } 
                catch	
                {
                    return DateTime.MinValue;
                }
            }
            set	{ Set("birthdate",	value); }
        }

		public int FontSize {
			get {
				try{
					return (int) GetObject("fontsize");
				} catch {}
				
				return 0;
			}
			set {
				if ((value >= -1) && (value <= 2))
					 Set("fontsize",value);
			}
		}


        ///	<summary>
        ///	Controls the skin the user views the site with
        ///	</summary>
        public Gender Gender 
        {
            get	
            { 
                try	
                {
                    int g = GetInt("gender");
                   Gender gender = (Gender)g;
					return gender;

                }
                catch{} //default value

                    return Gender.NotSet;
            }
            set	{ 
				int g = (int)value;
				Set("gender",g);	
			}
        }

		public bool EnableEmoticons
		{
			get { return  (bool)GetObject("enableEmoticons");}
			set { Set("enableEmoticons", value);}
		}

        ///	<summary>
        ///	Format for how the user	wishes to view date	values
        ///	</summary>
        public string DateFormat 
        {
            get	
            { 
                string dateFormat =	GetString("dateFormat");	
			
                if (dateFormat == null || dateFormat == string.Empty)
                    dateFormat = "yyyy-MM-dd";

                return dateFormat;
            }
            set	{ Set("dateFormat", value); }
        }

        ///	<summary>
		///	User's common name
		///	</summary>
		public String CommonName {
			get	{ return GetString("commonName");	}
			set	{ Set("commonName",	value);	}
		}


        ///	<summary>
        ///	Specifies the user's fake email	address.  This email address, if supplied, is the one
        ///	that is	displayed when showing a post posted by	the	user.
        ///	</summary>
        public String PublicEmail 
        {
            get	{ return GetString("publicEmail"); }
            set	{ Set("publicEmail", value); }
        }

		public String Language 
		{
			get	
			{ 
				// 2004-02-27: 修改，用户语言默认为系统默认语言
				string language =  GetString("language"); 
				if (language == null || language == "")
					language = Globals.Language;
				return language;
			}
			set	{ Set("language", value); }
		}


        ///	<summary>
        ///	The	user's homepage	or favorite	Url.  This Url is shown	at the end of each of the user's posts.
        ///	</summary>
        public String WebAddress 
        {
            get	{ return GetString("webAddress"); }
            set	{ Set("webAddress", value); }
        }

        ///	<summary>
        ///	The	user's homepage	or favorite	Url.  This Url is shown	at the end of each of the user's posts.
        ///	</summary>
        public String WebLog 
        {
            get	{ return GetString("webLog"); }
            set	{ Set("webLog", value); }
        }

        ///	<summary>
        ///	The	user's signature.  
        ///	Used to store raw bbcode version, for easier editting.
        ///	</summary>
        public String Signature 
        {
            get 
            { 
                return GetString("signature");	
            }
            set 
            { 
                Set("signature", value); 
            }
        }


        ///	<summary>
        ///	The	user's Formatted signature.  
        ///	Used to store the HTML formatted version, for faster performance.
        ///	
        ///	If specified, this signature is shown at the end of each of the user's posts.
        ///	</summary>
        public String SignatureFormatted 
        {
            get 
            { 
                return GetString("signatureFormatted");	
            }
            set 
            {
                Set("signatureFormatted", Transforms.FormatPostText( value ));
            }
        }

        ///	<summary>
        ///	The	user's location
        ///	</summary>
        public String Location 
        {
            get	{ return GetString("location"); }
            set	{ Set("location", value); }
        }

        ///	<summary>
        ///	The	user's occupation
        ///	</summary>
        public String Occupation 
        {
            get	{ return GetString("occupation"); }
            set	{ Set("occupation", value); }
        }

        ///	<summary>
        ///	The	user's interests
        ///	</summary>
        public String Interests	
        {
            get	{ return GetString("interests");	}
            set	{ Set("interests",	value);	}
        }
		
        ///	<summary>
        ///	MSN	IM address
        ///	</summary>
        public String MsnIM	
        {
            get	{ return GetString("msnIM");	}
            set	{ Set("msnIM",	value);	}
        }

        ///	<summary>
        ///	Yahoo IM address
        ///	</summary>
        public String YahooIM 
        {
            get	{ return GetString("yahooIM"); }
            set	{ Set("yahooIM", value); }
        }

        ///	<summary>
        ///	AOL	IM Address
        ///	</summary>
        public String AolIM	
        {
            get	{ return GetString("aolIM");	}
            set	{ Set("aolIM",	value);	}
        }

        ///	<summary>
        ///	ICQ	address
        ///	</summary>
        public String IcqIM	
        {
            get	{ return GetString("icqIM");	}
            set	{ Set("icqIM",	value);	}
        }

		/// 新增QQ号
        ///	<summary>
        ///	QQ	address
        ///	</summary>
        public String QQIM	
        {
            get	{ return GetString("qqIM");	}
            set	{ Set("qqIM",	value);	}
        }

        /// <summary>
        /// Returns true or false indicating whether the user has indicated they want to see post
        /// preview popups in the thread view of a forum.
        /// </summary>
        public bool EnablePostPreviewPopup 
        {
            get
            { 
                object obj = GetObject("enablePostPreviewPopup");
                return obj == null ? false : (bool)obj;
            }
            set{ 
                Set("enablePostPreviewPopup", value); 
            }
        }

        ///	<summary>
        ///	Specifies the user's timezone offset.
        ///	</summary>
        public double Timezone 
        {
            get	
            { 
                object obj = GetObject("timezone");
                return obj == null ? 0 : (double)obj;
            }
            set	
            {
                if (value <	-12	|| value > 12)
                    Set("timezone",0);
                else
                    Set("timezone",value);
            }
        }

        #endregion
	}
}
