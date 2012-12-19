//------------------------------------------------------------------------------
// <copyright company="Telligent Systems">
//     Copyright (c) Telligent Systems Corporation.  All rights reserved.
// </copyright> 
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Web;
using System.Xml;

namespace GPRP.GPRPComponents {
    
	/// <summary>
    /// Summary description for Vote.
    /// </summary>
    public class PollSummary {

		#region Contructor and member variables
		string question	= "";
		string description	= "";
		Hashtable voters = new Hashtable();
		SortedList answers;
		CSContext csContext = CSContext.Current;
		bool allowMultipleVotes = false;
		Post post;
		
        // *********************************************************************
        //  PollSummary
        //
        /// <summary>
        /// Constructor
        /// </summary>
        // ***********************************************************************/  
		public PollSummary(Post post) {
			this.post = post;
			GetPoll();
		}

		#endregion

		#region GetPoll
        // *********************************************************************
        //  GetPoll
        //
        /// <summary>
        /// Constructor
        /// </summary>
        // ***********************************************************************/        
        private void GetPoll() {
            answers = new SortedList();
            XmlDocument dom = new XmlDocument();
            XmlNodeList choicesNodeList;

            // Load the dom
            //
            dom.LoadXml(post.Body);

			XmlNode voteOptions = dom.SelectSingleNode("VoteOptions");
			if( voteOptions != null ) {
				if( voteOptions.Attributes["question"] != null )
					this.question = voteOptions.Attributes["question"].Value;

				if( voteOptions.Attributes["description"] != null )
					this.description = voteOptions.Attributes["description"].Value;

            // Bind to the Choices node and a Node Collection
            //
				choicesNodeList = voteOptions.ChildNodes;

            // Walk through each node in the Node List and add to Array List
            //
            foreach (XmlNode node in choicesNodeList)
                answers.Add(node.Name, new PollItem(node.Name, node.InnerText));
			}
        }

//		public static string SavePoll( string[] options ) {
//			return SavePoll( options, String.Empty );
//		}

		public static string SavePoll( string question, string[] options, string description ) {
			System.IO.MemoryStream stream = new System.IO.MemoryStream();
			System.Xml.XmlTextWriter writer = new XmlTextWriter( stream, System.Text.Encoding.UTF8 );
			writer.WriteStartDocument();
			writer.WriteStartElement("VoteOptions");
			writer.WriteAttributeString("question", question );
			writer.WriteAttributeString("description", description );
			int pos = 0;
			foreach( string s in options ) {
				writer.WriteElementString( System.Text.Encoding.UTF8.GetString( new byte[] { (byte)(97 + pos) }), s );
				pos++;
			}

			writer.WriteEndElement(); // /VoteOptions
			writer.WriteEndDocument();
			writer.Flush();
				
			return System.Text.Encoding.UTF8.GetString( stream.GetBuffer(), 0, (int)stream.Position );
		}
		#endregion

		#region HasVoted
		public bool HasVoted (int userID) {
			if (voters[userID] == null)
				return false;

			return true;
		}

		public string GetUserVote (int userID) {
			if (!HasVoted(userID))
				return null;

			return ((PollItem) answers[voters[userID].ToString()]).Answer;
		}
		#endregion

		#region Public Properties
		public string Question {
            get {
                return question;
            }
        }

		public  Hashtable Voters {
			get {
				return voters;
			}
		}

		public bool AllowMultipleVotes {
			get {
				return allowMultipleVotes;
			}
		}

        public SortedList Answers {
            get {
                return answers;
            }
        }

		public int PostID {
			get {
				return post.PostID;
			}
		}

		public string Description {
			get{
				return description; 
			}
		}
		#endregion

    }
}
