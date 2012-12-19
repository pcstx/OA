//------------------------------------------------------------------------------
// <copyright company="Telligent Systems">
//     Copyright (c) Telligent Systems Corporation.  All rights reserved.
// </copyright> 
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Specialized;
using System.Data.SqlClient;
using System.Web;
using System.IO;
using System.Web.Caching;
using System.Text.RegularExpressions;
using System.Text;
using GPRP.GPRPEnumerations;

namespace GPRP.GPRPComponents {

    /// <summary>
    /// This class is used to take the body of a post message and pre-format it to HTML. Two versions
    /// of the post body are stored in the database: raw original post and the transformed pre-formatted
    /// post. Pre-transforming the post offers better performance.
    /// </summary>
    public class Transforms {

		#region FormatPost
        // *********************************************************************
        //  FormatPost
        //
        /// <summary>
        /// Main public routine called to perform string transforms.
        /// </summary>
        /// 
        // ********************************************************************/
        public static string FormatPostText (string rawPostBody)  {
            return FormatPostText (rawPostBody, PostType.HTML, true);
        }

        public static string FormatPostText (string rawPostBody, PostType postType) {
            return FormatPostText (rawPostBody, postType, true);
        }

        public static string FormatPostText (string rawPostBody, PostType postType, bool allowCustomTransforms) {
			string formattedPost = rawPostBody;	

			// because of the way we receive the post from MSHTML, somethings can already be encoded. This breaks the regex patterns
			// so we're unencoding anything that is already encoded so regex patterns work correctly. Later we'll encode everything again.
			
			//SCOTTW: Why? Encoded markup should stay encoded...otherwise it will not render
			//formattedPost = HttpUtility.HtmlDecode( formattedPost );

			if (allowCustomTransforms ) {
				// must do emoteicons first because they are restricted from finding any inside [code][/code] blocks. We have to do
				// this before the later steps remove the [code][/code] blocks.
				if( CSContext.Current.SiteSettings.EnableEmoticons ) {
					formattedPost = EmoticonTransforms(formattedPost);
				}
			}

			// convert any html inside code/pre blocks into encoded html
			formattedPost = EncodeCodeBlocks( formattedPost );

			// strip the html posts of any offending html tags and encode any that don't meet the criteria.
			formattedPost = HtmlScrubber.Clean(formattedPost,false,true);//  ScrubHtml( formattedPost, postType );

			// convert all html tags to bbcode as the bbcode processor will only allow safe attributes to be included in the ponst.
			//			formattedPost = HtmlToBBCode( formattedPost );

			// Remove any script code
			//
			//			formattedPost = Transforms.StripScriptTags(formattedPost);

			// Peform specialized transforms first.
			//

			// Do BBCode transform, if any
			//
			formattedPost = BBcodeToHtml(formattedPost); 

			// Perform HTML Encoding of any tag elements, if not PostType.HTML
			//
			if (postType != PostType.HTML && postType != PostType.Poll) {
				formattedPost = HttpUtility.HtmlEncode(formattedPost);
				formattedPost = formattedPost.Replace("\n", Globals.HtmlNewLine);

				// Fix to reverse certain items that were HtmlEncoded above
				//
				formattedPost = UnHtmlEncode(formattedPost);
			} 

			return formattedPost;
		}
		#endregion

		#region CensorPost

		/// <summary>
		/// Validates if the supplied text would pass the censor test
		/// </summary>
		public static bool IsValidCensorText(string text)
		{
			return CensorPost(text) == text;
		}

		public static string CensorPost( string formattedPost ) {
            if(Globals.IsNullorEmpty(formattedPost))
                return formattedPost;


			RegexOptions options = RegexOptions.IgnoreCase | RegexOptions.Compiled;
			ArrayList censors = Censors.GetCensors();

			foreach( Censor censor in censors ) {
				string searchPattern; 

				// if the string contains any regex patterns, then just use the pattern
				if( -1 == censor.Word.IndexOfAny( new char[]{'\\', '[', '^', '*', '{', '.', '#', '?', '+', '$', '|' } ) ) {
					searchPattern = String.Format(@"\b{0}\b", censor.Word);
				}
				else {
					searchPattern = censor.Word;
				}

				formattedPost = Regex.Replace(formattedPost, searchPattern, censor.Replacement, options);
			}
			return formattedPost;
		}
		#endregion

		#region UnHtmlEncode
		// *******************************************************************
		// UnHtmlEncode
		//
		/// <summary>
		/// If a post has been HtmlEncoded using the Web.HttpUtility, there are
		/// a few characters we don't want encoded.  This will allow for the 
		/// bbcode transforms to properly work.  So we must "UnHtmlEncode" these
		/// items, back to what they originally were.
		/// </summary>
		/// <param name="formattedPost">string of post</param>
		/// <returns>
		/// An Un-HtmlEncoded String of select items.
		/// </returns>
		// *******************************************************************
		public static string UnHtmlEncode(string formattedPost) {
			RegexOptions options = RegexOptions.IgnoreCase | RegexOptions.Compiled;

			// &quot;
			// TODO: edit the regex match to only replace within a set of brackets [].
			//
			formattedPost = Regex.Replace(formattedPost, "&quot;", "\"", options);
			
			return formattedPost;
		}
		#endregion

