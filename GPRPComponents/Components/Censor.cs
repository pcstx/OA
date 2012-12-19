//------------------------------------------------------------------------------
// <copyright company="Telligent Systems">
//     Copyright (c) Telligent Systems Corporation.  All rights reserved.
// </copyright> 
//------------------------------------------------------------------------------

using System;
using System.Collections;

namespace GPRP.GPRPComponents {

	public class Censor : IComparable {

		#region Fields
		#endregion

		#region Properties

		public string Word {
			get{ return _word; }
			set{ _word = value; }
		}

		public string Replacement {
			get{ return _replacement; }
			set{ _replacement = value; }
		}

		#endregion

		#region Events
		#endregion

		#region Public Methods

		public Censor() {
		}

		public Censor( string word, string replacement ) {
			_word			= word;
			_replacement	= replacement;
		}

		#endregion

		#region Protected Methods
		#endregion

		#region Protected Data
		#endregion

		#region Private Methods
		#endregion

		#region Private Data
		private string _word;
		private string _replacement;
		#endregion

		#region IComparable Members

		public int CompareTo(object obj) {
			// TODO:  Add Censor.CompareTo implementation
			return 0;
		}

		#endregion
	}
}