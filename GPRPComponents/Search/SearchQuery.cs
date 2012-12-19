using System.Collections;
//------------------------------------------------------------------------------
// <copyright company="Telligent Systems">
//     Copyright (c) Telligent Systems Corporation.  All rights reserved.
// </copyright> 
//------------------------------------------------------------------------------

namespace GPRP.GPRPComponents
{
    /// <summary>
    /// Common Search Parameters
    /// </summary>
    public class SearchQuery
    {
        public static SearchQuery FirstPageQuery(string textToFind)
        {
            SearchQuery query = new SearchQuery();
            query.PageIndex = 0;
            query.PageSize = 20;
            query.SearchTerms = textToFind;
            return query;
        }

        public static SearchQuery SectionSearch(string textToFind, int SectionID)
        {
            SearchQuery query = FirstPageQuery(textToFind);
            query.SectionsToSearch = new string[]{SectionID.ToString()};
            return query;
        }

        public SearchQuery()
        {
           
        }

        public string[] GroupToSearch;
        public int PageIndex = 0;
        public int PageSize = 10;
        public int UserID = 0;
        public string[] SectionsToSearch;
        public string[] UsersToSearch;
        public ArrayList SectionsToFilter;
        
        public string SearchTerms;

		public bool IsValid(SearchTerms st)
		{
			return (st.HasTerms) || 
				(SectionsToSearch != null && SectionsToSearch.Length > 0) || 
				(UsersToSearch != null && UsersToSearch.Length > 0) || 
				(GroupToSearch != null && GroupToSearch.Length > 0);
		}
    }
}