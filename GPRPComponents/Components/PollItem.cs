//------------------------------------------------------------------------------
// <copyright company="Telligent Systems">
//     Copyright (c) Telligent Systems Corporation.  All rights reserved.
// </copyright> 
//------------------------------------------------------------------------------

using System;

namespace GPRP.GPRPComponents{
    /// <summary>
    /// Summary description for VoteResults.
    /// </summary>
    public class PollItem {
        string answerID;
		string answer;
        int total;

		public PollItem (string answerID, string answer) {
			this.answerID = answerID;
			this.answer = answer;
		}

		public string Answer {
			get {
				return answer;
			}
		}

        public string AnswerID {
            get {
                return answerID;
            }
        }

        public int Total {
            get {
                return total;
            }
            set {
                total = value;
            }
        }

		public void Increment() {
			total++;
		}
    }
}
