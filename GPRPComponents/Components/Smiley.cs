//------------------------------------------------------------------------------
// <copyright company="Telligent Systems">
//     Copyright (c) Telligent Systems Corporation.  All rights reserved.
// </copyright> 
//------------------------------------------------------------------------------

using System;

namespace GPRP.GPRPComponents
{
	/// <summary>
	/// Summary description for Smily.
	/// </summary>
	public class Smiley : IComparable
	{

		#region Fields
		#endregion

		#region Properties

		public int SmileyId {
			get{ return _smileyId; }
			set{ _smileyId = value; }
		}

		public string SmileyCode {
			get{ return _smileyCode; }
			set{ _smileyCode = value; }
		}

		public string SmileyUrl {
			get{ return _smileyUrl; }
			set{ _smileyUrl = value; }
		}

		public string SmileyText {
			get{ return _smileyText; }
			set{ _smileyText = value; }
		}


		#endregion

		#region Events
		#endregion

		#region Public Methods
		/// <summary>
		/// 
		/// </summary>
		public Smiley() {

		}

		public Smiley( int smileyId, string smileyCode, string smileyUrl, string smileyText ) : 
			this( smileyId, smileyCode, smileyUrl, smileyText, false )
		{
		}

		public Smiley( int smileyId, string smileyCode, string smileyUrl, string smileyText, bool bracketSafe ) 
		{
			_smileyId		= smileyId;
			_smileyCode		= smileyCode;
			_smileyUrl		= smileyUrl;
			_smileyText		= smileyText;
			_bracketSafe	= bracketSafe;
		}

		public bool IsSafeWithoutBrackets() {
			return _bracketSafe;
		}

		#endregion

		#region Protected Methods
		#endregion

		#region Protected Data
		#endregion

		#region Private Methods
		#endregion

		#region Private Data
		private int		_smileyId;
		private string	_smileyCode;
		private string	_smileyUrl;
		private string	_smileyText;
		private bool	_bracketSafe;
		#endregion

		#region IComparable Members

		public int CompareTo(object obj) {
			// TODO:  Add Smiley.CompareTo implementation
			return 0;
		}

		#endregion
	}
}