		#region BBDecode
        // *********************************************************************
        //  BBDecode
        //
        /// <summary>
        /// Transforms a BBCode encoded string in appropriate HTML
        /// </summary>
        /// 
        // ********************************************************************/
		public static string BBcodeToHtml(string encodedString) 
		{
			// TODO move all this formatting to an external style
			//
 
			// Used for normal quoting with a "<pic> <username> wrote:" prefix.
			//
			string quoteStartHtml = ""; 
			string quoteEndHtml = "</td></tr></table></td></tr></table></BLOCKQUOTE>";
			//quoteStartHtml = "<BLOCKQUOTE><table width=\"85%\"><tr><td class=\"txt4\"><img src=\"" + Globals.GetSkinPath() +"/images/icon-quote.gif" + "\">&nbsp;<strong>$1 wrote:</strong></td></tr><tr><td class=\"quoteTable\"><table width=\"100%\"><tr><td width=\"100%\" valign=\"top\" class=\"txt4\">$3</td></tr></table></td></tr></table></BLOCKQUOTE>";
 
			// Used for when a username is not supplied.
			//
			string emptyquoteStartHtml = "<BLOCKQUOTE><table width=\"85%\"><tr><td class=\"quoteTable\"><table width=\"100%\"><tr><td width=\"100%\" valign=\"top\" class=\"txt4\">";
			string emptyquoteEndHtml = "</td></tr></table></td></tr></table></BLOCKQUOTE>";
			//string emptyquoteStartHtml = "<BLOCKQUOTE><table width=\"85%\"><tr><td class=\"quoteTable\"><table width=\"100%\"><tr><td width=\"100%\" valign=\"top\" class=\"txt4\">$2</td></tr></table></td></tr></table></BLOCKQUOTE>";
 
			// When using the Importer, we do not have a skin path.  We hardcode it here to
			// the default path.
			//
			if (Globals.GetSkinPath() == "")
				quoteStartHtml = "<BLOCKQUOTE><table width=\"85%\"><tr><td class=\"txt4\"><img src=\"themes/default/images/icon-quote.gif" + "\">&nbsp;<strong>$1 wrote:</strong></td></tr><tr><td class=\"quoteTable\"><table width=\"100%\"><tr><td width=\"100%\" valign=\"top\" class=\"txt4\">";
			else
				quoteStartHtml = "<BLOCKQUOTE><table width=\"85%\"><tr><td class=\"txt4\"><img src=\"" + Globals.GetSkinPath() +"/images/icon-quote.gif" + "\">&nbsp;<strong>$1 wrote:</strong></td></tr><tr><td class=\"quoteTable\"><table width=\"100%\"><tr><td width=\"100%\" valign=\"top\" class=\"txt4\">";
 
			string quote = quoteStartHtml + "$3" + quoteEndHtml;
			string emptyquote = emptyquoteStartHtml + "$2" + emptyquoteEndHtml;
 
			RegexOptions options = RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.Singleline;
 
			// Bold, Italic, Underline
			//(
			encodedString = Regex.Replace(encodedString, @"\[b(?:\s*)\]((.|\n)*?)\[/b(?:\s*)\]", "<b>$1</b>", options);
			encodedString = Regex.Replace(encodedString, @"\[i(?:\s*)\]((.|\n)*?)\[/i(?:\s*)\]", "<i>$1</i>", options);
			encodedString = Regex.Replace(encodedString, @"\[u(?:\s*)\]((.|\n)*?)\[/u(?:\s*)\]", "<u>$1</u>", options);
 
			// marquee
			encodedString = Regex.Replace(encodedString, @"\[fly(?:\s*)\]((.|\n)*?)\[/fly(?:\s*)\]", "<marquee>$1</marquee>", options);
			encodedString = Regex.Replace(encodedString, @"\[marquee(?:\s*)\]((.|\n)*?)\[/marquee(?:\s*)\]", "<marquee>$1</marquee>", options);
 
			// color
			encodedString = Regex.Replace(encodedString, @"\[color=""((.|\n)*?)(?:\s*)""\]((.|\n)*?)\[/color(?:\s*)\]", "<font color=\"$1\">$3</font>", options );
			encodedString = Regex.Replace(encodedString, @"\[color=((.|\n)*?)(?:\s*)\]((.|\n)*?)\[/color(?:\s*)\]", "<font color=\"$1\">$3</font>", options );
 
			// Left, Right, Center
			encodedString = Regex.Replace(encodedString, @"\[left(?:\s*)\]((.|\n)*?)\[/left(?:\s*)]", "<div style=\"text-align:left\">$1</div>", options);
			encodedString = Regex.Replace(encodedString, @"\[center(?:\s*)\]((.|\n)*?)\[/center(?:\s*)]", "<div style=\"text-align:center\">$1</div>", options);
			encodedString = Regex.Replace(encodedString, @"\[right(?:\s*)\]((.|\n)*?)\[/right(?:\s*)]", "<div style=\"text-align:right\">$1</div>", options);
 
			// Quote
			//
			encodedString = Regex.Replace(encodedString, "\\[quote(?:\\s*)user=\"((.|\n)*?)\"\\]((.|\n)*?)\\[/quote(\\s*)\\]", quote, options);
			encodedString = Regex.Replace(encodedString, "\\[quote(\\s*)\\]((.|\n)*?)\\[/quote(\\s*)\\]", emptyquote, options);
			//   encodedString = Regex.Replace(encodedString, @"\[quote(?:\s*)user=""((.|\n)*?)""\]", quoteStartHtml, options);
			//   encodedString = Regex.Replace(encodedString, "\\[/quote(\\s*)\\]", quoteEndHtml, options);
			//   encodedString = Regex.Replace(encodedString, "\\[quote(\\s*)\\]", emptyquoteStartHtml, options);
			//   encodedString = Regex.Replace(encodedString, "\\[/quote(\\s*)\\]", emptyquoteEndHtml, options);
 
 
			// Anchors
			//
			//            encodedString = Regex.Replace(encodedString, "<a href=\"((.|\n)*?)\">((.|\n)*?)</a>", "<a target=\"_blank\" title=\"$1\" href=\"$1\">$3</a>", options);
			encodedString = Regex.Replace(encodedString, @"\[url(?:\s*)\]www\.(.*?)\[/url(?:\s*)\]", "<a href=\"http://www.$1\" target=\"_blank\" title=\"$1\">$1</a>", options );
			encodedString = Regex.Replace(encodedString, @"\[url(?:\s*)\]((.|\n)*?)\[/url(?:\s*)\]", "<a href=\"$1\" target=\"_blank\" title=\"$1\">$1</a>", options );
			encodedString = Regex.Replace(encodedString, @"\[url=""((.|\n)*?)(?:\s*)""\]((.|\n)*?)\[/url(?:\s*)\]", "<a href=\"$1\" target=\"_blank\" title=\"$1\">$3</a>", options );
			encodedString = Regex.Replace(encodedString, @"\[url=((.|\n)*?)(?:\s*)\]((.|\n)*?)\[/url(?:\s*)\]", "<a href=\"$1\" target=\"_blank\" title=\"$1\">$3</a>", options );
			encodedString = Regex.Replace(encodedString, @"\[link(?:\s*)\]((.|\n)*?)\[/link(?:\s*)\]", "<a href=\"$1\" target=\"_blank\" title=\"$1\">$1</a>", options );
			encodedString = Regex.Replace(encodedString, @"\[link=((.|\n)*?)(?:\s*)\]((.|\n)*?)\[/link(?:\s*)\]", "<a href=\"$1\" target=\"_blank\" title=\"$1\">$3</a>", options );
 
			// Image
			//
			encodedString = Regex.Replace(encodedString, @"\[img(?:\s*)\]((.|\n)*?)\[/img(?:\s*)\]", "<img src=\"$1\" border=\"0\" />", options);
			encodedString = Regex.Replace(encodedString, @"\[img=((.|\n)*?)x((.|\n)*?)(?:\s*)\]((.|\n)*?)\[/img(?:\s*)\]", "<img width=\"$1\" height=\"$3\" src=\"$5\" border=\"0\" />", options );
 
			// Flash
			//
			encodedString = Regex.Replace(encodedString, @"\[flash(?:\s*)\]((.|\n)*?)\[/flash(?:\s*)\]", "<embed src=\"$1\" quality=\"high\" pluginspage=\"http://www.macromedia.com/go/getflashplayer\" type=\"application/x-shockwave-flash\" width=\"550\" height=\"400\"></embed>", options);
			encodedString = Regex.Replace(encodedString, @"\[flash=((.|\n)*?)x((.|\n)*?)(?:\s*)\]((.|\n)*?)\[/flash(?:\s*)\]", "<embed src=\"$5\" quality=\"high\" pluginspage=\"http://www.macromedia.com/go/getflashplayer\" type=\"application/x-shockwave-flash\" width=\"$1\" height=\"$3\"></embed>", options );
			encodedString = Regex.Replace(encodedString, @"\[swf(?:\s*)\]((.|\n)*?)\[/swf(?:\s*)\]", "<embed src=\"$1\" quality=\"high\" pluginspage=\"http://www.macromedia.com/go/getflashplayer\" type=\"application/x-shockwave-flash\" width=\"550\" height=\"400\"></embed>", options);
			encodedString = Regex.Replace(encodedString, @"\[swf=((.|\n)*?)x((.|\n)*?)(?:\s*)\]((.|\n)*?)\[/swf(?:\s*)\]", "<embed src=\"$5\" quality=\"high\" pluginspage=\"http://www.macromedia.com/go/getflashplayer\" type=\"application/x-shockwave-flash\" width=\"$1\" height=\"$3\"></embed>", options );
 
			// WMV
			//
			encodedString = Regex.Replace(encodedString, @"\[wmv(?:\s*)\]((.|\n)*?)\[/wmv(?:\s*)\]", "<embed src=\"$1\" width=\"314\" height=\"256\" autostart=\"0\"></embed>", options);
			encodedString = Regex.Replace(encodedString, @"\[wmv=((.|\n)*?)x((.|\n)*?)(?:\s*)\]((.|\n)*?)\[/wmv(?:\s*)\]", "<embed src=\"$5\" autostart=\"0\" width=\"$1\" height=\"$3\"></embed>", options );
 
			// mid
			//
			encodedString = Regex.Replace(encodedString, @"\[mid(?:\s*)\]((.|\n)*?)\[/mid(?:\s*)\]", "<embed src=\"$1\" width=\"314\" height=\"45\" autostart=\"0\"></embed>", options);
 
			// ra
			//
			encodedString = Regex.Replace(encodedString, @"\[ra(?:\s*)\]((.|\n)*?)\[/ra(?:\s*)\]", "<object classid=\"clsid:CFCDAA03-8BE4-11CF-B84B-0020AFBBCCFA\" id=\"RAOCX\" width=\"253\" height=\"60\"><param name=\"_ExtentX\" value=\"6694\"><param name=\"_ExtentY\" value=\"1588\"><param name=\"AUTOSTART\" value=\"0\"><param name=\"SHUFFLE\" value=\"0\"><param name=\"PREFETCH\" value=\"0\"><param name=\"NOLABELS\" value=\"0\"><param name=\"SRC\" value=\"$1\"><param name=\"CONTROLS\" value=\"StatusBar,ControlPanel\"><param name=\"LOOP\" value=\"0\"><param name=\"NUMLOOP\" value=\"0\"><param name=\"CENTER\" value=\"0\"><param name=\"MAINTAINASPECT\" value=\"0\"><param name=\"BACKGROUNDCOLOR\" value=\"#000000\"><embed src=\"$1\" width=\"253\" autostart=\"true\" height=\"60\"></embed></object>", options);
 
			// rm
			//
			encodedString = Regex.Replace(encodedString, @"\[rm(?:\s*)\]((.|\n)*?)\[/rm(?:\s*)\]", "<object classid=\"clsid:CFCDAA03-8BE4-11cf-B84B-0020AFBBCCFA\" height=\"241\" id=\"Player\" width=\"316\" viewastext><param name=\"_ExtentX\" value=\"12726\"><param name=\"_ExtentY\" value=\"8520\"><param name=\"AUTOSTART\" value=\"0\"><param name=\"SHUFFLE\" value=\"0\"><param name=\"PREFETCH\" value=\"0\"><param name=\"NOLABELS\" value=\"0\"><param name=\"CONTROLS\" value=\"ImageWindow\"><param name=\"CONSOLE\" value=\"_master\"><param name=\"LOOP\" value=\"0\"><param name=\"NUMLOOP\" value=\"0\"><param name=\"CENTER\" value=\"0\"><param name=\"MAINTAINASPECT\" value=\"316\"><param name=\"BACKGROUNDCOLOR\" value=\"#000000\"></object><br><object classid=\"clsid:CFCDAA03-8BE4-11cf-B84B-0020AFBBCCFA\" height=\"32\" id=\"Player\" width=\"316\" VIEWASTEXT><param name=\"_ExtentX\" value=\"18256\"><param name=\"_ExtentY\" value=\"794\"><param name=\"AUTOSTART\" value=\"-1\"><param name=\"SHUFFLE\" value=\"0\"><param name=\"PREFETCH\" value=\"0\"><param name=\"NOLABELS\" value=\"0\"><param name=\"CONTROLS\" value=\"controlpanel\"><param name=\"CONSOLE\" value=\"_master\"><param name=\"LOOP\" value=\"0\"><param name=\"NUMLOOP\" value=\"0\"><param name=\"CENTER\" value=\"0\"><param name=\"MAINTAINASPECT\" value=\"0\"><param name=\"BACKGROUNDCOLOR\" value=\"#000000\"><param name=\"SRC\" value=\"$1\"></object>", options);
			encodedString = Regex.Replace(encodedString, @"\[rm=((.|\n)*?)x((.|\n)*?)(?:\s*)\]((.|\n)*?)\[/rm(?:\s*)\]", "<object classid=\"clsid:CFCDAA03-8BE4-11cf-B84B-0020AFBBCCFA\" height=\"$3\" id=\"Player\" width=\"$1\" viewastext><param name=\"_ExtentX\" value=\"12726\"><param name=\"_ExtentY\" value=\"8520\"><param name=\"AUTOSTART\" value=\"0\"><param name=\"SHUFFLE\" value=\"0\"><param name=\"PREFETCH\" value=\"0\"><param name=\"NOLABELS\" value=\"0\"><param name=\"CONTROLS\" value=\"ImageWindow\"><param name=\"CONSOLE\" value=\"_master\"><param name=\"LOOP\" value=\"0\"><param name=\"NUMLOOP\" value=\"0\"><param name=\"CENTER\" value=\"0\"><param name=\"MAINTAINASPECT\" value=\"$5\"><param name=\"BACKGROUNDCOLOR\" value=\"#000000\"></object><br><object classid=\"clsid:CFCDAA03-8BE4-11cf-B84B-0020AFBBCCFA\" height=\"32\" id=\"Player\" width=\"$1\" VIEWASTEXT><param name=\"_ExtentX\" value=\"18256\"><param name=\"_ExtentY\" value=\"794\"><param name=\"AUTOSTART\" value=\"-1\"><param name=\"SHUFFLE\" value=\"0\"><param name=\"PREFETCH\" value=\"0\"><param name=\"NOLABELS\" value=\"0\"><param name=\"CONTROLS\" value=\"controlpanel\"><param name=\"CONSOLE\" value=\"_master\"><param name=\"LOOP\" value=\"0\"><param name=\"NUMLOOP\" value=\"0\"><param name=\"CENTER\" value=\"0\"><param name=\"MAINTAINASPECT\" value=\"0\"><param name=\"BACKGROUNDCOLOR\" value=\"#000000\"><param name=\"SRC\" value=\"$5\"></object>", options );
 

			// Color
			//
			encodedString = Regex.Replace(encodedString, @"\[color=((.|\n)*?)(?:\s*)\]((.|\n)*?)\[/color(?:\s*)\]", "<span style=\"color=$1;\">$3</span>", options);
 
			// Horizontal Rule
			//
			//            encodedString = Regex.Replace(encodedString, @"\[hr(?:\s*)\]", "<hr />", options);
        
			// Email
			//
			encodedString = Regex.Replace(encodedString, @"\[email(?:\s*)\]((.|\n)*?)\[/email(?:\s*)\]", "<a href=\"mailto:$1\">$1</a>", options);
 
			// Font size
			//
			encodedString = Regex.Replace(encodedString, @"\[size=((.|\n)*?)(?:\s*)\]((.|\n)*?)\[/size(?:\s*)\]", "<span style=\"font-size:$1\">$3</span>", options);
			encodedString = Regex.Replace(encodedString, @"\[font=((.|\n)*?)(?:\s*)\]((.|\n)*?)\[/font(?:\s*)\]", "<span style=\"font-family:$1;\">$3</span>", options );
			encodedString = Regex.Replace(encodedString, @"\[align=((.|\n)*?)(?:\s*)\]((.|\n)*?)\[/align(?:\s*)\]", "<div style=\"text-align:$1;\">$3</span>", options );
			encodedString = Regex.Replace(encodedString, @"\[float=((.|\n)*?)(?:\s*)\]((.|\n)*?)\[/float(?:\s*)\]", "<div style=\"float:$1;\">$3</div>", options );
 
			string sListFormat = "<ol class=\"anf_list\" style=\"list-style:{0};\">$1</ol>";
			// Lists
			encodedString = Regex.Replace(encodedString, @"\[\*(?:\s*)]\s*([^\[]*)", "<li>$1</li>", options );
			encodedString = Regex.Replace(encodedString, @"\[list(?:\s*)\]((.|\n)*?)\[/list(?:\s*)\]", "<ul class=\"anf_list\">$1</ul>", options );
			encodedString = Regex.Replace(encodedString, @"\[list=1(?:\s*)\]((.|\n)*?)\[/list(?:\s*)\]", string.Format( sListFormat, "decimal" ), options );
			encodedString = Regex.Replace(encodedString, @"\[list=i(?:\s*)\]((.|\n)*?)\[/list(?:\s*)\]", string.Format( sListFormat, "lower-roman" ), RegexOptions.Compiled );
			encodedString = Regex.Replace(encodedString, @"\[list=I(?:\s*)\]((.|\n)*?)\[/list(?:\s*)\]", string.Format( sListFormat, "upper-roman" ), RegexOptions.Compiled );
			encodedString = Regex.Replace(encodedString, @"\[list=a(?:\s*)\]((.|\n)*?)\[/list(?:\s*)\]", string.Format( sListFormat, "lower-alpha" ), RegexOptions.Compiled );
			encodedString = Regex.Replace(encodedString, @"\[list=A(?:\s*)\]((.|\n)*?)\[/list(?:\s*)\]", string.Format( sListFormat, "upper-alpha" ), RegexOptions.Compiled );
 
			return encodedString;
		}
		#endregion

