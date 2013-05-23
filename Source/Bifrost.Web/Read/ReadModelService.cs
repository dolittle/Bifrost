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
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Bifrost.Execution;
using Bifrost.Extensions;
using Bifrost.Read;
using Bifrost.Concepts;

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

            var property = type.GetProperty(propertyName);
            if (property != null && property.PropertyType.IsConcept())
            {
                var outerMemberAccess = Expression.MakeMemberAccess(parameter, property);
                propertyExpression = Expression.Property(outerMemberAccess, "Value");
            }
            else
                propertyExpression = Expression.Property(parameter, propertyName);
            

            var body = Expression.Equal(
                            propertyExpression,
                            Expression.Constant(value)
                       );
            var lambda = Expression.Lambda(body, parameter);
            return lambda;
        }
    }
}
