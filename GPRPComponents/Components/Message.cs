//------------------------------------------------------------------------------
// <copyright company="Telligent Systems">
//     Copyright (c) Telligent Systems Corporation.  All rights reserved.
// </copyright> 
//------------------------------------------------------------------------------

using System;
using System.Xml;

namespace GPRP.GPRPComponents
{

    /// <summary>
    /// This class defines the properties that makeup a forum.
    /// </summary>
    public class Message {

        string title;
        string body;
        int messageID = -1;

        public Message(XmlNode node) {

            messageID = int.Parse(node.Attributes["id"].Value);
            title = node.SelectSingleNode("title").InnerText;
            body = node.SelectSingleNode("body").InnerText;

        }

        public int MessageID {
            get { return messageID; }
        }

        public string Title {
            get { return title; }
            set { title = value; }
        }

        public string Body {
            get { return body; }
            set { body = value; }
        }
    }
}
