using System;

namespace Bifrost.Interaction
{
    public class MoreThanOneParameter : ArgumentException
    {
        public MoreThanOneParameter(Type type, string methodName) : 
            base(string.Format("Method '{0}' on '{1}' can only have no arguments or one arguments", methodName, type.AssemblyQualifiedName)) { }
    }
}
