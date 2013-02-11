using System;

namespace Bifrost.Web.Proxies.JavaScript
{
    public static class AssignmentExtensions
    {
        public static KeyAssignment Assign(this ObjectLiteral objectLiteral, string name)
        {
            var keyAssignment = new KeyAssignment(name);
            objectLiteral.AddChild(keyAssignment);
            return keyAssignment;
        }

        public static Assignment WithThis(this Assignment assignment)
        {
            assignment.Value = new This();
            return assignment;
        }

        public static Assignment WithLiteral(this Assignment assignment, string literal)
        {
            assignment.Value = new Literal(literal);
            return assignment;
        }

        public static Assignment WithString(this Assignment assignment, string theString)
        {
            assignment.WithLiteral(string.Format("\"{0}\"", theString));
            return assignment;
        }

        public static Assignment WithType(this Assignment assignment, Action<TypeExtension> callback)
        {
            var typeExtension = new TypeExtension();
            assignment.Value = typeExtension;
            callback(typeExtension);
            return assignment;
        }

        public static Assignment WithFunction(this Assignment assignment, Action<Function> callback, params string[] dependencies)
        {
            var function = new Function(dependencies);
            assignment.Value = function;
            callback(function);
            return assignment;
        }

        public static Assignment WithFunctionCall(this Assignment assignment, Action<FunctionCall> callback)
        {
            var functionCall = new FunctionCall();
            assignment.Value = functionCall;
            callback(functionCall);
            return assignment;
        }

        public static Assignment WithObservable(this Assignment assignment, string defaultValue = null)
        {
            return assignment.WithFunctionCall(f =>
            {
                f.WithName("ko.observable");
                if (defaultValue != null) f.WithParameters(defaultValue);
            });
        }

        public static Assignment WithObservableArray(this Assignment assignment)
        {
            return assignment.WithFunctionCall(f => f.WithName("ko.observableArray"));
        }

        public static Assignment WithObjectLiteral(this Assignment assignment, Action<ObjectLiteral> callback = null)
        {
            var objectLiteral = new ObjectLiteral();
            if (callback != null) callback(objectLiteral);
            assignment.Value = objectLiteral;
            return assignment;
        }
    }
}