		#region Strip Tags
        public static string StripForPreview (string content) {
            content = Regex.Replace(content, "<br>", "\n", RegexOptions.IgnoreCase | RegexOptions.Compiled);
			content = Regex.Replace(content, "<br/>", "\n", RegexOptions.IgnoreCase | RegexOptions.Compiled);
			content = Regex.Replace(content, "<br />", "\n", RegexOptions.IgnoreCase | RegexOptions.Compiled);
            content = Regex.Replace(content, "<p>", "\n", RegexOptions.IgnoreCase | RegexOptions.Compiled);
            content = content.Replace("'", "&#39;");

            return StripHtmlXmlTags(content);
        }

        public static string StripHtmlXmlTags (string content) {
            return Regex.Replace(content, "<[^>]+>", "", RegexOptions.IgnoreCase | RegexOptions.Compiled);
        }

        // *********************************************************************
        //  StripScriptTags
        //
        /// <summary>
        /// Helper function used to ensure we don't inject script into the db.
        /// </summary>
        /// <param name="dirtyText">Text to be cleaned for script tags</param>
        /// <returns>Clean text with no script tags.</returns>
        /// 
        // ********************************************************************/
        public static string StripScriptTags(string content) {
            string cleanText;

            // Perform RegEx
            content = Regex.Replace(content, "<script((.|\n)*?)</script>", "", RegexOptions.IgnoreCase | RegexOptions.Multiline);
            cleanText = Regex.Replace(content, "\"javascript:", "", RegexOptions.IgnoreCase | RegexOptions.Multiline);

            return cleanText;
        }
		#endregion

