//------------------------------------------------------------------------------
// <copyright company="Telligent Systems">
//     Copyright (c) Telligent Systems Corporation.  All rights reserved.
// </copyright> 
//------------------------------------------------------------------------------

using System.Xml;

namespace GPRP.GPRPComponents
{
	public interface ICSModule
	{
		void Init(CSApplication csa, XmlNode node);
	}
}