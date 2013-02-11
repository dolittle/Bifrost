using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bifrost.Web.Proxies.JavaScript
{
    public static class ScopeExtensions 
    {
        public static Scope FunctionCall(this Scope scope, Action<FunctionCall> callback)
        {
            var functionCall = new FunctionCall();
            scope.AddChild(functionCall);
            callback(functionCall);
            return scope;
        }
    }
}
