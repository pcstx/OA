//------------------------------------------------------------------------------
// <copyright company="Telligent Systems">
//     Copyright (c) Telligent Systems Corporation.  All rights reserved.
// </copyright> 
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Text.RegularExpressions;
using GPRP.GPRPEnumerations;
namespace GPRP.GPRPComponents {

	public class Formatter {

        /// <summary>
        /// Many WYSWIGY editors accidently convert local Urls to relative Urls. ConvertLocalUrls looks for these
        /// and attempts to convert them back.
        /// </summary>
        public static string ConvertLocalUrls(string body, string host)
        {
            return Regex.Replace(body,"<a href=\"/","<a href=\"" + host + "/" ,RegexOptions.IgnoreCase);
        }


        /// <summary>
        /// We do not allow any markup in comments. This method will transform links to full hrefs and 
        /// add the ref=nofollow attribute
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string SafeFeedback(string text)
        {
            if(Globals.IsNullorEmpty(text))
                return text;

            text = Globals.HtmlEncode(text);

            //Find any links
            string pattern = @"(http|ftp|https):\/\/[\w]+(.[\w]+)([\w\-\.,@?^=%&amp;:/~\+#]*[\w\-\@?^=%&amp;/~\+#])";
            MatchCollection matchs;
                        
            matchs = Regex.Matches(text,pattern, RegexOptions.IgnoreCase | RegexOptions.Compiled);
            foreach (Match m in matchs) 
            {
                text = text.Replace(m.ToString(), "<a rel=\"nofollow\" target=\"_new\" href=\"" + m.ToString() + "\">" + m.ToString() + "</a>");
            }

            return text.Replace("\n", "<br>");			
        }

		#region FormatFontSize
		public static string FormatFontSize(double fontsize) {

			CSContext csContext = CSContext.Current;

			if (!csContext.User.IsAnonymous) {
				int size = csContext.User.Profile.FontSize;

				switch (size) {
					case -1:
						fontsize = fontsize - (fontsize * .02);
						break;

					case 1:
						fontsize = fontsize + (fontsize * .15);
						break;

					case 2:
						fontsize = fontsize + (fontsize * .30);
						break;

				}
			}

			return fontsize.ToString().Replace(",",".") + "em";
		}
		#endregion

		#region Format Date
		public static string FormatDate (DateTime date) {
			return FormatDate(date, false);
		}

		public static string FormatDate (DateTime date, bool showTime) {
		    CSContext csContext = CSContext.Current;
			string dateFormat = CSContext.Current.User.Profile.DateFormat;
            
			if (csContext.User.UserID > 0) {
				dateFormat = csContext.User.Profile.DateFormat;
				date = csContext.User.GetTimezone(date);
			}

			if (showTime)
				return date.ToString(dateFormat + " " + CSContext.Current.SiteSettings.TimeFormat);
			else
				return date.ToString(dateFormat);
		}
		#endregion

		#region Format Date Post
		// Used to return a date in Word format (Today, Yesterday, Last Week, etc)
		//
		public static string FormatDatePost (DateTime date) {
			// This doesn't have to be as complicated as the FormatLastPost.
			// TODO: Need to optimize FormatLastPost (the multiple calls to GetUser()).
			//
			string returnItem;
			string dateFormat;
			DateTime userLocalTime;
			User user;

			// Optimizing code to only grab GetUser() once, since it is a lot of overhead.
			//
			user = CSContext.Current.User;
			
			// Setting up the user's date profile
			//
			if (user.UserID > 0) {
				date = user.GetTimezone(date);
				dateFormat = user.Profile.DateFormat;
				userLocalTime = user.GetTimezone(DateTime.Now);
			} else {
				// date is already set
				dateFormat = CSContext.Current.SiteSettings.DateFormat;
				userLocalTime = DateTime.Now;
			}
			
			// little error checking
			//
			if (date < DateTime.Now.AddYears(-20) )
				return ResourceManager.GetString("NumberWhenZero");
			
			// make Today and Yesterday bold for now...
			//
			if ((date.DayOfYear == userLocalTime.DayOfYear) && (date.Year == userLocalTime.Year)) {

				returnItem = ResourceManager.GetString("TodayAt");
				returnItem += ((DateTime) date).ToString(CSContext.Current.SiteSettings.TimeFormat);

			} else if ((date.DayOfYear == (userLocalTime.DayOfYear - 1)) && (date.Year == userLocalTime.Year)) {

				returnItem = ResourceManager.GetString("YesterdayAt");
				returnItem += ((DateTime) date).ToString(CSContext.Current.SiteSettings.TimeFormat);

			} else {

				returnItem = date.ToString(dateFormat) + ", " + ((DateTime) date).ToString(CSContext.Current.SiteSettings.TimeFormat);

			}
			return returnItem;
		}
		#endregion

