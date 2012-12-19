//------------------------------------------------------------------------------
// <copyright company="Telligent Systems">
//     Copyright (c) Telligent Systems Corporation.  All rights reserved.
// </copyright> 
//------------------------------------------------------------------------------

using System;

//using CommunityServer;
using System.Collections;
using GPRP.GPRPEnumerations;

namespace GPRP.GPRPComponents {

    public class CSException : ApplicationException {

        #region member variables
        CSExceptionType exceptionType;
        CreateUserStatus status;
        #endregion

        string message;

        public CSException(CSExceptionType t) : base() {
            Init();
            this.exceptionType = t; 
        }

        public CSException(CSExceptionType t, string message) : base(message) {
            Init();
            this.exceptionType = t; 
            this.message = message;
        }

        public CSException(CSExceptionType t, string message, Exception inner) : base(message, inner) {
            Init();
            this.exceptionType = t; 
            this.message = message;
        }

        public CSException(CreateUserStatus status) : base () {
            Init();
            exceptionType = CSExceptionType.CreateUser;
            this.status = status;
        }

        public CSException(CreateUserStatus status, string message) : base (message) {
            Init();
            exceptionType = CSExceptionType.CreateUser;
            this.status = status;
            this.message = message;
        }

        public CSException(CreateUserStatus status, string message, Exception inner) : base (message, inner) {

            Init();
            exceptionType = CSExceptionType.CreateUser;
            this.status = status;
            this.message = message;

        }

        public CSExceptionType ExceptionType {
            get {
                return exceptionType;
            }
        }

        public CreateUserStatus CreateUserStatus {
            get {
                return status;
            }
        }

        public override string Message {
            get {
               switch (exceptionType) {
                    case CSExceptionType.GroupNotFound:
                        return string.Format(ResourceManager.GetString("Exception_ForumGroupNotFound"), base.Message);

                    case CSExceptionType.SectionNotFound:
                        return string.Format(ResourceManager.GetString("Exception_ForumNotFound"), base.Message);

                    case CSExceptionType.PostNotFound:
                        return string.Format(ResourceManager.GetString("Exception_PostNotFound"), base.Message);

                    case CSExceptionType.UserNotFound:
                        return string.Format(ResourceManager.GetString("Exception_UserNotFound"), base.Message);

                    case CSExceptionType.SkinNotSet:
                        return ResourceManager.GetString("Exception_SkinNotSet");

                    case CSExceptionType.SkinNotFound:
                        return string.Format(ResourceManager.GetString("Exception_SkinNotFound"), base.Message);

                    case CSExceptionType.PostAccessDenied:
                        string msg = ResourceManager.GetString("Exception_PostAccessDenied");
                        return string.Format(msg,this.message);

                    case CSExceptionType.PostEditAccessDenied:
                        return ResourceManager.GetString("Exception_PostEditAccessDenied");

                    case CSExceptionType.PostEditPermissionExpired:
                        return ResourceManager.GetString("Exception_PostEditPermissionExpired");

					case CSExceptionType.PostInvalidAttachmentType:
						return string.Format( ResourceManager.GetString("Exception_PostInvalidAttachmentType"), base.Message );

					case CSExceptionType.PostAttachmentTooLarge:
						return string.Format( ResourceManager.GetString("Exception_PostAttachmentTooLarge"), base.Message );
                   case CSExceptionType.FileNotFound:
                       return string.Format(ResourceManager.GetString("Config_dontExistFile"), base.Message);
                }

                return base.Message;
            }
        }

        #region Public methods
        public override int GetHashCode() {
            return this.GetHashCode(CSContext.Current.SiteSettings.SettingsID);
        }

		public int GetHashCode(int settingsID)
		{
			string stringToHash = (settingsID + exceptionType + this.ToString());

			return stringToHash.GetHashCode();
		}

		public void Log(int settingsID)
		{
			CommonDataProvider dp = CommonDataProvider.Instance();

			dp.LogException(this,settingsID);			
		}

        public void Log() {
			Log(CSContext.Current.SiteSettings.SettingsID);
        }
        #endregion


        public override string ToString()
        {
            switch(this.ExceptionType)
            {
                case CSExceptionType.PostAccessDenied:
                    return  string.Format("{0}{1}" , base.ToString(), new System.Diagnostics.StackTrace());
				case CSExceptionType.UnknownError:
					return  string.Format("{0}{1}" , base.ToString(), this.StackTrace);
                default:
                    return base.ToString();

            }
            
        }


        #region Public Properties
        // LN 6/9/04: Init the following properties
        //
        string userAgent = string.Empty;
        public string UserAgent {
            get { return userAgent; }
            set { userAgent = value; }
        }

