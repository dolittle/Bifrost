using System;

namespace Bifrost.Commands
{
    public class UnsupportedParametersConstruct : ArgumentException
    {
        public UnsupportedParametersConstruct()
            : base("Silverlight does not support reflection over properties in anonynomous types, due to security reasons")
        {
        }
    }
}
