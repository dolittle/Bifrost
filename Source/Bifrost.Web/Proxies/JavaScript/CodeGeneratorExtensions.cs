using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Bifrost.Concepts;
using Bifrost.Extensions;

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

        static void AddPropertiesFromType(LanguageElement parent, IEnumerable<PropertyInfo> properties)
        {
            foreach (var property in properties)
            {
                var propertyName = property.Name.ToCamelCase();

                Assignment assignment;
                if (parent is FunctionBody)
                    assignment = new PropertyAssignment(propertyName);
                else
                    assignment = new KeyAssignment(propertyName);

                if (property.IsDictionary())
                    assignment.WithObjectLiteral();
                else if (property.IsEnumerable())
                    assignment.WithObservableArray();
                else if (property.IsObservable())
                    assignment.WithObservable();
                else
                {
                    var objectLiteral = new ObjectLiteral();
                    assignment.Value = objectLiteral;
                    AddPropertiesFromType(objectLiteral, property.PropertyType.GetProperties());
                }

                parent.AddChild(assignment);

            }
        }

        static bool IsEnumerable(this PropertyInfo property)
        {
            return (property.PropertyType.HasInterface(typeof(IEnumerable<>)) ||
                   property.PropertyType.HasInterface<IEnumerable>()) && property.PropertyType != typeof(string);
        }

        static bool IsDictionary(this PropertyInfo property)
        {
            return  property.PropertyType.HasInterface(typeof(IDictionary<,>)) ||
                    property.PropertyType.HasInterface<IDictionary>();
        }

        static bool IsObservable(this PropertyInfo property)
        {
            return property.PropertyType.IsValueType ||
                    property.PropertyType == typeof(string) ||
                    property.PropertyType == typeof(Type) ||
                    property.PropertyType == typeof(MethodInfo) ||
                    property.PropertyType == typeof(Guid) ||
                    property.PropertyType.IsNullable() ||
                    property.PropertyType.IsConcept();
        }


        public static FunctionBody WithPropertiesFrom(this FunctionBody functionBody, Type type, Type excludePropertiesFrom = null)
        {
            var properties = type.GetProperties();
            if( excludePropertiesFrom != null )
                properties = properties.Where(p=>!excludePropertiesFrom.GetProperties().Select(pi=>pi.Name).Contains(p.Name)).ToArray();

            AddPropertiesFromType(functionBody, properties);

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

        public static Assignment WithObjectLiteral(this Assignment assignment, Action<ObjectLiteral> callback=null)
        {
            var objectLiteral = new ObjectLiteral();
            if( callback != null ) callback(objectLiteral);
            assignment.Value = objectLiteral;
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