		#region Expand/Collapse Icon
		public static string ExplandCollapseIcon (Group group) {

			if (group.HideSections)
				return Globals.GetSkinPath() +"/images/expand-closed.gif";
            
			return Globals.GetSkinPath() +"/images/expand-open.gif";
            
		}
		#endregion



        #region Format number
        public static string FormatNumber (int number) {
            return FormatNumber (number, ResourceManager.GetString("NumberFormat"), ResourceManager.GetString("NumberWhenZero"));
        }

        public static string FormatNumber (int number, string whenZero) {
            return FormatNumber (number, ResourceManager.GetString("NumberFormat"), whenZero);
        }

        public static string FormatNumber (int number, string format, string whenZero) {

            if (number == 0)
                return whenZero;
            else
                return number.ToString(format);

        }
        #endregion

        #region String length formatter
        public static string CheckStringLength (string stringToCheck, int maxLength) {
            string checkedString = null;

            if (stringToCheck.Length <= maxLength)
                return stringToCheck;

            // If the string to check is longer than maxLength 
            // and has no whitespace we need to trim it down
            if ((stringToCheck.Length > maxLength) && (stringToCheck.IndexOf(" ") == -1)) {
                checkedString = stringToCheck.Substring(0, maxLength) + "...";
            } else if (stringToCheck.Length > 0) {
                string[] words;
                int expectedWhitespace = stringToCheck.Length / 8;

                // How much whitespace is there?
                words = stringToCheck.Split(' ');

                // If the number of wor
                //if (expectedWhitespace > words.Length)
                    checkedString = stringToCheck.Substring(0, maxLength) + "...";
                //else
                //    checkedString = stringToCheck;
            } else {
                checkedString = stringToCheck;
            }

            return checkedString;
        }
        #endregion

		#region Strip All Tags from a String
		/*
		 * Takes a string and strips all bbcode and html from the
		 * the string. Replacing any <br />s with linebreaks.  This
		 * method is meant to be used by ToolTips to present a
		 * a stripped-down version of the post.Body
		 *
		 */
		public static string StripAllTags ( string stringToStrip ) {
			// paring using RegEx
			//
			stringToStrip = Regex.Replace(stringToStrip, "</p(?:\\s*)>(?:\\s*)<p(?:\\s*)>", "\n\n", RegexOptions.IgnoreCase | RegexOptions.Compiled);
			stringToStrip = Regex.Replace(stringToStrip, "<br(?:\\s*)/>", "\n", RegexOptions.IgnoreCase | RegexOptions.Compiled);
			stringToStrip = Regex.Replace(stringToStrip, "\"", "''", RegexOptions.IgnoreCase | RegexOptions.Compiled);
			stringToStrip = Transforms.StripHtmlXmlTags( stringToStrip );

			return stringToStrip;
		}

        public static string RemoveHtml(string html)
        {
           return RemoveHtml(html,0);
        }

        public static string RemoveHtml(string html, int charLimit)
        {
            if(html == null || html.Trim().Length == 0)
                return string.Empty;

            string nonhtml =  Regex.Replace(html, "<[^>]+>", "", RegexOptions.IgnoreCase | RegexOptions.Compiled);
            if(charLimit <= 0 || charLimit >= nonhtml.Length)
                return nonhtml;

            int place = charLimit;
            int len = nonhtml.Length - 1;

            while(!Char.IsWhiteSpace(nonhtml[place]) && place < len)
            {
                place ++;
            }
            //²¹ÉÏ¿Õ°×
            return nonhtml.Substring(0,place);


        }

		#endregion

