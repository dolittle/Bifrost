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

        static void AddPropertiesFromType(Container parent, IEnumerable<PropertyInfo> properties, Action<Assignment> assignmentVisitor)
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
                    AddPropertiesFromType(objectLiteral, property.PropertyType.GetProperties(), assignmentVisitor);
                }

                if (assignmentVisitor != null) assignmentVisitor(assignment);

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


        public static Container WithPropertiesFrom(this Container contaier, Type type, Type excludePropertiesFrom = null, Action<Assignment> assignmentVisitor = null)
        {
            var properties = type.GetProperties();
            if (excludePropertiesFrom != null)
                properties = properties.Where(p => !excludePropertiesFrom.GetProperties().Select(pi => pi.Name).Contains(p.Name)).ToArray();

            AddPropertiesFromType(contaier, properties, assignmentVisitor);

            return contaier;
        }



    }
}
