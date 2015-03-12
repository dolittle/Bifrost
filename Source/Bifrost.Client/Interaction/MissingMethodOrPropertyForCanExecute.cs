using System;

namespace Bifrost.Interaction
{
    public class MissingMethodOrPropertyForCanExecute : ArgumentException
    {
        public MissingMethodOrPropertyForCanExecute(string canExecuteWhen, Type type)
            : base(string.Format("Missing method or property called '{0}' on '{1}", canExecuteWhen, type.AssemblyQualifiedName)) { }
    }
}
