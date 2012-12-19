//------------------------------------------------------------------------------
// <copyright company="Telligent Systems">
//     Copyright (c) Telligent Systems Corporation.  All rights reserved.
// </copyright> 
//------------------------------------------------------------------------------

using System;
using System.Collections;

namespace GPRP.GPRPComponents {

	public class Report : IComparable {

		#region Fields
		#endregion

		#region Properties

		public int ReportId {
			get{ return _reportId; }
			set{ _reportId = value; }
		}

		public string ReportName {
			get{ return _reportName; }
			set{ _reportName = value; }
		}

		public bool IsActive {
			get{ return _active; }
			set{ _active = value; }
		}

		public string ReportCommand {
			get{ return _reportCommand; }
			set{ _reportCommand = value; }
		}

		public string ReportScript {
			get{ return _reportScript; }
			set{ _reportScript = value; }
		}

		#endregion

		#region Events
		#endregion

		#region Public Methods

		public Report() {
		}

		public Report( int reportId, string reportName, bool isActive, string reportCommand, string reportScript ) {
			_reportId		= reportId;
			_reportName		= reportName;
			_active			= isActive;
			_reportCommand	= reportCommand;
			_reportScript	= reportScript;
		}

		#endregion

		#region Protected Methods
		#endregion

		#region Protected Data
		#endregion

		#region Private Methods
		#endregion

		#region Private Data
		private int		_reportId;
		private string	_reportName;
		private bool	_active;
		private string	_reportCommand;
		private string	_reportScript;
		#endregion

	
		#region IComparable Members

		public int CompareTo(object obj) {
			// TODO:  Add Report.CompareTo implementation
			return 0;
		}

		#endregion
	}
}