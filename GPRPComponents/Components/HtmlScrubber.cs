//------------------------------------------------------------------------------
// <copyright company="Telligent Systems">
//     Copyright (c) Telligent Systems Corporation.  All rights reserved.
// </copyright> 
//------------------------------------------------------------------------------

using System;
using System.Collections.Specialized;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Caching;
using System.Xml;

namespace GPRP.GPRPComponents
{
	/// <summary>
	/// Validates Html only letting a predefined set of Elemtnts/Attributes remain.
	/// </summary>
	public class HtmlScrubber
	{
        #region Static Default
        static NameValueCollection DefaultTags()
        {
           NameValueCollection defaultTags = new NameValueCollection();
            defaultTags.Add("h1", "align" );
            defaultTags.Add("h2", "align" );
            defaultTags.Add("h3", "align" );
            defaultTags.Add("h4", "align" );
            defaultTags.Add("h5", "align" );
            defaultTags.Add("h6", "align" );
            defaultTags.Add("strong", "" );
            defaultTags.Add("em", "" );
            defaultTags.Add("u", "");
            defaultTags.Add("b", "");
            defaultTags.Add("i", "");
            defaultTags.Add("strike", "");
            defaultTags.Add("sup", "");
            defaultTags.Add("sub", "");
            defaultTags.Add("font", "color,size,face");
            defaultTags.Add("blockquote", "dir");
            defaultTags.Add("ul", "");
            defaultTags.Add("ol", "");
            defaultTags.Add("li", "");
            defaultTags.Add("p", "class,align,dir");
            defaultTags.Add("address", ""); 
            defaultTags.Add("pre", "class");
            defaultTags.Add("div", "align");
            defaultTags.Add("hr", "id");
            defaultTags.Add("br", "");
            defaultTags.Add("a", "href,target,name");
            defaultTags.Add("span", "align");
            defaultTags.Add("img", "src,alt,title");

			return defaultTags;
        }
        protected NameValueCollection allowedTags = null;
        static Regex regex = new Regex( "<[^>]+>",RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.Multiline);
        static Regex jsAttributeRegex = new Regex("javascript:",RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.Compiled);
		static Regex xmlLineBreak = new Regex("&#x([DA9]|20|85|2028)(;)?",RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.Compiled);
        #endregion

        #region Public Statics	

        /// <summary>
        /// Creates a new HtmlScrubber and optionally filters out javascipt;
        /// </summary>
        /// <param name="html">Html to clean/filter</param>
        /// <returns>Clean Html</returns>
        public static string Clean(string html, bool encodeExceptions,  bool filterScripts)
        {
            if(Globals.IsNullorEmpty(html))
                return html;

            HtmlScrubber f = new HtmlScrubber(html,encodeExceptions, filterScripts);
            return f.Clean();
        }

        private static string StripScriptTags(string text) 
        {
            return jsAttributeRegex.Replace(text,string.Empty);
        }

        #endregion

        #region Private members
        string input = null;
        StringBuilder output = new StringBuilder();
        bool cleanJS = false;
        bool isFormatted = false;
        bool encodeExceptions = false;
        #endregion

        #region cnstr

        /// <summary>
        /// Filters unknown markup. Will not encode exceptions
        /// </summary>
        /// <param name="html">Markup to filter</param>
        public HtmlScrubber(string html):this(html,false,true)
        {
        }

        /// <summary>
        /// Filters unknown markup
        /// </summary>
        /// <param name="html">Markup to filter</param>
        /// <param name="encodeRuleExceptions">Should unknown elements be encoded or removed?</param>
        public HtmlScrubber(string html, bool encodeRuleExceptions):this(html,encodeRuleExceptions,true)
        {

        }