		#region Emoticon & User Transforms
        public static string EmoticonTransforms (string formattedPost) {

            // remove some overhead if this is turned off
            if (!CSContext.Current.SiteSettings.EnableEmoticons)
                return formattedPost;

            try {
                // Load the emoticon transform table
                //
				ArrayList emoticonTxTable = Smilies.GetSmilies();
				
				const string imgFormat = "<img src=\"{0}{1}\" alt=\"{2}\" />";

				// EAD 6/27/2004: Changed to loop through twice.
				// Once for brackets first, so to capture the Party emoticon
				// (special emoticons that REQUIRE brackets), so not to replace
				// with other non-bracket icons. Less efficient yes, but captures 
				// all emoticons properly.
				//
				string smileyPattern	= "";
				string replacePattern	= "";
				RegexOptions options	= RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.Multiline ;
                string forumHomePath = Globals.GetSiteUrls().Emoticon;

				foreach ( Smiley smiley in emoticonTxTable ) {
					smileyPattern	= string.Format(@"\[{0}\]", Regex.Escape(smiley.SmileyCode) );
					replacePattern	= string.Format(imgFormat, forumHomePath, smiley.SmileyUrl, smiley.SmileyText + " [[SmileyCode]]");

					formattedPost = Regex.Replace( formattedPost, smileyPattern, replacePattern, options );
			
					if ( smiley.IsSafeWithoutBrackets() )
					{
						formattedPost = Regex.Replace( formattedPost, Regex.Escape(smiley.SmileyCode), 
								replacePattern, 
								options ); 
					}

					// If the smiley code contains a < or >, check to see if it was HtmlEncoded in the body
					if(smiley.SmileyCode.IndexOfAny( new char[] { '<', '>', '&' }) != -1)
					{
						try
						{
							smileyPattern	= string.Format(@"\[{0}\]", Regex.Escape(HttpContext.Current.Server.HtmlEncode(smiley.SmileyCode)) );
							formattedPost = Regex.Replace( formattedPost, smileyPattern, replacePattern, options );

							if(smiley.IsSafeWithoutBrackets())
							{
								formattedPost = Regex.Replace( formattedPost, Regex.Escape(HttpContext.Current.Server.HtmlEncode(smiley.SmileyCode)), 
									replacePattern, 
									options ); 
							}
						}
						catch(NullReferenceException) { }	// If we are outside of a web-context, referencing HttpContext.Current could throw a null ref.
					}

					// Finally, look for [SmileyCode] to place the smiley's code back in there (mostly for in the alt tags)
					// If the smiley was actually there, it could end up getting mistaken for another smiley... ie, [:)]
					// would get an alt tag of "Smile [:)]", it'll then search for the non-bracket version, and see :) and treat it as another instance
					formattedPost = Regex.Replace( formattedPost, @"\[SmileyCode\]", smiley.SmileyCode, options );

				}

				return formattedPost;
            } catch( Exception e ) {
                CSException ex = new CSException( CSExceptionType.UnknownError, "Could not transform smilies in the post.", e );
				ex.Log();

				return formattedPost;
            }
        }

