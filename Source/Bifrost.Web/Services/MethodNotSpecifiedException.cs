using System;

namespace Bifrost.Web.Services
{
    public class MethodNotSpecifiedException : ArgumentException
    {
        public MethodNotSpecifiedException(Type service, Uri uri) : base(string.Format("Method not specified for service invocation for type '{0}' with Uri '{1}",service.AssemblyQualifiedName, uri))
        {
        }
    }
}
