//------------------------------------------------------------------------------
// <copyright company="Telligent Systems">
//     Copyright (c) Telligent Systems Corporation.  All rights reserved.
// </copyright> 
//------------------------------------------------------------------------------

using System;
using System.Collections;

namespace GPRP.GPRPComponents {

	public class Rank : IComparable	{

		#region Fields
		#endregion

		#region Properties

		public int RankId {
			get{ return _rankId; }
			set{ _rankId = value; }
		}

		public string RankName {
			get{ return _rankName; }
			set{ _rankName = value; }
		}

		public int PostingCountMinimum {
			get{ return _postingCountMin; }
			set{ _postingCountMin = value; }
		}

		public int PostingCountMaximum {
			get{ return _postingCountMax; }
			set{ _postingCountMax = value; }
		}

		public string RankIconUrl {
			get{ return _rankIconUrl; }
			set{ _rankIconUrl = value; }
		}

		#endregion

		#region Events
		#endregion

		#region Public Methods
		
		public Rank() {
		}

		public Rank( int rankId, string rankName, int postingCountMinimum, int postingCountMaximum, string rankIconUrl ) {
			_rankId				= rankId;
			_rankName			= rankName;
			_postingCountMin	= postingCountMinimum;
			_postingCountMax	= postingCountMaximum;
			_rankIconUrl		= rankIconUrl;
		}

		#endregion

		#region Protected Methods
		#endregion

		#region Protected Data
		#endregion

		#region Private Methods
		#endregion

		#region Private Data
		private int		_rankId;
		private string 	_rankName;
		private int		_postingCountMin;
		private int		_postingCountMax;
		private string	_rankIconUrl;
		#endregion


	
		#region IComparable Members

		public int CompareTo(object obj) {
			Rank rhs = obj as Rank;
			
			if( rhs != null ) {
				
				if( RankId == rhs.RankId )
					return 0;
				if( RankId > rhs.RankId )
					return 1;
				else
					return -1;
			}
			else
				return -1;
		}

		#endregion
	}
}