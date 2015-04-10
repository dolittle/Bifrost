using System;

namespace Bifrost.Interaction
{
    public class ReturnValueShouldBeBoolean : ArgumentException
    {
        public ReturnValueShouldBeBoolean(string memberName, Type type)
            : base(string.Format("Method '{0}' on '{1}' must be return a boolean to be valid for canExecute checks", memberName, type.AssemblyQualifiedName)) { }
    }
}
