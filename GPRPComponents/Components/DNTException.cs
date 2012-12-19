using System;

namespace GPRP.GPRPComponents
{
	/// <summary>
	/// DNT自定义异常类。
	/// </summary>
	public class DNTException : Exception
	{
		public DNTException()
		{
			//
		}


		public DNTException(string msg) : base(msg)
		{
			//
		}
	}
}
