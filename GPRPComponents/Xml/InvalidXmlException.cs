using System;
using System.Text;

namespace GPRP.GPRPComponents
{
    public class InvalidXmlException : DNTException
    {
        public InvalidXmlException(string message)
            : base(message)
        {
        }
    }
}
