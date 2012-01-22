using System;

namespace Bifrost.Services.Execution
{
    public class ParameterCountMismatchException : ArgumentException
    {
        public ParameterCountMismatchException(Uri uri, string serviceName, int actual, int expected) 
            : base(string.Format("Expected {0} arguments, but got {1} for {2} with Uri : '{3}'", expected, actual, serviceName, uri))
        {
        }
    }
}