        /// <summary>
        /// Filters unknown markup
        /// </summary>
        /// <param name="html">Markup to filter</param>
        /// <param name="encodeRuleExceptions">Should unknown elements be encoded or removed?</param>
        /// <param name="removeScripts">Check for javascript: attributes</param>
		public HtmlScrubber(string html, bool encodeRuleExceptions, bool removeScripts)
		{
			input = html;
			cleanJS = removeScripts;
			isFormatted = false;
			encodeExceptions = encodeRuleExceptions;

			allowedTags = CSCache.Get("HtmlScrubber") as NameValueCollection;
			if(allowedTags == null)
			{
				XmlNode markUp = CSConfiguration.GetConfig().GetConfigSection("CommunityServer/MarkUp");
				allowedTags = new NameValueCollection();
				string atts = string.Empty;
				if(markUp != null)
				{
					#region Global Attributes
					XmlNode globalAttributes = markUp.SelectSingleNode("globalAttributes");
					if(globalAttributes != null)
					{
						foreach(XmlNode node in globalAttributes)
						{
							if(node.NodeType != XmlNodeType.Comment)
							{
								string name = node.Name;
								XmlAttribute enabled = node.Attributes["enable"];
								if(enabled != null)
								{
									if( string.Compare(enabled.Value, "true", true) == 0 )
									{
										if(atts.Length == 0)
											atts = name;
										else
											atts += "," + name;
									}
								}
							}
						}
					}
					#endregion

					#region Elements
					XmlNode elements = markUp.SelectSingleNode("html");
					if(elements != null)
					{
						foreach(XmlNode node in elements)
						{
							if(node.NodeType != XmlNodeType.Comment)
							{
								string name = node.Name;
								string localatts = atts;
								if(node.Attributes != null && node.Attributes.Count > 0)
								{
									foreach(XmlAttribute xatt in node.Attributes)
									{
										if( string.Compare(xatt.Value, "true", true) == 0 )
										{
											if(localatts.Length == 0)
												localatts = xatt.Name;
											else
												localatts += "," + xatt.Name;
										}
									}
								}

								allowedTags[name] = localatts;
							}
						}

					}
					#endregion
					
				}	
				else
				{
					allowedTags = DefaultTags();
				}

				CacheDependency dep = new CacheDependency(null, new string[]{CSConfiguration.CacheKey});
				CSCache.Insert("HtmlScrubber", allowedTags, dep);
			}

			


		}
        #endregion

        #region Cleaners
        /// <summary>
        /// Returns the results of a cleaning.
        /// </summary>
        /// <returns></returns>
        public string Clean()
        {
            if(!isFormatted)
            {
                Format();
                isFormatted = true;
            }
            return output.ToString();
        }

        #endregion

        #region Format / Walk
		
        /// <summary>
        /// Walks one time through the HTML. All elements/tags are validated.
        /// The rest of the text is simply added to the internal queue
        /// </summary>
        protected virtual void Format()
        {
//			// If the user is not moderated, then do not filter their posts
//			User user = CSContext.Current.User;
//			if(user.ModerationLevel == ModerationLevel.Unmoderated || user.ModerationLevel == ModerationLevel.AutoUnmoderate || user.ModerationLevel == ModerationLevel.CannotBeUnmoderated)
//			{
//				output.Append(input);
//				return;
//			}

            //Lets look for elements/tags
            Match mx = regex.Match(input,0,input.Length);

            //Never seems to be null
            while(mx.Value != string.Empty)
            {
                //find the first occurence of this elment
                int index = input.IndexOf(mx.Value);

                //add the begining to this tag
                output.Append(input.Substring(0,index));

                //remove this from the supplied text
                input = input.Remove(0,index);

                //validate the element
                output.Append(Validate(mx.Value));

                //remove this element from the supplied text
                input = input.Remove(0,mx.Length);

                //Get the next match
                mx = regex.Match(input,0,input.Length);
            }

            //If not Html is found, we should just place all the input into the output container
            if(input != null && input.Trim().Length > 0)
                output.Append(input);
        }

        #endregion

        #region Validators

        /// <summary>
        /// Main method for starting element validation
        /// </summary>
        /// <param name="tag"></param>
        /// <returns></returns>
        protected string Validate(string tag)
        {
            if(tag.StartsWith("</"))
                return ValidateEndTag(tag);

            if(tag.EndsWith("/>"))
                return ValidateSingleTag(tag);


            return ValidateStartTag(tag);

        }

        /// <summary>
        /// Validates single element tags such as <br /> and <hr class = "X" />
        /// </summary>
	    private string ValidateSingleTag(string tag)
	    {
            string strip = tag.Substring(1,tag.Length-3).Trim();

            int index = strip.IndexOf(" ");
            if(index == -1)
                index = strip.Length;
				
            string tagName = strip.Substring(0,index);
            
            int colonIndex = tagName.IndexOf(":") + 1;

            string safeTagName = tagName.Substring(colonIndex,tagName.Length - colonIndex);

            string allowedAttributes = allowedTags[safeTagName] as string;
            if(allowedAttributes == null)
                return encodeExceptions ? Globals.HtmlEncode(tag) : string.Empty;

            string atts = strip.Remove(0,tagName.Length).Trim();

            return ValidateAttributes(allowedAttributes,atts,tagName,"<{0}{1} />");



	    }