        // *********************************************************************
        //  PerformUserTransforms
        //
        /// <summary>
        /// Performs the user defined transforms
        /// </summary>
        /// 
        // ********************************************************************/
        static string PerformUserTransforms(string stringToTransform, ArrayList userDefinedTransforms) {
            int iLoop = 0;			

            while (iLoop < userDefinedTransforms.Count) {		
		        
                // Special work for anchors
                stringToTransform = Regex.Replace(stringToTransform, userDefinedTransforms[iLoop].ToString(), userDefinedTransforms[iLoop+1].ToString(), RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.Multiline);

                iLoop += 2;
            }

            return stringToTransform;
        }


        // *********************************************************************
        //  LoadTransformFile
        //
        /// <summary>
        /// Returns an array list containing transforms that the user defined in a transform file.
        /// </summary>
        /// 
        // ********************************************************************/
        private static ArrayList LoadTransformFile (string filename) {
            string cacheKey = "transformTable-" + filename;
            ArrayList tranforms;
            string filenameOfTransformFile;

            // read the transformation hashtable from the cache
            //
            tranforms = CSCache.Get(cacheKey) as ArrayList;

            if (tranforms == null) {

                tranforms = new ArrayList();

                // Grab the transform file
                //
                filenameOfTransformFile = CSContext.Current.Context.Request.MapPath("~/" + filename);

                if (filenameOfTransformFile.Length > 0) {

                    StreamReader sr = File.OpenText( filenameOfTransformFile );

                    // Read through each set of lines in the text file
                    //
                    string line = sr.ReadLine(); 
                    string replaceLine = "";

                    while (line != null) {

                        line = Regex.Escape(line);
                        replaceLine = sr.ReadLine();

                        // make sure replaceLine != null
                        //
                        if (replaceLine == null) 
                            break;
					
                        line = line.Replace("<CONTENTS>", "((.|\n)*?)");
                        line = line.Replace("<WORDBOUNDARY>", "\\b");
                        line = line.Replace("<", "&lt;");
                        line = line.Replace(">", "&gt;");
                        line = line.Replace("\"", "&quot;");

                        replaceLine = replaceLine.Replace("<CONTENTS>", "$1");					
					
                        tranforms.Add(line);
                        tranforms.Add(replaceLine);

                        line = sr.ReadLine();

                    }

                    // close the streamreader
                    //
                    sr.Close();		

                    // slap the ArrayList into the cache and set its dependency to the transform file.
                    //
                    CSCache.Insert(cacheKey, tranforms, new CacheDependency(filenameOfTransformFile));
                }
            }
  
            return tranforms;
        }
		#endregion

		#region FormatEditNotes()
        private static string FormatEditNotes2(string stringToTransform) {

            Match match;
            StringBuilder editTable = null;

            match = Regex.Match(stringToTransform, ".Edit by=&quot;(?<Editor>(.|\\n)*?)&quot;.(?<Notes>(.|\\n)*?)./Edit.", RegexOptions.IgnoreCase | RegexOptions.Compiled);

            if (match.Captures.Count > 0) {

                editTable = new StringBuilder();

                editTable.Append( "<table>" );
                editTable.Append( "<tr>" );
                editTable.Append( "<td>" );
                editTable.Append( match.Groups["Editor"].ToString() );
                editTable.Append( "</td>" );
                editTable.Append( "</tr>" );
                editTable.Append( "<tr>" );
                editTable.Append( "<td>" );
                editTable.Append( match.Groups["Notes"].ToString() );
                editTable.Append( "</td>" );
                editTable.Append( "</tr>" );
                editTable.Append( "</table>" );
            }
            if (editTable != null)
                return stringToTransform.Replace(match.ToString(), editTable.ToString());
            else
                return stringToTransform;
        }
		#endregion