        public int Category {
            get { return (int) exceptionType; }
            set { exceptionType = (CSExceptionType) value; }
        }

        string ipAddress = string.Empty;
        public string IPAddress {
            get { return ipAddress; }
            set { ipAddress = value; }
        }

        string httpReferrer = string.Empty;
        public string HttpReferrer {
            get { return httpReferrer; }
            set { httpReferrer = value; }
        }

        string httpVerb = string.Empty;
        public string HttpVerb {
            get { return httpVerb; }
            set { httpVerb = value; }
        }

        string httpPathAndQuery = string.Empty;
        public string HttpPathAndQuery {
            get { return httpPathAndQuery; }
            set { httpPathAndQuery = value; }
        }

        DateTime dateCreated;
        public DateTime DateCreated {
            get { return dateCreated; }
            set { dateCreated = value; }
        }

        DateTime dateLastOccurred;
        public DateTime DateLastOccurred {
            get { return dateLastOccurred; }
            set { dateLastOccurred = value; }
        }

        int frequency = 0;
        public int Frequency {
            get { return frequency; }
            set { frequency = value; }
        }

        string stackTrace = string.Empty;
        public string LoggedStackTrace {
            get {
                return stackTrace;
            }
            set {
                stackTrace = value;
            }
        }

        int exceptionID = 0;
        public int ExceptionID {
            get {
                return exceptionID;
            }
            set {
                exceptionID = value;
            }
        }
        #endregion

        #region Private helper functions
		void Init() {
			try {
				CSContext csContext = CSContext.Current;

				// TDD 10/27/2004
				// This was failing when trying to access the database that we didn't have permissions to. When this is
				// happening, the application is first loading (LoadSiteSettings) and not all of this context information 
				// is present. Because of this, we were getting an exception in the Exception class which defeats
				// the whole purpose of having this class. Adding some additional checks to ensure we don't throw an 
				// exception in our exception constructor
				if( csContext != null &&
					csContext.Context != null &&
					csContext.Context.Request != null ) {

					if (csContext.Context.Request.UrlReferrer != null)
						httpReferrer = csContext.Context.Request.UrlReferrer.ToString();
			
					if (csContext.Context.Request.UserAgent != null)
						userAgent = csContext.Context.Request.UserAgent;
			
					if (csContext.Context.Request.UserHostAddress != null)
						ipAddress = csContext.Context.Request.UserHostAddress;

					// ACS HttpRequest.RequestType can throw a null reference exception
					// and I can't find any way to test if it will.
					// This happens when the request's inner HttpWorkerRequest is null.
					// This is observable when the exception is created from the 
					// ForumsHttpModule.ScheduledWorkCallbackEmailInterval method's catch block.
					// I assume this is because it happens on timer/backgroundthread.
					try {
						if (csContext.Context.Request != null
							&& csContext.Context.Request.RequestType != null )
							httpVerb = csContext.Context.Request.RequestType;
					} catch( Exception ex ) {
						System.Diagnostics.Debug.WriteLine( ex.ToString() );
					}

					// ACS "forumContext.Context.Request.Url != null" check was added because
					// , similarly to above, the Url property will be null when this method is called
					// from the ForumsHttpModule.ScheduledWorkCallbackEmailInterval timer callback.
					if (csContext.Context.Request != null
						&& csContext.Context.Request.Url != null
						&& csContext.Context.Request.Url.PathAndQuery != null)
						httpPathAndQuery = csContext.Context.Request.Url.PathAndQuery;

					// LN 6/9/04: Added to have Log() working. The table columns that hold
					// all exception details doesn't support null values. In certain circumstances 
					// adding exception details to database for thrown exception might run into an 
					// unhandled exception: a new exception is thrown while current exception 
					// processing is not finished (ForumsHttpModule.Application_OnError).
					if (csContext.Context.Request != null
						&& csContext.Context.Request.UrlReferrer != null
						&& csContext.Context.Request.UrlReferrer.PathAndQuery != null)
						httpReferrer = csContext.Context.Request.UrlReferrer.PathAndQuery;
				}
			}
			catch{}
		}
        #endregion

        #region Statics
        public static ArrayList GetExceptions(int exceptionType, int minFrequency) {
            CommonDataProvider dp = CommonDataProvider.Instance();

            return dp.GetExceptions(exceptionType, minFrequency );
        }

        public static void DeleteExceptions(int settingsID, ArrayList deleteList) {
            CommonDataProvider dp = CommonDataProvider.Instance();

            dp.DeleteExceptions( settingsID, deleteList );
        }

        #endregion
    }

}
