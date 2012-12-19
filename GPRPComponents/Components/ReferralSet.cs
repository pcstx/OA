//------------------------------------------------------------------------------
// <copyright company="Telligent Systems">
//     Copyright (c) Telligent Systems Corporation.  All rights reserved.
// </copyright> 
//------------------------------------------------------------------------------

using System;
using System.Collections;

namespace GPRP.GPRPComponents
{
	/// <summary>
	/// Summary description for ReferralSet.
	/// </summary>
	public class ReferralSet
	{
		public ReferralSet()
		{
			_referrals = new ArrayList();
		}

        private ArrayList _referrals;
        /// <summary>
        /// Property Referrals (ArrayList)
        /// </summary>
        public ArrayList Referrals
        {
            get {  return this._referrals; }
            set {  this._referrals = value; }
        }

       private int _pageSize;
       /// <summary>
       /// Property PageSize (int)
       /// </summary>
       public int PageSize
       {
           get {  return this._pageSize; }
           set {  this._pageSize = value; }
       }

        private int _pageIndex;
        /// <summary>
        /// Property PageIndex (int)
        /// </summary>
        public int PageIndex
        {
            get {  return this._pageIndex; }
            set {  this._pageIndex = value; }
        }

        private int _totalRecords;
        /// <summary>
        /// Property TotalRecords (int)
        /// </summary>
        public int TotalRecords
        {
            get {  return this._totalRecords; }
            set {  this._totalRecords = value; }
        }
	}
}
