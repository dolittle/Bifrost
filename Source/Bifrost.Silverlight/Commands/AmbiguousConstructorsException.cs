using System;

namespace Bifrost.Commands
{
    public class AmbiguousConstructorsException : ArgumentException
    {
        public AmbiguousConstructorsException(Type commandType)
            : base(string.Format("Command of type '{0}' has ambiguous constructors, unable to create an instance", commandType.Name))
        {
        }
    }
}