		#region Format post emoticon
		public static string GetEmotionMarkup (int emoticonID) {

            const string imgFormat = "<img src=\"{0}{1}\" alt=\"{2}\" />";
            string forumHomePath = Globals.GetSiteUrls().Emoticon;

            // If we aren't using Emoticons, return an empty string.
            if ( (emoticonID == 0) || (!CSContext.Current.SiteSettings.EnableEmoticons) )
				return "";

            ArrayList emoticonTxTable = Smilies.GetSmilies();

            // EAD 2-9-2005: Removed try/catch block as this will return null if doesn't exist and changed
            // logic to detect the null returned.
            Smiley smiley = new Smiley();
            smiley = Smilies.GetSmiley( emoticonID );

            if (smiley != null)
                return string.Format(imgFormat, forumHomePath, smiley.SmileyUrl, smiley.SmileyText + " [" + smiley.SmileyCode + "]");
            else
                return "";

		}
		#endregion

        #region Format edit notes
        public static string EditNotes (string notes) {

            if (notes == null)
                return String.Empty;

            //string editNotesTable = "<table width=\"75%\" class=\"editTable\"><tr><td>{0}</td></tr></table>";
            //return string.Format(editNotesTable, Transforms.FormatPost(notes, PostType.HTML)) + Globals.HtmlNewLine + Globals.HtmlNewLine; 
			return Transforms.FormatPostText(notes, PostType.HTML);

        }
        #endregion

        #region Format Post IP Address
        public static string PostIPAddress (Post post) {

            if (post.UserHostAddress == "000.000.000.000")
                return string.Format(ResourceManager.GetString("PostFlatView_IPAddress"), ResourceManager.GetString("NotLogged"));

            if (CSContext.Current.SiteSettings.DisplayPostIP) {
                return string.Format(ResourceManager.GetString("PostFlatView_IPAddress"), post.UserHostAddress);
            } else if ((CSContext.Current.SiteSettings.DisplayPostIPAdminsModeratorsOnly) && ((CSContext.Current.User.IsForumAdministrator) || (CSContext.Current.User.IsModerator)) ){ 
                return string.Format(ResourceManager.GetString("PostFlatView_IPAddress"), post.UserHostAddress);
            } else {
                return string.Format(ResourceManager.GetString("PostFlatView_IPAddress"), ResourceManager.GetString("Logged"));
            }

        }
        #endregion

        #region Format Whitespace
        public static string Whitespace(int height, int width, bool preBreak, bool postBreak) {
            string imgTag = string.Format("<img width=\"{1}\" height=\"{0}\" src=\"" + Globals.ApplicationPath + "/Utility/1x1.gif\">", height, width);

            if (preBreak)
                imgTag = "<br>" + imgTag;

            if (postBreak)
                imgTag = imgTag + "<br>";

            return imgTag;

        }
        
        #endregion

        #region Format Username
        public static string FormatUsername (int userID, string username) {
            return FormatUsername( userID, username, false, true );
        }
        
        public static string FormatUsername (int userID, string username, bool showAsAnonymous) {
            return FormatUsername( userID, username, showAsAnonymous, true );
        }
        
        public static string FormatUsername (int userID, string username, bool showAsAnonymous, bool useSpanTag) {
            // Added actual param showAsAnonymous to display a normal user as anonymous
            // if it is required so.
            //
            if (username == "")
                username = ResourceManager.GetString("DefaultAnonymousUsername");

            if (userID == 0)
                return ResourceManager.GetString("DefaultAnonymousUsername");
            
            if (userID > 0 && showAsAnonymous) { 
                username = ResourceManager.GetString("DefaultAnonymousUsername");
                userID = Users.GetAnonymousUser().UserID;
            }

            if (useSpanTag)
            return "<span class=\"inlineLink\" onclick=\"window.open('" + Globals.GetSiteUrls().UserProfile(userID) + "')\">" + username + "</span>";
            else
                return "<a target=\"_blank\" href=\"" + Globals.GetSiteUrls().UserProfile(userID) + "\">" + username + "</a>";
        }
        #endregion
		
		public static string FormatIrcCommands (string postText, string postFrom) {
			return FormatIrcCommands (postText, postFrom, "");
		}

