/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Linq;
using System.Linq.Expressions;
using Bifrost.Execution;
using Bifrost.Extensions;
using Bifrost.Read;
using Bifrost.Concepts;
using System.Reflection;
using Bifrost.Security;

namespace Bifrost.Web.Read
{
    public class ReadModelService
    {
        ITypeDiscoverer _typeDiscoverer;
        IContainer _container;
        IReadModelFilters _readModelFilters;
        IFetchingSecurityManager _fetchingSecurityManager;
        MethodInfo _authorizeMethod;

        public ReadModelService(ITypeDiscoverer typeDiscoverer, IContainer container, IFetchingSecurityManager fetchingSecurityManager, IReadModelFilters readModelFilters)
        {
            _typeDiscoverer = typeDiscoverer;
            _container = container;
            _fetchingSecurityManager = fetchingSecurityManager;
            _readModelFilters = readModelFilters;

            _authorizeMethod = fetchingSecurityManager.GetType().GetMethods()
                .Where(m =>
                    m.Name == "Authorize" &&
                    m.GetParameters()[0].ParameterType.Name.StartsWith("IReadModelOf")).Single();
        }


        public object InstanceMatching(ReadModelQueryDescriptor descriptor)
        {
            var readModelType = _typeDiscoverer.FindTypeByFullName(descriptor.GeneratedFrom);
            if (readModelType != null)
            {
                var readModelOfType = typeof(IReadModelOf<>).MakeGenericType(readModelType);
                var readModelOf = _container.Get(readModelOfType);
                var instanceMatchingMethod = readModelOfType.GetMethod("InstanceMatching");

                var genericAuthorizeMethod = _authorizeMethod.MakeGenericMethod(readModelType);
                var authorizationResult = genericAuthorizeMethod.Invoke(_fetchingSecurityManager, new[] { readModelOf }) as AuthorizationResult;
                if (!authorizationResult.IsAuthorized) return null;

                var funcType = typeof(Func<,>).MakeGenericType(readModelType, typeof(bool));
                var expressionType = typeof(Expression<>).MakeGenericType(funcType);
                var expressions = Array.CreateInstance(expressionType, descriptor.PropertyFilters.Count);
                var index=0;
                foreach (var key in descriptor.PropertyFilters.Keys)
                {
                    var expression = GetPropertyEqualsExpression(readModelType, key.ToPascalCase(), descriptor.PropertyFilters[key]);
                    expressions.SetValue(expression, index);
                    index++;
                }

                var result = instanceMatchingMethod.Invoke(readModelOf, new[] { expressions });
                var filtered = _readModelFilters.Filter(new[] { result as IReadModel });
                if (filtered.Count() == 1) return filtered.First();
            }
            return null;
        }

        Expression GetPropertyEqualsExpression(Type type, string propertyName, object value)
        {
            var parameter = Expression.Parameter(type, "o");
            MemberExpression propertyExpression;

            Type targetValueType;

            var property = type.GetProperty(propertyName);
            if (property != null && property.PropertyType.IsConcept())
            {
                var outerMemberAccess = Expression.MakeMemberAccess(parameter, property);
                propertyExpression = Expression.Property(outerMemberAccess, "Value");

                targetValueType = property.PropertyType.GetConceptValueType();
            }
            else
            {
                propertyExpression = Expression.Property(parameter, propertyName);
                targetValueType = property.PropertyType;
            }

            if (value == null)
            {
                try
                {
                    value = Activator.CreateInstance(targetValueType);
                }
                catch { };
            }
            else
            {
                if (value.GetType() != targetValueType)
                {
                    if (targetValueType == typeof(Guid))
                        value = Guid.Parse(value.ToString());
                    else
                        value = Convert.ChangeType(value, targetValueType);
                }
            }

            var body = Expression.Equal(
                            propertyExpression,
                            Expression.Constant(value)
                       );
            var lambda = Expression.Lambda(body, parameter);
            return lambda;
        }
    }
}