		#region SourceCodeMarkup
        private static string SourceCodeMarkup(string stringToTransform) {
			StringBuilder formattedSource = new StringBuilder();
            string[] table = new string[3];

            table[0] = "<table border=\"0\" cellspacing=\"0\" width=\"100%\">";
            table[1] = "<tr><td width=\"15\"></td><td bgcolor=\"lightgrey\" width=\"15\"></td><td bgcolor=\"lightgrey\"><br>";
            table[2] = "<br>&nbsp;</td></tr></table>";
            MatchCollection matchs;

            // Look for code
            //
            matchs = Regex.Matches(stringToTransform, "\\[code language=\"(?<lang>(.|\\n)*?)\"\\](?<code>(.|\\n)*?)\\[/code\\]", RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.Compiled);

            foreach (Match match in matchs) {

                // Remove HTML formatting code
                //
                string codeToFormat = match.Groups["code"].ToString().Replace("</P>\r\n", "<br>").Replace("<P>","");

                // Get the formatted source code
                //
                formattedSource.Append(table[0]);
                formattedSource.Append(table[1]);
                formattedSource.Append(FormatSource(codeToFormat, GetLanguageType(match.Groups["lang"].ToString())).Replace(Globals.HtmlNewLine, ""));
                formattedSource.Append(table[2]);

                // Update the main string
                //
                stringToTransform = stringToTransform.Replace(match.ToString(), formattedSource.ToString());
				formattedSource.Remove( 0, formattedSource.Length );
            }

            return stringToTransform;
        }
		#endregion

		#region PerformSpecializedTransforms
        private static string PerformSpecializedTransforms(string stringToTransform) {
            StringBuilder stringToReturn = new StringBuilder();
//            MatchCollection matchs;

            stringToTransform = SourceCodeMarkup(stringToTransform);

            return stringToTransform;

			// TDD 3/16/2004
			// commenting out the rest of this code since it's unreachable. If not needed after release then we can remove it.
//            // First we need to crack the string into segments to be transformed
//            // there is only 1 special marker we care about: [code language="xxx"][/code]
//            matchs = Regex.Matches(stringToTransform, "\\[code language=&quot;(?<lang>(.|\\n)*?)&quot;\\](?<code>(.|\\n)*?)\\[/code\\]", RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.Compiled);
//
//            // Get the full string that we intend to return
//            //
//            stringToReturn.Append(stringToTransform);
//
//            foreach (Match match in matchs) {
//                char[] charCode = new char[] {'\r', '\n'};
//                string sourceCodeToMarkup;
//                string markedUpSourceCode;
//
//                // Code/Markup content
//                //
//                sourceCodeToMarkup = match.Groups["code"].ToString().TrimStart(charCode).TrimEnd(charCode);
//                //				markedUpSourceCode = FormatSource(sourceCodeToMarkup, ));
//
//                // Replace marked up code
//                //
//                string openTable = "<table width=\"100%\"><tr><td width=\"15\"></td><td bgcolor=\"lightgrey\">";
//                string closeTable = "</td></tr></table>";
//                stringToReturn = stringToReturn.Replace(sourceCodeToMarkup, openTable + markedUpSourceCode + closeTable);
//
//                // Remove the code tags
//                //
//                string openCodeTag = "[code language=&quot;" + match.Groups["lang"] + "&quot;]";
//                string closeCodeTag = "[/code]";
//
//                stringToReturn = stringToReturn.Replace(openCodeTag, ""); 
//                stringToReturn = stringToReturn.Replace(closeCodeTag, ""); 
//
//            }
//
//            return stringToReturn.ToString();
//
        }
		#endregion

		#region Private vars, enums, and helper functions
        static Language GetLanguageType(string language) {
            
            switch (language.ToUpper()) {
                case "VB" :
                    return Language.VB;
                case "JS" :
                    return Language.JScript;
                case "JScript" :
                    return Language.JScript;
                default :
                    return Language.CS;
            }
        }

        public enum Language {
            CS,
            VB,
            JScript
        }

        const int _fontsize = 2;
        const string TAG_FNTRED  = "<font color= \"red\">";
        const string TAG_FNTBLUE = "<font color= \"blue\">" ;
        const string TAG_FNTGRN  = "<font color= \"green\">" ;
        const string TAG_FNTMRN  = "<font color=\"maroon\">" ;
        const string TAG_EFONT   = "</font>" ;


        public static string FormatSource(string htmlEncodedSource, Language language) {

            StringWriter textBuffer = new StringWriter();

            textBuffer.Write("<font face=\"Lucida Console\" size=\"" + _fontsize + "\">");

            if(language == Language.CS) {
                textBuffer.Write(FixCSLine(htmlEncodedSource)) ;
            } else if(language == Language.JScript) {
                textBuffer.Write(FixJSLine(htmlEncodedSource)) ;
            } else if(language == Language.VB) {
                textBuffer.Write(FixVBLine(htmlEncodedSource)) ;
            }

            textBuffer.Write("</font>");

            return textBuffer.ToString();
        }

        static string FixCSLine(string source) {

            if (source == null)
                return null;

			source = Regex.Replace(source, @"(\<br(\s*|(\&nbsp;)*)\/{0,1}\>)", @"<br/>");
			return source;
			
/*
			// TDD I'm commenting this out for now for the 2.0.1 release. At a later point we'll figure out what is wrong with the color
			// syntax hightlighting.
						source = Regex.Replace(source, "(?i)(\\t)", "    ");
            source = Regex.Replace(source, "(?i) ", "&nbsp;");

            String[] keywords = new String[] {
                                                 "private", "protected", "public", "namespace", "class", "break",
                                                 "for", "if", "else", "while", "switch", "case", "using",
                                                 "return", "null", "void", "int", "bool", "string", "float",
                                                 "this", "new", "true", "false", "const", "static", "base",
                                                 "foreach", "in", "try", "catch", "finally", "get", "set", "char", "default"};

            String CombinedKeywords = "(?<keyword>" + String.Join("|", keywords) + ")";

            source = Regex.Replace(source, "\\b" + CombinedKeywords + "\\b(?<!//.*)", TAG_FNTBLUE + "${keyword}" + TAG_EFONT);
            source = Regex.Replace(source, "(?<comment>//.*$)", TAG_FNTGRN + "${comment}" + TAG_EFONT);

            return source;
*/
		}