		public static string FormatIrcCommands (string postText, string postFrom, string postTo) {
			// This is for our pure enjoyment. - Terry & Eric
			//

			// /me slaps Terry with a big-mouth trout.
			//
			postText = Regex.Replace(postText, @"(>/me\b|\n>/me)(.*?|\n)(<|\n|<\n)", "><span class=\"txtIrcMe\">&nbsp;*&nbsp;" + postFrom + "$2</span><", RegexOptions.IgnoreCase | RegexOptions.Compiled);
			postText = Regex.Replace(postText, @"(\n/me\b)(.*?|\n)(\n)", "<span class=\"txtIrcMe\">&nbsp;*&nbsp;" + postFrom + "$2</span>", RegexOptions.IgnoreCase | RegexOptions.Compiled);

			// /you
			//
			// TODO: when we figure out how to grab the "Replying To" from
			// create/edit post.


			return postText;
		}

        public static string FormatUserRank (Int32 postCount, bool showAsPicture) {
            ArrayList ranks = Ranks.GetRanks();
            return FormatUserRank( postCount, showAsPicture, ranks );
        }

        public static string FormatUserRank (Int32 postCount, bool showAsPicture, ArrayList ranks) { 
            string rankName = "";
            string rankUrlPicture = "";
            string imgHtmlTag = "<img src=\"{0}\" border=\"0\" alt=\"{1}\">";
            
            if (ranks == null)
                return ResourceManager.GetString( "Utility_UserRank_NotAvailable" );

            foreach (Rank rank in ranks) { 
                if (postCount >= rank.PostingCountMinimum && 
                    postCount <= rank.PostingCountMaximum) { 

                    rankName = rank.RankName.Trim();
                    rankUrlPicture = rank.RankIconUrl.Trim();
                    break;
                }
            }
            
            // Exit if there is an empty rank name
            //
            if (rankName.Length == 0) { 
                return ResourceManager.GetString( "Utility_UserRank_NotAvailable" );
            }
            
            // Do we need to display a pictured rank?
            //
            if (showAsPicture && rankUrlPicture.Length > 0) {
                string pictureUrl = Globals.GetSkinPath() + "/images/" + rankUrlPicture;

                // TODO: Check for picture availability ?!
                //

                return string.Format( imgHtmlTag, pictureUrl, rankName );
            } 
            
            return rankName;
        }

        #region Format Moderator Actions
        /// <summary>
        /// Gets a localized description for provided ModeratorAction.
        /// </summary>
        public static string FormatModeratorAction (ModeratorActions action) {
            return ResourceManager.GetString( "Utility_ModeratorActions_" + action.ToString() );
        }
        #endregion

        #region Format User Audit Counters
        /// <summary>
        /// Formats user moderation counters following this pattern "[message_actions] / [forum_actions]".
        /// If one of the ingredients are 0 then we will display a '-' character instead. 
        /// </summary>
        public static string FormatUserAuditCounters (int userID, AuditSummary summary) {
            string outString = "{0} / {1}";
            string counterLink = "<a href=\"{0}\">{1}</a>";
            int[] counters = Audit.GetUserAuditCounters( summary );

            if (counters == null) { 
                return string.Format( outString, "-", "-" );
            }

            
            if (counters[0] > 0 && counters[2] > 0)
                return string.Format( outString, string.Format( counterLink, SiteUrls.Instance().UserModerationHistory( userID, counters[1] ), counters[0] ), string.Format( counterLink, SiteUrls.Instance().UserModerationHistory( userID, counters[3] ), counters[2] ) );
            else {
                if (counters[0] == 0 && counters[2] > 0)
                    return string.Format( outString, "-", string.Format( counterLink, SiteUrls.Instance().UserModerationHistory( userID, counters[3] ), counters[2] ) );
                else 
                    if (counters[0] > 0 && counters[2] == 0)
                        return string.Format( outString, string.Format( counterLink, SiteUrls.Instance().UserModerationHistory( userID, counters[1] ), counters[0] ), "-" );
                    else
                        return string.Format( outString, "-", "-" );
            }
        }
        #endregion

        #region Format ban reason
        public static string FormatBanReason (UserBanReason banReason) { 
            return ResourceManager.GetString( "Utility_UserBanReason_" + banReason.ToString() );
        }
        #endregion
    }

}
