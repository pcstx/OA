//------------------------------------------------------------------------------
// <copyright company="Telligent Systems">
//     Copyright (c) Telligent Systems Corporation.  All rights reserved.
// </copyright> 
//------------------------------------------------------------------------------

using System;
using System.Web.Mail;
using System.ComponentModel;
using System.Xml;


namespace GPRP.GPRPComponents {

    /// <summary>
    /// This class contains properties for an EmailTemplate.
    /// </summary>
    public class EmailTemplate : MailMessage {
        string emailType;
        Guid emailID;
		int numberOfTries = 0;

        public EmailTemplate() {
        }

        public EmailTemplate(XmlNode node) {

            // Read the attributes
            //
            Priority		= (MailPriority) Enum.Parse(Priority.GetType(), node.Attributes.GetNamedItem("priority").InnerText);
            emailType		= node.Attributes.GetNamedItem("emailType").InnerText;
            Subject			= node.SelectSingleNode("subject").InnerText;
            Body			= node.SelectSingleNode("body").InnerText;
            From			= node.SelectSingleNode("from").InnerText;
			BodyEncoding	= System.Text.Encoding.UTF7;

        }

        public string EmailType {
            get {
                return emailType;
            }
        }

        public Guid EmailID {
            get { return emailID; }
            set { emailID = value; }
        }

		public int NumberOfTries
		{
			get { return numberOfTries; }
			set { numberOfTries = value; }
		}
    }

}
