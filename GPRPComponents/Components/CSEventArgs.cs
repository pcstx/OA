//------------------------------------------------------------------------------
// <copyright company="Telligent Systems">
//     Copyright (c) Telligent Systems Corporation.  All rights reserved.
// </copyright> 
//------------------------------------------------------------------------------

using System;
using GPRP.GPRPEnumerations;
namespace GPRP.GPRPComponents
{
	public class CSEventArgs : EventArgs
	{
		private ObjectState _state;
		private ApplicationType _appType;

		public ObjectState State
		{
			get{ return _state;}
		}

		public ApplicationType ApplicationType
		{
			get{return _appType;}
		}

		public CSEventArgs(ObjectState state, ApplicationType appType)
		{
			_state = state;
			_appType = appType;
		}

		public CSEventArgs():this(ObjectState.None,ApplicationType.Unknown){}

	}
}