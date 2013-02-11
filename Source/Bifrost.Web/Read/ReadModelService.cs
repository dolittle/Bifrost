using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Bifrost.Execution;
using Bifrost.Extensions;
using Bifrost.Read;

namespace Bifrost.Web.Read
{
    public class ReadModelService
    {
        IEnumerable<Type> _readModelTypes;
        IContainer _container;

        public ReadModelService(ITypeDiscoverer typeDiscoverer, IContainer container)
        {
            _container = container;
            _readModelTypes = typeDiscoverer.FindMultiple<IReadModel>();
        }


        public object InstanceMatching(ReadModelQueryDescriptor descriptor)
        {
            var readModel = descriptor.ReadModel.ToPascalCase();
            var readModelType = _readModelTypes.SingleOrDefault(t => t.Name == readModel);
            if (readModelType != null)
            {
                var readModelOfType = typeof(IReadModelOf<>).MakeGenericType(readModelType);
                var readModelOf = _container.Get(readModelOfType);
                var instanceMatchingMethod = readModelOfType.GetMethod("InstanceMatching");

                var funcType = typeof(Func<,>).MakeGenericType(readModelType, typeof(bool));
                var expressionType = typeof(Expression<>).MakeGenericType(funcType);
                var expressions = Array.CreateInstance(expressionType, descriptor.PropertyFilters.Count);
                var index=0;
                foreach (var key in descriptor.PropertyFilters.Keys)
                {
                    var expression = GetPropertyEqualsExpression(readModelType, key, descriptor.PropertyFilters[key]);
                    expressions.SetValue(expression, index);
                    index++;
                }

                var result = instanceMatchingMethod.Invoke(readModelOf, new[] { expressions });
                return result;
            }
            return null;
        }


        Expression GetPropertyEqualsExpression(Type type, string property, object value)
        {
            var parameter = Expression.Parameter(type, "o");
            var body = Expression.Equal(
                            Expression.PropertyOrField(parameter, property),
                            Expression.Constant(value)
                       );
            var lambda = Expression.Lambda(body, parameter);
            return lambda;
        }
    }
}
