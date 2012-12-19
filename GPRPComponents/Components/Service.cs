//------------------------------------------------------------------------------
// <copyright company="Telligent Systems">
//     Copyright (c) Telligent Systems Corporation.  All rights reserved.
// </copyright> 
//------------------------------------------------------------------------------

using System;
using System.Collections;


namespace GPRP.GPRPComponents {

	public class Service : IComparable {


		#region Fields
		public enum ServiceCodeType {
			ServicePlugIn	= 1,
			Executable		= 2,
		}
		#endregion

		#region Properties

		public int ServiceId {
			get{ return _serviceId; }
			set{ _serviceId = value; }
		}

		public string ServiceName {
			get{ return _serviceName; }
			set{ _serviceName = value; }
		}

		public ServiceCodeType ServiceCode {
			get{ return _serviceTypeCode; }
			set{ _serviceTypeCode = value; }
		}

		public string ServiceAssemblyPath {
			get{ return _serviceAssemblyPath; }
			set{ _serviceAssemblyPath = value; }
		}

		public string ServiceFullClassName {
			get{ return _serviceFullClassName; }
			set{ _serviceFullClassName = value; }
		}

		public string ServiceWorkingDirectory {
			get{ return _serviceWorkingDirectory; }
			set{ _serviceWorkingDirectory = value; }
		}

		#endregion

		#region Events
		#endregion

		#region Public Methods

		public Service() {

		}

		public Service( int serviceId, string serviceName, ServiceCodeType serviceTypeCode, string serviceAssemblyPath, string serviceFullClassName, string serviceWorkingDirectory ) {
			_serviceId					= serviceId;
			_serviceName				= serviceName;
			_serviceTypeCode			= serviceTypeCode;
			_serviceAssemblyPath		= serviceAssemblyPath;
			_serviceFullClassName		= serviceFullClassName;
			_serviceWorkingDirectory	= serviceWorkingDirectory;
		}

		#endregion

		#region Protected Methods
		#endregion

		#region Protected Data
		#endregion

		#region Private Methods
		#endregion

		#region Private Data
		private int				_serviceId;
		private string			_serviceName;
		private ServiceCodeType _serviceTypeCode;
		private string			_serviceAssemblyPath;
		private string			_serviceFullClassName;
		private string			_serviceWorkingDirectory;
		#endregion

	
		#region IComparable Members

		public int CompareTo(object obj) {
			// TODO:  Add Service.CompareTo implementation
			return 0;
		}

		#endregion
	}
}