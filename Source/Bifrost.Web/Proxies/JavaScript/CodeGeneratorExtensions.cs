using System;

namespace Bifrost.Web.Proxies.JavaScript
{
    public static class CodeGeneratorExtensions
    {
        public static Namespace Namespace(this ICodeGenerator generator, string name, Action<ObjectLiteral> callback)
        {
            var ns = new Namespace(name);
            callback(ns.Content);
            return ns;
        }

        public static FunctionBody Property(this FunctionBody functionBody, string name, Action<PropertyAssignment> callback)
        {
            var propertyAssignment = new PropertyAssignment(name);
            functionBody.AddChild(propertyAssignment);
            callback(propertyAssignment);
            return functionBody;
        }

        public static FunctionBody Variant(this FunctionBody functionBody, string name, Action<VariantAssignment> callback)
        {
            var variantAssignment = new VariantAssignment(name);
            functionBody.AddChild(variantAssignment);
            callback(variantAssignment);
            return functionBody;
        }

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

        public static Assignment WithType(this Assignment assignment, Action<TypeExtension> callback)
        {
            var typeExtension = new TypeExtension();
            assignment.Value = typeExtension;
            callback(typeExtension);
            return assignment;
        }

        public static Assignment WithFunctionCall(this Assignment assignment, Action<FunctionCall> callback)
        {
            var functionCall = new FunctionCall();
            assignment.Value = functionCall;
            callback(functionCall);
            return assignment;
        }

        public static FunctionCall WithParameters(this FunctionCall functionCall, params string[] parameters)
        {
            functionCall.Parameters = parameters;
            return functionCall;
        }

        public static FunctionCall WithName(this FunctionCall functionCall, string name)
        {
            functionCall.Function = name;
            return functionCall;
        }

        public static Assignment WithObservable(this Assignment assignment, string defaultValue = null)
        {
            return assignment.WithFunctionCall(f => {
                f.WithName("ko.observable");
                if (defaultValue != null) f.WithParameters(defaultValue);
            });
        }

        public static Assignment WithObservableArray(this Assignment assignment)
        {
            return assignment.WithFunctionCall(f => f.WithName("ko.observableArray"));
        }

        public static Assignment WithObjectLiteral(this Assignment assignment, Action<ObjectLiteral> callback)
        {
            var objectLiteral = new ObjectLiteral();
            callback(objectLiteral);
            assignment.Value = objectLiteral;
            return assignment;
        }
        

        public static TypeExtension WithSuper(this TypeExtension typeExtension, string super)
        {
            typeExtension.SuperType = super;
            return typeExtension;
        }

        public static Function WithDependencies(this Function function, params string[] dependencies)
        {
            function.Dependencies = dependencies;
            return function;
        }
    }
}