        static string FixJSLine(string source) {
            if (source == null)
                return null;

			source = Regex.Replace(source, @"(\<br(\s*|(\&nbsp;)*)\/{0,1}\>)", @"<br/>");
			return source;
			
/*
			// TDD I'm commenting this out for now for the 2.0.1 release. At a later point we'll figure out what is wrong with the color
			// syntax hightlighting.
						source = Regex.Replace(source, "(?i)(\\t)", "    ");

            String[] keywords = new String[] {
                                                 "private", "protected", "public", "namespace", "class", "var",
                                                 "for", "if", "else", "while", "switch", "case", "using", "get",
                                                 "return", "null", "void", "int", "string", "float", "this", "set",
                                                 "new", "true", "false", "const", "static", "package", "function",
                                                 "internal", "extends", "super", "import", "default", "break", "try",
                                                 "catch", "finally" };

            String CombinedKeywords = "(?<keyword>" + String.Join("|", keywords) + ")";

            source = Regex.Replace(source, "\\b" + CombinedKeywords + "\\b(?<!//.*)", TAG_FNTBLUE + "${keyword}" + TAG_EFONT);
            source = Regex.Replace(source, "(?<comment>//.*$)", TAG_FNTGRN + "${comment}" + TAG_EFONT);

            return source;
*/
		}

        static string FixVBLine(string source) {
            if (source == null)
                return null;

			source = Regex.Replace(source, @"(\<br(\s*|(\&nbsp;)*)\/{0,1}\>)", @"<br/>");
			return source;
			
/*
			// TDD I'm commenting this out for now for the 2.0.1 release. At a later point we'll figure out what is wrong with the color
			// syntax hightlighting.
						source = Regex.Replace(source, "(?i)(\\t)", "    ");

            String[] keywords = new String[] {
                                                 "Private", "Protected", "Public", "End Namespace", "Namespace",
                                                 "End Class", "Exit", "Class", "Goto", "Try", "Catch", "End Try",
                                                 "For", "End If", "If", "Else", "ElseIf", "Next", "While", "And",
                                                 "Do", "Loop", "Dim", "As", "End Select", "Select", "Case", "Or",
                                                 "Imports", "Then", "Integer", "Long", "String", "Overloads", "True",
                                                 "Overrides", "End Property", "End Sub", "End Function", "Sub", "Me",
                                                 "Function", "End Get", "End Set", "Get", "Friend", "Inherits",
                                                 "Implements","Return", "Not", "New", "Shared", "Nothing", "Finally",
                                                 "False", "Me", "My", "MyBase" };


            String CombinedKeywords = "(?<keyword>" + String.Join("|", keywords) + ")";

            source = Regex.Replace(source, "(?i)\\b" + CombinedKeywords + "\\b(?<!'.*)", TAG_FNTBLUE + "${keyword}" + TAG_EFONT);
            source = Regex.Replace(source, "(?<comment>'(?![^']*&quot;).*$)", TAG_FNTGRN + "${comment}" + TAG_EFONT);

            return source;
*/
		}

		// *********************************************************************
		//  ToDelimitedString
		//
		/// <summary>
		/// Private helper function to convert a collection to delimited string array
		/// </summary>
		/// 
		// ********************************************************************/
		public static string ToDelimitedString(ICollection collection, string delimiter) {

			StringBuilder delimitedString = new StringBuilder();

			// Hashtable is perfomed on Keys
			//
			if (collection is Hashtable) {

				foreach (object o in ((Hashtable) collection).Keys) {
					delimitedString.Append( o.ToString() + delimiter);
				}
			}

			// ArrayList is performed on contained item
			//
			if (collection is ArrayList) {
				foreach (object o in (ArrayList) collection) {
					delimitedString.Append( o.ToString() + delimiter);
				}
			}

			// String Array is performed on value
			//
			if (collection is String[]) {
				foreach (string s in (String[]) collection) {
					delimitedString.Append( s + delimiter);
				}
			}

			return delimitedString.ToString().TrimEnd(Convert.ToChar(delimiter));
		}
		#endregion

		#region HtmlToBBCode
		/// <summary>
		/// This method will transform a HTML tagged string into a bbcoded string. We first convert all HTML tags to bbcode prior
		/// to performing our bbcode translations to strip out any dangerous attributes.
		/// </summary>
		/// <param name="bbCodedString"></param>
		/// <returns></returns>
		static string HtmlToBBCode( string htmlTaggedString ) {
			return Regex.Replace( htmlTaggedString, @"(?'openingTag'<)(.*?)(?>(?!=[\/\?]?>)(?'closingSlash'\/\?)?(?'closingTag'>))", @"[$1${closingSlash}]", RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.IgnorePatternWhitespace );
		}
		#endregion

		#region EncodeCodeBlocks
		public static string EncodeCodeBlocks( string taggedString ) {
			StringBuilder sb = new StringBuilder();
			string output = "";
			bool encodeThisBlock = false;
			//			string[] blocks = Regex.Split(taggedString, @"\[(?:code|pre).*\].*\[\/(?:code|pre).*\]", RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.IgnorePatternWhitespace );
			string[] blocks = Regex.Split(taggedString, @"(\[(?:\/)?code\s*(?:language\s*=\s*"".*"")?\s*\])", RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.IgnorePatternWhitespace );
			string language = "";
			foreach( string block in blocks ) {
				output = block;

				if( encodeThisBlock ) {
					// temporary fix for FTB putting in </pre><pre class=source> tags in place of two \n\n
					output = Regex.Replace(output, @"\<\/pre\s*?\>\<pre\s+class=""?source""?\s*?\>", @"<br/><br/>", RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.IgnorePatternWhitespace);
					output = HttpUtility.HtmlEncode( Regex.Unescape(output) );//Regex.Replace( block, @"(?'openingTag'<)(.*?)(?>(?!=[\/\?]?>)(?'closingSlash'\/\?)?(?'closingTag'>))", @"&lt;$1${closingSlash}]", RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.IgnorePatternWhitespace );
					output = Regex.Replace(output, @"(\<|\&lt;)br?(\s*|(\&nbsp;)*)(?:\/)?(\>|\&gt;)", "<br/>", RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.IgnorePatternWhitespace);
					output = FormatSource( output, GetLanguageType( language ) );
					encodeThisBlock = false;
				}

				// dont write out the code blocks
				if( block.StartsWith("[code") ) {
					encodeThisBlock = true;
					language = "CS";
				}
				else if( block.StartsWith("[/code") ) {
					// skip this block
					encodeThisBlock = false;
					language = "";
				}
				else {
					sb.Append( output );
				}
			}

			return sb.ToString();
		}
		#endregion

