//------------------------------------------------------------------------------
// <copyright company="Telligent Systems">
//     Copyright (c) Telligent Systems Corporation.  All rights reserved.
// </copyright> 
//------------------------------------------------------------------------------

using System;
using System.Web;
using System.IO;

namespace GPRP.GPRPComponents {

    public class Avatar {
        int length = 0;
        int imageID = 0;
        int userID = 0;
        DateTime created;
        string contentType;
		string fileName = "";
		byte[] content = {byte.MinValue}; // –ﬁ∏ƒƒ¨»œ Ù–‘

        public Avatar() 
		{
        }

        public Avatar (Stream stream) {
            length = (int) stream.Length;
            content = new Byte[length];

            stream.Position = 0;
            stream.Read(content, 0, length);
        }

        public Avatar (HttpPostedFile postedFile) {

            // Get the file length and content type
            //
            length = postedFile.ContentLength;
            contentType = postedFile.ContentType;

            // Get the filename
            //
            fileName = postedFile.FileName.Substring(postedFile.FileName.LastIndexOf("\\") + 1);

            // Setup the byte array
            //
            content = new Byte[length];

            // Read in the attachment into a byte array
            //
            postedFile.InputStream.Read(content, 0, length);

        }

        public int ImageID {
            get {
                return imageID;
            }
            set {
                imageID = value;
            }
        }

        public int Length {
            get {
                return length;
            }
            set {
                length = value;
            }
        }

        public string ContentType {
            get {
                return contentType;
            }
            set {
                contentType = value;
            }
        }

        public string FileName {
            get {
                return fileName;
            }
            set {
                fileName = value;
            }

        }

        public Byte[] Content {
            get {
                return content;
            }
            set {
                content = value;
            }
        }

        public int UserID {
            get { return userID; }
            set { userID = value; }
        }

        public DateTime DateCreated {
            get {
                return created;
            }
            set {
                created = value;
            }
        }

    }
}