	    /// <summary>
        /// Validates a start tag
        /// </summary>
        /// <param name="tag"></param>
        /// <returns>the tag and validate attributes</returns>
        protected virtual string ValidateStartTag(string tag)
        {
            //Check for potential attributes
            int endIndex = tag.IndexOf(" ");

            //simple tag <tag>
            if(endIndex == -1)
                endIndex = tag.Length - 1;

            //Grab the tab name
            string tagName = tag.Substring(1,endIndex-1);

            //watch for html pasted from Office and messy namespaces
            int colonIndex = tagName.IndexOf(":");
            string safeTagName = tagName;
            if(colonIndex != -1)
                safeTagName = tagName.Substring(colonIndex+1);

			
            //Use safe incase a : is present
            string allowedAttributes = allowedTags[safeTagName] as string;

            //If we do not find a record in the Hashtable, this tag is not valid
            if(allowedAttributes == null)
                return encodeExceptions ? Globals.HtmlEncode(tag) : string.Empty; //remove element and all attributes if not valid

            //remove the tag name and find all of the current element's attributes
            int start = (colonIndex == -1) ? tagName.Length : safeTagName.Length + colonIndex + 1;

            string attributes = tag.Substring(start+1,(tag.Length - (start + 2)));

            //if we have attributes, make sure there is no extra padding in the way
            if(attributes != null)
                attributes = attributes.Trim();
			
            //Validate the attributes
            return ValidateAttributes(allowedAttributes,attributes,tagName,"<{0}{1}>");

			
        }

		/// <summary>
		/// Validates the elements attribute collection
		/// </summary>
		/// <param name="allowedAttributes"></param>
		/// <param name="tagAttributes"></param>
		/// <param name="tagName"></param>
		/// <returns></returns>
		protected virtual string ValidateAttributes(string allowedAttributes, string tagAttributes, string tagName, string tagFormat)
		{
			//container for attributes. We could use a stringbuilder here, but chances are we are removing most attributes
			string atts = "";
			//Do we even have any attributes to validate?
			if(allowedAttributes.Length > 0)
			{
				//&#xD;&#xA;
				

				
				tagAttributes = xmlLineBreak.Replace(tagAttributes,string.Empty);

				for (int start = 0, end = 0;
					start < tagAttributes.Length;
					start = end) 
				{
					//Put the end index at the end of the attribute name.
					end = tagAttributes.IndexOf('=', start);
					if (end < 0)
						end = tagAttributes.Length;
					//Get the attribute name and see if it's allowed.
					string att = tagAttributes.Substring(start, end - start).Trim();
					bool allowed = Regex.IsMatch(allowedAttributes,string.Format("({0},|{0}$)",att),RegexOptions.IgnoreCase);
					//Now advance the end index to include the attribute value.
					if (end < tagAttributes.Length) 
					{
						//Skip any blanks after the '='.
						for (++end;
							end < tagAttributes.Length && tagAttributes[end] == ' ';
							++end);
						if (end < tagAttributes.Length) 
						{
							//Find the end of the value.
							end = tagAttributes[end] == '"' //Quoted with double quotes?
								? tagAttributes.IndexOf('"', end + 1)
								: tagAttributes[end] == '\'' //Quoted with single quotes?
								? tagAttributes.IndexOf('\'', end + 1)
								: tagAttributes.IndexOf(' ', end); //Otherwise, assume not quoted.
							//If we didn't find the terminating character, just go to the end of the string.
							//Otherwise, advance the end index past the terminating character.
							end = end < 0 ? tagAttributes.Length : end + 1;
						}
					}
					//If the attribute is allowed, copy it.
					if (allowed)
						atts += ' ' + tagAttributes.Substring(start, end - start).Trim();
				}
				//Are we filtering for Javascript?
				if(cleanJS)
					atts = jsAttributeRegex.Replace(atts,string.Empty);
			}
			return string.Format(tagFormat,tagName,atts);
		}


        /// <summary>
        /// Validate End/Closing tag
        /// </summary>
        /// <param name="tag"></param>
        /// <returns></returns>
        protected virtual string ValidateEndTag(string tag)
        {
            string tagName = tag.Substring(2,tag.Length-3);

            int index = tag.IndexOf(":") - 1;
            if(index == -2)
            {
                index = 0;
            }
            tagName = tagName.Substring(index);
            string allowed = allowedTags[tagName] as string;

            if(allowed == null)
                return encodeExceptions ? Globals.HtmlEncode(tag) : string.Empty;

            return tag;

        }

        #endregion
	}
}