		#region ScrubHtml
		/// <summary>
		/// 
		/// </summary>
		/// <param name="encodedString"></param>
		/// <param name="postType"></param>
		/// <returns></returns>
		static string ScrubHtml( string encodedString, PostType postType ) {
			System.Collections.Hashtable ht = GetAllowedTags();

			System.Text.StringBuilder sb = new System.Text.StringBuilder();
			encodedString = Regex.Replace(encodedString, @"(<!--)", @"&lt;!--", RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.IgnorePatternWhitespace );
			encodedString = Regex.Replace(encodedString, @"(-->)", @"--&gt;", RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.IgnorePatternWhitespace );
			//			string[] blocks = Regex.Split(encodedString, @"((<(?:\/)?\w+(?:\s+\w+(?:\s*=\s*""?\w+""?)?)*\s*(?:\/)?>)|(<\!--)|(-->))", RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.IgnorePatternWhitespace );
			// string[] blocks = Regex.Split(encodedString, @"\b(<(?:\/)?\w+(?:\s+\w+(?:\s*=\s*""?(?:[\w-_:#\'\s%]*|(?:(?:http|ftp|https):\/\/)?[\w-_]+(?:\.[\w-_]+)+(?:[\w\-\.,@?^=%&:/~\+#]*[\w\-\@?^=%&/~\+#])?)""?)?)*\s*(?:\/)?>)", RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.IgnorePatternWhitespace );
			//			string[] blocks = Regex.Split(encodedString, @"(<(?:\/)?\w+\s*(?:\w+)*\s*(?:\/)?>)", RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.IgnorePatternWhitespace );
			string[] blocks = Regex.Split(encodedString, @"\b<(\/?\w*)(\s*(\w*)(?:\s*=\s*(?:""[^""]*""|'[^']'|[^>]*))?)*\/?>\b", RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.IgnorePatternWhitespace );
			string output = "";
			int keyStart = 1;

			foreach( string block in blocks ) {				
				output = block;
				if( output.StartsWith("<") ) {
					if( output[1] == '/' ) 
						keyStart = 2;
					else
						keyStart = 1;
						
					int keyEnd = output.IndexOfAny(new char[]{' ', '/', '>'}, keyStart) - keyStart;
					string key = output.Substring(keyStart, keyEnd).ToLower();
					StringBuilder sbAttr = new StringBuilder();
					if( ht.ContainsKey( key )) {
						string slashClosing = output.EndsWith("/>") ? "/" : "";
						string slashOpening = output.StartsWith("</") ? "/" : "";
						string attributes	= (string)ht[key];
						if( attributes != null && attributes.Length > 0 ) {
							string[] allowedAttr = ((string)ht[key]).Split(',');
							//foreach( Match match in Regex.Matches(output, String.Format(@"<{0}((?:\s+(\w+)(?:\s*=""?\w+""?)?)*)?\s*[\/\?]?>", key), RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.IgnorePatternWhitespace )) {
							//foreach( Match match in Regex.Matches(output, String.Format(@"<{0}((?:\s+(\w+)(?:\s*=\s*""?(?:\w*|(?:(?:http|ftp|https):\/\/)?[\w-_]+(?:\.[\w-_]+)+(?:[\w\-\.,@?^=%&:/~\+#]*[\w\-\@?^=%&/~\+#])?)""?)?)*)\s*[\/\?]?>", key), RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.IgnorePatternWhitespace )) {
							string attrPattern = String.Format(@"<{0}(\s*(\w*)(?:\s*=\s*(?:""[^""]*""|'[^']'|[^>]*))?)*\/?>", key);
							MatchCollection matches = Regex.Matches(output, attrPattern, RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.IgnorePatternWhitespace );
							foreach( Match match in matches ) {
								foreach( string attr in allowedAttr ) {
									if( match.Groups[2].Captures[0].Value == attr ) {
										sbAttr.Append( match.Groups[1].Captures[0].Value );
									}
								}
							}
						}
						
						sb.AppendFormat("<{0}{1}{2}{3}>", slashOpening, key, sbAttr.ToString(), slashClosing );//Regex.Replace( output, (string)ht[key], "<$1 $2>$3</$1>", RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.IgnorePatternWhitespace ) );
					}
					else {
						sb.Append( HttpUtility.HtmlEncode( output ) );
					}
				}
				else {
					sb.Append( output );
				}
			}

			return sb.ToString();
		}
		
		static System.Collections.Hashtable GetAllowedTags() {
			if( CSCache.Get("allowedTags") == null ) {
				
				System.Collections.Hashtable ht = new System.Collections.Hashtable();

				// TDD TODO eventually move these out to an external file
				ht.Add("h1", "align" );
				ht.Add("h2", "align" );
				ht.Add("h3", "align" );
				ht.Add("h4", "align" );
				ht.Add("h5", "align" );
				ht.Add("h6", "align" );
				ht.Add("strong", "" );
				ht.Add("em", "" );
				ht.Add("u", "");
				ht.Add("b", "");
				ht.Add("i", "");
				ht.Add("strike", "");
				ht.Add("sup", "");
				ht.Add("sub", "");
				ht.Add("font", "color,size,face");
				ht.Add("blockquote", "dir");
				ht.Add("ul", "");
				ht.Add("ol", "");
				ht.Add("li", "");
				ht.Add("p", "align,dir");
				ht.Add("address", ""); 
				ht.Add("pre", "class");
				ht.Add("div", "align");
				ht.Add("hr", "id");
				ht.Add("br", "");
				ht.Add("a", "href,target,name");
				ht.Add("span", "align");
				ht.Add("img", "src,alt,title");

				if( CSCache.Get("allowedTags") == null ) {
					CSCache.Insert("allowedTags", ht, 5);
				}

			}
			return (System.Collections.Hashtable)HttpContext.Current.Cache["allowedTags"];
		}
		#endregion

    }
}
#region Old Code
/*


            // replace all carraige returns with <br> tags
            strBody = strBody.Replace("\n", "\n" + Globals.HtmlNewLine + "\n");

            // Ensure we have safe anchors
            Match m = Regex.Match(strBody, "href=\"((.|\\n)*?)\"");
            foreach(Capture capture in m.Captures) {

                if ( (!capture.ToString().StartsWith("href=\"http://")) )
                  strBody = strBody.Replace(capture.ToString(), "");

//      TODO          if ( (!capture.ToString().StartsWith("href=\"http://")) && (!capture.ToString().StartsWith("href=\"/")) )
//                    strBody = strBody.Replace(capture.ToString(), "");
            }

            // Create mailto links with any words containing @
//            strBody = Regex.Replace(strBody, "\\b((?<!<a\\s+href\\s*=\\s*\\S+)\\w+@([\\w\\-\\.,@?^=%&:/~\\+#]*[\\w\\-\\@?^=%&/~\\+#])?)", "$1", RegexOptions.IgnoreCase | RegexOptions.Multiline);

            // replace all whitespace with &nbsp;
            //strBody = strBody.Replace(" ", "&nbsp;");

            return strBody;
            */
#endregion
