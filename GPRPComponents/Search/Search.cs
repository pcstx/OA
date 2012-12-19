//------------------------------------------------------------------------------
// <copyright company="Telligent Systems">
//     Copyright (c) Telligent Systems Corporation.  All rights reserved.
// </copyright> 
//------------------------------------------------------------------------------

using System;

using System.Web;
using System.Web.Caching;
using System.Collections;
using System.Text;
using System.Text.RegularExpressions;
using GPRP.GPRPEnumerations;

namespace GPRP.GPRPComponents {
    /// <summary>
    /// This class contains the method used to perform searches.  The Search Web control uses
    /// this class to perform its searching.
    /// </summary>
    public abstract class Search {

        
        public abstract void IndexPosts (int setSize, int settingsID);
        protected abstract double CalculateWordRank (Word word, Post post, int totalBodyWords);
        protected abstract SearchResultSet GetSearchResults (SearchQuery query, SearchTerms terms);
        protected abstract ArrayList GetPermissionFilterList();

        /// <summary>
        /// Returns the results of a searchr request
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public virtual SearchResultSet GetSearchResults(SearchQuery query)
        {
            query.SectionsToFilter = GetPermissionFilterList();

			SearchTerms st = GetSearchTerms(query);

			if(query.IsValid(st))
	            return GetSearchResults(query,GetSearchTerms(query));
			else
				return new SearchResultSet();
        }


        /// <summary>
        /// All applications use the same SearchBarrel. This method provides default access to it.
        /// </summary>
        public void InsertIntoSearchBarrel(Hashtable words, Post post, int settingsID, int totalBodyWords)
        {
            foreach(int key in words.Keys)
            {
                Word w = words[key] as Word;
                w.Weight = CalculateWordRank(w,post,totalBodyWords);
            }

            CommonDataProvider dp = CommonDataProvider.Instance(); 
            dp.InsertIntoSearchBarrel(words, post, settingsID);
        }

        #region static indexing

        #region Terms
		protected static SearchTerms GetSearchTerms(SearchQuery query)
		{
			SearchTerms st = new SearchTerms();
			ArrayList searchKeywords;
			//bool performLikeMatch = false; // Never Used
			string[] andTerms;
			string[] orTerms;

			// Clean the search terms
			//
			string searchTerms = Regex.Replace(query.SearchTerms, "[^\\w]", " ", RegexOptions.Compiled | RegexOptions.Multiline);

			// Added by backend@gmail.com
			bool toHashcode = (SiteSettingsManager.GetSiteSettings().SearchMode == SearchMode.ForumsIndexer);
			searchKeywords = TokenizeKeywords(searchTerms, toHashcode);
			
			/* Commented by backend@gmail.com
			// Tokenize the search terms and determine if we need to perform partial match
			//
			if (searchTerms.IndexOf("*") != 0) 
			{
				//performLikeMatch = false;
				searchKeywords = TokenizeKeywords(searchTerms, true);
			} 
			else 
			{
				//performLikeMatch = true;
				searchKeywords = TokenizeKeywords(searchTerms, false);
			}
			*/

			// Do we have users that we are searching for?
			//
			//            if ((usersToSearch[0] == "") || (usersToSearch[0] == "0"))
			//                usersToSearch = null;

			// If we don't have any results
			//
			//            if ((searchKeywords.Count == 0) && (query.UsersToSearch == null))
			//                throw new CSException(CSExceptionType.SearchNoResults);

			// Get the terms divided into and/or sets
			//
			// Modified by backend@gmail.com
			// add "toHashcode" parameter
			GetAndOrKeywords (searchKeywords, toHashcode, out andTerms, out orTerms);
                        
			st.And = andTerms;
			st.Or = orTerms;

			return st;
		}


        public static string ForumsToSearchEncode (string stringToEncode) 
		{
            ASCIIEncoding asciiEncoder = new ASCIIEncoding();

            stringToEncode = stringToEncode.TrimEnd(',');

            return Convert.ToBase64String( asciiEncoder.GetBytes(stringToEncode) );
        }

        public static string[] UsersToDecode (string stringToDecode) {
            UTF8Encoding decoder = new UTF8Encoding();
            string[] usersToSearch;

            try {
                byte[] userList = Convert.FromBase64String( stringToDecode );
                usersToSearch = decoder.GetString( userList ).Split(',');
            } catch {
                usersToSearch = null;
            }

            return usersToSearch;
        }



        #endregion

        #region Index
        // *********************************************************
        // Index
        //
        /// <summary>
        /// Populates a hashtable of words that will be entered into
        /// the forums search barrel.
        /// </summary>
        /// 
        protected static Hashtable Index (string contentToIndex, Hashtable words, WordLocation wordLocation, int settingsID) {

            // Get the ignore words
            //
            Hashtable ignoreWords = GetIgnoreWords(settingsID);

            // Get a string array of the words we want to index
            //
            string[] wordsToIndex = CleanSearchTerms(contentToIndex);

            // Ensure we have data to work with
            //
            if (wordsToIndex.Length == 0)
                return words;

            // Operate on each word in stringArrayOfWords
            //
            foreach (string word in wordsToIndex) {

				if(word != null && word.Length >= 3)
				{
					// Get the hash code for the word
					//
					int hashedWord = word.ToLower().GetHashCode();

					// Add the word to our words Hashtable
					//
					if (!ignoreWords.ContainsKey(hashedWord)) 
					{
						if (!words.Contains(hashedWord))
							words.Add(hashedWord, new Word(word, wordLocation));
						else
							((Word) words[hashedWord]).IncrementOccurence(wordLocation);
					}
				}
                
            }

            return words;
        }
        #endregion

