#region License
//
// Copyright (c) 2008-2013, Dolittle (http://www.dolittle.com)
//
// Licensed under the MIT License (http://opensource.org/licenses/MIT)
//
// You may not use this file except in compliance with the License.
// You may obtain a copy of the license at 
//
//   http://github.com/dolittle/Bifrost/blob/master/MIT-LICENSE.txt
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//
#endregion
using System;
using System.Linq.Expressions;
using Bifrost.Execution;
using Bifrost.Extensions;
using Bifrost.Read;
using Bifrost.Concepts;

namespace Bifrost.Web.Read
{
    public class ReadModelService
    {
        readonly ITypeDiscoverer _typeDiscoverer;
        readonly IContainer _container;

        public ReadModelService(ITypeDiscoverer typeDiscoverer, IContainer container)
        {
            _typeDiscoverer = typeDiscoverer;
            _container = container;
        }


        public object InstanceMatching(ReadModelQueryDescriptor descriptor)
        {
            var readModelType = _typeDiscoverer.FindTypeByFullName(descriptor.GeneratedFrom);
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
                    var expression = GetPropertyEqualsExpression(readModelType, key.ToPascalCase(), descriptor.PropertyFilters[key]);
                    expressions.SetValue(expression, index);
                    index++;
                }

                var result = instanceMatchingMethod.Invoke(readModelOf, new[] { expressions });
                return result;
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

            if (value.GetType() != targetValueType)
            {
                if (targetValueType == typeof(Guid))
                    value = Guid.Parse(value.ToString());
                else 
                    value = Convert.ChangeType(value, targetValueType);
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
