using System;
using System.Collections.Generic;
using System.Linq;
using Bifrost.Execution;
using Bifrost.Read;
using Bifrost.Extensions;
using System.Linq.Expressions;

namespace Bifrost.Web.Read
{
    public class MyStuff : IReadModel
    {
        public Guid Id { get; set; }
        public string Something { get; set; }
    }


    public class ReadModelService
    {
        IEnumerable<Type> _readModelTypes;
        IContainer _container;

        public ReadModelService(ITypeDiscoverer typeDiscoverer, IContainer container)
        {
            _container = container;
            _readModelTypes = typeDiscoverer.FindMultiple<IReadModel>();
        }


        public object InstanceMatchingPropertyWithValue(string readModel, string property, string value)
        {
            readModel = readModel.ToPascalCase();
            var readModelType = _readModelTypes.SingleOrDefault(t=>t.Name == readModel);
            var readModelOfType = typeof(IReadModelOf<>).MakeGenericType(readModelType);
            var readModelOf = _container.Get(readModelOfType);
            var instanceMatchingMethod = readModelOfType.GetMethod("InstanceMatching");

            var expression = GetPropertyEqualsExpression(readModelType, property, value);
            var array = Array.CreateInstance(expression.GetType(), 1);
            array.SetValue(expression, 0);
            
            var result = instanceMatchingMethod.Invoke(readModelOf, new object[] { array });

            return result;
        }                                             

        public object InstanceMatching(ReadModelQueryDescriptor descriptor)
        {
            throw new NotImplementedException();
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