		#region TokenizeKeywords
		private static ArrayList TokenizeKeywords (string searchTerms, bool toHashcode) 
		{
			ArrayList newWords = new ArrayList();
			ArrayList searchWords = new ArrayList();
			string[] words;

			// Clean the search terms
			//
			words = CleanSearchTerms(searchTerms);

			// Do these words already exist?
			foreach (string word in words) 
			{
				string wordLowerCase = word.ToLower();

				int wordHashCode = wordLowerCase.GetHashCode();

				// OR code
				// Modified by backend@nsfocus.com
				//if (wordHashCode == 5861509)
				if (wordHashCode == 5861509 && toHashcode)
					searchWords.Add(wordHashCode);

				// Only take words larger than 2 characters
				//
				if (wordLowerCase.Length > 1) 
				{

					// Do we need to convert words to hash codes
					//
					if (toHashcode) 
					{
						searchWords.Add(wordHashCode);
					} 
					else 
					{
						searchWords.Add(word);
					}

				}

			}

			return searchWords;

		}
		#endregion

		#region Get AND/OR Keywords
		// *********************************************************************
		//  GetAndOrKeywords
		//
		/// <summary>
		/// Private helper function to break tokenized search terms into the
		/// and and or terms used in the search.
		/// </summary>
		/// 
		// ********************************************************************/
		// Modified by backend@gmail.com
		// add "toHashcode" parameter
		private static void GetAndOrKeywords (ArrayList searchTerms, bool toHashcode, out string[] andTermsOut, out string[] orTermsOut) 
		{
			ArrayList andTerms = new ArrayList();
			ArrayList orTerms = new ArrayList();
			bool buildingORterm = false;
            
			// Modified by backend@nsfocus.com
			if (toHashcode)
			{
				int OR = "||".GetHashCode();
				int AND = "&&".GetHashCode();

				// Loop through all the tokens
				//
				foreach (int term in searchTerms) 
				{

					if (term == OR) 
					{

						// Found an OR token, do we have any items in the AND ArrayList?
						//
						if (andTerms.Count > 0) 
						{
							orTerms.Add(andTerms[andTerms.Count - 1]);
							andTerms.Remove(andTerms[andTerms.Count - 1]);
							buildingORterm = true;
						}

					} 
					else if (buildingORterm) 
					{

						// Closing statement for an OR term
						orTerms.Add(term);
						buildingORterm = false;

					} 
					else if (term == AND) 
					{

						// ignore

					} 
					else 
					{

						andTerms.Add(term);

					}

				}
			}
			else
			{
				foreach (string term in searchTerms) 
				{
					if (term.ToUpper() == "OR") 
					{
						// Found an OR token, do we have any items in the AND ArrayList?
						//
						if (andTerms.Count > 0) 
						{
							orTerms.Add(andTerms[andTerms.Count - 1]);
							andTerms.Remove(andTerms[andTerms.Count - 1]);
							buildingORterm = true;
						}
					} 
					else if (buildingORterm) 
					{
						// Closing statement for an OR term
						orTerms.Add(term);
						buildingORterm = false;
					} 
					else if (term.ToUpper() == "AND") 
					{
						// ignore
					} 
					else 
					{
						andTerms.Add(term);
					}
				}			
			}
			andTermsOut = ArrayListToStringArray( andTerms );
			orTermsOut = ArrayListToStringArray( orTerms );
		}

		#endregion

        #region ArrayList to String[] Helper function
        static string[] ArrayListToStringArray (ArrayList list) 
		{
            string[] s = new string[list.Count];

            for (int i = 0; i < list.Count; i++)
                s[i] = list[i].ToString();

            return s;
        }
        #endregion

        #region CleanSearchTerms helper function
        // *********************************************************************
        //  RemoveIgnoreWords
        //
        /// <summary>
        /// Removes ignore words and returns cleaned string array
        /// </summary>
        /// 
        // ********************************************************************/
        protected static string[] CleanSearchTerms(string searchTerms) {
            // Force the searchTerms to lower case
            //
            searchTerms = searchTerms.ToLower();

            // Strip any markup characters
            //
            searchTerms = Transforms.StripHtmlXmlTags(searchTerms);

            // Remove non-alpha/numeric characters
            //
            searchTerms = Regex.Replace(searchTerms, "[^\\w]", " ", RegexOptions.Compiled | RegexOptions.Multiline);

            // Replace special words with symbols
            //
            searchTerms = Regex.Replace(searchTerms, "\\bor\\b", "||", RegexOptions.Compiled | RegexOptions.Multiline);
            searchTerms = Regex.Replace(searchTerms, "\\band\\b", "&&", RegexOptions.Compiled | RegexOptions.Multiline);

            // Finally remove any extra spaces from the string
            //
            searchTerms = Regex.Replace(searchTerms, " {1,}", " ", RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.Multiline); 


            return searchTerms.Split(' ');
        }

        /// <summary>
        /// Retuns a hashtable of words that are ignored. Search terms are filtered with these words
        /// </summary>
        /// <returns></returns>
        static Hashtable GetIgnoreWords (int settingsID) {
            string cacheKey = string.Format("SettingsID:{0}SearchIgnoreWordsTable",settingsID);

            // Do we have the item in cache?
            //
            Hashtable ignoreWords = CSCache.Get(cacheKey) as Hashtable;
            if (ignoreWords == null) {

                // Create Instance of the CommonDataProvider
                //
                CommonDataProvider dp = CommonDataProvider.Instance();

                ignoreWords = dp.GetSearchIgnoreWords(settingsID);

                CSCache.Max(cacheKey, ignoreWords);

            }

            return ignoreWords;

        }
        #endregion

        #endregion
    
    }

}

